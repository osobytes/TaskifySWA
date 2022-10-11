using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using Taskify.AzureTables;
using TaskifyAPI.Managers;

[assembly: FunctionsStartup(typeof(Taskify.Api.Startup))]
namespace Taskify.Api
{
  public class Startup : FunctionsStartup
  {
    private const string AzureWebJobsStorage = nameof(AzureWebJobsStorage);

    public override void Configure(IFunctionsHostBuilder builder)
    {
      // Add Azure storage configuration.
      builder.Services.UseAzureStorage((config) =>
      {
        config.UseConnectionString(Environment.GetEnvironmentVariable(AzureWebJobsStorage));
      });

      // Add our Manager.
      // NOTE: Scoped, represents the lifespan of our manager.
      // There can be Scoped, Transient, and Singleton lifespan for dependencies.
      // More info: https://docs.microsoft.com/en-us/dotnet/core/extensions/dependency-injection-usage
      builder.Services.AddScoped<ITaskifyManager, TaskifyManager>();
    }
  }
}
