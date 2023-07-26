using Cms.Data.Repository.Models;

namespace Cms.Data.Repository.Repositories
{
    public interface ICmsRepository
    {
        IEnumerable<Course> GetAllCourses();

        Task<IEnumerable<Course>> GetAllCoursesAsync();

        // Individual items
        Course AddCourse(Course newCourse);

        bool IsCourseExists(int coursedId);

        Course GetCourse(int courseId);

        Course UpdateCourse(int courseId, Course newCourse);

        Course DeleteCourse(int courseId);

        // Association
        IEnumerable<Student> GetStudents(int coursedId);
        Student AddStudent(Student student);
    }
}