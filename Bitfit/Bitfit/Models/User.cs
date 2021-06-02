using System;
using System.ComponentModel.DataAnnotations;

namespace Bitfit.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The Full Name field is required")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "The Gender field is required")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "The Age field is required")]
        [Range(13, int.MaxValue, ErrorMessage = "The Age field must be greater than 12")]
        public int Age { get; set; }
        [Required(ErrorMessage = "The Resting Heart Rate field is required")]
        [Range(1, int.MaxValue, ErrorMessage = "The Resting Heart Rate field must be greater than 0")]
        public int RestHeartRate { get; set; }
        public int Rank { get; set; }
        public int ScheduleId { get; set; }
    }
}
