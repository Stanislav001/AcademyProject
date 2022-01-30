namespace Models.Models
{
    public class CourseStudent
    {
        public string CourseId { get; set; }
        public string CourseName { get; set; }
        public Course Course { get; set; }
        public string StudentId { get; set; }
        public string StudentName { get; set; }
        public Student Student { get; set; }
    }
}