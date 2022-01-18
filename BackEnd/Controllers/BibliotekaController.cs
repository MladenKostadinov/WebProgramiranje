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
            try
            {
                var x = await bibliotekaContext.Biblioteke.Include(p => p.Knjige).ThenInclude(q => q.Izdavac).ToListAsync();
                if (x != null)
                    return x;

                return BadRequest("Ne postoji ni jedna biblioteka!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost]
        [Route("DodajNovuBiblioteku")]
        public async Task<ActionResult> DodajBiblioteku([FromBody] Biblioteka biblioteka)
        {
            try
            {
                if (biblioteka != null)
                {
                    bibliotekaContext.Biblioteke.Add(biblioteka);
                    await bibliotekaContext.SaveChangesAsync();

                    return Ok("Uspesno dodata biblioteka: " + biblioteka.Ime);
                }
                else return BadRequest("Greska!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("BrisiBiblioteku")]
        public async Task<ActionResult> BrisiBiblioteku(int IdBiblioteke)
        {
            try
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
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet]
        [Route("Provera")]
        public async Task<ActionResult> Provera(int m, int n, int IdBiblioteke)
        {
            try
            {
                var biblioteka = await bibliotekaContext.Biblioteke.FindAsync(IdBiblioteke);
                if (biblioteka != null)
                {
                    int formula = n + m * biblioteka.N;
                    var knjige = await bibliotekaContext.Knjige.Where(p => p.BibliotekaOveKnjige.ID == IdBiblioteke).ToListAsync();

                    return Ok("M: " + knjige[formula].M + " N: " + knjige[formula].N);
                }
                else return BadRequest("Greska!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}

