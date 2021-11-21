using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Wordz.BC;
using Wordz.BE;
using Wordz.Lng;

namespace Wordz.Win {
    public class MainForm : Form {
        private Button btnClose;
        private TextBox txtSource;
        private Button btnLoad;
        private OpenFileDialog openFileDialog;
        private TextBox txtProcessed;
        private Button btnProcess;
        private GroupBox gbSaveFile;
        private GroupBox gbSortBy;
        private RadioButton rbSortAlphabet;
        private RadioButton rbSortFrequency;
        private RadioButton rbSortWordsLength;
        private RadioButton rbSortMixedOrder;
        private RadioButton rbSortOriginalOrder;
        private Button btnSaveTextFile;
        private Button btnSaveAudioFile;
        private GroupBox gbWordsOrder;
        private RadioButton rbWordsOrderRussian;
        private RadioButton rbWordsOrderEnglish;
        private GroupBox gbFrequencyTuning;
        private NumericUpDown nudFrequency;
        private Label lblWords;
        private TextBox txtWordsCount;
        private GroupBox gbUsePersonalDictionary;
        private CheckBox chkDontUseWellKnownWords;
        private SaveFileDialog saveFileDialog;
        private Button btnOpenDictFile;
        private Button btnExtractMP3;
        private Button btnSoundWordsSAPI;
        private Button btnUploadFilms;
        private Button btnLoadSpanishWords;
        private Container components = null;

        public MainForm() {
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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.btnClose = new System.Windows.Forms.Button();
            this.txtSource = new System.Windows.Forms.TextBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.txtProcessed = new System.Windows.Forms.TextBox();
            this.btnProcess = new System.Windows.Forms.Button();
            this.gbSaveFile = new System.Windows.Forms.GroupBox();
            this.gbUsePersonalDictionary = new System.Windows.Forms.GroupBox();
            this.chkDontUseWellKnownWords = new System.Windows.Forms.CheckBox();
            this.gbFrequencyTuning = new System.Windows.Forms.GroupBox();
            this.nudFrequency = new System.Windows.Forms.NumericUpDown();
            this.lblWords = new System.Windows.Forms.Label();
            this.txtWordsCount = new System.Windows.Forms.TextBox();
            this.gbWordsOrder = new System.Windows.Forms.GroupBox();
            this.rbWordsOrderRussian = new System.Windows.Forms.RadioButton();
            this.rbWordsOrderEnglish = new System.Windows.Forms.RadioButton();
            this.btnSaveAudioFile = new System.Windows.Forms.Button();
            this.gbSortBy = new System.Windows.Forms.GroupBox();
            this.rbSortOriginalOrder = new System.Windows.Forms.RadioButton();
            this.rbSortFrequency = new System.Windows.Forms.RadioButton();
            this.rbSortMixedOrder = new System.Windows.Forms.RadioButton();
            this.rbSortWordsLength = new System.Windows.Forms.RadioButton();
            this.rbSortAlphabet = new System.Windows.Forms.RadioButton();
            this.btnSaveTextFile = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.btnOpenDictFile = new System.Windows.Forms.Button();
            this.btnExtractMP3 = new System.Windows.Forms.Button();
            this.btnSoundWordsSAPI = new System.Windows.Forms.Button();
            this.btnUploadFilms = new System.Windows.Forms.Button();
            this.btnLoadSpanishWords = new System.Windows.Forms.Button();
            this.gbSaveFile.SuspendLayout();
            this.gbUsePersonalDictionary.SuspendLayout();
            this.gbFrequencyTuning.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFrequency)).BeginInit();
            this.gbWordsOrder.SuspendLayout();
            this.gbSortBy.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(296, 312);
            this.btnClose.Name = "btnClose";
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtSource
            // 
            this.txtSource.Location = new System.Drawing.Point(16, 16);
            this.txtSource.Multiline = true;
            this.txtSource.Name = "txtSource";
            this.txtSource.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSource.Size = new System.Drawing.Size(272, 280);
            this.txtSource.TabIndex = 1;
            this.txtSource.Text = "";
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(16, 312);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.TabIndex = 0;
            this.btnLoad.Text = "Load file";
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // txtProcessed
            // 
            this.txtProcessed.Location = new System.Drawing.Point(296, 16);
            this.txtProcessed.Multiline = true;
            this.txtProcessed.Name = "txtProcessed";
            this.txtProcessed.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtProcessed.Size = new System.Drawing.Size(272, 280);
            this.txtProcessed.TabIndex = 3;
            this.txtProcessed.Text = "";
            // 
            // btnProcess
            // 
            this.btnProcess.Location = new System.Drawing.Point(96, 312);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(96, 23);
            this.btnProcess.TabIndex = 2;
            this.btnProcess.Text = "Process file";
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // gbSaveFile
            // 
            this.gbSaveFile.Controls.Add(this.gbUsePersonalDictionary);
            this.gbSaveFile.Controls.Add(this.gbFrequencyTuning);
            this.gbSaveFile.Controls.Add(this.gbWordsOrder);
            this.gbSaveFile.Controls.Add(this.btnSaveAudioFile);
            this.gbSaveFile.Controls.Add(this.gbSortBy);
            this.gbSaveFile.Controls.Add(this.btnSaveTextFile);
            this.gbSaveFile.Location = new System.Drawing.Point(576, 8);
            this.gbSaveFile.Name = "gbSaveFile";
            this.gbSaveFile.Size = new System.Drawing.Size(248, 360);
            this.gbSaveFile.TabIndex = 5;
            this.gbSaveFile.TabStop = false;
            this.gbSaveFile.Text = "Save file";
            // 
            // gbUsePersonalDictionary
            // 
            this.gbUsePersonalDictionary.Controls.Add(this.chkDontUseWellKnownWords);
            this.gbUsePersonalDictionary.Location = new System.Drawing.Point(16, 72);
            this.gbUsePersonalDictionary.Name = "gbUsePersonalDictionary";
            this.gbUsePersonalDictionary.Size = new System.Drawing.Size(216, 48);
            this.gbUsePersonalDictionary.TabIndex = 0;
            this.gbUsePersonalDictionary.TabStop = false;
            this.gbUsePersonalDictionary.Text = "Using personal dictionary";
            // 
            // chkDontUseWellKnownWords
            // 
            this.chkDontUseWellKnownWords.Location = new System.Drawing.Point(16, 16);
            this.chkDontUseWellKnownWords.Name = "chkDontUseWellKnownWords";
            this.chkDontUseWellKnownWords.Size = new System.Drawing.Size(192, 24);
            this.chkDontUseWellKnownWords.TabIndex = 0;
            this.chkDontUseWellKnownWords.Text = "Don\'t use well-known words";
            this.chkDontUseWellKnownWords.CheckedChanged += new System.EventHandler(this.paramsChanged);
            // 
            // gbFrequencyTuning
            // 
            this.gbFrequencyTuning.Controls.Add(this.nudFrequency);
            this.gbFrequencyTuning.Controls.Add(this.lblWords);
            this.gbFrequencyTuning.Controls.Add(this.txtWordsCount);
            this.gbFrequencyTuning.Location = new System.Drawing.Point(16, 264);
            this.gbFrequencyTuning.Name = "gbFrequencyTuning";
            this.gbFrequencyTuning.Size = new System.Drawing.Size(216, 56);
            this.gbFrequencyTuning.TabIndex = 4;
            this.gbFrequencyTuning.TabStop = false;
            this.gbFrequencyTuning.Text = "Use words: Freq. > ";
            // 
            // nudFrequency
            // 
            this.nudFrequency.Location = new System.Drawing.Point(16, 24);
            this.nudFrequency.Maximum = new System.Decimal(new int[] {
                                                                         1,
                                                                         0,
                                                                         0,
                                                                         0});
            this.nudFrequency.Minimum = new System.Decimal(new int[] {
                                                                         1,
                                                                         0,
                                                                         0,
                                                                         0});
            this.nudFrequency.Name = "nudFrequency";
            this.nudFrequency.Size = new System.Drawing.Size(56, 22);
            this.nudFrequency.TabIndex = 6;
            this.nudFrequency.Value = new System.Decimal(new int[] {
                                                                       1,
                                                                       0,
                                                                       0,
                                                                       0});
            this.nudFrequency.ValueChanged += new System.EventHandler(this.paramsChanged);
            // 
            // lblWords
            // 
            this.lblWords.AutoSize = true;
            this.lblWords.Location = new System.Drawing.Point(88, 28);
            this.lblWords.Name = "lblWords";
            this.lblWords.Size = new System.Drawing.Size(44, 18);
            this.lblWords.TabIndex = 6;
            this.lblWords.Text = "words:";
            // 
            // txtWordsCount
            // 
            this.txtWordsCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
            this.txtWordsCount.Location = new System.Drawing.Point(136, 24);
            this.txtWordsCount.Name = "txtWordsCount";
            this.txtWordsCount.ReadOnly = true;
            this.txtWordsCount.Size = new System.Drawing.Size(72, 22);
            this.txtWordsCount.TabIndex = 6;
            this.txtWordsCount.Text = "";
            this.txtWordsCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // gbWordsOrder
            // 
            this.gbWordsOrder.Controls.Add(this.rbWordsOrderRussian);
            this.gbWordsOrder.Controls.Add(this.rbWordsOrderEnglish);
            this.gbWordsOrder.Location = new System.Drawing.Point(16, 24);
            this.gbWordsOrder.Name = "gbWordsOrder";
            this.gbWordsOrder.Size = new System.Drawing.Size(216, 48);
            this.gbWordsOrder.TabIndex = 1;
            this.gbWordsOrder.TabStop = false;
            this.gbWordsOrder.Text = "Words order";
            // 
            // rbWordsOrderRussian
            // 
            this.rbWordsOrderRussian.Location = new System.Drawing.Point(96, 16);
            this.rbWordsOrderRussian.Name = "rbWordsOrderRussian";
            this.rbWordsOrderRussian.Size = new System.Drawing.Size(80, 24);
            this.rbWordsOrderRussian.TabIndex = 1;
            this.rbWordsOrderRussian.Text = "Rus/Eng";
            this.rbWordsOrderRussian.CheckedChanged += new System.EventHandler(this.paramsChanged);
            // 
            // rbWordsOrderEnglish
            // 
            this.rbWordsOrderEnglish.Checked = true;
            this.rbWordsOrderEnglish.Location = new System.Drawing.Point(16, 16);
            this.rbWordsOrderEnglish.Name = "rbWordsOrderEnglish";
            this.rbWordsOrderEnglish.Size = new System.Drawing.Size(80, 24);
            this.rbWordsOrderEnglish.TabIndex = 0;
            this.rbWordsOrderEnglish.TabStop = true;
            this.rbWordsOrderEnglish.Text = "Eng/Rus";
            this.rbWordsOrderEnglish.CheckedChanged += new System.EventHandler(this.paramsChanged);
            // 
            // btnSaveAudioFile
            // 
            this.btnSaveAudioFile.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSaveAudioFile.Location = new System.Drawing.Point(128, 328);
            this.btnSaveAudioFile.Name = "btnSaveAudioFile";
            this.btnSaveAudioFile.Size = new System.Drawing.Size(104, 23);
            this.btnSaveAudioFile.TabIndex = 3;
            this.btnSaveAudioFile.Text = "Save audio file";
            this.btnSaveAudioFile.Click += new System.EventHandler(this.btnSaveAudioFile_Click);
            // 
            // gbSortBy
            // 
            this.gbSortBy.Controls.Add(this.rbSortOriginalOrder);
            this.gbSortBy.Controls.Add(this.rbSortFrequency);
            this.gbSortBy.Controls.Add(this.rbSortMixedOrder);
            this.gbSortBy.Controls.Add(this.rbSortWordsLength);
            this.gbSortBy.Controls.Add(this.rbSortAlphabet);
            this.gbSortBy.Location = new System.Drawing.Point(16, 120);
            this.gbSortBy.Name = "gbSortBy";
            this.gbSortBy.Size = new System.Drawing.Size(216, 144);
            this.gbSortBy.TabIndex = 1;
            this.gbSortBy.TabStop = false;
            this.gbSortBy.Text = "Sort by";
            // 
            // rbSortOriginalOrder
            // 
            this.rbSortOriginalOrder.Checked = true;
            this.rbSortOriginalOrder.Location = new System.Drawing.Point(16, 112);
            this.rbSortOriginalOrder.Name = "rbSortOriginalOrder";
            this.rbSortOriginalOrder.Size = new System.Drawing.Size(120, 24);
            this.rbSortOriginalOrder.TabIndex = 4;
            this.rbSortOriginalOrder.TabStop = true;
            this.rbSortOriginalOrder.Text = "Original order";
            this.rbSortOriginalOrder.CheckedChanged += new System.EventHandler(this.paramsChanged);
            // 
            // rbSortFrequency
            // 
            this.rbSortFrequency.Location = new System.Drawing.Point(16, 40);
            this.rbSortFrequency.Name = "rbSortFrequency";
            this.rbSortFrequency.Size = new System.Drawing.Size(120, 24);
            this.rbSortFrequency.TabIndex = 1;
            this.rbSortFrequency.Text = "Frequency";
            this.rbSortFrequency.CheckedChanged += new System.EventHandler(this.paramsChanged);
            // 
            // rbSortMixedOrder
            // 
            this.rbSortMixedOrder.Location = new System.Drawing.Point(16, 88);
            this.rbSortMixedOrder.Name = "rbSortMixedOrder";
            this.rbSortMixedOrder.Size = new System.Drawing.Size(120, 24);
            this.rbSortMixedOrder.TabIndex = 3;
            this.rbSortMixedOrder.Text = "Mixed order";
            this.rbSortMixedOrder.CheckedChanged += new System.EventHandler(this.paramsChanged);
            // 
            // rbSortWordsLength
            // 
            this.rbSortWordsLength.Location = new System.Drawing.Point(16, 64);
            this.rbSortWordsLength.Name = "rbSortWordsLength";
            this.rbSortWordsLength.Size = new System.Drawing.Size(120, 24);
            this.rbSortWordsLength.TabIndex = 2;
            this.rbSortWordsLength.Text = "Words length";
            this.rbSortWordsLength.CheckedChanged += new System.EventHandler(this.paramsChanged);
            // 
            // rbSortAlphabet
            // 
            this.rbSortAlphabet.Location = new System.Drawing.Point(16, 16);
            this.rbSortAlphabet.Name = "rbSortAlphabet";
            this.rbSortAlphabet.Size = new System.Drawing.Size(120, 24);
            this.rbSortAlphabet.TabIndex = 0;
            this.rbSortAlphabet.Text = "Alphabetically";
            this.rbSortAlphabet.CheckedChanged += new System.EventHandler(this.paramsChanged);
            // 
            // btnSaveTextFile
            // 
            this.btnSaveTextFile.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSaveTextFile.Location = new System.Drawing.Point(16, 328);
            this.btnSaveTextFile.Name = "btnSaveTextFile";
            this.btnSaveTextFile.Size = new System.Drawing.Size(104, 23);
            this.btnSaveTextFile.TabIndex = 2;
            this.btnSaveTextFile.Text = "Create text file";
            this.btnSaveTextFile.Click += new System.EventHandler(this.btnSaveTextFile_Click);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.FileName = "file.txt";
            // 
            // btnOpenDictFile
            // 
            this.btnOpenDictFile.Location = new System.Drawing.Point(16, 344);
            this.btnOpenDictFile.Name = "btnOpenDictFile";
            this.btnOpenDictFile.Size = new System.Drawing.Size(176, 23);
            this.btnOpenDictFile.TabIndex = 6;
            this.btnOpenDictFile.Text = "Add words from dictionary";
            this.btnOpenDictFile.Click += new System.EventHandler(this.btnOpenDictFile_Click);
            // 
            // btnExtractMP3
            // 
            this.btnExtractMP3.Location = new System.Drawing.Point(200, 344);
            this.btnExtractMP3.Name = "btnExtractMP3";
            this.btnExtractMP3.Size = new System.Drawing.Size(136, 23);
            this.btnExtractMP3.TabIndex = 7;
            this.btnExtractMP3.Text = "Extract words MP3";
            this.btnExtractMP3.Click += new System.EventHandler(this.btnExtractMP3_Click);
            // 
            // btnSoundWordsSAPI
            // 
            this.btnSoundWordsSAPI.Location = new System.Drawing.Point(344, 344);
            this.btnSoundWordsSAPI.Name = "btnSoundWordsSAPI";
            this.btnSoundWordsSAPI.Size = new System.Drawing.Size(136, 23);
            this.btnSoundWordsSAPI.TabIndex = 8;
            this.btnSoundWordsSAPI.Text = "Sound words (SAPI)";
            this.btnSoundWordsSAPI.Click += new System.EventHandler(this.btnSoundWordsSAPI_Click);
            // 
            // btnUploadFilms
            // 
            this.btnUploadFilms.Location = new System.Drawing.Point(16, 376);
            this.btnUploadFilms.Name = "btnUploadFilms";
            this.btnUploadFilms.Size = new System.Drawing.Size(104, 23);
            this.btnUploadFilms.TabIndex = 9;
            this.btnUploadFilms.Text = "Upload Films";
            this.btnUploadFilms.Click += new System.EventHandler(this.btnUploadFilms_Click);
            // 
            // btnLoadSpanishWords
            // 
            this.btnLoadSpanishWords.Location = new System.Drawing.Point(128, 376);
            this.btnLoadSpanishWords.Name = "btnLoadSpanishWords";
            this.btnLoadSpanishWords.Size = new System.Drawing.Size(144, 23);
            this.btnLoadSpanishWords.TabIndex = 6;
            this.btnLoadSpanishWords.Text = "Load Spanish Words";
            this.btnLoadSpanishWords.Click += new System.EventHandler(this.btnLoadSpanishWords_Click);
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(830, 400);
            this.Controls.Add(this.btnUploadFilms);
            this.Controls.Add(this.btnSoundWordsSAPI);
            this.Controls.Add(this.btnExtractMP3);
            this.Controls.Add(this.btnOpenDictFile);
            this.Controls.Add(this.gbSaveFile);
            this.Controls.Add(this.txtProcessed);
            this.Controls.Add(this.txtSource);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnProcess);
            this.Controls.Add(this.btnLoadSpanishWords);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Convert form";
            this.gbSaveFile.ResumeLayout(false);
            this.gbUsePersonalDictionary.ResumeLayout(false);
            this.gbFrequencyTuning.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudFrequency)).EndInit();
            this.gbWordsOrder.ResumeLayout(false);
            this.gbSortBy.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main() {
            Application.Run(new MainForm());
        }

        #region Properties

        public SortBy SortBy {
            get {
                if (rbSortAlphabet.Checked) {
                    return SortBy.Alphabetically;
                }
                if (rbSortFrequency.Checked) {
                    return SortBy.Frequency;
                }
                if (rbSortWordsLength.Checked) {
                    return SortBy.WordsLength;
                }
                if (rbSortMixedOrder.Checked) {
                    return SortBy.MixedOrder;
                }
                if (rbSortOriginalOrder.Checked) {
                    return SortBy.OriginalOrder;
                }
                return SortBy.Undefined;
            }
        }

        private Hashtable words = new Hashtable();

        public Hashtable Words {
            get { return words; }
            set { words = value; }
        }

        public WordsInfo WordsInfo {
            get { return BCCommon.GetWordsInfo(Words, nudFrequency.Value, int.MaxValue, chkDontUseWellKnownWords.Checked, true, false); }
        }

        #endregion

        private const string frequencyTuningFormat = "Use words: Freq. > {0}..{1}";

        private void btnClose_Click(object sender, EventArgs e) {
            Close();
        }

        private void btnLoad_Click(object sender, EventArgs e) {
            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                if ((openFileDialog.OpenFile()) != null) {
                    using (StreamReader sr = new StreamReader(openFileDialog.FileName)) {
                        txtSource.Text = sr.ReadToEnd();
                    }
                }
            }
        }

        private void btnProcess_Click(object sender, EventArgs e) {
            ProgressForm progressForm = new ProgressForm(txtSource.Text);
            DialogResult dialogResult = progressForm.ShowDialog();
            if (dialogResult != DialogResult.Cancel) {
                Words = progressForm.Words;
                txtProcessed.Text = BCCommon.ShowProcessText(WordsInfo.ProcessedWords, rbWordsOrderEnglish.Checked, SortBy);

                WordsInfo info = WordsInfo;
                nudFrequency.Value = 1;
                nudFrequency.Maximum = info.MaxFrequency;
                rbWordsOrderEnglish.Checked = true;
                chkDontUseWellKnownWords.Checked = false;
                rbSortOriginalOrder.Checked = true;
                UpdateFrequencyParams(info);
            }
        }

        private void UpdateFrequencyParams(WordsInfo info) {
            txtProcessed.Text = BCCommon.ShowProcessText(WordsInfo.ProcessedWords, rbWordsOrderEnglish.Checked, SortBy);
            txtWordsCount.Text = info.WordsCount.ToString();
            gbFrequencyTuning.Text = string.Format(
                frequencyTuningFormat,
                nudFrequency.Value.ToString(),
                nudFrequency.Maximum.ToString());
        }

        private void btnSaveTextFile_Click(object sender, EventArgs e) {
            if (saveFileDialog.ShowDialog() == DialogResult.OK) {
                using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName)) {
                    sw.Write(txtProcessed.Text);
                }
            }
        }

        private void btnSaveAudioFile_Click(object sender, EventArgs e) {}

        private void paramsChanged(object sender, EventArgs e) {
            UpdateFrequencyParams(WordsInfo);
        }

        private void btnOpenDictFile_Click(object sender, EventArgs e) {
            string text;
            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                if ((openFileDialog.OpenFile()) != null) {
                    Encoding encoding = Encoding.GetEncoding(1251);
                    using (StreamReader sr = new StreamReader(openFileDialog.FileName, encoding)) {
                        text = sr.ReadToEnd();
                        AddWordsProgressForm progressForm = new AddWordsProgressForm(text);
                        progressForm.ShowDialog();
                    }
                }
            }
        }

        private void btnExtractMP3_Click(object sender, EventArgs e) {
            ExtractWordsMp3ProgressForm progressForm = new ExtractWordsMp3ProgressForm();
            progressForm.ShowDialog();
        }

        private void btnSoundWordsSAPI_Click(object sender, EventArgs e) {
            SoundWordsSAPIForm progressForm = new SoundWordsSAPIForm();
            progressForm.ShowDialog();
        }

        private void btnUploadFilms_Click(object sender, EventArgs e) {
            string dirPath = @"D:\Test\LearnWords\_A_Work\film-online2\";
            string filePattern = @"???-*.htm*";

            string[] files = Directory.GetFiles(dirPath, filePattern);
            Debug.Assert(files.Length == 365);

            ArrayList films = new ArrayList();
            foreach (string filePath in files) {
                Film film = new Film();

                string fileId = filePath.Substring(dirPath.Length, 3);
                film.Id = int.Parse(fileId);

                string fileContent = "";
                string playerPattern = "";
                string filmUrl = "";
                string[] data = new string[2];
                string filmUrlStart = "";
                string filmUrlEnd = "";
                string filmUrlStart2 = "";
                using (StreamReader sr = File.OpenText(filePath)) {
                    fileContent = sr.ReadToEnd();

                    string filmNameStart = "<td class=\"MainBar\">";
                    int filmNameStartIndex = fileContent.IndexOf(filmNameStart);
                    string filmNameEnd = "</td>";
                    int filmNameEndIndex = fileContent.IndexOf(filmNameEnd, filmNameStartIndex);
                    string filmName = fileContent.Substring(filmNameStartIndex + filmNameStart.Length,
                        filmNameEndIndex - filmNameStartIndex - filmNameStart.Length);
                    film.Name = filmName;
                    Debug.Assert(film.Name.Length > 0 && film.Name.Length <= 100);

                    string categoryStart = "<font color=\"#800000\">Category: ";
                    int categoryStartIndex = fileContent.IndexOf(categoryStart);
                    string categoryEnd = "</font>";
                    int categoryEndIndex = fileContent.IndexOf(categoryEnd, categoryStartIndex);
                    string category = fileContent.Substring(categoryStartIndex + categoryStart.Length,
                        categoryEndIndex - categoryStartIndex - categoryStart.Length);
                    film.Category = category;
                    Debug.Assert(film.Category.Length > 0 && film.Category.Length <= 30);

                    string playerStart = "<object";
                    int playerStartIndex = fileContent.IndexOf(playerStart);
                    if (playerStartIndex < 0) {
                        continue;
                    }
                    Debug.Assert(playerStartIndex == fileContent.LastIndexOf(playerStart));
                    string playerEnd = "</object>";
                    int playerEndIndex = fileContent.IndexOf(playerEnd, playerStartIndex);
                    string player = fileContent.Substring(playerStartIndex,
                        playerEndIndex + playerEnd.Length - playerStartIndex);

                    if (player.IndexOf("value=\"http://static.youku.com") >= 0) {
                        filmUrlStart = "?VideoIDS="; filmUrlEnd = "&amp;";
                    } else if (player.IndexOf("tppabs=\"http://player.youku.com") >= 0) {
                        filmUrlStart = "/sid/"; filmUrlEnd = "=/v.swf";
                    } else if (player.IndexOf("value=\"http://www.freemooviesonline.com") >= 0) {
                        filmUrlStart = "?id="; filmUrlEnd = "\">";
                    } else if (player.IndexOf("value=\"http://www.tudou.com") >= 0) {
                        filmUrlStart = "?iid="; filmUrlEnd = "\">"; filmUrlStart2 = "/v/";
                    } else if (player.IndexOf("value=\"http://dv.ouou.com") >= 0) {
                        filmUrlStart = "/v/"; filmUrlEnd = "\">";
                    } else if (player.IndexOf("value=\"http://video.google.com") >= 0) {
                        filmUrlStart = "?docId="; filmUrlEnd = "&amp;";
                    } else if (player.IndexOf("value=\"http://www.megavideo.com") >= 0) {
                        filmUrlStart = "?v="; filmUrlEnd = "\">"; filmUrlStart2 = "/v/";
                    } else if (player.IndexOf("value=\"http://www.guba.com") >= 0) {
                        filmUrlStart = "/uploaditem/"; filmUrlEnd = "/flash";
                    } else if (player.IndexOf("value=\"http://www.youtube.com") >= 0) {
                        filmUrlStart = "/v/"; filmUrlEnd = "&amp;";
                    } else if (player.IndexOf("tppabs=\"http://www.56.com") >= 0) {
                        filmUrlStart = "/n_v"; filmUrlEnd = ".swf";
                    } else {
                        Debug.Assert(false);
                    }

                    data = GetFilmUrlAndPlayerPattern(player, filmUrlStart, filmUrlEnd, filmUrlStart2);

                    filmUrl = data[0];
                    playerPattern = data[1];
                    film.Url = filmUrl;
                    film.PlayerPattern = playerPattern;
                    Debug.Assert(film.Url.Length > 0 && film.Url.Length <= 100);
                    Debug.Assert(film.PlayerPattern.Length > 0 && film.PlayerPattern.Length <= 2000);

                }   //end process main file content

                //parts
                string partsStart = "<h2>Part:";
                int partsStartIndex = fileContent.IndexOf(partsStart);
                string partsEnd = "</td>";
                int partsEndIndex = fileContent.IndexOf(partsEnd, partsStartIndex);
                string partsHTML = fileContent.Substring(partsStartIndex + partsStart.Length,
                    partsEndIndex - partsStartIndex - partsStart.Length);

                int partsHTMLStartIndex = 0;
                while (true) {
                    string hrefStart = "href=\"";
                    int hrefStartIndex = partsHTML.IndexOf(hrefStart, partsHTMLStartIndex);
                    if (hrefStartIndex < 0) {
                        break;
                    }
                    string hrefEnd = "\"";
                    int hrefEndIndex = partsHTML.IndexOf(hrefEnd, hrefStartIndex + hrefStart.Length);
                    string href = partsHTML.Substring(hrefStartIndex + hrefStart.Length,
                        hrefEndIndex - hrefStartIndex - hrefStart.Length);
                    partsHTMLStartIndex = hrefEndIndex;

                    if (href.Length < 2) {
                        continue;
                    }

                    href = dirPath + href.Replace("&amp;", "&");

                    if (File.Exists(href)) {
                        using (StreamReader sr = File.OpenText(href)) {
                            fileContent = sr.ReadToEnd();

                            data = GetFilmUrlAndPlayerPattern(fileContent, filmUrlStart, filmUrlEnd, filmUrlStart2);

                            filmUrl = data[0];
                            playerPattern = data[1];
                            Debug.Assert(film.Url.Length > 0 && film.Url.Length <= 100);
                            Debug.Assert(film.PlayerPattern.Length > 0 && film.PlayerPattern.Length <= 2000);

                            film.AddPartUrl(filmUrl);
                        }
                    }
                }   //end while cycle - process part content file

                films.Add(film);
            }

            BCCommon.FilmClearAll();
            BCCommon.AddFilmsWithParts((Film[])films.ToArray(typeof(Film)), CurrentLanguage.NativeId, CurrentLanguage.LearnId);
        }

        //TODO: youku.com - we have v1.0.0234 in player url, v1.0.0248 and so on.

        private string[] GetFilmUrlAndPlayerPattern(string player, string filmUrlStart, string filmUrlEnd, string filmUrlStart2) {
            int filmUrlStartIndex = player.IndexOf(filmUrlStart);
            if (filmUrlStartIndex < 0) {
                filmUrlStart = filmUrlStart2;
                filmUrlStartIndex = player.IndexOf(filmUrlStart);
            }
            int filmUrlEndIndex = player.IndexOf(filmUrlEnd, filmUrlStartIndex);
            string filmUrl = player.Substring(filmUrlStartIndex + filmUrlStart.Length,
                filmUrlEndIndex - filmUrlStartIndex - filmUrlStart.Length);
            string playerPattern = player.Replace(filmUrl, "{0}");

            return new string[] { filmUrl, playerPattern };
        }

        private void btnLoadSpanishWords_Click(object sender, EventArgs e) {
            string text;
            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                if ((openFileDialog.OpenFile()) != null) {
                    //int codepage = 1251;//Russian
                    //int codepage = 1252;//Western European
                    //int codepage = 936;//Chinese
                    //Encoding encoding = Encoding.GetEncoding(codepage);
                    string codepageName = "EUC-JP";//Japanese
                    Encoding encoding = Encoding.GetEncoding(codepageName);
                    using (StreamReader sr = new StreamReader(openFileDialog.FileName, encoding)) {
                        text = sr.ReadToEnd();
                        LoadSpanishWordsProgressForm progressForm = new LoadSpanishWordsProgressForm(text);
                        progressForm.ShowDialog();
                    }
                }
            }
        }
    }
}