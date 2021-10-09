using System;
using System.Data.Common;
using System.Linq;
using Microsoft.Data.SqlClient;
using Parser.Validators;
using Parser.WebsiteCache;
using Parser.WebsiteParsers;

namespace Parser
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var url = args.ElementAtOrDefault(0) ?? ReadArgument("Type website URL");

            if (!DefaultUrlValidator.IsValid(url))
            {
                Console.WriteLine("URL argument is invalid.");
                Environment.ExitCode = 1;

                return;
            }

            var parser = new AngleSharpWebsiteParser();
            DatabaseWebsiteCache? cache = null;

            try
            {
                cache = new DatabaseWebsiteCache();
            } catch (SqlException)
            {
                if (!PromptConfirmation("Sql connection error. Do you want continue?"))
                {
                    return;
                }
            }

            var handler = new UniqueWordCountHandler(parser, cache ?? null);
            var words = handler.GetUniqueWordsCount(url);

            foreach (var word in words)
            {
                Console.WriteLine(word);
            }
        }

        private static bool PromptConfirmation(string confirmText)
        {
            Console.Write(confirmText + " [y/n] ");
            var response = Console.ReadKey(false).Key;
            Console.WriteLine();

            return (response == ConsoleKey.Y);
        }

        private static string ReadArgument(string text)
        {
            Console.Write(text + ": ");

            return Console.ReadLine() ?? "";
        }
    }
}