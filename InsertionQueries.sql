
INSERT INTO practices (practiceName, taxID)
VALUES
('HealthOne Clinic', 'TX001'),
('CarePlus Center', 'TX002'),
('MediAid Practice', 'TX003'),
('BrightHealth Clinic', 'TX004'),
('CityCare Medical', 'TX005'),
('GreenValley Health', 'TX006'),
('PrimeLife Practice', 'TX007'),
('UrbanWellness', 'TX008'),
('NorthStar Clinic', 'TX009'),
('EverCare Medical', 'TX010'),
('TruHealth Group', 'TX011'),
('WellSpring Clinic', 'TX012'),
('HealPoint Practice', 'TX013'),
('LifeBridge Health', 'TX014'),
('Nova Medical', 'TX015'),
('MetroHealth', 'TX016'),
('BlueStone Clinic', 'TX017'),
('Skyline Health', 'TX018'),
('Unity Medical', 'TX019'),
('SummitCare', 'TX020');



DELETE FROM practiceLocations;
INSERT INTO practiceLocations (practiceAddress, contactEmail, POS, practiceID)
VALUES
('123 Main St', 'main1@healthone.com', 'P01', 1),
('45 River Rd', 'loc2@careplus.com', 'P02', 2),
('77 High St', 'loc3@mediaid.com', 'P03', 3),
('88 Park Ave', 'loc4@brighthealth.com', 'P04', 4),
('90 Elm St', 'loc5@citycare.com', 'P05', 5),
('91 Pine Rd', 'loc6@greenvalley.com', 'P06', 6),
('92 Oak Ave', 'loc7@primelife.com', 'P07', 7),
('93 Maple Blvd', 'loc8@urbanwellness.com', 'P08', 8),
('94 Cedar Dr', 'loc9@northstar.com', 'P09', 9),
('95 Birch St', 'loc10@evercare.com', 'P10', 10),
('96 Walnut Ln', 'loc11@truhealth.com', 'P11', 11),
('97 Ash Ct', 'loc12@wellspring.com', 'P12', 12),
('98 Willow Rd', 'loc13@healpoint.com', 'P13', 13),
('99 Chestnut St', 'loc14@lifebridge.com', 'P14', 14),
('100 Spruce St', 'loc15@nova.com', 'P15', 15),
('101 Cypress St', 'loc16@metro.com', 'P16', 16),
('102 Palm Dr', 'loc17@bluestone.com', 'P17', 17),
('103 Redwood Blvd', 'loc18@skyline.com', 'P18', 18),
('104 Fir Ln', 'loc19@unity.com', 'P19', 19),
('105 Beech Ave', 'loc20@summit.com', 'P20', 20);



DELETE FROM patients;
INSERT INTO patients (Title, FirstName, LastName, DateOfBirth, Gender, EmailAddress, ContactNumber)
VALUES
('Mr','John','Smith','1985-02-14','Male','john.smith@example.com','03001234567'),
('Ms','Emily','Brown','1990-07-20','Female','emily.brown@example.com','03011234567'),
('Mrs','Sana','Khan','1988-04-10','Female','sana.khan@example.com','03021234567'),
('Mr','Ali','Raza','1975-11-05','Male','ali.raza@example.com','03031234567'),
('Ms','Zainab','Ahmed','1993-09-18','Female','zainab.ahmed@example.com','03041234567'),
('Mr','Ahmad','Malik','1980-01-01','Male','ahmad.malik@example.com','03051234567'),
('Dr','Sara','Imran','1987-06-25','Female','sara.imran@example.com','03061234567'),
('Mr','Bilal','Shah','1989-03-30','Male','bilal.shah@example.com','03071234567'),
('Mrs','Maryam','Ali','1991-12-12','Female','maryam.ali@example.com','03081234567'),
('Mr','Omar','Farooq','1984-08-21','Male','omar.farooq@example.com','03091234567'),
('Mr','Tariq','Mehmood','1978-02-02','Male','tariq.mehmood@example.com','03101234567'),
('Ms','Ayesha','Noor','1995-07-07','Female','ayesha.noor@example.com','03111234567'),
('Mr','Hamza','Iqbal','1992-03-13','Male','hamza.iqbal@example.com','03121234567'),
('Mrs','Hina','Sohail','1986-05-28','Female','hina.sohail@example.com','03131234567'),
('Mr','Usman','Akram','1983-09-09','Male','usman.akram@example.com','03141234567'),
('Mr','Adnan','Qureshi','1977-10-01','Male','adnan.qureshi@example.com','03151234567'),
('Ms','Fatima','Rashid','1997-01-19','Female','fatima.rashid@example.com','03161234567'),
('Mr','Fahad','Anwar','1982-11-23','Male','fahad.anwar@example.com','03171234567'),
('Mr','Saad','Nawaz','1989-06-16','Male','saad.nawaz@example.com','03181234567'),
('Ms','Hafsa','Kamal','1994-04-08','Female','hafsa.kamal@example.com','03191234567');


DELETE FROM providers;
INSERT INTO providers (FirstName, LastName, licenseType, EmailAddress, Specialization)
VALUES
('Dr. Ali', 'Hassan', 'MBBS', 'ali.hassan@clinic.com', 'Cardiology'),
('Dr. Sara', 'Malik', 'MBBS', 'sara.malik@clinic.com', 'Dermatology'),
('Dr. Kamran', 'Rafiq', 'FCPS', 'kamran.rafiq@clinic.com', 'Neurology'),
('Dr. Aisha', 'Naz', 'MBBS', 'aisha.naz@clinic.com', 'Pediatrics'),
('Dr. Bilal', 'Usman', 'MBBS', 'bilal.usman@clinic.com', 'Orthopedics'),
('Dr. Hina', 'Qureshi', 'FCPS', 'hina.qureshi@clinic.com', 'Radiology'),
('Dr. Imran', 'Farid', 'MBBS', 'imran.farid@clinic.com', 'ENT'),
('Dr. Maria', 'Khalid', 'MBBS', 'maria.khalid@clinic.com', 'Gynecology'),
('Dr. Raza', 'Ahmed', 'MBBS', 'raza.ahmed@clinic.com', 'Cardiology'),
('Dr. Uzma', 'Siddiq', 'FCPS', 'uzma.siddiq@clinic.com', 'Pulmonology'),
('Dr. Faisal', 'Khan', 'MBBS', 'faisal.khan@clinic.com', 'Neurology'),
('Dr. Nida', 'Aslam', 'MBBS', 'nida.aslam@clinic.com', 'Dermatology'),
('Dr. Khalid', 'Rauf', 'FCPS', 'khalid.rauf@clinic.com', 'Cardiology'),
('Dr. Maryam', 'Riaz', 'MBBS', 'maryam.riaz@clinic.com', 'ENT'),
('Dr. Zafar', 'Iqbal', 'MBBS', 'zafar.iqbal@clinic.com', 'Radiology'),
('Dr. Ahmed', 'Nawaz', 'MBBS', 'ahmed.nawaz@clinic.com', 'Orthopedics'),
('Dr. Saima', 'Akhtar', 'FCPS', 'saima.akhtar@clinic.com', 'Pediatrics'),
('Dr. Rehan', 'Malik', 'MBBS', 'rehan.malik@clinic.com', 'Cardiology'),
('Dr. Sana', 'Rafiq', 'MBBS', 'sana.rafiq@clinic.com', 'Neurology'),
('Dr. Talha', 'Iftikhar', 'MBBS', 'talha.iftikhar@clinic.com', 'Gynecology');



DELETE FROM resources;
INSERT INTO resources (resourceName)
VALUES
('Room 101'),('Room 102'),('Room 103'),('Room 104'),('Room 105'),
('Room 106'),('Room 107'),('Room 108'),('Room 109'),('Room 110'),
('MRI Machine'),('Ultrasound 1'),('Ultrasound 2'),('X-Ray 1'),('ECG 1'),
('ECG 2'),('Lab A'),('Lab B'),('Operation Theatre 1'),('Operation Theatre 2');




DELETE FROM providerSchedules;
INSERT INTO providerSchedules (ScheduleDate, ScheduleTime, Duration, ScheduleStatus, PatientID, ProviderID, ResourceID, LocationID)
VALUES
('2025-10-05','09:00',30,1,1,1,1,1),
('2025-10-05','09:45',30,1,2,2,2,2),
('2025-10-05','10:30',45,0,3,3,3,3),
('2025-10-06','11:00',30,1,4,4,4,4),
('2025-10-06','11:45',30,1,5,5,5,5),
('2025-10-06','12:30',30,0,6,6,6,6),
('2025-10-07','09:00',30,1,7,7,7,7),
('2025-10-07','09:45',30,0,8,8,8,8),
('2025-10-07','10:30',30,1,9,9,9,9),
('2025-10-07','11:15',30,1,10,10,10,10),
('2025-10-07','12:00',30,0,11,11,11,11),
('2025-10-07','12:45',30,1,12,12,12,12),
('2025-10-07','13:30',30,1,13,13,13,13),
('2025-10-07','14:15',30,1,14,14,14,14),
('2025-10-07','15:00',30,1,15,15,15,15),
('2025-10-07','15:45',30,1,16,16,16,16),
('2025-10-07','16:30',30,0,17,17,17,17),
('2025-10-07','17:15',30,1,18,18,18,18),
('2025-10-07','18:00',30,1,19,19,19,19),
('2025-10-07','18:45',30,0,20,20,20,20);




exec getAllPa
go

-- a view is called a virtual table in sql. it does not store the table data
-- it stores the sql query that is used to generate that data
create view [my patients] as
select * from patients where patientID<10
go
select * from [my patients]
go


