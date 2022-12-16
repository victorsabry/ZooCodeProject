using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZooCode.Data;
using ZooCode.Models;

namespace ZooCode.Pages.Zoos
{
    public class DeleteModel : PageModel
    {
        private readonly ZooCode.Data.ZooProjectContext _context;

        public DeleteModel(ZooCode.Data.ZooProjectContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Zoo Zoo { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Zoo = await _context.Zoo.FirstOrDefaultAsync(m => m.ZooID == id);

            if (Zoo == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Zoo = await _context.Zoo.FindAsync(id);

            if (Zoo != null)
            {
                _context.Zoo.Remove(Zoo);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
