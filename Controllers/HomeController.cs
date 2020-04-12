using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyPodcastBank.Models;
using MyPodcastBank.Data;
using Microsoft.EntityFrameworkCore;

namespace MyPodcastBank.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult RSSFeed()
        {
            IEnumerable<Podcast> pods = _context.Podcasts
                                                .Include(p => p.Episodes)
                                                .ToList();
            
            
            List<string> returnString = new List<string>();
            returnString.Add("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            returnString.Add("<rss version=\"2.0\" xmlns:itunes=\"http://www.itunes.com/dtds/podcast-1.0.dtd\" xmlns:content=\"http://purl.org/rss/1.0/modules/content/\">");
            foreach (Podcast p in pods)
            {
                IEnumerable<Episode> eps = p.Episodes;
                returnString.Add("<channel>");
                returnString.Add($"<title>{p.Title}</title>");
                returnString.Add($"<link>{p.Link}</link>");
                returnString.Add($"<language>{p.Language}</language>");
                returnString.Add($"<copyright>{p.Copyright}</copyright>");
                returnString.Add($"<itunes:author>{p.Author}</itunes:author>");
                returnString.Add($"<description>{p.Description}</description>");
                returnString.Add($"<itunes:owner>");
                returnString.Add($"<itunes:name>{p.OwnerName}</itunes:name>");
                returnString.Add($"<itunes:email>{p.OwnerEmail}</itunes:email>");
                returnString.Add($"</itunes:owner>");
                returnString.Add($"<itunes:image href=\"{p.Image}\" />");
                string[] cats = p.Category.Split(", ");
                for (int i = 0; i < cats.Length; i++)
                {
                    returnString.Add($"<itunes:category text=\"{cats[i]}\"" + ((i > 0) ? " /" : "") + ">");
                }
                returnString.Add($"</itunes:category>");
                returnString.Add($"<itunes:explicit>{p.Explicit.ToString().ToLower()}</itunes:explicit>");

                foreach (Episode e in eps)
                {
                    returnString.Add($"<item>");
                    returnString.Add($"<itunes:episodeType>{e.Type}</itunes:episodeType>");
                    if (e.EpisodeNumber != 0) returnString.Add($"<itunes:episode>{e.EpisodeNumber}</itunes:episode>");
                    if (e.Season != 0) returnString.Add($"<itunes:season>{e.Season}</itunes:season>");
                    returnString.Add($"<itunes:title>{e.Title}</itunes:title>");
                    returnString.Add($"<description>{e.Description}</description>");
                    if (e.ImageUrl != null) returnString.Add($"<itunes:image href=\"{e.ImageUrl}\" />");
                    if (e.Link != null) returnString.Add($"<link>{e.Link}</link>");
                    returnString.Add($"<enclosure length=\"{e.Length}\" type=\"{e.EpisodeType}\" url=\"{e.Url}\" />");
                    returnString.Add($"<guid>{e.Guid}</guid>");
                    returnString.Add($"<pubDate>{e.PubDate}</pubDate>");
                    returnString.Add($"<itunes:duration>{e.Duration}</itunes:duration>");
                    returnString.Add($"<itunes:explicit>{e.Explicit.ToString().ToLower()}</itunes:explicit>");
                    returnString.Add($"</item>");
                }
                returnString.Add($"</channel>");
                
            }
            returnString.Add($"</rss>");

            return View(returnString);
            
        }
    }
}
