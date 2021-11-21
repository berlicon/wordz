using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Wordz.BC;
using Wordz.Web.Helpers;
using Wordz.Lng;

namespace Wordz.Web.Handlers.OnlineFilm
{
    public class GetFilmCategoriesHandler : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            var filmCategories = BCCommon.GetFilmCategories(StorageManager.LanguageNativeId)
                .Where(category => category.Id > 0);
            context.WriteSuccessAndExit(CurrentLanguage.Success, JsonHelper.JsonSerializer(filmCategories));
        }

        public bool IsReusable
        {
            get { return true; }
        }
    }
}