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
    public DbSet<RecenzijaModel> Recenzije { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Korisnik>()
            .Property(k => k.Id)
            .ValueGeneratedOnAdd();
    
        modelBuilder.Entity<UslugaPeglanja>()
            .Property(u => u.Cijena)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Narudzba>()
    .Property(n => n.UkupnaCijena)
    .HasPrecision(18, 2);

modelBuilder.Entity<Narudzba>()
        .HasOne(n => n.Recenzija)
        .WithOne( )
        .HasForeignKey<RecenzijaModel>(r => r.NarudzbaId);

        modelBuilder.Entity<StavkaNarudzbe>()
            .Property(s => s.CijenaUTrenutkuNarudzbe)
            .HasPrecision(18, 2);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Provjeri server name! Ako je VALENTINAANUSIC\SQLEXPRESS01, koristi ga.
        // Ovdje sam ostavila tvoju konfiguraciju s retry mehanizmom.
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=VALENTINAANUSIC\\SQLEXPRESS01;Database=IroningServiceDb;Integrated Security=True;TrustServerCertificate=True;", 
                sqlOptions => sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5, 
                    maxRetryDelay: TimeSpan.FromSeconds(30), 
                    errorNumbersToAdd: null)
            );
        }
    }
}