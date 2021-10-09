using System.Threading.Tasks;

namespace Parser.WebsiteParsers
{
    public interface IWebsiteParser
    {
        public Task<string> GetBodyContent(string url);
    }
}