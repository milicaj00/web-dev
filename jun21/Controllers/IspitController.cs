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
    public struct fali
    {
        public int kolicina;
        public int sastojak;
        public fali(int k, int s){
            this.kolicina = k;
            this.sastojak = s;
        }
    } ;

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
                p.Frizider = new List<ProdavnicaSastojak>();
                p.Meni = new List<Proizvod>();

                Context.Prodavnice.Add(p);
                await Context.SaveChangesAsync();

                return Ok(p);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [Route("dodajProizvod/{naziv}")]
        [HttpPost]
        public async Task<ActionResult> dodajProizvod(string naziv){
            try
            {
                Proizvod p = new Proizvod();
                p.Naziv = naziv;
                p.Sastojci = new List<ProizvodSastojak>();

                Context.Proizvodi.Add(p);
                await Context.SaveChangesAsync();

                return Ok(p);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [Route("dodajSastojak/{naziv}")]
        [HttpPost]
        public async Task<ActionResult> dodajSastojak(string naziv){

            try
            {
                Sastojak s = new Sastojak();
                s.Naziv = naziv;
                Context.Sastojci.Add(s);
                await Context.SaveChangesAsync();
                return Ok(s);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [Route("dodajSastojakUProdavnicu/{idProd}/{idSas}/{kolicina}")]
        [HttpPost]
        public async Task<ActionResult> dodajSastojakUProdavnicu(int idProd, int idSas, int kolicina){

            try
            {
                //provera ako postoji da poveca
                var prod = await Context.Prodavnice.Where(p=> p.Id == idProd).FirstOrDefaultAsync();
                var sastojak = await Context.Sastojci.Where(p=> p.Id == idSas).FirstOrDefaultAsync();

                ProdavnicaSastojak ps = new ProdavnicaSastojak();
                ps.Prodavnica = prod;
                ps.Sastojak = sastojak;
                ps.Kolicina = kolicina;

                Context.ProdavnicaSastojak.Add(ps);

                prod.Frizider.Add(ps);

                await Context.SaveChangesAsync();

                return Ok(ps);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [Route("dodajSastojakUProizvod/{idProi}/{idSas}/{kolicina}")]
        [HttpPost]
        public async Task<ActionResult> dodajSastojakUProizvod(int idProi, int idSas, int kolicina){

            try
            {
                //provera ako postoji da poveca
                var proi = await Context.Proizvodi.Where(p=> p.Id == idProi).FirstOrDefaultAsync();
                var sastojak = await Context.Sastojci.Where(p=> p.Id == idSas).FirstOrDefaultAsync();

                ProizvodSastojak ps = new ProizvodSastojak();
                ps.Proizvod = proi;
                ps.Sastojak = sastojak;
                ps.Kolicina = kolicina;

                Context.ProizvodSastojak.Add(ps);

                proi.Sastojci.Add(ps);

                await Context.SaveChangesAsync();
                return Ok(ps);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [Route("sviProizvodi")]
        [HttpGet]
        public async Task<ActionResult> sviProizvodi(){

            try
            {

                var lista = await Context.Proizvodi.Include(p=>p.Sastojci).ThenInclude(s=>s.Sastojak).ToListAsync();
                
                return Ok(lista);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [Route("sviSastojci")]
        [HttpGet]
        public async Task<ActionResult> sviSastojci(){

            try
            {
                var lista = await Context.Sastojci.ToListAsync();
            
                return Ok(lista);
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
               .Include(p => p.Meni)
               .Include(p=>p.Frizider)
               .ThenInclude(s=>s.Sastojak).ToListAsync();
                
                return Ok(lista);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [Route("naruci/{idPodavnice}/{idProizvoda}/{komad}")]
        [HttpPut]
        public async Task<ActionResult> naruci(int idPodavnice, int idProizvoda, int komad){

            try
            {
               var potrebniSastojci = await Context.ProizvodSastojak
                                            .Where(p => p.Proizvod.Id == idProizvoda)
                                            .Include(p => p.Sastojak)
                                            // .Select( p => new {
                                            //     kolicina = p.Kolicina,
                                            //     sastojak = p.Sastojak.Id
                                            // })
                                            .ToListAsync();
                
                var frizider = await Context.ProdavnicaSastojak
                                            .Where(p => p.Prodavnica.Id == idPodavnice)
                                            .Include(p => p.Sastojak)
                                            // .Select( p => new {
                                            //     kolicina = p.Kolicina,
                                            //     sastojak = p.Sastojak.Id
                                            // })
                                            .ToListAsync();
//spoljna petlja ce ide po komad
//prolaz kroz frizider
//ako nadjem sastojak oduzmem
//ako je < 0 izadje iz spoljnu

    

               
                List<fali> lista = new List<fali>() ;
                int brojKomada = komad;

                
                    foreach (var fr in frizider)
                    {
                        foreach (var sas in potrebniSastojci)
                        {
                               
                            if(sas.Sastojak.Id == fr.Sastojak.Id){
                                if(fr.Kolicina - komad*sas.Kolicina <= 0)
                               {
                                    if(brojKomada > fr.Kolicina / sas.Kolicina){
                                        brojKomada = fr.Kolicina / sas.Kolicina;
                                       
                                    }
                                }
                               
                            }
                        }
                    }
                
                    foreach (var fr in frizider)
                    {
                        foreach (var sas in potrebniSastojci)
                        {
                            if(sas.Sastojak.Id == fr.Sastojak.Id){
                                fr.Kolicina -= brojKomada*sas.Kolicina;

                                if(brojKomada < komad){
                                    var l = new fali (
                                       sas.Kolicina*(komad-brojKomada)-fr.Kolicina,
                                        fr.Sastojak.Id
                                    );
                                    lista.Add(l);
                                    return Ok(l);
                                }
                            }
                        }
                    }



                // Boolean nemaNiZaJedan = true;

                // for(int i = 0; i < potrebniSastojci.Count() && nemaNiZaJedan; i++){
                //     var s = potrebniSastojci[i];

                //     int br = 0;
                //     foreach(var f in frizider){
                //         if(f.sastojak == s.sastojak){
                //             br = 1;
                //             if(f.kolicina >= s.kolicina){
                //                 if(f.kolicina >= s.kolicina*komad){
                //                     // ima za sve
                //                 }
                //                 else{
                //                     //nema za vise od jednog komada
                                    
                //                 }
                //             }else{
                //                 //nema ni za jedan
                //                 nemaNiZaJedan = false;
                //             }
                //         }
                //     }
                //     if(br != 1){ //nema tog sastojka u frizideru
                //         nemaNiZaJedan = false;
                //     }
                // }

                // if(!nemaNiZaJedan){
                //     return Ok("nema ni za jedan");
                // }


                if(brojKomada == komad)
                    return Ok("ima dovoljno");
                else{
                    return Ok(lista);
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex);
            }

        }

    }
}
