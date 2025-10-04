CREATE TABLE [Grade] (
	Id              INT              NOT NULL    PRIMARY KEY IDENTITY(1, 1),
	Uid             UNIQUEIDENTIFIER NOT NULL,
	IsDeleted       BIT              NOT NULL    DEFAULT 0,
	Value           INT              NOT NULL,
	DateAssigned    DATE             NOT NULL,
	StudentFk       INT              NOT NULL    FOREIGN KEY REFERENCES [User]([Id]),
	CourseFk        INT              NOT NULL    FOREIGN KEY REFERENCES [Course]([Id]),
	ProfessorFk     INT              NOT NULL    FOREIGN KEY REFERENCES [User]([Id])
)