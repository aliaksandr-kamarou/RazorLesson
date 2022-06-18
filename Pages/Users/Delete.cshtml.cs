using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorLesson.DataAcess;
using RazorLesson.Models;

namespace RazorLesson.Pages.Users
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly AppDbContext _context;

        [BindProperty]
        public User User { get; set; }

        public DeleteModel(AppDbContext context)
        {
            _context = context;
        }

        public async Task OnGet(int id)
        {
            User = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IActionResult> OnPost()
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == User.Id);

            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("Index");
        }

    }
}
