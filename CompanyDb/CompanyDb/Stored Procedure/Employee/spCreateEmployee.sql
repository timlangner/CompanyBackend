CREATE PROCEDURE [dbo].[spCreateEmployee]
	@FirstName varchar(256),
	@LastName varchar(256),
	@Birthdate date,
	@DepartmentId int,
	@AdressId int
AS
	INSERT INTO [Employee] 
				(
					FirstName,
					LastName,
					Birthdate,
					DepartmentId,
					AdressId
				) 
	VALUES 
				(
					@FirstName,
					@LastName,
					@Birthdate,
					@DepartmentId,
					@AdressId
				)
RETURN 0
