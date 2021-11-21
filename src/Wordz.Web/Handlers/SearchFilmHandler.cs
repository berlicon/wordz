using System.Web;
using System.Web.SessionState;
using Wordz.BC;
using Wordz.BE;
using Wordz.Lng;

namespace Wordz.Web.Handlers {
    public class SearchFilmHandler : IHttpHandler, IRequiresSessionState {
        public bool IsReusable {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context) {
            
            var accountId = StorageManager.UserLogined ? StorageManager.CurrentAccountId : BCCommon.AnonymousAccountId;
            
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;

            int category = 0;
            if (request.QueryString["Category"] != null) {
                category = int.Parse(request.QueryString["Category"]);
            }

            string search = "";
            if (request.QueryString["Search"] != null) {
                search = request.QueryString["Search"];
            }

			var films = (string.IsNullOrEmpty(search))
                ? BCCommon.GetFilmsByCategory(accountId, category, CurrentLanguage.NativeId, CurrentLanguage.LearnId)
                : BCCommon.GetFilmsBySearch(accountId, search, CurrentLanguage.NativeId, CurrentLanguage.LearnId);
            StorageManager.FoundedFilms = films;

            string content = BCCommon.GetFilmsListHTML(
                films, StorageManager.FilmIconMode);

            response.Write(content);
            response.End();
        }
    }
}