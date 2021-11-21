/*
Deployment script for wordzDEV
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, NUMERIC_ROUNDABORT, QUOTED_IDENTIFIER OFF;


GO
:setvar Path1 "F:\Test\LearnWords\Src\Wordz\Wordz.Database\"
:setvar Path2 "F:\Test\LearnWords\Src\Wordz\Wordz.Database\"
:setvar DatabaseName "wordzDEV"
:setvar DefaultDataPath "C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\"
:setvar DefaultLogPath "C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\"

GO
:on error exit
GO
USE [master]
GO
IF (DB_ID(N'$(DatabaseName)') IS NOT NULL
    AND DATABASEPROPERTYEX(N'$(DatabaseName)','Status') <> N'ONLINE')
BEGIN
    RAISERROR(N'The state of the target database, %s, is not set to ONLINE. To deploy to this database, its state must be set to ONLINE.', 16, 127,N'$(DatabaseName)') WITH NOWAIT
    RETURN
END

GO

IF NOT EXISTS (SELECT 1 FROM [master].[dbo].[sysdatabases] WHERE [name] = N'$(DatabaseName)')
BEGIN
    RAISERROR(N'You cannot deploy this update script to target BERKPC. The database for which this script was built, wordzDEV, does not exist on this server.', 16, 127) WITH NOWAIT
    RETURN
END

GO

IF (@@servername != 'BERKPC')
BEGIN
    RAISERROR(N'The server name in the build script %s does not match the name of the target server %s. Verify whether your database project settings are correct and whether your build script is up to date.', 16, 127,N'BERKPC',@@servername) WITH NOWAIT
    RETURN
END

GO

IF CAST(DATABASEPROPERTY(N'$(DatabaseName)','IsReadOnly') as bit) = 1
BEGIN
    RAISERROR(N'You cannot deploy this update script because the database for which it was built, %s , is set to READ_ONLY.', 16, 127, N'$(DatabaseName)') WITH NOWAIT
    RETURN
END

GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ENABLE_BROKER 
            WITH ROLLBACK IMMEDIATE;
    END


GO
USE [$(DatabaseName)]
GO
/*
 Pre-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be executed before the build script.	
 Use SQLCMD syntax to include a file in the pre-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the pre-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

GO
PRINT N'Altering [dbo].[pr_split_string]...';


GO
ALTER FUNCTION [dbo].[pr_split_string]
(   
 @String varchar(max)
,@Delimiter char
)
RETURNS @Results table
(
 Ordinal int
,StringValue varchar(max)
)
as
begin

    set @String = isnull(@String,'')
    set @Delimiter = isnull(@Delimiter,'')

    declare
     @TempString varchar(max) = @String
    ,@Ordinal int = 0
    ,@CharIndex int = 0

    set @CharIndex = charindex(@Delimiter, @TempString)
    while @CharIndex != 0 begin     
        set @Ordinal += 1       
        insert @Results values
        (
         @Ordinal
        ,substring(@TempString, 0, @CharIndex)
        )       
        set @TempString = substring(@TempString, @CharIndex + 1, len(@TempString) - @CharIndex)     
        set @CharIndex = charindex(@Delimiter, @TempString)
    end

    if @TempString != '' begin
        set @Ordinal += 1 
        insert @Results values
        (
         @Ordinal
        ,@TempString
        )
    end

    return
end
GO
/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

GO
