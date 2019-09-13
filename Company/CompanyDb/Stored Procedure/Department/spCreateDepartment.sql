CREATE PROCEDURE [dbo].[spCreateDepartment]
	@Name varchar(256),
	@Description nvarchar(MAX),
	@CompanyId int
AS
	INSERT INTO [Department] 
				(
					[Name], 
					[Description], 
					[CompanyId]
				) 
	VALUES		
				(
					@Name, 
					@Description, 
					@CompanyId
				);
RETURN 0
