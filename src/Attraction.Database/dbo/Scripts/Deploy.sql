GO
USE [Attraction]

GO
INSERT INTO [dbo].[TypeEvent] ([dbo].[TypeEvent].[Name], [dbo].[TypeEvent].[Description])
	VALUES ('test1', 'test1'),
		   ('test2', 'test2'),
		   ('test3', 'test3'),
		   ('test4', 'test4'),
		   ('test5', 'test5')

GO
INSERT INTO [dbo].[TypeAttraction] ([dbo].[TypeAttraction].[Name], [dbo].[TypeAttraction].[Description]) 
	VALUES ('test1', 'test1'), 
		   ('test2', 'test2'),
		   ('test3', 'test3'),
		   ('test4', 'test4'),
		   ('test5', 'test5')

GO
INSERT INTO [dbo].[Locality] ([dbo].[Locality].[Name], [dbo].[Locality].[Region], 
							  [dbo].[Locality].[Address], [dbo].[Locality].[Latitude],
							  [dbo].[Locality].[Longitude])
	VALUES ('test1', 'test1', 'test1', NULL, NULL), 
		   ('test2', 'test2', 'test2', NULL, NULL),
		   ('test3', 'test3', 'test3', NULL, NULL),
		   ('test4', 'test4', 'test4', NULL, NULL),
		   ('test5', 'test5', 'test5', NULL, NULL)

GO
INSERT INTO [dbo].[Attraction] ([dbo].[Attraction].[Name], [dbo].[Attraction].[FoundationDate],
								[dbo].[Attraction].[Description], [dbo].[Attraction].[Image], 
								[dbo].[Attraction].[Phone], [dbo].[Attraction].[IsRoundСlock],
								[dbo].[Attraction].[StartTime], [dbo].[Attraction].[EndTime],
								[dbo].[Attraction].[LocalityId], [dbo].[Attraction].[TypeAttractionId]) 
	VALUES ('test1', '2000-01-01', NULL, NULL, '+375-29-111-11-11', 1, '00:00:00', '00:00:00', 1, 1), 
		   ('test2', '2000-01-01', NULL, NULL, '+375-29-111-11-22', 0, '00:00:00', '00:00:00', 2, 2),
		   ('test3', '2000-01-01', NULL, NULL, '+375-29-111-11-33', 1, '00:00:00', '00:00:00', 3, 3),
		   ('test4', '2000-01-01', NULL, NULL, '+375-29-111-11-44', 0, '00:00:00', '00:00:00', 4, 4),
		   ('test5', '2000-01-01', NULL, NULL, '+375-29-111-11-55', 1, '00:00:00', '00:00:00', 5, 5)

GO
INSERT INTO [dbo].[Event] ([dbo].[Event].[Name], [dbo].[Event].[Date],
						   [dbo].[Event].[StartTime], [dbo].[Event].[Description],
						   [dbo].[Event].[TypeEventId], [dbo].[Event].[AttractionId]) 
	VALUES ('test1', '2000-01-01', '00:00:00', 'test1', 1, 1), 
		   ('test2', '2000-01-01', '00:00:00', 'test2', 2, 2),
		   ('test3', '2000-01-01', '00:00:00', 'test3', 3, 3),
		   ('test4', '2000-01-01', '00:00:00', 'test4', 4, 4),
		   ('test5', '2000-01-01', '00:00:00', 'test5', 5, 5)
