using backend.DTOs;
using backend.Services.Implementations;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [ApiController]
    public class HeroController : ControllerBase
    {
        private readonly HeroService _heroService;

        public HeroController(HeroService heroService)
        {
            _heroService = heroService;
        }

        [HttpGet("/heroes")]
        public async Task<IActionResult> GetAllHeroes()
        {
            var heroes = await _heroService.GetAllHeroes();
            return Ok(heroes);
        }

        [HttpGet("/heroes/{heroID}")]
        public async Task<IActionResult> GetHeroByID(int heroID)
        {
            CompleteHeroDTO? hero = await _heroService.GetHeroByID(heroID);
            if (hero == null)
            {
                return NotFound();
            }
            return Ok(hero);
        }
    }
}
