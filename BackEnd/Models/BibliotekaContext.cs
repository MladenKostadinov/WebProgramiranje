using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class BibliotekaContext : DbContext
    {
        public BibliotekaContext(DbContextOptions<BibliotekaContext> options): base(options)
        {

        }
    
        public DbSet<Biblioteka> Biblioteke { get; set; }

        public DbSet<Knjiga> Knjige { get; set; }

        public DbSet<Izdavac> Izdavaci { get; set; }
        
    }
}