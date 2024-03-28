namespace AES_Demo
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnEncrypt = new System.Windows.Forms.Button();
            this.btnDecrypt = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.KeyInput = new System.Windows.Forms.TextBox();
            this.IVInput = new System.Windows.Forms.TextBox();
            this.KeyLabel = new System.Windows.Forms.Label();
            this.radioCBC = new System.Windows.Forms.RadioButton();
            this.radioECB = new System.Windows.Forms.RadioButton();
            this.radioCFB = new System.Windows.Forms.RadioButton();
            this.radioCTR = new System.Windows.Forms.RadioButton();
            this.radioOFB = new System.Windows.Forms.RadioButton();
            this.groupMode = new System.Windows.Forms.GroupBox();
            this.btnIVRandom = new System.Windows.Forms.Button();
            this.IVLabel = new System.Windows.Forms.Label();
            this.BlockSizeLabel = new System.Windows.Forms.Label();
            this.groupKey = new System.Windows.Forms.GroupBox();
            this.radioKey128 = new System.Windows.Forms.RadioButton();
            this.radioKey192 = new System.Windows.Forms.RadioButton();
            this.radioKey256 = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.InputTextBox = new System.Windows.Forms.TextBox();
            this.OutputTextBox = new System.Windows.Forms.TextBox();
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.selectedFileBox = new System.Windows.Forms.TextBox();
            this.btnSelectKey = new System.Windows.Forms.Button();
            this.btnSwap = new System.Windows.Forms.Button();
            this.btnKeyRandom = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.historyListBox = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabText = new System.Windows.Forms.TabPage();
            this.tabFile = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.selectedSaveBox = new System.Windows.Forms.TextBox();
            this.btnSelectLocation = new System.Windows.Forms.Button();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.PassInput = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.genKeyIV = new System.Windows.Forms.Button();
            this.btnSelectIV = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.groupMode.SuspendLayout();
            this.groupKey.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabText.SuspendLayout();
            this.tabFile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnEncrypt
            // 
            this.btnEncrypt.AutoSize = true;
            this.btnEncrypt.Font = new System.Drawing.Font("Nunito", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEncrypt.Location = new System.Drawing.Point(263, 530);
            this.btnEncrypt.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnEncrypt.Name = "btnEncrypt";
            this.btnEncrypt.Size = new System.Drawing.Size(117, 54);
            this.btnEncrypt.TabIndex = 0;
            this.btnEncrypt.Text = "Encrypt";
            this.btnEncrypt.UseVisualStyleBackColor = true;
            this.btnEncrypt.Click += new System.EventHandler(this.btnEncrypt_Click);
            // 
            // btnDecrypt
            // 
            this.btnDecrypt.AutoSize = true;
            this.btnDecrypt.Font = new System.Drawing.Font("Nunito", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDecrypt.Location = new System.Drawing.Point(495, 530);
            this.btnDecrypt.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnDecrypt.Name = "btnDecrypt";
            this.btnDecrypt.Size = new System.Drawing.Size(117, 54);
            this.btnDecrypt.TabIndex = 1;
            this.btnDecrypt.Text = "Decrypt";
            this.btnDecrypt.UseVisualStyleBackColor = true;
            this.btnDecrypt.Click += new System.EventHandler(this.btnDecrypt_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.AutoSize = false;
            this.statusStrip1.Font = new System.Drawing.Font("Nunito", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripProgressBar1});
            this.statusStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.statusStrip1.Location = new System.Drawing.Point(0, 593);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(2, 0, 21, 0);
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip1.Size = new System.Drawing.Size(1068, 28);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(66, 23);
            this.toolStripStatusLabel1.Text = "Ready...";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(150, 22);
            this.toolStripProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // KeyInput
            // 
            this.KeyInput.Font = new System.Drawing.Font("Nunito", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyInput.Location = new System.Drawing.Point(292, 108);
            this.KeyInput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.KeyInput.Name = "KeyInput";
            this.KeyInput.Size = new System.Drawing.Size(473, 29);
            this.KeyInput.TabIndex = 3;
            // 
            // IVInput
            // 
            this.IVInput.Font = new System.Drawing.Font("Nunito", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IVInput.Location = new System.Drawing.Point(292, 175);
            this.IVInput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.IVInput.Name = "IVInput";
            this.IVInput.Size = new System.Drawing.Size(473, 29);
            this.IVInput.TabIndex = 4;
            // 
            // KeyLabel
            // 
            this.KeyLabel.AutoSize = true;
            this.KeyLabel.Font = new System.Drawing.Font("Nunito", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyLabel.Location = new System.Drawing.Point(292, 81);
            this.KeyLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.KeyLabel.Name = "KeyLabel";
            this.KeyLabel.Size = new System.Drawing.Size(37, 22);
            this.KeyLabel.TabIndex = 6;
            this.KeyLabel.Text = "Key";
            this.KeyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // radioCBC
            // 
            this.radioCBC.AutoSize = true;
            this.radioCBC.Checked = true;
            this.radioCBC.Location = new System.Drawing.Point(16, 32);
            this.radioCBC.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioCBC.Name = "radioCBC";
            this.radioCBC.Size = new System.Drawing.Size(61, 26);
            this.radioCBC.TabIndex = 11;
            this.radioCBC.TabStop = true;
            this.radioCBC.Text = "CBC";
            this.radioCBC.UseVisualStyleBackColor = true;
            this.radioCBC.CheckedChanged += new System.EventHandler(this.radioCBC_CheckedChanged);
            // 
            // radioECB
            // 
            this.radioECB.AutoSize = true;
            this.radioECB.Location = new System.Drawing.Point(16, 68);
            this.radioECB.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioECB.Name = "radioECB";
            this.radioECB.Size = new System.Drawing.Size(59, 26);
            this.radioECB.TabIndex = 12;
            this.radioECB.Text = "ECB";
            this.radioECB.UseVisualStyleBackColor = true;
            this.radioECB.CheckedChanged += new System.EventHandler(this.radioECB_CheckedChanged);
            // 
            // radioCFB
            // 
            this.radioCFB.AutoSize = true;
            this.radioCFB.Location = new System.Drawing.Point(16, 104);
            this.radioCFB.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioCFB.Name = "radioCFB";
            this.radioCFB.Size = new System.Drawing.Size(59, 26);
            this.radioCFB.TabIndex = 13;
            this.radioCFB.Text = "CFB";
            this.radioCFB.UseVisualStyleBackColor = true;
            this.radioCFB.CheckedChanged += new System.EventHandler(this.radioCFB_CheckedChanged);
            // 
            // radioCTR
            // 
            this.radioCTR.AutoSize = true;
            this.radioCTR.Font = new System.Drawing.Font("Nunito", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioCTR.Location = new System.Drawing.Point(16, 141);
            this.radioCTR.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioCTR.Name = "radioCTR";
            this.radioCTR.Size = new System.Drawing.Size(60, 26);
            this.radioCTR.TabIndex = 14;
            this.radioCTR.Text = "CTR";
            this.radioCTR.UseVisualStyleBackColor = true;
            this.radioCTR.CheckedChanged += new System.EventHandler(this.radioCTR_CheckedChanged);
            // 
            // radioOFB
            // 
            this.radioOFB.AutoSize = true;
            this.radioOFB.Font = new System.Drawing.Font("Nunito", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioOFB.Location = new System.Drawing.Point(16, 176);
            this.radioOFB.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioOFB.Name = "radioOFB";
            this.radioOFB.Size = new System.Drawing.Size(60, 26);
            this.radioOFB.TabIndex = 15;
            this.radioOFB.Text = "OFB";
            this.radioOFB.UseVisualStyleBackColor = true;
            this.radioOFB.CheckedChanged += new System.EventHandler(this.radioOFB_CheckedChanged);
            // 
            // groupMode
            // 
            this.groupMode.Controls.Add(this.radioCBC);
            this.groupMode.Controls.Add(this.radioECB);
            this.groupMode.Controls.Add(this.radioCTR);
            this.groupMode.Controls.Add(this.radioCFB);
            this.groupMode.Controls.Add(this.radioOFB);
            this.groupMode.Font = new System.Drawing.Font("Nunito", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupMode.Location = new System.Drawing.Point(14, 14);
            this.groupMode.Margin = new System.Windows.Forms.Padding(5);
            this.groupMode.Name = "groupMode";
            this.groupMode.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupMode.Size = new System.Drawing.Size(143, 211);
            this.groupMode.TabIndex = 16;
            this.groupMode.TabStop = false;
            this.groupMode.Text = "Chaining Mode";
            // 
            // btnIVRandom
            // 
            this.btnIVRandom.Font = new System.Drawing.Font("Nunito", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIVRandom.Location = new System.Drawing.Point(773, 175);
            this.btnIVRandom.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnIVRandom.Name = "btnIVRandom";
            this.btnIVRandom.Size = new System.Drawing.Size(88, 29);
            this.btnIVRandom.TabIndex = 18;
            this.btnIVRandom.Text = "Random";
            this.btnIVRandom.UseVisualStyleBackColor = true;
            this.btnIVRandom.Click += new System.EventHandler(this.btnIVRandom_Click);
            // 
            // IVLabel
            // 
            this.IVLabel.AutoSize = true;
            this.IVLabel.Font = new System.Drawing.Font("Nunito", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IVLabel.Location = new System.Drawing.Point(292, 148);
            this.IVLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.IVLabel.Name = "IVLabel";
            this.IVLabel.Size = new System.Drawing.Size(25, 22);
            this.IVLabel.TabIndex = 21;
            this.IVLabel.Text = "IV";
            this.IVLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BlockSizeLabel
            // 
            this.BlockSizeLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BlockSizeLabel.Font = new System.Drawing.Font("Nunito", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BlockSizeLabel.Location = new System.Drawing.Point(166, 164);
            this.BlockSizeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.BlockSizeLabel.Name = "BlockSizeLabel";
            this.BlockSizeLabel.Size = new System.Drawing.Size(119, 61);
            this.BlockSizeLabel.TabIndex = 22;
            this.BlockSizeLabel.Text = "Block Size: 128 bits";
            this.BlockSizeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupKey
            // 
            this.groupKey.Controls.Add(this.radioKey128);
            this.groupKey.Controls.Add(this.radioKey192);
            this.groupKey.Controls.Add(this.radioKey256);
            this.groupKey.Font = new System.Drawing.Font("Nunito", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupKey.Location = new System.Drawing.Point(166, 14);
            this.groupKey.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupKey.Name = "groupKey";
            this.groupKey.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupKey.Size = new System.Drawing.Size(118, 142);
            this.groupKey.TabIndex = 17;
            this.groupKey.TabStop = false;
            this.groupKey.Text = "Key Size";
            // 
            // radioKey128
            // 
            this.radioKey128.AutoSize = true;
            this.radioKey128.Checked = true;
            this.radioKey128.Location = new System.Drawing.Point(16, 32);
            this.radioKey128.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioKey128.Name = "radioKey128";
            this.radioKey128.Size = new System.Drawing.Size(89, 26);
            this.radioKey128.TabIndex = 11;
            this.radioKey128.TabStop = true;
            this.radioKey128.Text = "128 bits";
            this.radioKey128.UseVisualStyleBackColor = true;
            this.radioKey128.CheckedChanged += new System.EventHandler(this.radioKey128_CheckedChanged);
            // 
            // radioKey192
            // 
            this.radioKey192.AutoSize = true;
            this.radioKey192.Location = new System.Drawing.Point(16, 68);
            this.radioKey192.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioKey192.Name = "radioKey192";
            this.radioKey192.Size = new System.Drawing.Size(89, 26);
            this.radioKey192.TabIndex = 12;
            this.radioKey192.Text = "192 bits";
            this.radioKey192.UseVisualStyleBackColor = true;
            this.radioKey192.CheckedChanged += new System.EventHandler(this.radioKey192_CheckedChanged);
            // 
            // radioKey256
            // 
            this.radioKey256.AutoSize = true;
            this.radioKey256.Location = new System.Drawing.Point(16, 104);
            this.radioKey256.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioKey256.Name = "radioKey256";
            this.radioKey256.Size = new System.Drawing.Size(89, 26);
            this.radioKey256.TabIndex = 13;
            this.radioKey256.Text = "256 bits";
            this.radioKey256.UseVisualStyleBackColor = true;
            this.radioKey256.CheckedChanged += new System.EventHandler(this.radioKey256_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Nunito", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(477, 3);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 22);
            this.label1.TabIndex = 26;
            this.label1.Text = "Output";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Nunito", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 3);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 22);
            this.label2.TabIndex = 25;
            this.label2.Text = "Input";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // InputTextBox
            // 
            this.InputTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.InputTextBox.Font = new System.Drawing.Font("Nunito", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InputTextBox.Location = new System.Drawing.Point(12, 30);
            this.InputTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.InputTextBox.Multiline = true;
            this.InputTextBox.Name = "InputTextBox";
            this.InputTextBox.Size = new System.Drawing.Size(350, 207);
            this.InputTextBox.TabIndex = 24;
            // 
            // OutputTextBox
            // 
            this.OutputTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OutputTextBox.Font = new System.Drawing.Font("Nunito", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OutputTextBox.Location = new System.Drawing.Point(477, 30);
            this.OutputTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.OutputTextBox.Multiline = true;
            this.OutputTextBox.Name = "OutputTextBox";
            this.OutputTextBox.Size = new System.Drawing.Size(350, 207);
            this.OutputTextBox.TabIndex = 23;
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Location = new System.Drawing.Point(725, 30);
            this.btnSelectFile.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(104, 29);
            this.btnSelectFile.TabIndex = 30;
            this.btnSelectFile.Text = "Select File";
            this.btnSelectFile.UseVisualStyleBackColor = true;
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // selectedFileBox
            // 
            this.selectedFileBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.selectedFileBox.BackColor = System.Drawing.SystemColors.Window;
            this.selectedFileBox.Location = new System.Drawing.Point(12, 30);
            this.selectedFileBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.selectedFileBox.Name = "selectedFileBox";
            this.selectedFileBox.Size = new System.Drawing.Size(707, 29);
            this.selectedFileBox.TabIndex = 31;
            this.selectedFileBox.Text = "Select a file...";
            this.selectedFileBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.selectedFileBox_MouseClick);
            // 
            // btnSelectKey
            // 
            this.btnSelectKey.Location = new System.Drawing.Point(400, 212);
            this.btnSelectKey.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSelectKey.Name = "btnSelectKey";
            this.btnSelectKey.Size = new System.Drawing.Size(100, 29);
            this.btnSelectKey.TabIndex = 32;
            this.btnSelectKey.Text = "Select Key";
            this.btnSelectKey.UseVisualStyleBackColor = true;
            this.btnSelectKey.Click += new System.EventHandler(this.btnSelectKey_Click);
            // 
            // btnSwap
            // 
            this.btnSwap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSwap.Location = new System.Drawing.Point(398, 111);
            this.btnSwap.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSwap.Name = "btnSwap";
            this.btnSwap.Size = new System.Drawing.Size(45, 45);
            this.btnSwap.TabIndex = 35;
            this.btnSwap.Text = "<--";
            this.btnSwap.UseVisualStyleBackColor = true;
            this.btnSwap.Click += new System.EventHandler(this.btnSwap_Click);
            // 
            // btnKeyRandom
            // 
            this.btnKeyRandom.Font = new System.Drawing.Font("Nunito", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKeyRandom.Location = new System.Drawing.Point(773, 108);
            this.btnKeyRandom.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnKeyRandom.Name = "btnKeyRandom";
            this.btnKeyRandom.Size = new System.Drawing.Size(88, 29);
            this.btnKeyRandom.TabIndex = 34;
            this.btnKeyRandom.Text = "Random";
            this.btnKeyRandom.UseVisualStyleBackColor = true;
            this.btnKeyRandom.Click += new System.EventHandler(this.btnKeyRandom_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.historyListBox);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(868, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 593);
            this.panel1.TabIndex = 36;
            // 
            // historyListBox
            // 
            this.historyListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.historyListBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.historyListBox.FormattingEnabled = true;
            this.historyListBox.ItemHeight = 22;
            this.historyListBox.Location = new System.Drawing.Point(0, 41);
            this.historyListBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.historyListBox.Name = "historyListBox";
            this.historyListBox.Size = new System.Drawing.Size(200, 552);
            this.historyListBox.TabIndex = 0;
            this.historyListBox.SelectedIndexChanged += new System.EventHandler(this.historyListBox_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(200, 41);
            this.label5.TabIndex = 1;
            this.label5.Text = "History";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabText);
            this.tabControl1.Controls.Add(this.tabFile);
            this.tabControl1.Location = new System.Drawing.Point(14, 233);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(848, 289);
            this.tabControl1.TabIndex = 37;
            // 
            // tabText
            // 
            this.tabText.BackColor = System.Drawing.SystemColors.Window;
            this.tabText.Controls.Add(this.label2);
            this.tabText.Controls.Add(this.label1);
            this.tabText.Controls.Add(this.InputTextBox);
            this.tabText.Controls.Add(this.btnSwap);
            this.tabText.Controls.Add(this.OutputTextBox);
            this.tabText.Location = new System.Drawing.Point(4, 31);
            this.tabText.Name = "tabText";
            this.tabText.Padding = new System.Windows.Forms.Padding(3);
            this.tabText.Size = new System.Drawing.Size(840, 254);
            this.tabText.TabIndex = 0;
            this.tabText.Text = "Plain Text";
            this.tabText.ToolTipText = "Encrypt / Decrypt Text";
            // 
            // tabFile
            // 
            this.tabFile.BackColor = System.Drawing.SystemColors.Window;
            this.tabFile.Controls.Add(this.label6);
            this.tabFile.Controls.Add(this.label3);
            this.tabFile.Controls.Add(this.selectedSaveBox);
            this.tabFile.Controls.Add(this.btnSelectLocation);
            this.tabFile.Controls.Add(this.selectedFileBox);
            this.tabFile.Controls.Add(this.btnSelectFile);
            this.tabFile.Location = new System.Drawing.Point(4, 31);
            this.tabFile.Name = "tabFile";
            this.tabFile.Padding = new System.Windows.Forms.Padding(3);
            this.tabFile.Size = new System.Drawing.Size(840, 254);
            this.tabFile.TabIndex = 1;
            this.tabFile.Text = "File";
            this.tabFile.ToolTipText = "Encrypt / Decrypt File";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Nunito", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(12, 78);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 22);
            this.label6.TabIndex = 40;
            this.label6.Text = "Output File";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Nunito", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 3);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(165, 22);
            this.label3.TabIndex = 40;
            this.label3.Text = "Encrypt / Decrypt File";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // selectedSaveBox
            // 
            this.selectedSaveBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.selectedSaveBox.BackColor = System.Drawing.SystemColors.Window;
            this.selectedSaveBox.Location = new System.Drawing.Point(12, 105);
            this.selectedSaveBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.selectedSaveBox.Name = "selectedSaveBox";
            this.selectedSaveBox.Size = new System.Drawing.Size(682, 29);
            this.selectedSaveBox.TabIndex = 31;
            this.selectedSaveBox.Text = "Select save location...";
            this.selectedSaveBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.selectedSaveBox_MouseClick);
            // 
            // btnSelectLocation
            // 
            this.btnSelectLocation.Location = new System.Drawing.Point(700, 105);
            this.btnSelectLocation.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSelectLocation.Name = "btnSelectLocation";
            this.btnSelectLocation.Size = new System.Drawing.Size(129, 29);
            this.btnSelectLocation.TabIndex = 30;
            this.btnSelectLocation.Text = "Select Location";
            this.btnSelectLocation.UseVisualStyleBackColor = true;
            this.btnSelectLocation.Click += new System.EventHandler(this.btnSelectLocation_Click);
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // PassInput
            // 
            this.PassInput.Font = new System.Drawing.Font("Nunito", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PassInput.Location = new System.Drawing.Point(292, 41);
            this.PassInput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.PassInput.Name = "PassInput";
            this.PassInput.Size = new System.Drawing.Size(446, 29);
            this.PassInput.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Nunito", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(292, 14);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 22);
            this.label4.TabIndex = 6;
            this.label4.Text = "Password";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // genKeyIV
            // 
            this.genKeyIV.Font = new System.Drawing.Font("Nunito", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.genKeyIV.Location = new System.Drawing.Point(746, 41);
            this.genKeyIV.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.genKeyIV.Name = "genKeyIV";
            this.genKeyIV.Size = new System.Drawing.Size(115, 29);
            this.genKeyIV.TabIndex = 38;
            this.genKeyIV.Text = "Gen Key && IV";
            this.genKeyIV.UseVisualStyleBackColor = true;
            this.genKeyIV.Click += new System.EventHandler(this.genKeyIV_Click);
            // 
            // btnSelectIV
            // 
            this.btnSelectIV.Location = new System.Drawing.Point(590, 212);
            this.btnSelectIV.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSelectIV.Name = "btnSelectIV";
            this.btnSelectIV.Size = new System.Drawing.Size(100, 29);
            this.btnSelectIV.TabIndex = 39;
            this.btnSelectIV.Text = "Select IV";
            this.btnSelectIV.UseVisualStyleBackColor = true;
            this.btnSelectIV.Click += new System.EventHandler(this.btnSelectIV_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1068, 621);
            this.Controls.Add(this.btnSelectIV);
            this.Controls.Add(this.btnSelectKey);
            this.Controls.Add(this.genKeyIV);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnKeyRandom);
            this.Controls.Add(this.groupKey);
            this.Controls.Add(this.BlockSizeLabel);
            this.Controls.Add(this.IVLabel);
            this.Controls.Add(this.btnIVRandom);
            this.Controls.Add(this.groupMode);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.KeyLabel);
            this.Controls.Add(this.IVInput);
            this.Controls.Add(this.PassInput);
            this.Controls.Add(this.KeyInput);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnDecrypt);
            this.Controls.Add(this.btnEncrypt);
            this.Font = new System.Drawing.Font("Nunito", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1084, 660);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AES Demonstration";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupMode.ResumeLayout(false);
            this.groupMode.PerformLayout();
            this.groupKey.ResumeLayout(false);
            this.groupKey.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabText.ResumeLayout(false);
            this.tabText.PerformLayout();
            this.tabFile.ResumeLayout(false);
            this.tabFile.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnEncrypt;
        private System.Windows.Forms.Button btnDecrypt;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.TextBox KeyInput;
        private System.Windows.Forms.TextBox IVInput;
        private System.Windows.Forms.Label KeyLabel;
        private System.Windows.Forms.RadioButton radioCBC;
        private System.Windows.Forms.RadioButton radioECB;
        private System.Windows.Forms.RadioButton radioCFB;
        private System.Windows.Forms.RadioButton radioCTR;
        private System.Windows.Forms.RadioButton radioOFB;
        private System.Windows.Forms.GroupBox groupMode;
        private System.Windows.Forms.Button btnIVRandom;
        private System.Windows.Forms.Label IVLabel;
        private System.Windows.Forms.Label BlockSizeLabel;
        private System.Windows.Forms.GroupBox groupKey;
        private System.Windows.Forms.RadioButton radioKey128;
        private System.Windows.Forms.RadioButton radioKey192;
        private System.Windows.Forms.RadioButton radioKey256;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox InputTextBox;
        private System.Windows.Forms.TextBox OutputTextBox;
        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.TextBox selectedFileBox;
        private System.Windows.Forms.Button btnSelectKey;
        private System.Windows.Forms.Button btnSwap;
        private System.Windows.Forms.Button btnKeyRandom;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListBox historyListBox;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabFile;
        private System.Windows.Forms.TabPage tabText;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private System.Windows.Forms.Button genKeyIV;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox PassInput;
        private System.Windows.Forms.Button btnSelectIV;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox selectedSaveBox;
        private System.Windows.Forms.Button btnSelectLocation;
    }
}