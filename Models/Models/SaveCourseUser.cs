using System.ComponentModel.DataAnnotations;

using Models.Base;

namespace Models.Models
{
    public class SaveCourseUser : BaseModel
    {
        [Required]
        public string CourseId { get; set; }
        public virtual Course Course { get; set; }
        [Required]
        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}