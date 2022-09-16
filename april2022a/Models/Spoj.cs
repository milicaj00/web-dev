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
        public Artikal Artikal { get; set; }
        [JsonIgnore]
        public Prodavnica Prodavnica { get; set; }
        public int S { get; set; }
        public int M { get; set; }
        public int L { get; set; }
        public int Cena { get; set; }
    }
}