using WebScrapper.Infrastructure.Services;

[assembly: FunctionsStartup(typeof(Startup))]
namespace WebScrapper.EntryApp;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder
            .AddGraphQLFunction()
            .AddQueryType<ScrapperService>();
      
    }
}
