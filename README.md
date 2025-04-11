# SnakeGame
// 4. Controlador para exponer endpoints
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GameLeaderboard.Models;
using GameLeaderboard.Services;

namespace GameLeaderboard.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScoresController : ControllerBase
    {
        private readonly ScoreService _scoreService;

        public ScoresController(ScoreService scoreService)
        {
            _scoreService = scoreService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Score>>> Get() =>
            await _scoreService.GetAsync();

        [HttpGet("{username}")]
        public async Task<ActionResult<List<Score>>> GetByUsername(string username) =>
            await _scoreService.GetByUsernameAsync(username);

        [HttpPost]
        public async Task<ActionResult<Score>> Post(Score score)
        {
            await _scoreService.CreateAsync(score);
            return CreatedAtAction(nameof(Get), new { id = score.Id }, score);
        }
    }
}
