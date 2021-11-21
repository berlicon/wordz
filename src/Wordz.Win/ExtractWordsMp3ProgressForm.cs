using System;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Wordz.BC;
using Wordz.Lng;
using Wordz.LngOpt;

namespace Wordz.Win {
    public class ExtractWordsMp3ProgressForm : Form {
        private Label lblStatus;
        private ProgressBar progressBar;
        private Button btnCancel;
        private Thread thread;

        private Container components = null;

        public ExtractWordsMp3ProgressForm() {
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
            // AddWordsProgressForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(312, 122);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.btnCancel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddWordsProgressForm";
            this.Opacity = 0.7;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AddWordsProgressForm";
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

        private void DoWork() {
            lblStatus.Text = "Started...";

            DirectoryInfo root = Directory.CreateDirectory(LanguageOptions.SoundPath);
            DirectoryInfo learnDir = root.CreateSubdirectory(LanguageOptions.GetWordsPath(CurrentLanguage.LearnId));
            DirectoryInfo nativeDir = root.CreateSubdirectory(LanguageOptions.GetWordsPath(CurrentLanguage.NativeId));

            byte[] fileContent;
            string fileName;
            int count = 18630;
            progressBar.Maximum = count;
            string format = "{0} words from " + count;

            for (int i = 1; i <= count; i++) {
                fileContent = BCCommon.GetWordSoundTranslation(i, true);
                fileName = Path.Combine(learnDir.FullName, i.ToString() + ".mp3");
                BCCommon.CreateFile(fileContent, fileName);

                fileContent = BCCommon.GetWordSoundTranslation(i, false);
                fileName = Path.Combine(nativeDir.FullName, i.ToString() + ".mp3");
                BCCommon.CreateFile(fileContent, fileName);

                progressBar.PerformStep();
                lblStatus.Text = string.Format(format, i);
            }

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}