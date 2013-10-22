using MomentOfZenGenerator.Wordnik;
using MomentOfZenGenerator.YouTube;
using System;
using System.Web.Mvc;

namespace MomentOfZenGenerator.Web.Controllers
{
    public class GeneratorController : Controller
    {
        private Generator generator;

        public GeneratorController()
        {
            var random = new Random();

            var responseProvider = new ResponseProvider();
            var wordnikRequestUriBuilder = new WordnikRequestUriBuilder(responseProvider);
            var wordnikResponseProvider = new WordnikResponseProvider(wordnikRequestUriBuilder, responseProvider);

            var youTubeResponseProvider = new YouTubeResponseProvider();
            var filter = new Filter(youTubeResponseProvider);

            generator = new Generator(random, wordnikResponseProvider, filter);
        }

        public ActionResult Generate()
        {
            var videoUrl = generator.GetMomentOfZen();

            while (String.IsNullOrEmpty(videoUrl))
                videoUrl = generator.GetMomentOfZen();

            ViewBag.VideoUrl = videoUrl;

            return View();
        }
    }
}