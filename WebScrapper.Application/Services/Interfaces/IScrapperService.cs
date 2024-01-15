using WebScrapper.Application.Models;

namespace WebScrapper.Application.Services.Interfaces;
public interface IScrapperService
{
    Task<List<Response>> ScrapeUrlAsync(string url);
}
