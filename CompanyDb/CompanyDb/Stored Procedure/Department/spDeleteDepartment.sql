CREATE PROCEDURE [dbo].[spDeleteDepartment]
	@DbId int
AS
	UPDATE Department SET DeleteTime = GetDate() WHERE Id = @DbId
RETURN 0
