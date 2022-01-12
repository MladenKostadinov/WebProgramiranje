using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models
{
    public class Izdavac
    {

        [Key]
        public int ID { get; set; }

        [MaxLength(50)]
        [Required]
        public string Naziv { get; set; }

        [MaxLength(50)]
        public string Adresa { get; set; }

        [MaxLength(2000)]
        public string Deskripcija { get; set; }

        [JsonIgnore]
        public List<Knjiga> Knjige { get; set; }
    }
}