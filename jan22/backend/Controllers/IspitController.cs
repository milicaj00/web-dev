using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;

namespace Template.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IspitController : ControllerBase
    {
        IspitDbContext Context { get; set; }

        public IspitController(IspitDbContext context)
        {
            Context = context;
        }

        [Route("DodajProdKucu/{naziv}")]
        [HttpPost]
        public async Task<ActionResult> DodajProdKucu(string naziv){

            if(naziv == null)
                return BadRequest("NEMA NAZIV");

            ProdukcijskaKuca p = new ProdukcijskaKuca();
            p.Naziv = naziv;
            p.Filmovi = new List<Film>();
            p.ListaKategorija = new List<Kategorija>();

            try
            {
                Context.ProdukcijskaKuca.Add(p);
                await Context.SaveChangesAsync();
                return Ok(p);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }          
        }

        [Route("DodajKategoriju/{naziv}")]
        [HttpPost]
        public async Task<ActionResult> DodajKategoriju(string naziv){

            if(naziv == null)
                return BadRequest("NEMA NAZIV");

            Kategorija p = new Kategorija();
            p.Naziv = naziv;

            try
            {
                Context.Kategorije.Add(p);
                await Context.SaveChangesAsync();
                return Ok(p);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }          
        }
    

        [Route("DodajFilm/{nazivFilma}/{kategorijaId}/{idKuce}")]
        [HttpPost]
        public async Task<ActionResult> DodajFilm(string nazivFilma, int kategorijaId, int idKuce){

            if(String.IsNullOrWhiteSpace(nazivFilma))
                return BadRequest("Nevalidno ime");
                //provere

            Film f = new Film();
            f.Naziv = nazivFilma;
            f.KategorijaId = kategorijaId;
            f.ProsecnaOcena = 0;
            f.BrojOcena = 0;

            var prodKuca = Context.ProdukcijskaKuca
                        .Where( p => p.Id == idKuce)
                        .Include(p => p.ListaKategorija)
                        .Include(p => p.Filmovi)
                        .FirstOrDefault();

            if(prodKuca == null){
                return BadRequest("ne postoji kuca");
            }

            Kategorija k = Context.Kategorije.Where( p => p.Id == kategorijaId).FirstOrDefault();
            if(k == null){
                return BadRequest("ne postoji kategorija");
            }


            if(!prodKuca.ListaKategorija.Contains(k)){
                prodKuca.ListaKategorija.Add(k);
            }

           
          //  f.idProdKuce = prodKuca.Id;


            try{
                Context.Filmovi.Add(f);
                prodKuca.Filmovi.Add(f);
                await Context.SaveChangesAsync();
                return Ok(f);
            }
            catch(Exception e){
                return BadRequest(e.Message);
            }
        }

        [Route("VratiSveKuce")]
        [HttpGet]
        public async Task<ActionResult> VratiSveKuce(){
            var kuce = await Context.ProdukcijskaKuca.Include(p => p.ListaKategorija).ToListAsync();

            return Ok(kuce);
        }

        [Route("VratiFilmove/{idKuce}/{idKategorije}")]
        [HttpGet]
        public async Task<ActionResult> VratiFilmove(int idKuce, int idKategorije){
            
            var filmovi = await Context.ProdukcijskaKuca
                .Where(k => k.Id == idKuce)
                .Include( k => k.Filmovi)
                .Select(k => k.Filmovi)
                .FirstOrDefaultAsync();
            
            List<Film> films = new List<Film>();
            foreach (var film in filmovi)
            {
                if(film.KategorijaId == idKategorije)
                    films.Add(film);
            }

            return Ok(films);
        }

        [Route("OceniFilm/{idFilma}/{ocena}")]
        [HttpPut]
        public async Task<ActionResult> OceniFilm(int idFilma, double ocena){

            var film = await Context.Filmovi.Where(f => f.Id == idFilma).FirstOrDefaultAsync();

            if(film == null){
                return BadRequest("Ne postoji film");
            }

            film.ProsecnaOcena = (film.ProsecnaOcena*film.BrojOcena+ocena)/(film.BrojOcena+1);
            film.BrojOcena++;

            await Context.SaveChangesAsync();

            return Ok(film);

        }

    }

}
