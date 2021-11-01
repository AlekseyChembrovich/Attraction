CREATE TABLE [dbo].[Attraction]
(
	[Id] INT NOT NULL IDENTITY (1, 1) PRIMARY KEY, 
    [Name] NVARCHAR(100) NULL, 
    [FoundationDate] DATE, 
    [Description] NVARCHAR(200) NULL, 
    [Image] IMAGE NULL, 
    [Phone] NCHAR(30) NULL, 
    [IsRoundСlock] BIT NULL, 
    [StartTime] TIME NULL, 
    [EndTime] TIME NULL, 
    [LocalityId] INT NOT NULL,
    [TypeAttractionId] INT NOT NULL,
    CONSTRAINT [FK_Attraction_Locality] FOREIGN KEY ([LocalityId]) REFERENCES [dbo].[Locality] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_Attraction_TypeAttraction] FOREIGN KEY ([TypeAttractionId]) REFERENCES [dbo].[TypeAttraction] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
)
