using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZooCode.Data;
using ZooCode.Models;

namespace ZooCode.Pages.Zoos
{
    public class EditModel : PageModel
    {
        private readonly ZooCode.Data.ZooProjectContext _context;

        public EditModel(ZooCode.Data.ZooProjectContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Zoo Zoo { get; set; }
        public string ErrorEditMSG { get; set; }

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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            bool exists = _context.Zoo.Any(z => z.Zoo_name == Zoo.Zoo_name && z.Zoo_address == Zoo.Zoo_address);
            if (exists)
            {
                ErrorEditMSG = "THAT ZOO ALREADY EXISTS. TRY A DIFFERENT NAME OR ADDRESS.";
                return Page();                
            }

            _context.Attach(Zoo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ZooExists(Zoo.ZooID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ZooExists(int id)
        {
            return _context.Zoo.Any(e => e.ZooID == id);
        }
    }
}
