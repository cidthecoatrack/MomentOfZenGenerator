using System;
using System.Linq;

namespace MomentOfZenGenerator.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Generating random Youtube video...");

            var filter = new Filter();
            var videos = Enumerable.Empty<String>();
            var attempts = 1;

            do
            {
                Console.Write("\rAttempt {0}", attempts++);
                videos = filter.GetVideoUrlsLessThanOneMinuteLong();
            } while (!videos.Any());

            Console.WriteLine(videos.First());
            Console.Read();
        }
    }
}