using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

using Models.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace Models.Models
{
   public class Course: BaseModel
    {
        public string CourseName { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Duration { get; set; }
        public int Votes { get; set; }
        public string ImageName { get; set; }
        public bool isStarted { get; set; }
        public bool isCompleted { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public string CourseId { get; set; }
        public string StudentId { get; set; }
        public List<CourseUser> CourseUser { get; set; }
    }
}