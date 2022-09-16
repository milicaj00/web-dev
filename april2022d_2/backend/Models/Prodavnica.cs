using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Models
{
    public class Prodavnica
    {
        public Prodavnica(){
            this.Komponente = new List<Spoj>();
        }
        [Key]
        public int Id { get; set; }
        public string Naziv { get; set; }
        public List<Spoj> Komponente { get; set; }

        //bilo bi lepo da ima i listu brendova i tipova
    }
}