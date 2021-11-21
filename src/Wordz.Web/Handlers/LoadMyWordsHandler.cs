using System.Web;
using System.Web.SessionState;
using Wordz.BC;
using Wordz.BE;
using Wordz.Lng;

namespace Wordz.Web.Handlers {
    public class LoadMyWordsHandler : IHttpHandler, IRequiresSessionState {
        public bool IsReusable {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context) {
            HttpResponse response = context.Response;

            string vocabularyInfo = (StorageManager.UserLogined)
                ? BCCommon.GetVocabularyInfo(
                StorageManager.CurrentAccount.Id,
                StorageManager.LanguageLearnId,
                StorageManager.LanguageNativeId)
                : CurrentLanguage.MessageUnknown;
            
            IdNamePair[] pairs = BCCommon.GetWordIds(StorageManager.CurrentAccountId, StorageManager.LanguageLearnId);
            string controlsHtml = BCCommon.GetCheckedWordsHTML(pairs);
            string content = string.Format("{0}{1}{2}", vocabularyInfo, BCCommon.Separator, controlsHtml);

            StorageManager.ProcessedText = BCCommon.ShowProcessText(pairs);

            response.Write(content);
            response.End();
        }
    }
}