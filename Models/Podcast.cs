using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPodcastBank.Data;

namespace MyPodcastBank.Models
{
    public class Podcast
    {
        
        public int PodcastID { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string Language { get; set; }
        public string Copyright { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string OwnerName { get; set; }
        public string OwnerEmail { get; set; }
        public string Image { get; set; }
        public string Category { get; set; }
        public bool Explicit { get; set; }

        public ICollection<Episode> Episodes { get; set; }

    }
}
