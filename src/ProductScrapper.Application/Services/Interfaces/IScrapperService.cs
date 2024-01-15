using ProductScrapper.Application.Models;

namespace ProductScrapper.Application.Services.Interfaces;
public interface IScrapperService
{
    Task<List<Response>> ScrapeUrlAsync(string url);
}
