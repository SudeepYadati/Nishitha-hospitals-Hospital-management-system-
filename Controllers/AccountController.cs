using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using HospitalManagementSystem.Models;
using HospitalManagementSystem.Models.AccountViewModel;

public class AccountController : Controller
{
    private readonly UserManager<ApplicationUserModel> _userManager;
    private readonly SignInManager<ApplicationUserModel> _signInManager;

    public AccountController(UserManager<ApplicationUserModel> userManager, SignInManager<ApplicationUserModel> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    //  DISPLAY REGISTRATION PAGE
    [HttpGet]
    public IActionResult Register()
    {
        return View(new RegisterViewModel())
        ;
    }

   
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var user = new ApplicationUserModel
        {
            UserName = model.FullName,
            Email = model.Email,
            FullName = model.FullName,
            Role = model.Role
        };

        var result =  _userManager.CreateAsync(user, model.Password).Result;
        await _userManager.UpdateAsync(user);
        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, model.Role);
            return RedirectToAction(nameof(Login), "Account");  // Redirects to Login page
        }

        // Log errors for debugging
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError("", error.Description);
        }

        ModelState.AddModelError("", "Registration failed.");
        return View(model);
    }

    //  DISPLAY LOGIN PAGE
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    //  HANDLE USER LOGIN
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            ModelState.AddModelError("", "Invalid email or password.");
            return View(model);
        }

        var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, false, false);
        if (!result.Succeeded)
        {
            ModelState.AddModelError("", "Invalid email or password.");
            return View(model);
        }

        return RedirectToAction("Index", "Home"); //  Redirect to dashboard after login
    }

    //  HANDLE USER LOGOUT
   
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}
