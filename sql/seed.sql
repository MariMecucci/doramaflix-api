USE DoramaFlix;
GO

DELETE FROM Dramas;
GO

DBCC CHECKIDENT ('Dramas', RESEED, 0);
GO

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
VALUES
    ('Crash Landing on You', 'South Korea', 'Romance', 2019, 16, 8.7, 1),
    ('Lovely Runner', 'South Korea', 'Romance', 2024, 16, 8.8, 1),
    ('Alchemy of Souls', 'South Korea', 'Fantasy', 2022, 20, 8.8, 1),
    ('Business Proposal', 'South Korea', 'Romantic Comedy', 2022, 12, 8.4, 1),
    ('Queen of Tears', 'South Korea', 'Romance', 2024, 16, 8.5, 1),
    ('Twenty-Five Twenty-One', 'South Korea', 'Coming of Age', 2022, 16, 8.6, 1),
    ('Extraordinary Attorney Woo', 'South Korea', 'Legal Drama', 2022, 16, 8.7, 1),
    ('Vincenzo', 'South Korea', 'Crime', 2021, 20, 8.4, 1),
    ('The Glory', 'South Korea', 'Thriller', 2022, 16, 8.7, 1),
    ('My Demon', 'South Korea', 'Fantasy Romance', 2023, 16, 8.2, 1),
    ('Hotel Del Luna', 'South Korea', 'Fantasy', 2019, 16, 8.3, 1),
    ('Hometown Cha-Cha-Cha', 'South Korea', 'Romantic Comedy', 2021, 16, 8.4, 1),
    ('Moving', 'South Korea', 'Action Fantasy', 2023, 20, 8.8, 1),
    ('True Beauty', 'South Korea', 'Romantic Comedy', 2020, 16, 8.0, 1),
    ('Start-Up', 'South Korea', 'Drama', 2020, 16, 8.0, 1);
GO

SELECT *
FROM Dramas
ORDER BY Id;
GO