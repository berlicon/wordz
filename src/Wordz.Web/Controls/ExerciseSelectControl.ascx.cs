using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI;
using Wordz.BC;
using Wordz.BE;
using Wordz.BE.Dto;
using Wordz.DB.Accessors;
using Wordz.Web.Helpers;

namespace Wordz.Web.Controls
{
    public partial class ExerciseSelectControl : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //submitExerciseSelectToServerBtn.Click += new EventHandler(submitToServerBtn_Click);

            //int exerciseId;
            //if (int.TryParse(Request.QueryString["Id"], out exerciseId))
            //{
            //    var accountId = StorageManager.UserLogined ? StorageManager.CurrentAccount.Id : BCCommon.AnonymousAccountId;
            //    var exerciseText = ExerciseRelatedAccessor.GetExerciseSelectById(exerciseId, accountId);
            //    if (exerciseText != null)
            //    {
            //        ExerciseT = exerciseText;
            //    }
            //    else
            //    {
            //        ExerciseT = new ExerciseSelectDto()
            //        {
            //            Answers = new List<AnswerDto>()
            //        };
            //    }
            //}
            //else
            //{
                ExerciseT = new ExerciseSelectDto()
                {
                    Answers = new List<AnswerDto>()
                };
            //}
            //int moduleId;
            //if (int.TryParse(Request.QueryString["ModuleId"], out moduleId))
            //{
            //    //SelectModuleId.Value = moduleId.ToString();
            //    ExerciseT.ModuleId = moduleId;
            //}
            //else
            //{
            //    Response.Write("{'status': 'ERR','msg': 'Не определен модуль!'}");
            //    Response.Redirect("/");
            //}
        }

        //void submitToServerBtn_Click(object sender, EventArgs e)
        //{
        //    ExerciseT.Name = Request.Params["questionName"];
        //    ExerciseT.Description = Request.Params["questionDescription"];
        //    ExerciseT.Text = Request.Params["questionText"];

        //    //изображение для вопроса
        //    var questionPicture = Request.Files["questionFile"];

        //    if (Request.Params["changeQuestionPicture"] == "+1")
        //    {
        //        if (questionPicture != null)
        //        {
        //            var questionBytes = new byte[questionPicture.ContentLength];
        //            questionPicture.InputStream.Read(questionBytes, 0, questionPicture.ContentLength);
        //            questionPicture.InputStream.Flush();

        //            if (ExerciseT.PictureId == 0)
        //            {
        //                ExerciseT.QuestionPicture = new Picture
        //                {
        //                    Data = questionBytes
        //                };
        //            }
        //            else
        //            {
        //                ExerciseT.QuestionPicture.Data = questionBytes;
        //            }
        //            //сохраняем картинку
        //            ExerciseT.QuestionPicture.Id =
        //                ExerciseT.PictureId = PictureRelatedAccessor.UpdatePicture(0, ExerciseT.QuestionPicture);
        //        }
        //    }
        //    if (Request.Params["changeQuestionPicture"] == "-1")
        //    {
        //        if (ExerciseT.Id != 0)
        //        {
        //            //удаляем изображение из базы
        //            //TODO: удаление изображения!
        //        }
        //        ExerciseT.QuestionPicture = null;
        //        ExerciseT.PictureId = null;
        //    }

        //    var accountId = StorageManager.UserLogined ? StorageManager.CurrentAccount.Id : BCCommon.AnonymousAccountId;
        //    //сохраняем само упражнение
        //    if (ExerciseT.Id == 0)
        //    {
        //        var savedExercise = ExerciseRelatedAccessor.AddExerciseSelect(ExerciseT, accountId);
        //        ExerciseT.Id = savedExercise.Id;
        //    }
        //    else
        //    {
        //        ExerciseT.Id = ExerciseRelatedAccessor.UpdateExerciseSelect(ExerciseT, accountId);
        //    }

        //    //получаем все ответы
        //    NameValueCollection collection = Request.Params;
        //    var regex = new Regex("text_[0-9]+");

        //    var numList = new List<int>();
        //    foreach (var item in collection.Keys.OfType<string>())
        //    {
        //        if (regex.IsMatch(item))
        //        {
        //            int currentNumber;
        //            var numberString = item.Substring(5);
        //            if (int.TryParse(numberString, out currentNumber)
        //                && !numList.Contains(currentNumber))
        //            {
        //                numList.Add(currentNumber);
        //            }
        //        }
        //    }
        //    var answerDtos = ExerciseT.Answers.ToList();
        //    var existIds = new List<int>();

        //    foreach (var itemNumber in numList)
        //    {
        //        int itemId;
        //        var text = collection["text_" + itemNumber];
        //        var picture = Request.Files["myFile_" + itemNumber];
        //        var isAnswer = collection["isAnswer_" + itemNumber].ReturnStringIfNull("false");

        //        if (int.TryParse(collection["answerId_" + itemNumber], out itemId))
        //        {
        //            if (Request.Params["changePicture_" + itemNumber] == "+1")
        //            {
        //                if (picture != null)
        //                {
        //                    var bytes = new byte[picture.ContentLength];
        //                    picture.InputStream.Read(bytes, 0, picture.ContentLength);
        //                    picture.InputStream.Flush();

        //                    if (itemId == -1)
        //                    {
        //                        var newItem = new AnswerDto
        //                        {
        //                            IsRight = Convert.ToBoolean(isAnswer),
        //                            Picture = new Picture
        //                            {
        //                                Data = bytes
        //                            },
        //                            Text = text
        //                        };

        //                        //сохраняем картинку
        //                        newItem.Picture.Id = newItem.PictureId =
        //                                             PictureRelatedAccessor.UpdatePicture(0, newItem.Picture);
        //                        answerDtos.Add(newItem);
        //                    }
        //                    else
        //                    {
        //                        existIds.Add(itemId);
        //                        var oldItem = answerDtos.FirstOrDefault(x => x.Id == itemId);
        //                        if (oldItem != null)
        //                        {
        //                            oldItem.IsRight = Convert.ToBoolean(isAnswer);
        //                            oldItem.Picture = new Picture
        //                            {
        //                                Data = bytes
        //                            };
        //                            oldItem.Text = text;
        //                            //сохраняем картинку
        //                            oldItem.Picture.Id = oldItem.PictureId =
        //                                                 PictureRelatedAccessor.UpdatePicture(0, oldItem.Picture);
        //                        }
        //                    }
        //                }
        //            }
        //            if (Request.Params["changePicture_" + itemNumber] == "-1")
        //            {
        //                if (itemId == -1)
        //                {
        //                    var newItem = new AnswerDto
        //                    {
        //                        IsRight = Convert.ToBoolean(isAnswer),
        //                        PictureId = null,
        //                        Picture = null,
        //                        Text = text
        //                    };
        //                    answerDtos.Add(newItem);
        //                }
        //                else
        //                {
        //                    existIds.Add(itemId);
        //                    var oldItem = answerDtos.FirstOrDefault(x => x.Id == itemId);
        //                    if (oldItem != null)
        //                    {
        //                        oldItem.IsRight = Convert.ToBoolean(isAnswer);
        //                        oldItem.Picture = null;
        //                        oldItem.Text = text;
        //                        oldItem.PictureId = null;
        //                    }
        //                    //удаляем изображение из базы
        //                    //TODO: удаление изображения!
        //                }
        //            }
        //        }
        //    }

        //    var listToDelete = answerDtos.Where(x => !existIds.Exists(y => y == x.Id && x.Id != 0)).Select(x => x).ToList();
        //    var listToUpd = answerDtos.Where(x => existIds.Exists(y => y == x.Id)).Select(x => x).ToList();
        //    var listToAdd = answerDtos.Where(x => x.Id == 0).ToList();

        //    //сохраняем ответы
        //    foreach (var answer in listToDelete)
        //    {
        //        ExerciseRelatedAccessor.DeleteAnswerById(answer.Id);
        //    }
        //    foreach (var answer in listToUpd)
        //    {
        //        ExerciseRelatedAccessor.UpdateAnswer(answer);
        //    }
        //    foreach (var answer in listToAdd)
        //    {
        //        answer.ExerciseId = ExerciseT.Id;
        //        ExerciseRelatedAccessor.AddAnswer(answer);
        //    }
        //}

        protected ExerciseSelectDto ExerciseT { get; set; }
    }
}