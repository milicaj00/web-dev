using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Models
{
    public class Prodavnica
    {
        [Key]
        public int Id { get; set; }
        public string Naziv { get; set; }
        public List<Brend> Brendovi { get; set; }
        public List<Spoj> Artikli { get; set; }
    }
}