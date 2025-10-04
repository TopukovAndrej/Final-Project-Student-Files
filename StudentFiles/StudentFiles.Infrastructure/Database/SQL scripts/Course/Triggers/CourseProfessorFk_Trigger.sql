CREATE TRIGGER [dbo].[TR_Course_ProfessorFk] ON [dbo].[Course]
AFTER INSERT, UPDATE AS
BEGIN
	SET NOCOUNT ON;

	IF EXISTS(SELECT 1
			  FROM inserted i
			  JOIN [User] AS U ON U.Id = i.ProfessorFk
			  WHERE U.Role != 'PROFESSOR')
	BEGIN
		THROW 50001, 'The ProfessorFk does not reference a User with role Professor', 1;
	END
END;