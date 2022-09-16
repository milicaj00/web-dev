using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Models
{
    public class Spoj
    {
        [Key]
        public int Id { get; set; }
        [JsonIgnore]
        public Prodavnica Prodavnica { get; set; }
        public Sastojak Sastojak { get; set; }
        public int Kolicina { get; set; }
        public int Cena { get; set; }
    }
}