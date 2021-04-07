using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace AnyoneDeveloper.Grpc.UI
{
    public static class IWebHostBuilderExtensions
    {
        private static Process process = new Process();

        public static IApplicationBuilder UseGrpcUI(this IApplicationBuilder app, ILoggerFactory factory)
        {
            var logger = factory.CreateLogger("GrpcUI");

            var appUrl = Environment.GetEnvironmentVariable("GrpcUI_Url") ?? string.Empty;
            var arch = RuntimeInformation.OSArchitecture switch
            {
                Architecture.X86 => "x86_32",
                Architecture.X64 => "x86_64",
                _ => throw new NotSupportedException($"Architecture {Enum.GetName(RuntimeInformation.OSArchitecture)} not supported"),
            };

            var os = "windows";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                os = "osx";
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                os = "linux";

            process.StartInfo.FileName = $"grpcui_{os}_{arch}{(RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? ".exe" : string.Empty)}";
            process.StartInfo.Arguments = $"-plaintext {appUrl}";

            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            logger.LogInformation($"{typeof(IWebHostBuilderExtensions).FullName}");
            logger.LogInformation($"Now GrpcUI Listening on: {appUrl}");

            process.OutputDataReceived += (sender, data) => {
                logger.LogInformation(data.Data);
            };
            process.StartInfo.RedirectStandardError = true;
            process.ErrorDataReceived += (sender, data) => {
                logger.LogError(data.Data);
            };
            process.Start();
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(TerminateProcess);
                
            return app;
        }

        private static void TerminateProcess(object? sender, EventArgs e)
        {
            process.Kill(true);
        }
    }
}
