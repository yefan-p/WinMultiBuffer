USE master;  
GO

declare @pathMdf nvarchar(max)
declare @pathLog nvarchar(max)
set @pathMdf = null
set @pathLog = null

select @pathMdf = CONVERT(sysname, SERVERPROPERTY('InstanceDefaultDataPath')) + 'MultiBuffer.mdf'
select @pathLog = CONVERT(sysname, SERVERPROPERTY('InstanceDefaultDataPath')) + 'MultiBuffer_log.ldf'

execute(
'CREATE DATABASE MultiBuffer
ON   
( NAME = MultiBuffer,  
    FILENAME =''' + @pathMdf + ''',  
    SIZE = 10,  
    MAXSIZE = UNLIMITED,  
    FILEGROWTH = 5 )  
LOG ON  
( NAME = MultiBuffer_log,  
    FILENAME = ''' + @pathLog + ''',  
    SIZE = 5MB,  
    MAXSIZE = 100MB,  
    FILEGROWTH = 5MB ) ;  
');
GO