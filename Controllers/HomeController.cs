using System.Diagnostics;
using System.Threading.Tasks;
using ESAPrizes.Models;
using ESAPrizes.Services;
using Microsoft.AspNetCore.Mvc;

namespace ESAPrizes.Controllers {
    [Route("/")]
    public class HomeController : Controller {

        private readonly PrizesService _prizes;

        public HomeController(PrizesService prizesService) {
            _prizes = prizesService;
        }

        [HttpGet("/")]
        public async Task<IActionResult> Index() {
            ViewData["Title"] = "Home page";
            var prizes = await _prizes.GetPrizes();
            return View(prizes);
        }

        [HttpGet("/privacy")]
        public IActionResult Privacy() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpGet("/Error")]
        public IActionResult Error() {
            var model = new ErrorModel() {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
            };

            return View(model);
        }
    }
}