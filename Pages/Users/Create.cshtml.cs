using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using RazorLesson.DataAcess;
using RazorLesson.Models;

namespace RazorLesson.Pages.Users
{
    [Authorize(Policy = "Admins")]
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;

        [BindProperty]
        public User User { get; set; }

        [BindProperty]
        public int[] SelectedRoleIds { get; set; }

        public SelectList Roles { get; set; }

        public CreateModel(AppDbContext context)
        {
            _context = context;
        }

        public async Task OnGet()
        {
            Roles = new SelectList(await _context.Roles.ToListAsync(), nameof(Role.Id), nameof(Role.Name));
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid && SelectedRoleIds?.Any() == true)
            {
                User.UserRoles = new List<UserRoles>();

                foreach (var selectedRoleId in SelectedRoleIds)
                {
                    User.UserRoles.Add(new UserRoles { RoleId = selectedRoleId, User = User });
                }

                await _context.Users.AddAsync(User);
                await _context.SaveChangesAsync();

                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}
