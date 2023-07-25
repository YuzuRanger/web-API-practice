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
        public CoursesController(ICmsRepository cmsRepository)
        {
            this.CmsRepository = cmsRepository;
        }

        public ICmsRepository CmsRepository { get; }

        // [HttpGet]
        // public IEnumerable<Course> GetCourses()
        // {
        //     return CmsRepository.GetAllCourses();
        // }

        [HttpGet]
        public IEnumerable<CourseDto> GetCourses()
        {
            try
            {
                IEnumerable<Course> courses = CmsRepository.GetAllCourses();
                var result = MapCourseToCourseDto(courses);
                return result;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        // custom map function
        private CourseDto MapCourseToCourseDto(Course course)
        {
            return new CourseDto()
            {
                CourseId = course.CourseId,
                CourseName = course.CourseName,
                CourseDuration = course.CourseDuration,
                CourseType = (Cms.WebApi.DTOs.COURSE_TYPE)course.CourseType
            };
        }

        private IEnumerable<CourseDto> MapCourseToCourseDto(IEnumerable<Course> courses)
        {
            IEnumerable<CourseDto> result;

            result = courses.Select(c => new CourseDto()
            {
                CourseId = c.CourseId,
                CourseName = c.CourseName,
                CourseDuration = c.CourseDuration,
                CourseType = (Cms.WebApi.DTOs.COURSE_TYPE)c.CourseType
            });

            return result;

            // return new CourseDto(){
            //     CourseId = course.CourseId,
            //     CourseName = course.CourseName,
            //     CourseDuration = course.CourseDuration,
            //     CourseType = (Cms.WebApi.DTOs.COURSE_TYPE)course.CourseType
            // };
        }
    }
}