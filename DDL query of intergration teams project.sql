-- original work 2
use IntegrationDB_MuhammadSharjeelFarzad
IF OBJECT_ID(N'roles', N'U') IS NULL
BEGIN
create table roles(
roleID int identity(1,1) primary key,
roleName nvarchar(50) not null
)
END
go


IF OBJECT_ID(N'users', N'U') IS NULL
BEGIN
create table users(
UserID int identity(1,1) primary key,
emailAddress nvarchar(100) unique not null,
Username nvarchar(100) not null,
userPassword nvarchar(200) not null,
RoleID int not null foreign key references roles(roleID)
)
END
go

drop table users


IF OBJECT_ID(N'practices', N'U') IS NULL
BEGIN
create table practices(
PracticeID int identity(1,1) primary key,
practiceName nvarchar(100) not null,
taxID NVARCHAR(30) NULL
)
end
go

IF OBJECT_ID(N'practiceLocations', N'U') IS NULL
BEGIN
create table practiceLocations(
LocationID int identity(1,1) primary key,
practiceAddress nvarchar(100) not null,
contactEmail nvarchar(100),
POS nvarchar(10),
practiceID int not null foreign key references practices(PracticeID)
)
end
go

If object_id(N'patients',N'U') is null
begin
create table patients(
patientID int Identity(1,1) primary key,
Title NVARCHAR(20) NULL,
FirstName NVARCHAR(50) NOT NULL,
LastName NVARCHAR(50) NOT NULL,
DateOfBirth DATE NOT NULL,
Gender NVARCHAR(10) NULL,
EmailAddress NVARCHAR(100) NULL,
ContactNumber NVARCHAR(20) NULL
)
end
go
-- added unique index
create unique index UX_patients_EmailAddress on patients(EmailAddress) where EmailAddress is not null 


If object_id(N'providers',N'U') is null
begin
create table providers(
providerID int Identity(1,1) primary key,
FirstName NVARCHAR(50) NOT NULL,
LastName NVARCHAR(50) NOT NULL,
licenseType nvarchar(30) NOT NULL,
EmailAddress NVARCHAR(100) NULL,
Specialization nvarchar(30) Not null
)
end
go
-- added unique index
If object_id(N'UX_providers_EmailAddress',N'I') is null
begin
create unique index UX_providers_EmailAddress on providers(EmailAddress) where EmailAddress is not null 
end
go


If object_id(N'resources',N'U') is null
begin
create table resources(
resourceID int identity(1,1) primary key,
resourceName nvarchar(100) not null
)
end
go
--alter table resources
--add LocationID int foreign key references practiceLocations(LocationID);

If object_id(N'providerSchedules',N'U') is null
begin
create table providerSchedules(
ProviderScheduleID INT IDENTITY(1,1) PRIMARY KEY, -- PK
ScheduleDate DATE NOT NULL,
ScheduleTime TIME NOT NULL,
Duration INT NOT NULL,  
ScheduleStatus BIT NOT NULL DEFAULT 0,
PatientID int not null foreign key references patients(patientID),
ProviderID INT NOT NULL foreign key references providers(providerID),
ResourceID INT NULL foreign key references resources(resourceID),
LocationID INT NULL foreign key references practiceLocations(LocationID)
)
end
go




--very very slow

--select ans_1.scheduleID, ans_1.patientName, ans_1.providerName, ans_2.practiceLocation, ans_2.practiceName from
--(select x.scheduleID as scheduleID, x.patientName as patientName, y.providerName as providerName
--from
--(select ps.ProviderScheduleID as scheduleID, p.FirstName+' '+ p.LastName as patientName
--from providerSchedules as ps left join patients as p on ps.patientID=p.patientID)as x

--left join

--(select ps.ProviderScheduleID as scheduleID, pr.FirstName +' '+ pr.LastName as providerName
--from providerSchedules as ps left join providers as pr on ps.providerID=pr.providerID)as y
-- on x.scheduleID = y.scheduleID) as ans_1
-- left join
--( select a.scheduleID as scheduleID, a.res as res, b.practiceLocation as practiceLocation, b.practiceName as practiceName  from
-- (select ps.ProviderScheduleID as scheduleID, r.resourceName as res
--from providerSchedules as ps left join resources as r on ps.resourceID=r.resourceID) as a

--left join
-- (select ps.ProviderScheduleID as scheduleID, pl.practiceAddress as practiceLocation, pr.practiceName as practiceName
--from providerSchedules as ps left join practiceLocations as pl on ps.LocationID=pl.LocationID left join
--practices as pr on pr.practiceID=pl.practiceID
--) b on a.scheduleID=b.scheduleID) as ans_2 on ans_1.scheduleID=ans_2.scheduleID


use IntegrationDB_MuhammadSharjeelFarzad


select * from users

exec getAllPractices
select * from practices where PracticeID=1;