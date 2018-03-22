
CREATE TABLE profile (
  Id int NOT NULL identity(1,1) primary key,
  Email varchar(50) NOT NULL,
  FirstName varchar(256) DEFAULT NULL,
  LastName varchar(256) DEFAULT NULL,
  ProfileName varchar(512) DEFAULT NULL,
  ProfilePic varchar(1024) DEFAULT NULL,
  Location varchar(64) DEFAULT NULL,
  Live bit NOT NULL,
  IsActive bit NOT NULL,
  Status int NOT NULL,
  AccountType int NOT NULL
) ;



CREATE TABLE [user] (
  Id int primary key NOT NULL,
  Email varchar(50) NOT NULL,
  RegistrationIP varchar(50) NOT NULL,
  RegistrationTime datetime NOT NULL,
  EmailVerification varchar(256) DEFAULT NULL,
  PasswordHash varchar(512) DEFAULT NULL,
  Salt varchar(256) DEFAULT NULL,
  Mobile varchar(15) DEFAULT NULL,
  TempMobile varchar(15) DEFAULT NULL,
  MobileVerificationOTP varchar(10) DEFAULT NULL,
  GooglePlus varchar(30) DEFAULT NULL,
  FacebookID varchar(30) DEFAULT NULL,
  KeepMe varchar(256) DEFAULT NULL,
  RecoverType bit DEFAULT NULL,
  RecoverHash varchar(256) DEFAULT NULL,
  RecoverTimeStamp datetime DEFAULT NULL,
  FOREIGN KEY (Id) REFERENCES profile (Id)
) ;






