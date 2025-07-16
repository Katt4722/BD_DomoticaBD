using System.Data;
using MySqlConnector;
using Scalar.AspNetCore;
using Biblioteca;
using Biblioteca.Persistencia.Dapper;


var builder = WebApplication.CreateBuilder(args);

//  Obtener la cadena de conexión desde appsettings.json
var connectionString = builder.Configuration.GetConnectionString("MySQL");

//  Registrando IDbConnection para que se inyecte como dependencia
//  Cada vez que se inyecte, se creará una nueva instancia con la cadena de conexión
builder.Services.AddScoped<IDbConnection>(sp => new MySqlConnection(connectionString));

//Cada vez que necesite la interfaz, se va a instanciar automaticamente AdoDapper y se va a pasar al metodo de la API
builder.Services.AddScoped<IAdo, AdoDapper>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options =>
    {
        options.RouteTemplate = "/openapi/{documentName}.json";
    });
    app.MapScalarApiReference();
}

app.MapGet("/", () => "Hello World!");
// Trae electrodomesticos por id
app.MapGet("/electrodomesticos/{id}", async (int id, IAdo repo) =>
    await repo.ObtenerElectrodomesticoAsync(id));

// Agrega un nuevo electrodomestico
app.MapPost("/electrodomesticos", async (Electrodomestico electro, IAdo repo) =>
{
    await repo.AltaElectrodomesticoAsync(electro);
    return Results.Created($"/electrodomesticos/{electro.IdElectrodomestico}", electro);
}); 

// Trae casas por id
app.MapGet("/casas/{id}", async (int id, IAdo repo) =>
    await repo.ObtenerCasaAsync(id));

// Agrega una nueva casa
app.MapPost("/casas", async (Casa casa, IAdo repo) =>
{
    await repo.AltaCasaAsync(casa);
    return Results.Created($"/casas/{casa.IdCasa}", casa);
});

app.Run();