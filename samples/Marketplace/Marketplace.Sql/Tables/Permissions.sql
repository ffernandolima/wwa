-- Copyright 2017 (c) [Denis Da Silva]. All rights reserved.
-- See License.txt in the project root for license information.

CREATE TABLE [Permissions]
(
	Id			INT IDENTITY NOT NULL,
	
	RoleId		INT NOT NULL,
	FeatureId	INT NOT NULL,

	CONSTRAINT PK_Permissions PRIMARY KEY (Id),
	CONSTRAINT UK_Permissions UNIQUE (RoleId, FeatureId),
	CONSTRAINT FK_Permissions_Role FOREIGN KEY (RoleId) REFERENCES Roles (Id),
	CONSTRAINT FK_Permissions_Feature FOREIGN KEY (FeatureId) REFERENCES Features (Id)
)
