.586P
.MODEL FLAT, STDCALL
OPTION      CASEMAP:NONE
UNICODE     = 0
ARGUMENTS   = 1

INCLUDELIB  iNTOSKRNL.LIB
INCLUDELIB  iKERNEL32.LIB

ASSUME FS:NOTHING
.CODE
;--------------------------------------------------------------------------
Device_name                       dw '\','D','e','v','i','c','e','\','r','i','n','g','0',0
Device_type                       dw '\','D','o','s','D','e','v','i','c','e','s','\','r','i','n','g','0',0
;--------------------------------------------------------------------------
align 4

myMOV            MACRO   R32, Win32API
                EXTERN  C _imp__&Win32API :DWORD
                MOV     R32, _imp__&Win32API
                ENDM  

myWin32	MACRO	Win32API,p00,p01,p02,p03,p04,p05,p06,p07,p08,p09,p10,p11,p12,p13,p14,p15,p16,p17,p18,p19
		IRP pxx,<p19,p18,p17,p16,p15,p14,p13,p12,p11,p10,p09,p08,p07,p06,p05,p04,p03,p02,p01,p00>
		 IFNB <pxx>
                  PUSHD pxx
		 ENDIF
ENDM
EXTERN C _imp__&Win32API:DWORD
	CALL	_imp__&Win32API
ENDM
;--------------------------------------------------------------------------
DriverEntry:	; pDriverObject, pusRegistryPath

SymbolicLinkName = dword ptr -14h
DeviceName       = dword ptr -0Ch
DeviceObject     = dword ptr -4
DriverObject     = dword ptr  8

         push    ebp
         mov     ebp,esp
         sub     esp,14h
         push    ebx
         push    esi
         myMOV    esi,RtlInitUnicodeString
         lea     eax,[ebp+DeviceName]

         push    offset Device_name
         push    eax
         call    esi ; RtlInitUnicodeString

         mov     ebx,[ebp+DriverObject] ; DRIVER_OBJECT
         lea     eax,[ebp+DeviceObject] ; DEVICE_OBJECT

         push    eax                  ;DeviceObject
         push    0                    ;Exclusive
         push    0                    ;DeviceCharacteristics
         lea     eax,[ebp+DeviceName]
         push    22h                  ;DeviceType FILE_DEVICE_UNKNOWN
         push    eax                  ;DeviceName
         push    0                    ;DeviceExtensionSize
         push    ebx                  ;pDriverObject
         myWin32 IoCreateDevice
         test    eax,eax
         jnz     Exit_on_failed_creation

         lea     eax, [ebp+SymbolicLinkName] ; SymbolicLinkName
         push    offset Device_type
         push    eax
         call    esi ; RtlInitUnicodeString

         lea     eax, [ebp+DeviceName]
         push    eax
         lea     eax, [ebp+SymbolicLinkName]
         push    eax
         myWin32 IoCreateSymbolicLink
         mov     esi, eax
         test    esi, esi
         jz      Symbolic_link_success

         push    [ebp+DeviceObject]
         myWin32 IoDeleteDevice

         mov     eax, esi
         jmp     Exit_on_failed_creation
;--------------------------------------------------------------------------
Symbolic_link_success:
         mov     dword ptr [ebx+34h], offset UnloadDriver       ; DRIVER_OBJECT.PDRIVER_UNLOAD
         mov     dword ptr [ebx+38h], offset RequestHandler     ; DRIVER_OBJECT.PDISPATCH_IRP_MJ_CREATE
         mov     dword ptr [ebx+40h], offset RequestHandler     ; DRIVER_OBJECT.PDISPATCH_IRP_MJ_CLOSE
         mov     dword ptr [ebx+70h], offset ServiceHandler     ; DRIVER_OBJECT.PDISPATCH_IRP_MJ_DEVICE_CONTROL
         nop     ; << important!
         call    initmysys
;--------------------------------------------------------------------------
Exit_on_failed_creation:
         pop     esi
         pop     ebx
         leave
         retn    8
;------------------------.IRP.PROCESS.------------------------------------
RequestHandler:
         mov     ecx, [esp+8]
         xor     dl, dl
         and     dword ptr [ecx+18h], 0 ; _IRP.IoStatus.IO_STATUS_BLOCK.Status < STATUS_SUCCESS 
         and     dword ptr [ecx+1Ch], 0 ; _IRP.IoStatus.IO_STATUS_BLOCK.Information < nowt
         myWin32 IofCompleteRequest
         xor     eax, eax
         retn    8
;--------------------------------------------------------------------------
initmysys:
 xor eax,eax
ret
;--------------------------------------------------------------------------
failload:
 mov eax,1
ret
; --------------------------------------------------------------------------
ServiceHandler:		; pDeviceObject, pIrp
         push    ebp
         mov     ebp, esp
         push    ebx
         mov     ebx, [ebp+0ch] ; pIrp
         push    esi
         mov     edx, [ebx+60h] ; _IRP.Tail.Overlay.CurrentStackLocation IO_STACK_LOCATION.DeviceIoControl
         mov     [ebp+0ch], edx
         mov     eax,[edx+0Ch]  ;  DeviceIoDeviceIoControl.IoControlCode
         cmp     eax, 99
         jne     SH_OK
         push    ecx
         push    edx
         push    dword ptr [ebx+0ch] ; _IRP.SystemBuffer
         myWin32 MmIsAddressValid    
         pop     edx
         pop     ecx
         cmp     AL,1
         jne     SH_FAIL       ; invalid address
         mov     eax,[ebx+0ch] ; _IRP.SystemBuffer
         mov     eax,[eax] ; input buffer mapped to ring0 memory
;process passed data

         mov     eax,[edx+08h]  ; DeviceIoControl.InputBufferLength
         mov     ecx,[edx+04h]  ; DeviceIoControl.OutputBufferLength
         mov     edi,[ebx+0ch]  ; _IRP.SystemBuffer
         mov     [ebx+1Ch],ecx  ; _IRP.IoStatus+4 ? size to write
         mov     dword ptr [edi],'eonN' ; None, no service existed
         xor     esi,esi
    
SH_FAIL:
         xor     dl, dl
         mov     ecx, ebx
         myWin32 IofCompleteRequest
         xor     eax,eax
         inc     eax    ; unhandled
         jmp     Exit_SH

SH_OK:
         xor     dl, dl
         mov     ecx, ebx
         myWin32 IofCompleteRequest
         xor     eax,eax  ; ok

Exit_SH:
         pop     esi
         pop     ebx
         pop     ebp
         retn    8
;--------------------------------------------------------------------------
UnloadDriver:
         push    ebp
         mov     ebp, esp
         push    ecx
         push    ecx
         mov     eax, [ebp+8]
         push    dword ptr [eax+4]
         myWin32 IoDeleteDevice

         lea     eax, [ebp-8]
         push    offset  Device_type
         push    eax
         myWin32  RtlInitUnicodeString

         lea     eax, [ebp-8]
         push    eax
         myWin32  IoDeleteSymbolicLink

         leave
         retn    4
END DriverEntry
; --------------------------------------------------------------------------