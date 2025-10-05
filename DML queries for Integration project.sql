use IntegrationDB_MuhammadSharjeelFarzad


insert into roles(roleName) values
('Admin'),('Provider'),('Receptionist');
go

-- sp for fetching the roles
create procedure getRoles as
begin
select * from roles;
end
go
-- sp for creating user
CREATE PROCEDURE AddUser 
    @email NVARCHAR(100), 
    @username NVARCHAR(100), 
    @userPassword VARBINARY(MAX), 
    @userRole NVARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @roleID INT = (SELECT roleID FROM roles WHERE roleName = @userRole);

    IF @roleID IS NULL
    BEGIN
        RAISERROR('Invalid user role specified.', 16, 1);
        RETURN;
    END

    BEGIN TRY
        INSERT INTO users(emailAddress, Username, userPassword, RoleID)
        VALUES (@email, @username, @userPassword, @roleID);
    END TRY
    BEGIN CATCH
        IF ERROR_NUMBER() IN (2601,2627)
            RAISERROR('Duplicate email detected.', 16, 1);
        ELSE
            THROW;
    END CATCH
END
GO

-- sp for fetching all the users
create procedure getAllUsers as
begin
select * from users;
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
 -- sp for getting all the practices
 create procedure getAllPractices as
 begin
 select pl.practiceID, pl.LocationID, pl.practiceAddress, pl.contactEmail, pl.POS, pr.practiceName, pr.TaxID from 
 practiceLocations as pl left join  practices as pr on pr.practiceID=pl.PracticeID; 
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
create procedure updateLocation @locationID int, @practiceID int, @practiceName nvarchar(100), @practiceAddress nvarchar(100),@contactEmail nvarchar(100),
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
set practiceAddress=@practiceAddress, contactEmail=@contactEmail, POS=@POS, practiceID=@practiceID 
where locationID=@locationID;

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

create procedure updatePatientRecord @patientID int, @Title NVARCHAR(20), @FirstName NVARCHAR(50), @LastName NVARCHAR(50), @DateOfBirth DATE,
@Gender NVARCHAR(10), @EmailAddress NVARCHAR(100), @ContactNumber NVARCHAR(20) as 
begin 
begin try
begin tran

update patients 
set Title=@Title,FirstName=@FirstName,LastName=@LastName,DateOfBirth=@DateOfBirth,Gender=@Gender,ContactNumber=@ContactNumber, EmailAddress=@EmailAddress where 
patientID=@patientID;

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
	RAISERROR('duplication in patient email found', 16, 1);
else
	throw;
end catch


end
go
-- deleting the patient
create procedure deletePatientRecord @patientID int as 
begin
delete from patients where patientID=@patientID;
end
go
-- fetching all the patient data 
create procedure getAllPatients as 
begin
select * from patients;
end
go

-- sps for crud operations on provider
create procedure addProvider @FirstName NVARCHAR(50), @LastName NVARCHAR(50), @licenseType nvarchar(30), 
@EmailAddress NVARCHAR(100), @Specialization nvarchar(30) as 
begin
begin try
insert into providers(FirstName, LastName, licenseType, EmailAddress, Specialization) values
(@FirstName, @LastName, @licenseType, @EmailAddress, @Specialization);
end try
begin catch
if ERROR_NUMBER() in (2601,2627)
	RAISERROR('duplication in provider email found', 16, 1);
else
	throw;
end catch
end
go

-- update provider
create procedure updateProvider @providerID int, @FirstName NVARCHAR(50), @LastName NVARCHAR(50), @licenseType nvarchar(30), 
@EmailAddress NVARCHAR(100), @Specialization nvarchar(30) as 
begin
begin try
begin tran
update providers
set FirstName=@FirstName, LastName=@LastName, licenseType=@licenseType, EmailAddress=@EmailAddress, Specialization=@Specialization
where providerID=@providerID;

if @@ROWCOUNT =0 begin
insert into providers(FirstName, LastName, licenseType, EmailAddress, Specialization) values
(@FirstName, @LastName, @licenseType, @EmailAddress, @Specialization);
end

commit tran
end try
begin catch
if @@TRANCOUNT>0 rollback tran;
if ERROR_NUMBER() in (2601,2627)
	RAISERROR('duplication in provider email found', 16, 1);
else
	throw;
end catch
end
go
-- delete the provider
create procedure deleteProvider @providerID int as 
begin 
delete from providers where providerID=@providerID;
end
go
-- fetching all the provider data 
create procedure getAllProviders as 
begin
select * from providers;
end
go

-- crud on resources
create procedure addResource @resourceName nvarchar(100) as
begin
insert into resources(resourceName) values (@resourceName);
end
go

create procedure deleteResource @ResourceID int as
begin
delete from resources where resourceID=@ResourceID;
end
go

-- crud on Appointments/ schedule table
CREATE PROCEDURE UpsertSchedule
    @ProviderScheduleID INT,        
    @ScheduleDate DATE,
    @ScheduleTime TIME,
    @Duration INT,                
    @ScheduleStatus BIT,
    @PatientID INT,
    @ProviderID INT,
    @ResourceID INT,
    @LocationID INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRAN

        -- Compose full start/end datetimes for the new appointment
        DECLARE @StartDT DATETIME2(0) =
            DATEADD(MINUTE, DATEDIFF(MINUTE, 0, @ScheduleTime), CAST(@ScheduleDate AS DATETIME2(0)));
        DECLARE @EndDT   DATETIME2(0) = DATEADD(MINUTE, @Duration, @StartDT);

        -- (start must be in the future)
        IF @StartDT < SYSDATETIME()
        BEGIN
            raiserror( 'Appointment cannot be scheduled in the past.',16, 1);
        END

        -- Weekend guard 
        IF DATENAME(WEEKDAY, @ScheduleDate) IN ('Saturday', 'Sunday')
        BEGIN
            raiserror( 'Appointment cannot be scheduled on weekends.',16, 1);
        END

        -- Overlap check:
        
        IF EXISTS (
            SELECT 1
            FROM ProviderSchedules s
            WHERE s.ProviderID = @ProviderID
              AND s.ScheduleDate = @ScheduleDate
              AND (@ProviderScheduleID IS NULL OR @ProviderScheduleID = 0 OR s.ProviderScheduleID <> @ProviderScheduleID)
            
              AND (
                   
                    DATEADD(MINUTE, DATEDIFF(MINUTE, 0, s.ScheduleTime), CAST(s.ScheduleDate AS DATETIME2(0))) < @EndDT
                AND 
                    DATEADD(MINUTE, s.Duration,
                        DATEADD(MINUTE, DATEDIFF(MINUTE, 0, s.ScheduleTime), CAST(s.ScheduleDate AS DATETIME2(0)))
                    ) > @StartDT
                  )
        )
        BEGIN
            raiserror('Time conflict detected. Appointment cannot be made.', 16,1);
        END

        -- Perform UPSERT
        IF EXISTS (SELECT 1 FROM ProviderSchedules WHERE ProviderScheduleID = @ProviderScheduleID)
        BEGIN
            UPDATE dbo.ProviderSchedules
            SET ScheduleDate   = @ScheduleDate,
                ScheduleTime   = @ScheduleTime,
                Duration       = @Duration,
                ScheduleStatus = @ScheduleStatus,
                PatientID      = @PatientID,
                ProviderID     = @ProviderID,
                ResourceID     = @ResourceID,
                LocationID     = @LocationID
            WHERE ProviderScheduleID = @ProviderScheduleID;
        END
        ELSE
        BEGIN
            INSERT INTO dbo.ProviderSchedules
                (ScheduleDate, ScheduleTime, Duration, ScheduleStatus,
                 PatientID, ProviderID, ResourceID, LocationID)
            VALUES
                (@ScheduleDate, @ScheduleTime, @Duration, @ScheduleStatus,
                 @PatientID, @ProviderID, @ResourceID, @LocationID);
        END

        COMMIT tran
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT>0 ROLLBACK tran;
        THROW; 
    END CATCH
END
GO


-- sp for adding the schedule

CREATE PROCEDURE CreateSchedule    
    @ScheduleDate DATE,
    @ScheduleTime TIME,
    @Duration INT,                
    @ScheduleStatus BIT,
    @PatientID INT,
    @ProviderID INT,
    @ResourceID INT,
    @LocationID INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRAN

        -- Compose full start/end datetimes for the new appointment
        DECLARE @StartDT DATETIME2(0) =
            DATEADD(MINUTE, DATEDIFF(MINUTE, 0, @ScheduleTime), CAST(@ScheduleDate AS DATETIME2(0)));
        DECLARE @EndDT   DATETIME2(0) = DATEADD(MINUTE, @Duration, @StartDT);

        -- (start must be in the future)
        IF @StartDT < SYSDATETIME()
        BEGIN
            raiserror( 'Appointment cannot be scheduled in the past.',16, 1);
        END

        -- Weekend guard 
        IF DATENAME(WEEKDAY, @ScheduleDate) IN ('Saturday', 'Sunday')
        BEGIN
            raiserror( 'Appointment cannot be scheduled on weekends.',16, 1);
        END

        -- Overlap check:
        
        IF EXISTS (
            SELECT 1
            FROM ProviderSchedules s
            WHERE s.ProviderID = @ProviderID
              AND s.ScheduleDate = @ScheduleDate            
              AND (
                   
                    DATEADD(MINUTE, DATEDIFF(MINUTE, 0, s.ScheduleTime), CAST(s.ScheduleDate AS DATETIME2(0))) < @EndDT
                AND 
                    DATEADD(MINUTE, s.Duration,
                        DATEADD(MINUTE, DATEDIFF(MINUTE, 0, s.ScheduleTime), CAST(s.ScheduleDate AS DATETIME2(0)))
                    ) > @StartDT
                  )
        )
        BEGIN
            raiserror('Time conflict detected. Appointment cannot be made.', 16,1);
        END
		
		INSERT INTO ProviderSchedules(ScheduleDate, ScheduleTime, Duration, ScheduleStatus, PatientID, ProviderID, ResourceID, LocationID) 
		VALUES(@ScheduleDate, @ScheduleTime, @Duration, @ScheduleStatus, @PatientID, @ProviderID, @ResourceID, @LocationID);
        

        COMMIT tran
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT>0 ROLLBACK tran;
        THROW; 
    END CATCH
END
GO

-- deleting an appointment
create procedure deleteSchedule @ProviderScheduleID int
as
begin
delete from providerSchedules where ProviderScheduleID=@ProviderScheduleID
end
go

-- reading all the schedules
create procedure getAllSchedules 
as
begin
select * from providerSchedules
end
go
-- getting schedules in a presentable way
create procedure getAllSchedules_V2
as
begin

select ps.ProviderScheduleID as ScheduleID , ps.ScheduleDate as ScheduleDate, ps.ScheduleTime as ScheduleTime, 
ps.Duration as Duration,
ps.ScheduleStatus as ScheduleStatus, p.FirstName+' '+p.LastName as patientName, 
pr.FirstName+' '+pr.LastName as providerName,
r.resourceName as resourceName,
pl.practiceAddress as AppointmentLocation,
prac.PracticeName as PracticeName
from providerSchedules as ps 
left join patients as p on p.patientID=ps.PatientID
left join providers as pr on pr.providerID=ps.ProviderID
left join resources as r on ps.ResourceID=r.resourceID
left join practiceLocations as pl on pl.LocationID=ps.LocationID
left join practices as prac on prac.PracticeID=pl.practiceID;


end










