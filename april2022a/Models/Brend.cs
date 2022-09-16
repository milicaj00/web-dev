using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Models
{
    public class Brend
    {
        [Key]
        public int Id { get; set; }
        public string Naziv { get; set; }
    }
}