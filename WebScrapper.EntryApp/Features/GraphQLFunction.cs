namespace WebScrapper.EntryApp.Features;

public static class GraphQLFunction
{
    [FunctionName("GraphQLHttpFunction")]
    public static Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "graphql/{**slug}")]
        HttpRequest request,
        [GraphQL]
        IGraphQLRequestExecutor executor)
        => executor.ExecuteAsync(request);
}