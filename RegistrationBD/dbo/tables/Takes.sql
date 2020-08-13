CREATE TABLE [dbo].[Takes]
(
	[TakesID] INT IDENTITY (1,1) NOT NULL, 
    [CoursesID] INT NOT NULL, 
    [StudentID] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([TakesID] ASC),
    CONSTRAINT [FK_takes_Courses] FOREIGN KEY ([CoursesID]) REFERENCES [dbo].[Courses] ([CoursesID]),
    CONSTRAINT [FK_takes_Students] FOREIGN KEY ([StudentID]) REFERENCES [dbo].[Students] ([StudentID]),
)
