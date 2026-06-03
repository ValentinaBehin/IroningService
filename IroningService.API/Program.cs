using IroningService.Repozitorij.Data;
using IroningService.Repozitorij.Repozitoriji;
using IroningService.Repozitorij.Suclja;
using IroningService.Servis.Implementacije;
using IroningService.Servis.Suclja;
using Microsoft.EntityFrameworkCore;
using IroningService.Blazor.Components;

var builder = WebApplication.CreateBuilder(args);

// Baza podataka
builder.Services.AddDbContext<RepozitorijContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// HttpClient
builder.Services.AddScoped(sp => new HttpClient { 
    BaseAddress = new Uri(builder.Configuration.GetValue<string>("ApiBaseUrl") ?? "http://localhost:5038") 
});

// REGISTRACIJA REPOZITORIJA
builder.Services.AddScoped<IUslugaRepozitorij, UslugaRepozitorij>();
builder.Services.AddScoped<INarudzbaRepozitorij, NarudzbaRepozitorij>();
builder.Services.AddScoped<IKorisnikRepozitorij, KorisnikRepozitorij>(); // Dodano

// REGISTRACIJA SERVISA
builder.Services.AddScoped<IUslugaServis, UslugaServis>();
builder.Services.AddScoped<INarudzbaServis, NarudzbaServis>();
builder.Services.AddScoped<IKorisnikServis, KorisnikServis>(); // Dodano

// Blazor i Kontroleri
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddControllers();


var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<RepozitorijContext>();
    // Osiguraj da je baza kreirana
    context.Database.EnsureCreated(); 
    // Pokreni punjenje podataka
    DataSeeder.SeedUsluge(context);
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapControllers(); 

app.Run();