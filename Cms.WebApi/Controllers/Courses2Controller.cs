using AutoMapper;
using Cms.Data.Repository.Models;
using Cms.Data.Repository.Repositories;
using Cms.WebApi.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Cms.WebApi.Controllers
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("courses")]
    public class Courses2Controller: ControllerBase
    {
        public Courses2Controller(ICmsRepository cmsRepository, IMapper mapper)
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

                // version 2
                foreach (var item in result)
                {
                    item.CourseName += " (v2.0)";
                }

                return result.ToList(); // convert to support ActionResult<T>
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [MapToApiVersion("3.0")]
        // TO DO: THIS DOESNT WORK, IDK WHY
        // ../courses?api-version=3.0
        public ActionResult<IEnumerable<CourseDto>> GetCourses_v3()
        {
            try
            {
                IEnumerable<Course> courses = CmsRepository.GetAllCourses();
                // var result = MapCourseToCourseDto(courses);
                var result = mapper.Map<CourseDto[]>(courses);

                // version 3
                foreach (var item in result)
                {
                    item.CourseName += " (v3.0)";
                }

                return result.ToList(); // convert to support ActionResult<T>
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPut("{courseId}")]
        public ActionResult<CourseDto> UpdateCourse(int courseId, CourseDto course)
        {
            try
            {
                if(!CmsRepository.IsCourseExists(courseId))
                {
                    return NotFound();
                }
                Course updatedCourse = mapper.Map<Course>(course);
                updatedCourse = CmsRepository.UpdateCourse(courseId, updatedCourse);
                var result = mapper.Map<CourseDto>(updatedCourse);
                return result;
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{courseId}")]
        public ActionResult<CourseDto> DeleteCourse(int courseId)
        {
            try
            {
                if(!CmsRepository.IsCourseExists(courseId))
                {
                    return NotFound();
                }
                Course course = CmsRepository.DeleteCourse(courseId);

                if(course == null)
                {
                    return BadRequest();
                }
                var result = mapper.Map<CourseDto>(course);
                return result;
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        #region Async Methods
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
        #endregion Async Methods

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

        // GET ../courses/1/students
        [HttpGet("{courseId}/students")]
        public ActionResult<IEnumerable<StudentDto>> GetStudents(int courseId)
        {
            try
            {
                if(!CmsRepository.IsCourseExists(courseId))
                {
                    return NotFound();
                }

                IEnumerable<Student> students = CmsRepository.GetStudents(courseId);
                var result = mapper.Map<StudentDto[]>(students);
                return result;
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST ../courses/1/students
        [HttpPost("{courseId}/students")]
        public ActionResult<StudentDto> AddStudent(int courseId, StudentDto student)
        {
            try
            {
                if(!CmsRepository.IsCourseExists(courseId))
                {
                    return NotFound();
                }

                Student newStudent = mapper.Map<Student>(student);

                // assign course
                Course course = CmsRepository.GetCourse(courseId);
                newStudent.Course = course;

                newStudent = CmsRepository.AddStudent(newStudent);
                var result = mapper.Map<StudentDto>(newStudent);

                return StatusCode(StatusCodes.Status201Created, result);
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