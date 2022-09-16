using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Globalization;

namespace Models
{
    public class Vozilo
    {
        [Key]
        public int Id { get; set; }
        public string Naziv { get; set; }
        public int maxTezina { get; set; }
        public int maxZapremina { get; set; }
        public DateTime ZauzetOd { get; set; }
        public DateTime ZauzetDo { get; set; }
        public int CenaPoDanu { get; set; }
        public double ProsecnaZarada { get; set; }
        public int BrojIsporuka { get; set; }
        public Kompanija Kompanija { get; set; }
      
    }
}