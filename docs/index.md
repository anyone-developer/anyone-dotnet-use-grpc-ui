# anyone-dotnet-use-grpc-ui

This repo simply help you to hosting a grpcUI portal from referenced application. This repo depends on *gRpc reflection* and this project: https://github.com/fullstorydev/grpcui

I created library for .NET 5 web application. I use **Process** class to run released executable program. And output the information to the logger. You can add *GrpcUI_Url* as Environment Variable.

This repo would bundle nuget package. Anyone can reference it from *nuget.org*. The nuget package only provide a simple way to hosting gRpc UI whenever application runs up. When you debug gRpc application, No need to type in commands every time.

Microsoft doc reference:
https://docs.microsoft.com/en-us/aspnet/core/grpc/test-tools?view=aspnetcore-5.0

nuget package:
https://www.nuget.org/packages/AnyoneDeveloper.Grpc.UI/#

*If you like my module, please buy me a coffee.*

*More and more tiny and useful GitHub action modules are on the way. Please donate to me. I accept a part-time job contract. if you need, please contact me: zhang_nan_163@163.com*

## Be Attention

I tested it in windows 10 x86_64 environment. There is no guarantee that it would run properly in linux or macOS. Official executable program only support x86, x64 architecture. It cannot run in Arm platform. Probably *Apple M1 chip platform* won't work. 

## How to use

### Set up gRPC reflection

- Add a *Grpc.AspNetCore.Server.Reflection* package reference.
- Register reflection in *Startup.cs*
  - *AddGrpcReflection* to register services that enable reflection.
  - *MapGrpcReflectionService* to add a reflection service endpoint.

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddGrpc();
    services.AddGrpcReflection();    //add code
}

public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    app.UseRouting();
    
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapGrpcService<GreeterService>();

        /*add below code*/
        if (env.IsDevelopment())
        {
            endpoints.MapGrpcReflectionService();
        }
    });
}
```

### Add Environment Variable

Library rely on *GrpcUI_Url* variable. Please set applicationUrl as value from *launchSettings.json*.

<img src="https://raw.githubusercontent.com/anyone-developer/anyone-dotnet-use-grpc-ui/main/misc/screenshot1.png" width="500">

### Reference nupkg and use it

reference nupkg from nuget server. just simply adding:

```csharp
if (env.IsDevelopment())
{
  app.UseDeveloperExceptionPage();
  app.UseGrpcUI(factory);       //add
}
```

Every time you debug your application. the grpcui would run in background and prompt up portal page. just like Swagger UI did.

<img src="https://raw.githubusercontent.com/anyone-developer/anyone-dotnet-use-grpc-ui/main/misc/screenshot2.png" width="500">
<img src="https://raw.githubusercontent.com/anyone-developer/anyone-dotnet-use-grpc-ui/main/misc/screenshot3.png" width="500">

## Donation

PalPal: https://paypal.me/nzhang4

<img src="https://raw.githubusercontent.com/anyone-developer/anyone-dotnet-use-grpc-ui/main/misc/alipay.JPG" width="500">

<img src="https://raw.githubusercontent.com/anyone-developer/anyone-dotnet-use-grpc-ui/main/misc/webchat_pay.JPG" width="500">


