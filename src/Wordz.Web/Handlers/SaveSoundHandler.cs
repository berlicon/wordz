using System.Web;
using System.Web.SessionState;
using Wordz.BC;
using Wordz.BE;
using Wordz.Lng;

namespace Wordz.Web.Handlers {
    public class SaveSoundHandler : IHttpHandler, IRequiresSessionState {
        public bool IsReusable {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context) {
            HttpResponse response = context.Response;

            response.ClearContent();
            response.ClearHeaders();

            response.ContentType = "audio/mp3";
            response.AddHeader("Content-Disposition", "attachment; filename=\"words.mp3\"");

            bool contentExists = false;
            bool learnFirstWord = StorageManager.LearnFirstWord;
            foreach (IdNamePair word in StorageManager.ProcessedWordIds) {
                byte[] sound = BCCommon.GetWordSound(word.Id, word.Name, word.Name2, learnFirstWord, StorageManager.LanguageNativeId, StorageManager.LanguageLearnId);
                if (sound != null && sound.Length > 0) {
                    response.OutputStream.Write(sound, 0, sound.Length);
                    contentExists = true;
                }
            }

            if (!contentExists) {
                response.ClearHeaders();
                response.ContentType = "text/plain";
                response.AddHeader("Content-Disposition", "attachment; filename=\"error.txt\"");
                response.Write(CurrentLanguage.MessageNoSoundedWords);
            }

            response.End();
        }
    }
}