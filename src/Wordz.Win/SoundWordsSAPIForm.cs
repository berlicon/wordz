using System;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Wordz.BC;
using Wordz.BE;
using Wordz.Lng;
using Wordz.LngOpt;

namespace Wordz.Win {
    public class SoundWordsSAPIForm : Form {
        private Label lblStatus;
        private ProgressBar progressBar;
        private Button btnCancel;
        private Thread thread;

        private Container components = null;

        public SoundWordsSAPIForm() {
            InitializeComponent();
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                if (components != null) {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent() {
            this.lblStatus = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte) (204)));
            this.lblStatus.Location = new System.Drawing.Point(16, 48);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(280, 23);
            this.lblStatus.TabIndex = 5;
            this.lblStatus.Text = "Status...";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(16, 16);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(280, 23);
            this.progressBar.Step = 1;
            this.progressBar.TabIndex = 4;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(120, 88);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // SoundWordsSAPIForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(312, 122);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.btnCancel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SoundWordsSAPIForm";
            this.Opacity = 0.7;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SoundWordsSAPIForm";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.AddWordsProgressForm_Closing);
            this.Load += new System.EventHandler(this.AddWordsProgressForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private void btnCancel_Click(object sender, EventArgs e) {
            Close();
        }

        private void AddWordsProgressForm_Load(object sender, EventArgs e) {
            thread = new Thread(new ThreadStart(DoWork));
            thread.Start();
        }

        private void AddWordsProgressForm_Closing(object sender, CancelEventArgs e) {
            thread.Abort();
        }

        private void DoWork3() {
            //Process filenames (be.mp3 make 9.mp3 for british dictor)
            lblStatus.Text = "Started...";

            string dirVerbs = Directory.GetCurrentDirectory() + "\\" +
                LanguageOptions.SoundPath + LanguageOptions.VerbsPath;

            string fileName;
            string fileNameNew;
            int firstIndex = 1;
            int count = 481;
            progressBar.Maximum = count;
            string format = "{0} words from " + count;

            VerbElement[] verbs = BCCommon.GetVerbs(-1, false, SortColumn.Form1, false, 481);
            for (int i = 0, wordId = i + firstIndex; i < count; i++, wordId++) {
                VerbElement verb = verbs[i];

                fileName = dirVerbs + verb.Form1 + ".mp3";

                if (File.Exists(fileName)) {
                    fileNameNew = Path.Combine(dirVerbs, wordId.ToString() + ".mp3");
                    File.Move(fileName, fileNameNew);
                }
                
                progressBar.PerformStep();
                lblStatus.Text = string.Format(format, i);
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void DoWork2() {
            //sound Verbs
            lblStatus.Text = "Started...";

            DirectoryInfo root = Directory.CreateDirectory(LanguageOptions.SoundPath);
            DirectoryInfo dirVerbs = root.CreateSubdirectory(LanguageOptions.VerbsPath);
            DirectoryInfo dirLearn = dirVerbs.CreateSubdirectory(LanguageOptions.GetWordsPath(CurrentLanguage.LearnId));
            DirectoryInfo dirNative = dirVerbs.CreateSubdirectory(LanguageOptions.GetWordsPath(CurrentLanguage.NativeId));

            string dirLearnFullName = dirLearn.FullName;
            string dirNativeFullName = dirNative.FullName;

            byte[] fileContent;
            string fileName;
            int firstIndex = 1;
            int count = 481;
            progressBar.Maximum = count;
            string format = "{0} words from " + count;
            string text;

            VerbElement[] verbs = BCCommon.GetVerbs(-1, false, SortColumn.Form1, false, 481);
            for (int i = 0, wordId = i + firstIndex; i < count; i++, wordId++) {
                VerbElement verb = verbs[i];
                
                text =
                    verb.Form1 + ", " + 
                    verb.Form2 + ", " + 
                    verb.Form3;
                fileContent = BCCommon.GetWordSoundFromText(text);
                fileName = Path.Combine(dirLearnFullName, wordId.ToString() + ".mp3");
                BCCommon.CreateFile(fileContent, fileName);
                
                text = verb.Translation; 
                fileContent = BCCommon.GetWordSoundFromText(text);
                fileName = Path.Combine(dirNativeFullName, wordId.ToString() + ".mp3");
                BCCommon.CreateFile(fileContent, fileName);

                progressBar.PerformStep();
                lblStatus.Text = string.Format(format, i);
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void DoWork1() {
           //sound unsounded words
            lblStatus.Text = "Started...";

            DirectoryInfo root = Directory.CreateDirectory(LanguageOptions.SoundPath);
            DirectoryInfo learnDir = root.CreateSubdirectory(LanguageOptions.GetWordsPath(CurrentLanguage.LearnId));
            DirectoryInfo nativeDir = root.CreateSubdirectory(LanguageOptions.GetWordsPath(CurrentLanguage.NativeId));

            string learnDirFullName = learnDir.FullName;
            string nativeDirFullName = nativeDir.FullName;

            byte[] fileContent;
            string fileName;
            int firstIndex = 18656;
            int count = 49729;
            progressBar.Maximum = count;
            string format = "{0} words from " + count;

            for (int i = 0, wordId = i + firstIndex; i < count; i++, wordId++) {
                WordElement wordElement = BCCommon.GetWordInfo(wordId, CurrentLanguage.NativeId, CurrentLanguage.LearnId);

                if (wordElement.Original == null || wordElement.Translation == null) {
                    continue;
                }

                fileContent = BCCommon.GetWordSoundFromText(wordElement.Original);
                fileName = Path.Combine(learnDirFullName, wordId.ToString() + ".mp3");
                BCCommon.CreateFile(fileContent, fileName);

                fileContent = BCCommon.GetWordSoundFromText(wordElement.Translation);
                fileName = Path.Combine(nativeDirFullName, wordId.ToString() + ".mp3");
                BCCommon.CreateFile(fileContent, fileName);

                progressBar.PerformStep();
                lblStatus.Text = string.Format(format, i);
            }

            DialogResult = DialogResult.OK;
            Close();
        }
        private void DoWork() {
           //sound unsounded words only short and easy
            lblStatus.Text = "Started...";

            DirectoryInfo root = Directory.CreateDirectory(LanguageOptions.SoundPath);
            DirectoryInfo learnDir = root.CreateSubdirectory(LanguageOptions.GetWordsPath(CurrentLanguage.LearnId));
            DirectoryInfo nativeDir = root.CreateSubdirectory(LanguageOptions.GetWordsPath(CurrentLanguage.NativeId));

            string learnDirFullName = learnDir.FullName;
            string nativeDirFullName = nativeDir.FullName;

            int[] ids = BCCommon.GetWordsUnsoundedShort(CurrentLanguage.NativeId, CurrentLanguage.LearnId);

            byte[] fileContent;
            string fileName;
            int firstIndex = 5300;
            int count = ids.Length;
            progressBar.Maximum = count;
            string format = "{0} words from " + count;

            for (int i = firstIndex; i < count; i++) {
                int wordId = ids[i];
                WordElement wordElement = BCCommon.GetWordInfo(wordId, CurrentLanguage.NativeId, CurrentLanguage.LearnId);

                if (wordElement.Original == null || wordElement.Translation == null) {
                    continue;
                }

//                fileContent = BCCommon.GetWordSoundFromText(wordElement.Original);
//                fileName = Path.Combine(enDirFullName, wordId.ToString() + ".mp3");
//                BCCommon.CreateFile(fileContent, fileName);

                fileContent = BCCommon.GetWordSoundFromText(wordElement.Translation);
                fileName = Path.Combine(nativeDirFullName, wordId.ToString() + ".mp3");
                BCCommon.CreateFile(fileContent, fileName);

                progressBar.PerformStep();
                lblStatus.Text = string.Format(format, i);
            }

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}