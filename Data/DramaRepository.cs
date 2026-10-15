using DoramaFlix.Api.Models;
using Microsoft.Data.SqlClient;

namespace DoramaFlix.Api.Data;

public class DramaRepository
{
    private readonly string _connectionString;

    public DramaRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DoramaFlixConnection")
            ?? throw new InvalidOperationException(
                "Connection string 'DoramaFlixConnection' was not found."
            );
    }

    public async Task<List<Drama>> GetAllAsync()
    {
        var dramas = new List<Drama>();

        const string sql = """
            SELECT
                Id,
                Title,
                Country,
                Genre,
                ReleaseYear,
                Episodes,
                Rating,
                IsCompleted
            FROM Dramas
            ORDER BY Id;
            """;

        await using var connection = new SqlConnection(_connectionString);
        await using var command = new SqlCommand(sql, connection);

        await connection.OpenAsync();

        await using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            dramas.Add(new Drama
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Title = reader.GetString(reader.GetOrdinal("Title")),
                Country = reader.GetString(reader.GetOrdinal("Country")),
                Genre = reader.GetString(reader.GetOrdinal("Genre")),
                ReleaseYear = reader.GetInt32(reader.GetOrdinal("ReleaseYear")),
                Episodes = reader.GetInt32(reader.GetOrdinal("Episodes")),
                Rating = Convert.ToDouble(
                    reader.GetDecimal(reader.GetOrdinal("Rating"))
                ),
                IsCompleted = reader.GetBoolean(
                    reader.GetOrdinal("IsCompleted")
                )
            });
        }

        return dramas;
    }

    public async Task<Drama?> GetByIdAsync(int id)
    {
        const string sql = """
            SELECT
                Id,
                Title,
                Country,
                Genre,
                ReleaseYear,
                Episodes,
                Rating,
                IsCompleted
            FROM Dramas
            WHERE Id = @Id;
            """;

        await using var connection = new SqlConnection(_connectionString);
        await using var command = new SqlCommand(sql, connection);

        command.Parameters.AddWithValue("@Id", id);

        await connection.OpenAsync();

        await using var reader = await command.ExecuteReaderAsync();

        if (!await reader.ReadAsync())
        {
            return null;
        }

        return new Drama
        {
            Id = reader.GetInt32(reader.GetOrdinal("Id")),
            Title = reader.GetString(reader.GetOrdinal("Title")),
            Country = reader.GetString(reader.GetOrdinal("Country")),
            Genre = reader.GetString(reader.GetOrdinal("Genre")),
            ReleaseYear = reader.GetInt32(reader.GetOrdinal("ReleaseYear")),
            Episodes = reader.GetInt32(reader.GetOrdinal("Episodes")),
            Rating = Convert.ToDouble(
                reader.GetDecimal(reader.GetOrdinal("Rating"))
            ),
            IsCompleted = reader.GetBoolean(
                reader.GetOrdinal("IsCompleted")
            )
        };
    }

    public async Task<List<Drama>> GetByGenreAsync(string genre)
    {
        var dramas = new List<Drama>();

        const string sql = """
            SELECT
                Id,
                Title,
                Country,
                Genre,
                ReleaseYear,
                Episodes,
                Rating,
                IsCompleted
            FROM Dramas
            WHERE Genre = @Genre
            ORDER BY Rating DESC;
            """;

        await using var connection = new SqlConnection(_connectionString);
        await using var command = new SqlCommand(sql, connection);

        command.Parameters.AddWithValue("@Genre", genre);

        await connection.OpenAsync();

        await using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            dramas.Add(new Drama
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Title = reader.GetString(reader.GetOrdinal("Title")),
                Country = reader.GetString(reader.GetOrdinal("Country")),
                Genre = reader.GetString(reader.GetOrdinal("Genre")),
                ReleaseYear = reader.GetInt32(reader.GetOrdinal("ReleaseYear")),
                Episodes = reader.GetInt32(reader.GetOrdinal("Episodes")),
                Rating = Convert.ToDouble(
                    reader.GetDecimal(reader.GetOrdinal("Rating"))
                ),
                IsCompleted = reader.GetBoolean(
                    reader.GetOrdinal("IsCompleted")
                )
            });
        }

        return dramas;
    }

    public async Task<Drama> CreateAsync(Drama drama)
    {
        const string sql = """
            INSERT INTO Dramas
                (
                    Title,
                    Country,
                    Genre,
                    ReleaseYear,
                    Episodes,
                    Rating,
                    IsCompleted
                )
            OUTPUT INSERTED.Id
            VALUES
                (
                    @Title,
                    @Country,
                    @Genre,
                    @ReleaseYear,
                    @Episodes,
                    @Rating,
                    @IsCompleted
                );
            """;

        await using var connection = new SqlConnection(_connectionString);
        await using var command = new SqlCommand(sql, connection);

        command.Parameters.AddWithValue("@Title", drama.Title);
        command.Parameters.AddWithValue("@Country", drama.Country);
        command.Parameters.AddWithValue("@Genre", drama.Genre);
        command.Parameters.AddWithValue("@ReleaseYear", drama.ReleaseYear);
        command.Parameters.AddWithValue("@Episodes", drama.Episodes);
        command.Parameters.AddWithValue("@Rating", drama.Rating);
        command.Parameters.AddWithValue("@IsCompleted", drama.IsCompleted);

        await connection.OpenAsync();

        var newId = await command.ExecuteScalarAsync();

        drama.Id = Convert.ToInt32(newId);

        return drama;
    }

    public async Task<bool> UpdateAsync(int id, Drama drama)
    {
        const string sql = """
            UPDATE Dramas
            SET
                Title = @Title,
                Country = @Country,
                Genre = @Genre,
                ReleaseYear = @ReleaseYear,
                Episodes = @Episodes,
                Rating = @Rating,
                IsCompleted = @IsCompleted
            WHERE Id = @Id;
            """;

        await using var connection = new SqlConnection(_connectionString);
        await using var command = new SqlCommand(sql, connection);

        command.Parameters.AddWithValue("@Id", id);
        command.Parameters.AddWithValue("@Title", drama.Title);
        command.Parameters.AddWithValue("@Country", drama.Country);
        command.Parameters.AddWithValue("@Genre", drama.Genre);
        command.Parameters.AddWithValue("@ReleaseYear", drama.ReleaseYear);
        command.Parameters.AddWithValue("@Episodes", drama.Episodes);
        command.Parameters.AddWithValue("@Rating", drama.Rating);
        command.Parameters.AddWithValue("@IsCompleted", drama.IsCompleted);

        await connection.OpenAsync();

        var rowsAffected = await command.ExecuteNonQueryAsync();

        return rowsAffected > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        const string sql = """
            DELETE FROM Dramas
            WHERE Id = @Id;
            """;

        await using var connection = new SqlConnection(_connectionString);
        await using var command = new SqlCommand(sql, connection);

        command.Parameters.AddWithValue("@Id", id);

        await connection.OpenAsync();

        var rowsAffected = await command.ExecuteNonQueryAsync();

        return rowsAffected > 0;
    }
}