using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Wordz.BE;
using Wordz.BE.Dto;

namespace Wordz.DB.Accessors
{
    public static class ExerciseRelatedAccessor
    {
        public static ExerciseBase[] GetExercisesBaseArray(int accountId, int moduleId)
        {
            Database database = null;
            ExerciseBase[] exerciseBaseArray;
            try
            {
                database = Database.GetInstance();
                exerciseBaseArray = GetExercisesBaseArray(database, accountId, moduleId);
            }
            finally
            {
                if (database != null)
                {
                    database.CloseConnection();
                }
            }

            return exerciseBaseArray ?? new ExerciseBase[] {};
        }

        public static ExerciseBase[] GetExercisesBaseArray(Database database, int accountId, int moduleId)
        {
            var exerciseBaseList = new List<ExerciseBase>();

            var command = database.pr_exercises_for_module_lst;
            var cmdParams = command.Parameters;
            cmdParams["@account_id"].Value = accountId;
            cmdParams["@module_id"].Value = moduleId;
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var exerciseBase = new ExerciseBase
                                           {
                                               Id = reader.GetValueOrDefault<int>("id"),
                                               Name = reader.GetValueOrDefault<string>("name"),
                                               Description = reader.GetValueOrDefault<string>("description"),
                                               OrdinalNumber = reader.GetValueOrDefault<int>("ordinal_number"),
                                               ModuleId = moduleId,
                                               ExerciseType = reader.GetValueOrDefault<ExerciseType>("Type")
                                           };
                    exerciseBaseList.Add(exerciseBase);
                }
            }

            return exerciseBaseList.ToArray();
        }

        #region Упражнение "Текст"

        /// <summary>
        /// Получение упражнения по идетификатору
        /// </summary>
        /// <param name="exerciseTextId"></param>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public static ExerciseText GetExerciseTextById(int exerciseTextId, int accountId)
        {
            Database database = null;
            ExerciseText exerciseText = null;
            try
            {
                database = Database.GetInstance();
                var exerciseTextGetCmd = database.pr_exercise_text_get;
                var exerciseTextGetCmdParams = exerciseTextGetCmd.Parameters;
                exerciseTextGetCmdParams["@exercise_id"].Value = exerciseTextId;
                exerciseTextGetCmdParams["@account_id"].Value = accountId;
                using (SqlDataReader reader = exerciseTextGetCmd.ExecuteReader())
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            exerciseText = new ExerciseText
                                               {
                                                   Id = reader.GetValueOrDefault<int>("id"),
                                                   Text = reader.GetValueOrDefault<string>("text"),
                                                   Name = reader.GetValueOrDefault<string>("name"),
                                                   Description = reader.GetValueOrDefault<string>("description"),
                                                   ModuleId = reader.GetValueOrDefault<int>("module_id"),
                                                   OrdinalNumber = reader.GetValueOrDefault<int>("ordinal_number"),
                                                   ParentId = reader.GetValueOrDefault<int?>("parent_id"),
                                                   IsApproved = reader.GetValueOrDefault<bool>("is_approved")
                                               };
                            break;
                        }
                    }
                }
            }
            finally
            {
                if (database != null)
                {
                    database.CloseConnection();
                }
            }

            return exerciseText;
        }

        /// <summary>
        /// Получение списка упражнений по идентификатору модуля
        /// </summary>
        /// <param name="moduleId"></param>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public static IEnumerable<ExerciseText> GetExerciseTextList(int moduleId, int accountId)
        {
            Database database = null;
            var modules = new List<ExerciseText>();
            try
            {
                database = Database.GetInstance();

                var command = database.pr_exercise_text_list;
                var commandParams = command.Parameters;

                commandParams["@module_id"].Value = moduleId;
                commandParams["@account_id"].Value = accountId;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            var module = new ExerciseText
                                             {
                                                 Id = reader.GetValueOrDefault<int>("id"),
                                                 Text = reader.GetValueOrDefault<string>("text"),
                                                 ModuleId = reader.GetValueOrDefault<int>("module_id"),
                                                 Name = reader.GetValueOrDefault<string>("name"),
                                                 Description = reader.GetValueOrDefault<string>("description"),
                                                 OrdinalNumber = reader.GetValueOrDefault<int>("ordinal_number"),
                                                 ParentId = reader.GetValueOrDefault<int?>("parent_id"),
                                                 IsApproved = reader.GetValueOrDefault<bool>("is_approved")
                                             };
                            modules.Add(module);
                        }
                    }
                }
            }
            finally
            {
                if (database != null)
                {
                    database.CloseConnection();
                }
            }
            return modules;
        }

        /// <summary>
        /// Добавление нового упражнения "текст"
        /// </summary>
        /// <param name="exerciseText">Добавляемое упражнение</param>
        /// <param name="accountId"></param>
        /// <returns>Добавленная запись с присвоенным идентификатором</returns>
        public static ExerciseText AddExerciseText(ExerciseText exerciseText, int accountId)
        {
            Database database = null;
            try
            {
                database = Database.GetInstance();
                database.pr_exercise_text_ins.Parameters["@account_id"].DbType = DbType.Int32;
                database.pr_exercise_text_ins.Parameters["@name"].DbType = DbType.String;
                database.pr_exercise_text_ins.Parameters["@text"].DbType = DbType.String;
                database.pr_exercise_text_ins.Parameters["@module_id"].DbType = DbType.Int32;
                database.pr_exercise_text_ins.Parameters["@description"].DbType = DbType.String;

                database.pr_exercise_text_ins.Parameters["@account_id"].Value = accountId;
                database.pr_exercise_text_ins.Parameters["@name"].Value = exerciseText.Name;
                database.pr_exercise_text_ins.Parameters["@text"].Value = exerciseText.Text;
                database.pr_exercise_text_ins.Parameters["@module_id"].Value = exerciseText.ModuleId;
                database.pr_exercise_text_ins.Parameters["@description"].Value = exerciseText.Description;

                try
                {
                    using (SqlDataReader reader = database.pr_exercise_text_ins.ExecuteReader())
                    {
                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                exerciseText.Id = reader.GetValueOrDefault<int>("id");
                                exerciseText.OrdinalNumber = reader.GetValueOrDefault<int>("ordinal_number");
                            }
                        }
                    }
                    return exerciseText;
                }
                catch
                {
                    return exerciseText;
                }
            }
            finally
            {
                if (database != null)
                {
                    database.CloseConnection();
                }
            }
        }

        /// <summary>
        /// Изменение упражнения "текст"
        /// </summary>
        /// <param name="exerciseText">Изменяемое упражнение</param>
        /// <param name="accountId"></param>
        /// <returns>В случае успешного обновления возращает идентификатор записи, в случае неуспеха -1</returns>
        public static int UpdateExerciseText(ExerciseText exerciseText, int accountId)
        {
            Database database = null;
            int? resultId;
            try
            {
                database = Database.GetInstance();

                var exerciseUpdateCmd = database.pr_exercise_text_upd;
                var exerciseUpdateCmdParams = exerciseUpdateCmd.Parameters;

                exerciseUpdateCmdParams["@account_id"].Value = accountId;
                exerciseUpdateCmdParams["@exercise_text_id"].Value = exerciseText.Id;
                exerciseUpdateCmdParams["@name"].Value = exerciseText.Name;
                exerciseUpdateCmdParams["@description"].Value = exerciseText.Description;
                exerciseUpdateCmdParams["@module_id"].Value = exerciseText.ModuleId;
                exerciseUpdateCmdParams["@text"].Value = exerciseText.Text;
                exerciseUpdateCmdParams["@ordinal_number"].Value = exerciseText.OrdinalNumber;
                exerciseUpdateCmdParams["@result_id"].Value = -1;

                exerciseUpdateCmd.ExecuteNonQuery();
                resultId = exerciseUpdateCmdParams["@result_id"].Value as int?;
            }
            finally
            {
                if (database != null)
                {
                    database.CloseConnection();
                }
            }
            return resultId.HasValue ? resultId.Value : -1;
        }

        /// <summary>
        /// Удаление упражнения по идетификатору
        /// </summary>
        /// <param name="exerciseTextId">Идентификатор упражнения на удаление</param>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public static void DeleteExerciseTextById(int exerciseTextId, int accountId)
        {
            Database database = null;
            try
            {
                database = Database.GetInstance();
                var exerciseTextCmd = database.pr_exercise_text_del;
                var exerciseTextCmdParams = exerciseTextCmd.Parameters;
                exerciseTextCmdParams["@exercise_text_id"].Value = exerciseTextId;
                exerciseTextCmdParams["@account_id"].Value = accountId;
                exerciseTextCmd.ExecuteNonQuery();
            }
            finally
            {
                if (database != null)
                {
                    database.CloseConnection();
                }
            }
        }

        #endregion

        #region Упражнение "Выбор"

        /// <summary>
        /// Получение упражнения "Выбор" по идетификатору
        /// </summary>
        /// <param name="exerciseSelectId">Идентификатор упражнения</param>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public static ExerciseSelectDto GetExerciseSelectById(int exerciseSelectId, int accountId)
        {
            Database database = null;
            ExerciseSelectDto exercise = null;
            try
            {
                database = Database.GetInstance();
                var exerciseGetCmd = database.pr_exercise_select_get;
                var exerciseGetCmdParams = exerciseGetCmd.Parameters;
                exerciseGetCmdParams["@account_id"].Value = accountId;
                exerciseGetCmdParams["@exercise_id"].Value = exerciseSelectId;
                using (SqlDataReader reader = exerciseGetCmd.ExecuteReader())
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            exercise = new ExerciseSelectDto
                                           {
                                               Id = reader.GetValueOrDefault<int>("id"),
                                               Text = reader.GetValueOrDefault<string>("text"),
                                               Name = reader.GetValueOrDefault<string>("name"),
                                               Description = reader.GetValueOrDefault<string>("description"),
                                               ModuleId = reader.GetValueOrDefault<int>("module_id"),
                                               OrdinalNumber = reader.GetValueOrDefault<int>("ordinal_number"),
                                               PictureId = reader.GetValueOrDefault<int>("picture_id"),
                                               ParentId = reader.GetValueOrDefault<int?>("parent_id"),
                                               IsApproved = reader.GetValueOrDefault<bool>("is_approved")
                                           };
                            break;
                        }
                    }
                }
                if (exercise != null)
                {
                    exercise.QuestionPicture = exercise.PictureId.HasValue ? PictureRelatedAccessor.GetPictureById(exercise.PictureId.Value) : null;
                    exercise.Answers = GetExerciseSelectAnswers(accountId, exercise.Id);
                }
            }
            finally
            {
                if (database != null)
                {
                    database.CloseConnection();
                }
            }

            return exercise;
        }

        /// <summary>
        /// Возвращает список ответов для упражнения "Выбор"
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="exerciseId"></param>
        /// <returns></returns>
        public static List<AnswerDto> GetExerciseSelectAnswers(int accountId, int exerciseId)
        {
            Database database = null;
            var answers = new List<AnswerDto>();
            try
            {
                database = Database.GetInstance();

                var command = database.pr_exercise_select_answer_list;
                var commandParams = command.Parameters;

                commandParams["@account_id"].Value = accountId;
                commandParams["@exercise_id"].Value = exerciseId;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            var answer = new AnswerDto
                            {
                                Id = reader.GetValueOrDefault<int>("id"),
                                Text = reader.GetValueOrDefault<string>("text"),
                                ExerciseId = reader.GetValueOrDefault<int>("exercise_id"),
                                PictureId = reader.GetValueOrDefault<int>("picture_id"),
                                IsRight = reader.GetValueOrDefault<bool>("is_right")
                            };
                            answers.Add(answer);
                        }
                    }
                }
                foreach (var answer in answers)
                {
                    if (answer != null)
                    {
                        answer.Picture = answer.PictureId.HasValue ? PictureRelatedAccessor.GetPictureById(answer.PictureId.Value) : null;
                    }
                }
            }
            finally
            {
                if (database != null)
                {
                    database.CloseConnection();
                }
            }
            return answers;
        }

        /// <summary>
        /// Добавление нового упражнения "выбор"
        /// </summary>
        /// <param name="exerciseSelect">Добавляемое упражнение</param>
        /// <param name="accountId"></param>
        /// <returns>Добавленная запись с присвоенным идентификатором</returns>
        public static ExerciseSelect AddExerciseSelect(ExerciseSelect exerciseSelect, int accountId)
        {
            Database database = null;
            try
            {
                database = Database.GetInstance();
                database.pr_exercise_select_ins.Parameters["@account_id"].DbType = DbType.Int32;
                database.pr_exercise_select_ins.Parameters["@name"].DbType = DbType.String;
                database.pr_exercise_select_ins.Parameters["@text"].DbType = DbType.String;
                database.pr_exercise_select_ins.Parameters["@module_id"].DbType = DbType.Int32;
                database.pr_exercise_select_ins.Parameters["@description"].DbType = DbType.String;
                database.pr_exercise_select_ins.Parameters["@picture_id"].DbType = DbType.Object;

                database.pr_exercise_select_ins.Parameters["@account_id"].Value = accountId;
                database.pr_exercise_select_ins.Parameters["@name"].Value = exerciseSelect.Name;
                database.pr_exercise_select_ins.Parameters["@text"].Value = exerciseSelect.Text;
                database.pr_exercise_select_ins.Parameters["@module_id"].Value = exerciseSelect.ModuleId;
                database.pr_exercise_select_ins.Parameters["@description"].Value = exerciseSelect.Description;
                database.pr_exercise_select_ins.Parameters["@picture_id"].Value = exerciseSelect.PictureId;

                try
                {
                    using (SqlDataReader reader = database.pr_exercise_select_ins.ExecuteReader())
                    {
                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                exerciseSelect.Id = reader.GetValueOrDefault<int>("id");
                                exerciseSelect.OrdinalNumber = reader.GetValueOrDefault<int>("ordinal_number");
                            }
                        }
                    }
                    return exerciseSelect;
                }
                catch
                {
                    return exerciseSelect;
                }
            }
            finally
            {
                if (database != null)
                {
                    database.CloseConnection();
                }
            }
        }

        /// <summary>
        /// Изменение упражнения "выбор"
        /// </summary>
        /// <param name="exerciseSelect">Изменяемое упражнение</param>
        /// <param name="accountId"></param>
        /// <returns>В случае успешного обновления возращает идентификатор записи, в случае неуспеха -1</returns>
        public static int UpdateExerciseSelect(ExerciseSelect exerciseSelect, int accountId)
        {
            Database database = null;
            int? resultId = null;
            try
            {
                database = Database.GetInstance();

                var exerciseUpdateCmd = database.pr_exercise_select_upd;
                var exerciseUpdateCmdParams = exerciseUpdateCmd.Parameters;

                exerciseUpdateCmdParams["@account_id"].Value = accountId;
                exerciseUpdateCmdParams["@exercise_select_id"].Value = exerciseSelect.Id;
                exerciseUpdateCmdParams["@name"].Value = exerciseSelect.Name;
                exerciseUpdateCmdParams["@description"].Value = exerciseSelect.Description;
                exerciseUpdateCmdParams["@module_id"].Value = exerciseSelect.ModuleId;
                exerciseUpdateCmdParams["@ordinal_number"].Value = exerciseSelect.OrdinalNumber;
                exerciseUpdateCmdParams["@text"].Value = exerciseSelect.Text;
                exerciseUpdateCmdParams["@picture_id"].Value = exerciseSelect.PictureId;
                exerciseUpdateCmdParams["@result_id"].Value = -1;

                exerciseUpdateCmd.ExecuteNonQuery();
                resultId = exerciseUpdateCmdParams["@result_id"].Value as int?;
            }
            catch(Exception exp)
            {
            }
            finally
            {
                if (database != null)
                {
                    database.CloseConnection();
                }
            }
            return resultId.HasValue ? resultId.Value : -1;
        }

        /// <summary>
        /// Удаление упражнения по идетификатору
        /// </summary>
        /// <param name="exerciseSelectId">Идентификатор упражнения на удаление</param>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public static void DeleteExerciseSelectById(int exerciseSelectId, int accountId)
        {
            Database database = null;
            try
            {
                database = Database.GetInstance();
                var exerciseSelectCmd = database.pr_exercise_select_del;
                var exerciseSelectCmdParams = exerciseSelectCmd.Parameters;
                exerciseSelectCmdParams["@account_id"].Value = accountId;
                exerciseSelectCmdParams["@exercise_select_id"].Value = exerciseSelectId;
                exerciseSelectCmd.ExecuteNonQuery();
            }
            finally
            {
                if (database != null)
                {
                    database.CloseConnection();
                }
            }
        }

        /// <summary>
        /// Добавление нового ответа
        /// </summary>
        /// <param name="answer">Добавляемый ответ</param>
        /// <returns>Добавленная запись с присвоенным идентификатором</returns>
        public static Answer AddAnswer(Answer answer)
        {
            Database database = null;
            try
            {
                database = Database.GetInstance();
                database.pr_answer_ins.Parameters["@text"].DbType = DbType.String;
                database.pr_answer_ins.Parameters["@exercise_id"].DbType = DbType.Int32;
                database.pr_answer_ins.Parameters["@picture_id"].DbType = DbType.Int32;
                database.pr_answer_ins.Parameters["@is_right"].DbType = DbType.Boolean;

                database.pr_answer_ins.Parameters["@text"].Value = answer.Text;
                database.pr_answer_ins.Parameters["@exercise_id"].Value = answer.ExerciseId;
                database.pr_answer_ins.Parameters["@picture_id"].Value = answer.PictureId;
                database.pr_answer_ins.Parameters["@is_right"].Value = answer.IsRight;

                try
                {
                    object answerId = database.pr_answer_ins.ExecuteScalar();

                    if (answerId != null)
                    {
                        answer.Id = int.Parse(answerId.ToString());
                    }
                    return answer;
                }
                catch
                {
                    return answer;
                }
            }
            finally
            {
                if (database != null)
                {
                    database.CloseConnection();
                }
            }
        }

        /// <summary>
        /// Изменение ответа
        /// </summary>
        /// <param name="answer">Изменяемый ответ</param>
        /// <returns>В случае успешного обновления возращает идентификатор записи, в случае неуспеха -1</returns>
        public static int UpdateAnswer(Answer answer)
        {
            Database database = null;
            int? resultId;
            try
            {
                database = Database.GetInstance();

                var answerCmd = database.pr_answer_upd;
                var answerCmdParams = answerCmd.Parameters;

                answerCmdParams["@answer_id"].Value = answer.Id;
                answerCmdParams["@exercise_id"].Value = answer.ExerciseId;
                answerCmdParams["@text"].Value = answer.Text;
                answerCmdParams["@is_right"].Value = answer.IsRight;
                answerCmdParams["@picture_id"].Value = answer.PictureId;
                answerCmdParams["@result_id"].Value = -1;

                answerCmd.ExecuteNonQuery();
                resultId = answerCmdParams["@result_id"].Value as int?;
            }
            finally
            {
                if (database != null)
                {
                    database.CloseConnection();
                }
            }
            return resultId.HasValue ? resultId.Value : -1;
        }

        /// <summary>
        /// Удаление ответа по идетификатору
        /// </summary>
        /// <param name="answerId">Идентификатор ответа на удаление</param>
        /// <returns></returns>
        public static void DeleteAnswerById(int answerId)
        {
            Database database = null;
            try
            {
                database = Database.GetInstance();
                var answerDelCmd = database.pr_answer_del;
                var answerDelCmdParams = answerDelCmd.Parameters;
                answerDelCmdParams["@answer_id"].Value = answerId;
                answerDelCmd.ExecuteNonQuery();
            }
            finally
            {
                if (database != null)
                {
                    database.CloseConnection();
                }
            }
        }

        #endregion

        #region Упражнение "Текстовый ответ"

        /// <summary>
        /// Получение упражнения по идетификатору
        /// </summary>
        /// <param name="exerciseTextId"></param>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public static ExerciseAnswerText GetExerciseAnswerTextById(int exerciseTextId, int accountId)
        {
            Database database = null;
            ExerciseAnswerText exerciseText = null;
            try
            {
                database = Database.GetInstance();
                var exerciseTextGetCmd = database.pr_exercise_answer_text_get;
                var exerciseTextGetCmdParams = exerciseTextGetCmd.Parameters;
                exerciseTextGetCmdParams["@account_id"].Value = accountId;
                exerciseTextGetCmdParams["@exercise_id"].Value = exerciseTextId;
                using (SqlDataReader reader = exerciseTextGetCmd.ExecuteReader())
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            exerciseText = new ExerciseAnswerText
                            {
                                Id = reader.GetValueOrDefault<int>("id"),
                                Text = reader.GetValueOrDefault<string>("text"),
                                Name = reader.GetValueOrDefault<string>("name"),
                                Description = reader.GetValueOrDefault<string>("description"),
                                ModuleId = reader.GetValueOrDefault<int>("module_id"),
                                OrdinalNumber = reader.GetValueOrDefault<int>("ordinal_number"),
                                ParentId = reader.GetValueOrDefault<int?>("parent_id"),
                                IsApproved = reader.GetValueOrDefault<bool>("is_approved")
                            };
                            break;
                        }
                    }
                }
            }
            finally
            {
                if (database != null)
                {
                    database.CloseConnection();
                }
            }

            return exerciseText;
        }

        /// <summary>
        /// Добавление нового упражнения "текстовый ответ"
        /// </summary>
        /// <param name="exerciseText">Добавляемое упражнение</param>
        /// <param name="accountId"></param>
        /// <returns>Добавленная запись с присвоенным идентификатором</returns>
        public static ExerciseAnswerText AddExerciseAnswerText(ExerciseAnswerText exerciseText, int accountId)
        {
            Database database = null;
            try
            {
                database = Database.GetInstance();
                database.pr_exercise_answer_text_ins.Parameters["@account_id"].DbType = DbType.Int32;
                database.pr_exercise_answer_text_ins.Parameters["@name"].DbType = DbType.String;
                database.pr_exercise_answer_text_ins.Parameters["@text"].DbType = DbType.String;
                database.pr_exercise_answer_text_ins.Parameters["@module_id"].DbType = DbType.Int32;
                database.pr_exercise_answer_text_ins.Parameters["@description"].DbType = DbType.String;

                database.pr_exercise_answer_text_ins.Parameters["@account_id"].Value = accountId;
                database.pr_exercise_answer_text_ins.Parameters["@name"].Value = exerciseText.Name;
                database.pr_exercise_answer_text_ins.Parameters["@text"].Value = exerciseText.Text;
                database.pr_exercise_answer_text_ins.Parameters["@module_id"].Value = exerciseText.ModuleId;
                database.pr_exercise_answer_text_ins.Parameters["@description"].Value = exerciseText.Description;

                try
                {
                    using (SqlDataReader reader = database.pr_exercise_answer_text_ins.ExecuteReader())
                    {
                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                exerciseText.Id = reader.GetValueOrDefault<int>("id");
                                exerciseText.OrdinalNumber = reader.GetValueOrDefault<int>("ordinal_number");
                            }
                        }
                    }
                    return exerciseText;
                }
                catch
                {
                    return exerciseText;
                }
            }
            finally
            {
                if (database != null)
                {
                    database.CloseConnection();
                }
            }
        }

        /// <summary>
        /// Изменение упражнения "текстовый ответ"
        /// </summary>
        /// <param name="exerciseText">Изменяемое упражнение</param>
        /// <param name="accountId"></param>
        /// <returns>В случае успешного обновления возращает идентификатор записи, в случае неуспеха -1</returns>
        public static int UpdateExerciseAnswerText(ExerciseAnswerText exerciseText, int accountId)
        {
            Database database = null;
            int? resultId;
            try
            {
                database = Database.GetInstance();

                var exerciseUpdateCmd = database.pr_exercise_answer_text_upd;
                var exerciseUpdateCmdParams = exerciseUpdateCmd.Parameters;

                exerciseUpdateCmdParams["@account_id"].Value = accountId;
                exerciseUpdateCmdParams["@exercise_text_id"].Value = exerciseText.Id;
                exerciseUpdateCmdParams["@name"].Value = exerciseText.Name;
                exerciseUpdateCmdParams["@description"].Value = exerciseText.Description;
                exerciseUpdateCmdParams["@module_id"].Value = exerciseText.ModuleId;
                exerciseUpdateCmdParams["@ordinal_number"].Value = exerciseText.OrdinalNumber;
                exerciseUpdateCmdParams["@text"].Value = exerciseText.Text;
                exerciseUpdateCmdParams["@result_id"].Value = -1;

                exerciseUpdateCmd.ExecuteNonQuery();
                resultId = exerciseUpdateCmdParams["@result_id"].Value as int?;
            }
            finally
            {
                if (database != null)
                {
                    database.CloseConnection();
                }
            }
            return resultId.HasValue ? resultId.Value : -1;
        }

        /// <summary>
        /// Получение ответа пользователя по упражнению
        /// </summary>
        /// <param name="accountId">Идентификатор пользователя</param>
        /// <param name="exerciseId">Идентификатор упражения</param>
        /// <returns></returns>
        public static ExerciseTextAnswer GetExerciseTextAnswers(int accountId, int exerciseId)
        {
            Database database = null;
            ExerciseTextAnswer answer = null;
            try
            {
                database = Database.GetInstance();
                var exerciseTextGetCmd = database.pr_exercise_text_answer_get;
                var exerciseTextGetCmdParams = exerciseTextGetCmd.Parameters;
                exerciseTextGetCmdParams["@exercise_id"].Value = exerciseId;
                exerciseTextGetCmdParams["@account_id"].Value = accountId;
                using (SqlDataReader reader = exerciseTextGetCmd.ExecuteReader())
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            answer = new ExerciseTextAnswer
                            {
                                Id = reader.GetValueOrDefault<int>("id"),
                                Text = reader.GetValueOrDefault<string>("text"),
                                AccountId = reader.GetValueOrDefault<int>("account_id"),
                                ExerciseId = reader.GetValueOrDefault<int>("exercise_id"),
                                Mark = reader.GetValueOrDefault<int>("mark")
                            };
                            break;
                        }
                    }
                }
            }
            finally
            {
                if (database != null)
                {
                    database.CloseConnection();
                }
            }

            return answer;
        }

        /// <summary>
        /// Добавление нового ответа на упражнение "Текстовый ответ"
        /// </summary>
        /// <param name="answer">Добавляемый ответ</param>
        /// <returns>Добавленная запись с присвоенным идентификатором</returns>
        public static ExerciseTextAnswer AddExerciseTextAnswer(ExerciseTextAnswer answer)
        {
            Database database = null;
            try
            {
                database = Database.GetInstance();
                database.pr_exercise_text_answer_ins.Parameters["@text"].DbType = DbType.String;
                database.pr_exercise_text_answer_ins.Parameters["@exercise_id"].DbType = DbType.Int32;
                database.pr_exercise_text_answer_ins.Parameters["@account_id"].DbType = DbType.Int32;

                database.pr_exercise_text_answer_ins.Parameters["@text"].Value = answer.Text;
                database.pr_exercise_text_answer_ins.Parameters["@exercise_id"].Value = answer.ExerciseId;
                database.pr_exercise_text_answer_ins.Parameters["@account_id"].Value = answer.AccountId;

                try
                {
                    object answerId = database.pr_exercise_text_answer_ins.ExecuteScalar();

                    if (answerId != null)
                    {
                        answer.Id = int.Parse(answerId.ToString());
                    }
                    return answer;
                }
                catch
                {
                    return answer;
                }
            }
            finally
            {
                if (database != null)
                {
                    database.CloseConnection();
                }
            }
        }

        /// <summary>
        /// Изменение ответа на упражнение "Текстовый ответ"
        /// </summary>
        /// <param name="answer">Изменяемый ответ</param>
        /// <returns>В случае успешного обновления возращает идентификатор записи, в случае неуспеха -1</returns>
        public static int UpdateExerciseTextAnswer(ExerciseTextAnswer answer)
        {
            Database database = null;
            int? resultId;
            try
            {
                database = Database.GetInstance();

                var answerCmd = database.pr_exercise_text_answer_upd;
                var answerCmdParams = answerCmd.Parameters;

                answerCmdParams["@id"].Value = answer.Id;
                answerCmdParams["@mark"].Value = answer.Mark;
                answerCmdParams["@text"].Value = answer.Text;
                answerCmdParams["@result_id"].Value = -1;

                answerCmd.ExecuteNonQuery();
                resultId = answerCmdParams["@result_id"].Value as int?;
            }
            finally
            {
                if (database != null)
                {
                    database.CloseConnection();
                }
            }
            return resultId.HasValue ? resultId.Value : -1;
        }

        #endregion

        #region Упражнение "Выбор в тексте"

        /// <summary>
        /// Получение ответа пользователя по упражнению
        /// </summary>
        /// <param name="exerciseId">Идентификатор упражения</param>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public static ExerciseSelectText GetExerciseSelectTextById(int exerciseId, int accountId)
        {
            Database database = null;
            ExerciseSelectText exerciseText = null;
            try
            {
                database = Database.GetInstance();
                var exerciseTextGetCmd = database.pr_exercise_select_text_get;
                var exerciseTextGetCmdParams = exerciseTextGetCmd.Parameters;
                exerciseTextGetCmdParams["@account_id"].Value = accountId;
                exerciseTextGetCmdParams["@exercise_id"].Value = exerciseId;
                using (SqlDataReader reader = exerciseTextGetCmd.ExecuteReader())
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            exerciseText = new ExerciseSelectText
                            {
                                Id = reader.GetValueOrDefault<int>("id"),
                                Text = reader.GetValueOrDefault<string>("text"),
                                Name = reader.GetValueOrDefault<string>("name"),
                                Description = reader.GetValueOrDefault<string>("description"),
                                ModuleId = reader.GetValueOrDefault<int>("module_id"),
                                OrdinalNumber = reader.GetValueOrDefault<int>("ordinal_number"),
                                ParentId = reader.GetValueOrDefault<int?>("parent_id"),
                                IsApproved = reader.GetValueOrDefault<bool>("is_approved")
                            };
                            break;
                        }
                    }
                }
            }
            finally
            {
                if (database != null)
                {
                    database.CloseConnection();
                }
            }

            return exerciseText;
        }

        /// <summary>
        /// Добавление нового упражнения "Выбор в тексте"
        /// </summary>
        /// <returns>Добавленная запись с присвоенным идентификатором</returns>
        public static ExerciseSelectText AddExerciseSelectText(ExerciseSelectText exercise, int accountId)
        {
            Database database = null;
            try
            {
                database = Database.GetInstance();
                database.pr_exercise_select_text_ins.Parameters["@account_id"].DbType = DbType.Int32;
                database.pr_exercise_select_text_ins.Parameters["@name"].DbType = DbType.String;
                database.pr_exercise_select_text_ins.Parameters["@text"].DbType = DbType.String;
                database.pr_exercise_select_text_ins.Parameters["@module_id"].DbType = DbType.Int32;
                database.pr_exercise_select_text_ins.Parameters["@description"].DbType = DbType.String;

                database.pr_exercise_select_text_ins.Parameters["@account_id"].Value = accountId;
                database.pr_exercise_select_text_ins.Parameters["@name"].Value = exercise.Name;
                database.pr_exercise_select_text_ins.Parameters["@text"].Value = exercise.Text;
                database.pr_exercise_select_text_ins.Parameters["@module_id"].Value = exercise.ModuleId;
                database.pr_exercise_select_text_ins.Parameters["@description"].Value = exercise.Description;

                try
                {
                    using (SqlDataReader reader = database.pr_exercise_select_text_ins.ExecuteReader())
                    {
                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                exercise.Id = reader.GetValueOrDefault<int>("id");
                                exercise.OrdinalNumber = reader.GetValueOrDefault<int>("ordinal_number");
                            }
                        }
                    }
                    return exercise;
                }
                catch
                {
                    return exercise;
                }
            }
            finally
            {
                if (database != null)
                {
                    database.CloseConnection();
                }
            }
        }

        /// <summary>
        /// Изменение упражнения "Выбор в тексте"
        /// </summary>
        /// <returns>В случае успешного обновления возращает идентификатор записи, в случае неуспеха -1</returns>
        public static int UpdateExerciseSelectText(ExerciseSelectText exercise, int accountId)
        {
            Database database = null;
            int? resultId;
            try
            {
                database = Database.GetInstance();

                var exerciseUpdateCmd = database.pr_exercise_select_text_upd;
                var exerciseUpdateCmdParams = exerciseUpdateCmd.Parameters;

                exerciseUpdateCmdParams["@account_id"].Value = accountId;
                exerciseUpdateCmdParams["@exercise_select_text_id"].Value = exercise.Id;
                exerciseUpdateCmdParams["@name"].Value = exercise.Name;
                exerciseUpdateCmdParams["@description"].Value = exercise.Description;
                exerciseUpdateCmdParams["@module_id"].Value = exercise.ModuleId;
                exerciseUpdateCmdParams["@ordinal_number"].Value = exercise.OrdinalNumber;
                exerciseUpdateCmdParams["@text"].Value = exercise.Text;
                exerciseUpdateCmdParams["@result_id"].Value = -1;

                exerciseUpdateCmd.ExecuteNonQuery();
                resultId = exerciseUpdateCmdParams["@result_id"].Value as int?;
            }
            finally
            {
                if (database != null)
                {
                    database.CloseConnection();
                }
            }
            return resultId.HasValue ? resultId.Value : -1;
        }

        #endregion

        #region Упражнение "Пропуски в тексте"

        /// <summary>
        /// Получение упражнения "Пропуски в тексте"
        /// </summary>
        /// <param name="exerciseId">Идентификатор упражения</param>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public static ExerciseSkipText GetExerciseSkipTextById(int exerciseId, int accountId)
        {
            Database database = null;
            ExerciseSkipText exerciseText = null;
            try
            {
                database = Database.GetInstance();
                var exerciseTextGetCmd = database.pr_exercise_skip_text_get;
                var exerciseTextGetCmdParams = exerciseTextGetCmd.Parameters;
                exerciseTextGetCmdParams["@account_id"].Value = accountId;
                exerciseTextGetCmdParams["@exercise_id"].Value = exerciseId;
                using (SqlDataReader reader = exerciseTextGetCmd.ExecuteReader())
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            exerciseText = new ExerciseSkipText
                            {
                                Id = reader.GetValueOrDefault<int>("id"),
                                Text = reader.GetValueOrDefault<string>("text"),
                                Name = reader.GetValueOrDefault<string>("name"),
                                Description = reader.GetValueOrDefault<string>("description"),
                                ModuleId = reader.GetValueOrDefault<int>("module_id"),
                                OrdinalNumber= reader.GetValueOrDefault<int>("ordinal_number"),
                                ParentId = reader.GetValueOrDefault<int?>("parent_id"),
                                IsApproved = reader.GetValueOrDefault<bool>("is_approved")
                            };
                            break;
                        }
                    }
                }
            }
            finally
            {
                if (database != null)
                {
                    database.CloseConnection();
                }
            }

            return exerciseText;
        }

        /// <summary>
        /// Добавление нового упражнения "Пропуски в тексте"
        /// </summary>
        /// <returns>Добавленная запись с присвоенным идентификатором</returns>
        public static ExerciseSkipText AddExerciseSkipText(ExerciseSkipText exercise, int accountId)
        {
            Database database = null;
            try
            {
                database = Database.GetInstance();
                database.pr_exercise_skip_text_ins.Parameters["@account_id"].DbType = DbType.Int32;
                database.pr_exercise_skip_text_ins.Parameters["@name"].DbType = DbType.String;
                database.pr_exercise_skip_text_ins.Parameters["@text"].DbType = DbType.String;
                database.pr_exercise_skip_text_ins.Parameters["@module_id"].DbType = DbType.Int32;
                database.pr_exercise_skip_text_ins.Parameters["@description"].DbType = DbType.String;

                database.pr_exercise_skip_text_ins.Parameters["@account_id"].Value = accountId;
                database.pr_exercise_skip_text_ins.Parameters["@name"].Value = exercise.Name;
                database.pr_exercise_skip_text_ins.Parameters["@text"].Value = exercise.Text;
                database.pr_exercise_skip_text_ins.Parameters["@module_id"].Value = exercise.ModuleId;
                database.pr_exercise_skip_text_ins.Parameters["@description"].Value = exercise.Description;

                try
                {
                    using (SqlDataReader reader = database.pr_exercise_skip_text_ins.ExecuteReader())
                    {
                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                exercise.Id = reader.GetValueOrDefault<int>("id");
                                exercise.OrdinalNumber = reader.GetValueOrDefault<int>("ordinal_number");
                            }
                        }
                    }
                    return exercise;
                }
                catch
                {
                    return exercise;
                }
            }
            finally
            {
                if (database != null)
                {
                    database.CloseConnection();
                }
            }
        }

        /// <summary>
        /// Изменение упражнения "Пропуски в тексте"
        /// </summary>
        /// <returns>В случае успешного обновления возращает идентификатор записи, в случае неуспеха -1</returns>
        public static int UpdateExerciseSkipText(ExerciseSkipText exercise, int accountId)
        {
            Database database = null;
            int? resultId;
            try
            {
                database = Database.GetInstance();

                var exerciseUpdateCmd = database.pr_exercise_skip_text_upd;
                var exerciseUpdateCmdParams = exerciseUpdateCmd.Parameters;

                exerciseUpdateCmdParams["@account_id"].Value = accountId;
                exerciseUpdateCmdParams["@exercise_skip_text_id"].Value = exercise.Id;
                exerciseUpdateCmdParams["@name"].Value = exercise.Name;
                exerciseUpdateCmdParams["@description"].Value = exercise.Description;
                exerciseUpdateCmdParams["@module_id"].Value = exercise.ModuleId;
                exerciseUpdateCmdParams["@ordinal_number"].Value = exercise.OrdinalNumber;
                exerciseUpdateCmdParams["@text"].Value = exercise.Text;
                exerciseUpdateCmdParams["@result_id"].Value = -1;

                exerciseUpdateCmd.ExecuteNonQuery();
                resultId = exerciseUpdateCmdParams["@result_id"].Value as int?;
            }
            finally
            {
                if (database != null)
                {
                    database.CloseConnection();
                }
            }
            return resultId.HasValue ? resultId.Value : -1;
        }

        #endregion
    }
}
