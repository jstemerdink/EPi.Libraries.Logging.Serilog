
The settings were added to your appsettings:

<add key="serilog:minimum-level" value="Error" />

If you want to use e.g. a variable "basepath" in your pathformat,  you can add "Environment.SetEnvironmentVariable("BASEPATH", AppDomain.CurrentDomain.BaseDirectory);" to the Application_Start in the global.asax

See https://github.com/serilog/serilog/wiki/AppSettings for all configuration options.

