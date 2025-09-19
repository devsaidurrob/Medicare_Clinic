using Medicare.Repository.Interfaces;
using Medicare.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Medicare.Controllers
{

    public class AccountController : Controller
    {
        private readonly IUserRepository _repo;
        private readonly IDoctorRepository _doctorRepository;
        public AccountController(IUserRepository userRepository, IDoctorRepository doctorRepository)
        {
            _repo = userRepository;
            _doctorRepository = doctorRepository;
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var user = await _repo.FindUserByAsync(model.UserName);

            if (user != null && BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
            {
                var profileImg = string.IsNullOrEmpty("")
                                    ? $"https://ui-avatars.com/api/?name={user.FirstName} {user.LastName}&background=random"
                                    : "";
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim("FullName", $"{user.FirstName} {user.LastName}"),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
                    new Claim("ProfilePhoto", profileImg)
                };

                if (user.Roles.Any(x => x.Role.Name == "Doctor"))
                {
                    var doctorResult = await _doctorRepository.GetByIdAsync(user.Id);
                    foreach(var dept in doctorResult.DoctorDepartments)
                    {
                        if (dept.Department != null)
                            claims.Add(new Claim("Department", dept.Department.Name));
                    }
                    foreach (var spec in doctorResult.DoctorSpecializations)
                    {
                        if (spec.Specialization != null)
                            claims.Add(new Claim("Specialization", spec.Specialization.Name));
                    }
                }
                // Add a claim for each role
                foreach (var userRole in user.Roles)
                {
                    if (userRole.Role != null)
                        claims.Add(new Claim(ClaimTypes.Role, userRole.Role.Name));
                }

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(30)
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                if (user.Roles.Any(x => x.Role.Name == "Doctor"))
                    return RedirectToAction("Index", "Doctor");
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Invalid login attempt");
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        public IActionResult AccessDenied() => View();
    }
}
