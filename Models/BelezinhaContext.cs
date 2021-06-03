using Microsoft.EntityFrameworkCore;

namespace BelezinhaFut.Models{
    public class BelezinhaContext : DbContext{
        public BelezinhaContext(DbContextOptions<BelezinhaContext> options)
            : base(options){

            }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Time>()
            .HasOne<Treinador>(s => s.NomeTreinador)
            .WithOne(ad => ad.Time)
            .HasForeignKey<Treinador>(ad => ad.Id);
    }
        public DbSet<Time> Times {get; set;}
        public DbSet<Treinador> Treinadores {get; set;}
    }
}