using System;
using System.ComponentModel;
using System.Configuration;
using System.Web;
using Wordz.BC;

namespace Wordz.Web {
    public class Global : HttpApplication {
        private IContainer components = null;

        public Global() {
            InitializeComponent();
        }

        protected void Application_Start(Object sender, EventArgs e) {
            string soundPath = ConfigurationSettings.AppSettings.Get("SoundPath");
            string appPath = Server.MapPath(Context.Request.ApplicationPath);
            string joinPath = appPath + soundPath;
            BCCommon.SetSoundPath(joinPath);

            string connString = ConfigurationSettings.AppSettings.Get("ConnectionString");
            if (connString != null) {
                BCCommon.SetConnectionString(connString);
            }

            string useSoundFiles = ConfigurationSettings.AppSettings.Get("UseSoundFiles");
            if (useSoundFiles != null) {
                BCCommon.SetUseSoundFiles(bool.Parse(useSoundFiles));
            }

            string transcriptRussianWords = ConfigurationSettings.AppSettings.Get("TranscriptRussianWords");
            if (transcriptRussianWords != null) {
                BCCommon.SetTranscriptRussianWords(bool.Parse(transcriptRussianWords));
            }        
        }

        protected void Session_Start(Object sender, EventArgs e)
        {
            
        }

        protected void Application_BeginRequest(Object sender, EventArgs e) {}

        protected void Application_EndRequest(Object sender, EventArgs e) {}

        protected void Application_AuthenticateRequest(Object sender, EventArgs e) {}

        protected void Application_Error(Object sender, EventArgs e) {}

        protected void Session_End(Object sender, EventArgs e) {}

        protected void Application_End(Object sender, EventArgs e) {}

        #region Web Form Designer generated code

        private void InitializeComponent() {
            this.components = new Container();
        }

        #endregion
    }
}