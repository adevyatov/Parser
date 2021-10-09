using System.Collections.Generic;
using System.Linq;
using Parser.TextProcessors;
using Parser.WebsiteCache;
using Parser.WebsiteParsers;

namespace Parser
{
    public class UniqueWordCountHandler
    {
        private readonly IWebsiteParser _parser;
        private readonly IWebsiteCache? _cache;

        public UniqueWordCountHandler(IWebsiteParser parser, IWebsiteCache? cache = null)
        {
            _parser = parser;
            _cache = cache;
        }

        public IEnumerable<string> GetUniqueWordsCount(string url)
        {
            var processors = new List<ITextProcessor>
            {
                new RemoveWhitespacesProcessor(),
                new SplitTextProcessor(),
                new WordFilterProcessor(),
                new CountUniqueWordsProcessor()
            };

            var provider = (_cache?.HasBodyContent(url) ?? false) ? _cache : _parser;
            var content = provider.GetBodyContent(url).Result;

            if (_cache != null && provider == _parser)
            {
                _cache.SetBodyContent(url, content);
            }

            return processors.Aggregate(
                content.Split("\n").ToList(),
                (current, processor) => processor.Process(current).ToList()
            );
        }
    }
}