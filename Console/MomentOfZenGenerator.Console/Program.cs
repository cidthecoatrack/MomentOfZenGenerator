using System;
using MomentOfZenGenerator.Wordnik;
using MomentOfZenGenerator.YouTube;

namespace MomentOfZenGenerator.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Generating random Youtube video...");

            var random = new Random();

            var responseProvider = new ResponseProvider();
            var wordnikUriBuilder = new WordnikRequestUriBuilder(responseProvider);
            var wordnikResponseProvider = new WordnikResponseProvider(wordnikUriBuilder, responseProvider);

            var youTubeResponseProvider = new YouTubeResponseProvider();
            var filter = new Filter(youTubeResponseProvider);

            var generator = new Generator(random, wordnikResponseProvider, filter);
            var attempts = 1;
            var videoUrl = String.Empty;

            do
            {
                Console.Write("\rAttempt {0} ({1})\t\t", attempts++);
                videoUrl = generator.GetMomentOfZen();
            } while (String.IsNullOrEmpty(videoUrl));

            Console.WriteLine(videoUrl);
            Console.Read();
        }
    }
}