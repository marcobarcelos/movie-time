using System.Collections.Generic;
using Newtonsoft.Json;

namespace MovieTime.Models
{
    public class GenreList
    {
        [JsonProperty(PropertyName = "genres")]
        public List<Genre> Genres { get; set; }
    }
}
