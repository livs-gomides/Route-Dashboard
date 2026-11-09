
using RouteDashboard.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddSingleton<MongoDbService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//app.UseHttpsRedirection();

app.MapGet("/api/health", () =>
{
    return Results.Ok(new
    {
        status = "API funcionando",
        project = "Route Dashboard"
    });
});

app.MapGet("/api/health/mongodb", async (MongoDbService mongoDb) =>
{
    try
    {
        await mongoDb.PingAsync();

        return Results.Ok(new
        {
            status = "Conectado",
            database = "RouteDashboard",
            message = "Conexão com MongoDB realizada com sucesso!"
        });
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "Erro ao conectar ao MongoDB.");

        return Results.Problem(
            title: "Erro na conexão com MongoDB",
            detail: "Não foi possível conectar ao banco de dados.",
            statusCode: 500
        );
    }
});

app.MapControllers();

app.Run();
