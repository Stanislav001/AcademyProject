

namespace Services.ViewModels
{
    public class GradeViewModel
    {
        public string Id { get; set; }
        public int StudentGrade { get; set; }
        public string StudentId { get; set; }
        public StudentViewModel Student { get; set; }
        public string CourseId { get; set; }
        public CourseViewModel Course { get; set; }
    }
}
