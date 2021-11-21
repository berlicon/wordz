using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Wordz.BE;
using Wordz.BE.Dto;
using System.Data.SqlClient;

namespace Wordz.DB.Accessors
{
    public static class AccountRelatedAccessor
    {
        public static Account GetAccount(string nick, string password)
        {
            Database database = null;
            try
            {
                database = Database.GetInstance();
                Account account = new Account();
                database.pr_account_get.Parameters["@nick"].DbType = DbType.String;
                database.pr_account_get.Parameters["@nick"].Value = nick;
                database.pr_account_get.Parameters["@password"].DbType = DbType.String;
                database.pr_account_get.Parameters["@password"].Value =
                    (password.Length > 0) ? password : null;

                using (SqlDataReader reader = database.pr_account_get.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        account.Id = reader.GetInt32(reader.GetOrdinal("id"));
                        account.Nick = reader.GetString(reader.GetOrdinal("nick"));
                        object emailValue = reader.GetValue(reader.GetOrdinal("email"));
                        account.Email = (emailValue != null) ? emailValue.ToString() : "";
                        object passwordValue = reader.GetValue(reader.GetOrdinal("password"));
                        account.Password = (passwordValue != null) ? passwordValue.ToString() : "";
                        account.IsAdmin = reader.GetValueOrDefault<bool>("is_admin");
                    }
                }
                return account;
            }
            finally
            {
                database.CloseConnection();
            }
        }

        public static Account AddAccount(Account account)
        {
            Database database = null;
            try
            {
                database = Database.GetInstance();
                database.pr_account_ins.Parameters["@nick"].DbType = DbType.String;
                database.pr_account_ins.Parameters["@nick"].Value = account.Nick;
                database.pr_account_ins.Parameters["@email"].DbType = DbType.String;
                database.pr_account_ins.Parameters["@email"].Value =
                    (account.Email.Length > 0) ? account.Email : null;
                database.pr_account_ins.Parameters["@password"].DbType = DbType.String;
                database.pr_account_ins.Parameters["@password"].Value =
                    (account.Password.Length > 0) ? account.Password : null;

                try
                {
                    object accountId = database.pr_account_ins.ExecuteScalar();

                    if (accountId != null)
                    {
                        account.Id = int.Parse(accountId.ToString());
                    }
                    return account;
                }
                catch
                {
                    return account;
                }
            }
            finally
            {
                database.CloseConnection();
            }
        }

        public static bool UpdateAccount(Account account)
        {
            Database database = null;
            try
            {
                database = Database.GetInstance();
                database.pr_account_upd.Parameters["@id"].Value = account.Id;
                database.pr_account_upd.Parameters["@nick"].DbType = DbType.String;
                database.pr_account_upd.Parameters["@nick"].Value = account.Nick;
                database.pr_account_upd.Parameters["@email"].DbType = DbType.String;
                database.pr_account_upd.Parameters["@email"].Value =
                    (account.Email.Length > 0) ? account.Email : null;
                database.pr_account_upd.Parameters["@password"].DbType = DbType.String;
                database.pr_account_upd.Parameters["@password"].Value =
                    (account.Password.Length > 0) ? account.Password : null;

                try
                {
                    database.pr_account_upd.ExecuteNonQuery();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            finally
            {
                database.CloseConnection();
            }
        }

        public static AccountMoneyInfoDto[] GetAccountMoneyInfos(int accountId)
        {
            Database database = null;
            var listOfMoneyInfos = new List<AccountMoneyInfoDto>();
            try
            {
                database = Database.GetInstance();

                var command = database.pr_account_money_balance_lst;
                var commandParams = command.Parameters;

                commandParams["@account_id"].Value = accountId;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var currency = new Currency
                                           {
                                               Id = reader.GetValueOrDefault<int>("currency_id"),
                                               Name = reader.GetValueOrDefault<string>("currency_name"),
                                               LetterCode = reader.GetValueOrDefault<string>("letter_code"),
                                               DigitCode = reader.GetValueOrDefault<int>("digit_code")
                                           };
                        var accountMoneyInfo = new AccountMoneyInfoDto
                                                   {
                                                       Value = reader.GetValueOrDefault<decimal>("value"),
                                                       Currency = currency
                                                   };
                        listOfMoneyInfos.Add(accountMoneyInfo);
                    }
                }
            }
            finally
            {
                database.CloseConnection();
            }

            return listOfMoneyInfos.ToArray();
        }
    }
}
