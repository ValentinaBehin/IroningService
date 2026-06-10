using IroningService.Repozitorij.Data;
using IroningService.Repozitorij.Repozitoriji;
using IroningService.Repozitorij.Suclja;
using IroningService.Servis.Implementacije;
using IroningService.Servis.Suclja;
using Microsoft.EntityFrameworkCore;
using IroningService.Blazor.Components;

var tempBuilder = WebApplication.CreateBuilder(args);
var connString = tempBuilder.Configuration.GetConnectionString("DefaultConnection");
Console.WriteLine("--------------------------------------------------");
Console.WriteLine($"KORISTIM OVAJ STRING: {connString}");
Console.WriteLine("--------------------------------------------------");

var builder = WebApplication.CreateBuilder(args);

// 1. LOGGING
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// Baza podataka
builder.Services.AddDbContext<RepozitorijContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// HttpClient
builder.Services.AddScoped(sp => new HttpClient { 
    BaseAddress = new Uri(builder.Configuration.GetValue<string>("ApiBaseUrl") ?? "http://localhost:5038") 
});

// REGISTRACIJA REPOZITORIJA I SERVISA
builder.Services.AddScoped<IUslugaRepozitorij, UslugaRepozitorij>();
builder.Services.AddScoped<INarudzbaRepozitorij, NarudzbaRepozitorij>();
builder.Services.AddScoped<IKorisnikRepozitorij, KorisnikRepozitorij>();
builder.Services.AddScoped<IRecenzijaRepozitorij, RecenzijaRepozitorij>();
builder.Services.AddScoped<IUslugaServis, UslugaServis>();
builder.Services.AddScoped<INarudzbaServis, NarudzbaServis>();
builder.Services.AddScoped<IKorisnikServis, KorisnikServis>();

// AUTHENTICATION
builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options => {
        options.LoginPath = "/Login";
        options.AccessDeniedPath = "/AccessDenied";
    });

// Blazor i Kontroleri
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddControllers();

var app = builder.Build();

// 2. ERROR HANDLING MIDDLEWARE
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

// 3. INICIJALIZACIJA BAZE
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<RepozitorijContext>();
    
    // Ovo će kreirati bazu i tablice prema tvom modelu
    context.Database.EnsureCreated(); 
}
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<RepozitorijContext>();
    try {
        if (context.Database.CanConnect()) {
            Console.WriteLine("USPJEH: Baza je dostupna!");
        } else {
            Console.WriteLine("GREŠKA: Ne mogu se spojiti na bazu!");
        }
    } catch (Exception ex) {
        Console.WriteLine($"KRITIČNA GREŠKA: {ex.Message}");
    }
}

  //app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

// 4. AUTH MIDDLEWARE
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapControllers(); 

app.Run();