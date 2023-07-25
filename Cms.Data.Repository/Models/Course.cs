namespace Cms.Data.Repository.Models
{
    // take note that this is the data model. it represents the data in the db.
    // in a real web api, data models and data transfer objects should be separate.
    // the DTO are models for the web api (client exposed)
    // data models usually show the exact db schema
    // we want the data layer separated from the business layer
    // see cms.webapi > DTOs
    public class Course
    {
        public int CourseId { get; set; }

        public string CourseName { get; set; }

        public int CourseDuration { get; set; }

        public COURSE_TYPE CourseType { get; set; }

        public enum COURSE_TYPE
        {
            ENGINEERING,
            MEDICAL,
            MANAGEMENT
        }
    }
}