using System.ComponentModel.DataAnnotations;

namespace LinkDev.IKEA.PresentationLayer.ViewModels.Identity
{
	public class SignInViewModel
	{
		[EmailAddress(ErrorMessage = "Invalid Email Address")]
		public string Email { get; set; } = null!;

		[DataType(DataType.Password)]
		public string Password { get; set; } = null!;

        public bool RememberMe { get; set; }
    }
}
