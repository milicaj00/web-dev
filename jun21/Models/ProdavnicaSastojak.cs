using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class ProdavnicaSastojak
    {
        [Key]
        public int Id { get; set; }

        public int Kolicina { get; set; }

[JsonIgnore]
        public Prodavnica Prodavnica { get; set; }

        public Sastojak Sastojak { get; set; }
    }
}