namespace ProductScrapper.Infrastructure.Services;
public class ScrapperService : IScrapperService
{
    private static readonly HttpClient _httpClient = new(new HttpClientHandler
    {
        AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
    });

    public async Task<List<Response>> ScrapeUrlAsync(string url)
    {
        try
        {
            ConfigureHttpClientHeaders(_httpClient);

            var content = await GetHtmlContentAsync(_httpClient, url);
            content = StripScriptsAndStyles(content);

            var htmlDocument = LoadHtmlDocument(content);

            var list = ExtractTableData(htmlDocument);
            list.AddRange(ExtractUnorderedListData(htmlDocument));

            return list;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Error while scraping: {ex.Message}", ex);
        }
    }

    private static void ConfigureHttpClientHeaders(HttpClient client)
    {
        client.DefaultRequestHeaders.Clear();
        client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "PostmanRuntime/7.36.1");
        client.DefaultRequestHeaders.TryAddWithoutValidation("Connection", "keep-alive");
        client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "*/*");
        client.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate");
    }

    private static async Task<string> GetHtmlContentAsync(HttpClient client, string url)
    {
        using var response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    private static HtmlDocument LoadHtmlDocument(string content)
    {
        var doc = new HtmlDocument();
        doc.LoadHtml(content);
        return doc;
    }

    private static List<Response> ExtractTableData(HtmlDocument htmlDocument)
    {
        var list = new List<Response>();

        var tables = htmlDocument.DocumentNode.SelectNodes("//table");

        if (tables != null)
        {
            foreach (var table in tables)
            {
                var rows = table.SelectNodes("tr");

                // Check if rows is not null
                if (rows != null)
                {
                    // Iterate through each row
                    foreach (var row in rows)
                    {
                        // Select the header cell (th) and data cell (td)
                        var th = row.SelectSingleNode("th[@class='a-color-secondary a-size-base prodDetSectionEntry']");
                        var td = row.SelectSingleNode("td[@class='a-size-base prodDetAttrValue']");

                        if (th != null && td != null)
                        {
                            // Assuming the content of th is the tag and the content of td is the value
                            string tag = th.InnerText.Trim();
                            string value = td.InnerText.Trim();

                            // Create a new Response object and add it to the list
                            list.Add(new Response { Tag = tag, Value = value });
                        }
                    }
                }
            }
        }

        return list;
    }

    private static List<Response> ExtractUnorderedListData(HtmlDocument htmlDocument)
    {
        var list = new List<Response>();

        var unorderedLists = htmlDocument.DocumentNode.SelectNodes("//ul");

        if (unorderedLists != null)
        {
            foreach (var ul in unorderedLists)
            {

                // Select all list items (li) within the unordered list (ul)
                var listItems = ul.SelectNodes("li");

                if (listItems != null)
                {
                    // List to store the mapped data

                    // Iterate through each list item
                    foreach (var listItem in listItems)
                    {
                        // Select the two spans within the list item
                        var spans = listItem.SelectNodes("span");

                        if (spans != null && spans.Count == 2)
                        {
                            // Extract tag and value from the spans
                            string tag = spans[0].InnerText.Trim();
                            string value = spans[1].InnerText.Trim();

                            // Create a new Response object and add it to the list
                            list.Add(new Response { Tag = tag, Value = value });
                        }
                    }
                }
            }
        }

        return list;
    }

    public static string StripScriptsAndStyles(string html)
    {
        var doc = new HtmlDocument();
        doc.LoadHtml(html);

        var nodesToRemove = doc.DocumentNode.SelectNodes("//script|//style");
        if (nodesToRemove != null)
        {
            foreach (HtmlNode node in nodesToRemove)
            {
                node.ParentNode.RemoveChild(node);
            }
        }

        return doc.DocumentNode.OuterHtml;
    }
}

