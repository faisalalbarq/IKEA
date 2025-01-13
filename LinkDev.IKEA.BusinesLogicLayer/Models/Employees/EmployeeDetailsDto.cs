﻿using LinkDev.IKEA.DataAccessLayer.Common.Enums;
using System.ComponentModel.DataAnnotations;
namespace LinkDev.IKEA.BusinesLogicLayer.Models.Employees
{
    public class EmployeeDetailsDto
    {

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? Age { get; set; }

        public string? Address { get; set; }


        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        [Display(Name = "Hiring Date")]
        public DateOnly HiringDate { get; set; }

        public Gender Gender { get; set; } 
        public EmployeeType EmployeeType { get; set; }
        public string? Department {  get; set; }

        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int LastModifiedBy { get; set; }
        public DateTime LastModifiedOn { get; set; }
    }
}
