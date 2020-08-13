CREATE TABLE [dbo].[Students]
(
	[StudentID] INT IDENTITY (1,1) NOT NULL, 
    [FirstName] NVARCHAR(50) NOT NULL, 
    [LastName] NVARCHAR(50) NOT NULL 
    PRIMARY KEY CLUSTERED ([StudentID] ASC),
)
