CREATE TABLE [Course] (
	Id              INT              NOT NULL    PRIMARY KEY IDENTITY(1, 1),
	Uid             UNIQUEIDENTIFIER NOT NULL,
	IsDeleted       BIT              NOT NULL    DEFAULT 0,
	CourseId        NVARCHAR(7)      NOT NULL,
	CourseName      NVARCHAR(100)     NOT NULL,
	ProfessorFk     INT              NOT NULL    FOREIGN KEY REFERENCES [User]([Id])
)