using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Sastojak
    {
        [Key]
        public int Id { get; set; }
        public string Naziv { get; set; }
       
    }
}