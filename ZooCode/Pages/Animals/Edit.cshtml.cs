using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using ZooCode.Data;
using ZooCode.Models;

namespace ZooCode.Pages.Animals
{
    public class EditModel : PageModel
    {
        private readonly ZooCode.Data.ZooProjectContext _context;

        public EditModel(ZooCode.Data.ZooProjectContext context)
        {
            _context = context;
        }

        [BindProperty]        
        public Animal Animal { get; set; }
        public string ErrorEditMSG { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Animal = await _context.Animal.FirstOrDefaultAsync(m => m.AnimalID == id);

            if (Animal == null)
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
            
            bool exists = _context.Animal.Any(a => a.Animal_name == Animal.Animal_name);
            if (exists)
            {
                ErrorEditMSG= "THAT ANIMAL ALREADY EXISTS. TRY A DIFFERENT ONE.";
                return Page();
            }

            _context.Attach(Animal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnimalExists(Animal.AnimalID))
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

        private bool AnimalExists(int id)
        {
            return _context.Animal.Any(e => e.AnimalID == id);
        }
    }
}
