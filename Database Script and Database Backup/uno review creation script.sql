USE UNOMVC;

--LEAVE
IF OBJECT_ID (N'DBO.ESS_TA_OD', N'U') IS NOT NULL  
DROP TABLE DBO.ESS_TA_OD;

IF OBJECT_ID (N'DBO.ESS_TA_CO', N'U') IS NOT NULL  
DROP TABLE DBO.ESS_TA_CO;

IF OBJECT_ID (N'DBO.ESS_TA_MA', N'U') IS NOT NULL  
DROP TABLE DBO.ESS_TA_MA;
IF OBJECT_ID (N'DBO.ESS_TA_GP', N'U') IS NOT NULL  
DROP TABLE DBO.ESS_TA_GP;

IF OBJECT_ID (N'DBO.ESS_TA_LA', N'U') IS NOT NULL  
DROP TABLE DBO.ESS_TA_LA;

IF OBJECT_ID (N'DBO.ESS_TA_OPT_HO', N'U') IS NOT NULL  
DROP TABLE DBO.ESS_TA_OPT_HO;

---SENTINAL


IF OBJECT_ID (N'DBO.TDAY', N'U') IS NOT NULL  
DROP TABLE DBO.TDAY;

IF OBJECT_ID (N'DBO.TDAY_STATUS', N'U') IS NOT NULL  
DROP TABLE DBO.TDAY_STATUS;

----TEMPUS

---LEAVE

IF OBJECT_ID (N'DBO.TA_LEAVE_SUMMARY', N'U') IS NOT NULL  
DROP TABLE DBO.TA_LEAVE_SUMMARY; 


IF OBJECT_ID (N'DBO.TA_LEAVE_YEAR', N'U') IS NOT NULL  
DROP TABLE DBO.TA_LEAVE_YEAR; 



IF OBJECT_ID (N'DBO.TA_LEAVE_RULE', N'U') IS NOT NULL  
DROP TABLE DBO.TA_LEAVE_RULE; 

IF OBJECT_ID (N'DBO.TA_LEAVE_ENTITIES', N'U') IS NOT NULL  
DROP TABLE DBO.TA_LEAVE_ENTITIES; 

IF OBJECT_ID (N'DBO.TA_Leave_File', N'U') IS NOT NULL  
DROP TABLE DBO.TA_Leave_File; 


IF OBJECT_ID (N'DBO.TA_WKLYOFF', N'U') IS NOT NULL  
DROP TABLE DBO.TA_WKLYOFF; 

----SHIFT


IF OBJECT_ID (N'DBO.TNA_EMPLOYEE_TA_CONFIG', N'U') IS NOT NULL  
DROP TABLE DBO.TNA_EMPLOYEE_TA_CONFIG; 

IF OBJECT_ID (N'DBO.TA_SHIFT_PATTERN', N'U') IS NOT NULL  
DROP TABLE DBO.TA_SHIFT_PATTERN; 

IF OBJECT_ID (N'DBO.TA_SHIFT', N'U') IS NOT NULL  
DROP TABLE DBO.TA_SHIFT; 


---CATEGORY




IF OBJECT_ID (N'DBO.ENT_CATEGORY', N'U') IS NOT NULL  
DROP TABLE DBO.ENT_CATEGORY; 


--RoLE


IF OBJECT_ID (N'DBO.ENT_ROLE_DATA_ACCESS', N'U') IS NOT NULL  
DROP TABLE DBO.ENT_ROLE_DATA_ACCESS; 


IF OBJECT_ID (N'DBO.ENT_User', N'U') IS NOT NULL  
DROP TABLE DBO.ENT_User;  
IF OBJECT_ID (N'DBO.ENT_ROLE_MENU_ACCESS', N'U') IS NOT NULL  
DROP TABLE DBO.ENT_ROLE_MENU_ACCESS;  


IF OBJECT_ID (N'DBO.ENT_ROLE_MASTER', N'U') IS NOT NULL  
DROP TABLE DBO.ENT_ROLE_MASTER;  

--MENU
IF OBJECT_ID (N'DBO.ENT_MENU_MASTER', N'U') IS NOT NULL  
DROP TABLE DBO.ENT_MENU_MASTER;  

IF OBJECT_ID (N'DBO.ENT_SUB_MODULE_MASTER', N'U') IS NOT NULL  
DROP TABLE DBO.ENT_SUB_MODULE_MASTER;

IF OBJECT_ID (N'DBO.ENT_MODULE_MASTER', N'U') IS NOT NULL  
DROP TABLE DBO.ENT_MODULE_MASTER; 



 

--EMPLOYEE


--IF OBJECT_ID (N'DBO.checkhierarchy', N'FN') IS NOT NULL  
--DROP FUNCTION DBO.checkhierarchy; 


IF OBJECT_ID (N'DBO.ENT_HierarchyDef', N'U') IS NOT NULL  
DROP TABLE DBO.ENT_HierarchyDef;  


IF OBJECT_ID (N'DBO.[ENT_EMPLOYEE_FAMILY_DETAILS]', N'U') IS NOT NULL  
DROP TABLE DBO.[ENT_EMPLOYEE_FAMILY_DETAILS]; 

IF OBJECT_ID (N'DBO.ENT_EMPLOYEE_Nomineedetails', N'U') IS NOT NULL  
DROP TABLE DBO.ENT_EMPLOYEE_Nomineedetails; 

IF OBJECT_ID (N'DBO.[ENT_NOMINEE_TYPES]', N'U') IS NOT NULL  
DROP TABLE DBO.[ENT_NOMINEE_TYPES]; 

IF OBJECT_ID (N'DBO.ENT_EMPLOYEE_ADDRESS', N'U') IS NOT NULL  
DROP TABLE DBO.ENT_EMPLOYEE_ADDRESS; 
IF OBJECT_ID (N'DBO.ENT_EMPLOYEE_PERSONAL_DTLS', N'U') IS NOT NULL  
DROP TABLE DBO.ENT_EMPLOYEE_PERSONAL_DTLS; 
IF OBJECT_ID (N'DBO.ENT_EMPLOYEE_DTLS', N'U') IS NOT NULL  
DROP TABLE DBO.ENT_EMPLOYEE_DTLS; 
--HOLIDAY
IF OBJECT_ID (N'DBO.ENT_HOLIDAYLOC', N'U') IS NOT NULL  
DROP TABLE DBO.ENT_HOLIDAYLOC; 
IF OBJECT_ID (N'DBO.ENT_HOLIDAY', N'U') IS NOT NULL  
DROP TABLE DBO.ENT_HOLIDAY; 

--REASON
IF OBJECT_ID (N'DBO.ENT_REASON', N'U') IS NOT NULL  
DROP TABLE DBO.ENT_REASON; 
IF OBJECT_ID (N'DBO.ENT_REASON_TYPE', N'U') IS NOT NULL  
DROP TABLE DBO.ENT_REASON_TYPE;  

--COMMON
IF OBJECT_ID (N'DBO.ENT_ORG_COMMON_ENTITIES', N'U') IS NOT NULL  
DROP TABLE DBO.ENT_ORG_COMMON_ENTITIES;  
IF OBJECT_ID (N'DBO.ENT_ORG_COMMON_TYPES', N'U') IS NOT NULL  
DROP TABLE DBO.ENT_ORG_COMMON_TYPES;  


----COMPANY
IF OBJECT_ID (N'DBO.ENT_COMPANY_DETAILS', N'U') IS NOT NULL  
DROP TABLE DBO.ENT_COMPANY_DETAILS;
IF OBJECT_ID (N'DBO.ENT_COMPANY', N'U') IS NOT NULL  
DROP TABLE DBO.ENT_COMPANY;
IF OBJECT_ID (N'DBO.ENT_COMPANY_ADDRESS_TYPE', N'U') IS NOT NULL  
DROP TABLE DBO.ENT_COMPANY_ADDRESS_TYPE;




	

CREATE TABLE ENT_COMPANY
(
	[COMPANY_ID] int primary key Identity, 
	[COMPANY_CODE] nvarchar(15) unique not null,
	[COMPANY_NAME] [varchar](50) NULL,		
	[COMPANY_CREATEDDATE] [datetime] NULL,
	[COMPANY_CREATEDBY] varchar(10),
	[COMPANY_MODIFIEDDATE] [datetime] NULL,	
	[COMPANY_MODIFIEDBY] varchar(10),
	[COMPANY_ISDELETED] [bit] NULL,
	[COMPANY_DELETEDDATE] [datetime] NULL,
	[COMPANY_DELETEDBY] varchar(10)	,
	[IS_SYNC] [bit] NOT NULL DEFAULT 0
)

CREATE TABLE ENT_COMPANY_ADDRESS_TYPE
(
	ADDRESS_TYPE_ID int primary key identity,
	ADDRESS_TYPE nvarchar(20) unique not null,
    [IS_SYNC] [bit] NOT NULL DEFAULT 0,
)

CREATE TABLE ENT_COMPANY_DETAILS
(
	COMPANY_ADDRESS_ID int primary key Identity, 
	[COMPANY_ADDRESS] [varchar](150) NULL,
	[COMPANY_CITY] [varchar](20) NULL,
	[COMPANY_PIN] [varchar](10) NULL,
	[COMPANY_PHONE1] [varchar](15) NULL,
	[COMPANY_PHONE2] [varchar](15) NULL,	
	[COMPANY_STATE] [varchar](50) NULL,
	[COMPANY_ISDELETED] [bit] NULL,
	[ADDRESS_TYPE_ID] int foreign key references ENT_COMPANY_ADDRESS_TYPE(ADDRESS_TYPE_ID),
	[COMPANY_ID] [int] foreign key references ENT_COMPANY(COMPANY_ID),
	[IS_SYNC] [bit] NOT NULL DEFAULT 0
)

	
CREATE TABLE [dbo].[ENT_ORG_COMMON_TYPES](
	[COMMON_TYPES_ID] [int] primary key IDENTITY(1,1) NOT NULL,
	[COMMON_TYPES] [nvarchar](15) unique NOT NULL,
	[IS_SYNC] [bit] NOT NULL DEFAULT 0,
	[COMPANY_ID] [int] foreign key references ENT_COMPANY(COMPANY_ID),	
	CONSTRAINT COMMON_TYPES_COMPANY_ID_Composite UNIQUE(COMMON_TYPES,COMPANY_ID)
)

insert into ENT_ORG_COMMON_TYPES(COMMON_TYPES) values('LOCATION'),('GRADE'),('GROUP'),('CATEGORY'),('COMPANY'),('DIVISION'),('DEPARTMENT'),('DESIGNATION'),('EMPLOYEEE')




CREATE TABLE [dbo].[ENT_ORG_COMMON_ENTITIES]
(
	[ID] [int] IDENTITY(1,1) primary key,
	[COMMON_TYPES_ID] int foreign key references ENT_ORG_COMMON_TYPES(COMMON_TYPES_ID),	
	[OCE_ID] [nvarchar](100) NOT NULL,
	[OCE_DESCRIPTION] [nvarchar](50) NULL,
	[OCE_CREATEDDATE] [datetime] NULL,
	[OCE_CREATEDBY] varchar(10),
	[OCE_MODIFIEDDATE] [datetime] NULL,	
	[OCE_MODIFIEDBY] varchar(10),
	[OCE_ISDELETED] [bit] NULL,
	[OCE_DELETEDDATE] [datetime] NULL,
	[OCE_DELETEDBY] varchar(10)	,
	[IS_SYNC] [bit] NOT NULL DEFAULT 0,
	[COMPANY_ID] [int] foreign key references ENT_COMPANY(COMPANY_ID)
	CONSTRAINT OCE_ID_COMPANY_ID_Composite UNIQUE(OCE_ID,COMPANY_ID)
)
	
CREATE TABLE ENT_REASON_TYPE
(
	REASON_TYPE_ID int primary key identity,
	REASON_TYPE nvarchar(5) unique not null,
	REASON_DESC nvarchar(50),
	[IS_SYNC] [bit] NOT NULL DEFAULT 0,
	[COMPANY_ID] [int] foreign key references ENT_COMPANY(COMPANY_ID),	
)

CREATE TABLE ENT_REASON
(
	REASON_ID int primary key identity,
	REASON_CODE nvarchar(5) not null,
	REASON_DESC nvarchar(50),
	[REASON_ISDELETED] [bit] NULL,
	[IS_SYNC] [bit] NOT NULL DEFAULT 0,
	REASON_TYPE_ID [int] foreign key references ENT_REASON_TYPE(REASON_TYPE_ID),
	[COMPANY_ID] [int] foreign key references ENT_COMPANY(COMPANY_ID),	
	CONSTRAINT REASON_CODE_COMPANY_ID_Composite UNIQUE(REASON_CODE,COMPANY_ID)
)

	
CREATE TABLE [dbo].[ENT_HOLIDAY]
(
	[HOLIDAY_ID] int identity(1,1) primary key,
	[HOLIDAY_CODE] [nvarchar](15) NOT NULL,
	[HOLIDAY_DESCRIPTION] [nvarchar](50) NULL,
	[HOLIDAY_TYPE] [nvarchar](2) NULL,
	[HOLIDAY_DATE] [datetime] NULL,
	[HOLIDAY_SWAP] [datetime] NULL,
	[HOLIDAY_CREATEDDATE] [datetime] NULL,
	[HOLIDAY_CREATEDBY] varchar(10),
	[HOLIDAY_MODIFIEDDATE] [datetime] NULL,	
	[HOLIDAY_MODIFIEDBY] varchar(10),
	[HOLIDAY_ISDELETED] [bit] NULL,
	[HOLIDAY_DELETEDDATE] [datetime] NULL,
	[HOLIDAY_DELETEDBY] varchar(10),
    [IS_SYNC] [bit] NOT NULL DEFAULT 0,
	[COMPANY_ID] [int] foreign key references ENT_COMPANY(COMPANY_ID),	
	CONSTRAINT HOLIDAY_CODE_COMPANY_ID_Composite UNIQUE(HOLIDAY_CODE,COMPANY_ID)
)


CREATE TABLE [dbo].[ENT_HOLIDAYLOC]
(
	[HOLIDAYLOC_ID] int identity(1,1) primary key,
	[HOLIDAY_ID] int foreign key references ENT_HOLIDAY(HOLIDAY_ID),
	[IS_OPTIONAL] [bit] NULL,
	[IS_SYNC] [bit] NOT NULL DEFAULT 0,
	[HOLIDAY_LOC_ID] int foreign key references ENT_ORG_COMMON_ENTITIES(ID),
	[COMPANY_ID] [int] foreign key references ENT_COMPANY(COMPANY_ID),	
) 


CREATE TABLE [dbo].[ENT_EMPLOYEE_DTLS]
(
	[EMPLOYEE_ID] int identity(1,1) primary key,
	[EOD_EMPID] [nvarchar](15) NOT NULL,
	[EPD_SALUTATION] [nvarchar](50) NULL,
	[EPD_FIRST_NAME] [nvarchar](50) NULL,
	[EPD_MIDDLE_NAME] [nvarchar](50) NULL,
	[EPD_LAST_NAME] [nvarchar](50) NULL,
	[EPD_PERSO_FLAG] [varchar](5) NULL,
	[EPD_CARD_ID] [nvarchar](15) NULL,
	[EOD_JOINING_DATE] [datetime] NULL,
	[EOD_CONFIRM_DATE] [datetime] NULL,
	[EOD_LEFT_DATE] [datetime] NULL,
	[EOD_LEFT_REASON_ID] [int] foreign key references ENT_REASON(REASON_ID),
	[EOD_COMPANY_ID] int foreign key references ENT_COMPANY(COMPANY_ID),
	[EOD_LOCATION_ID] [int] foreign key references ENT_ORG_COMMON_ENTITIES(ID),
	[EOD_DIVISION_ID] [int] foreign key references ENT_ORG_COMMON_ENTITIES(ID),
	[EOD_DEPARTMENT_ID] [int] foreign key references ENT_ORG_COMMON_ENTITIES(ID),
	[EOD_DESIGNATION_ID] [int] foreign key references ENT_ORG_COMMON_ENTITIES(ID),
	[EOD_CATEGORY_ID] [int] foreign key references ENT_ORG_COMMON_ENTITIES(ID),
	[EOD_GROUP_ID] [int] foreign key references ENT_ORG_COMMON_ENTITIES(ID),
	[EOD_GRADE_ID] [int] foreign key references ENT_ORG_COMMON_ENTITIES(ID),
	[EOD_STATUS] [int] NULL,
	[EOD_ISDELETED] [bit] NULL,
	[EOD_DELETEDDATE] [datetime] NULL,
	[EOD_TYPE] [varchar](30) NULL,
	[EOD_WORKTYPE] [varchar](300) NULL,
	[EOD_CARD_PIN] [varchar](max) NULL,
	[IS_SYNC] [bit] NOT NULL DEFAULT 0,
	[PREVIOUS_EMPLOYEE_ID] [int] foreign key references ENT_EMPLOYEE_DTLS(EMPLOYEE_ID),
	CONSTRAINT EmployeenameComposite UNIQUE(EPD_FIRST_NAME, EPD_MIDDLE_NAME,EPD_LAST_NAME,EOD_ISDELETED),
	CONSTRAINT EmployeeIDComposite UNIQUE(EOD_EMPID,EOD_COMPANY_ID)
)
	
CREATE TABLE [dbo].[ENT_EMPLOYEE_PERSONAL_DTLS]
(
	[EMPLOYEE_ID] int identity(1,1) primary key,
	[EPD_GENDER] [int] NULL,
	[EPD_MARITAL_STATUS] [int] NULL,
	[EPD_DOB] [datetime] NULL,
	[EPD_DateOFMarriage] [datetime] NULL,
	[EPD_RELIGION] [int] NULL,
	[EPD_REFERENCE_ONE] [nvarchar](50) NULL,
	[EPD_REFERENCE_TWO] [nvarchar](50) NULL,
	[EPD_DOMICILE] [nvarchar](50) NULL,
	[EPD_BLOODGROUP] [int] NULL,
	[EPD_EMAIL] [nvarchar](50) NULL,
	[EPD_PAN] [nvarchar](15) NULL,	
	[EPD_PHOTOURL] [nvarchar](max) NULL,
	[EPD_AADHARCARD] [varchar](30) NULL,
	[EPD_UAN] [varchar](30) NULL,
	[EPD_ISDELETED] [bit] NULL,
	[EPD_DELETEDDATE] [datetime] NULL,
	[IS_SYNC] [bit] NOT NULL DEFAULT 0,
	CONSTRAINT PERSONAL_DTLS_EmployeeId foreign key(EMPLOYEE_ID) references ENT_EMPLOYEE_DTLS(EMPLOYEE_ID)
)


	
CREATE TABLE [dbo].[ENT_EMPLOYEE_ADDRESS]
(
	[EMPLOYEE_ADDRESS] int primary key identity,
	[EMPLOYEE_ID] int foreign key references ENT_EMPLOYEE_DTLS(EMPLOYEE_ID),
	[EAD_ADDRESS_TYPE] [nvarchar](20) NOT NULL,
	[EAD_ADDRESS] [nvarchar](200) NULL,
	[EAD_CITY] [nvarchar](50) NULL,
	[EAD_PIN] [nvarchar](10) NULL,
	[EAD_STATE] [nvarchar](50) NULL,
	[EAD_COUNTRY] [nvarchar](50) NULL,
	[EAD_PHONE_ONE] [nvarchar](50) NULL,
	[EAD_PHONE_TWO] [nvarchar](50) NULL,
	[EAD_ISDELETED] [bit] NULL,
	[EAD_DELETEDDATE] [datetime] NULL,
	[IS_SYNC] [bit] NOT NULL DEFAULT 0,
)
	


CREATE TABLE [dbo].[ENT_NOMINEE_TYPES](
	[NOMINEE_TYPE_ID] [int] IDENTITY(1,1) primary key,
	[NOMINEE_TYPE] varchar(50),
    [NOMINEE_ISDELETED] [bit] NOT NULL DEFAULT 0,
	[NOMINEE_DELETEDDATE] [datetime] NULL,
	[IS_SYNC] [bit] NOT NULL DEFAULT 0,
) 

INSERT INTO ENT_NOMINEE_TYPES(NOMINEE_TYPE) VALUES('PF'),('GRATUITY'),('ESIC'),('MEDICLAIM')

CREATE TABLE [dbo].[ENT_EMPLOYEE_Nomineedetails](
	[NOMINEE_DETAIL_ID] [int] IDENTITY(1,1) primary key,
	[EMPLOYEE_ID] int foreign key references ENT_EMPLOYEE_DTLS(EMPLOYEE_ID),
	[Nominee] [varchar](200) NULL,
	[NomineesAddress] [varchar](200) NULL,
	[Relation] [varchar](80) NULL,
	[BirthDate] [datetime] NULL,
	[SharePercent] decimal(12,2) NULL,
	[GuardianAddress] [varchar](200) NULL,
	[IS_SYNC] [bit] NOT NULL DEFAULT 0,
	[NOMINEE_TYPE_ID] int foreign key references ENT_NOMINEE_TYPES(NOMINEE_TYPE_ID)
) 

CREATE TABLE [dbo].[ENT_EMPLOYEE_FAMILY_DETAILS](
	[FAMILY_DETAIL_ID] [int] IDENTITY(1,1) primary key,
	[EMPLOYEE_ID] int foreign key references ENT_EMPLOYEE_DTLS(EMPLOYEE_ID),
	[FIRSTNAME] [varchar](200) NULL,
	[LASTNAME] [varchar](200) NULL,
	[BirthDate] [datetime] NULL,
	[Gender] [varchar](10) NULL,
	[RELATIONTYPE][varchar](2),
	[IS_SYNC] [bit] NOT NULL DEFAULT 0,	
) 


CREATE TABLE [dbo].[ENT_HierarchyDef](
	ENT_HierarchyDef_ID [int] IDENTITY(1,1) primary key,
	[Hier_Emp_ID] int foreign key references ENT_EMPLOYEE_DTLS(EMPLOYEE_ID),
	[Hier_Mgr_ID] int foreign key references ENT_EMPLOYEE_DTLS(EMPLOYEE_ID) null,
	[EOD_DESIGNATION_ID] [int] foreign key references ENT_ORG_COMMON_ENTITIES(ID),
	[Hier_IsDeleted] [bit] NULL,
	[Hier_DeletedDate] [datetime] NULL,
	[Hier_Entity] [varchar](15) NULL,
	[IS_SYNC] [bit] NOT NULL DEFAULT 0,
	CONSTRAINT [ck_hierarchy_eq] CHECK  (([Hier_Mgr_ID]<>[Hier_Emp_ID]))
)
--;
--go
--CREATE FUNCTION [dbo].[checkhierarchy](@t2 int)
--RETURNS int as
--begin
--declare @cc int
--select @cc=count(1) from ENT_HierarchyDef where Hier_Emp_ID=@t2
--return @cc
--end



create table ENT_MODULE_MASTER
(
	MODULE_ID int identity(1,1) primary key,
	MODULE_NAME varchar(50)
)

insert into ENT_MODULE_MASTER values('Core Configuration');
insert into ENT_MODULE_MASTER values('ESS');
insert into ENT_MODULE_MASTER values ('Tempus');
insert into ENT_MODULE_MASTER values ('Sentinal' );
insert into ENT_MODULE_MASTER values('PERSO');
insert into ENT_MODULE_MASTER values('Vehicle Mangement');
insert into ENT_MODULE_MASTER values('Visitor Management');

create table ENT_SUB_MODULE_MASTER
(
	SMODULE_ID int identity(1,1) primary key,
	SMODULE_NAME varchar(50),
	MODULE_ID int foreign key references  ENT_MODULE_MASTER(MODULE_ID)

)
create table ENT_MENU_MASTER
(
	MENU_ID int identity(1,1) primary key,
	MENU_NAME varchar(50),
	MENU_URL varchar(200),
	MENU_IsDeleted [bit] NULL,
	MENU_DeletedDate [datetime] NULL,
	MODULE_ID int foreign key references  ENT_MODULE_MASTER(MODULE_ID),
	SMODULE_ID int foreign key references  ENT_SUB_MODULE_MASTER(SMODULE_ID),
)



create table ENT_ROLE_MASTER
(
	ROLE_ID int identity(1,1) primary key,
	ROLE_NAME varchar(50),
	ROLE_IsDeleted [bit] DEFAULT 0,
	ROLE_DeletedDate [datetime] ,
	[COMPANY_ID] [int] foreign key references ENT_COMPANY(COMPANY_ID),
	[IS_SYNC] [bit] NOT NULL DEFAULT 0,
	CONSTRAINT ROLE_NAME_COMPANY_ID_Composite UNIQUE(ROLE_NAME,COMPANY_ID)
)

INSERT INTO ENT_ROLE_MASTER(ROLE_NAME) VALUES ('SYSADMIN'),('EMPLOYEE')
SELECT * FROM ENT_ROLE_MASTER

create table ENT_ROLE_MENU_ACCESS
(
	ROLE_ACCESS_ID int identity(1,1) primary key,
	ROLE_ADD bit not null,
	ROLE_DELETE bit not null,
	ROLE_EDIT bit not null,
	ROLE_VIEW bit not null,
	ROLE_IsDeleted [bit] NULL,
	ROLE_DeletedDate [datetime] NULL,
	[IS_SYNC] [bit] NOT NULL DEFAULT 0,
	ROLE_ID int foreign key references  ENT_ROLE_MASTER(ROLE_ID),
	MENU_ID int foreign key references  ENT_MENU_MASTER(MENU_ID)
)



CREATE TABLE [dbo].[ENT_User](
	[USER_ID] INT PRIMARY KEY IDENTITY,
	USER_CODE [nvarchar](15) NOT NULL,	
	[Password] [varchar](100) NULL,
	ROLE_ID [int] foreign key references ENT_ROLE_MASTER(ROLE_ID),
	[EssEnabled] [bit] NULL,	
	[PASSWORD_RESET] [bit] NULL,
	[USER_CREATEDDATE] [datetime] NULL,
	[USER_CREATEDBY] varchar(10),
	[USER_MODIFIEDDATE] [datetime] NULL,	
	[USER_MODIFIEDBY] varchar(10),
	[USER_ISDELETED] [bit] NULL,
	[USER_DELETEDDATE] [datetime] NULL,
	[USER_DELETEDBY] varchar(10),
	[IS_SYNC] [bit] NOT NULL DEFAULT 0,
	[COMPANY_ID] [int] foreign key references ENT_COMPANY(COMPANY_ID),	
	CONSTRAINT USERCODE_COMPANYID_Composite UNIQUE(USER_CODE,COMPANY_ID)
	)

	INSERT INTO ENT_USER(USER_CODE,[Password],ROLE_ID,EssEnabled,PASSWORD_RESET) values('superuser','123456',1,0,0)

	select * from ent_user

CREATE TABLE ENT_ROLE_DATA_ACCESS
(
	DATA_ACCESS_ID INT PRIMARY KEY IDENTITY,
	USER_CODE int foreign key references ENT_User([USER_ID]),
	[COMMON_TYPES_ID] int foreign key references ENT_ORG_COMMON_TYPES(COMMON_TYPES_ID),	
	[MAPPED_ENTITY_ID] INT,
	[IS_SYNC] [bit] NOT NULL DEFAULT 0,
)





----------------------------TEMPUS

----Category

CREATE TABLE [dbo].[ENT_CATEGORY](
	[CATEGORY_ID] [int] IDENTITY(1,1) primary key,
	[ORG_CATEGORY_ID] [int] foreign key references ENT_ORG_COMMON_ENTITIES(ID),
	[EARLY_GOING] [datetime] NULL,
	[LATE_COMING] [datetime] NULL,
	[EXTRA_CHECK] [bit] NULL,
	[EXHRS_BEFORE_SHIFT_HRS] [datetime] NULL,
	[EXHRS_AFTER_SHIFT_HRS] [datetime] NULL,
	[COMPENSATORYOFF_CODE] [nvarchar](16) NULL,
	[DED_FROM_EXHRS_EARLY_GOING] [bit] NULL,
	[DED_FROM_EXHRS_LATE_COMING] [bit] NULL,
	[CREATEDDATE] [datetime] NULL,
	[CREATEDBY] varchar(10),
	[MODIFIEDDATE] [datetime] NULL,	
	[MODIFIEDBY] varchar(10),
	[ISDELETED] [bit] NULL,
	[DELETEDDATE] [datetime] NULL,
	[DELETEDBY] varchar(10)	,
	[IS_SYNC] [bit] NOT NULL DEFAULT 0,
)



---SHIFT
CREATE TABLE [dbo].[TA_SHIFT](
	[SHIFT_ID] [int] IDENTITY(1,1) PRIMARY KEY,
	[SHIFT_CODE] [nvarchar](15) NOT NULL,
	[SHIFT_DESCRIPTION] [nvarchar](50) NULL,
	[SHIFT_ALLOCATION_TYPE] [nvarchar](50) NULL,
	[SHIFT_AUTO_SEARCH_START] [datetime] NULL,
	[SHIFT_AUTO_SEARCH_END] [datetime] NULL,
	[SHIFT_TYPE] [nvarchar](10) NULL,
	[SHIFT_START] [datetime] NULL,
	[SHIFT_END] [datetime] NULL,
	[SHIFT_BREAK_START] [datetime] NULL,
	[SHIFT_BREAK_END] [datetime] NULL,
	[SHIFT_BREAK_HRS] [datetime] NULL,
	[SHIFT_WORKHRS] [datetime] NULL,
	[SHIFT_FLAG_ADD_BREAK] [bit] NULL,
	[SHIFT_WEEKEND_DIFF_TIME] [bit] NULL,
	[SHIFT_WEEKEND_START] [datetime] NULL,
	[SHIFT_WEEKEND_END] [datetime] NULL,
	[SHIFT_WEEKEND_BREAK_START] [datetime] NULL,
	[SHIFT_WEEKEND_BREAK_END] [datetime] NULL,
	[SHIFT_EARLY_SEARCH_HRS] [varchar](20) NULL,
	[SHIFT_LATE_SEARCH_HRS] [varchar](20) NULL,
	[SHIFT_ISDELETED] [bit] NULL,
	[SHIFT_DELETEDDATE] [datetime] NULL,
	[SHIFT_START_EARLY_SEARCH_HRS] [datetime] NULL,
	[SHIFT_START_LATE_SEARCH_HRS] [datetime] NULL,
	[SHIFT_START_EARLY_VALID_HRS] [datetime] NULL,
	[SHIFT_START_LATE_VALID_HRS] [datetime] NULL,
	[SHIFT_BREAK_OUT_EARLY_SEARCH_HRS] [datetime] NULL,
	[SHIFT_BREAK_OUT_LATE_SEARCH_HRS] [datetime] NULL,
	[SHIFT_BREAK_OUT_EARLY_VALID_HRS] [datetime] NULL,
	[SHIFT_BREAK_OUT_LATE_VALID_HRS] [datetime] NULL,
	[SHIFT_BREAK_IN_EARLY_SEARCH_HRS] [datetime] NULL,
	[SHIFT_BREAK_IN_LATE_SEARCH_HRS] [datetime] NULL,
	[SHIFT_BREAK_IN_EARLY_VALID_HRS] [datetime] NULL,
	[SHIFT_BREAK_IN_LATE_VALID_HRS] [datetime] NULL,
	[SHIFT_END_EARLY_SEARCH_HRS] [datetime] NULL,
	[SHIFT_END_LATE_SEARCH_HRS] [datetime] NULL,
	[SHIFT_END_EARLY_VALID_HRS] [datetime] NULL,
	[SHIFT_END_LATE_VALID_HRS] [datetime] NULL,
	[SHIFT_BREAK_OUT_IN_MIN_DUR] [datetime] NULL,
	[IS_SYNC] [bit] NOT NULL DEFAULT 0,
	[COMPANY_ID] [int] foreign key references ENT_COMPANY(COMPANY_ID),	
	CONSTRAINT SHIFT_CODE_COMPANYID_Composite UNIQUE(SHIFT_CODE,COMPANY_ID)
	)


	
CREATE TABLE [dbo].[TA_SHIFT_PATTERN](
	[SHIFT_PATTERN_ID] [int] IDENTITY(1,1) PRIMARY KEY,
	[SHIFT_PATTERN_CODE] [nvarchar](15) UNIQUE NOT NULL,
	[SHIFT_PATTERN_DESCRIPTION] [nvarchar](50) NULL,
	[SHIFT_PATTERN_TYPE] [nvarchar](50) NULL,
	[SHIFT_PATTERN] [nvarchar](max) NULL,
	[SHIFT_ISDELETED] [bit] NULL,
	[SHIFT_DELETEDDATE] [datetime] NULL,
	[IS_SYNC] [bit] NOT NULL DEFAULT 0,
)



CREATE TABLE [dbo].[TNA_EMPLOYEE_TA_CONFIG]
(
	[ETC_ROWID] [bigint] IDENTITY(1,1) PRIMARY KEY ,
	[ETC_EMP_ID] int foreign key references ENT_EMPLOYEE_DTLS(EMPLOYEE_ID),
	[ETC_MINIMUM_SWIPE] [nvarchar](5) NULL,
	[ETC_SHIFTCODE] [nvarchar](5) NULL,
	[ETC_WEEKEND] [nvarchar](3) NULL,
	[ETC_WEEKOFF] [nvarchar](3) NULL,
	[ETC_SHIFT_START_DATE] [datetime] NULL,
	[ETC_ISDELETED] [bit] NULL,
	[ETC_DELETEDDATE] [datetime] NULL,
	[ScheduleType] [varchar](255) NULL,
	[ShiftType] [varchar](255) NULL,
	[IS_SYNC] [bit] NOT NULL DEFAULT 0,
	)


	
CREATE TABLE [dbo].[TA_WKLYOFF](
	[MWK_CD] INT PRIMARY KEY IDENTITY,
	[MWK_DAY] [numeric](5, 0) NOT NULL,
	[MWK_OFF] [numeric](5, 0) NOT NULL,
	[MWK_PAT] [varchar](255) NULL,
	[IS_SYNC] [bit] NOT NULL DEFAULT 0,
	[COMPANY_ID] [int] foreign key references ENT_COMPANY(COMPANY_ID),	
	
	)
	
	CREATE TABLE [dbo].[TA_Leave_File]
	(
	[Leave_File_ID] [int] IDENTITY(1,1) PRIMARY KEY,
	[Leave_CODE] [nvarchar](15) NOT NULL,
	[Leave_Description] [nvarchar](50) NULL,
	[Leave_IsPaid] [bit] NULL,
	[Leave_Group] [nvarchar](15) NULL,
	[Leave_ISDELETED] [bit] NULL,
	[Leave_DELETEDDATE] [datetime] NULL,
	[Leave_IsProDataBasiss] [bit] NOT NULL,
	[MAXCARRYFORWARD] INT, ----USER FOR LEAVE YEAR END CALCULATION
	[IS_SYNC] [bit] NOT NULL DEFAULT 0,
	[COMPANY_ID] [int] foreign key references ENT_COMPANY(COMPANY_ID),	
	CONSTRAINT Leave_CODE_COMPANYID_Composite UNIQUE(Leave_CODE,COMPANY_ID)
)



CREATE TABLE TA_LEAVE_ENTITIES
(
	LEAVE_ENTITIES_ID INT PRIMARY KEY IDENTITY,
	LEAVE_ENTITIES_CODE NVARCHAR(200),	
)

INSERT INTO TA_LEAVE_ENTITIES(LEAVE_ENTITIES_CODE) VALUES ('MAX'),('MIN'),('PREFIX'),('SUFFIX'),('COUNT'),('PREAPPLY'),('POSTAPPLY'),('COMBINED')
----PROVISIONING TO BE MADE FOR MATERNITY,PATERNITY
CREATE TABLE [dbo].[TA_LEAVE_RULE](
	[LEAVE_RULE_ID] [int] IDENTITY(1,1) PRIMARY KEY,
	[Leave_File_ID] [int] foreign key references TA_Leave_File(Leave_File_ID),	
	[LEAVE_ENTITIES_ID] [int] foreign key references TA_LEAVE_ENTITIES(LEAVE_ENTITIES_ID),	
	[LEAVE_VALUES] [nvarchar](200) NULL,	
	[LR_ISDELETED] [bit] NULL,
	[LR_DELETEDDATE] [datetime] NULL,
	[IS_SYNC] [bit] NOT NULL DEFAULT 0,
	CONSTRAINT Leave_File_ID_LEAVE_ENTITIES_ID_Composite UNIQUE(Leave_File_ID,LEAVE_ENTITIES_ID)
)

CREATE  TABLE TA_LEAVE_YEAR
(
LEAVE_YEAR_ID INT PRIMARY KEY IDENTITY,
FROMDATE DATETIME,
TODATE DATETIME,
[IS_SYNC] [bit] NOT NULL DEFAULT 0,
[COMPANY_ID] [int] foreign key references ENT_COMPANY(COMPANY_ID),	
[PREVIOUS_LEAVE_YEAR_ID] [int] foreign key references TA_LEAVE_YEAR(LEAVE_YEAR_ID),	
)

CREATE TABLE [dbo].[TA_LEAVE_SUMMARY](
	[LV_ID] [int] IDENTITY(1,1) PRIMARY KEY,
	[LV_EMP_ID] int foreign key references ENT_EMPLOYEE_DTLS(EMPLOYEE_ID),
	[LV_LEAVE_ID] int foreign key references TA_Leave_File(Leave_File_ID),
	[LV_OPENINGBAL] [decimal](12, 2) NULL,
	[LV_ALLOTMENT] [decimal](12, 2) NULL,
	[LV_AVAILABLE] [decimal](12, 2) NULL,
	[LV_AVAILED] [decimal](12, 2) NULL,
	[LV_ENCASHED] [decimal](12, 2) NULL,
	[LV_CUT] [decimal](12, 2) NULL,
	[LV_LAPSED] [decimal](12, 2) NULL,
	[LV_LEAVE_YEAR] int foreign key references TA_LEAVE_YEAR(LEAVE_YEAR_ID),
	[LV_ISDELETED] [bit] NULL,
	[LV_DELETEDDATE] [datetime] NULL,
	[IS_SYNC] [bit] NOT NULL DEFAULT 0,
)


------------------------------------------------------------------

CREATE TABLE [dbo].[TDAY_STATUS](
	[TDAY_STATUS_ID] INT PRIMARY KEY IDENTITY,
	[TDAY_STATUS] [varchar](20) NULL,
	[TDAY_SHORT_CODE] [varchar](50) NULL	
)

insert into tday_status values ('AB', 'ABSENT'), ('PR' , 'PRESENT') , ('WO', 'WEEK OFF'), ('ABW2' ,'AB')


	CREATE TABLE [dbo].[TDAY](
	[TDAY_ID] INT PRIMARY KEY IDENTITY,
	[TDAY_EMPCDE] int foreign key references ENT_EMPLOYEE_DTLS(EMPLOYEE_ID),
	[TDAY_DATE] [datetime] NOT NULL,
	[TDAY_SFTASSG] [varchar](2) NULL,
	[TDAY_SFTREPO] [varchar](2) NULL,
	[TDAY_INTIME] [datetime] NULL,
	[TDAY_OUTIME] [datetime] NULL,
	[TDAY_LUNCH_OUT] [datetime] NULL,
	[TDAY_LUNCH_IN] [datetime] NULL,
	[TDAY_OUDATE] [datetime] NULL,
	[TDAY_EXHR] [datetime] NULL,
	[TDAY_STATUS_ID] int foreign key references TDAY_STATUS(TDAY_STATUS_ID),
	[TDAY_STATUS_ID_FH] int foreign key references TDAY_STATUS(TDAY_STATUS_ID),
	[TDAY_STATUS_ID_SH] int foreign key references TDAY_STATUS(TDAY_STATUS_ID),
	[TDAY_LATE] [datetime] NULL,
	[TDAY_EARLY] [datetime] NULL,
	[TDAY_WRKHR] [datetime] NULL,
	[TDAY_ERLNCH] [decimal](1, 0) NULL,
	[TDAY_LTLNCH] [decimal](1, 0) NULL,
	[TDAY_TOTIN] [varchar](3) NULL,
	[TDAY_TOTOUT] [varchar](3) NULL,
	[TDAY_ENTRY] [varchar](2) NULL,
	[TDAY_LVCUT] [varchar](5) NULL,
	[TDAY_LTOT] [datetime] NULL,
	[TDAY_EROT] [datetime] NULL,
	[TDAY_SHIFT_INDEX] [int] NULL,
	[TDAY_InDATE] [datetime] NULL,
	[TDAY_SHIFT_PATTERN_ID] [varchar](20) NULL,
	[isProcessed] [int] NULL,
	[TDAY_LEAVE_YEAR] int foreign key references TA_LEAVE_YEAR(LEAVE_YEAR_ID)
)



CREATE TABLE [dbo].[ESS_TA_LA](
	[ESS_LA_ID] [bigint] IDENTITY(1,1) primary key,
	[ESS_LA_EMPID] int foreign key references ENT_EMPLOYEE_DTLS(EMPLOYEE_ID),
	[ESS_LA_REQUESTDT] [datetime] NULL,
	[ESS_LA_FROMDT] [datetime] NULL,
	[ESS_LA_TODT] [datetime] NULL,
	[ESS_LA_LVCD] [varchar](15) NOT NULL,
	[ESS_LA_REASON_ID] [int] foreign key references ENT_REASON(REASON_ID),
	[ESS_LA_REMARK] [varchar](50) NULL,
	[ESS_LA_SANCID] [varchar](15) NULL,
	[ESS_LA_SANCDT] [datetime] NULL,
	[ESS_LA_SANC_REMARK] [varchar](50) NULL,
	[ESS_LA_ORDER] [numeric](18, 0) NULL,
	[ESS_LA_STATUS] [varchar](50) NULL,
	[ESS_LA_OLDSTATUS] [varchar](50) NULL,
	[ESS_LA_ISDELETED] [bit] NULL,
	[ESS_LA_DELETEDDATE] [datetime] NULL,
	[ESS_LA_LVDAYS] [numeric](12, 2) NULL,
	[ESS_REQUEST_TYPE] [varchar](10)	
)


CREATE TABLE [dbo].[ESS_TA_MA](
	[ESS_MA_ID] [bigint] IDENTITY(1,1) primary key,
	[ESS_MA_EMPID] int foreign key references ENT_EMPLOYEE_DTLS(EMPLOYEE_ID),
	[ESS_MA_REQUESTDT] [datetime] NULL,
	ESS_MA_FROMDT [datetime] NULL,
	ESS_MA_FROMTM [datetime] NULL,
	ESS_MA_TODT [datetime] NULL,
	ESS_MA_TOTM [datetime] NULL,
	[ESS_MA_REASON_ID] [int] foreign key references ENT_REASON(REASON_ID),
	[ESS_MA_REMARK] [varchar](50) NULL,
	[ESS_SANCTION_ID] int foreign key references ENT_User([USER_ID]),
	[ESS_MA_SANCDT] [datetime] NULL,
	[ESS_MA_SANC_REMARK] [varchar](50) NULL,
	[ESS_MA_ORDER] [numeric](18, 0) NULL,
	[ESS_MA_STATUS] [varchar](50) NULL,
	[ESS_MA_OLDSTATUS] [varchar](50) NULL,
	[ESS_MA_ISDELETED] [bit] NULL,
	[ESS_MA_DELETEDDATE] [datetime] NULL,
	[ESS_MA_LVDAYS] [numeric](12, 2) NULL,
	[ESS_REQUEST_TYPE] [varchar](10)	
)


CREATE TABLE [dbo].[ESS_TA_OD](
	[ESS_OD_ID] [bigint] IDENTITY(1,1) primary key,
	[ESS_OD_EMPID] int foreign key references ENT_EMPLOYEE_DTLS(EMPLOYEE_ID),
	ESS_OD_FROLOC varchar(200) NULL,
	ESS_OD_TOLOC varchar(200) NULL,
	ESS_OD_ODCD varchar(100),
	[ESS_OD_REQUESTDT] [datetime] NULL,
	ESS_OD_FROMDT [datetime] NULL,
	ESS_OD_FROMTM [datetime] NULL,
	ESS_OD_TODT [datetime] NULL,
	ESS_OD_TOTM [datetime] NULL,
	[ESS_OD_REASON_ID] [int] foreign key references ENT_REASON(REASON_ID),
	[ESS_OD_REODRK] [varchar](50) NULL,
	[ESS_SANCTION_ID] int foreign key references ENT_User([USER_ID]),
	[ESS_OD_SANCDT] [datetime] NULL,
	[ESS_OD_SANC_REODRK] [varchar](50) NULL,
	[ESS_OD_ORDER] [numeric](18, 0) NULL,
	[ESS_OD_STATUS] [varchar](50) NULL,
	[ESS_OD_OLDSTATUS] [varchar](50) NULL,
	[ESS_OD_ISDELETED] [bit] NULL,
	[ESS_OD_DELETEDDATE] [datetime] NULL,
	[ESS_OD_LVDAYS] [numeric](12, 2) NULL,
	[ESS_REQUEST_TYPE] [varchar](10)	
)


CREATE TABLE [dbo].[ESS_TA_CO](
	[ESS_CO_ID] [bigint] IDENTITY(1,1) primary key,
	[ESS_CO_EMPID] int foreign key references ENT_EMPLOYEE_DTLS(EMPLOYEE_ID),
	ESS_CO_DAYS numeric(4,2),
	ESS_CO_LEAVEAGANISTDATE [datetime] NULL,
	[ESS_CO_REQUESTDT] [datetime] NULL,
	ESS_CO_DT [datetime] NULL,
	[ESS_CO_REASON_ID] [int] foreign key references ENT_REASON(REASON_ID),
	[ESS_CO_REMARK] [varchar](50) NULL,
	[ESS_SANCTION_ID] int foreign key references ENT_User([USER_ID]),
	[ESS_CO_SANCDT] [datetime] NULL,
	[ESS_CO_SANC_RECORK] [varchar](50) NULL,
	[ESS_CO_STATUS] [varchar](50) NULL,
	[ESS_CO_ISDELETED] [bit] NULL,
	[ESS_CO_DELETEDDATE] [datetime] NULL,
	[ESS_REQUEST_TYPE] [varchar](10)	
)


CREATE TABLE [dbo].[ESS_TA_GP](
	[ESS_GP_ID] [bigint] IDENTITY(1,1) primary key,
	[ESS_GP_EMPID] int foreign key references ENT_EMPLOYEE_DTLS(EMPLOYEE_ID),
	ESS_GP_GPCD varchar(100),
	[ESS_GP_REQUESTDT] [datetime] NULL,
	ESS_GP_FROMDT [datetime] NULL,
	ESS_GP_FROMTM [datetime] NULL,
	ESS_GP_TODT [datetime] NULL,
	ESS_GP_TOTM [datetime] NULL,
	[ESS_GP_REASON_ID] [int] foreign key references ENT_REASON(REASON_ID),
	[ESS_GP_REMARK] [varchar](50) NULL,
	[ESS_SANCTION_ID] int foreign key references ENT_User([USER_ID]),
	[ESS_GP_SANCDT] [datetime] NULL,
	[ESS_GP_SANC_REMARK] [varchar](50) NULL,
	[ESS_GP_STATUS] [varchar](50) NULL,
	[ESS_GP_OLDSTATUS] [varchar](50) NULL,
	[ESS_GP_ISDELETED] [bit] NULL,
	[ESS_GP_DELETEDDATE] [datetime] NULL,
	[ESS_REQUEST_TYPE] [varchar](10)	
)


CREATE TABLE [dbo].[ESS_TA_OPT_HO](
	[ESS_OPT_HO_ID] [bigint] IDENTITY(1,1) primary key,
	[ESS_HO_EMPID] [varchar](15) NULL,
	[ESS_HO_REQ_DATE] [datetime] NULL,
	[ESS_HOLIDAYD_ID] [varchar](50) NULL,
	[ESS_HOLIDAY_DATE] [datetime] NULL,
	[ESS_HO_STATUS] [varchar](3) NULL,
	[ESS_HO_SANCID] [varchar](15) NULL,
	[ESS_HO_SANC_DATE] [datetime] NULL,
	[ESS_REMARK] [varchar](100) NULL,
	[ESS_HO_ISDELETED] [bit] NULL,
	[ESS_HO_DELETEDDATE] [datetime] NULL
) 



-----OD FROM TO LOCATION
--[dbo].[TA_Manual_Att]
--[dbo].[TA_LEAVE_REQ]
--[dbo].[TA_Outpass_Att]
--[dbo].[TA_GATEPASS]
--[dbo].[TA_COMP_OFF_REQ]
--[dbo].[TA_CO_USEDDETAIL]
----------------------------
--[dbo].[ESS_TA_MS]
--[dbo].[ESS_TA_MA]
--[dbo].[ESS_TA_OPT_HO]
--[dbo].[ESS_TA_LA]
--[dbo].[ESS_TA_GP]
--[dbo].[ESS_TA_CO]
--[dbo].[ESS_TA_OD]

------------------------------------TRANSACTION DATABASE---------------------------------------
USE MASTER;
IF DB_ID('unomvc_transaction') IS NOT NULL
BEGIN
    DROP DATABASE unomvc_transaction
END
create database unomvc_transaction
GO
use unomvc_transaction;

IF OBJECT_ID (N'DBO.ACS_Events', N'U') IS NOT NULL  
DROP TABLE DBO.ACS_Events;


IF OBJECT_ID (N'DBO.TASC', N'U') IS NOT NULL  
DROP TABLE DBO.TASC;


CREATE TABLE [dbo].[ACS_Events](
	[Event_ID] [BIGint] IDENTITY(1,1) PRIMARY KEY,
	[Event_Type] [char](2) NULL,
	[Event_Datetime] [datetime] NULL,
	[Event_DayOfWeek]  AS (datename(weekday,CONVERT([datetime],[Event_Datetime],(103)))),
	[Event_Trace] [int] NULL,
	[Event_Employee_Code] [nvarchar](15) NULL,
	[Event_Card_Code] [nvarchar](15) NULL,
	[Event_Reader_ID] [numeric](18, 0) NULL,
	[Event_Controller_ID] [numeric](18, 0) NULL,
	[Event_Status] [numeric](18, 0) NULL,
	[Event_Alarm_Type] [numeric](18, 0) NULL,
	[Event_Alarm_Action] [char](2) NULL,
	[Event_Access_Point_ID] [char](3) NULL,
	[Event_EventCount] [numeric](18, 0) NULL,
	[EVENT_FLAG] [bit] NULL,
	[TASC_FLAG] [bit] NULL,
	[Event_mode] [nvarchar](50) NULL,	
	[LocationId] [varchar](30) NULL,
	[CenterId] [varchar](30) NULL,
	[COMPANY_ID] [int] --foreign key references ENT_COMPANY(COMPANY_ID),	
	)


	CREATE TABLE [dbo].[TASC](
	TASC_ID [BIGint] IDENTITY(1,1) PRIMARY KEY,
	[TAsc_empcode] [varchar](20) NOT NULL,
	[TAsc_terno] [varchar](4) NULL,
	[TAsc_time] [datetime] NULL,
	[TAsc_date] [datetime] NULL,
	[TAsc_Cocode] [varchar](50) NULL,
	[TAsc_Mode] [varchar](50) NULL,
	[Tasc_flag] [decimal](18, 0) NULL,
	[Tasc_swipe] [varchar](50) NULL,
	[TAsc_Rcode] [varchar](50) NULL,
	[Tsc_RDt] [datetime] NULL,
	[Tasc_ErrorCode] [decimal](18, 0) NULL,
	[Event_Trace] [int] NOT NULL,
	[Event_ID] [bigint] NOT NULL
)



