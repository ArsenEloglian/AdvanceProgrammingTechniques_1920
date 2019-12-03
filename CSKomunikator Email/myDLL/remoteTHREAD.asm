.386
.model flat, stdcall
option casemap :none
      include \masm32\include\windows.inc
      include \masm32\include\user32.inc
      include \masm32\include\kernel32.inc
      
      includelib \masm32\lib\user32.lib
      includelib \masm32\lib\kernel32.lib
      includelib myDLL.lib

.DATA
msg2 db "here remote thread",0
msg3 db "exec remote thread",0
.CODE
remoteTHREAD proc
  invoke MessageBox,0,addr msg2,addr msg2,MB_OK
RET
remoteTHREAD endp
START:
  invoke MessageBox,0,addr msg3,addr msg3,MB_OK
  invoke ExitProcess,0
END START
