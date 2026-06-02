using IroningService.Domena.Entiteti;
using Microsoft.EntityFrameworkCore;

namespace IroningService.Repozitorij.Data;

public class RepozitorijContext : DbContext
{
    public RepozitorijContext(DbContextOptions<RepozitorijContext> options) : base(options) { }
    public DbSet<UslugaPeglanja> Usluge { get; set; }
    public DbSet<Narudzba> Narudzbe { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}