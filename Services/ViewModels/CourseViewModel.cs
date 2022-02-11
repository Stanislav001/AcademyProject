using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Services.ViewModels
{
    public class CourseViewModel
    {
        public string Id { get; set; }
        public string CourseName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Duration { get; set; }
        public string ImageName { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool CurrentUserIsVoted { get; set; }

        public List<TeacherViewModel> Teachers { get; set; }
        public List<StudentViewModel> Students { get; set; }
    }
}