@echo off
rem DNA-AND build: our own C# source, compiled by the C# compiler that ships with Windows.
rem No outside tools, libraries or packages.
setlocal
set CSC=%WINDIR%\Microsoft.NET\Framework64\v4.0.30319\csc.exe
if not exist "%CSC%" (
  echo Windows' built-in C# compiler was not found at %CSC%
  exit /b 1
)
rem   build.cmd       builds build\DNA-AND.exe (the copy you play)
rem   build.cmd dev   builds build\dev\DNA-AND.exe (a separate working copy, so a running game is never closed or touched)
set OUT=build\DNA-AND.exe
if /i "%1"=="dev" set OUT=build\dev\DNA-AND.exe
if not exist build mkdir build
if not exist build\dev mkdir build\dev
"%CSC%" /nologo /target:winexe /platform:x64 /optimize+ /unsafe /warnaserror+ /out:%OUT% /recurse:src\*.cs
if errorlevel 1 (
  echo Build failed.
  exit /b 1
)
echo Built %OUT%
