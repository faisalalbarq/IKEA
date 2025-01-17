﻿using LinkDev.IKEA.DataAccessLayer.Common.Enums;

namespace LinkDev.IKEA.DataAccessLayer.Models
{
    public class Employee : ModelBase
    {
        //[Required]
        //[MaxLength(50, ErrorMessage = "Max Length of Name is 50 Characters")]
        //[MinLength(5, ErrorMessage = "Min Length of Name is 5  Characters")]
        public string Name { get; set; } = null!;

        //[Range(22,30)]
        public int? Age { get; set; }

        //[RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4-10}-[a-zA-z]{5-10}$", ErrorMessage ="Address must be like 123-Street-City-Country")]
        public string? Address { get; set; }

        //[DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        //[Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        //[EmailAddress]
        public string? Email { get; set; }

        //[Display(Name = "Phone Number")]
        //[Phone]
        public string? PhoneNumber { get; set; }

        //[Display(Name = "Hiring Date")]
        public DateOnly HiringDate { get; set; }

        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }

        public int? DepartmentId { get; set; }

        // Fully qualify the Department type to avoid ambiguity
        public virtual Department? Department { get; set; }

        public string? Image { get; set; }
    }
}
