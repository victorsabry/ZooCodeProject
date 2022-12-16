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
    public class DetailsModel : PageModel
    {
        private readonly ZooCode.Data.ZooProjectContext _context;

        public DetailsModel(ZooCode.Data.ZooProjectContext context)
        {
            _context = context;
        }

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
    }
}
