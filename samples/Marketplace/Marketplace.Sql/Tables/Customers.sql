-- Copyright 2017 (c) [Denis Da Silva]. All rights reserved.
-- See License.txt in the project root for license information.

CREATE TABLE Customers
(
	Id		INT IDENTITY NOT NULL,
	Active	BIT NOT NULL DEFAULT 1,

	[Name]		VARCHAR(100) NOT NULL DEFAULT '',
	PhoneNumber	VARCHAR(20) NOT NULL DEFAULT '',

	[Address]	VARCHAR(200) NOT NULL DEFAULT '',
	City		VARCHAR(100) NOT NULL DEFAULT '',
	ZipCode		VARCHAR(20) NOT NULL DEFAULT '',

	ProvinceId INT NOT NULL,
	CountryId INT NOT NULL,

	UserId INT,
	
	CONSTRAINT PK_Customers PRIMARY KEY (Id),
	CONSTRAINT UK_Customers UNIQUE ([Name]),
	CONSTRAINT FK_Customers_Countries FOREIGN KEY (CountryId) REFERENCES Countries (Id),
	CONSTRAINT FK_Customers_Provinces FOREIGN KEY (ProvinceId) REFERENCES Provinces (Id),
	CONSTRAINT FK_Customers_Users FOREIGN KEY (UserId) REFERENCES Users (Id)
)
