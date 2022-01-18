using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Projekat.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IzdavacController : ControllerBase
    {
        private BibliotekaContext bibliotekaContext { get; set; }

        public IzdavacController(BibliotekaContext context)
        {
            bibliotekaContext = context;
        }

        [HttpGet]
        [Route("PribaviIzdavace")]
        public async Task<ActionResult<List<Izdavac>>> PribaviIzdavace()
        {
            try
            {
                var x = await bibliotekaContext.Izdavaci.ToListAsync();
                if (x != null)
                {
                    return x;
                }
                else return BadRequest("Greska!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("DodajIzdavaca")]
        public async Task<ActionResult> DodajIzdavaca([FromBody] Izdavac izdavac)
        {
            try
            {
                if (izdavac != null)
                {
                    bibliotekaContext.Izdavaci.Add(izdavac);
                    await bibliotekaContext.SaveChangesAsync();
                    return Ok("Uspesno dodat izdavac: " + izdavac.Naziv);
                }
                else return BadRequest("Greska!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet]
        [Route("PribaviKnjigeIzdavaca/{idIzdavaca}/{idBiblioteke}")]
        public async Task<ActionResult<List<Knjiga>>> PribaviKnjigeIzdavaca(int idIzdavaca, int idBiblioteke)
        {
            try
            {
                var x = await bibliotekaContext.Knjige.Where(p => p.Izdavac.ID == idIzdavaca).Where(q => q.BibliotekaOveKnjige.ID == idBiblioteke).ToListAsync();
                if (x != null)
                {
                    return x;
                }
                else return BadRequest("Greska!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet]
        [Route("PribaviDeskripciju/{idIzdavaca}")]
        public async Task<ActionResult<string>> PribaviDeskripciju(int idIzdavaca)
        {
            try
            {
                var x = await bibliotekaContext.Izdavaci.Where(p => p.ID == idIzdavaca).FirstOrDefaultAsync();
                if (x != null)
                    return x.Deskripcija;
                else return BadRequest("Ne postoji izdavac sa id: " + idIzdavaca);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut]
        [Route("ModifikujDeskripciju/{idIzdavaca}")]
        public async Task<ActionResult> ModifikujDeskripciju(int idIzdavaca, string deskripcija)
        {
            try
            {
                var x = await bibliotekaContext.Izdavaci.Where(p => p.ID == idIzdavaca).FirstOrDefaultAsync();
                if (x != null)
                {
                    x.Deskripcija = deskripcija;
                    await bibliotekaContext.SaveChangesAsync();
                    return Ok("Uspesno izmenjena deskripcija!");
                }
                else return BadRequest("Ne postoji izdavac sa id: " + idIzdavaca);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpDelete]
        [Route("BrisiIzdavaca/{Id}")]
        public async Task<ActionResult> BrisiIzdavaca(int Id)
        {
            try
            {
                var x = await bibliotekaContext.Izdavaci.Where(p => p.ID == Id).FirstOrDefaultAsync();
                if (x != null)
                {
                    string pom = x.Naziv;
                    bibliotekaContext.Izdavaci.Remove(x);
                    await bibliotekaContext.SaveChangesAsync();
                    return Ok("Uspesno izbrisan izdavac sa Id: " + Id + " Naziv: " + pom);
                }
                else return BadRequest("Ne postoji izdavac sa id: " + Id);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet]
        [Route("PribaviIzdavaca")]
        public async Task<ActionResult<Izdavac>> PribaviIzdavaca(int idIzdavaca)
        {
            try
            {
                var x = await bibliotekaContext.Izdavaci.Where(p => p.ID == idIzdavaca).FirstOrDefaultAsync();
                if (x != null)
                {
                    return x;
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