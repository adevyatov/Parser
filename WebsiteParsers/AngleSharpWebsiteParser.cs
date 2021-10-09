using System.Linq;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.XPath;

namespace Parser.WebsiteParsers
{
    public class AngleSharpWebsiteParser : IWebsiteParser
    {
        private readonly IBrowsingContext _context;

        public AngleSharpWebsiteParser(IConfiguration? configuration = null)
        {
            configuration ??= Configuration.Default.WithDefaultLoader();
            configuration.WithXPath();

            _context = BrowsingContext.New(configuration);
        }

        public async Task<string> GetBodyContent(string url)
        {
            var document = await _context.OpenAsync(url);
            const string xpath = ".//text()[not(ancestor::script|ancestor::style|ancestor::noscript)]";

            return string.Join("\n", document.Body.SelectNodes(xpath).Select(t => t.TextContent).ToArray());
        }
    }
}