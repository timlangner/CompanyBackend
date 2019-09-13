CREATE VIEW [dbo].[viDepartment]
	AS SELECT 
		[Id], 
		[Name], 
		[Description], 
		[CompanyId] 
	FROM 
		[Department] 
	WHERE 
		[DeleteTime] 
	IS NULL
