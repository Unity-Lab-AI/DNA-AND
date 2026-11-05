@echo off
rem DNA-AND build: our own C# source, compiled by the C# compiler that ships with Windows.
rem No outside tools, libraries or packages.
setlocal
set CSC=%WINDIR%\Microsoft.NET\Framework64\v4.0.30319\csc.exe
if not exist "%CSC%" (
  echo Windows' built-in C# compiler was not found at %CSC%
  exit /b 1
)
if not exist build mkdir build
"%CSC%" /nologo /target:winexe /platform:x64 /optimize+ /unsafe /warnaserror+ /out:build\DNA-AND.exe /recurse:src\*.cs
if errorlevel 1 (
  echo Build failed.
  exit /b 1
)
echo Built build\DNA-AND.exe
