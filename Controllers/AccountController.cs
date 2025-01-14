using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Run.exe.Entities;
using Run.exe.Models;
using System.Security.Claims;

namespace Run.exe.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public IActionResult Index()
        {
            return View(_context.Users.ToList());
        }

        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registration(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserAccount account = new UserAccount();
                account.Email = model.Email;
                account.FirstName = model.FirstName;
                account.LastName = model.LastName;
                account.Password = model.Password;
                account.UserName = model.UserName;

                account.Age = model.Age;
                account.Gender = (Run.exe.Entities.Gender)model.Gender;
                account.PhoneNumber = model.PhoneNumber;
                account.Address = model.Address;
                account.TShirtSize = (Run.exe.Entities.TShirtSize)model.TShirtSize;
                account.MedicalConditions = model.MedicalConditions;
                account.WaiverAndLiabilityAcknowledged = model.WaiverAndLiabilityAcknowledged;
                account.PhotoVideoRelease = model.PhotoVideoRelease;
                account.TermsAndConditions = model.TermsAndConditions;

                try
                {
                    _context.Users.Add(account);
                    _context.SaveChanges();

                    ModelState.Clear();
                    ViewBag.Message = $"{account.FirstName} {account.LastName} registered successfully. Please Login.";
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "Please enter unique Email or Password.");
                    return View(model);
                }
            }
            return View(model);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users.Where(x => x.UserName == model.UserNameOrEmail || x.Email == model.UserNameOrEmail && x.Password == model.Password).FirstOrDefault();
                if (user != null)
                {
                    //Success, create cookie
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Email),
                        new Claim("Name", user.FirstName),
                        new Claim(ClaimTypes.Role, "User"),

                    };
                    
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction("SecurePage");
                }
                else
                {
                    ModelState.AddModelError("", "Username/Email or Password is not correct.");
                }
            }
            return View(model);
        }

        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult SecurePage()
        {
            // Get the logged-in user's email from the claims
            var userEmail = HttpContext.User.Identity.Name;

            // Check if the email is available (indicating that the user is logged in)
            if (string.IsNullOrEmpty(userEmail))
            {
                return RedirectToAction("Login"); // Redirect to login if no email found
            }

            // Fetch user details from the database using the email
            var user = _context.Users.FirstOrDefault(u => u.Email == userEmail);

            // If no user found, redirect to login (or display an error)
            if (user == null)
            {
                return RedirectToAction("Login");
            }

            // Set the ViewBag.Name to display the user's first name (or any other property)
            ViewBag.Name = user.FirstName;

            // Pass the user data to the SecurePage view
            return View(user); // Pass the UserAccount model to the view
        }




    }

}
