@echo off
rem DNA-AND safe test runner.
rem   test.cmd          Look-around test: the game stays open. Every 2 minutes it asks you to press K to keep it
rem                     running; if you don't answer within 15 seconds it closes itself safely.
rem   test.cmd 3000     Quick timed check: runs 3000 ms, then closes.
rem Either way, if the game is still running past its hard limit it is force-closed, which releases the window
rem and the graphics card. The game never changes display settings, so closing it is a full revert.
setlocal
if not exist "%~dp0build\DNA-AND.exe" (
  echo Build first: build.cmd
  exit /b 1
)
if "%1"=="" (
  set ARGS='--try','120'
  set LIMIT=14400
) else (
  set ARGS='--smoke','%1'
  set /a LIMIT=%1/1000+10
)
powershell -NoProfile -ExecutionPolicy Bypass -Command ^
  "$p = Start-Process -FilePath '%~dp0build\DNA-AND.exe' -ArgumentList %ARGS% -WorkingDirectory '%~dp0' -PassThru;" ^
  "if (-not $p.WaitForExit(%LIMIT% * 1000)) { Stop-Process -Id $p.Id -Force; Write-Host 'SAFETY REVERT: test overran its limit, game force-closed'; exit 4 }" ^
  "Write-Host ('Test finished, exit code ' + $p.ExitCode); exit $p.ExitCode"
