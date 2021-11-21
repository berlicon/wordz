using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Wordz.BC;
using Wordz.Lng;
using Wordz.Web.Helpers;

namespace Wordz.Web.Handlers.OnlineFilm
{
    public class GetFilmChannelByIdHandler : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            int id;
            if (!int.TryParse(context.Request.QueryString["filmId"], out id))
            {
                context.WriteErrorAndExit(CurrentLanguage.IncorrectParams);
            }

            var film = BCCommon.GetFilm(id, StorageManager.LanguageNativeId);
            if (film != null)
            {
                var retobj = JsonHelper.JsonSerializer(film);
                context.WriteSuccessAndExit(CurrentLanguage.Success, retobj);
            }
            else
            {
                context.WriteErrorAndExit(CurrentLanguage.CantGetObject);
            }
        }

        public bool IsReusable
        {
            get { return true; }
        }
    }
}