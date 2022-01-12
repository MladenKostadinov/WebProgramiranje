using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;

namespace Projekat.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BibliotekaController : ControllerBase
    {
        private BibliotekaContext bibliotekaContext { get; set; }

        public BibliotekaController(BibliotekaContext context)
        {
            bibliotekaContext = context;
        }

        [HttpGet]
        [Route("PribaviSveBiblioteke")]
        public async Task<ActionResult<List<Biblioteka>>> PribaviSveBiblioteke()
        {
            var x =await bibliotekaContext.Biblioteke.Include(p => p.Knjige).ThenInclude(q=>q.Izdavac).ToListAsync();
            if(x!=null)
            return x;
            
            return BadRequest("Ne postoji ni jedna biblioteka!");
        }
        [HttpPost]
        [Route("DodajNovuBiblioteku")]
        public async Task<ActionResult> DodajBiblioteku([FromBody] Biblioteka biblioteka)
        {
            bibliotekaContext.Biblioteke.Add(biblioteka);
            await bibliotekaContext.SaveChangesAsync();

            return Ok("Uspesno dodata biblioteka: " + biblioteka.Ime);
        }
        
        [HttpDelete]
        [Route("BrisiBiblioteku")]
        public async Task<ActionResult> BrisiBiblioteku(int IdBiblioteke)
        {
            var biblioteka = await bibliotekaContext.Biblioteke.FindAsync(IdBiblioteke);

            if (biblioteka != null)
            {
                var knjige = await bibliotekaContext.Knjige.Where(p => p.BibliotekaOveKnjige.ID == IdBiblioteke).ToListAsync();
                foreach (var knjiga in knjige)
                {
                    bibliotekaContext.Knjige.Remove(knjiga);
                }

                bibliotekaContext.Biblioteke.Remove(biblioteka);
                await bibliotekaContext.SaveChangesAsync();
                return Ok("Uspesno obrisana biblioteka: " + biblioteka.Ime);
            }


            return Ok("Ne postoji biblioteka sa ID: " + IdBiblioteke);
        }
        [HttpGet]
        [Route("Provera")]
        public async Task<ActionResult> Provera(int m, int n, int IdBiblioteke)
        {
            var biblioteka = await bibliotekaContext.Biblioteke.FindAsync(IdBiblioteke);
            int formula = n + m * biblioteka.N;
            var knjige = await bibliotekaContext.Knjige.Where(p => p.BibliotekaOveKnjige.ID == IdBiblioteke).ToListAsync();

            return Ok("M: " + knjige[formula].M + " N: " + knjige[formula].N);
        }
       
    }
}

