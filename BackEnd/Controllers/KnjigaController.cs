using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Drawing;

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

            var biblioteka = await bibliotekaContext.Biblioteke.FindAsync(IdBiblioteke);

            if (biblioteka != null)
            {
                if (knjiga.M < 0 || knjiga.M > biblioteka.M)
                    return BadRequest("M je nevalidno!");

                if (knjiga.N < 0 || knjiga.N > biblioteka.N)
                    return BadRequest("N je nevalidno!");

                if (knjiga.TrenKolicina <= 0)
                    return BadRequest("Broj knjiga koji se dodaje mora da bude veci od 0");

                if (knjiga.TrenKolicina > biblioteka.MaxKolicina)
                    return BadRequest("Max kolicina knjige je: "+biblioteka.MaxKolicina);

                int index = knjiga.N + knjiga.M * biblioteka.N;

                var x = await bibliotekaContext.Knjige.Where(p => p.BibliotekaOveKnjige.ID == IdBiblioteke).Include(q => q.Izdavac).ToListAsync();

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
                if (x[index].Izdavac != null)
                {
                    if (knjiga.Izdavac.ID != x[index].Izdavac.ID)
                        return BadRequest("Razliciti izdavaci!");
                }
                if (x[index].Naslov == knjiga.Naslov && x[index].TrenKolicina + knjiga.TrenKolicina <= biblioteka.MaxKolicina && x[index].Autor == knjiga.Autor)
                {
                    x[index].TrenKolicina += knjiga.TrenKolicina;
                    await bibliotekaContext.SaveChangesAsync();
                    return Ok("Uspesno dopunjena knjiga na: " + knjiga.M + "," + knjiga.N + " biblioteke: " + biblioteka.Ime + " sa " + knjiga.TrenKolicina + " kopija!");
                }
                else if (x[index].Naslov == "")
                {
                    foreach (var item in x)
                    {
                        if (item.Naslov == knjiga.Naslov && item.TrenKolicina + knjiga.TrenKolicina <= biblioteka.MaxKolicina && item.Autor == knjiga.Autor)
                            return BadRequest("Postoji lokacija koja nije popunjenja: " + knjiga.Naslov + " x: " + item.M + " y: " + item.N);
                    }
                    x[index].Naslov = knjiga.Naslov;
                    x[index].TrenKolicina += knjiga.TrenKolicina;
                    x[index].Autor = knjiga.Autor;
                    var izdavac = await bibliotekaContext.Izdavaci.FindAsync(knjiga.Izdavac.ID);
                    x[index].Izdavac = izdavac;
                    await bibliotekaContext.SaveChangesAsync();
                    return Ok("Uspesno Dodata nova knjiga!");
                }
                else if (x[index].Naslov != knjiga.Naslov)
                {
                    return BadRequest("Razlicite knjige!");
                }
                else if (x[index].TrenKolicina + knjiga.TrenKolicina > biblioteka.MaxKolicina)
                {
                    return BadRequest("Nema dovljno slobodnih mesta na ovoj lokaciji!");
                }
                else if (x[index].Autor != knjiga.Autor)
                {
                    return BadRequest("Razliciti autori!");
                }

                return BadRequest("Greska!");
            }
            else
                return BadRequest("Biblioteka sa id: " + IdBiblioteke + " ne postoji!");
        }

        [HttpGet]
        [Route("VratiSveKnjige")]
        public async Task<ActionResult<List<Knjiga>>> Test(int IdBiblioteke)
        {
            return await bibliotekaContext.Knjige.Where(p => p.BibliotekaOveKnjige.ID == IdBiblioteke).Include(q => q.Izdavac).ToListAsync();

        }
        [HttpPost]
        [Route("DodajSliku")]
        public async Task<ActionResult> DodajSliku(IFormFile img, int IdKnjige)
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
            var x = await bibliotekaContext.Knjige.Where(p => p.ID == IdKnjige).FirstOrDefaultAsync();
            if(x!=null)
            {
                return File(x.Slika, "image/png");
            }
            return BadRequest("Ne postoji knjiga sa id: " + IdKnjige);
        }
        [HttpDelete]
        [Route("BrisiKnjigu/{IdKnjige}")]
        public async Task<ActionResult> BrisiKnjigu(int IdKnjige)
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
        [HttpGet]
        [Route("PreuzmiIdKnjige")]
        public async Task<ActionResult<int>> PreuzmiIdKnjige(int M, int N, int IdBiblioteke)
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
        [HttpPut]
        [Route("IzmeniOpis")]
        public async Task<ActionResult> IzmeniOpis(int IdKnjige, string opis)
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
        [HttpGet]
        [Route("PreuzmiOpis")]
        public async Task<ActionResult<string>> PreuzmiOpis(int IdKnjige)
        {
            var x = await bibliotekaContext.Knjige.Where(p => p.ID == IdKnjige).FirstOrDefaultAsync();
            if (x != null)
            {
                return x.Opis;
            }
            return BadRequest("Ne postoji knjiga sa id: " + IdKnjige);
        }
        [HttpPut]
        [Route("IzmeniIzdavacaKnjige")]
        public async Task<ActionResult> IzmeniIzdavacaKnjige(int IdKnjige, string naziv)
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
        [HttpGet]
        [Route("PreuzmiIzdavaca")]
        public async Task<ActionResult<string>> PreuzmiIzdavaca(int IdKnjige)
        {
            var x = await bibliotekaContext.Knjige.Where(p => p.ID == IdKnjige).Include(q => q.Izdavac).FirstOrDefaultAsync();
            if (x != null)
            {
                return x.Izdavac.Naziv;
            }
            return BadRequest("Ne postoji knjiga sa id: " + IdKnjige);
        }
        [HttpPut]
        [Route("IzmeniNaslovKnjige")]
        public async Task<ActionResult> IzmeniNaslovKnjige(int IdKnjige, string naslov)
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
        [HttpGet]
        [Route("PreuzmiNaslovKnjige")]
        public async Task<ActionResult<string>> PreuzmiNaslovKnjige(int IdKnjige)
        {
            var x = await bibliotekaContext.Knjige.Where(p => p.ID == IdKnjige).FirstOrDefaultAsync();
            if (x != null)
            {
                return x.Naslov;
            }
            return BadRequest("Ne postoji knjiga sa id: " + IdKnjige);
        }
        [HttpPut]
        [Route("IzmeniAutoraKnjige")]
        public async Task<ActionResult> IzmeniAutoraKnjige(int IdKnjige, string autor)
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
        [HttpGet]
        [Route("PreuzmiAutoraKnjige")]
        public async Task<ActionResult<string>> PreuzmiAutoraKnjige(int IdKnjige)
        {
            var x = await bibliotekaContext.Knjige.Where(p => p.ID == IdKnjige).FirstOrDefaultAsync();
            if (x != null)
            {
                return x.Autor;
            }
            return BadRequest("Ne postoji knjiga sa id: " + IdKnjige);
        }
        [HttpPut]
        [Route("IzmeniKolicinu")]
        public async Task<ActionResult> IzmeniKolicinu(int IdKnjige, int Nkolicina)
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
        [HttpGet]
        [Route("PreuzmiKolicinu")]
        public async Task<ActionResult<int>> PreuzmiKolicinu(int IdKnjige)
        {
            var x = await bibliotekaContext.Knjige.Where(p => p.ID == IdKnjige).FirstOrDefaultAsync();
            if (x != null)
            {
                return x.TrenKolicina;
            }
            return BadRequest("Ne postoji knjiga sa id: " + IdKnjige);
        }
    }
}