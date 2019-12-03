@echo off

if exist remoteTHREAD.obj del remoteTHREAD.obj
if exist remoteTHREAD.exp del remoteTHREAD.exp
if exist remoteTHREAD.lib del remoteTHREAD.lib

\masm32\bin\ml /c /coff remoteTHREAD.asm
\masm32\bin\Link /SUBSYSTEM:WINDOWS /DEF:remoteTHREAD.def remoteTHREAD.obj

if exist remoteTHREAD.obj del remoteTHREAD.obj
if exist remoteTHREAD.exp del remoteTHREAD.exp
if exist remoteTHREAD.lib del remoteTHREAD.lib
dir callDLL.*
pause