using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sandy.API.Business;
using Sandy.API.Models.DataTransferObjects.GolfCourseDto;
using Sandy.Data;

namespace Sandy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAngularOrigins")]
    public class GolfCoursesController : ControllerBase
    {
        private GolfCourseService _golfCourseService;

        public GolfCoursesController(SandyDbContext dbContext)
        {
            this._golfCourseService = new GolfCourseService(dbContext);
        }

        [HttpPost]
        public IActionResult AddGolfCourse(AddGolfCourseRequest addGolfCourseRequest)
        {
            var createdGolfCourse = _golfCourseService.AddGolfCourse(addGolfCourseRequest);

            return Ok(createdGolfCourse);
        }

        [HttpGet]
        public IActionResult GetAllGolfCourses()
        {
            var golfCoursesDTO = _golfCourseService.GetAllGolfCourses();

            golfCoursesDTO = golfCoursesDTO.OrderBy(x => x.Name).ToList();

            return Ok(golfCoursesDTO);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetGolfCourseById(Guid id)
        {
            var golfCourseDTO = _golfCourseService.GetGolfCourseById(id);

            if (golfCourseDTO != null)
            {
                return Ok(golfCourseDTO);
            }

            return BadRequest();
        }

        [HttpGet]
        [Route("{golfCourseName}")]
        public IActionResult GetGolfCourseByName(string golfCourseName)
        {
            var golfCourseDTO = _golfCourseService.GetGolfCourseByName(golfCourseName);

            if (golfCourseDTO != null)
            {
                return Ok(golfCourseDTO);
            }

            return BadRequest();
        }

        [HttpGet]
        [Route("names")]
        public IActionResult GetAllGolfCourseNames()
        {
            var golfCoursesDTO = _golfCourseService.GetAllGolfCourses();
            var golfCourseNames = new List<string>();

            foreach (var golfCourse in golfCoursesDTO)
            {
                golfCourseNames.Add(golfCourse.Name);
            }

            golfCourseNames.Sort();

            return Ok(golfCourseNames);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult DeleteGolfCourse(Guid id)
        {
            var deletedGolfCourse = _golfCourseService.DeleteGolfCourse(id);

            if (deletedGolfCourse != false)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
