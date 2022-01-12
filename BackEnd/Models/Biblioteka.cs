using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Biblioteka
    {

        [Key]
        public int ID { get; set; }

        [Required]
        public int M { get; set; }

        [Required]
        public int N { get; set; }

        [MaxLength(50)]
        [Required]
        public string Ime { get; set; }

        [Required]
        public int MaxKolicina { get; set; }

        public List<Knjiga> Knjige { get; set; }
    }
}