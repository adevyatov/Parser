using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Parser.TextProcessors
{
    public class WordFilterProcessor : ITextProcessor
    {
        private readonly Regex _regex;

        public WordFilterProcessor(string? regexp = null)
        {
            _regex = new Regex(regexp ?? @"[a-zа-я]+", RegexOptions.IgnoreCase);
        }

        public IEnumerable<string> Process(IEnumerable<string> lines)
        {
            return lines.Where(w => _regex.IsMatch(w));
        }
    }
}