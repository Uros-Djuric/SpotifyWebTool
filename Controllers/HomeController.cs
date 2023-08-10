using Microsoft.AspNetCore.Mvc;
using SpotifyApp.Models;
using System.Diagnostics;
using System.Threading.Tasks;
using SpotifyAPI.Web;
using System;

namespace SpotifyApp.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var spotify = new SpotifyClient("BQAUOKzmFMDlm5t9uleYot41qiej1q7UewCrxZT4KI0o1rDp_-Quxr5aUpGrRcr_h3UKlxWEjXVAzsKOTNA3F7sg2AGuqJ75Rf9ndjnkscN-4KJFYk4CQoE_jew7H1tg2xRMNjmvh-oeXH3_SlVKkcvpvYRz2pbTMZKEJXu5-hlIl24kJc049UiMoX9qOycs-FjwGvsaNdOa24nBMcz8T0Hca3BQMuwpR5sXdab-qgWfSt6J6Uv_4CKQO3oeSfMOruwoza3QUW8fy5TUktOVIMl0GK7HFvDmuuhkKlevDdoPldCQNOk");
            var recently_played = await spotify.Player.GetRecentlyPlayed();



            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}