CREATE VIEW [dbo].[viEmployee]
	AS SELECT 
		[Id], 
		[FirstName], 
		[LastName], 
		[Birthdate], 
		[DepartmentId], 
		[AdressId] 
	FROM 
		[Employee] 
	WHERE 
		[DeleteTime] 
	IS NULL
