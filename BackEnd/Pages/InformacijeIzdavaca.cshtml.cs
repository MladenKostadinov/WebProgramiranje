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
    public class InformacijeIzdavacaModel : PageModel
    {
        public int Id { get; set; }
        public string deskripcija { get; set; }

        private BibliotekaContext bibliotekaContext;

        public InformacijeIzdavacaModel(BibliotekaContext context)
        {
            bibliotekaContext = context;
        }
        public async Task OnGet(int Id)
        {
            this.Id = Id;
            var izdavac = await bibliotekaContext.Izdavaci.Where(p=>p.ID==Id).FirstOrDefaultAsync();
            if(izdavac!=null)
            {
                deskripcija=izdavac.Deskripcija;
            }
        }
    }
}
