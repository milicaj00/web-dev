using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Models
{
    public class Spoj{

        [Key]
        public int Id { get; set; }
        public double Cena {get; set; }
        public int Kolicina { get; set; }
        public int ProdavnicaId { get; set; }
        public Komponenta Komponenta { get; set; }
        public string Sifra { get; set; }

    }
}