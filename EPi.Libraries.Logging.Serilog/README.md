# EPi.Libraries.Logging.Serilog

[![Build status](https://ci.appveyor.com/api/projects/status/dc0ds3aafwfui3a3/branch/master?svg=true)](https://ci.appveyor.com/project/jstemerdink/epi-libraries-logging-serilog/branch/master)
[![GitHub version](https://badge.fury.io/gh/jstemerdink%2FEPi.Libraries.Logging.Serilog.svg)](http://badge.fury.io/gh/jstemerdink%2FEPi.Libraries.Logging.Serilog)
[![Platform](https://img.shields.io/badge/platform-.NET%204.6.1-blue.svg?style=flat)](https://msdn.microsoft.com/en-us/library/w0x726c2%28v=vs.110%29.aspx)
[![Platform](https://img.shields.io/badge/EPiServer-%2011.0.0-orange.svg?style=flat)](http://world.episerver.com/cms/)
[![GitHub license](https://img.shields.io/badge/license-MIT%20license-blue.svg?style=flat)](LICENSE)

## About
This will add Serilog logging to your EPiServer site.
See [Serilog wiki](https://github.com/serilog/serilog/wiki/ for information about Serilog.

You will need a Logger configuration. You can either use my [AppSettings provider](../EPi.Libraries.Logging.Serilog.AppSettings/README.md), so you can configure Serilog in the AppSettings
or you can create your own implementation.

```C#
[ServiceConfiguration(ServiceType = typeof(ILoggerConfigurator), Lifecycle = ServiceInstanceScope.Singleton)]
public class LoggerConfigurator : ILoggerConfigurator
{
    public ILogger GetLogger()
    {
        return your own configuration here;
    }
}
```


> *Powered by ReSharper*
> [![image](https://i0.wp.com/jstemerdink.files.wordpress.com/2017/08/logo_resharper.png)](http://jetbrains.com)

