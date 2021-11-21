using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.SessionState;
using Wordz.BC;
using Wordz.BE.Dto;
using Wordz.DB;
using Wordz.DB.Accessors;
using Wordz.Lng;
using Wordz.Web.Helpers;

namespace Wordz.Web.Handlers.OnlineFilm
{
    public class UpdateFilmChannelsHandler : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            context.WriteErrorIfNotLoggedAndExit();

            try
            {
                context.Request.Form.ToString();
            }
            catch (HttpRequestValidationException e)
            {
                //пропускаем ошибку валидации
                //потом обязательно идет проверка параметров!
            }

            var account = StorageManager.CurrentAccount;

            var idReplacementDictionary = new Dictionary<int, int>();

            var actionRegex = new Regex(@"^action_(\w+)_(-?\d+)$");
            foreach (var param in context.Request.Params.AllKeys)
            {
                var match = actionRegex.Match(param);
                if (match.Success)
                {
                    var actionType = match.Groups[1].Value;
                    int targetId;
                    if (int.TryParse(match.Groups[2].Value, out targetId))
                    {
                        var channelJSON = context.Request.Params[param];
                        FilmUpdateDto film;
                        try
                        {
                            film = JsonHelper.JsonDeserialize<FilmUpdateDto>(channelJSON);
                            //fm.AccountId = account.Id;
                            film.NativeLanguageId = CurrentLanguage.NativeId;
                            film.LearnLanguageId = CurrentLanguage.LearnId;

                            // Необходимо убрать теги <script> для безопасности
                            film.Name = film.Name.ReplaceScriptTags();
                            //fm.Description = fm.Description.ReplaceScriptTags();
                            film.ImageUrl = film.ImageUrl.ReturnEmptyIfNull().ReplaceScriptTags();
                            film.PlayerCode = film.PlayerCode.ReturnEmptyIfNull().ReplaceScriptTags();
                        }
                        catch (Exception)
                        {
                            context.WriteErrorAndExit(CurrentLanguage.CantUpdate);
                            throw;
                        }

                        int resultId = 0;
                        switch (actionType)
                        {
                            case "add":
                                resultId = FilmRelatedAccessor.UpdateFilmChannel(account.Id, film);
                                if (resultId < 0)
                                {
                                    context.WriteErrorAndExit(CurrentLanguage.CantAddOrUpdateFmChannel);
                                }
                                if (resultId > 0)
                                {
                                    idReplacementDictionary.Add(targetId, resultId);
                                }
                                break;

                            case "edit":
                                resultId = FilmRelatedAccessor.UpdateFilmChannel(account.Id, film);
                                if (resultId < 0)
                                {
                                    context.WriteErrorAndExit(CurrentLanguage.CantAddOrUpdateFmChannel);
                                }
                                break;

                            case "delete":
                                resultId = FilmRelatedAccessor.DeleteFilmChannel(account.Id, film.Id);
                                if (resultId < 0)
                                {
                                    context.WriteErrorAndExit(CurrentLanguage.CantDeleteChannel);
                                }
                                break;
                        }

                    }
                }
            }

            var updatingDto = new EntityOrderingUpdateInfoDto();

            var regex = new Regex(@"^channel_(-?\d+)$");
            foreach (var param in context.Request.Params.AllKeys)
            {
                var match = regex.Match(param);
                if (match.Success)
                {
                    int itemId;
                    if (int.TryParse(match.Groups[1].Value, out itemId))
                    {
                        int orderIndex;
                        if (int.TryParse(context.Request.Params[param], out orderIndex))
                        {
                            if (idReplacementDictionary.ContainsKey(itemId))
                            {
                                itemId = idReplacementDictionary[itemId];
                            }
                            updatingDto.OrderingInfo.Add(new EntityOrderingInfo { Id = itemId, OrderIndex = orderIndex });
                        }
                    }
                }
            }

            if (updatingDto.OrderingInfo.Any())
            {
                var accountId = StorageManager.CurrentAccountId;
                var result = FilmRelatedAccessor.UpdateFilmChannelOrder(accountId, updatingDto);
                if (result < 0)
                {
                    context.WriteErrorAndExit(CurrentLanguage.CantSave);
                }
                else
                {
                    context.WriteSuccessAndExit(CurrentLanguage.Success);
                }
            }
        }

        public bool IsReusable
        {
            get { return true; }
        }
    }
}