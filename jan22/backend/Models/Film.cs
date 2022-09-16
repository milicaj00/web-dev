using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Models{

    public class Film {
    
        [Key]
        public int Id { get; set; }

        public string Naziv { get; set; }

        public int KategorijaId { get; set; }

        [Range(1,10)]
        public double ProsecnaOcena { get; set; }

        public int BrojOcena { get; set; }

      //  public int idProdKuce { get; set; }

    }
}