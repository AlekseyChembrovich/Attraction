CREATE TABLE [dbo].[Locality]
(
	[Id] INT NOT NULL IDENTITY(1, 1) PRIMARY KEY,
	[Name] NVARCHAR(100) NULL,
	[Region] NVARCHAR(50) NULL,
	[Address] NVARCHAR(100) NULL,
	[Latitude] NCHAR(25) NULL, 
    [Longitude] NCHAR(25) NULL
)
