using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using Wordz.BC;
using Wordz.LngOpt;

namespace Wordz.Win {
    public class LoadSpanishWordsProgressForm : Form {
        private Label lblStatus;
        private ProgressBar progressBar;
        private Button btnCancel;
        private Thread thread;

        private string text;

        private Container components = null;

        public LoadSpanishWordsProgressForm() {
            InitializeComponent();
        }

        public LoadSpanishWordsProgressForm(string textForProcess) : this() {
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
            this.lblStatus = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
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
            // LoadSpanishWordsProgressForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(312, 122);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.btnCancel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoadSpanishWordsProgressForm";
            this.Opacity = 0.7;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LoadSpanishWordsProgressForm";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.LoadSpanishWordsProgressForm_Closing);
            this.Load += new System.EventHandler(this.LoadSpanishWordsProgressForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private void btnCancel_Click(object sender, EventArgs e) {
            Close();
        }

        private void LoadSpanishWordsProgressForm_Load(object sender, EventArgs e) {
            thread = new Thread(new ThreadStart(DoWork));
            thread.Start();
        }

        private void LoadSpanishWordsProgressForm_Closing(object sender, CancelEventArgs e) {
            thread.Abort();
        }

//        private void DoWork() {
//            lblStatus.Text = "Started...";
//
//            int wordsPairLength =
//                Constants.EnglishWordLengthInDictionary +
//                Constants.SpanishWordLengthInDictionary;
//            int wordPairsCount = text.Length / wordsPairLength;
//            progressBar.Maximum = wordPairsCount;
//            string format = "{0} words from " + progressBar.Maximum;
//
//            for (int i = 0; i < wordPairsCount; i++) {
//                string wordsPair = text.Substring(i * wordsPairLength, wordsPairLength);
//                BCCommon.AddWordToDictionaryUseExist(wordsPair,
//                    //(int)Language.French, (int)Language.English);
//                    (int)Language.Chinese, (int)Language.English);
//                progressBar.PerformStep();
//                lblStatus.Text = string.Format(format, 1 + i);
//            }
//
//            DialogResult = DialogResult.OK;
//            Close();
//        }

//        private void DoWork() {
//            try {
//                lblStatus.Text = "Started...";
//
//                string[] wordPairs = text.Split(Environment.NewLine.ToCharArray());
//
//                int wordPairsCount = wordPairs.Length;
//                progressBar.Maximum = wordPairsCount;
//                string format = "{0} words from " + progressBar.Maximum;
//
//                int i = 0;
//                foreach (string wordPair in wordPairs) {
//                    if (wordPair.Length > 0) {
//                        int firstSpacePos = wordPair.IndexOf(" ");
//                        if (firstSpacePos < 0) goto end;
//
//                        int firstBreackPos = wordPair.IndexOf("[");
//                        string translation = 
//                            (firstBreackPos > 0 && (firstBreackPos < firstSpacePos))
//                            ? wordPair.Substring(0, firstBreackPos).Trim()
//                            : wordPair.Substring(0, firstSpacePos).Trim();
//
//                        if (translation.Length == 0) goto end;
//
//                        int lastBreackPos = wordPair.IndexOf("]");
//                        string original = (lastBreackPos >= 0)
//                            ? wordPair.Substring(lastBreackPos + 2)
//                            : wordPair.Substring(translation.Length);
//
//                        if (original.Length == 0) goto end;
//
//                        if (original[0] == '/') {
//                            original = original.Substring(1);
//                        }
//                        if (original[original.Length - 1] == '/') {
//                            original = original.Substring(0, original.Length - 1);
//                        }
//
//                        original = original.Replace("/", ", ").Trim();
//                        translation = translation.Replace("/", "").Trim();
//
//                        BCCommon.AddWordToDictionaryUseExist(
//                            original, translation,
//                            (int)Language.Chinese, (int)Language.English);
//                    }
//                    end:
//                    progressBar.PerformStep();
//                    lblStatus.Text = string.Format(format, ++i);
//                }
//
//                DialogResult = DialogResult.OK;
//                Close();
//            }
//            catch (Exception e) {
//                Console.Write(e);
//            }
//        }

        private void DoWork() {
            try {
                lblStatus.Text = "Started...";

                string[] wordPairs = text.Split(Environment.NewLine.ToCharArray());

                int wordPairsCount = wordPairs.Length;
                progressBar.Maximum = wordPairsCount;
                string format = "{0} words from " + progressBar.Maximum;

                int i = 0;
                foreach (string wordPair in wordPairs) {
                    if (wordPair.Length > 0) {
                        int firstSpacePos = wordPair.IndexOf(" ");
                        if (firstSpacePos < 0) goto end;

                        int firstBreackPos = wordPair.IndexOf("[");
                        string translation = 
                            (firstBreackPos > 0 && (firstBreackPos < firstSpacePos))
                            ? wordPair.Substring(0, firstBreackPos).Trim()
                            : wordPair.Substring(0, firstSpacePos).Trim();

                        if (translation.Length == 0) goto end;

                        int lastBreackPos = wordPair.IndexOf("]");
                        string original = (lastBreackPos >= 0)
                            ? wordPair.Substring(lastBreackPos + 2)
                            : wordPair.Substring(translation.Length);

                        if (firstBreackPos > 0 && lastBreackPos > 0) {
                            translation = wordPair.Substring(
                                firstBreackPos + 1,
                                lastBreackPos - firstBreackPos - 1);
                        }

                        if (original.Length == 0) goto end;

                        if (original[0] == '/') {
                            original = original.Substring(1);
                        }
                        if (original[original.Length - 1] == '/') {
                            original = original.Substring(0, original.Length - 1);
                        }

                        original = original.Replace("/", ", ").Trim();
                        translation = translation.Replace("/", "").Trim();

                        BCCommon.AddWordToDictionaryUseExist(
                            original, translation,
                            (int)Language.Japanese, (int)Language.English);
                    }
                    end:
                    progressBar.PerformStep();
                    lblStatus.Text = string.Format(format, ++i);
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception e) {
                Console.Write(e);
            }
        }
    }
}