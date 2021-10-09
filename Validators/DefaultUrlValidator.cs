using System;

#nullable enable
namespace Parser.Validators
{
    public static class DefaultUrlValidator
    {
        public static bool IsValid(string url)
        {
            return Uri.IsWellFormedUriString(url, UriKind.Absolute);
        }
    }
}