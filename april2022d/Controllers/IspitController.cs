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

        [Route("sveProdavnice")]
        [HttpGet]
        public async Task<ActionResult> SveProdavnice(){
            try
            {
                var prodavnice = await Context.Prodavnice.ToListAsync();
                return Ok(prodavnice);
            }
            catch (System.Exception)
            {
                return BadRequest("Greska");
            }
        }
        [Route("sviTipovi")]
        [HttpGet]
        public async Task<ActionResult> sviTipovi(){
            try
            {
                var prodavnice = await Context.Tip.ToListAsync();
                return Ok(prodavnice);
            }
            catch (System.Exception)
            {
                return BadRequest("Greska");
            }
        }
        [Route("sviBrendovi")]
        [HttpGet]
        public async Task<ActionResult> sviBrendovi(){
            try
            {
                var prodavnice = await Context.Brend.ToListAsync();
                return Ok(prodavnice);
            }
            catch (System.Exception)
            {
                return BadRequest("Greska");
            }
        }
        [Route("novaProdavnica/{naziv}")]
        [HttpPost]
        public async Task<ActionResult> NovaProdavnica(string naziv){
            try
            {
                Prodavnica prodavnica = new Prodavnica();
                prodavnica.Naziv = naziv;
                prodavnica.Komponente = new List<Spoj> ();
                Context.Prodavnice.Add(prodavnica);
                await Context.SaveChangesAsync();

                return Ok(prodavnica);
            }
            catch (System.Exception)
            {
                return BadRequest("Greska");
            }
        }

        [Route("noviBrend/{naziv}")]
        [HttpPost]
        public async Task<ActionResult> noviBrend(string naziv){
            try
            {
                Brend b = new Brend();
                b.Naziv = naziv;
               
                Context.Brend.Add(b);
                await Context.SaveChangesAsync();

                return Ok(b);
            }
            catch (System.Exception)
            {
                return BadRequest("Greska");
            }
        }
        [Route("noviTip/{naziv}")]
        [HttpPost]
        public async Task<ActionResult> noviTip(string naziv){
            try
            {
                Tip prodavnica = new Tip();
                prodavnica.Naziv = naziv;
                
                Context.Tip.Add(prodavnica);
                await Context.SaveChangesAsync();

                return Ok(prodavnica);
            }
            catch (System.Exception)
            {
                return BadRequest("Greska");
            }
        }

        [Route("novaKomponenta/{tip}/{brend}")]
        [HttpPost]
        public async Task<ActionResult> novaKomponenta (int tip, int brend){
        
            try
            {
                //provere
                Komponenta k = new Komponenta();
                k.Brend = brend;
                k.Tip = tip;
                Context.Komponente.Add(k);
                await Context.SaveChangesAsync();
                return Ok(k);
            }
            catch (System.Exception e)
            {
                
                return BadRequest(e);
            }

        }

        [Route("brisiKomponentu/{id}")]
        [HttpDelete]
        public async Task<ActionResult> brisiKomponentu (int id){
        
            try
            {
                var k = await Context.Komponente.Where(p => p.Id == id).FirstOrDefaultAsync();
                Context.Komponente.Remove(k);
                await Context.SaveChangesAsync();
                return Ok(k);
            }
            catch (System.Exception e)
            {
                
                return BadRequest(e);
            }

        }

        [Route("dodajKomponentuProdavnici/{idProd}/{idKomp}/{kolicina}/{cena}")]
        [HttpPost]
         public async Task<ActionResult> dodajKomponentuProdavnici (int idProd, int idKomp, int kolicina, double cena){
        
            try
            {
                //provere
                Prodavnica p = await Context.Prodavnice.Where(p => p.Id == idProd).FirstOrDefaultAsync();
                Komponenta komp = await Context.Komponente.Where(p => p.Id == idKomp).FirstOrDefaultAsync();


                Spoj k = new Spoj();
                k.Prodavnica = idProd;
                k.Kolicina = kolicina;
                k.Komponenta = komp;
                k.Cena = cena;

                // ovde treba provera da li vec postoji mada nije bitno jer ne trazi u zadatak to


                Context.Spojevi.Add(k);

                if(p.Komponente == null)
                    p.Komponente = new List<Spoj>();

                p.Komponente.Add(k);
              
                await Context.SaveChangesAsync();
                return Ok(k);
            }
            catch (System.Exception e)
            {
                
                return BadRequest(e);
            }
        }

        [Route("sveKomponente")]
        [HttpGet]
        public async Task<ActionResult> sveKomponente(){
            try
            {
                var prodavnice = await Context.Komponente.ToListAsync();
                return Ok(prodavnice);
            }
            catch (System.Exception)
            {
                return BadRequest("Greska");
            }
        }

        //pretraga

        [Route("nadjiKomponentu/{prodId}/{tipId}")]
        [HttpGet]
        public async Task<ActionResult> nadjiKomponentu(int prodId, int tipId, [FromQuery(Name = "brend")]  int brend, [FromQuery(Name = "cenaOd")]  double cenaOd, [FromQuery(Name = "cenaDo")]  double cenaDo){
            try
            {

                if(brend > 0){
                      if(cenaDo > 0 && cenaOd > 0){ 
                              var sp = await Context.Spojevi
                                .Where(s => s.Prodavnica == prodId && s.Cena < cenaDo && s.Cena > cenaOd)
                                .Include(s => s.Komponenta)
                                .Where(s => s.Komponenta.Tip == tipId && s.Komponenta.Brend == brend)
                                .Select(s => new {
                                cena = s.Cena,
                                kolicina = s.Kolicina,
                                id = s.Id,
                                idKomp = s.Komponenta.Id,
                                tip = s.Komponenta.Tip,
                                brend = s.Komponenta.Brend
                            })
                                .ToListAsync();
                            return Ok(sp);
                        } 

                        if(cenaDo > 0 && cenaOd <= 0){ //nema cena od
                              var sp = await Context.Spojevi
                                .Where(s => s.Prodavnica == prodId && s.Cena < cenaDo )
                                .Include(s => s.Komponenta)
                                .Where(s => s.Komponenta.Tip == tipId && s.Komponenta.Brend == brend)
                                .Select(s => new {
                                cena = s.Cena,
                                kolicina = s.Kolicina,
                                id = s.Id,
                                idKomp = s.Komponenta.Id,
                                tip = s.Komponenta.Tip,
                                brend = s.Komponenta.Brend
                            })
                                .ToListAsync();
                            return Ok(sp);
                        } 

                        if(cenaDo <= 0 && cenaOd > 0){ //nema cena do
                              var sp = await Context.Spojevi
                                .Where(s => s.Prodavnica == prodId && s.Cena > cenaOd )
                                .Include(s => s.Komponenta)
                                .Where(s => s.Komponenta.Tip == tipId && s.Komponenta.Brend == brend)
                                .Select(s => new {
                                cena = s.Cena,
                                kolicina = s.Kolicina,
                                id = s.Id,
                                idKomp = s.Komponenta.Id,
                                tip = s.Komponenta.Tip,
                                brend = s.Komponenta.Brend
                            })
                                .ToListAsync();
                            return Ok(sp);
                        } 
                        //ima samo brend
                        var spo = await Context.Spojevi
                                .Where(s => s.Prodavnica == prodId  )
                                .Include(s => s.Komponenta)
                                .Where(s => s.Komponenta.Tip == tipId && s.Komponenta.Brend == brend)
                                .Select(s => new {
                                cena = s.Cena,
                                kolicina = s.Kolicina,
                                id = s.Id,
                                idKomp = s.Komponenta.Id,
                                tip = s.Komponenta.Tip,
                                brend = s.Komponenta.Brend
                            })
                                .ToListAsync();
                        return Ok(spo);
                    }
                    else // nema brend
                    {
                        if(cenaDo > 0 && cenaOd > 0){ 
                              var sp = await Context.Spojevi
                                .Where(s => s.Prodavnica == prodId && s.Cena < cenaDo && s.Cena > cenaOd)
                                .Include(s => s.Komponenta)
                                .Where(s => s.Komponenta.Tip == tipId )
                                .Select(s => new {
                                cena = s.Cena,
                                kolicina = s.Kolicina,
                                id = s.Id,
                                idKomp = s.Komponenta.Id,
                                tip = s.Komponenta.Tip,
                                brend = s.Komponenta.Brend
                            })
                                .ToListAsync();
                            return Ok(sp);
                        } 

                        if(cenaDo > 0 && cenaOd <= 0){ //nema cena od
                              var sp = await Context.Spojevi
                                .Where(s => s.Prodavnica == prodId && s.Cena < cenaDo )
                                .Include(s => s.Komponenta)
                                .Where(s => s.Komponenta.Tip == tipId)
                                .Select(s => new {
                                cena = s.Cena,
                                kolicina = s.Kolicina,
                                id = s.Id,
                                idKomp = s.Komponenta.Id,
                                tip = s.Komponenta.Tip,
                                brend = s.Komponenta.Brend
                            })
                                .ToListAsync();
                            return Ok(sp);
                        } 

                        if(cenaDo <= 0 && cenaOd > 0){ //nema cena do
                              var sp = await Context.Spojevi
                                .Where(s => s.Prodavnica == prodId && s.Cena > cenaOd )
                                .Include(s => s.Komponenta)
                                .Where(s => s.Komponenta.Tip == tipId )
                                .Select(s => new {
                                cena = s.Cena,
                                kolicina = s.Kolicina,
                                id = s.Id,
                                idKomp = s.Komponenta.Id,
                                tip = s.Komponenta.Tip,
                                brend = s.Komponenta.Brend
                            })
                                .ToListAsync();
                            return Ok(sp);
                        } 
                    }

//inace ima samo tip
               var spoj = await Context.Spojevi
                            .Where(s => s.Prodavnica == prodId)
                            .Include(s => s.Komponenta)
                            .Where(s => s.Komponenta.Tip == tipId)
                            .Select(s => new {
                                cena = s.Cena,
                                kolicina = s.Kolicina,
                                id = s.Id,
                                idKomp = s.Komponenta.Id,
                                tip = s.Komponenta.Tip,
                                brend = s.Komponenta.Brend
                            })
                            .ToListAsync();

                //var k = await Context.Komponente.Where().ToListAsync();
                return Ok(spoj);
            }
            catch (System.Exception)
            {
                return BadRequest("Greska");
            }
        }

        [Route("kupiKomponentu/{prodId}/{kompId}/{kolicina}")]
        [HttpPut]
        public async Task<ActionResult> nadjiKomponentu(int prodId, int kompId, int kolicina){
            try
            {
 
                var spoj = await Context.Spojevi
                            .Where(s => s.Prodavnica == prodId && s.Komponenta.Id == kompId)
                            .Include(s=>s.Komponenta)
                            .FirstOrDefaultAsync();

                if(spoj == null){
                    return BadRequest("Nema spoja");
                }

                if(spoj.Kolicina <= 0 || spoj.Kolicina-kolicina <= 0){
                    return BadRequest("Nema dovoljno");
                }
                spoj.Kolicina-=kolicina;
                await Context.SaveChangesAsync();
                return Ok(spoj);
            }
            catch (System.Exception)
            {
                return BadRequest("Greska");
            }
        }

    }
}
