using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Microsoft.EntityFrameworkCore;


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

        [Route("novaProdavnica/{naziv}")]
        [HttpPost]
        public async Task<ActionResult> novaProdavnica(string naziv){
            try
            {
                 if(String.IsNullOrWhiteSpace(naziv)){
                    return StatusCode(400,"losi podaci");
                }

                Prodavnica p = new Prodavnica();
                p.Naziv = naziv;
                p.Brendovi = new List<Brend>();
                p.Artikli = new List<Spoj>();
                Context.Prodavnice.Add(p);
                await Context.SaveChangesAsync();
                return Ok(p);
            }
            catch (System.Exception ex)
            {
                
                return BadRequest(ex);
            }
        }
        [Route("dodajBrend/{naziv}")]
        [HttpPost]
        public async Task<ActionResult> dodajBrend(string naziv){
            try
            {
                if(String.IsNullOrWhiteSpace(naziv)){
                    return StatusCode(400,"losi podaci");
                }
                Brend p = new Brend();
                p.Naziv = naziv;
                Context.Brendovi.Add(p);
                await Context.SaveChangesAsync();
                return Ok(p);
            }
            catch (System.Exception ex)
            {
                
                return BadRequest(ex);
            }
        }
        [Route("dodajArtikal/{naziv}/{brendId}/{prodId}/{s}/{m}/{l}/{cena}")]
        [HttpPost]
        public async Task<ActionResult> dodajArtikal(string naziv, int brendId, int prodId, int s, int m, int l, int cena){
            try
            {
                if(String.IsNullOrWhiteSpace(naziv) || brendId <= 0){
                    return StatusCode(400,"losi podaci");
                }
                Brend b = await Context.Brendovi.Where( p=> p.Id == brendId).FirstOrDefaultAsync();
                if(b == null){
                    return StatusCode(404,"Brend ne postoji");
                }
                Prodavnica prod = await Context.Prodavnice
                                    .Where( p=> p.Id == prodId)
                                    .Include(p=>p.Brendovi)
                                    .FirstOrDefaultAsync();
                if(prod == null){
                    return StatusCode(404,"Prodavnica ne postoji");
                }

                Artikal p = new Artikal();
                p.Naziv = naziv;
                p.BrendId = brendId;
                Context.Artikli.Add(p);

                if(!prod.Brendovi.Contains(b)){
                    prod.Brendovi.Add(b);
                }

                Spoj spoj = new Spoj();
                spoj.Artikal = p;
                spoj.Prodavnica = prod;
                spoj.S = s;
                spoj.M = m;
                spoj.L = l;
                spoj.Cena = cena;
                Context.ProdavnicaArtikal.Add(spoj);

                await Context.SaveChangesAsync();
                return Ok(spoj);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [Route("sveProdavnice")]
        [HttpGet]
        public async Task<ActionResult> sveProdavnice(){
            try
            {
                var lista = await Context.Prodavnice
                            .Include(p => p.Brendovi)
                         //   .Include( p => p.Artikli)
                            .ToListAsync();
                return Ok(lista);
            }
            catch (System.Exception e)
            {
                
                return BadRequest(e);
            }
        }

        [Route("sviBrendovi")]
        [HttpGet]
        public async Task<ActionResult> sviBrendovi(){
            try
            {
                var lista = await Context.Brendovi
                            .ToListAsync();
                return Ok(lista);
            }
            catch (System.Exception e)
            {
                
                return BadRequest(e);
            }
        }

        [Route("nadjiArtikle/{prodId}/{brendId}")]
        [HttpGet]
        public async Task<ActionResult> nadjiArtikle(int prodId, int brendId, int cenaOd, int cenaDo){
            try
            {
                Prodavnica p = await Context.Prodavnice
                                            .Where(p => p.Id == prodId)
                                            .Include(p => p.Brendovi)
                                            .FirstOrDefaultAsync();
                if(p == null){
                    return StatusCode(404,"Prodavnica ne postoji");
                }

                Brend b = await Context.Brendovi.Where(b => b.Id == brendId).FirstOrDefaultAsync();
                if(b == null){
                    return StatusCode(404,"Brend ne postoji");
                }

                if(!p.Brendovi.Contains(b)){
                    return StatusCode(404,"Prodavnica ne sadrzi taj brend");
                }

                int pom = cenaDo;
                if(cenaDo <= 0){
                    pom = 40000000;
                }

                var listaArtikala = await Context.ProdavnicaArtikal
                                    .Where(s => s.Prodavnica == p 
                                            && s.Artikal.BrendId == brendId 
                                            && s.Cena >= cenaOd
                                            && s.Cena <= pom)
                                    .Include( s => s.Artikal)
                                    .ToListAsync();

                return Ok(listaArtikala);

            }
            catch (System.Exception e)
            {
                
                return BadRequest(e);
            }
        }

        [Route("kupi/{spojId}/{velicina}")]
        [HttpPut]
        public async Task<ActionResult> kupi(int spojId, char velicina, int p){
            try
            {
                 Spoj s = await Context.ProdavnicaArtikal
                                            .Where(p => p.Id == spojId)
                                            .FirstOrDefaultAsync();
                if(s == null){
                    return StatusCode(404,"Spoj ne postoji");
                }
                switch (velicina)
                {
                    case 's':{
                        if(s.S != 0)
                            s.S--;
                        else{
                             return BadRequest("nema na stanju");
                        }
                        break;
                    }
                    case 'm':
                    {  
                        if(s.M != 0)
                            s.M--;
                        else{
                             return BadRequest("nema na stanju");
                        }
                        break;
                    }
                    case 'l':
                    { 
                        if(s.L != 0)
                            s.L--;
                        else{
                            return BadRequest("nema na stanju");
                        }
                        break;
                    }
                    default:{
                        return BadRequest("losa velicina");
                    }
                }
                await Context.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception e)
            {
                
                return BadRequest(e);
            }
        }

    }
}
