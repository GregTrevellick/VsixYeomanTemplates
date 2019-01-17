@echo off

REM INITIALISE VARIABLES
set arg1YoApp=%1
set "arg1GenYoApp=generator-%arg1YoApp%"
set arg2RegularProjDir=%2
set arg3AssemblyDirectory=%3

REM INSTALL YEOMAN TEMPLATE
cd %arg2RegularProjDir%
setlocal enableDelayedExpansion
call npm install -g yo %arg1GenYoApp%
call yo %arg1YoApp%

REM OPEN VISUAL STUDIO 
cd %arg3AssemblyDirectory%
CommonIdeLauncher.exe %arg2RegularProjDir%
::pause

REM Check for error (e.g. yo command not installed, npm not exists)
IF %ERRORLEVEL% NEQ 0 GOTO ProcessError

REM Success
exit /b 0

:ProcessError
REM Failure
exit /b 1