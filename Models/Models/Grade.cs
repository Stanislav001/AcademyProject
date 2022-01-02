using Models.Base;

namespace Models.Models
{
    public class Grade : BaseModel
    {
        public int StudentGrade { get; set; }
        public string StudentId { get; set; }
        public Student Student { get; set; }
        public string CourseId { get; set; }
        public Course Course { get; set; }
    }
}