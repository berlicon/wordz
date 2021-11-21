using System;
using System.Data.SqlClient;
using Wordz.BE;

namespace Wordz.DB.Accessors
{
    public static class PictureRelatedAccessor
    {
        public static Picture GetPictureById(int pictureId)
        {
            Database database = null;
            Picture pictureItem = null;
            try
            {
                database = Database.GetInstance();

                var command = database.pr_picture_get;
                var commandParams = command.Parameters;

                commandParams["@picture_id"].Value = pictureId;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        pictureItem = new Picture
                                     {
                                         Id = pictureId
                                     };
                        int picture = reader.GetOrdinal("data");

                        if (reader.IsDBNull(picture))
                        {
                            pictureItem.Data = null;
                            break;
                        }

                        long length = reader.GetBytes(picture, 0, null, 0, 0);
                        var sound = new byte[length];
                        reader.GetBytes(picture, 0, sound, 0, (int)length);
                        pictureItem.Data = sound;

                        break;
                    }
                }
            }
            finally
            {
                database.CloseConnection();
            }

            return pictureItem;
        }

        public static int UpdatePicture(int accountId, Picture picture)
        {
            Database database = null;
            int resultId = -1;
            try
            {
                database = Database.GetInstance();
                resultId = UpdatePicture(database, accountId, picture);
            }
            finally
            {
                database.CloseConnection();
            }
            return resultId;
        }

        public static int UpdatePicture(Database database, int accountId, Picture picture)
        {
            int? resultId;
            
            var command = database.pr_picture_update;
            var commandParams = command.Parameters;

            commandParams["@picture_data"].Value = picture.Data;
            commandParams["@account_id"].Value = accountId;
            commandParams["@result_id"].Value = 0;
            command.ExecuteNonQuery();
            resultId = commandParams["@result_id"].Value as int?;

            return resultId.HasValue? resultId.Value : -1;
        }
    }
}