CREATE PROCEDURE [dbo].[spUpdateDepartment]
	@DbId int,
	@Name nvarchar(256),
	@Description varchar(max) = null,
	@CompanyId int
AS
	UPDATE Department SET
		[Name] = @Name,
		[Description] = CASE WHEN @Description IS NULL THEN [Description] ELSE @Description end,
		[CompanyId] = @CompanyId
	WHERE Id = @DbId;
RETURN 0
