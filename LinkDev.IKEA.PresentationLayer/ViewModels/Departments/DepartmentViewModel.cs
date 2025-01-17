﻿using System.ComponentModel.DataAnnotations;

namespace LinkDev.IKEA.PresentationLayer.ViewModels.Departments
{
    public class DepartmentViewModel
    {
        [Required(ErrorMessage = "The Code is required")]
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        [Display(Name = "Creation Date")]
        public DateTime CreationDate { get; set; }
    }
}
