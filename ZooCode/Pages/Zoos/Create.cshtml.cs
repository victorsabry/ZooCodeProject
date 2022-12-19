using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ZooCode.Data;
using ZooCode.Models;

namespace ZooCode.Pages.Zoos
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
        public Zoo Zoo { get; set; }
        public string ErrorMSGCreate { get; set; }
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            bool exists = _context.Zoo.Any(z => z.Zoo_name == Zoo.Zoo_name && z.Zoo_address == Zoo.Zoo_address);
            if (exists)
            {                
                ErrorMSGCreate = "THAT ZOO ALREADY EXISTS. TRY A DIFFERENT NAME OR ADDRESS.";
                return Page();               
            }

            _context.Zoo.Add(Zoo);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
