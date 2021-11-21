ALTER DATABASE [$(DatabaseName)]
    ADD LOG FILE (NAME = [wordz_Log], FILENAME = '$(Path1)$(DatabaseName)_Log.LDF', SIZE = 245952 KB, FILEGROWTH = 10 %);

