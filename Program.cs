using DoramaFlix.Api.Data;
using DoramaFlix.Api.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<DramaRepository>();

var app = builder.Build();

app.MapGet("/", () =>
{
    return "DoramaFlix API is running";
});

app.MapGet("/api/dramas", async (DramaRepository repository) =>
{
    var dramas = await repository.GetAllAsync();

    return Results.Ok(dramas);
});

app.MapGet("/api/dramas/{id:int}", async (
    int id,
    DramaRepository repository
) =>
{
    var drama = await repository.GetByIdAsync(id);

    return drama is not null
        ? Results.Ok(drama)
        : Results.NotFound(new { message = "Drama not found." });
});

app.MapGet("/api/dramas/genre/{genre}", async (
    string genre,
    DramaRepository repository
) =>
{
    var dramas = await repository.GetByGenreAsync(genre);

    return dramas.Count > 0
        ? Results.Ok(dramas)
        : Results.NotFound(
            new { message = "No dramas found for this genre." }
        );
});

app.MapPost("/api/dramas", async (
    Drama newDrama,
    DramaRepository repository
) =>
{
    var createdDrama = await repository.CreateAsync(newDrama);

    return Results.Created(
        $"/api/dramas/{createdDrama.Id}",
        createdDrama
    );
});

app.MapPut("/api/dramas/{id:int}", async (
    int id,
    Drama updatedDrama,
    DramaRepository repository
) =>
{
    var updated = await repository.UpdateAsync(id, updatedDrama);

    if (!updated)
    {
        return Results.NotFound(
            new { message = "Drama not found." }
        );
    }

    updatedDrama.Id = id;

    return Results.Ok(updatedDrama);
});

app.MapDelete("/api/dramas/{id:int}", async (
    int id,
    DramaRepository repository
) =>
{
    var deleted = await repository.DeleteAsync(id);

    return deleted
        ? Results.NoContent()
        : Results.NotFound(
            new { message = "Drama not found." }
        );
});

app.Run();