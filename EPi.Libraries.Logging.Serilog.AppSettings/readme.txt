
The settings were added to your appsettings:

<add key="serilog:minimum-level" value="Error" />
<add key="serilog:write-to:RollingFile.pathFormat" value="C:\Logs\myapp-{Date}.txt" />
<add key="serilog:write-to:RollingFile.retainedFileCountLimit" value="10" />

See https://github.com/serilog/serilog/wiki/AppSettings for all configuration options.

