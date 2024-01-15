
[assembly: FunctionsStartup(typeof(ProductScrapper.EntryApp.Startup))]
namespace ProductScrapper.EntryApp;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder
            .AddGraphQLFunction()
            .AddQueryType<ScrapperService>();
      
    }
}
