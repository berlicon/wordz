using System.Web;

namespace Wordz.Web.Helpers
{
    public static class HandlerHelper
    {
        public static void WriteErrorIfNotLoggedAndExit(this HttpContext context)
        {
            if (!StorageManager.UserLogined)
            {
                context.NoCache();
                
                context.Response.Write("{'status': 'ERR','msg': 'Ошибка. Вы не вошли в систему!'}");
                context.Response.Flush();
                context.Response.End();
            }
        }

        public static void WriteErrorAndExit(this HttpContext context, string errorText)
        {
            context.NoCache();

            context.Response.Write("{'status': 'ERR','msg': '" + errorText + "'}");
            context.Response.Flush();
            context.Response.End();
        }

        public static void WriteErrorAndExit(this HttpContext context, string errorText, string returnObject)
        {
            context.NoCache();
            
            context.Response.Write("{'status': 'ERR','msg': '" + errorText + "', 'returnObject': " + returnObject + "}");
            context.Response.Flush();
            context.Response.End();
        }

        public static void WriteSuccessAndExit(this HttpContext context, string text)
        {
            context.NoCache();
            
            context.Response.Write("{'status': 'OK','msg': '" + text + "'}");
            context.Response.Flush();
            context.Response.End();
        }

        public static void WriteSuccessAndExit(this HttpContext context, string text, string returnObject)
        {
            context.NoCache();
            
            context.Response.Write("{'status': 'OK','msg': '" + text + "', 'returnObject': " + returnObject + "}");
            context.Response.Flush();
            context.Response.End();
        }

        public static void NoCache(this HttpContext context)
        {
            context.Response.AddHeader("Cache-Control", "no-cache");
        }

        /// <summary>
        /// Отключает валидационное сообщение при отправке html-тегов
        /// </summary>
        /// <param name="context"></param>
        public static void IgnoreValidationException(this HttpContext context)
        {
            try
            {
                context.Request.Form.ToString();
            }
            catch (HttpRequestValidationException e)
            {
                //пропускаем ошибку валидации
                //потом обязательно идет проверка параметров!
            }
        }
    }
}