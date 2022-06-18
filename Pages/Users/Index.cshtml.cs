using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorLesson.DataAcess;
using RazorLesson.Models;

namespace RazorLesson.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IEnumerable<User> Users { get; set; }

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public async Task OnGet()
        {
            Users = await _context.Users.ToListAsync();
        }
    }
}
