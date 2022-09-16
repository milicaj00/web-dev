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

        [Route("dodajProdavnicu/{naziv}")]
        [HttpPost]
        public async Task<ActionResult> dodajProdavnicu(string naziv){
            try
            {
                Prodavnica p = new Prodavnica();
                p.Naziv = naziv;
                Context.Prodavnice.Add(p);
                await Context.SaveChangesAsync();
                return Ok(p);
            }
            catch(System.Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("dodajKomponentu/{naziv}/{brend}/{tip}")]
        [HttpPost]
        public async Task<ActionResult> dodajKomponentu(string naziv, string brend, string tip){

            try
            {
                // Brend b = new Brend();
                // b.Naziv = brend;
                // Context.Brendovi.Add(b);

                // Tip t = new Tip();
                // t.Naziv = tip;
                // Context.Tipovi.Add(t);

                Brend b = await Context.Brendovi.Where(b => b.Naziv == brend).FirstOrDefaultAsync();
                if(b == null){
                    b = new Brend();
                    b.Naziv = brend;
                    Context.Brendovi.Add(b);
                }
                Tip t = await Context.Tipovi.Where(b => b.Naziv == tip).FirstOrDefaultAsync();
                if(t == null){
                    t = new Tip();
                    t.Naziv = tip;
                    Context.Tipovi.Add(t);
                }

                await Context.SaveChangesAsync();

                Komponenta k = new Komponenta();
                k.Naziv = naziv;
                k.BrendId = b.Id;
                k.TipId = t.Id;
                Context.Komponente.Add(k);

                await Context.SaveChangesAsync();
                return Ok(k);
            }
            catch(System.Exception ex)
            {
                return BadRequest(ex);
            }

        }
        [Route("dodajKomponentuUProdavnicu/{kompId}/{prodId}/{sifra}/{kolicina}/{cena}")]
        [HttpPost]
        public async Task<ActionResult> dodajKomponentuUProdavnicu(int kompId, int prodId, string sifra, int kolicina, double cena){

            try
            {
                Komponenta k = await Context.Komponente.Where( k => k.Id == kompId).FirstOrDefaultAsync();

                Spoj s = new Spoj();
                s.ProdavnicaId = prodId;
                s.Komponenta = k;
                s.Cena = cena;
                s.Kolicina = kolicina;
                s.Sifra = sifra;

                Context.Spoj.Add(s);

                await Context.SaveChangesAsync();
                return Ok(s);
            }
            catch(System.Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [Route("sveProdavnice")]
        [HttpGet]
        public async Task<ActionResult> sveProdavnice(){
            try
            {
                var prodavnice = await Context.Prodavnice.ToListAsync();
                var brendovi = await Context.Brendovi.ToListAsync();
                var tipovi = await Context.Tipovi.ToListAsync();
                return Ok(new {
                    prodavnice = prodavnice,
                    brendovi = brendovi,
                    tipovi = tipovi
                });
            }
            catch (System.Exception ex)
            {
                
                return BadRequest(ex);
            }
        }
        [Route("nadjiKomponente/{prodId}/{tipId}")]
        [HttpGet]
        public async Task<ActionResult> nadjiKomponente(int prodId, int tipId, int brendId, double cenaOd, double cenaDo){
            try
            {
                if(cenaDo <= 0){
                    cenaDo = Int32.MaxValue;
                }

                
                if(brendId > 0)
                {
                    var lista = await Context.Spoj
                                .Where(s => s.ProdavnicaId == prodId
                                && s.Komponenta.BrendId == brendId
                                && s.Komponenta.TipId == tipId
                                && s.Cena > cenaOd
                                && s.Cena < cenaDo
                                && s.Kolicina > 0
                                )
                                .Include(p => p.Komponenta)
                                .ToListAsync();
                    return Ok(lista);
                }
                
                var list = await Context.Spoj
                            .Where(s => s.ProdavnicaId == prodId
                            && s.Komponenta.TipId == tipId
                            && s.Cena > cenaOd
                            && s.Cena < cenaDo
                            && s.Kolicina > 0
                            )
                            .Include(p => p.Komponenta)
                            .ToListAsync();
               
                return Ok(list);
            }
            catch (System.Exception ex)
            {
                
                return BadRequest(ex);
            }
        }

        [Route("kupiKomponente/{prodId}")]
        [HttpPut]
        public async Task<ActionResult> kupiKomponente(int prodId, List<Spoj> listaKomp){
            try
            {
               
                foreach (var item in listaKomp)
                {
                    Spoj spoj = await Context.Spoj
                    .Where(s => s.Id == item.Id
                            && s.Kolicina-item.Kolicina >= 0
                    ).FirstOrDefaultAsync();

                    if(spoj == null){
                        return StatusCode(422,"Nema dovoljno");
                    }
                    spoj.Kolicina -= item.Kolicina;
                    Context.Spoj.Update(spoj);
                }

                await Context.SaveChangesAsync();

                return Ok("uspesno kupljeno");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
