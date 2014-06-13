using System;
using System.Diagnostics;
using System.Web.Mvc;
using MomentOfZenGenerator.Wordnik;
using MomentOfZenGenerator.YouTube;
using statsd_csharp;

namespace MomentOfZenGenerator.Web.Controllers
{
    public class GeneratorController : Controller
    {
        private Generator generator;
        private IStatsdClient statsdClient;
        private Stopwatch stopwatch;

        public GeneratorController()
        {
            var random = new Random();

            var responseProvider = new ResponseProvider();
            var wordnikRequestUriBuilder = new WordnikRequestUriBuilder(responseProvider);
            var wordnikResponseProvider = new WordnikResponseProvider(wordnikRequestUriBuilder, responseProvider);

            var youTubeResponseProvider = new YouTubeResponseProvider();
            var filter = new Filter(youTubeResponseProvider);

            generator = new Generator(random, wordnikResponseProvider, filter);

            statsdClient = StatsdClient.Create("191.238.44.201", 8125, "moment-of-zen-generator");
            stopwatch = new Stopwatch();
        }

        public ActionResult Generate()
        {
            stopwatch.Start();

            var videoUrl = generator.GetMomentOfZen();

            while (String.IsNullOrEmpty(videoUrl))
                videoUrl = generator.GetMomentOfZen();

            ViewBag.VideoUrl = videoUrl;

            var totalMilliseconds = Convert.ToInt32(stopwatch.Elapsed.TotalMilliseconds);
            statsdClient.SendTiming("generator.duration", totalMilliseconds);

            stopwatch.Reset();

            return View();
        }
    }
}