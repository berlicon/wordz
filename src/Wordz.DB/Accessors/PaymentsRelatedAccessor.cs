using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wordz.BE.Dto;

namespace Wordz.DB.Accessors
{
    public static class PaymentsRelatedAccessor
    {

        public static int AddPaymentForCourse(int accountId, int courseId, int currencyId, DateTime paymentDate, decimal paymentValue)
        {
            Database database = null;
            int? resultId;
            try
            {
                database = Database.GetInstance();
                //Todo: получить курс по id
                var command = database.pr_payment_for_course_add;
                var commandParams = command.Parameters;
                commandParams["@account_id"].Value = accountId;
                commandParams["@course_id"].Value = courseId;
                commandParams["@currency_id"].Value = currencyId;
                commandParams["@payment_date"].Value = paymentDate;
                commandParams["@payment_value"].Value = paymentValue;
                commandParams["@result_id"].Value = 0;
                command.ExecuteNonQuery();
                resultId = commandParams["@result_id"].Value as int?;
            }
            finally
            {
                database.CloseConnection();
            }

            return resultId.HasValue ? resultId.Value : -1;
        }

        public static int AddPaymentForModule(int accountId, int moduleId, int currencyId, DateTime paymentDate, decimal paymentValue)
        {
            Database database = null;
            int? resultId;
            try
            {
                database = Database.GetInstance();
                //Todo: получить курс по id
                var command = database.pr_payment_for_module_add;
                var commandParams = command.Parameters;
                commandParams["@account_id"].Value = accountId;
                commandParams["@module_id"].Value = moduleId;
                commandParams["@currency_id"].Value = currencyId;
                commandParams["@payment_date"].Value = paymentDate;
                commandParams["@payment_value"].Value = paymentValue;
                commandParams["@discount_rate"].Value = 1.0; // Нет скидки, если платим за один модуль
                commandParams["@result_id"].Value = 0;
                command.ExecuteNonQuery();
                resultId = commandParams["@result_id"].Value as int?;
            }
            finally
            {
                database.CloseConnection();
            }

            return resultId.HasValue ? resultId.Value : -1;
        }
    }
}
