using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Wordz.BE;
using Wordz.BE.Dto;
using Wordz.DB;
using Wordz.Lng;
using Wordz.Web.Helpers;

namespace Wordz.Web.Handlers.OnlineTv
{
    public class UpdateTvChannelHandler : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            context.WriteErrorIfNotLoggedAndExit();
            var account = StorageManager.CurrentAccount;

            try
            {
                context.Request.Form.ToString();
            }
            catch (HttpRequestValidationException e)
            {
                //пропускаем ошибку валидации
                //потом обязательно идет проверка параметров!
            }
            
            var tv = getTvFromCollectionOrNull(context.Request.Form);
            if (tv != null)
            {
                if (tv.Id <= 0)
                {
                    tv.AccountId = account.Id;
                }
                tv.NativeLanguageId = CurrentLanguage.NativeId;
                tv.LearnLanguageId = CurrentLanguage.LearnId;
                var resultid = DBCommon.UpdateTvChannel(account.Id, tv);
                if (resultid < 0)
                {
                    context.WriteErrorAndExit(CurrentLanguage.CantAddOrUpdateTvChannel);
                }
                else
                {
                    context.WriteSuccessAndExit(CurrentLanguage.UpdateSuccessful);
                }
            }
        }

        private TvUpdateDto getTvFromCollectionOrNull(NameValueCollection collection)
        {
            int tvId;
            if (!int.TryParse(collection["channelId"], out tvId))
            {
                return null;
            }
            var name = collection["name"]
                .ReturnEmptyIfNull()
                .ReplaceScriptTags();
            var description = collection["description"]
                .ReturnEmptyIfNull()
                .ReplaceScriptTags();
            var url = collection["embededCode"]
                .ReturnEmptyIfNull()
                .ReplaceScriptTags();
            var imageUrl = collection["imageUrl"]
                .ReturnEmptyIfNull()
                .ReplaceScriptTags();
            var isEditable = collection["isEditable"] == "1";

            return
                new TvUpdateDto
                    {
                        Description = description,
                        ImageUrl = imageUrl,
                        Id = tvId,
                        IsEditable = isEditable,
                        Name = name,
                        Url = url
                    };
        }

        public bool IsReusable
        {
            get { return true; }
        }
    }
}