using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Nancy.Diagnostics;
using Nancy.Configuration;
using Nancy;

namespace NancyApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseKestrel()
                .UseStartup<Startup>()
                .Build();
 
            host.Run();
        }
    }

    public class CustomBootStrapper : DefaultNancyBootstrapper {
        public override void Configure(INancyEnvironment environment)
        {
            environment.Tracing(enabled: false, displayErrorTraces: true);
            environment.Diagnostics(true, "password");
            base.Configure(environment);
        }
    } 
}