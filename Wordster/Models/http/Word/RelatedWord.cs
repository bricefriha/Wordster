using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordster.Models.http.Word
{
    public class RelatedWord

    {
        [JsonProperty("word")]
        public string Word { get; set; }
        [JsonProperty("score")]
        public int Score { get; set; }
        [JsonProperty("tags")]
        public string[] Tags { get; set; }
    }
}
