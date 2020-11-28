# Byndyusoft.AspNetCore.Cors
Extends [Microsoft.AspNetCore.Cors](https://www.nuget.org/packages/Microsoft.AspNetCore.Cors) and allows `AllowsAnyOrigin` and `AllowsCredentials` policy options to be used together.

[![(License)](https://img.shields.io/github/license/Byndyusoft/Byndyusoft.AspNetCore.Cors.svg)](LICENSE.txt)
[![Nuget](http://img.shields.io/nuget/v/Byndyusoft.AspNetCore.Cors.svg?maxAge=10800)](https://www.nuget.org/packages/Byndyusoft.AspNetCore.Cors/) [![NuGet downloads](https://img.shields.io/nuget/dt/Byndyusoft.AspNetCore.Cors.svg)](https://www.nuget.org/packages/Byndyusoft.AspNetCore.Cors/) 

Browser security prevents a web page from making requests to a different domain than the one that served the web page. 
This restriction is called the same-origin policy. The same-origin policy prevents a malicious site from reading sensitive data from another site. 
Sometimes, you might want to allow other sites to make cross-origin requests to your app. For more information, see the [Mozilla CORS article](https://developer.mozilla.org/ru/docs/Web/HTTP/CORS).

Starting with APP.NET Core 3.0, Microsoft has prevented the `AllowsAnyOrigin` and `AllowsCredentials` policy options from being used together.
You can read about the reasons for this decision in the corresponding article [Enable CORS in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/security/cors#set-the-allowed-origins).
This package allows you `AllowsAnyOrigin` and `AllowsCredentials` policy options to be used together again.

## Usage

Package extends `IApplicationBuilder` and `IServiceCollection` with `UseInsecureCors` methods with the same signatures than the standard `UseCors` ones.

```csharp
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {   
        services.UseInsecureCors();
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseRouting();
        app.UseInsecureCors(cors => 
		cors
			.AllowAnyOrigin()
			.AllowAnyHeader()
			.AllowAnyMethod()
			.AllowCredentials());
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
} 
```


## Installing

```shell
dotnet add package Byndyusoft.AspNetCore.Cors
```

# Contributing

To contribute, you will need to setup your local environment, see [prerequisites](#prerequisites). For the contribution and workflow guide, see [package development lifecycle](#package-development-lifecycle).

A detailed overview on how to contribute can be found in the [contributing guide](CONTRIBUTING.md).

## Prerequisites

Make sure you have installed all of the following prerequisites on your development machine:

- Git - [Download & Install Git](https://git-scm.com/downloads). OSX and Linux machines typically have this already installed.
- .NET Core (version 5.0 or higher) - [Download & Install .NET Core](https://dotnet.microsoft.com/download/dotnet-core/5.0).

## General folders layout

### src
- source code

### tests

- unit-tests

## Package development lifecycle

- Implement package logic in `src`
- Add or addapt unit-tests (prefer before and simultaneously with coding) in `tests`
- Add or change the documentation as needed
- Open pull request in the correct branch. Target the project's `master` branch

# Maintainers

[github.maintain@byndyusoft.com](mailto:github.maintain@byndyusoft.com)