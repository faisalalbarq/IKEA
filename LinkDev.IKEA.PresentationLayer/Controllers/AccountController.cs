using LinkDev.IKEA.DataAccessLayer.Models.Identity;
using LinkDev.IKEA.PresentationLayer.ViewModels.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.IKEA.PresentationLayer.Controllers
{
    public class AccountController : Controller
    {
        #region Sign Up
        /*
        first one i want to create an action method that will return the view for the sign up page.
        and then i want to create a layout that named _authLayout, and i will put (Creative signUp form).
        The title layoutpage will using @ViewData["Title"].
        Inside the AuthLayout.html i will remove the form code and put RenderBody() method, To using my Sign Up 
        View.
        Inside the Sign Up View i will choose the model that i will binding with it, @model IdentityUser,
        but not all the properties i want to use, so i will create a new class named SignUpViewModel,
        inside ViewModels folder, i will create a new folder named Identity, and i will add the first ViewModel for identity.
        and put my Form code inside the Sign Up View.


        then i will create a new action SignUp take SignUpViewModel as a parameter
        then i will make mapping between SignUpViewModel and IdentityUser.
        don't write mapping for password because i will make it in hashing format, and the IdentityUser
        dont have an IsAgree property, i will make customizing. i will create my own class that represent 
        the user in security module, inside the DAL in IdentityFolder i will create a new class named ApplicationUser 
        that inherit from IdentityUser, and i will add the IsAgree property & First last name,
        then i need to say to my project i will work using ApplicationUser not IdentityUser, so i will go to 
        ApplicationDbContext class and using the second overload of the IdentityDbContext that take ApplicationUser,
        the DbSet will be ApplicationUser not IdentityUser, then i will add the migration for this changes

        inside the AccountController in SignUp View => i will make mapping between SignUpViewModel and ApplicationUser.
        * i will add the First Last Name properties inside the SignUpViewModel.
        * inside the SignUp View i will add the First Last Name Form fields.
		 
		inside the signup view i will create the user, i must be call the service that i can manage the users
        i have 3 managers: userManager, signInManager, roleManager 
        i will ask the clr in AccountController to give me an object from class Usermanager with type ApplicationUser
        & SignInManager, Then i will Allow the Dependency Injection for these two managers.

        *Important: 
         inside the program.cs i will allow the dependency injection for the three services and the all services
         that these three services depend on it. ex(hash password, generate token)


        * now, when i create a user, i will using the _usermanager service, because i need to create a user 
		* using createAsync with second overloading that take user and user password parameters.
		* createAsync => create a user in the backing store (backing repository), that's mean the Create method 
		* that inside the usermanager Service will call the Add method that inside userStore Service, this service will be call the applicationDbcontext
		* that's means: creation object from class userManager thats dependecies set of options must be allow dependency injection for all these parameters
		* ex(userStore (Repository)) 
		* that's mean i must be allow the dependency injection for the userStore, using AddEntityFrameworkStores method => that's will be register the 
		* userStore to dependency injection container. and i must be chosse the identityStore that's will be call any DbContext that i want to use.
		* 
		* if i don't want the default identityStore, i can create my own store using .AddUserStore<MyUserStore>()
		* if i don't want the default UserManager, i can create my own store using .UserManager<>()
		*/

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [HttpGet] // GET: Account/SignUp
        public IActionResult SignUp()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user is { })
            {
                ModelState.AddModelError(nameof(SignUpViewModel.UserName), "This User Name is already in user for another account");
                // i can use nameof(SignUpViewModel.UserName) to send the error message to the specific field
                return View(model);
            }


            user = new ApplicationUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = model.Email,
                IsAgree = model.IsAgree
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(SignIn));
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }
        #endregion


        #region Sign In

        [HttpGet] // GET: Account/SignIn
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is { })
            {
                var CheckPassword = await _userManager.CheckPasswordAsync(user, model.Password);
                if (CheckPassword)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, true);


                    if (result.IsNotAllowed) // that's check if the email is not confirmed
                    {
                        ModelState.AddModelError(string.Empty, "your Email is not confirmed");
                    }

                    if (result.IsLockedOut)
                    {
                        ModelState.AddModelError(string.Empty, "your account is locked out");
                    }

                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(HomeController.Index), "Home");
                    }
                }

            }
			ModelState.AddModelError(string.Empty, "Invalid Email or Password");

			return View(model);
        }
        #endregion

        #region Sign Out
        [HttpGet]
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(SignIn));
        }
        #endregion
    }
} 