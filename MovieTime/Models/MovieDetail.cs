using System.Collections.Generic;
using Newtonsoft.Json;

namespace MovieTime.Models
{
    public class MovieDetail
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "original_title")]
        public string OriginalTitle { get; set; }

        [JsonProperty(PropertyName = "overview")]
        public string Overview { get; set; }

        [JsonProperty(PropertyName = "release_date")]
        public string ReleaseDate { get; set; }

        [JsonProperty(PropertyName = "poster_path")]
        public string PosterPath { get; set; }

        [JsonProperty(PropertyName = "backdrop_path")]
        public string BackdropPath { get; set; }

        [JsonProperty(PropertyName = "vote_average")]
        public double VoteAverage { get; set; }

        [JsonProperty(PropertyName = "genres")]
        public List<Genre> Genres { get; set; }

        [JsonProperty(PropertyName = "homepage")]
        public string Homepage { get; set; }
    }
}
