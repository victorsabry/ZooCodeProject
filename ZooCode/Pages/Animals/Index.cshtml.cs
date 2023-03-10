using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZooCode.Data;
using ZooCode.Models;

namespace ZooCode.Pages.Animals
{
    public class IndexModel : PageModel
    {
        private readonly ZooCode.Data.ZooProjectContext _context;

        public IndexModel(ZooCode.Data.ZooProjectContext context)
        {
            _context = context;
        }

        public IList<Animal> Animal { get;set; }

        public async Task OnGetAsync()
        {
            Animal = await _context.Animal.ToListAsync();
        }
    }
}
