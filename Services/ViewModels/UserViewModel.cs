using System;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace Services.ViewModels
{
    public class UserViewModel
    {
        public string Profession { get; set; }
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string ImageName { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public string Country { get; set; }

        // Student info
        public string StudentFirstName { get; set; }
        public string StudentSecondName { get; set; }
        public string StudentLastName { get; set; }
        public string StudentPhoneNumber { get; set; }
        public string StudentEmail { get; set; }

        // Course Info
        public string CourseName { get; set; }
        public decimal CoursePrice { get; set; }
        public string CourseDuration { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        // Post Info
        public string PostTitle { get; set; }
    }
}