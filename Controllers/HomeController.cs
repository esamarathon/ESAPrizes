using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ESAPrizes.Models;
using ESAPrizes.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace ESAPrizes.Controllers {
    [Route("/")]
    public class HomeController : Controller {

        private readonly PrizesService _prizes;
        private readonly CategorizationService _categorizationService;

        public HomeController(PrizesService prizesService, CategorizationService categorizationService) {
            _prizes = prizesService;
            _categorizationService = categorizationService;
        }

        [HttpGet("/")]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 300)]
        public async Task<IActionResult> Index() {
            ViewData["Title"] = "Home page";
            var prizes = await _prizes.GetPrizes();
            

            var model = new HomeModel() {
                Prizes = prizes
                    .GroupBy(_categorizationService.GetCategory)
                    .OrderByDescending(g => g.Key.Item1),
            };

            return View(model);
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