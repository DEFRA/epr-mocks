using Azure.Storage.Blobs;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(EPR.Mock.Antivirus.Functions.Startup))]

namespace EPR.Mock.Antivirus.Functions;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        var configuration = builder.GetContext().Configuration;
        builder.Services.AddSingleton(x =>
        {
            var connectionString = configuration.GetValue<string>("AzureWebJobsStorage");
            var container = configuration.GetValue<string>("ContainerName");
            return new BlobContainerClient(connectionString, container);
        });
    }
}