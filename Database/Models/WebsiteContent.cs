using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Parser.Database.Models
{
    [Index(nameof(Url), IsUnique = true)]
    public class WebsiteContent
    {
        public WebsiteContent(string url, string content, DateTime expireAt)
        {
            Url = url;
            ExpireAt = expireAt;
            Content = content;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Url { get; init; }

        public string Content { get; set; }

        public DateTime ExpireAt { get; set; }
    }
}