@echo off
rem Fills the Universe project (Universe\, its own git repo, never part of DNA-AND's) with what builds the universe and
rem only its own documents: the source, the content, the installer, its readme, how-to-build, players' readme and credits,
rem and its own build and release scripts. DNA-AND's design notes, TODO, wiki and workflow files never go there.
setlocal
set U=%~dp0Universe
if not exist "%U%" mkdir "%U%"
if exist "%U%\src" rmdir /s /q "%U%\src"
if exist "%U%\content" rmdir /s /q "%U%\content"
if exist "%U%\installer" rmdir /s /q "%U%\installer"
xcopy /e /i /q /y "%~dp0src" "%U%\src" > nul
xcopy /e /i /q /y "%~dp0content" "%U%\content" > nul
xcopy /e /i /q /y "%~dp0installer" "%U%\installer" > nul
if not exist "%U%\docs" mkdir "%U%\docs"
copy /y "%~dp0docs\release\UNIVERSE-README.md" "%U%\README.md" > nul
copy /y "%~dp0docs\release\UNIVERSE-HOW-TO-BUILD.md" "%U%\docs\HOW-TO-BUILD.md" > nul
copy /y "%~dp0docs\release\UNIVERSE-GUIDE.md" "%U%\docs\GUIDE.md" > nul
copy /y "%~dp0docs\release\universe\README.txt" "%U%\docs\README.txt" > nul
copy /y "%~dp0docs\release\universe\CREDITS.md" "%U%\CREDITS.md" > nul
if not exist "%U%\docs\in-game" mkdir "%U%\docs\in-game"
copy /y "%~dp0docs\release\IN-GAME-ABOUT.md" "%U%\docs\in-game\ABOUT.md" > nul
copy /y "%~dp0docs\release\IN-GAME-README.md" "%U%\docs\in-game\README.md" > nul
copy /y "%~dp0docs\release\IN-GAME-HOW-IT-WORKS.md" "%U%\docs\in-game\HOW-IT-WORKS.md" > nul
copy /y "%~dp0docs\release\LICENSE.txt" "%U%\LICENSE.txt" > nul
copy /y "%~dp0docs\release\PRIVACY.txt" "%U%\PRIVACY.txt" > nul
copy /y "%~dp0docs\release\universe\build.cmd" "%U%\build.cmd" > nul
copy /y "%~dp0docs\release\universe\release.cmd" "%U%\release.cmd" > nul
rem DNA-AND's own tooling and icon stay out of it
if exist "%U%\installer\universe-names.ps1" del "%U%\installer\universe-names.ps1"
if exist "%U%\installer\dna-and.ico" del "%U%\installer\dna-and.ico"
rem its own name throughout its code and content (namespace, file headers, content labels), never DNA-AND's
powershell -NoProfile -ExecutionPolicy Bypass -File "%~dp0installer\universe-names.ps1" "%U%" || exit /b 1
> "%U%\.gitignore" echo build/
>> "%U%\.gitignore" echo release/
echo Universe synced into %U%
