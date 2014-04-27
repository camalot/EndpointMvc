﻿CREATE TABLE [dbo].[ActionLogs]
(
		[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [PackageId] NVARCHAR(255) NOT NULL, 
    [Version] NVARCHAR(50) NOT NULL, 
    [Action] NVARCHAR(50) NOT NULL, 
    [Timestamp] DATETIME2 NOT NULL DEFAULT GETUTCDATE(), 
    [IPAddress] NVARCHAR(25) NOT NULL, 
    [HostName] NVARCHAR(512) NOT NULL, 
    [OS] NVARCHAR(512) NOT NULL
)