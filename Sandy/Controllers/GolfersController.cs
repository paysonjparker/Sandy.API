using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sandy.Business;
using Sandy.Data;
using Sandy.Models.DataTransferObjects.GolferDto;

namespace Sandy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAngularOrigins")]
    public class GolfersController : ControllerBase
    {
        private GolferService _golferService;

        public GolfersController(SandyDbContext dbContext)
        {
            this._golferService = new GolferService(dbContext);
        }

        [HttpPost]
        public IActionResult AddGolfer(AddGolferRequest addGolferRequest)
        {
            var golfer = _golferService.AddGolfer(addGolferRequest);

            return Ok(golfer);
        }

        [HttpGet]
        public IActionResult GetAllGolfers()
        {
            var golfers = _golferService.GetAllGolfers();

            golfers= golfers.OrderBy(x => x.Name).ToList();

            return Ok(golfers);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetGolferById(Guid id) 
        {
            var golfer = _golferService.GetGolferById(id);

            if(golfer != null)
            {
                return Ok(golfer);
            }

            return BadRequest();
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult UpdateGolfer(Guid id, UpdateGolferRequest updateGolferRequest)
        {
            var exisitngGolfer = _golferService.UpdateGolfer(id, updateGolferRequest);

            if (exisitngGolfer != null)
            {
                return Ok(exisitngGolfer);
            }

            return BadRequest();
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult DeleteGolfer(Guid id)
        {
            var existingGolfer = _golferService.DeleteGolfer(id);

            if (existingGolfer != false)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
