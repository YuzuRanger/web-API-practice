using AutoMapper;
using Cms.Data.Repository.Models;
using Cms.Data.Repository.Repositories;
using Cms.WebApi.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Cms.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoursesController: ControllerBase
    {
        public CoursesController(ICmsRepository cmsRepository, IMapper mapper)
        {
            this.CmsRepository = cmsRepository;
            this.mapper = mapper;
        }

        public ICmsRepository CmsRepository { get; }
        public IMapper mapper;

        // [HttpGet]
        // public IEnumerable<Course> GetCourses()
        // {
        //     return CmsRepository.GetAllCourses();
        // }

        // approach 1 - return type of primitive or complex
        // [HttpGet]
        // public IEnumerable<CourseDto> GetCourses()
        // {
        //     try
        //     {
        //         IEnumerable<Course> courses = CmsRepository.GetAllCourses();
        //         var result = MapCourseToCourseDto(courses);
        //         return result;
        //     }
        //     catch (System.Exception)
        //     {
        //         throw;
        //     }
        // }

        // approach 2 - IActionResult
        //[HttpGet]
        // public IActionResult GetCourses()
        // {
        //     try
        //     {
        //         IEnumerable<Course> courses = CmsRepository.GetAllCourses();
        //         var result = MapCourseToCourseDto(courses);
        //         return Ok(result);
        //     }
        //     catch (System.Exception ex)
        //     {
        //         return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //     }
        // }

        // approach 3 - ActionResult<T>
        [HttpGet]
        public ActionResult<IEnumerable<CourseDto>> GetCourses()
        {
            try
            {
                IEnumerable<Course> courses = CmsRepository.GetAllCourses();
                // var result = MapCourseToCourseDto(courses);
                var result = mapper.Map<CourseDto[]>(courses);
                return result.ToList(); // convert to support ActionResult<T>
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // 
        // [HttpGet]
        // public async Task<ActionResult<IEnumerable<CourseDto>>> GetCoursesAsync()
        // {
        //     try
        //     {
        //         IEnumerable<Course> courses = await CmsRepository.GetAllCoursesAsync();
        //         var result = MapCourseToCourseDto(courses);
        //         return result.ToList(); // convert to support ActionResult<T>
        //     }
        //     catch (System.Exception ex)
        //     {
        //         return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //     }
        // }

        [HttpPost]
        public ActionResult<CourseDto> AddCourse([FromBody]CourseDto course)
        {
            try
            {
                // if(!ModelState.IsValid)
                // {
                //     return BadRequest(ModelState);
                // }
                
                var newCourse = mapper.Map<Course>(course);
                newCourse = CmsRepository.AddCourse(newCourse);
                return mapper.Map<CourseDto>(newCourse);
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{courseId}")]
        public ActionResult<CourseDto> GetCourse(int courseId)
        {
            try
            {
                if(!CmsRepository.IsCourseExists(courseId))
                {
                    return NotFound();
                }

                Course course = CmsRepository.GetCourse(courseId);
                var result = mapper.Map<CourseDto>(course);
                return result;
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        #region Custom Mapper Functions
        // custom map function
        // private CourseDto MapCourseToCourseDto(Course course)
        // {
        //     return new CourseDto()
        //     {
        //         CourseId = course.CourseId,
        //         CourseName = course.CourseName,
        //         CourseDuration = course.CourseDuration,
        //         CourseType = (Cms.WebApi.DTOs.COURSE_TYPE)course.CourseType
        //     };
        // }

        // private IEnumerable<CourseDto> MapCourseToCourseDto(IEnumerable<Course> courses)
        // {
        //     IEnumerable<CourseDto> result;

        //     result = courses.Select(c => new CourseDto()
        //     {
        //         CourseId = c.CourseId,
        //         CourseName = c.CourseName,
        //         CourseDuration = c.CourseDuration,
        //         CourseType = (Cms.WebApi.DTOs.COURSE_TYPE)c.CourseType
        //     });

        //     return result;

        //     // return new CourseDto(){
        //     //     CourseId = course.CourseId,
        //     //     CourseName = course.CourseName,
        //     //     CourseDuration = course.CourseDuration,
        //     //     CourseType = (Cms.WebApi.DTOs.COURSE_TYPE)course.CourseType
        //     // };
        // }
        #endregion Custom Mapper Functions
    }
}