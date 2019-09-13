CREATE VIEW [dbo].[viAdress]
	AS SELECT 
		[Id], 
		[Street], 
		[City], 
		[ZipCode], 
		[Country] 
	FROM 
		[Adress] 
	WHERE
		[DeleteTime]
	IS NULL
