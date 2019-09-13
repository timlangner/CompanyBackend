CREATE VIEW [dbo].[viCompany]
	AS SELECT 
		[Id], 
		[Name], 
		[FoundedDate] 
	FROM 
		[Company] 
	WHERE 
		[DeleteTime]
	IS NULL
