@echo off
rem DNA-AND release: builds the universe preview and its Windows installer, using only what comes with Windows
rem (its C# compiler and PowerShell's zip). No outside tools.
rem   release.cmd   ->  release\DNA-AND-Setup-0.1.2.exe  and  release\DNA-AND-0.1.2-windows.zip
setlocal
set VER=0.1.2
set CSC=%WINDIR%\Microsoft.NET\Framework64\v4.0.30319\csc.exe
set FW=%WINDIR%\Microsoft.NET\Framework64\v4.0.30319
call "%~dp0build.cmd" preview-release preview || exit /b 1
set STAGE=%~dp0release\stage
if exist "%STAGE%" rmdir /s /q "%STAGE%"
mkdir "%STAGE%"
copy /y "%~dp0build\preview-release\DNA-AND.exe" "%STAGE%\DNA-AND.exe" > nul
xcopy /e /i /q /y "%~dp0content" "%STAGE%\content" > nul
copy /y "%~dp0docs\release\README.txt" "%STAGE%\README.txt" > nul
copy /y "%~dp0LICENSE.txt" "%STAGE%\LICENSE.txt" > nul
copy /y "%~dp0PRIVACY.txt" "%STAGE%\PRIVACY.txt" > nul
rem the public docs: what the universe is (plain words) and how to build, change and ship your own
mkdir "%STAGE%\docs"
copy /y "%~dp0docs\release\UNIVERSE-README.md" "%STAGE%\docs\README.md" > nul
copy /y "%~dp0docs\release\UNIVERSE-HOW-TO-BUILD.md" "%STAGE%\docs\HOW-TO-BUILD.md" > nul
copy /y "%~dp0docs\release\UNIVERSE-GUIDE.md" "%STAGE%\docs\GUIDE.md" > nul
copy /y "%~dp0docs\release\UNIVERSE-LINUX.md" "%STAGE%\docs\LINUX.md" > nul
copy /y "%~dp0linux\run.sh" "%STAGE%\run-on-linux.sh" > nul
if exist "%~dp0release\payload.zip" del "%~dp0release\payload.zip"
powershell -NoProfile -Command "Compress-Archive -Path '%STAGE%\*' -DestinationPath '%~dp0release\payload.zip' -Force" || exit /b 1
copy /y "%~dp0release\payload.zip" "%~dp0release\DNA-AND-%VER%-windows.zip" > nul
"%CSC%" /nologo /target:winexe /platform:x64 /optimize+ /warnaserror+ /win32icon:"%~dp0installer\dna-and.ico" /out:"%~dp0release\DNA-AND-Setup-%VER%.exe" /resource:"%~dp0release\payload.zip",payload.zip /r:"%FW%\System.IO.Compression.dll" /r:"%FW%\System.IO.Compression.FileSystem.dll" /r:System.Windows.Forms.dll /r:System.Drawing.dll "%~dp0installer\Setup.cs" || exit /b 1
rmdir /s /q "%STAGE%"
del "%~dp0release\payload.zip"
echo Built release\DNA-AND-Setup-%VER%.exe and release\DNA-AND-%VER%-windows.zip
