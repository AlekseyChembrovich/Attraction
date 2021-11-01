CREATE TABLE [dbo].[Event]
(
	[Id] INT NOT NULL IDENTITY (1, 1) PRIMARY KEY, 
    [Name] NVARCHAR(100) NULL, 
    [Date] DATE NULL, 
    [StartTime] TIME NULL,
    [Description] NVARCHAR(200) NULL,
    [TypeEventId] INT NOT NULL, 
    [AttractionId] INT NOT NULL, 
    CONSTRAINT [FK_Event_TypeEvent] FOREIGN KEY ([TypeEventId]) REFERENCES [dbo].[TypeEvent] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_Event_Attraction] FOREIGN KEY ([AttractionId]) REFERENCES [dbo].[Attraction] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
)
