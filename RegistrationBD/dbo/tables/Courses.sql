CREATE TABLE [dbo].[Courses]
(
	[CoursesID] INT IDENTITY (1, 1) NOT NULL, 
    [Name] NVARCHAR(50) NOT NULL, 
    [StartTime] DATETIME NOT NULL, 
    [EndTime] DATETIME NOT NULL, 
    [ClassRoomID] INT NOT NULL, 
    PRIMARY KEY CLUSTERED ([CoursesID] ASC),
    CONSTRAINT [FK_Courses] FOREIGN KEY (ClassRoomID) REFERENCES [dbo].[ClassRoom] ([ClassRoomID]),
)