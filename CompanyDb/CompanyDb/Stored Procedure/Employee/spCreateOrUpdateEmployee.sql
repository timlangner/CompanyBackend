CREATE PROCEDURE [dbo].[spCreateOrUpdateEmployee] 
    @EmployeeId int = 0, 
    @FirstName nvarchar(128), 
    @LastName nvarchar(128), 
    @Birthdate date, 
    @DepartmentId int 
AS 

      declare @dbId int = (select id from viEmployee where id = @EmployeeId) 

      if(@dbId is null) 
      begin 
             INSERT INTO [dbo].[Employee] 
           ([FirstName] 
           ,[LastName] 
           ,[Birthdate]
           ,[DepartmentId]) 
             VALUES 
                     (@FirstName,@LastName,@Birthdate,@DepartmentId) 

            set @dbId = @@IDENTITY 
      end 
            else 
      begin 
            update  [dbo].[Employee] set 
                        FirstName = case when @FirstName is null then FirstName else @FirstName end, 
                        LastName = @LastName 
			where id = @dbId
      end 



Select @dbId; 
RETURN @dbId;
