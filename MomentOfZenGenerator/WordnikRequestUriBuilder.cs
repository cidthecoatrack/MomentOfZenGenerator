using MomentOfZenGenerator.Interfaces;
using System;

namespace MomentOfZenGenerator
{
    public class WordnikRequestUriBuilder : IWordnikRequestUriBuilder
    {
        private IResponseProvider responseProvider;

        private const String root = "http://api.wordnik.com/v4/words.json";
        private const Int32 frequencyDivisor = 2;
        private const String apiKey = "774294bdb97d07a79400d06796f04c17b6ad2bb70a90c1127";

        public WordnikRequestUriBuilder(IResponseProvider responseProvider)
        {
            this.responseProvider = responseProvider;
        }

        public String BuildRequestUri()
        {
            var frequency = GetFrequency();

            return String.Format("{0}/randomWords?minCorpusCount={1}&excludePartOfSpeech=proper-noun,proper-noun-plural,proper-noun-posessive,suffix,family-name,idiom,affix&hasDictionaryDef=true&includePartOfSpeech=noun,verb,adjective,definite-article,conjunction&limit=26&maxLength=7&api_key={2}",
                root, frequency, apiKey);
        }

        private Int64 GetFrequency()
        {
            var frequencyUri = GetFrequencyUri();
            var frequencyContent = responseProvider.GetJsonResponseContent<WordnikFrequencyResponse>(frequencyUri);

            return frequencyContent.TotalCount / frequencyDivisor;
        }

        private String GetFrequencyUri()
        {
            var word = "the";
            var year = 2012;

            return String.Format("{0}/{1}/frequency?useCanonical=false&startYear={2}&endYear={2}&api_key={3}",
                root, word, year, apiKey);
        }
    }
}