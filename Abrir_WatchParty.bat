@echo off
set "P=%~dp0WatchParty.html"
set "P=%P:\=/%"
start "" chrome --app="file:///%P%" --disable-renderer-backgrounding --disable-backgrounding-occluded-windows --disable-features=CalculateNativeWinOcclusion --autoplay-policy=no-user-gesture-required --disable-background-mode 2>nul || start "" msedge --app="file:///%P%" --disable-renderer-backgrounding --disable-backgrounding-occluded-windows --disable-features=CalculateNativeWinOcclusion --autoplay-policy=no-user-gesture-required 2>nul || start "" "%~dp0WatchParty.html"
