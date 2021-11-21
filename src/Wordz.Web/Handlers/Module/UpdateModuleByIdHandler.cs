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

namespace Wordz.Web.Handlers.Module
{
    public class UpdateModuleByIdHandler : IHttpHandler, IRequiresSessionState
    {
        private ValidationList ValidationList { get; set; }

        public void ProcessRequest(HttpContext context)
        {
            context.WriteErrorIfNotLoggedAndExit();

            context.IgnoreValidationException();

            var account = StorageManager.CurrentAccount;

            ValidationList = new ValidationList();

            var module = getModuleFromParamsOrNull(context.Request.Form);
            if (module != null)
            {
                var pictureFile = context.Request.Files["picture"];

                if (pictureFile != null
                    && pictureFile.ContentLength > 0
                    && !string.IsNullOrEmpty(pictureFile.FileName))
                {
                    var picture = new Picture { Data = new byte[pictureFile.ContentLength] };
                    pictureFile.InputStream.Read(picture.Data, 0, picture.Data.Length);

                    module.Picture = picture;
                }
                var updateResult = CoursesRelatedAccessor.UpdateModuleWithPicture(account.Id, module);
                if (updateResult >= 0)
                {
                    module.Id = updateResult;
                    context.WriteSuccessAndExit(CurrentLanguage.UpdateSuccessful, JsonHelper.JsonSerializer(module.Id));
                }
                else
                {
                    context.WriteErrorAndExit(CurrentLanguage.CantUpdateModule);
                }
            }
            else
            {
                context.WriteErrorAndExit(CurrentLanguage.ValidationErrors, JsonHelper.JsonSerializer(ValidationList));
            }
        }

        public bool IsReusable
        {
            get { return true; }
        }

        private ModuleWithPictureUpdateDto getModuleFromParamsOrNull(NameValueCollection collection)
        {
            var nfi = new NumberFormatInfo()
                          {
                              CurrencyDecimalSeparator = ".",
                              NumberDecimalSeparator = ".",
                              PercentDecimalSeparator = "."
                          };

            int currencyId;
            if (!int.TryParse(collection["currency"], NumberStyles.Integer, nfi, out currencyId))
            {
                return null;
            }
            string description = collection["description"];
            if (description.Length > 300)
            {
                ValidationList.Add(new ValidationForPropertyMessage(CurrentLanguage.DescriptionShouldBeLess300, "description"));
            }
            string detailDescription = collection["detailDescription"];

            int moduleId;
            if (!int.TryParse(collection["moduleId"], NumberStyles.Integer, nfi, out moduleId))
            {
                return null;
            }

            int courseId;
            if (!int.TryParse(collection["courseId"], NumberStyles.Integer, nfi, out courseId))
            {
                return null;
            }

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
                ValidationList.AddValidationMessage("name", CurrentLanguage.NameShouldBeNotEmpty);
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
            
            string url = collection["url"];
            if (url.Length > 100)
            {
				ValidationList.Add(new ValidationForPropertyMessage(CurrentLanguage.UrlShouldBeLess100, "url"));
            }

            var isNeedRemovePicture = collection["isRemovePicture"] == "1";

            //Получени информации о сортировке упражнений
            var listOfOrderInfo = new List<ExerciseOrderInfo>();
            var regex = new Regex(@"^exercise_(\d+)_(\d+)$");
            foreach (var param in collection.AllKeys)
            {
                var match = regex.Match(param);
                if (match.Success)
                {
                    int itemId;
                    int itemType;
                    int orderIndex;
                    if (int.TryParse(match.Groups[1].Value, out itemId)
                        && int.TryParse(match.Groups[2].Value, out itemType)
                        && int.TryParse(collection[param], out orderIndex))
                    {
                        listOfOrderInfo.Add(new ExerciseOrderInfo
                                                {
                                                    ExerciseId = itemId,
                                                    ExerciseType = (ExerciseType) itemType,
                                                    OrderIndex = orderIndex
                                                });
                    }
                }
            }

            if (ValidationList.Any())
            {
                return null;
            }

            var updatingModule = new ModuleWithPictureUpdateDto
                                     {
                                         CourceId = courseId,
                                         CurrencyId = currencyId,
                                         Description = description,
                                         DetailedDescription = detailDescription,
                                         Id = moduleId,
                                         Links = links,
                                         Tags = tags,
                                         Name = name,
                                         Price = price,
                                         Url = url,
                                         IsNeedRemovePicture = isNeedRemovePicture,
                                         ExercisesOrder = listOfOrderInfo.ToArray(),
                                     };
            if (moduleId > 0)
            {
                var existingModule = CoursesRelatedAccessor.GetModuleById(StorageManager.CurrentAccountId, moduleId);
                if (existingModule == null)
                {
                    ValidationList.AddValidationMessage("none", CurrentLanguage.NoModule);
                    return null;
                }
                // Обновляем поля, которые не указаны юзером
                updatingModule.ExerciseMaxNumber = existingModule.ExerciseMaxNumber;
                updatingModule.Number = existingModule.Number;
                updatingModule.OrderInCourse = existingModule.OrderInCourse;

            }
            return updatingModule;
        }
    }
}