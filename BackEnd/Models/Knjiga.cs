using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models
{
    public class Knjiga
    {

        [Key]
        public int ID { get; set; }

        [Required]
        public int M { get; set; }

        [Required]
        public int N { get; set; }

        [MaxLength(50)]
        [Required]
        public string Naslov { get; set; }

        [MaxLength(50)]
        public string Autor { get; set; }

        public int TrenKolicina { get; set; }

        [MaxLength(1000)]
        public string Opis { get; set; }

        public Izdavac Izdavac { get; set; }

        public byte[] Slika { get; set; }

        [JsonIgnore]
        public Biblioteka BibliotekaOveKnjige { get; set; }
    }
}