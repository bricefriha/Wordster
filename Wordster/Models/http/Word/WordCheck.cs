using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordster.Models.http.Word
{
    public class WordCheck
    {
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("resolution")]
        public string Resolution { get; set; }
    }
}
