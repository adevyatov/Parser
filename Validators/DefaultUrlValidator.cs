using System;

#nullable enable
namespace Parser.Validators
{
    public class DefaultUrlValidator : IUrlValidator
    {
        public bool isValid(string url)
        {
            return Uri.IsWellFormedUriString(url, UriKind.Absolute);
        }
    }
}