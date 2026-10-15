IF DB_ID('DoramaFlix') IS NULL
BEGIN
    CREATE DATABASE DoramaFlix;
END
GO

USE DoramaFlix;
GO

IF OBJECT_ID('dbo.Dramas', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Dramas
    (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Title NVARCHAR(200) NOT NULL,
        Country NVARCHAR(100) NOT NULL,
        Genre NVARCHAR(100) NOT NULL,
        ReleaseYear INT NOT NULL,
        Episodes INT NOT NULL,
        Rating DECIMAL(3,1) NOT NULL,
        IsCompleted BIT NOT NULL
    );
END
GO