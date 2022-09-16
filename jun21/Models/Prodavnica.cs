using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Prodavnica
    {
        [Key]
        public int Id { get; set; }
        public string Naziv { get; set; }
        // [JsonIgnore]
        public List<ProdavnicaSastojak> Frizider { get; set; }
        public List<Proizvod> Meni { get; set; }
    }
}