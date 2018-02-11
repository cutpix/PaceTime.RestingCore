using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaceTime.API.Models
{

    public class InstagramMedia
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "images")]
        public JObject Images { get; set; }
        [JsonProperty(PropertyName = "caption")]
        public JObject Caption { get; set; }
        [JsonProperty(PropertyName = "tags")]
        public IList<string> Tags { get; set; }
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
    }
}
