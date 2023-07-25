using Cms.Data.Repository.Models;

namespace Cms.Data.Repository.Repositories
{
    public class InMemoryCmsRepository: ICmsRepository
    {
        public InMemoryCmsRepository()
        {

        }

        public IEnumerable<Course> GetAllCourses()
        {
            return null;
        }
    }
}