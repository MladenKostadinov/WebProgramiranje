using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Drawing;
using System;

namespace Projekat.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KnjigaController : ControllerBase
    {

        private BibliotekaContext bibliotekaContext { get; set; }


        public KnjigaController(BibliotekaContext context)
        {
            bibliotekaContext = context;
        }

        [HttpPost]
        [Route("DodajKnjigu")]
        public async Task<ActionResult> DodajKnjigu([FromBody] Knjiga knjiga, int IdBiblioteke)
        {
            try
            {
                var biblioteka = await bibliotekaContext.Biblioteke.FindAsync(IdBiblioteke);

                if (biblioteka != null && knjiga != null)
                {
                    if (knjiga.M < 0 || knjiga.M > biblioteka.M)
                        return BadRequest("M je nevalidno!");

                    if (knjiga.N < 0 || knjiga.N > biblioteka.N)
                        return BadRequest("N je nevalidno!");

                    if (knjiga.TrenKolicina <= 0)
                        return BadRequest("Broj knjiga koji se dodaje mora da bude veci od 0");

                    if (knjiga.TrenKolicina > biblioteka.MaxKolicina)
                        return BadRequest("Max kolicina knjige je: " + biblioteka.MaxKolicina);

                    int index = knjiga.N + knjiga.M * biblioteka.N;

                    var x = await bibliotekaContext.Knjige.Where(p => p.BibliotekaOveKnjige.ID == IdBiblioteke).Include(q => q.Izdavac).ToListAsync();

                    var izdavaciList = await bibliotekaContext.Izdavaci.ToListAsync();

                    bool t = izdavaciList.Any(izdavac => izdavac.ID == knjiga.Izdavac.ID && izdavac.Naziv == knjiga.Izdavac.Naziv);
                    if (!t)
                        return BadRequest("Pogresan izdavac!");

                    if (biblioteka.Knjige == null)
                        biblioteka.Knjige = new List<Knjiga>();

                    var pom = 0;
                    foreach (var book in x)
                    {
                        if (book.M == knjiga.M && book.N == knjiga.N)
                        {
                            pom = 1;
                            break;
                        }
                    }
                    if (pom == 0)
                    {
                        var knjigaP = new Knjiga();
                        knjigaP.Naslov = knjiga.Naslov;
                        knjigaP.TrenKolicina += knjiga.TrenKolicina;
                        knjigaP.Autor = knjiga.Autor;
                        knjigaP.M = knjiga.M;
                        knjigaP.N = knjiga.N;
                        var izdavac = await bibliotekaContext.Izdavaci.FindAsync(knjiga.Izdavac.ID);
                        knjigaP.Izdavac = izdavac;
                        biblioteka.Knjige.Add(knjigaP);
                        await bibliotekaContext.SaveChangesAsync();
                        return Ok("Uspesno dodata nova knjiga sa m: " + knjiga.M + " n: " + knjiga.N);
                    }
                    else if (pom == 1)
                    {
                        return BadRequest("Vec postoji knjiga na lokaciji: M: " + knjiga.M + " N: " + knjiga.N + "!");
                    }
                    return BadRequest("Greska!");
                }
                else return BadRequest("Greska!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("VratiSveKnjige")]
        public async Task<ActionResult<List<Knjiga>>> Test(int IdBiblioteke)
        {
            try
            {
                var x = await bibliotekaContext.Knjige.Where(p => p.BibliotekaOveKnjige.ID == IdBiblioteke).Include(q => q.Izdavac).ToListAsync();
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
        [Route("DodajSliku")]
        public async Task<ActionResult> DodajSliku(IFormFile img, int IdKnjige)
        {
            try
            {
                var x = await bibliotekaContext.Knjige.Where(p => p.ID == IdKnjige).FirstOrDefaultAsync();
                if (img != null && x != null)
                {
                    x.Slika = GetByteArrayFromImage(img);
                    //x.ImageContentType = img.ContentType;
                    await bibliotekaContext.SaveChangesAsync();
                    return Ok("Uspesno dodata slika za knjigu: " + x.Naslov);
                }
                return BadRequest("Ne postoji knjiga ili slika!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        private byte[] GetByteArrayFromImage(IFormFile file)
        {
            using (var target = new MemoryStream())
            {
                file.CopyTo(target);
                return target.ToArray();
            }
        }
        [HttpGet]
        [Route("PreuzmiSliku")]
        public async Task<ActionResult> PreuzmiSliku(int IdKnjige)
        {
            try
            {
                var x = await bibliotekaContext.Knjige.Where(p => p.ID == IdKnjige).FirstOrDefaultAsync();
                if (x != null)
                {
                    return File(x.Slika, "image/png");
                }
                return BadRequest("Ne postoji knjiga sa id: " + IdKnjige);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpDelete]
        [Route("BrisiKnjigu/{IdKnjige}")]
        public async Task<ActionResult> BrisiKnjigu(int IdKnjige)
        {
            try
            {
                var x = await bibliotekaContext.Knjige.Where(p => p.ID == IdKnjige).FirstOrDefaultAsync();
                if (x != null)
                {
                    string naslov = x.Naslov;
                    bibliotekaContext.Knjige.Remove(x);
                    await bibliotekaContext.SaveChangesAsync();
                    return Ok("Uspesno obrisana knjiga: " + naslov);
                }
                return BadRequest("Ne postoji knjiga sa id: " + IdKnjige);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet]
        [Route("PreuzmiIdKnjige")]
        public async Task<ActionResult<int>> PreuzmiIdKnjige(int M, int N, int IdBiblioteke)
        {
            try
            {
                var biblioteka = await bibliotekaContext.Biblioteke.FindAsync(IdBiblioteke);
                var knjige = await bibliotekaContext.Knjige.Where(p => p.BibliotekaOveKnjige.ID == IdBiblioteke).ToListAsync();

                if (biblioteka == null)
                    return BadRequest("Ne postoji biblioteka sa id: " + IdBiblioteke);

                foreach (var book in knjige)
                {
                    if (book.M == M && book.N == N)
                        return book.ID;
                }
                return BadRequest("Ne postoji knjiga na m: " + M + " n: " + N);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut]
        [Route("IzmeniOpis")]
        public async Task<ActionResult> IzmeniOpis(int IdKnjige, string opis)
        {
            try
            {
                if (opis.Length > 999)
                    return BadRequest("Predugacak opis!");

                var x = await bibliotekaContext.Knjige.Where(p => p.ID == IdKnjige).FirstOrDefaultAsync();
                if (x != null)
                {
                    x.Opis = opis;
                    await bibliotekaContext.SaveChangesAsync();
                    return Ok("Uspesno izmenjen opis!");
                }
                return BadRequest("Ne postoji knjiga sa id: " + IdKnjige);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet]
        [Route("PreuzmiOpis")]
        public async Task<ActionResult<string>> PreuzmiOpis(int IdKnjige)
        {
            try
            {
                var x = await bibliotekaContext.Knjige.Where(p => p.ID == IdKnjige).FirstOrDefaultAsync();
                if (x != null)
                {
                    return x.Opis;
                }
                return BadRequest("Ne postoji knjiga sa id: " + IdKnjige);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut]
        [Route("IzmeniIzdavacaKnjige")]
        public async Task<ActionResult> IzmeniIzdavacaKnjige(int IdKnjige, string naziv)
        {
            try
            {
                var x = await bibliotekaContext.Knjige.Where(p => p.ID == IdKnjige).Include(q => q.Izdavac).FirstOrDefaultAsync();
                var y = await bibliotekaContext.Izdavaci.ToListAsync();
                foreach (var izdavac in y)
                {
                    if (naziv == izdavac.Naziv)
                    {
                        if (x != null)
                        {
                            var izdavac1 = await bibliotekaContext.Izdavaci.FindAsync(izdavac.ID);
                            x.Izdavac = izdavac1;
                            await bibliotekaContext.SaveChangesAsync();
                            return Ok("Uspesno izmenjen opis!");
                        }
                        else
                        {
                            return BadRequest("Ne postoji knjiga sa id: " + IdKnjige);
                        }
                    }
                }
                return BadRequest("Ne postoji izdavac sa nazivom: " + naziv);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet]
        [Route("PreuzmiIzdavaca")]
        public async Task<ActionResult<string>> PreuzmiIzdavaca(int IdKnjige)
        {
            try
            {
                var x = await bibliotekaContext.Knjige.Where(p => p.ID == IdKnjige).Include(q => q.Izdavac).FirstOrDefaultAsync();
                if (x != null)
                {
                    return x.Izdavac.Naziv;
                }
                return BadRequest("Ne postoji knjiga sa id: " + IdKnjige);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut]
        [Route("IzmeniNaslovKnjige")]
        public async Task<ActionResult> IzmeniNaslovKnjige(int IdKnjige, string naslov)
        {
            try
            {
                var x = await bibliotekaContext.Knjige.Where(p => p.ID == IdKnjige).FirstOrDefaultAsync();
                if (x != null)
                {
                    x.Naslov = naslov;
                    await bibliotekaContext.SaveChangesAsync();
                    return Ok("Uspesno izmenjen naslov!");
                }
                else
                {
                    return BadRequest("Ne postoji knjiga sa id: " + IdKnjige);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet]
        [Route("PreuzmiNaslovKnjige")]
        public async Task<ActionResult<string>> PreuzmiNaslovKnjige(int IdKnjige)
        {
            try
            {
                var x = await bibliotekaContext.Knjige.Where(p => p.ID == IdKnjige).FirstOrDefaultAsync();
                if (x != null)
                {
                    return x.Naslov;
                }
                return BadRequest("Ne postoji knjiga sa id: " + IdKnjige);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut]
        [Route("IzmeniAutoraKnjige")]
        public async Task<ActionResult> IzmeniAutoraKnjige(int IdKnjige, string autor)
        {
            try
            {
                var x = await bibliotekaContext.Knjige.Where(p => p.ID == IdKnjige).FirstOrDefaultAsync();
                if (x != null)
                {
                    x.Autor = autor;
                    await bibliotekaContext.SaveChangesAsync();
                    return Ok("Uspesno izmenjen autor!");
                }
                else
                {
                    return BadRequest("Ne postoji knjiga sa id: " + IdKnjige);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet]
        [Route("PreuzmiAutoraKnjige")]
        public async Task<ActionResult<string>> PreuzmiAutoraKnjige(int IdKnjige)
        {
            try
            {
                var x = await bibliotekaContext.Knjige.Where(p => p.ID == IdKnjige).FirstOrDefaultAsync();
                if (x != null)
                {
                    return x.Autor;
                }
                return BadRequest("Ne postoji knjiga sa id: " + IdKnjige);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut]
        [Route("IzmeniKolicinu")]
        public async Task<ActionResult> IzmeniKolicinu(int IdKnjige, int Nkolicina)
        {
            try
            {
                if (Nkolicina < 0)
                    return BadRequest("Kolicina mora da bude 0 ili vise!");

                var x = await bibliotekaContext.Knjige.Where(p => p.ID == IdKnjige).Include(q => q.BibliotekaOveKnjige).FirstOrDefaultAsync();
                if (x != null)
                {
                    if (Nkolicina > x.BibliotekaOveKnjige.MaxKolicina)
                        return BadRequest("Max kolicina je: " + x.BibliotekaOveKnjige.MaxKolicina);

                    x.TrenKolicina = Nkolicina;
                    await bibliotekaContext.SaveChangesAsync();
                    return Ok("Uspesno izmenjen opis!");
                }
                return BadRequest("Ne postoji knjiga sa id: " + IdKnjige);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet]
        [Route("PreuzmiKolicinu")]
        public async Task<ActionResult<int>> PreuzmiKolicinu(int IdKnjige)
        {
            try
            {
                var x = await bibliotekaContext.Knjige.Where(p => p.ID == IdKnjige).FirstOrDefaultAsync();
                if (x != null)
                {
                    return x.TrenKolicina;
                }
                return BadRequest("Ne postoji knjiga sa id: " + IdKnjige);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}