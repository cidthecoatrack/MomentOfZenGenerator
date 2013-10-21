using System;
using MomentOfZenGenerator.Interfaces;

namespace MomentOfZenGenerator.Wordnik
{
    public class WordnikResponseProvider : IWordnikResponseProvider
    {
        private IWordnikRequestUriBuilder uriBuilder;
        private IResponseProvider responseProvider;

        public WordnikResponseProvider(IWordnikRequestUriBuilder uriBuilder, IResponseProvider responseProvider)
        {
            this.uriBuilder = uriBuilder;
            this.responseProvider = responseProvider;
        }

        public String GetWord()
        {
            var wordnikRandomWordUri = uriBuilder.BuildRequestUri();
            var randomWordResponse = responseProvider.GetJsonResponseContent<WordnikRandomWordResponse>(wordnikRandomWordUri);
            return randomWordResponse.Word;
        }
    }
}