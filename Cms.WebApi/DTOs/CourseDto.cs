namespace Cms.WebApi.DTOs
{
    // the DTO are models for the web api (client exposed)
    // see cms.data.repository > models
    public class CourseDto
    {
        public int CourseId { get; set; }

        public string CourseName { get; set; }

        public int CourseDuration { get; set; }

        public COURSE_TYPE CourseType { get; set; }

    }
    public enum COURSE_TYPE
    {
        ENGINEERING,
        MEDICAL,
        MANAGEMENT
    }
}