using System.Web;
using System.Web.SessionState;
using Wordz.BC;

namespace Wordz.Web.Handlers {
    public class SetFilmModeHandler : IHttpHandler, IRequiresSessionState {
        public bool IsReusable {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context) {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;

            bool isFilmIconMode = bool.Parse(request.QueryString["IsFilmIconMode"].ToString());

            StorageManager.FilmIconMode = isFilmIconMode;

            if (StorageManager.FoundedFilms != null) {
                string content = BCCommon.GetFilmsListHTML(
                    StorageManager.FoundedFilms,
                    StorageManager.FilmIconMode);

                response.Write(content);
            }

            response.End();
        }
    }
}