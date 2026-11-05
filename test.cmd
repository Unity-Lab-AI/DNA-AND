@echo off
rem DNA-AND safe test runner.
rem Runs the game for a short timed test. If it is still running after the hard limit,
rem it is force-closed, which releases the window and the graphics card.
rem The game never changes display settings, so closing it is a full revert.
rem Usage: test.cmd [milliseconds]   (default 2000)
setlocal
set MS=%1
if "%MS%"=="" set MS=2000
set /a LIMIT=%MS%/1000+10
if not exist "%~dp0build\DNA-AND.exe" (
  echo Build first: build.cmd
  exit /b 1
)
powershell -NoProfile -ExecutionPolicy Bypass -Command ^
  "$p = Start-Process -FilePath '%~dp0build\DNA-AND.exe' -ArgumentList '--smoke','%MS%' -WorkingDirectory '%~dp0' -PassThru;" ^
  "if (-not $p.WaitForExit(%LIMIT% * 1000)) { Stop-Process -Id $p.Id -Force; Write-Host 'SAFETY REVERT: test overran %LIMIT% s, game force-closed'; exit 4 }" ^
  "Write-Host ('Test finished, exit code ' + $p.ExitCode); exit $p.ExitCode"
