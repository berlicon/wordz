using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Wordz.BC;
using Wordz.Web.Helpers;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace Wordz.Web.Handlers.DictionariesHandlers
{
    public class GetCategoriesHandler : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            // для тестов пока так
            var languageId = 1;

            var categories = BCCommon.GetCategories(languageId);
            context.Response.Write(JsonHelper.JsonSerializer(categories));
        }

        public bool IsReusable
        {
            get { return true; }
        }
    }
}