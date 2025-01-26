using System.ComponentModel.DataAnnotations;

namespace LinkDev.IKEA.PresentationLayer.ViewModels.Identity
{
	public class SignUpViewModel
	{
		[Display(Name = "First Name")]
		public string FirstName { get; set; } = null!;
		[Display(Name = "Last Name")]
		public string LastName { get; set; } = null!;


		[Required(ErrorMessage = "User Name is required")]
		public string UserName { get; set; } = null!;

		[EmailAddress(ErrorMessage = "Invalid Email Address")]
		public string Email { get; set; } = null!;

		[DataType(DataType.Password)]
		public string Password { get; set; } = null!;


		[Display(Name = "Confirm Password")]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "Password and Confirm Password do not match")]
		public string ConfirmPassword { get; set; } = null!; 


		public bool IsAgree { get; set; }
	}
}
