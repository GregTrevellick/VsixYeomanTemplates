@echo off

set arg1YoApp=%1
set arg2RegularProjDir=%2
set "arg3GenYoApp=generator-%arg1YoApp%"

cd %arg2RegularProjDir%

setlocal enableDelayedExpansion

call npm install -g yo %arg3GenYoApp%

call yo %arg1YoApp%

::build "array" of .csproj files in sub-folders
set folderCnt=0

rem /F = loop thru files
rem /b = bare format output of dir
rem /o-d = sort output of dir, -d means sort by newest date 
rem /s = trawl sub-directories
rem /tc = sort by time created
for /f "eol=: delims=" %%F in ('dir /b/o-d/s/tc *.csproj') do (
  set /a folderCnt+=1
  set "folder!folderCnt!=%%F"
)

:: Open the last .csproj in "array" which is the .csproj file we just created (ideally we would open the newly generated project in the existing instance of Visual Studio, but the closest I got to this was "C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\Common7\IDE\devenv.exe" /edit C:\temp\j2\j2.csproj which opens the .csproj file in existing VS instance (not the experimental one), but as a text file not as a project file)
!folder%folderCnt%!
rem gregt is this really the proj just created ???













rem pause
rem set foo="bar"
rem echo %foo%
rem https://stackoverflow.com/questions/10544646/dir-output-into-bat-array