using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SnakeApi.Models.DTOs;
using SnakeApi.Services;

namespace SnakeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoreController : ControllerBase
    {
        private readonly ScoreService _scoreService;

        public ScoreController(ScoreService scoreService)
        {
            _scoreService = scoreService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Score>>> Get() =>
            await _scoreService.GetAsync();

        [HttpGet("{username}")]
        public async Task<ActionResult<List<Score>>> GetByUsername(string username) =>
            await _scoreService.GetByUsernameAsync(username);

        //hacer un post o revisar que el post de abajo mande nombre Y puntaje

        [HttpPost]
        public async Task<ActionResult<Score>> Post(Score score)
        {
            await _scoreService.CreateAsync(score);
            return CreatedAtAction(nameof(Get), new { id = score.Id }, score);
        }
    }
}
