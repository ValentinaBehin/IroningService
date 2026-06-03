using IroningService.Repozitorij.Data;
using IroningService.Repozitorij.Repozitoriji;
using IroningService.Repozitorij.Suclja;
using IroningService.Servis.Implementacije;
using IroningService.Servis.Suclja;
using Microsoft.EntityFrameworkCore;
using IroningService.Blazor.Components;

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

// REGISTRACIJA REPOZITORIJA
builder.Services.AddScoped<IUslugaRepozitorij, UslugaRepozitorij>();
builder.Services.AddScoped<INarudzbaRepozitorij, NarudzbaRepozitorij>();
builder.Services.AddScoped<IKorisnikRepozitorij, KorisnikRepozitorij>();

// REGISTRACIJA SERVISA
builder.Services.AddScoped<IUslugaServis, UslugaServis>();
builder.Services.AddScoped<INarudzbaServis, NarudzbaServis>();
builder.Services.AddScoped<IKorisnikServis, KorisnikServis>();

// AUTHENTICATION (Sada je ispravno na vrhu!)
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

// Inicijalizacija baze
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<RepozitorijContext>();
    context.Database.EnsureCreated(); 
    DataSeeder.SeedUsluge(context);
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

// 3. MIDDLEWARE ZA AUTH (Nakon Build-a)
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapControllers(); 

app.Run();