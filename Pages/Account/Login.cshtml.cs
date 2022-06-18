using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorLesson.DataAcess;
using RazorLesson.Models;

namespace RazorLesson.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly AppDbContext _context;

        public LoginModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public User User { get; set; }

        [BindProperty]
        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users
                    .Include(c => c.UserRoles)
                    .ThenInclude(c => c.Role)
                    .FirstOrDefaultAsync(u => u.Email.Equals(User.Email) && u.Password.Equals(User.Password));

                if (user == null)
                {
                    return Page();
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email)
                };

                foreach (var userRole in user.UserRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, userRole.Role.Name));
                }

                var identity = new ClaimsIdentity(claims, "RazorLessonCookieAuth");

                ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                var authProperties = new AuthenticationProperties { IsPersistent = RememberMe };

                await HttpContext.SignInAsync("RazorLessonCookieAuth", principal, authProperties);
            }

            return RedirectToPage("/Index");
        }
    }
}
