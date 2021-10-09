using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Parser.Database.Models;
using AppContext = Parser.Database.Context.AppContext;

namespace Parser.WebsiteCache
{
    public class DatabaseWebsiteCache : IWebsiteCache
    {
        private readonly int _ttl;

        private readonly AppContext _context;

        public DatabaseWebsiteCache(int ttl = 3600)
        {
            _context = new AppContext();
            _ttl = ttl;
        }

        public bool HasBodyContent(string url)
        {
            var now = DateTime.Now;

            return _context.WebsitesContent
                .Where(w => w.Url == url)
                .Where(w => w.ExpireAt > now)
                .Select(w => w.Id)
                .Any();
        }

        public async Task<string> GetBodyContent(string url)
        {
            var now = DateTime.Now;

            return await _context.WebsitesContent
                .Where(w => w.Url == url)
                .Where(w => w.ExpireAt > now)
                .Select(w => w.Content)
                .FirstOrDefaultAsync();
        }

        public void SetBodyContent(string url, string content)
        {
            var context = _context;
            var expiration = DateTime.Now.AddSeconds(_ttl);
            var websiteContent = context.WebsitesContent.FirstOrDefault(w => w.Url == url);

            if (websiteContent == null)
            {
                websiteContent = new WebsiteContent(url, content, expiration);
                context.WebsitesContent.Add(websiteContent);
            }

            websiteContent.Content = content;
            websiteContent.ExpireAt = expiration;
            context.SaveChanges();
        }
    }
}