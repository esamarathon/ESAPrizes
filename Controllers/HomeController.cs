using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ESAPrizes.Models;
using ESAPrizes.Services;
using ESAPrizes.Utils;
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
                    .GroupBy(_categorizationService.GetCategory, new CategoryComparer())
                    .OrderByDescending(g => g.Key.Order),
            };

            return View(model);
        }

        [HttpGet("/error/404")]
        public IActionResult Error404() {
            var model = new ErrorModel() {
                StatusCode = 404,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
            };

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpGet("/error/{code}")]
        public IActionResult Error(int code) {
            var model = new ErrorModel() {
                StatusCode = code,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
            };

            return View(model);
        }
    }
}