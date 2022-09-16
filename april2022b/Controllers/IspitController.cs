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

        [Route("dodajProdavnicu/{naziv}/{brojSendvica}")]
        [HttpPost]
        public async Task<ActionResult> dodajProdavnicu(string naziv, int brojSendvica){
            try
            {
                Prodavnica p = new Prodavnica();
                p.Naziv = naziv;
                p.BrojSendvica = brojSendvica;
                p.Sastojci = new List<Spoj>();
                p.DnevnaZarada = 0;
                Context.Prodavnice.Add(p);
                await Context.SaveChangesAsync();
                return Ok(p);
            }
            catch (System.Exception e)
            {
                return BadRequest(e);
                throw;
            }
        }

        [Route("dodajSastojak/{idProd}/{naziv}/{kolicina}/{cena}")]
        [HttpPost]
        public async Task<ActionResult> dodajSastojak(int idProd, string naziv, int kolicina, int cena){
            try
            {
                Prodavnica p = await Context.Prodavnice.Where( p => p.Id == idProd).FirstOrDefaultAsync();
                
                Sastojak s = new Sastojak();
                s.Naziv = naziv;
                Context.Sastojci.Add(s);

                Spoj spoj = new Spoj();
                spoj.Prodavnica = p;
                spoj.Sastojak = s;
                spoj.Kolicina = kolicina;
                spoj.Cena = cena;
                Context.Spoj.Add(spoj);

                await Context.SaveChangesAsync();
                return Ok(new {spoj = spoj, sastojak = s});
            }
            catch (System.Exception e)
            {
                return BadRequest(e);
                throw;
            }
        }

        [Route("vratiProdavnice")]
        [HttpGet]
        public async Task<ActionResult> vratiProdavnice(){
            try
            {
                var lista = await Context.Prodavnice
                                .Include( p => p.Sastojci)
                                .ThenInclude( s => s.Sastojak)
                                .ToListAsync();

                return Ok(lista);
            }
            catch (System.Exception e)
            {
                return BadRequest(e);
                throw;
            }
        }

        [Route("kupiSendvic/{idProd}")]
        [HttpPut]
        public async Task<ActionResult> kupiSendvic(int idProd, [FromBody] Spoj[] lista){
            try
            {
                //return Ok(lista);
                Prodavnica p = await Context.Prodavnice.Where( p => p.Id == idProd).FirstOrDefaultAsync();

                int zarada = 0;
                foreach (var item in lista)
                {
                    Spoj spoj = await Context.Spoj.Where( p => p.Id == item.Id).Include(p=>p.Prodavnica).FirstOrDefaultAsync();
                    if(spoj.Prodavnica.Id != p.Id){
                         return BadRequest("los spoj");
                    }
                    spoj.Kolicina -= item.Kolicina;
                    zarada += spoj.Cena*item.Kolicina;
                }

                p.DnevnaZarada += zarada;
                await Context.SaveChangesAsync();

                return Ok(p);
            }
            catch (System.Exception e)
            {
                return BadRequest(e);
                throw;
            }
        }

    }
}
