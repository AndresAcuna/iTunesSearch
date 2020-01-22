using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iTunesSearch.Models
{
    public class ITunesResponse
    {
        [JsonProperty("resultCount")]
        public int ResultCount { get; set; }
        [JsonProperty("results")]
        public List<Result> Results { get; set; }

    }

    public class Result
    {
        [JsonProperty("wrapperType")]
        public String WrapperType { get; set; }
        [JsonProperty("kind")]
        public String Kind { get; set; }
        [JsonProperty("trackName")]
        public String TrackName { get; set; }
        [JsonProperty("artistName")]
        public String ArtistName { get; set; }
        [JsonProperty("collectionName")]
        public String CollectionName { get; set; }
        [JsonProperty("artworkUrl100")]
        public String ArtworkURL100 { get; set; }
        [JsonProperty("trackViewUrl")]
        public String ViewURL { get; set; }
        
    }
}