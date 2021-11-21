using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.SessionState;
using Wordz.BE.Dto;
using Wordz.DB;
using Wordz.Lng;
using Wordz.Web.Helpers;

namespace Wordz.Web.Handlers.OnlineTv
{
    public class UpdateTvChannelsOrderHandler : IHttpHandler, IRequiresSessionState
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
                        TvUpdateDto tv;
                        try
                        {
                            tv = JsonHelper.JsonDeserialize<TvUpdateDto>(channelJSON);
                            //tv.AccountId = account.Id;
                            tv.NativeLanguageId = CurrentLanguage.NativeId;
                            tv.LearnLanguageId = CurrentLanguage.LearnId;

                            // Необходимо убрать теги <script> для безопасности
                            tv.Name = tv.Name.ReplaceScriptTags();
                            tv.Description = tv.Description.ReplaceScriptTags();
                            tv.ImageUrl = tv.ImageUrl.ReplaceScriptTags();
                            tv.Url = tv.Url.ReplaceScriptTags();
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
                                resultId = DBCommon.UpdateTvChannel(account.Id, tv);
                                if (resultId < 0)
                                {
                                    context.WriteErrorAndExit(CurrentLanguage.CantAddOrUpdateTvChannel);
                                }
                                if (resultId > 0)
                                {
                                    idReplacementDictionary.Add(targetId, resultId);
                                }
                                break;

                            case "edit":
                                resultId = DBCommon.UpdateTvChannel(account.Id, tv);
                                if (resultId < 0)
                                {
                                    context.WriteErrorAndExit(CurrentLanguage.CantAddOrUpdateTvChannel);
                                }
                                break;

                            case "delete":
                                resultId = DBCommon.DeleteTvChannel(account.Id, tv.Id);
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
                            updatingDto.OrderingInfo.Add(new EntityOrderingInfo{Id = itemId, OrderIndex = orderIndex});
                        }
                    }
                }
            }

            if (updatingDto.OrderingInfo.Any())
            {
                var accountId = StorageManager.CurrentAccountId;
                var result = DBCommon.UpdateTvChannelOrder(accountId, updatingDto);
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