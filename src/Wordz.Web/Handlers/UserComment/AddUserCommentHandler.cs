using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Wordz.BC;
using Wordz.Web.Helpers;
using Wordz.Lng;

namespace Wordz.Web.Handlers.UserComment
{
    public class AddUserCommentHandler : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            var text = context.Request.Form["text"].ReturnEmptyIfNull();

            var captchaText = context.Request.Form["captchaText"];
            
            if (!CaptchaHelper.VerifyMd5Hash(captchaText, (string)context.Session["captch_hash"]))
            {
                context.WriteErrorAndExit(CurrentLanguage.WrongCaptcha, JsonHelper.JsonSerializer("WrongCaptcha"));
            }

            if (string.IsNullOrEmpty(text.Trim()))
            {
                context.WriteErrorAndExit(CurrentLanguage.EmptyComment);
            }

            int? parentCommentId = null;
            int tempParentid;
            if (int.TryParse(context.Request.Form["parentId"], out tempParentid))
            {
                parentCommentId = tempParentid;
            }

            Guid targetElement;

            if (!Guid.TryParse(context.Request.Form["targetElement"], out targetElement))
            {
				context.WriteErrorAndExit(CurrentLanguage.IncorrectParams);
            }

            var accountId = StorageManager.UserLogined ? StorageManager.CurrentAccountId : BCCommon.AnonymousAccountId;
            var comment = new BE.UserComment
                              {
                                  AuthorAccountId = accountId,
                                  CreatedDate = DateTime.UtcNow,
                                  Text = text,
                                  TargetElement = targetElement
                              };
            var resultId = BCCommon.AddUserComment(comment, parentCommentId);
            if (resultId < 0)
            {
				context.WriteErrorAndExit(CurrentLanguage.CantAddComment);
            }
            else
            {
                context.WriteSuccessAndExit("Ок!", JsonHelper.JsonSerializer(comment));
            }
        }

        public bool IsReusable
        {
            get { return false; }
        }
    }
}