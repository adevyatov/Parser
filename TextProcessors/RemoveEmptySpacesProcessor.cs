using System.Collections.Generic;
using System.Linq;

namespace Parser.TextProcessors
{
    public class RemoveWhitespacesProcessor : ITextProcessor
    {
        public IEnumerable<string> Process(IEnumerable<string> lines)
        {
            return lines.Where(t => !string.IsNullOrWhiteSpace(t)).Select(t => t.Trim());
        }
    }
}