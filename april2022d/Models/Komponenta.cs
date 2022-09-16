using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Models
{
    public class Komponenta{

        [Key]
        public int Id { get; set; }

        public int BrendId { get; set; }

        public int TipId { get; set; }

        public string Naziv { get; set; }

        // [JsonIgnore]
       // public List<Spoj> Prodavnice { get; set; }
        
    }
    
}