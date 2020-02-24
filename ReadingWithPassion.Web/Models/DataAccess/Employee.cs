﻿using ReadingWithPassion.Web.Models.Helper;
using System.ComponentModel.DataAnnotations;

namespace ReadingWithPassion.Web.Models.DataAccess
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        [MinLength(5,ErrorMessage ="Name must be more than 5 characters")]
        [MaxLength(50,ErrorMessage ="Name cann't exceed 50 characters")]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",ErrorMessage ="Invalid Email")]
        [Display(Name ="Office email")]
        public string Email { get; set; }
        [Required]
        public Dept? Department { get; set; }
        public string PhotoPath { get; set; }
    }
}