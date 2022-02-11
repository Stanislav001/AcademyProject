using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Services.ViewModels.RankingViewModels
{
    public class TopCourseViewModel
    {
        public string Id { get; set; }
        public string CourseName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Duration { get; set; }
        public int Votes { get; set; }
        public string ImageName { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}