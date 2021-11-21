using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Wordz.BC;
using Wordz.Web.Helpers;

namespace Wordz.Web.Handlers.DictionariesHandlers
{
    public class GetLanguagesHandler : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            var langs = BCCommon.GetLanguages();
            context.Response.Write(JsonHelper.JsonSerializer(langs));
        }

        public bool IsReusable
        {
            get { return false; }
        }
    }
}