CREATE PROCEDURE [dbo].[spDeleteEmployee]
	@Id int
AS
	UPDATE Employee SET DeleteTime = GetDate() WHERE Id = @Id
RETURN 0
