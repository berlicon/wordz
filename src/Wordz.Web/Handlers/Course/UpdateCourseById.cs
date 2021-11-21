using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.SessionState;
using Wordz.BC.Validation;
using Wordz.BE;
using Wordz.BE.Dto;
using Wordz.DB.Accessors;
using Wordz.Web.Helpers;
using Wordz.Lng;

namespace Wordz.Web.Handlers.Course
{
    public class UpdateCourseById : IHttpHandler, IRequiresSessionState
    {
        private HttpRequest Request { get; set; }
        private HttpResponse Response { get; set; }
        private ValidationList ValidationList { get; set; }

        public void ProcessRequest(HttpContext context)
        {
            context.WriteErrorIfNotLoggedAndExit();

            context.IgnoreValidationException();

            Request = context.Request;
            Response = context.Response;
            ValidationList = new ValidationList();

            var account = StorageManager.CurrentAccount;

            var course = getCourseFromParams(Request.Form);
            if (course != null && course.Id.HasValue)
            {
                var pictureFile = Request.Files["picture"];

                if (pictureFile != null
                    && pictureFile.ContentLength > 0
                    && !string.IsNullOrEmpty(pictureFile.FileName))
                {
                    var picture = new Picture { Data = new byte[pictureFile.ContentLength] };
                    pictureFile.InputStream.Read(picture.Data, 0, picture.Data.Length);

                    course.Picture = picture;
                }

                course.Id = CoursesRelatedAccessor.UpdateCourseWithPictureById(account.Id, course);
                context.WriteSuccessAndExit(CurrentLanguage.UpdateSuccessful, JsonHelper.JsonSerializer(course.Id));
            }
            else
            {
                context.WriteErrorAndExit(CurrentLanguage.ValidationErrors, JsonHelper.JsonSerializer(ValidationList));
            }
        }

        private IEnumerable<int> getDeletedModulesFromParams(NameValueCollection collection)
        {
            var delItemsIdString = collection["moduleDeletedListId"];
            foreach (var strItem in delItemsIdString.Split(';'))
            {
                int moduleId;
                if (int.TryParse(strItem, out moduleId))
                {
                    yield return moduleId;
                }
            }
        }

        /// <summary>
        /// Получение курса из потока и его обновление
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        private CourseWithPictureUpdateDto getCourseFromParams(NameValueCollection collection)
        {
            var nfi = new NumberFormatInfo()
                          {CurrencyDecimalSeparator = ".", NumberDecimalSeparator = ".", PercentDecimalSeparator = "."};

            string authors = collection["authors"];
            if (authors.Length > 1000)
            {
                ValidationList.Add(new ValidationForPropertyMessage(CurrentLanguage.AuthorsShouldBeLess1000, "authors"));
            }

            int categoryId;
            if (!int.TryParse(collection["category"], NumberStyles.Integer, nfi, out categoryId))
            {
                ValidationList.Add(new ValidationForPropertyMessage(CurrentLanguage.NoCategory, "category"));
            }
            
            string contacts = collection["contacts"];
            if (contacts.Length > 1000)
            {
                ValidationList.Add(new ValidationForPropertyMessage(CurrentLanguage.ContactsShouldBeLess1000, "contacts"));
            }

            int currencyId;
            if (!int.TryParse(collection["currency"], NumberStyles.Integer, nfi, out currencyId))
            {
                ValidationList.Add(new ValidationForPropertyMessage(CurrentLanguage.NoCurrency, "currency"));
            }
            string description = collection["description"];
            if (description.Length > 300)
            {
                ValidationList.Add(new ValidationForPropertyMessage(CurrentLanguage.DescriptionShouldBeLess300, "description"));
            }
            string detailDescription = collection["detailDescription"];
            if (detailDescription.Length > 15000)
            {
                ValidationList.Add(new ValidationForPropertyMessage(CurrentLanguage.DetailedDescriptionShouldBeLess15000, "detailDescription"));
            }
            string adsence = collection["adsence"];

            int id;
            if (!int.TryParse(collection["courseId"], NumberStyles.Integer, nfi, out id))
            {
                return null;
            }
            bool isCopyable = collection["isCopyable"] == "1";
            bool isEditable = collection["isEditable"] == "1";
            bool isPublic = collection["isPublic"] == "1";

            string links = collection["links"];
            if (links.Length > 4000)
            {
                ValidationList.Add(new ValidationForPropertyMessage(CurrentLanguage.LinksShouldBeLess4000, "links"));
            }
            string name = collection["name"];
            if (name.Length > 100)
            {
                ValidationList.Add(new ValidationForPropertyMessage(CurrentLanguage.NameShouldBeLess100, "name"));
            }

            if (name.Length == 0 || name.Trim().Length == 0)
            {
                ValidationList.Add(new ValidationForPropertyMessage(CurrentLanguage.NameShouldBeNotEmpty, "name"));
            }

            decimal price;
            if (!decimal.TryParse(collection["price"], NumberStyles.Float, nfi, out price))
            {
                ValidationList.Add(new ValidationForPropertyMessage(CurrentLanguage.PriceShouldBePositive, "price"));
            }

            if (price < 0)
            {
                ValidationList.Add(new ValidationForPropertyMessage(CurrentLanguage.PriceShouldBePositive, "price"));
            }

            string tags = collection["tags"];
            if (tags.Length > 1000)
            {
                ValidationList.Add(new ValidationForPropertyMessage(CurrentLanguage.TagsShouldBeLess1000, "tags"));
            }

            int langId;
            if (!int.TryParse(collection["language"], NumberStyles.Number, nfi, out langId))
            {
                ValidationList.Add(new ValidationForPropertyMessage(CurrentLanguage.NoLanguage, "language"));
            }

            string url = collection["url"];
            if (url.Length > 100)
            {
                ValidationList.Add(new ValidationForPropertyMessage(CurrentLanguage.UrlShouldBeLess100, "url"));
            }

            var isNeedRemovePicture = Request.Form["isRemovePicture"] == "1";

            var listOfOrderInfo = new List<ModuleOrderInfo>();

            //Получени информации о сортировке модулей
            var regex = new Regex(@"^module_(\d+)$");
            foreach (var param in collection.AllKeys)
            {
                var match = regex.Match(param);
                if (match.Success)
                {
                    int itemId;
                    if (int.TryParse(match.Groups[1].Value, out itemId))
                    {
                        int orderIndex;
                        if (int.TryParse(collection[param], out orderIndex))
                        {
                            listOfOrderInfo.Add(new ModuleOrderInfo { ModuleId = itemId, OrderIndex = orderIndex });
                        }
                    }
                }
            }

            if (ValidationList.Any())
            {
                return null;
            }

            //Получение и сравнение паролей

            var password = collection["firstPassword"];
            if (password.Length > 100)
            {
                ValidationList.Add(new ValidationForPropertyMessage(CurrentLanguage.PasswordShouldBeLess100, "password"));
            }

            var moduleIdsToDelete = getDeletedModulesFromParams(Request.Form).ToArray();

            var updatingCourse = new CourseWithPictureUpdateDto
                                     {
                                         Authors = authors,
                                         CategoryId = categoryId,
                                         Contacts = contacts,
                                         CurrencyId = currencyId,
                                         Description = description,
                                         DetailedDescription = detailDescription,
                                         GoogleAdvertisment = adsence,
                                         Id = id,
                                         IsCopied = isCopyable,
                                         IsEditable = isEditable,
                                         IsPublic = isPublic,
                                         Links = links,
                                         Name = name,
                                         Price = price,
                                         Tags = tags,
                                         UILanguageId = langId,
                                         Url = url,
                                         LocationId = null,
                                         Password = password,
                                         PictureId = null,
                                         IsNeedRemovePicture = isNeedRemovePicture,
                                         ModulesOrder = listOfOrderInfo.ToArray(),
                                         ModuleIdsToDelete = moduleIdsToDelete
                             };
            if (id > 0)
            {
                var existingCourse = CoursesRelatedAccessor.GetCourseDetailsById(StorageManager.CurrentAccountId, id);
                if (existingCourse == null)
                {
                    ValidationList.AddValidationMessage("none", CurrentLanguage.NoCourse);
                    return null;
                }
                updatingCourse.Number = existingCourse.Number;
                updatingCourse.OwnerId = existingCourse.OwnerId;
                updatingCourse.IsApproved = existingCourse.IsApproved;
            }

            return updatingCourse;
        }

        public bool IsReusable
        {
            get { return false; }
        }
    }
}