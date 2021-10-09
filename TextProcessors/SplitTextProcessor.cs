using System;
using System.Collections.Generic;
using System.Linq;

namespace Parser.TextProcessors
{
    public class SplitTextProcessor : ITextProcessor
    {
        private readonly char[] _delimiters;

        public SplitTextProcessor(char[]? delimiters = null)
        {
            _delimiters = delimiters ?? new[]
                {' ', ',', '.', '!', '?', '\"', ';', ':', '[', ']', '(', ')', '\n', '\r', '\t'};
        }

        public IEnumerable<string> Process(IEnumerable<string> lines)
        {
            var words = Enumerable.Empty<string>();


            foreach (var line in lines)
            {
                var split = line.Split(_delimiters, StringSplitOptions.None).Where(w => w != "");

                words = words.Concat(split);
            }

            return words;
        }
    }
}