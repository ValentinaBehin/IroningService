using IroningService.Repozitorij.Data;
using IroningService.Repozitorij.Repozitoriji;
using IroningService.Repozitorij.Suclja;
using IroningService.Servis.Implementacije;
using IroningService.Servis.Suclja;
using Microsoft.EntityFrameworkCore;
using IroningService.Blazor.Components;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<RepozitorijContext>(options =>
    options.UseSqlServer(connString));

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

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<RepozitorijContext>();
    
    context.Database.EnsureCreated(); 
    
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

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); 
}

app.UseStaticFiles();
app.UseAntiforgery();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorComponents<App>().AddInteractiveServerRenderMode();
app.MapControllers(); 

app.Run();