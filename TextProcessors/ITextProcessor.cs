using System.Collections.Generic;

namespace Parser.TextProcessors
{
    public interface ITextProcessor
    {
        public IEnumerable<string> Process(IEnumerable<string> lines);
    }
}