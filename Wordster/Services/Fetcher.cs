using CustardApi.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wordster.Models.http.Word;

namespace Wordster.Services
{
    public class Fetcher
    {
        private const string _heokuApiHost = "random-word-api.herokuapp.com";
        private const string _datamuseApiHost = "api.datamuse.com";
        private readonly Service _wsHerokuApi;
        private readonly Service _wsDatamuseApi;

        public Fetcher() 
        {
            _wsHerokuApi = new Service(_heokuApiHost, sslCertificate: true) ;
            _wsDatamuseApi = new Service(_datamuseApiHost, sslCertificate: true) ;
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
            return (await _wsHerokuApi.Get<string[]>(controller))[0];

        }
        /// <summary>
        /// Check if a word exists
        /// </summary>
        /// <param name="word">word we want to check</param>
        /// <returns>true: exist | false: doesn't</returns>
        public async Task<bool> CheckWord(string word)
        {
            // Define
            string controller = $"words?ml={word}";
            // Process
            var relatedWords = await _wsDatamuseApi.Get<List<RelatedWord>>(controller);

            return relatedWords.Count > 0;

        }
    }
}
