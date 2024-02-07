using CustardApi.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordster.Services
{
    public class Fetcher
    {
        private readonly Service _wsHerokuApp;

        public Fetcher() 
        {
            _wsHerokuApp = new Service("random-word-api.herokuapp.com", sslCertificate: true) ;
        }
        /// <summary>
        /// Returns a random word using parameters given
        /// </summary>
        /// <param name="length">length of the word you want to be returned</param>
        /// <param name="lang">language of the word you want</param>
        /// <returns></returns>
        public async Task<string> GetRandomWord(int length, string lang = "en")
        {
            // Define
            string controller = $"word?length={length}&lang={lang}";
            // Process
            return (await _wsHerokuApp.Get<string[]>("word"))[0];

        }
    }
}
