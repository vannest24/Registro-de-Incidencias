using Supabase;
using Npgsql;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;
var builder = WebApplication.CreateBuilder(args);

//Configuración de RateLimiting
builder.Services.AddRateLimiter((RateLimiterOptions options) =>
{
    options.AddFixedWindowLimiter("LoginLimiter", opt =>
    {
        opt.Window = TimeSpan.FromMinutes(1);
        opt.PermitLimit = 3;
        opt.QueueLimit = 0;
    });

    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
});


var supabaseUrl = builder.Configuration["Supabase:Url"];
var supabaseKey = builder.Configuration["Supabase:Key"];

var options = new SupabaseOptions { AutoConnectRealtime = true };

// Add services to the container.
builder.Services.AddRazorPages();


// --- BLOQUE DE PRUEBA: quitar después de confirmar conexión ---
/*var connString = builder.Configuration.GetConnectionString("DefaultConnection");
try
{
    await using var testConn = new NpgsqlConnection(connString);
    await testConn.OpenAsync();
    Console.WriteLine("Conexión exitosa a Supabase");
}
catch (Exception ex)
{
    Console.WriteLine($" Error de conexión: {ex.Message}");
}
// --- FIN BLOQUE DE PRUEBA ---*/
var app = builder.Build();

app.UseRateLimiter();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
