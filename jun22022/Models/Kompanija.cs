using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Models
{
    public class Kompanija
    {
        // public Kompanija(){
        //     Vozila = new List<Vozilo>();
        // }
        [Key]
        public int Id { get; set; }
        public string Naziv { get; set; }
        // [JsonIgnore]
        // public List<Vozilo> Vozila { get; set; }

    }
}