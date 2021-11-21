using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Wordz.BC;
using Wordz.BE.Dto;
using Wordz.Web.Helpers;
using Wordz.Lng;

namespace Wordz.Web.Handlers.UserComment
{
    public class GetUserCommentsHandler : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            var pageSize = 10;
            
            var accountId = StorageManager.UserLogined ? StorageManager.CurrentAccountId : BCCommon.AnonymousAccountId;

            Guid targetElement;
            if (!Guid.TryParse(context.Request.Form["targetElement"], out targetElement))
            {
                context.WriteErrorAndExit(CurrentLanguage.IncorrectParams);
            }

            int pageNumber = 1;
            int.TryParse(context.Request.Form["pageNumber"], out pageNumber);

            var comments = BCCommon.GetUserComments(accountId, targetElement, pageSize, pageNumber);
            foreach (var userComment in comments)
            {
                userComment.CreateDateString = userComment.CreatedDate.ToLocalTime().ToString();
                userComment.IsCanRate = userComment.AuthorAccountId != accountId;
            }

            var count = BCCommon.GetUserCommentsCount(accountId, targetElement);
            context.WriteSuccessAndExit("Ok!",
                                        JsonHelper.JsonSerializer(new GetUserCommentsDto
                                                                      {
                                                                          Comments = comments,
                                                                          Pages = count/pageSize + 1,
                                                                          PageSize = pageSize,
                                                                          Count = count,
                                                                          CurrentAccountId = accountId
                                                                      }));
        }

        public bool IsReusable
        {
            get { return false; }
        }
    }
}