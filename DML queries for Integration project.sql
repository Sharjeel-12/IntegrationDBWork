use IntegrationDB_MuhammadSharjeelFarzad

insert into roles(roleName) values
('Admin'),('Provider'),('Receptionist');
go

-- sp for creating user
create procedure AddUser @email nvarchar(100), @username nvarchar(100), @userPassword varbinary(max), @userRole nvarchar(20)
as 
begin
declare @roleID int;
set @roleID=(select roleID from roles where roleName=@userRole);
--select @roleID=roleID from roles where roleName=@userRole;
insert into users(emailAddress, Username, userPassword, RoleID) values (@email, @username, @userPassword, @roleID );
end

go
-- sp for fetching user by email
create procedure getUser @email nvarchar(100)
as 
begin
select * from users where emailAddress=@email;
end
go

-- sp for changing password of user
create procedure changePassword @email nvarchar(100), @oldPassword varbinary(max), @newPassword varbinary(max)
as 
begin
update users
set userPassword=@newPassword where userPassword=@oldPassword and emailAddress=@email;

-- @@ROWCOUNT is a system variable that stores the number of rows updated by the last sql query
IF @@ROWCOUNT = 0
    BEGIN
        RAISERROR('Invalid email or old password.', 16, 1);
    END

end
go

-- sp for CRUD of practices
create procedure createNewPractice @name nvarchar(100), @TID nvarchar(50), @practiceAddress nvarchar(100),
@contactEmail nvarchar(100), @POS nvarchar(10)
as
begin

begin try
begin tran;
insert into practices(practiceName,taxID) values (@name,@TID);
Declare @practiceID int = SCOPE_IDENTITY();
insert into practiceLocations(practiceAddress,contactEmail, POS, practiceID) values (@practiceAddress,@contactEmail,@POS,@practiceID);
commit tran;
end try

begin catch
if @@TRANCOUNT>0 rollback tran;
throw;

end catch
end
go

-- sp for adding new location for an exisiting practice
create procedure addNewLocation @practiceID int, @practiceName nvarchar(100), @practiceAddress nvarchar(100),@contactEmail nvarchar(100),
 @POS nvarchar(10) as
begin
--Declare @practiceID int = (select practiceID from practices where practiceName=@practiceName);
insert into practiceLocations(practiceAddress,contactEmail,POS,practiceID) values (@practiceAddress,@contactEmail,@POS,@practiceID);
end
go

-- sp for updating a practice location --fixed
create procedure updateLocation @practiceID int, @practiceName nvarchar(100), @practiceAddress nvarchar(100),@contactEmail nvarchar(100),
 @POS nvarchar(10) as
begin

begin try

begin tran

--Declare @practiceID int;
--select @practiceID=practiceID from practices where practiceName=@practiceName;
--IF @practiceID IS NULL
--    BEGIN
--        RAISERROR('No such practice exists', 16, 1);
--        RETURN;
--    END

update practiceLocations
set practiceAddress=@practiceAddress, contactEmail=@contactEmail, POS=@POS where practiceID=@practiceID;

if @@ROWCOUNT =0
begin
INSERT INTO practiceLocations (practiceAddress, contactEmail, POS, practiceID)
            VALUES (@practiceAddress, @contactEmail, @POS, @practiceID);
end

commit tran

end try

begin catch
IF @@TRANCOUNT > 0 ROLLBACK TRAN;
        ;THROW;
end catch

end
go


-- sp for deleting the existing location for a practice
create procedure deletePracticeLocation @locationID int as 
begin

delete from practiceLocations where locationID=@locationID
end
go
-- CRUD operations for patients table


-- creating new patient
create procedure addNewPatient @Title NVARCHAR(20), @FirstName NVARCHAR(50), @LastName NVARCHAR(50), @DateOfBirth DATE,
@Gender NVARCHAR(10), @EmailAddress NVARCHAR(100), @ContactNumber NVARCHAR(20) as 

begin
begin try

begin tran
declare @duplicateChecker int;
select @duplicateChecker=count(*) from patients where EmailAddress=@EmailAddress;
if @duplicateChecker >0 
begin
raiserror('the patient with same email already exists!',16,1);
end

insert into patients(Title,FirstName,LastName,DateOfBirth,Gender,EmailAddress,ContactNumber)
values (@Title,@FirstName,@LastName,@DateOfBirth,@Gender,@EmailAddress,@ContactNumber);
commit tran

end try
begin catch
if @@TRANCOUNT>0 rollback tran;
;throw;
end catch
end
go

-- updating a patient record

create procedure updatePatientRecord @Title NVARCHAR(20), @FirstName NVARCHAR(50), @LastName NVARCHAR(50), @DateOfBirth DATE,
@Gender NVARCHAR(10), @EmailAddress NVARCHAR(100), @ContactNumber NVARCHAR(20) as 
begin 
begin try
begin tran
update patients 
set Title=@Title,FirstName=@FirstName,LastName=@LastName,DateOfBirth=@DateOfBirth,Gender=@Gender,ContactNumber=@ContactNumber where EmailAddress=@EmailAddress;

if @@ROWCOUNT=0 
begin
insert into patients(Title,FirstName,LastName,DateOfBirth,Gender,EmailAddress,ContactNumber)
values (@Title,@FirstName,@LastName,@DateOfBirth,@Gender,@EmailAddress,@ContactNumber);
end
commit tran
end try
begin catch
if @@TRANCOUNT>0 rollback tran;
if ERROR_NUMBER() in (2601,2627)
begin
-- try updating again
update patients
set Title=@Title,FirstName=@FirstName,LastName=@LastName,DateOfBirth=@DateOfBirth,Gender=@Gender,ContactNumber=@ContactNumber where EmailAddress=@EmailAddress;

end

else
	throw;
end catch


end

-- deleting the patient
create procedure deletePatientRecord @patientID int as 
begin
delete from patients where patientID=@patientID;
end


-- sps for crud operations on 















