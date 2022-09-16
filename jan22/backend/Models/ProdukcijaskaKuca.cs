using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Models{

    public class ProdukcijskaKuca {
    
        [Key]
        public int Id { get; set; }

        public string Naziv { get; set; }

        [JsonIgnore]
        public List<Film> Filmovi { get; set; }

        public List<Kategorija> ListaKategorija { get; set; }

    }
}