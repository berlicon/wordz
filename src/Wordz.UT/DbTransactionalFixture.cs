using System;
using NUnit.Framework;
using Wordz.BC;
using Wordz.BE;
using Wordz.DB;

namespace Wordz.UT {
    public class DbTransactionalFixture {
        protected string word = "abandon";
        protected string word2 = "agent";
        protected string wordWithoutSoundAndOnlyAtEnglish = "zzz";
        protected string wordAbsentInDB = "qkdirhspgkcmкуцекуыивр";
        protected string wordTranslation = "оставлять";
        protected string word2Translation = "агент";
        protected int wordId = 4;
        protected int wordId2 = 363;
        protected int wordIdWithoutSound = 18631;
        protected int verbId = 9;
        protected int verbIdWithoutSound = -1;
        protected int accountId = -1;
        protected string accountNick = "";
        
        private Database database = null;

        public DbTransactionalFixture() {
            Database.AlwaysCreateNewInstance = false;
            database = Database.GetInstance();
        }

        [SetUp]
        public void Setup() {
            database.BeginTransaction();
            CreateTestData();
        }

        [TearDown ]
        public void TearDown() {
//            IDatabaseEx database = (IDatabaseEx)DatabaseRepository.GetDatabase(DOConstants.DatabaseInstance);
//            if(DoRollback)
//                database.RollbackTransaction();
//            else 
//                database.CommitTransaction();
//            SAInfrastructure.EndRequest();

            database.RollbackTransaction();
        }

        private void CreateTestData() {
        }

        protected void CreateNewAccount() {
            accountNick = Guid.NewGuid().ToString().Substring(0, 10);
            accountId = BCCommon.AddAccount(
                new Account(0, accountNick, "", "")).Id;
        }
    }
}