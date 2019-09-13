CREATE TABLE [dbo].[Employee]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[FirstName] varchar(256) NOT NULL,
	[LastName] varchar(256) NOT NULL,
	[Birthdate] date,
	[CreationTime] datetime2 NOT NULL DEFAULT GETDATE(),
	[DeleteTime] datetime2,
	[DepartmentId] INT NOT NULL REFERENCES Department(Id),
	[AdressId] INT REFERENCES Adress(Id)
)
