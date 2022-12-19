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

namespace ZooCode.Pages.ZooAnimals
{
    public class EditModel : PageModel
    {
        private readonly ZooCode.Data.ZooProjectContext _context;

        public EditModel(ZooCode.Data.ZooProjectContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ZooAnimal ZooAnimal { get; set; }
        public string ErrorEditMSG { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ZooAnimal = await _context.ZooAnimal
                .Include(z => z.Animal)
                .Include(z => z.Zoo).FirstOrDefaultAsync(m => m.ZooAnimalID == id);

            if (ZooAnimal == null)
            {
                return NotFound();
            }
           ViewData["AnimalID"] = new SelectList(_context.Animal, "AnimalID", "Animal_name");
           ViewData["ZooID"] = new SelectList(_context.Zoo, "ZooID", "Zoo_name");
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

            bool exists = _context.ZooAnimal.Any(za => za.AnimalID == ZooAnimal.AnimalID && za.ZooID == ZooAnimal.ZooID);
            if (exists)
            {
                ErrorEditMSG = "THIS RELATION BETWEEN ZOO AND ANIMAL ALREADY EXISTS. TRY A DIFFERENT RELATION.";                
                ViewData["AnimalID"] = new SelectList(_context.Animal, "AnimalID", "Animal_name");
                ViewData["ZooID"] = new SelectList(_context.Zoo, "ZooID", "Zoo_name");
                return Page();
            }

            _context.Attach(ZooAnimal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ZooAnimalExists(ZooAnimal.ZooAnimalID))
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

        private bool ZooAnimalExists(int id)
        {
            return _context.ZooAnimal.Any(e => e.ZooAnimalID == id);
        }
    }
}
