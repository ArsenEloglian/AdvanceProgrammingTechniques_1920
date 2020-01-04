@echo off
ML /c /coff ring0.asm
LINK *.obj /SUBSYSTEM:NATIVE /ALIGN:0X20 /OUT:ring0.sys /BASE:0X10000 /DRIVER /def:ring0.def
DEL *.obj
DEL *.exp
DEL ring0.lib