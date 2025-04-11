using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SnakeApi.Models;
using SnakeApi.Models.DTOs;
using SnakeApi.Services;
using SnakeApi.Tools.Mapper;

namespace SnakeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoreController : ControllerBase
    {
        private readonly ScoreService _scoreService;
        private readonly ScoreMapper _scoreMapper;


        public ScoreController(ScoreService scoreService, ScoreMapper scoreMapper)
        {
            _scoreService = scoreService;
            _scoreMapper = scoreMapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Score>>> GetTopScores()
        {
            var scores = await _scoreService.GetAsync();// Obtener entidades de la base de datos
            var scoreDTOs = _scoreMapper.Get(scores);// Convertir entidades a DTOs para el frontend
            return Ok(scoreDTOs);
        }
  
        //[HttpGet("{username}")]
        //public async Task<ActionResult<List<Score>>> GetByUsername(string username) =>
        //    await _scoreService.GetByUsernameAsync(username);


        [HttpPost]
        public async Task<ActionResult<Score>> CreateScore(ScoreDTO scoreDTO)
        {
            var score = _scoreMapper.Set(scoreDTO);// Convertir DTO a entidad para la base de datos
            await _scoreService.CreateAsync(score);//Guardar en la base de datos
            return CreatedAtAction(nameof(GetTopScores), new { id = score.Id }, score);//Devolver DTO actualizado
        }
    }
}
