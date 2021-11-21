using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.SessionState;
using Wordz.BC;
using Wordz.BE.Dto;
using Wordz.DB;
using Wordz.Lng;
using Wordz.Web.Helpers;

namespace Wordz.Web.Handlers.OnlineFm
{
    public class UpdateFmChannelsHandler : IHttpHandler, IRequiresSessionState
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
                        FmUpdateDto fm;
                        try
                        {
                            fm = JsonHelper.JsonDeserialize<FmUpdateDto>(channelJSON);
                            //fm.AccountId = account.Id;
                            fm.NativeLanguageId = CurrentLanguage.NativeId;
                            fm.LearnLanguageId = CurrentLanguage.LearnId;

                            // Необходимо убрать теги <script> для безопасности
                            fm.Name = fm.Name.ReplaceScriptTags();
                            fm.Description = fm.Description.ReplaceScriptTags();
                            fm.ImageUrl = fm.ImageUrl.ReplaceScriptTags();
                            fm.Url = fm.Url.ReplaceScriptTags();
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
                                resultId = DBCommon.UpdateFmChannel(account.Id, fm);
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
                                resultId = DBCommon.UpdateFmChannel(account.Id, fm);
                                if (resultId < 0)
                                {
                                    context.WriteErrorAndExit(CurrentLanguage.CantAddOrUpdateFmChannel);
                                }
                                break;

                            case "delete":
                                resultId = DBCommon.DeleteFmChannel(account.Id, fm.Id);
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
                var result = DBCommon.UpdateFmChannelOrder(accountId, updatingDto);
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