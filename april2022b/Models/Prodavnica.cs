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
        public int BrojSendvica { get; set; }
        public int DnevnaZarada { get; set; }
        public List<Spoj> Sastojci { get; set; }
    }
}