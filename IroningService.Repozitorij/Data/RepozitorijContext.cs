using IroningService.Domena.Entiteti;
using Microsoft.EntityFrameworkCore;

namespace IroningService.Repozitorij.Data;

public class RepozitorijContext : DbContext
{
    public RepozitorijContext(DbContextOptions<RepozitorijContext> options) : base(options) { }
    public DbSet<Korisnik> Korisnici { get; set; }
    public DbSet<UslugaPeglanja> Usluge { get; set; }
    public DbSet<Narudzba> Narudzbe { get; set; }
    public DbSet<StavkaNarudzbe> StavkeNarudzbe { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
         modelBuilder.Entity<UslugaPeglanja>()
            .Property(u => u.Cijena)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Narudzba>()
            .Property(n => n.UkupnaCijena)
            .HasPrecision(18, 2);

        modelBuilder.Entity<StavkaNarudzbe>()
            .Property(s => s.CijenaUTrenutkuNarudzbe)
            .HasPrecision(18, 2);
    }
}