CREATE PROCEDURE [dbo].[spDeleteCompany]
	@DbId int
AS
	UPDATE Company SET DeleteTime = GetDate() WHERE Id = @DbId
RETURN 0
