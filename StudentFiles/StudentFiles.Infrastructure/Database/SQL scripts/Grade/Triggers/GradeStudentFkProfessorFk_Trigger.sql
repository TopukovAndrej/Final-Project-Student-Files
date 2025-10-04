CREATE TRIGGER TR_Grade_StudentFk_ProfessorFk ON [Grade]
AFTER INSERT, UPDATE AS
BEGIN
	SET NOCOUNT ON;

	IF (EXISTS(SELECT 1
			  FROM inserted i
			  JOIN [User] AS U ON U.Id = i.StudentFk
			  WHERE U.Role != 'STUDENT')
	 OR EXISTS(SELECT 1
			   FROM inserted i
			   JOIN [User] AS U ON U.Id = i.ProfessorFk
			   WHERE U.Role != 'PROFESSOR'))
	BEGIN
		THROW 50001, 'Either StudentFk or ProfessorFk does not reference the relevant user role in the User table', 1;
	END
END;