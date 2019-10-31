using System.Threading.Tasks;
using ESAPrizes.Services;
using Microsoft.AspNetCore.Mvc;

namespace ESAPrizes.Controllers {
    [Route("/api/prizes")]
    public class PrizeController : Controller {

        private readonly PrizesService _prizes;

        public PrizeController(PrizesService prizesService) {
            _prizes = prizesService;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllPrizes() {
            var prizes = await _prizes.GetPrizes();
            return Ok(prizes);
        }
    }
}