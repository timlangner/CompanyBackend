CREATE TABLE [dbo].[Company2Adress]
(
	[CompanyId] INT NOT NULL REFERENCES Company(Id),
	[AdressId] INT NOT NULL REFERENCES Adress(Id),
	[CreationTime] datetime2 NOT NULL DEFAULT GETDATE(),
	PRIMARY KEY (CompanyId, AdressId)
)
