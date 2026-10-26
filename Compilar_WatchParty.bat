@echo off
rem Recompila o WatchParty.exe (coloque este arquivo na mesma pasta de WatchParty.html, Launcher.cs e icone.ico)
rem Usa o compilador C# que já vem no Windows (.NET Framework 4) - nao precisa instalar nada.
cd /d "%~dp0"
set "CSC=%WINDIR%\Microsoft.NET\Framework64\v4.0.30319\csc.exe"
if not exist "%CSC%" set "CSC=%WINDIR%\Microsoft.NET\Framework\v4.0.30319\csc.exe"
"%CSC%" /nologo /target:winexe /out:WatchParty.exe /win32icon:icone.ico /resource:WatchParty.html,WatchParty.html /r:System.Windows.Forms.dll Launcher.cs
if errorlevel 1 (echo Deu erro ao compilar.) else (echo Pronto: WatchParty.exe criado.)
pause
