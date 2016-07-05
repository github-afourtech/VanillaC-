cd %NUNIT_HOME%\bin
pause
nunit-console.exe /fixture=Vanilla.TestSuite.TestSuite %ASSEMBLIES_DIR%\Vanilla.dll
pause
cd %ALLURE_CLI_HOME% 
allure report generate %OUTPUT_DIR%
pause
allure report
