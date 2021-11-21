using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Wordz.DB.Accessors
{
    public static class CurrencyRelatedDataAccessor
    {
        public static Wordz.BE.Currency [] GetCurrencies()
        {
            Database database = null;
            var currencyList = new List<Wordz.BE.Currency>();
            try
            {
                database = Database.GetInstance();

                var command = database.pr_currency_lst;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new Wordz.BE.Currency
                                       {
                                           Id = reader.GetValueOrDefault<int>("id"),
                                           DigitCode = reader.GetValueOrDefault<int>("digit_code"),
                                           LetterCode = reader.GetValueOrDefault<string>("letter_code"),
                                           Name = reader.GetValueOrDefault<string>("name")
                                       };
                        currencyList.Add(item);
                    }
                }
            }
            finally
            {
                database.CloseConnection();
            }

            return currencyList.ToArray();
        }
    }
}
