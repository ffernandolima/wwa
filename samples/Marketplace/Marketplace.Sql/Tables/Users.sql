-- Copyright 2017 (c) [Denis Da Silva]. All rights reserved.
-- See License.txt in the project root for license information.

CREATE TABLE Users
(
	Id		INT IDENTITY NOT NULL,
	Active	BIT NOT NULL DEFAULT 1,

	[Name]			VARCHAR(100) NOT NULL DEFAULT '',
	Email			VARCHAR(50) NOT NULL DEFAULT '',
	
	RoleId			INT NOT NULL,
	IdentityId		NVARCHAR (128),

	CONSTRAINT PK_Users PRIMARY KEY (Id),
	CONSTRAINT FK_Users_Roles FOREIGN KEY (RoleId) REFERENCES Roles (Id),
)
GO

CREATE UNIQUE NONCLUSTERED INDEX IDX_Users_IdentityId
	ON Users (IdentityId) 
	WHERE IdentityId IS NOT NULL;
GO

