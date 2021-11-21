using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.SessionState;
using Wordz.BC;
using Wordz.BE;
using Wordz.BE.Dto;
using Wordz.DB.Accessors;
using Wordz.Web.Helpers;
using Wordz.Lng;

namespace Wordz.Web.Handlers.Exercise
{
    public class ExerciseSelectAddHandler : IHttpHandler, IRequiresSessionState
    {
        private HttpRequest Request { get; set; }
        private HttpResponse Response { get; set; }

        public void ProcessRequest(HttpContext context)
        {
            context.WriteErrorIfNotLoggedAndExit();

            Request = context.Request;
            Response = context.Response;

            List<int> existIds;
            var exercise = GetExerciseFromParams(Request.Form, out existIds);
            if (exercise != null)
            {
                var accountId = StorageManager.UserLogined ? StorageManager.CurrentAccount.Id : BCCommon.AnonymousAccountId;
                if (exercise.Id != 0)
                {
                    exercise.Id = ExerciseRelatedAccessor.UpdateExerciseSelect(exercise, accountId);
                    SaveAnswers(exercise, existIds);
                    context.WriteSuccessAndExit("Update");
                }
                else
                {
                    exercise.Name = string.IsNullOrEmpty(exercise.Name) ? "Name" : exercise.Name;
                    exercise.Description = string.IsNullOrEmpty(exercise.Description) ? "Description" : exercise.Description;
                    exercise.Text = string.IsNullOrEmpty(exercise.Text) ? "Text" : exercise.Text;
                    exercise = (ExerciseSelectDto) ExerciseRelatedAccessor.AddExerciseSelect(exercise, accountId);
                    SaveAnswers(exercise, existIds);
                    context.WriteSuccessAndExit("Save", JsonHelper.JsonSerializer(exercise));
                }
            }
            else
            {
                context.WriteErrorAndExit(CurrentLanguage.Errors);
            }
        }

        private void SaveAnswers(ExerciseSelectDto exercise, List<int> existIds)
        {
            var listToDelete = exercise.Answers.Where(x => !existIds.Exists(y => y == x.Id) && x.Id != 0).Select(x => x).ToList();
            var listToUpd = exercise.Answers.Where(x => existIds.Exists(y => y == x.Id)).Select(x => x).ToList();
            var listToAdd = exercise.Answers.Where(x => x.Id == 0).ToList();

            //сохраняем ответы
            foreach (var answer in listToDelete)
            {
                ExerciseRelatedAccessor.DeleteAnswerById(answer.Id);
            }
            foreach (var answer in listToUpd)
            {
                answer.ExerciseId = exercise.Id;
                ExerciseRelatedAccessor.UpdateAnswer(answer);
            }
            foreach (var answer in listToAdd)
            {
                answer.ExerciseId = exercise.Id;
                ExerciseRelatedAccessor.AddAnswer(answer);
            }
        }

        private ExerciseSelectDto GetExerciseFromParams(NameValueCollection collection, out List<int> existIds)
        {
            existIds = new List<int>();
            int exerciseId;
            if (!int.TryParse(collection["SelectText"], out exerciseId))
            {
                return null;
            }
            int moduleId;
            if (!int.TryParse(collection["SelectModule"], out moduleId))
            {
                return null;
            }
            var exercise = new ExerciseSelectDto
            {
                Id = exerciseId,
                Name = collection["questionName"],
                Description = collection["questionDescription"],
                Text = collection["questionText"],
                ModuleId = moduleId
            };

            var answers = ExerciseRelatedAccessor.GetExerciseSelectAnswers(StorageManager.CurrentAccountId, exerciseId);

            //получаем все ответы
            var regex = new Regex("text_[0-9]+");

            var numList = new List<int>();
            foreach (var item in collection.Keys.OfType<string>())
            {
                if (regex.IsMatch(item))
                {
                    int currentNumber;
                    var numberString = item.Substring(5);
                    if (int.TryParse(numberString, out currentNumber)
                        && !numList.Contains(currentNumber))
                    {
                        numList.Add(currentNumber);
                    }
                }
            }

            foreach (var itemNumber in numList)
            {
                int itemId;
                var text = collection["text_" + itemNumber];
                var isAnswer = collection["isAnswer_" + itemNumber].ReturnStringIfNull("false");
                var isNeedDeleteImage = collection["isNeedDeleteImage_" + itemNumber] == "1";

                int? pictureId = null;
                var pictureFile = Request.Files["image_" + itemNumber];

                if (pictureFile != null
                    && pictureFile.ContentLength > 0
                    && !string.IsNullOrEmpty(pictureFile.FileName)
                    && !isNeedDeleteImage)
                {
                    var picture = new Picture {Data = new byte[pictureFile.ContentLength]};
                    pictureFile.InputStream.Read(picture.Data, 0, picture.Data.Length);
                    pictureId = PictureRelatedAccessor.UpdatePicture(StorageManager.CurrentAccountId, picture);
                }

                if (int.TryParse(collection["answerId_" + itemNumber], out itemId))
                {
                    var newItem = new AnswerDto
                                      {
                                          Id = itemId,
                                          IsRight = Convert.ToBoolean(isAnswer != "false" ? "true" : isAnswer),
                                          Text = text,
                                          PictureId = pictureId
                                      };
                    if (itemId == 0)
                    {
                        answers.Add(newItem);
                    }
                    else
                    {
                        var answerDto = answers.FirstOrDefault(x => x.Id == itemId);
                        if (answerDto != null)
                        {
                            answerDto.IsRight = newItem.IsRight;
                            answerDto.Text = newItem.Text;
                            if (isNeedDeleteImage || pictureId != null)
                            {
                                answerDto.PictureId = pictureId;
                            }
                            //answers.Remove(answerDto);
                            //answers.Add(newItem);
                            existIds.Add(itemId);
                        }
                    }
                }
            }
            exercise.Answers = answers;
            return exercise;
        }

        public bool IsReusable
        {
            get { return false; }
        }
    }
}