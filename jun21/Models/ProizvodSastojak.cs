using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class ProizvodSastojak
    {
        [Key]
        public int Id { get; set; }

        public int Kolicina { get; set; }

[JsonIgnore]
        public Proizvod Proizvod { get; set; }

        public Sastojak Sastojak { get; set; }
    }
}