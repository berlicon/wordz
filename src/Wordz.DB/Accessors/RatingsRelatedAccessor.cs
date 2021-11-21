using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wordz.DB.Accessors
{
    public static class RatingsRelatedAccessor
    {
        public static void UpdateRating(int accountId, Guid targetElement, double value)
        {
            Database database = null;
            try
            {
                database = Database.GetInstance();

                var command = database.pr_rating_update;
                var commandParams = command.Parameters;

                commandParams["@account_id"].Value = accountId;
                commandParams["@target_element"].Value = targetElement;
                commandParams["@value"].Value = value;
                command.ExecuteNonQuery();
            }
            finally
            {
                database.CloseConnection();
            }
        }
    }
}
