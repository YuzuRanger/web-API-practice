using Cms.Data.Repository.Models;
using Cms.Data.Repository.Repositories;
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

        public IEnumerable<Course> GetCourses()
        {
            return CmsRepository.GetAllCourses();
            
        }
    }
}