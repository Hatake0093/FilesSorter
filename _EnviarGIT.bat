@echo off
setlocal

rem Obtener la fecha en formato YYYY-MM-DD
for /f "tokens=2 delims==" %%I in ('wmic os get localdatetime /value') do set datetime=%%I
set year=%datetime:~0,4%
set month=%datetime:~4,2%
set day=%datetime:~6,2%
set date=%year%-%month%-%day%

rem Formatear el mensaje de commit
set message="cambios de %date%"

rem Ejecutar comandos Git
git add .
git commit -m %message%
git push origin main

pause
