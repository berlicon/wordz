using System.Web;
using System.Web.SessionState;
using Wordz.BC;
using Wordz.BE;
using Wordz.Lng;

namespace Wordz.Web.Handlers {
    public class SaveSoundVerbsHandler : IHttpHandler, IRequiresSessionState {
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
            foreach (VerbElement verb in StorageManager.ProcessedVerbs) {
                byte[] sound = BCCommon.GetVerbSoundFromFile(verb.Id, StorageManager.LanguageNativeId, StorageManager.LanguageLearnId);
                
                if (sound.Length > 0) {
                    response.OutputStream.Write(sound, 0, sound.Length);
                    contentExists = true;
                }
            }

            if (!contentExists) {
                response.ClearHeaders();
                response.ContentType = "text/plain";
                response.AddHeader("Content-Disposition", "attachment; filename=\"error.txt\"");
                response.Write(CurrentLanguage.MessageNoSoundedVerbs);
            }

            response.End();
        }
    }
}