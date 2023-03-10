using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ZooCode.Data;
using ZooCode.Models;

namespace ZooCode.Pages.Animals
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
            return Page();
        }

        [BindProperty]
        public Animal Animal { get; set; }
        public string ErrorCreateMSG { get; set; }  

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            bool exists = _context.Animal.Any(a => a.Animal_name == Animal.Animal_name);
            if (exists)
            {
                ErrorCreateMSG = "THAT ANIMAL ALREADY EXISTS. TRY A DIFFERENT ONE.";
                return Page(); 
            }
               

            _context.Animal.Add(Animal);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
