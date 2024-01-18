namespace ProductScrapper.Infrastructure.Services;
public class ScrapperService : IScrapperService
{
    public async Task<List<Response>> ScrapeUrlAsync(string url)
    {
        try
        {
            HttpClientHandler handler = new()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };
            var client = new HttpClient(handler);
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "PostmanRuntime/7.36.1");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Connection", "keep-alive");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "*/*");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();

            content = StripScriptsAndStyles(content);
            content = content.Replace("&lrm;", string.Empty);

            HtmlDocument htmlDocument = new();

            htmlDocument.LoadHtml(content);
            var list = new List<Response>();
            var h1WithTitle = htmlDocument.DocumentNode.SelectSingleNode("//h1[@id='title']");

            list.Add(new Response { Tag = h1WithTitle.Name, Value = h1WithTitle.InnerText });


            var tables = htmlDocument.DocumentNode.SelectNodes("//table");

            if (tables != null)
            {
                // Iterate through each table
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
        catch (Exception ex)
        {
            throw new InvalidOperationException(ex.Message);
        }

    }
    public static string StripScriptsAndStyles(string html)
    {
        // Use HtmlAgilityPack for robust HTML parsing and manipulation
        HtmlDocument doc = new();
        doc.LoadHtml(html);

        // Remove script and style nodes directly
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
