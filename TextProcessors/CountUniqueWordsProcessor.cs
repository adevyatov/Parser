using System.Collections.Generic;
using System.Linq;

namespace Parser.TextProcessors
{
    public class CountUniqueWordsProcessor : ITextProcessor
    {
        public IEnumerable<string> Process(IEnumerable<string> lines)
        {
            return lines
                .GroupBy(w => w)
                .OrderBy(w => w.Count())
                .Select(w => w.Key + ": " + w.Count());
        }
    }
}