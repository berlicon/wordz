using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wordz.DB.Accessors;
using Wordz.Web.Helpers;

namespace Wordz.Web.Handlers.DictionariesHandlers
{
    public class GetCurrenciesHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            var currencies = CurrencyRelatedDataAccessor.GetCurrencies();
            context.Response.Write(JsonHelper.JsonSerializer(currencies));
        }

        public bool IsReusable
        {
            get { return true; }
        }
    }
}