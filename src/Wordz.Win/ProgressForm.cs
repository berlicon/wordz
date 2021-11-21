using System;
using System.Collections;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using Wordz.BC;
using Wordz.BE;
using Wordz.Lng;

namespace Wordz.Win {
    public class ProgressForm : Form {
        private Button btnCancel;
        private Container components = null;

        private string text;
        private ProgressBar progressBar;
        private Label lblStatus;
        private Hashtable words;
        private Thread thread;

        public Hashtable Words {
            get { return words; }
            set { words = value; }
        }

        public ProgressForm() {
            InitializeComponent();
        }

        public ProgressForm(string textForProcess) : this() {
            text = textForProcess;
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(128, 104);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(24, 32);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(280, 23);
            this.progressBar.Step = 1;
            this.progressBar.TabIndex = 1;
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte) (204)));
            this.lblStatus.Location = new System.Drawing.Point(24, 64);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(280, 23);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "Status...";
            // 
            // ProgressForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(328, 144);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProgressForm";
            this.Opacity = 0.7;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Progress form";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.ProgressForm_Closing);
            this.Load += new System.EventHandler(this.ProgressForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private void btnCancel_Click(object sender, EventArgs e) {
            Close();
        }

        private void ProgressForm_Load(object sender, EventArgs e) {
            thread = new Thread(new ThreadStart(DoWork));
            thread.Start();
        }

        private void DoWork() {
            int accountId = 1;
            lblStatus.Text = "CreateWordsCollection started...";
            Words = BCCommon.CreateWordsCollection(text, false, CurrentLanguage.LearnId);

            string wordsFormat = "GetWordTranslation started: {0} words";
            lblStatus.Text = string.Format(wordsFormat, words.Count);
            progressBar.Maximum = words.Count;
            Hashtable wordsCopy = new Hashtable(words.Count);
            foreach (object key in words.Keys) {
                WordElement wordElement = (WordElement) words[key];
                BCCommon.FillWordInfo(ref wordElement, accountId, CurrentLanguage.NativeId, CurrentLanguage.LearnId);
                wordsCopy[key] = wordElement;
                progressBar.PerformStep();
            }
            words.Clear();
            words = wordsCopy;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void ProgressForm_Closing(object sender, CancelEventArgs e) {
            thread.Abort();
        }
    }
}