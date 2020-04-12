using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyPodcastBank.Models
{
    public class Episode
    {
        public string EpisodeType { get; set; }
        public int PodcastID { get; set; }
        [ForeignKey("PodcastID")]
        public Podcast Podcast { get; set; }
        public int Season { get; set; }
        public int EpisodeNumber { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Link { get; set; }
        public string Length { get; set; }
        public string Type { get; set; }
        public string Url { get; set; }
        [Key]
        public string Guid { get; set; }
        public DateTime PubDate { get; set; }
        public int Duration { get; set; }
        public bool Explicit { get; set; }
    }
}
