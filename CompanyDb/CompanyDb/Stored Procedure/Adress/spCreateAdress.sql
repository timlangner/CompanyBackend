CREATE PROCEDURE [dbo].[spCreateAdress]
	@Street nvarchar(512),
	@City nvarchar(256),
	@ZipCode varchar(128),
	@Country varchar(256)

AS
	INSERT INTO [Adress] 
				(
					Street,
					City,
					ZipCode,
					Country
				) 
	VALUES 
				(
					@Street,
					@City,
					@ZipCode,
					@Country
				);
RETURN 0
