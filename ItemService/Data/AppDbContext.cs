using ItemService.Models;
using Microsoft.EntityFrameworkCore;

namespace ItemService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }

        public DbSet<Restaurante> Restaurantes { get; set; }
        public DbSet<Item> Itens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Restaurante>()
                .HasMany(c => c.Itens)
                .WithOne(a => a.Restaurante!)
                .HasForeignKey(a => a.IdRestaurante);

            modelBuilder
                .Entity<Item>()
                .HasOne(a => a.Restaurante)
                .WithMany(c => c.Itens)
                .HasForeignKey(a => a.IdRestaurante);
        }
    }
}