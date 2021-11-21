using System;
using System.Web;
using System.Web.SessionState;
using Wordz.BC;
using Wordz.BE;

namespace Wordz.Web.Handlers {
    public class LoadVerbsHandler : IHttpHandler, IRequiresSessionState {
        public bool IsReusable {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context) {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;

            bool loadPopular = bool.Parse(request.QueryString["LoadPopular"].ToString());
            ShowColumn showColumn = (ShowColumn) int.Parse(request.QueryString["ShowColumn"]);
            SortColumn sortColumn = (SortColumn) int.Parse(request.QueryString["SortColumn"]);
            bool notUseWellKnownVerbs = bool.Parse(request.QueryString["NotUseWellKnownVerbs"].ToString());

            int wordCount = Constants.DefaultVerbCount;
            try {
                wordCount = Math.Abs(int.Parse(request.QueryString["WordCount"]));
            } catch {}

            string content = "";
            VerbElement[] elements =
                BCCommon.GetVerbs(StorageManager.CurrentAccountId, loadPopular,
                sortColumn, notUseWellKnownVerbs, wordCount);
            content = BCCommon.GetTestedVerbsHTML(elements, showColumn, StorageManager.LanguageNativeId);
            StorageManager.ProcessedText = BCCommon.GetTestedVerbsTEXT(
                elements, sortColumn);
            StorageManager.ProcessedVerbs = elements;

            response.Write(content);
            response.End();
        }
    }
}