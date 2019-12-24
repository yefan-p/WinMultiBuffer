create table tblClipboards
(
	id int identity(1, 1),
	idUser int not null,
	intCopyKeyCode int not null,
	intPasteKeyCode int not null,
	nvcName nvarchar(50),
	nvcValue nvarchar(max),
);

create table tblUsers
(
	id int identity(1, 1),
	nvcName nvarchar(50) not null,
);

ALTER TABLE tblClipboards 
ADD CONSTRAINT PKtblClipboardsId
PRIMARY KEY CLUSTERED (id)
GO

ALTER TABLE tblUsers 
ADD CONSTRAINT PKtblUsersId
PRIMARY KEY CLUSTERED (id)
GO

ALTER TABLE tblClipboards 
WITH CHECK ADD CONSTRAINT FKtblClipboardsTblUsersIdUser
FOREIGN KEY(idUser) 
REFERENCES tblUsers (id) 
GO
