ALTER DATABASE [$(DatabaseName)]
    ADD FILE (NAME = [wordz_Data], FILENAME = '$(Path2)$(DatabaseName).mdf', SIZE = 69632 KB, FILEGROWTH = 10240 KB) TO FILEGROUP [PRIMARY];

