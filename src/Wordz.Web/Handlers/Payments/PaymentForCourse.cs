using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.SessionState;
using Wordz.BC;
using Wordz.DB.Accessors;
using Wordz.Web.Helpers;
using Wordz.Lng;

namespace Wordz.Web.Handlers.Payments
{
    public class PaymentForCourse : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            context.WriteErrorIfNotLoggedAndExit();

            var account = StorageManager.CurrentAccount;

            int courseId;
            if (!int.TryParse(context.Request.Params["id"], out courseId))
            {
                context.Response.Write(string.Format("{{'status': 'ERR','msg': '{0}'}}", CurrentLanguage.IncorrectParams));
                context.Response.End();
            }

            decimal price;
            if (!decimal.TryParse(context.Request.Params["price"], NumberStyles.Currency,
                new NumberFormatInfo { CurrencyDecimalSeparator = ".", NumberDecimalSeparator = "." },
                out price))
            {
                context.Response.Write(string.Format("{{'status': 'ERR','msg': '{0}'}}", CurrentLanguage.IncorrectParams));
                context.Response.End();
            }

            int currencyId;
            if (!int.TryParse(context.Request.Params["currencyId"], out currencyId))
            {
                context.Response.Write(string.Format("{{'status': 'ERR','msg': '{0}'}}", CurrentLanguage.IncorrectParams));
                context.Response.End();
            }

            var result = BCCommon.AddPaymentForCourse(account.Id,
                                                      courseId,
                                                      currencyId,
                                                      DateTime.UtcNow,
                                                      price);
            if (result < 0)
            {
				context.Response.Write(string.Format("{{'status': 'ERR','msg': '{0}'}}", CurrentLanguage.CantPayCourse));
                context.Response.End();
            }
            else
            {
				context.Response.Write(string.Format("{{'status': 'OK','msg': '{0}'}}", CurrentLanguage.PayCourseSuccessful));
                context.Response.End();
            }
        }

        public bool IsReusable
        {
            get { return true; }
        }
    }
}