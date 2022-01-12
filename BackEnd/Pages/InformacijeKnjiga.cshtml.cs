using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Projekat.Pages
{
    public class InformacijeKnjigaModel : PageModel
    {
        private BibliotekaContext bibliotekaContext;

        public Knjiga knjiga { get; set; }
        public byte[] slika { get; set; }
        public InformacijeKnjigaModel(BibliotekaContext context)
        {
            bibliotekaContext = context;
        }

        public async Task OnGet(int IdKnjige)
        {
            var knjiga = await bibliotekaContext.Knjige.Where(p=>p.ID==IdKnjige).Include(q=>q.Izdavac).FirstOrDefaultAsync();
            if(knjiga!=null)
            {
                this.knjiga=knjiga;  
                slika = knjiga.Slika;
            }
        }
    }
}
