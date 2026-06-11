using IroningService.Repozitorij.Data;
using IroningService.Repozitorij.Repozitoriji;
using IroningService.Repozitorij.Suclja;
using IroningService.Servis.Implementacije;
using IroningService.Servis.Suclja;
using Microsoft.EntityFrameworkCore;
using IroningService.Blazor.Components;

var builder = WebApplication.CreateBuilder(args);

// 1. POVEZIVANJE BAZE
var connString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<RepozitorijContext>(options =>
    options.UseSqlServer(connString));

// 2. REGISTRACIJA SERVISA I REPOZITORIJA
builder.Services.AddScoped<IUslugaRepozitorij, UslugaRepozitorij>();
builder.Services.AddScoped<INarudzbaRepozitorij, NarudzbaRepozitorij>();
builder.Services.AddScoped<IKorisnikRepozitorij, KorisnikRepozitorij>();
builder.Services.AddScoped<IRecenzijaRepozitorij, RecenzijaRepozitorij>();
builder.Services.AddScoped<IUslugaServis, UslugaServis>();
builder.Services.AddScoped<INarudzbaServis, NarudzbaServis>();
builder.Services.AddScoped<IKorisnikServis, KorisnikServis>();
builder.Services.AddScoped<IRecenzijaServis, RecenzijaServis>();
builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);

builder.Services.AddRazorComponents().AddInteractiveServerComponents();

var app = builder.Build();

// 4. INICIJALIZACIJA BAZE I SEEDANJE (KLJUČNI DIO)
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<RepozitorijContext>();
    
    // Kreira tablice ako ne postoje
    context.Database.EnsureCreated(); 
    
    // Provjera: ako nema usluga, pokreni seeder
    if (!context.Usluge.Any()) 
    {
        Console.WriteLine(">>> Baza je prazna. Pokrećem DataSeeder...");
        DataSeeder.Seed(context); 
        Console.WriteLine(">>> Podaci su uspješno dodani.");
    }
    else
    {
        Console.WriteLine(">>> Baza već sadrži podatke, preskačem seedanje.");
    }
}

// 5. MIDDLEWARE
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();
app.UseAntiforgery();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorComponents<App>().AddInteractiveServerRenderMode();
app.MapControllers(); 

app.Run();