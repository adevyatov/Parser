using Parser.WebsiteParsers;

namespace Parser.WebsiteCache
{
    public interface IWebsiteCache : IWebsiteParser
    {
        public bool HasBodyContent(string url);

        public void SetBodyContent(string url, string content);
    }
}