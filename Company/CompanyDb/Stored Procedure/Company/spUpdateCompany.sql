CREATE PROCEDURE [dbo].[spUpdateCompany]
	@DbId int,
	@CompanyName nvarchar(256),
	@FoundedDate date = null
AS
	UPDATE Company SET
		[Name] = @CompanyName,
		[FoundedDate] = CASE WHEN @FoundedDate IS NULL THEN FoundedDate ELSE @FoundedDate end
	WHERE Id = @DbId;
RETURN 0
