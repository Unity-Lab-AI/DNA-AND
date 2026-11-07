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
copy /y "%~dp0docs\release\universe\README.txt" "%U%\docs\README.txt" > nul
copy /y "%~dp0docs\release\universe\CREDITS.md" "%U%\CREDITS.md" > nul
copy /y "%~dp0docs\release\universe\build.cmd" "%U%\build.cmd" > nul
copy /y "%~dp0docs\release\universe\release.cmd" "%U%\release.cmd" > nul
> "%U%\.gitignore" echo build/
>> "%U%\.gitignore" echo release/
echo Universe synced into %U%
