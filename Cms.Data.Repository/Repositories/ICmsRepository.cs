using Cms.Data.Repository.Models;

namespace Cms.Data.Repository.Repositories
{
    public interface ICmsRepository
    {
        IEnumerable<Course> GetAllCourses();

        Task<IEnumerable<Course>> GetAllCoursesAsync();

        Course AddCourse(Course newCourse);

        bool IsCourseExists(int coursedId);

        Course GetCourse(int courseId);
    }
}