CREATE TABLE [dbo].[Product]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NTEXT NULL, 
    [Price] NTEXT NULL, 
    [Image] NTEXT NULL, 
    [CateID] INT NOT NULL
)
