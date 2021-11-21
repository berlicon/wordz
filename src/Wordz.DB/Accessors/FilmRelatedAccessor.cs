using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Wordz.BE;
using Wordz.BE.Dto;

namespace Wordz.DB.Accessors
{
    public class FilmRelatedAccessor
    {
        public static FilmWithCategoryAndOrderDto[] GetFilmChannels(int accountId, int nativeLanguageId, int learnLanguageId)
        {
            Database database = null;
            try
            {
                database = Database.GetInstance();
                var filmList = new List<FilmWithCategoryAndOrderDto>();
                var procName = database.pr_film_get_list;
                procName.Parameters["@native_language_id"].Value = nativeLanguageId;
                procName.Parameters["@learn_language_id"].Value = learnLanguageId;
                procName.Parameters["@account_id"].Value = accountId;
                using (SqlDataReader reader = procName.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(reader.GetOrdinal("id"));
                        string imageUrl = reader.GetValueOrDefault<string>("image_url");
                        string url = reader.GetValueOrDefault<string>("url");
                        string name = reader.GetValueOrDefault<string>("name");
                        string description = reader.GetValueOrDefault<string>("description");
                        int? authorId = reader.GetValueOrDefault<int?>("account_id");
                        bool isEditable = reader.GetValueOrDefault<bool>("is_editable");
                        //bool useMediaPlayer = reader.GetBoolean(reader.GetOrdinal("use_media_player"));
                        int orderInList = reader.GetValueOrDefault<int>("order_in_list");
                        int? categoryId = reader.GetValueOrDefault<int?>("film_category_id");
                        string categoryName = reader.GetValueOrDefault<string>("category_name");
                        string pattern = reader.GetValueOrDefault<string>("player_patter");
                        var film = new FilmWithCategoryAndOrderDto
                                       {
                                           Id = id,
                                           Url = url,
                                           Name = name,
                                           AccountId = authorId,
                                           CategoryId = categoryId,
                                           CategoryName = categoryName,
                                           Category = categoryName,
                                           OrderInList = orderInList,
                                           PlayerPattern = pattern,
                                           IsEditable = isEditable,
                                           ImageUrl = imageUrl,
                                           Description = description
                                       };
                        filmList.Add(film);
                    }
                }
                return filmList.ToArray();
            }
            finally
            {
                database.CloseConnection();
            }
        }

        public static FilmWithCategoryAndOrderDto[] GetOtherFilmChannels(int accountId, int nativeLanguageId, int learnLanguageId)
        {
            Database database = null;
            try
            {
                database = Database.GetInstance();
                var filmList = new List<FilmWithCategoryAndOrderDto>();
                var procName = database.pr_film_get_other_list;
                procName.Parameters["@native_language_id"].Value = nativeLanguageId;
                procName.Parameters["@learn_language_id"].Value = learnLanguageId;
                procName.Parameters["@account_id"].Value = accountId;
                using (SqlDataReader reader = procName.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(reader.GetOrdinal("id"));
                        string imageUrl = reader.GetValueOrDefault<string>("image_url");
                        string url = reader.GetValueOrDefault<string>("url");
                        string name = reader.GetValueOrDefault<string>("name");
                        string description = reader.GetValueOrDefault<string>("description");
                        int? authorId = reader.GetValueOrDefault<int?>("account_id");
                        bool isEditable = reader.GetValueOrDefault<bool>("is_editable");
                        //bool useMediaPlayer = reader.GetBoolean(reader.GetOrdinal("use_media_player"));
                        //int orderInList = reader.GetValueOrDefault<int>("order_in_list");
                        int? categoryId = reader.GetValueOrDefault<int?>("film_category_id");
                        string categoryName = reader.GetValueOrDefault<string>("category_name");
                        string pattern = reader.GetValueOrDefault<string>("player_patter");
                        var film = new FilmWithCategoryAndOrderDto
                        {
                            Id = id,
                            Url = url,
                            Name = name,
                            AccountId = authorId,
                            CategoryId = categoryId,
                            CategoryName = categoryName,
                            Category = categoryName,
                            OrderInList = 0,
                            PlayerPattern = pattern,
                            IsEditable = isEditable,
                            ImageUrl = imageUrl,
                            Description = description
                        };
                        filmList.Add(film);
                    }
                }
                return filmList.ToArray();
            }
            finally
            {
                database.CloseConnection();
            }
        }

        public static int UpdateFilmChannel(int accountId, FilmUpdateDto film)
        {
            Database database = null;
            int? resultId;
            try
            {
                database = Database.GetInstance();
                var command = database.pr_film_update_or_insert;
                var cmdParams = command.Parameters;
                cmdParams[Film.AccountIdDbPropertyName.AsDbParam()].Value = accountId;
                cmdParams[Film.DescriptionDbPropertyName.AsDbParam()].Value = film.Description;
                cmdParams[Film.IdDbPropertyName.AsDbParam()].Value = film.Id;
                cmdParams[Film.ImageUrlDbPropertyName.AsDbParam()].Value = film.ImageUrl;
                cmdParams[Film.IsEditableDbPropertyName.AsDbParam()].Value = film.IsEditable;
                cmdParams[Film.NameDbPropertyName.AsDbParam()].Value = film.Name;
                cmdParams[Film.PlayerCodeDbPropertyName.AsDbParam()].Value = film.PlayerCode;
                cmdParams[FilmUpdateDto.NativeLanguageDbPropertyName.AsDbParam()].Value = film.NativeLanguageId;
                cmdParams[FilmUpdateDto.LearnLanguageDbPropertyName.AsDbParam()].Value = film.LearnLanguageId;
                cmdParams[Film.CategoryDbPropertyName.AsDbParam()].Value = film.CategoryId;
                resultId = command.ExecuteScalar() as int?;
            }
            finally
            {
                database.CloseConnection();
            }

            return resultId.HasValue ? resultId.Value : -1;
        }

        public static int DeleteFilmChannel(int accountId, int fmId)
        {
            Database database = null;
            try
            {
                database = Database.GetInstance();
                var command = database.pr_film_delete;
                var cmdParams = command.Parameters;
                cmdParams["@account_id"].Value = accountId;
                cmdParams["@film_id"].Value = fmId;
                var result = command.ExecuteScalar() as int?;
                if (result.HasValue)
                {
                    return result.Value;
                }
            }
            finally
            {
                database.CloseConnection();
            }
            return -1;
        }

        public static int UpdateFilmChannelOrder(int accountId, EntityOrderingUpdateInfoDto info)
        {
            Database database = null;
            try
            {
                var orderingString = string.Join(";", info.OrderingInfo.Select(oi => oi.Id.ToString() + "=" + oi.OrderIndex.ToString()));

                database = Database.GetInstance();
                var command = database.pr_film_update_order;
                var cmdParams = command.Parameters;
                cmdParams["@account_id"].Value = accountId;
                cmdParams["@ordering_string"].Value = orderingString;
                var result = command.ExecuteScalar() as int?;
                if (result.HasValue)
                {
                    return result.Value;
                }
            }
            finally
            {
                database.CloseConnection();
            }
            return -1;
        }
    }
}
