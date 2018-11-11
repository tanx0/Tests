@echo off
set /p rootUrl=Enter root URL 
nunit3-console ReactAppTest.dll --params=url=%rootUrl%