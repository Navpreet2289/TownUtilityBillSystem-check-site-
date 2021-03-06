--USE master
--GO

-- Drop the database if it already exists
--IF  EXISTS (
--	SELECT name 
--		FROM sys.databases 
--		WHERE name = N'<TOWN_UTILITY_BILL_SYSTEM>'
--)
--DROP DATABASE TOWN_UTILITY_BILL_SYSTEM
--GO

--CREATE DATABASE TOWN_UTILITY_BILL_SYSTEM
--GO

USE TOWN_UTILITY_BILL_SYSTEM
GO

CREATE TABLE dbo.UNIT
(
	ID int IDENTITY NOT NULL,
	NAME VARCHAR(50) NOT NULL,
	CONSTRAINT PK_UNIT_table PRIMARY KEY (ID)
)


CREATE TABLE dbo.TOWN
(
	ID int IDENTITY NOT NULL,
	NAME VARCHAR(50) NOT NULL,
	CONSTRAINT PK_TOWN_table PRIMARY KEY (ID)
)

CREATE TABLE dbo.[INDEX]
(
	ID int IDENTITY NOT NULL,
	VALUE int NOT  NULL,
	CONSTRAINT PK_INDEX_table PRIMARY KEY (ID)
)

CREATE TABLE dbo.STREET
(
	ID int IDENTITY NOT NULL,
	NAME VARCHAR(50) NOT NULL,
	TOWN_ID int NOT  NULL,
	INDEX_ID int NOT  NULL,
	CONSTRAINT PK_STREET_table PRIMARY KEY (ID),
	CONSTRAINT FK_STREET_TOWN FOREIGN KEY (TOWN_ID) REFERENCES TOWN(ID),
	CONSTRAINT FK_STREET_INDEX FOREIGN KEY (INDEX_ID) REFERENCES [INDEX](ID)
)

CREATE TABLE dbo.[IMAGEBUILDING]
(
	ID int IDENTITY NOT NULL,
	[PATH] NVARCHAR(MAX) NOT NULL,
	CONSTRAINT PK_IMAGEBUILDING_table PRIMARY KEY (ID)
)

CREATE TABLE dbo.[IMAGEUTILITY]
(
	ID int IDENTITY NOT NULL,
	[PATH] NVARCHAR(MAX) NOT NULL,
	CONSTRAINT PK_IMAGEUTILITY_table PRIMARY KEY (ID)
)


CREATE TABLE dbo.UTILITY
(
	ID int IDENTITY NOT NULL,
	NAME VARCHAR(50) NOT NULL,
	PRICE decimal(18,2) NOT NULL,
	USAGEFORSTANDARTPRICE decimal(18,2) NULL,
	BIGUSAGEPRICE decimal(18,2) NULL,
	UNIT_ID int NOT  NULL,
	IMAGE_ID int NOT NULL,
	CONSTRAINT PK_UTILITY_table PRIMARY KEY (ID),
	CONSTRAINT FK_UTILITY_UNIT FOREIGN KEY (UNIT_ID) REFERENCES UNIT(ID),
	CONSTRAINT FK_UTILITY_IMAGEUTILITY FOREIGN KEY (IMAGE_ID) REFERENCES [IMAGEUTILITY](ID)
)



CREATE TABLE dbo.BUILDING
(
	ID int IDENTITY NOT NULL,	
	NUMBER VARCHAR(20) NOT  NULL,
	[SQUARE] float(2) NULL,
	STREET_ID int NOT  NULL,	
	IMAGE_ID int NULL,
	CONSTRAINT PK_BUILDING_table PRIMARY KEY (ID),
	CONSTRAINT FK_BUILDING_STREET FOREIGN KEY (STREET_ID) REFERENCES STREET(ID),
	CONSTRAINT FK_BUILDING_IMAGEBUILDING FOREIGN KEY (IMAGE_ID) REFERENCES [IMAGEBUILDING](ID)
)

CREATE TABLE dbo.FLAT_PART
(
	ID int IDENTITY NOT NULL,
	NUMBER int NULL,
	NAME VARCHAR(20) NULL,
  [SQUARE] float(2) NULL,
	BUILDING_ID int NOT  NULL,	
	CONSTRAINT PK_FLAT_table PRIMARY KEY (ID),
	CONSTRAINT FK_FLAT_BUILDING FOREIGN KEY (BUILDING_ID) REFERENCES BUILDING(ID)
)


CREATE TABLE dbo.[ADDRESS]
(
	ID int IDENTITY NOT NULL,
	INDEX_ID int NOT  NULL,
	TOWN_ID int NOT  NULL,
	STREET_ID int NOT  NULL,
	BUILDING_ID int NOT  NULL,
	FLAT_PART_ID int NULL,
	CONSTRAINT PK_ADDRESS_table PRIMARY KEY (ID),
	CONSTRAINT FK_ADDRESS_INDEX FOREIGN KEY (INDEX_ID) REFERENCES [INDEX](ID),
	CONSTRAINT FK_ADDRESS_TOWN FOREIGN KEY (TOWN_ID) REFERENCES TOWN(ID),
	CONSTRAINT FK_ADDRESS_STREET FOREIGN KEY (STREET_ID) REFERENCES STREET(ID),
	CONSTRAINT FK_ADDRESS_BUILDING FOREIGN KEY (BUILDING_ID) REFERENCES BUILDING(ID),
	CONSTRAINT FK_ADDRESS_FLAT_PART FOREIGN KEY (FLAT_PART_ID) REFERENCES FLAT_PART(ID)
)


CREATE TABLE dbo.TEMPERATURE
(
	ID int IDENTITY NOT NULL,
	[DATE] date NOT NULL,
	TOWN_ID int NOT  NULL,
	MINVALUE int NOT  NULL,
    MAXVALUE int NOT  NULL,
	CONSTRAINT PK_TEMPERATURE_table PRIMARY KEY (ID),
	CONSTRAINT FK_TEMPERATURE_TOWN FOREIGN KEY (TOWN_ID) REFERENCES TOWN(ID),
)
CREATE TABLE dbo.CUSTOMER_TYPE
(
	ID int IDENTITY NOT NULL,
	NAME VARCHAR(50) NOT NULL,
	CONSTRAINT PK_CUSTOMER_TYPE_table PRIMARY KEY (ID)
)

CREATE TABLE dbo.CUSTOMER
(
	ID int IDENTITY NOT NULL,
	ACCOUNT VARCHAR(20) NOT NULL,
	SURNAME VARCHAR(50) NULL,
	NAME VARCHAR(50) NOT NULL,
	EMAIL VARCHAR(50) NOT NULL,
	PHONE VARCHAR(50) NOT NULL,	
	ADDRESS_ID int NOT NULL,	
	CUSTOMER_TYPE_ID int NOT NULL,
	CONSTRAINT PK_CUSTOMER_table PRIMARY KEY (ID),
	CONSTRAINT FK_CUSTOMER_ADDRESS FOREIGN KEY (ADDRESS_ID) REFERENCES [ADDRESS](ID),
	CONSTRAINT FK_CUSTOMER_CUSTOMER_TYPE FOREIGN KEY (CUSTOMER_TYPE_ID) REFERENCES CUSTOMER_TYPE(ID)	
)

CREATE TABLE dbo.BILL
(
	ID INT IDENTITY NOT NULL,
	NUMBER VARCHAR(25) NOT NULL,
	CUSTOMER_ID int NOT  NULL,
	[DATE] date NOT NULL,
	PERIOD CHAR(7) NOT NULL,
	[SUM] DECIMAL (18,2) NOT NULL,
	PAID BIT NOT NULL,	
	CONSTRAINT PK_BILL_table PRIMARY KEY (ID),
	CONSTRAINT FK_BILL_CUSTOMER_ID FOREIGN KEY (CUSTOMER_ID) REFERENCES CUSTOMER(ID)
)

CREATE TABLE dbo.METER_TYPE
(
	ID INT IDENTITY NOT NULL,
	NAME VARCHAR(50) NOT NULL,
	VARIFICATION_PERIOD_YEARS int NOT NULL,
	UTILITY_ID int NOT  NULL,
	CONSTRAINT PK_METER_TYPE_table PRIMARY KEY (ID),
	CONSTRAINT FK_METER_TYPE_UTILITY FOREIGN KEY (UTILITY_ID) REFERENCES UTILITY(ID)	
)

CREATE TABLE dbo.METER
(
	ID INT IDENTITY NOT NULL,
	SERIAL_NUMBER VARCHAR(20) NOT  NULL,
	ADDRESS_ID int NOT  NULL,
	METER_TYPE_ID int NOT NULL,
	RELEASE_DATE DATE NOT NULL,
	VARIFICATION_DATE DATE NOT NULL,
	CONSTRAINT PK_METER_table PRIMARY KEY (ID),
	CONSTRAINT FK_METER_ADDRESS FOREIGN KEY (ADDRESS_ID) REFERENCES [ADDRESS](ID),
	CONSTRAINT FK_METER_METER_TYPE FOREIGN KEY (METER_TYPE_ID) REFERENCES METER_TYPE(ID)
)

CREATE TABLE dbo.METER_ITEM
(
	ID INT IDENTITY NOT NULL,
	METER_ID INT NOT NULL,
	[DATE] DATE NOT NULL,
	VALUE float(2) NOT NULL,
	CONSTRAINT PK_METER_ITEM_table PRIMARY KEY (ID),
	CONSTRAINT FK_METER_ITEM_METER FOREIGN KEY (METER_ID) REFERENCES METER(ID)
)


CREATE TABLE dbo.[IMAGENEWS]
(
	ID int IDENTITY NOT NULL,
	[PATH] NVARCHAR(MAX) NOT NULL,
	CONSTRAINT PK_IMAGENEWS_table PRIMARY KEY (ID)
)


CREATE TABLE dbo.NEWS
(
	ID int IDENTITY NOT NULL,
	TITLE VARCHAR(100) NOT NULL,
	[DATE] DATETIME NOT NULL,
	IMAGE_ID int NULL,
	CONSTRAINT PK_NEWS_table PRIMARY KEY (ID),	
	CONSTRAINT FK_NEWS_IMAGENEWS FOREIGN KEY (IMAGE_ID) REFERENCES IMAGENEWS(ID)
)


CREATE TABLE dbo.NEWSCHAPTER
(
	ID int IDENTITY NOT NULL,
	[TEXT] NVARCHAR(MAX) NOT NULL,
	NEWS_ID int NOT  NULL,
	CONSTRAINT PK_NEWSCHAPTER_table PRIMARY KEY (ID),
	CONSTRAINT FK_NEWSCHAPTER_NEWS FOREIGN KEY (NEWS_ID) REFERENCES NEWS(ID)
)

