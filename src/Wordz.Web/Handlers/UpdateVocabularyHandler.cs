using System.Web;
using System.Web.SessionState;
using Wordz.BC;
using Wordz.Web.Util;

namespace Wordz.Web.Handlers {
    public class UpdateVocabularyHandler : IHttpHandler, IRequiresSessionState {
        public bool IsReusable {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context) {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;

            if (StorageManager.UserLogined) {
                string wordsString = HandlersUtil.GetContent(request.InputStream);
                string[] words = wordsString.Split(BCCommon.Separator2);
                string[] wordsKnownList = words[0].Split(BCCommon.Separator);
                string[] wordsUnknownList = words[1].Split(BCCommon.Separator);
                BCCommon.DeleteWordsFromAccount(StorageManager.CurrentAccount.Id, wordsUnknownList, StorageManager.LanguageLearnId);
                BCCommon.AddWordsToAccount(StorageManager.CurrentAccount.Id, wordsKnownList, StorageManager.LanguageLearnId);
                response.Write(BCCommon.OkText);
            }
            response.End();
        }
    }
}