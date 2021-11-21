using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using Wordz.BE;
using Wordz.BE.Dto;

namespace Wordz.DB.Accessors
{
    public static class CoursesRelatedAccessor
    {
        public static IEnumerable<CourseDetailsDto> GetCourses(int accountId, bool isPublic)
        {
            Database database = null;
            try
            {
                database = Database.GetInstance();
                var list = new List<CourseDetailsDto>();

                database.pr_course_get_list.Parameters["@account_id"].Value = accountId;
                database.pr_course_get_list.Parameters["@is_public"].Value = isPublic;
                using (SqlDataReader reader = database.pr_course_get_list.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new CourseDetailsDto
                        {
                            Id = reader.GetValueOrDefault<int>("id"),
                            Name = reader.GetString(reader.GetOrdinal("name")),
                            Authors = reader.GetString(reader.GetOrdinal("authors")),
                            PictureId = reader.GetValueOrDefault<int?>("picture_id"),
                            CategoryId = reader.GetValueOrDefault<int?>("category_id"),
                            Contacts = reader.GetString(reader.GetOrdinal("contacts")),
                            Description = reader.GetString(reader.GetOrdinal("description")),
                            DetailedDescription = reader.GetString(reader.GetOrdinal("detailed_description")),
                            GoogleAdvertisment = reader.GetString(reader.GetOrdinal("google_advertisement")),
                            IsApproved = reader.GetBoolean(reader.GetOrdinal("is_approved")),
                            IsCopied = reader.GetBoolean(reader.GetOrdinal("is_copied")),
                            IsEditable = reader.GetBoolean(reader.GetOrdinal("is_editable")),
                            IsPublic = reader.GetBoolean(reader.GetOrdinal("is_public")),
                            Links = reader.GetString(reader.GetOrdinal("links")),
                            LocationId = reader.GetValueOrDefault<int?>("location_id"),
                            Price = reader.GetDecimal(reader.GetOrdinal("price")),
                            Tags = reader.GetString(reader.GetOrdinal("tags")),
                            Url = reader.GetString(reader.GetOrdinal("url")),
                            CategoryName = reader.GetString(reader.GetOrdinal("category_name")),
                            UILanguageId = reader.GetValueOrDefault<int?>("ui_langauge_id"),
                            UILanguageName = reader.GetString(reader.GetOrdinal("ui_langauge_name")),
                            OwnerId = reader.GetValueOrDefault<int?>("owner_id"),
                            CurrencyName = reader.GetValueOrDefault<string>("currency_name")
                        });
                    }
                }
                return list;
            }
            finally
            {
                database.CloseConnection();
            }
        }
        
        /// <summary>
        /// Получает информацию о курсе вместе с модулем
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static CourseDetailsWithModulesInfoDto GetCourseDetailsWithModulesById(int accountId, int id)
        {
            Database database = null;
            CourseDetailsWithModulesInfoDto course;
            try
            {
                database = Database.GetInstance();
                course = GetCourseDetailsById<CourseDetailsWithModulesInfoDto>(database, accountId, id);
                if (course != null)
                {
                    course.Modules = GetModulesForCourse(database, accountId, id);
                }
            }
            finally
            {
                database.CloseConnection();
            }

            return course;
        }

        public static CourseDetailsDto GetCourseDetailsById(Database database, int accountId, int id)
        {
            return GetCourseDetailsById<CourseDetailsDto>(database, accountId, id);
        }

        /// <summary>
        /// получить курс по id
        /// </summary>
        /// <param name="database"></param>
        /// <param name="accountId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static T GetCourseDetailsById<T>(Database database, int accountId, int id)
            where T : CourseDetailsDto, new()
        {
            var courseGetCmd = database.pr_course_details_get;
            var courseGetCmdParams = courseGetCmd.Parameters;
            courseGetCmdParams["@account_id"].Value = accountId;
            courseGetCmdParams["@course_id"].Value = id;
            using (SqlDataReader reader = courseGetCmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    return
                        new T
                            {
                                Id = reader.GetValueOrDefault<int>("id"),
                                Number = reader.GetValueOrDefault<Guid>("number"),
                                Name = reader.GetValueOrDefault<string>("name"),
                                Description = reader.GetValueOrDefault<string>("description"),
                                DetailedDescription = reader.GetValueOrDefault<string>("detailed_description"),
                                //Logotype = reader.GetValueOrDefault<>()
                                PictureId = reader.GetValueOrDefault<int?>("picture_id"),
                                CurrencyId = reader.GetValueOrDefault<int>("currency_id"),
                                Price = reader.GetValueOrDefault<decimal>("price"),
                                UILanguageId = reader.GetValueOrDefault<int?>("ui_langauge_id"),
                                LocationId = reader.GetValueOrDefault<int?>("location_id"),
                                CategoryId = reader.GetValueOrDefault<int?>("category_id"),
                                Url = reader.GetValueOrDefault<string>("url"),
                                Authors = reader.GetValueOrDefault<string>("authors"),
                                Contacts = reader.GetValueOrDefault<string>("contacts"),
                                Tags = reader.GetValueOrDefault<string>("tags"),
                                Links = reader.GetValueOrDefault<string>("links"),
                                IsEditable = reader.GetValueOrDefault<bool>("is_editable"),
                                IsCopied = reader.GetValueOrDefault<bool>("is_copied"),
                                IsPublic = reader.GetValueOrDefault<bool>("is_public"),
                                Password = reader.GetValueOrDefault<string>("password"),
                                GoogleAdvertisment = reader.GetValueOrDefault<string>("google_advertisement"),
                                IsApproved = reader.GetValueOrDefault<bool>("is_approved"),
                                IsBuyedByCurrentUser =
                                    reader.GetValueOrDefault<bool>("is_buyed_by_current_user"),
                                RateByCurrentUser = reader.GetValueOrDefault<double?>("rate_by_current_user"),
                                CategoryName = reader.GetValueOrDefault<string>("category_name"),
                                UILanguageName = reader.GetValueOrDefault<string>("ui_langauge_name"),
                                OwnerId = reader.GetValueOrDefault<int?>("owner_id"),
                                CurrencyName = reader.GetValueOrDefault<string>("currency_name"),
                                IsAllChildsApproved = reader.GetValueOrDefault<bool>("is_all_childs_approved"),
                                StoredPasswordByUser = reader.GetValueOrDefault<string>("stored_password_by_user")
                            };
                }
            }
            return null;
        }

        public static CourseDetailsDto GetCourseDetailsById(int accountId, int id)
        {
            Database database = null;
            CourseDetailsDto course = null;
            try
            {
                database = Database.GetInstance();
                course = GetCourseDetailsById<CourseDetailsDto>(database, accountId, id);
            }
            finally
            {
                database.CloseConnection();
            }

            return course;
        }

        public static int UpdateCourseById(Database database, int accountId, Course course, bool isNeedDeletePicture)
        {
            var courseUpdateCmd = database.pr_course_update;
            var courseUpdateCmdParams = courseUpdateCmd.Parameters;

            courseUpdateCmdParams["@account_id"].Value = accountId;
            courseUpdateCmdParams["@course_id"].Value = course.Id;
            courseUpdateCmdParams["@number"].Value = course.Number;
            courseUpdateCmdParams["@name"].Value = course.Name;
            courseUpdateCmdParams["@description"].Value = course.Description;
            courseUpdateCmdParams["@detailed_description"].Value = course.DetailedDescription;
            courseUpdateCmdParams["@picture_id"].Value = isNeedDeletePicture ? -1 : course.PictureId;
            courseUpdateCmdParams["@currency_id"].Value = course.CurrencyId;
            courseUpdateCmdParams["@price"].Value = course.Price;
            courseUpdateCmdParams["@ui_langauge_id"].Value = course.UILanguageId;
            courseUpdateCmdParams["@location_id"].Value = course.LocationId;
            courseUpdateCmdParams["@category_id"].Value = course.CategoryId;
            courseUpdateCmdParams["@url"].Value = course.Url;
            courseUpdateCmdParams["@authors"].Value = course.Authors;
            courseUpdateCmdParams["@contacts"].Value = course.Contacts;
            courseUpdateCmdParams["@tags"].Value = course.Tags;
            courseUpdateCmdParams["@links"].Value = course.Links;
            courseUpdateCmdParams["@is_editable"].Value = course.IsEditable;
            courseUpdateCmdParams["@is_copied"].Value = course.IsCopied;
            courseUpdateCmdParams["@is_public"].Value = course.IsPublic;
            courseUpdateCmdParams["@password"].Value = course.Password;
            courseUpdateCmdParams["@google_advertisement"].Value = course.GoogleAdvertisment;
            courseUpdateCmdParams["@is_approved"].Value = course.IsApproved;
            courseUpdateCmdParams["@result_id"].Value = 0;

            courseUpdateCmd.ExecuteNonQuery();
            var resultId = courseUpdateCmdParams["@result_id"].Value as int?;

            return resultId.HasValue ? resultId.Value : -1;
        }

        /// <summary>
        /// Обновляет курс вместе с изображением тразакционно
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="course"></param>
        /// <returns></returns>
        public static int UpdateCourseWithPictureById(int accountId, CourseWithPictureUpdateDto course)
        {
            Database database = null;
            int resultId;
            try
            {
                database = Database.GetInstance();

                int pictureId = 0;
                if (!course.IsNeedRemovePicture && course.Picture != null)
                {
                    pictureId = PictureRelatedAccessor.UpdatePicture(accountId, course.Picture);

                    if (pictureId > 0)
                    {
                        course.PictureId = pictureId;
                    }
                    else
                    {
                        course.PictureId = null;
                    }
                }

                if (course.ModulesOrder != null && course.ModulesOrder.Any())
                {
                    UpdateModulesOrderOnServer(database, accountId, course.ModulesOrder);
                }

                if (course.ModuleIdsToDelete != null && course.ModuleIdsToDelete.Any())
                {
                    DeleteModulesByIds(database, accountId, course.ModuleIdsToDelete);
                }

                if (pictureId >= 0)
                {
                    resultId = UpdateCourseById(database, accountId, course, course.IsNeedRemovePicture);
                }
                else
                {
                    resultId = -1;
                }
            }
            finally
            {
                database.CloseConnection();
            }

            return resultId;
        }
        
        public static int UpdateCourseById(int accountId, Course course, bool isNeedDeletePicture)
        {
            Database database = null;
            int? resultId;
            try
            {
                database = Database.GetInstance();
                resultId = UpdateCourseById(database, accountId, course, isNeedDeletePicture);
            }
            finally
            {
                database.CloseConnection();
            }

            return resultId.HasValue ? resultId.Value : -1;
        }

        public static int UpdateModulesOrder(int accountId, ModuleOrderInfo[] info)
        {
            Database database = null;
            int? resultId;
            try
            {
                database = Database.GetInstance();
                resultId = UpdateModulesOrder(database, accountId, info);
            }
            finally
            {
                database.CloseConnection();
            }

            return resultId.HasValue ? resultId.Value : -1;
        }

        public static int UpdateModulesOrderOnServer(Database database, int accountId, ModuleOrderInfo[] info)
        {
            try
            {
                foreach (var orderInfo in info)
                {
                    var module = GetModuleById<ModuleWithPaymentInfoDto>(database, accountId, orderInfo.ModuleId);
                    if (module != null)
                    {
                        module.OrderInCourse = orderInfo.OrderIndex;
                        var result = UpdateModuleWithPicture(database, accountId, module, false);
                        if (result < 0)
                        {
                            //Todo: вопрос, что тут делать
                        }
                    }
                }
            }
            catch (Exception)
            {
                return -1;
            }
            return 0;
        }

        public static int UpdateModulesOrder(Database database, int accountId, ModuleOrderInfo[] info)
        {
            var orderingString = string.Join(";", info.Select(item => item.ModuleId.ToString() + "=" + item.OrderIndex.ToString()));

            var command = database.pr_modules_for_course_order;
            var cmdParams = command.Parameters;
            cmdParams["@account_id"].Value = accountId;
            cmdParams["@ordering_string"].Value = orderingString;
            var result = command.ExecuteScalar() as int?;
            if (result.HasValue)
            {
                return result.Value;
            }
            return -1;
        }

        public static int DeleteModulesByIds(Database database, int accountId, int[] ids)
        {
            foreach (var id in ids)
            {
                DeleteModule(database, accountId, id);
            }
            return 0;
        }
        
        public static int DeleteModule(Database database, int accountId, int moduleId)
        {
            var cmd = database.pr_module_delete;
            var cmdParams = cmd.Parameters;
            cmdParams["@account_id"].Value = accountId;
            cmdParams["@module_id"].Value = moduleId;
            var result = cmd.ExecuteScalar() as int?;
            if (result.HasValue)
            {
                return result.Value;
            }
            return -1;
        }

        public static ModuleWithPaymentInfoDto[] GetModulesForCourse(Database database, int accountId, int courseId)
        {
            var modules = new List<ModuleWithPaymentInfoDto>();

            var command = database.pr_modules_for_course_lst;
            var commandParams = command.Parameters;

            commandParams["@account_id"].Value = accountId;
            commandParams["@course_id"].Value = courseId;

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var module = new ModuleWithPaymentInfoDto
                    {
                        Id = reader.GetValueOrDefault<int>("id"),
                        Number = reader.GetValueOrDefault<Guid>("number"),
                        CourceId = reader.GetValueOrDefault<int>("course_id"),
                        Name = reader.GetValueOrDefault<string>("name"),
                        Description = reader.GetValueOrDefault<string>("description"),
                        DetailedDescription =
                            reader.GetValueOrDefault<string>("detailed_description"),
                        PictureId = reader.GetValueOrDefault<int?>("picture_id"),
                        CurrencyId = reader.GetValueOrDefault<int>("currency_id"),
                        Price = reader.GetValueOrDefault<decimal>("price"),
                        Url = reader.GetValueOrDefault<string>("url"),
                        Tags = reader.GetValueOrDefault<string>("tags"),
                        Links = reader.GetValueOrDefault<string>("links"),
                        OrderInCourse = reader.GetValueOrDefault<int>("order_in_course"),
                        IsPayd = reader.GetValueOrDefault<bool>("is_payd"),
                        ExerciseMaxNumber = reader.GetValueOrDefault<int>("exercise_max_number"),
                        CurrencyName = reader.GetValueOrDefault<string>("currency_name"),
                        OwnerId = reader.GetValueOrDefault<int?>("owner_id"),
                        PriceInCourseCurrency = reader.GetValueOrDefault<decimal>("price_in_course_currency"),
                        TotalRate = reader.GetValueOrDefault<double?>("total_rate")
                    };
                    modules.Add(module);
                }
            }
            return modules.ToArray();
        }

        public static ModuleWithPaymentInfoDto[] GetModulesForCourse(int accountId, int courseId)
        {
            Database database = null;
            ModuleWithPaymentInfoDto[] modules;
            try
            {
                database = Database.GetInstance();
                modules = GetModulesForCourse(database, accountId, courseId);
            }
            finally
            {
                database.CloseConnection();
            }

            return modules ?? new ModuleWithPaymentInfoDto[] { };
        }

        public static T GetModuleById<T>(Database database, int accountId, int moduleId)
            where T : ModuleWithPaymentInfoDto, new()
        {
            var command = database.pr_module_get;
            var commandParams = command.Parameters;

            commandParams["@account_id"].Value = accountId;
            commandParams["@module_id"].Value = moduleId;

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    return new T
                               {
                                   Id = reader.GetValueOrDefault<int>("id"),
                                   Number = reader.GetValueOrDefault<Guid>("number"),
                                   CourceId = reader.GetValueOrDefault<int>("course_id"),
                                   Name = reader.GetValueOrDefault<string>("name"),
                                   Description = reader.GetValueOrDefault<string>("description"),
                                   DetailedDescription =
                                       reader.GetValueOrDefault<string>("detailed_description"),
                                   PictureId = reader.GetValueOrDefault<int?>("picture_id"),
                                   CurrencyId = reader.GetValueOrDefault<int>("currency_id"),
                                   Price = reader.GetValueOrDefault<decimal>("price"),
                                   Url = reader.GetValueOrDefault<string>("url"),
                                   Tags = reader.GetValueOrDefault<string>("tags"),
                                   Links = reader.GetValueOrDefault<string>("links"),
                                   OrderInCourse = reader.GetValueOrDefault<int>("order_in_course"),
                                   IsPayd = reader.GetValueOrDefault<bool>("is_payd"),
                                   ExerciseMaxNumber = reader.GetValueOrDefault<int>("exercise_max_number"),
                                   CurrencyName = reader.GetValueOrDefault<string>("currency_name"),
                                   RateByCurrentUser = reader.GetValueOrDefault<double?>("rate_by_current_user"),
                                   OwnerId = reader.GetValueOrDefault<int?>("owner_id")
                               };
                }
            }
            return null;
        }

        public static ModuleWithPaymentInfoDto GetModuleById(int accountId, int moduleId)
        {
            Database database = null;
            ModuleWithPaymentInfoDto module = null;
            try
            {
                database = Database.GetInstance();

                module = GetModuleById<ModuleWithPaymentInfoDto>(database, accountId, moduleId);
            }
            finally
            {
                database.CloseConnection();
            }

            return module;
        }

        public static ModuleWithPaymentAndExercisesInfoDto GetModuleWithExercisesById(int accountId, int moduleId)
        {
            Database database = null;
            ModuleWithPaymentAndExercisesInfoDto module = null;
            try
            {
                database = Database.GetInstance();

                module = GetModuleById<ModuleWithPaymentAndExercisesInfoDto>(database, accountId, moduleId);
                if (module != null)
                {
                    module.Exercises = ExerciseRelatedAccessor.GetExercisesBaseArray(database, accountId, moduleId);
                }
            }
            finally
            {
                database.CloseConnection();
            }

            return module;
        }

        public static int UpdateModuleWithPicture(Database database, int accountId, Module module, bool isNeedDeletePicture)
        {
            var command = database.pr_module_update;
            var cmdParams = command.Parameters;
            cmdParams["@account_id"].Value = accountId;
            cmdParams["@module_id"].Value = module.Id;
            cmdParams["@course_id"].Value = module.CourceId;
            cmdParams["@name"].Value = module.Name;
            cmdParams["@description"].Value = module.Description;
            cmdParams["@detailed_description"].Value = module.DetailedDescription;
            cmdParams["@picture_id"].Value = isNeedDeletePicture ? -1 : module.PictureId;
            cmdParams["@currency_id"].Value = module.CurrencyId;
            cmdParams["@price"].Value = module.Price;
            cmdParams["@url"].Value = module.Url;
            cmdParams["@tags"].Value = module.Tags;
            cmdParams["@links"].Value = module.Links;
            cmdParams["@exercise_max_number"].Value = module.ExerciseMaxNumber;
            cmdParams["@order_in_course"].Value = module.OrderInCourse;

            var result = command.ExecuteScalar() as int?;

            if (result.HasValue)
            {
                return result.Value;
            }
            else
            {
                return -1;
            }
        }

        public static int UpdateModuleWithPicture(int accountId, ModuleWithPictureUpdateDto module)
        {
            Database database = null;
            int resultId = -1;
            try
            {
                database = Database.GetInstance();

                int pictureId = 0;
                if (!module.IsNeedRemovePicture && module.Picture != null)
                {
                    pictureId = PictureRelatedAccessor.UpdatePicture(database, accountId, module.Picture);

                    if (pictureId > 0)
                    {
                        module.PictureId = pictureId;
                    }
                    else
                    {
                        module.PictureId = null;
                    }
                }

                UpdateExercisesOrder(database, accountId, module.ExercisesOrder);

                if (pictureId >= 0)
                {
                    resultId = UpdateModuleWithPicture(database, accountId, module, module.IsNeedRemovePicture);
                }
                else
                {
                    resultId = -1;
                }
            }
            finally
            {
                database.CloseConnection();
            }

            return resultId;
        }

        public static int UpdateExercisesOrder(Database database, int accountId, ExerciseOrderInfo[] exerciseOrderInfos)
        {
            var orderingString = string.Join(";",
                                             exerciseOrderInfos.Select(
                                                 row =>
                                                 row.ExerciseId + "," + (int) row.ExerciseType + "=" + row.OrderIndex));
            
            database = Database.GetInstance();
            var command = database.pr_exercises_for_module_order;
            var cmdParams = command.Parameters;
            cmdParams["@account_id"].Value = accountId;
            cmdParams["@ordering_string"].Value = orderingString;
            var result = command.ExecuteScalar() as int?;
            if (result.HasValue)
            {
                return result.Value;
            }
            return -1;
        }

        public static int CourseApprove(Database database, int accountId, int courseId)
        {
            var command = database.pr_course_approve;
            var cmdParams = command.Parameters;
            cmdParams["@account_id"].Value = accountId;
            cmdParams["@course_id"].Value = courseId;
            command.ExecuteNonQuery();
            var retValue = cmdParams["@RETURN_VALUE"].Value as int?;

            if (retValue.HasValue)
            {
                return retValue.Value;
            }

            return -1;
        }

        public static int CourseApprove(int accountId, int courseId)
        {
            int resultId = -1;
            Database database = null;
            try
            {
                database = Database.GetInstance();
                resultId = CourseApprove(database, accountId, courseId);
            }
            finally
            {
                database.CloseConnection();
            }

            return resultId;
        }

        public static int CheckAndUpdatePasswordForCource(int accountId, int courseId, string password)
        {
            int resultId = -1;
            Database database = null;
            try
            {
                database = Database.GetInstance();
                resultId = CheckAndUpdatePasswordForCource(database, accountId, courseId, password);
            }
            finally
            {
                database.CloseConnection();
            }

            return resultId;
        }

        public static int CheckAndUpdatePasswordForCource(Database database, int accountId, int courseId, string password)
        {
            var command = database.pr_user_course_password_check_and_update;
            var cmdParams = command.Parameters;
            cmdParams["@user_id"].Value = accountId;
            cmdParams["@course_id"].Value = courseId;
            cmdParams["@password"].Value = password;
            command.ExecuteNonQuery();
            var retValue = cmdParams["@RETURN_VALUE"].Value as int?;

            if (retValue.HasValue)
            {
                return retValue.Value;
            }

            return -1;
        }
    }
}
