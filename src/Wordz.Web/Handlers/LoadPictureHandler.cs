using System;
using System.Web;
using System.Web.SessionState;
using Wordz.DB.Accessors;
using Wordz.Lng;

namespace Wordz.Web.Handlers
{
    public class LoadPictureHandler : IHttpHandler, IRequiresSessionState
    {
        /// <summary>
        /// Разрешает обработку веб-запросов НТТР для пользовательского элемента HttpHandler, который реализует интерфейс <see cref="T:System.Web.IHttpHandler"/>.
        /// </summary>
        /// <param name="context">Объект <see cref="T:System.Web.HttpContext"/>, предоставляющий ссылки на внутренние серверные объекты (например, Request, Response, Session и Server), используемые для обслуживания HTTP-запросов. </param>
        public void ProcessRequest(HttpContext context)
        {
            
            var request = context.Request;
            context.Response.ContentType = "image/bmp";
            int targetElement;

            if (!int.TryParse(request.Params["id"], out targetElement))
            {
				context.Response.Write(string.Format("{{'status': 'ERR','msg': '{0}'}}", CurrentLanguage.IncorrectParams));
                context.Response.End();
                return;
            }
            if (targetElement == 0)
            {
                return;
            }

            try
            {
                var picture = PictureRelatedAccessor.GetPictureById(targetElement);
                context.Response.BinaryWrite(picture.Data);
                context.Response.Flush();
            }
            catch(Exception e)
            {
                //context.Response.Write(e.Message);
            }
        }

        /// <summary>
        /// Возвращает значение, позволяющее определить, может ли другой запрос использовать экземпляр класса <see cref="T:System.Web.IHttpHandler"/>.
        /// </summary>
        /// <returns>
        /// Значение true, если экземпляр <see cref="T:System.Web.IHttpHandler"/> доступен для повторного использования; в противном случае — значение false.
        /// </returns>
        public bool IsReusable { get { return true; } }
    }
}