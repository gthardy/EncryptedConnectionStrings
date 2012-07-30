@ECHO OFF
SET ASPNETREGIIS="C:\Windows\Microsoft.NET\Framework\v4.0.30319\aspnet_regiis.exe"
ECHO Encrypting app.config connectionStrings ...
%ASPNETREGIIS% -pef "connectionStrings" ".\EncryptedConnectionStrings" -prov "DataProtectionConfigurationProvider"
ECHO Finished Encrypting app.config connectionStrings...
pause

