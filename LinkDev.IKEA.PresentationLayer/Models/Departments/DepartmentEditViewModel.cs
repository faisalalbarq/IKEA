using System.ComponentModel.DataAnnotations;

namespace LinkDev.IKEA.PresentationLayer.Models.Departments
{
    public class DepartmentEditViewModel
    {
        [Required(ErrorMessage = "The Code is required")]
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        [Display(Name = "Creation Date")]
        public DateTime CreationDate { get; set; }
    }
}
