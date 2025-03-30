CREATE TABLE [User] (
	Id              INT              NOT NULL    PRIMARY KEY IDENTITY(1, 1),
	Uid             UNIQUEIDENTIFIER NOT NULL,
	IsDeleted       BIT              NOT NULL    DEFAULT 0,
	Username        NVARCHAR(77)     NOT NULL,
	Password        NVARCHAR(60)     NOT NULL,
	Role            NVARCHAR(9)      NOT NULL
)