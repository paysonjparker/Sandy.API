using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sandy.Business;
using Sandy.Data;
using Sandy.Models.DataTransferObjects.ScoreDto;

namespace Sandy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAngularOrigins")]
    public class ScoresController : ControllerBase
    {
        private ScoreService _scoreService;

        public ScoresController(SandyDbContext dbContext)
        {
            this._scoreService = new ScoreService(dbContext);
        }

        [HttpPost]
        public IActionResult AddScore(AddScoreRequest addScoreRequest)
        {
            var score = _scoreService.AddScore(addScoreRequest);

            return Ok(score);
        }

        [HttpGet]
        [Route("{golferId:Guid}")]
        public IActionResult GetAllScoresByGolfer(Guid golferId)
        {
            var scores = _scoreService.GetAllScoresByGolfer(golferId);

            scores.Reverse();

            return Ok(scores);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult DeleteScore(Guid id)
        {
            var existingScore = _scoreService.DeleteScore(id);

            if (existingScore != false)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
