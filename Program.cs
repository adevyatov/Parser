using System;
using System.Data.Common;
using System.Linq;
using Microsoft.Data.SqlClient;
using Parser.Exception;
using Parser.Validators;
using Parser.WebsiteCache;
using Parser.WebsiteParsers;

namespace Parser
{
    internal static class Program
    {
        private static void Main()
        {
            var url = ReadUrl();
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

            try
            {
                var handler = new UniqueWordCountHandler(parser, cache ?? null);
                var words = handler.GetUniqueWordsCount(url);

                foreach (var word in words)
                {
                    Console.WriteLine(word);
                }
            }
            catch (AggregateException e)
            {
                foreach (var inner in e.InnerExceptions)
                {
                    if (inner.GetType() != typeof(ParseTimeoutException)) throw inner;
                    Console.Error.WriteLine("Cannot parse given URL. Timeout has been reached!");
                }
            }
        }

        private static string ReadUrl()
        {
            while (true)
            {
                var url = ReadArgument("Type website URL");

                if (DefaultUrlValidator.IsValid(url))
                {
                    return url;
                }

                Console.WriteLine("URL argument is invalid.");
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