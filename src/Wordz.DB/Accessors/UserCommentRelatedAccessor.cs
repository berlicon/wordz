using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Wordz.BE;
using Wordz.BE.Dto;

namespace Wordz.DB.Accessors
{
    public static class UserCommentRelatedAccessor
    {
        /// <summary>
        /// Добавление комментария
        /// </summary>
        /// <param name="userComment"></param>
        /// <param name="parentCommentId"></param>
        /// <returns></returns>
        public static int AddUserComment(UserComment userComment, int? parentCommentId)
        {
            Database database = null;
            int? resultId;
            try
            {
                database = Database.GetInstance();

                var command = database.pr_user_comment_add;
                var commandParams = command.Parameters;

                commandParams["@account_id"].Value = userComment.AuthorAccountId;
                commandParams["@target_element"].Value = userComment.TargetElement;
                commandParams["@comment_text"].Value = userComment.Text;
                commandParams["@created_date"].Value = userComment.CreatedDate;
                commandParams["@parent_comment_id"].Value = parentCommentId;
                command.ExecuteNonQuery();
                resultId = commandParams["@result_id"].Value as int?;
            }
            finally
            {
                database.CloseConnection();
            }

            return resultId.HasValue ? resultId.Value : -1;
        }

        /// <summary>
        /// Получение списка комментариев, сортированных по времени
        /// </summary>
        /// <param name="accountId">Юзер, запросивший</param>
        /// <param name="targetElement"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public static IList<UserCommentWithExtraDto> GetUserComments(int accountId, Guid targetElement, int pageSize, int pageNumber)
        {
            Database database = null;
            var listOfComments = new List<UserCommentWithExtraDto>();
            try
            {
                database = Database.GetInstance();

                var command = database.pr_user_comment_lst;
                var commandParams = command.Parameters;

                commandParams["@account_id"].Value = accountId;
                commandParams["@target_element"].Value = targetElement;
                commandParams["@page_size"].Value = pageSize;
                commandParams["@page_number"].Value = pageNumber;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var comment = new UserCommentWithExtraDto
                                          {
                                              AuthorAccountId = reader.GetValueOrDefault<int>("account_id"),
                                              ClaimsCount = reader.GetValueOrDefault<int>("claims_count"),
                                              AnswerLevel = reader.GetValueOrDefault<int>("answer_level"),
                                              TargetElement = reader.GetValueOrDefault<Guid>("target_element"),
                                              CreatedDate = reader.GetValueOrDefault<DateTime>("created_date"),
                                              Id = reader.GetValueOrDefault<int>("id"),
                                              Rating = reader.GetValueOrDefault<int>("rating"),
                                              Text = reader.GetValueOrDefault<string>("comment_text"),
                                              UserName = reader.GetValueOrDefault<string>("user_name")
                                          };
                        listOfComments.Add(comment);
                    }
                }

                
            }
            finally
            {
                database.CloseConnection();
            }

            return listOfComments.ToArray();
        }

        public static int GetUserCommentsCount(int accountId, Guid targetElement)
        {
            Database database = null;
            try
            {
                database = Database.GetInstance();

                var command = database.pr_user_comment_items_count;
                var commandParams = command.Parameters;

                commandParams["@account_id"].Value = accountId;
                commandParams["@target_element"].Value = targetElement;

                int? resultCount = command.ExecuteScalar() as int?;
                return resultCount.HasValue ? resultCount.Value : 0;
            }
            finally
            {
                database.CloseConnection();
            }
        }

        public static int RateUserComment(int accountId, int userCommentId, bool isPositive)
        {
            Database database = null;
            try
            {
                database = Database.GetInstance();

                var command = database.pr_user_comment_rate;
                var commandParams = command.Parameters;

                commandParams["@account_id"].Value = accountId;
                commandParams["@user_comment_id"].Value = userCommentId;
                commandParams["@is_positive"].Value = isPositive;

                var resultCount = command.ExecuteScalar() as int?;
                return resultCount.HasValue ? resultCount.Value : 0;
            }
            finally
            {
                database.CloseConnection();
            }
        }

        public static int ClaimUserComment(int accountId, int userCommentId)
        {
            Database database = null;
            try
            {
                database = Database.GetInstance();

                var command = database.pr_user_comment_claim;
                var commandParams = command.Parameters;

                commandParams["@account_id"].Value = accountId;
                commandParams["@user_comment_id"].Value = userCommentId;

                var resultCount = command.ExecuteScalar() as int?;
                return resultCount.HasValue ? resultCount.Value : 0;
            }
            finally
            {
                database.CloseConnection();
            }
        }
    }
}
