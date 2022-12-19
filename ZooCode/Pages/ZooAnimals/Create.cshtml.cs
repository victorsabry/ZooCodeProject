using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ZooCode.Data;
using ZooCode.Models;

namespace ZooCode.Pages.ZooAnimals
{
    public class CreateModel : PageModel
    {
        private readonly ZooCode.Data.ZooProjectContext _context;

        public CreateModel(ZooCode.Data.ZooProjectContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["AnimalID"] = new SelectList(_context.Animal, "AnimalID", "Animal_name");
            ViewData["ZooID"] = new SelectList(_context.Zoo, "ZooID", "Zoo_name");
            return Page();
        }

        [BindProperty]
        public ZooAnimal ZooAnimal { get; set; }
        public string ErrorCreateMSG { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            bool exists = _context.ZooAnimal.Any(za => za.AnimalID == ZooAnimal.AnimalID && za.ZooID == ZooAnimal.ZooID);
            if (exists)
            {
                ErrorCreateMSG = "THIS RELATION BETWEEN ZOO AND ANIMAL ALREADY EXISTS. TRY A DIFFERENT RELATION.";
                return OnGet();                                
            }

            _context.ZooAnimal.Add(ZooAnimal);            

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
