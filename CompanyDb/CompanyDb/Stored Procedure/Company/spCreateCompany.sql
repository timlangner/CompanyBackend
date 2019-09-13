CREATE PROCEDURE [dbo].[spCreateCompany]
	@CompanyName nvarchar(256),
	@FoundedDate date
AS

	INSERT INTO [Company]	
				(
					[Name],
					[FoundedDate]
				) 
	VALUES		
				(
					@CompanyName,
					@FoundedDate
				)

SELECT SCOPE_IDENTITY();
