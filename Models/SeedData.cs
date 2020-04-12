using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPodcastBank.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MyPodcastBank.Models
{
    public static class SeedData
    {
        public static void Init(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.Podcasts.Any()) return;
                Podcast dummyPodcast = new Podcast()
                {
                    Title = "Professional Development",
                    Link = "https://azure.us/podcasts/",
                    Language = "en-us",
                    Copyright = "2020 DEVELOPERS INC",
                    Author = "John Appleseed",
                    Description = "This is the most important thing that I am going to tell you... on everything.",
                    Type = "serial",
                    OwnerName = "All About Everything",
                    OwnerEmail = "microsoftazure@icloud.com",
                    Image = "https://azure.us/podcasts/images/artwork.png",
                    Category = "Self Improvement, Professional Development",
                    Explicit = true
                };
                context.Podcasts.AddRange(
                    dummyPodcast
                    );
                //context.SaveChanges();
                if (context.Episodes.Any()) return;
                try
                {
                    context.Episodes.AddRange(
                    new Episode
                    {
                        Type = "Trailer",
                        PodcastID = 1,
                        Podcast = dummyPodcast,
                        Title = "DEVELOPERS INC Trailer",
                        Description = "DEVELOPERS INC tells you abou the important stuff. Listen on Azure Podcasts",
                        Length = "498537",
                        EpisodeType = "audio/mpeg",
                        Url = "https://azure.us/podcasts/everything/AllAboutEverything/Trailer.mp3",
                        Guid = "aae20200401",
                        PubDate = DateTime.Parse("01/04/2020 01:15:00", System.Globalization.CultureInfo.InvariantCulture),
                        Duration = 1079,
                        Explicit = false
                    },
                    new Episode
                    {
                        Type = "Full",
                        PodcastID = 1,
                        Podcast = dummyPodcast,
                        EpisodeNumber = 1,
                        Season = 1,
                        Title = "S01 EP01 Tell Me About Yourself",
                        Description = "Talking about nailing the imfamous 'Tell me about yourself' interview question.",
                        ImageUrl = "http://azure.us/podcasts/images/Season1_Episode1.jpg",
                        Link = "http://example.com/podcasts/everything/",
                        Length = "5650889",
                        EpisodeType = "video/mp4",
                        Url = "http://azure.us/podcasts/everything/AllAboutEverything/Season1_Episode1.mp3",
                        Guid = "aae20200405",
                        PubDate = DateTime.Parse("05/04/2020 13:00:00", System.Globalization.CultureInfo.InvariantCulture),
                        Duration = 3627,
                        Explicit = false
                    }
                    );
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex}");
                    context.SaveChanges();
                }
                context.SaveChanges();
            }
            
        }
    }
}
