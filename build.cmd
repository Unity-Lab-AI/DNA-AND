@echo off
rem DNA-AND build: our own C# source, compiled by the C# compiler that ships with Windows.
rem No outside tools, libraries or packages.
setlocal
set CSC=%WINDIR%\Microsoft.NET\Framework64\v4.0.30319\csc.exe
if not exist "%CSC%" (
  echo Windows' built-in C# compiler was not found at %CSC%
  exit /b 1
)
rem   build.cmd        builds build\DNA-AND.exe (the copy you play)
rem   build.cmd NAME   builds build\NAME\DNA-AND.exe (a separate copy, such as dev or test, so a running game is never closed or touched)
rem   build.cmd NAME preview   builds the universe preview release (it always opens in the one-world universe)
set DEF=
if "%2"=="preview" set DEF=/define:PREVIEW
set OUT=build\DNA-AND.exe
if not "%1"=="" set OUT=build\%1\DNA-AND.exe
if not exist build mkdir build
if not "%1"=="" if not exist build\%1 mkdir build\%1
"%CSC%" /nologo /target:winexe /platform:x64 /optimize+ /unsafe /warnaserror+ %DEF% /win32icon:installer\dna-and.ico /out:%OUT% /resource:docs\release\UNIVERSE-README.md,universe-readme.md /resource:docs\release\UNIVERSE-HOW-TO-BUILD.md,how-to-build.md /resource:docs\release\IN-GAME-ABOUT.md,about.md /resource:docs\release\IN-GAME-README.md,readme.md /resource:docs\release\IN-GAME-HOW-IT-WORKS.md,how-it-works.md /resource:docs\release\universe\CREDITS.md,credits.md /resource:docs\release\UNIVERSE-GUIDE.md,guide.md /recurse:src\*.cs
if errorlevel 1 (
  echo Build failed.
  exit /b 1
)
echo Built %OUT%
