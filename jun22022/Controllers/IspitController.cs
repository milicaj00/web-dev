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

        [Route("dodajKompaniju/{naziv}")]
        [HttpPost]
        public async Task<ActionResult> dodajKompaniju(string naziv){
            try
            {
                Kompanija k = new Kompanija();
                k.Naziv = naziv;
                Context.Kompanije.Add(k);
                await Context.SaveChangesAsync();
                return Ok(k);
            }
            catch (System.Exception e)
            {
                
                return BadRequest(e);
            }

        }
        [Route("dodajVozilo/{naziv}/{maxTezina}/{maxZapremina}/{cenaPoDanu}/{kompId}")]
        [HttpPost]
        public async Task<ActionResult> dodajVozilo(string naziv, int maxTezina, int maxZapremina, int cenaPoDanu, int kompId){
            try
            {
                Kompanija k = await Context.Kompanije.Where(k => k.Id == kompId).FirstOrDefaultAsync();
                
                Vozilo v = new Vozilo();
                v.Naziv = naziv;
                v.maxTezina = maxTezina;
                v.maxZapremina = maxZapremina;
                v.CenaPoDanu = cenaPoDanu;
                v.ZauzetDo = DateTime.Today.AddDays(-1);
                v.ZauzetOd = DateTime.Today.AddDays(-1);
                v.Kompanija = k;
                Context.Vozila.Add(v);
                await Context.SaveChangesAsync();

                // k.Vozila.Add(v);
                // Context.Kompanije.Update(k);
                // await Context.SaveChangesAsync();

                return Ok(v);
            }
            catch (System.Exception e)
            {
                return BadRequest(e);
            }

        }

        [Route("sveKompanije")]
        [HttpGet]
        public async Task<ActionResult> sveKompanije(){
            try
            {
                var lista = await Context.Kompanije.ToListAsync();
                return Ok(lista);
            }
             catch (System.Exception e)
            {
                return BadRequest(e);
            }
        }

        [Route("vratiVozila/{zapremina}/{tezina}/{cenaOd}/{cenaDo}/{datumOd}")]
        [HttpGet]
        public async Task<ActionResult> vratiVozila(int zapremina, int tezina, int cenaOd, int cenaDo, DateTime datumOd){
            try
            {
                var vozila = await Context.Vozila
                            .Where(v => v.maxZapremina >= zapremina
                                && v.maxTezina >= tezina
                                && v.CenaPoDanu <= cenaDo
                                && v.CenaPoDanu >= cenaOd
                                && v.ZauzetDo.Date < datumOd.Date
                            )
                            .Include(v => v.Kompanija).ToListAsync();

                return Ok(vozila);
            }
             catch (System.Exception e)
            {
                return BadRequest(e);
            }
        }
        
        [Route("zauzimiVozilo/{voziloId}/{datumOd}/{datumDo}")]
        [HttpPut]
        public async Task<ActionResult> zauzimiVozilo(int voziloId, DateTime datumOd, DateTime datumDo){
            try
            {
                var vozilo = await Context.Vozila
                            .Where(v => v.Id == voziloId ).FirstOrDefaultAsync();
                
                vozilo.ZauzetOd = datumOd;
                vozilo.ZauzetDo = datumDo;
                
                double dani = (datumDo - datumOd).TotalDays;
                vozilo.ProsecnaZarada = (vozilo.ProsecnaZarada*vozilo.BrojIsporuka + dani*vozilo.CenaPoDanu)/(++vozilo.BrojIsporuka);

                await Context.SaveChangesAsync();

                return Ok(vozilo);
            }
             catch (System.Exception e)
            {
                return BadRequest(e);
            }
        }
        
    }
}
