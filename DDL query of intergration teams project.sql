-- use IntegrationDB_MuhammadSharjeelFarzad
create table roles(
roleID int identity(1,1) primary key,
roleName nvarchar(50) not null
)
go

create table users(
UserID int identity(1,1) primary key,
emailAddress nvarchar(100) unique not null,
Username nvarchar(100) not null,
userPassword varbinary(max) not null,
RoleID int not null foreign key references roles(roleID)
)
go

create table practices(
PracticeID int identity(1,1) primary key,
practiceName nvarchar(100) not null,
taxID NVARCHAR(30) NULL
)
go
create table practiceLocations(
LocationID int identity(1,1) primary key,
practiceAddress nvarchar(100) not null,
contactEmail nvarchar(100),
POS nvarchar(10),
practiceID int not null foreign key references practices(PracticeID)
)
go
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
go

create table providers(
providerID int Identity(1,1) primary key,
FirstName NVARCHAR(50) NOT NULL,
LastName NVARCHAR(50) NOT NULL,
licenseType nvarchar(30) NOT NULL,
EmailAddress NVARCHAR(100) NULL,
Specialization nvarchar(30) Not null
)
go



create table resources(
resourceID int identity(1,1) primary key,
resourceName nvarchar(100) not null
)
go

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
go

