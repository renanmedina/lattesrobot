namespace LattesRobotGUI
{
    partial class LattesRobotGUI
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LattesRobotGUI));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtIdsList = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rdTxtInput = new System.Windows.Forms.RadioButton();
            this.rdFileInput = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtSeparatorChar = new System.Windows.Forms.TextBox();
            this.rdSeparatorChar = new System.Windows.Forms.RadioButton();
            this.rdSeparatorLine = new System.Windows.Forms.RadioButton();
            this.btnOpenFolder = new System.Windows.Forms.Button();
            this.lblSelectedFile = new System.Windows.Forms.Label();
            this.rdXmlOutput = new System.Windows.Forms.RadioButton();
            this.rdZipOutput = new System.Windows.Forms.RadioButton();
            this.btnOutputFolder = new System.Windows.Forms.Button();
            this.txtOutputPath = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lblIdsCount = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblTimeSpent = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.listboxLattesIDS = new System.Windows.Forms.ListBox();
            this.folderOuputDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.btnStartRobot = new System.Windows.Forms.Button();
            this.openFileInputDialog = new System.Windows.Forms.OpenFileDialog();
            this.pgrDownloads = new System.Windows.Forms.ProgressBar();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.lblPercentage = new System.Windows.Forms.Label();
            this.lblProgress = new System.Windows.Forms.Label();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnStopRobot = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lblSearching = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cbLimitSearch = new System.Windows.Forms.ComboBox();
            this.lnkResultCount = new System.Windows.Forms.LinkLabel();
            this.btnDownloadSelects = new System.Windows.Forms.Button();
            this.dataGridResultSearch = new System.Windows.Forms.DataGridView();
            this.columnLattesID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kcodeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnEduc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSearchLattes = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSearchName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.tabControlMain.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridResultSearch)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtIdsList);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.btnOpenFolder);
            this.groupBox1.Controls.Add(this.lblSelectedFile);
            this.groupBox1.Controls.Add(this.rdXmlOutput);
            this.groupBox1.Controls.Add(this.rdZipOutput);
            this.groupBox1.Controls.Add(this.btnOutputFolder);
            this.groupBox1.Controls.Add(this.txtOutputPath);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(411, 306);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Configurações";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(11, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(156, 17);
            this.label4.TabIndex = 10;
            this.label4.Text = "Saída dos curriculos";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(209, 169);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "Lista de ID\'s ";
            // 
            // txtIdsList
            // 
            this.txtIdsList.Location = new System.Drawing.Point(209, 188);
            this.txtIdsList.Multiline = true;
            this.txtIdsList.Name = "txtIdsList";
            this.txtIdsList.Size = new System.Drawing.Size(196, 110);
            this.txtIdsList.TabIndex = 0;
            this.txtIdsList.TextChanged += new System.EventHandler(this.txtIdsList_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(7, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(139, 17);
            this.label2.TabIndex = 8;
            this.label2.Text = "Saída de arquivos";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(209, 115);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 17);
            this.label1.TabIndex = 7;
            this.label1.Text = "Arquivo de ID\'s";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rdTxtInput);
            this.groupBox2.Controls.Add(this.rdFileInput);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Location = new System.Drawing.Point(6, 119);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(197, 180);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tipo de entrada";
            // 
            // rdTxtInput
            // 
            this.rdTxtInput.AutoSize = true;
            this.rdTxtInput.Checked = true;
            this.rdTxtInput.Location = new System.Drawing.Point(108, 28);
            this.rdTxtInput.Name = "rdTxtInput";
            this.rdTxtInput.Size = new System.Drawing.Size(64, 21);
            this.rdTxtInput.TabIndex = 3;
            this.rdTxtInput.TabStop = true;
            this.rdTxtInput.Text = "Texto";
            this.rdTxtInput.UseVisualStyleBackColor = true;
            this.rdTxtInput.CheckedChanged += new System.EventHandler(this.rdTxtInput_CheckedChanged);
            // 
            // rdFileInput
            // 
            this.rdFileInput.AutoSize = true;
            this.rdFileInput.Location = new System.Drawing.Point(10, 28);
            this.rdFileInput.Name = "rdFileInput";
            this.rdFileInput.Size = new System.Drawing.Size(77, 21);
            this.rdFileInput.TabIndex = 2;
            this.rdFileInput.Text = "Arquivo";
            this.rdFileInput.UseVisualStyleBackColor = true;
            this.rdFileInput.CheckedChanged += new System.EventHandler(this.rdFileInput_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtSeparatorChar);
            this.groupBox3.Controls.Add(this.rdSeparatorChar);
            this.groupBox3.Controls.Add(this.rdSeparatorLine);
            this.groupBox3.Location = new System.Drawing.Point(10, 55);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(173, 111);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Separação de ID\'s";
            // 
            // txtSeparatorChar
            // 
            this.txtSeparatorChar.AcceptsReturn = true;
            this.txtSeparatorChar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSeparatorChar.Location = new System.Drawing.Point(123, 67);
            this.txtSeparatorChar.MaxLength = 1;
            this.txtSeparatorChar.Name = "txtSeparatorChar";
            this.txtSeparatorChar.Size = new System.Drawing.Size(38, 34);
            this.txtSeparatorChar.TabIndex = 2;
            this.txtSeparatorChar.Text = ",";
            this.txtSeparatorChar.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSeparatorChar.TextChanged += new System.EventHandler(this.txtSeparatorChar_TextChanged);
            // 
            // rdSeparatorChar
            // 
            this.rdSeparatorChar.AutoSize = true;
            this.rdSeparatorChar.Checked = true;
            this.rdSeparatorChar.Location = new System.Drawing.Point(11, 70);
            this.rdSeparatorChar.Name = "rdSeparatorChar";
            this.rdSeparatorChar.Size = new System.Drawing.Size(83, 21);
            this.rdSeparatorChar.TabIndex = 1;
            this.rdSeparatorChar.TabStop = true;
            this.rdSeparatorChar.Text = "Carácter";
            this.rdSeparatorChar.UseVisualStyleBackColor = true;
            this.rdSeparatorChar.CheckedChanged += new System.EventHandler(this.rdSeparatorChar_CheckedChanged);
            // 
            // rdSeparatorLine
            // 
            this.rdSeparatorLine.AutoSize = true;
            this.rdSeparatorLine.Location = new System.Drawing.Point(11, 31);
            this.rdSeparatorLine.Name = "rdSeparatorLine";
            this.rdSeparatorLine.Size = new System.Drawing.Size(131, 21);
            this.rdSeparatorLine.TabIndex = 0;
            this.rdSeparatorLine.Text = "Quebra de linha";
            this.rdSeparatorLine.UseVisualStyleBackColor = true;
            this.rdSeparatorLine.CheckedChanged += new System.EventHandler(this.rdSeparatorLine_CheckedChanged);
            // 
            // btnOpenFolder
            // 
            this.btnOpenFolder.Location = new System.Drawing.Point(266, 74);
            this.btnOpenFolder.Name = "btnOpenFolder";
            this.btnOpenFolder.Size = new System.Drawing.Size(127, 24);
            this.btnOpenFolder.TabIndex = 6;
            this.btnOpenFolder.Text = "Abrir pasta";
            this.btnOpenFolder.UseVisualStyleBackColor = true;
            this.btnOpenFolder.Click += new System.EventHandler(this.btnOpenFolder_Click);
            // 
            // lblSelectedFile
            // 
            this.lblSelectedFile.AutoSize = true;
            this.lblSelectedFile.Location = new System.Drawing.Point(209, 132);
            this.lblSelectedFile.Name = "lblSelectedFile";
            this.lblSelectedFile.Size = new System.Drawing.Size(192, 17);
            this.lblSelectedFile.TabIndex = 5;
            this.lblSelectedFile.Text = "Nenhum arquivo selecionado";
            // 
            // rdXmlOutput
            // 
            this.rdXmlOutput.AutoSize = true;
            this.rdXmlOutput.Location = new System.Drawing.Point(72, 94);
            this.rdXmlOutput.Name = "rdXmlOutput";
            this.rdXmlOutput.Size = new System.Drawing.Size(53, 21);
            this.rdXmlOutput.TabIndex = 3;
            this.rdXmlOutput.Text = ".xml";
            this.rdXmlOutput.UseVisualStyleBackColor = true;
            // 
            // rdZipOutput
            // 
            this.rdZipOutput.AutoSize = true;
            this.rdZipOutput.Checked = true;
            this.rdZipOutput.Location = new System.Drawing.Point(10, 92);
            this.rdZipOutput.Name = "rdZipOutput";
            this.rdZipOutput.Size = new System.Drawing.Size(51, 21);
            this.rdZipOutput.TabIndex = 2;
            this.rdZipOutput.TabStop = true;
            this.rdZipOutput.Text = ".zip";
            this.rdZipOutput.UseVisualStyleBackColor = true;
            // 
            // btnOutputFolder
            // 
            this.btnOutputFolder.Location = new System.Drawing.Point(266, 42);
            this.btnOutputFolder.Name = "btnOutputFolder";
            this.btnOutputFolder.Size = new System.Drawing.Size(127, 26);
            this.btnOutputFolder.TabIndex = 1;
            this.btnOutputFolder.Text = "Selecionar pasta";
            this.btnOutputFolder.UseVisualStyleBackColor = true;
            this.btnOutputFolder.Click += new System.EventHandler(this.btnOutputFolder_Click);
            // 
            // txtOutputPath
            // 
            this.txtOutputPath.Location = new System.Drawing.Point(10, 42);
            this.txtOutputPath.Multiline = true;
            this.txtOutputPath.Name = "txtOutputPath";
            this.txtOutputPath.ReadOnly = true;
            this.txtOutputPath.Size = new System.Drawing.Size(250, 24);
            this.txtOutputPath.TabIndex = 0;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.lblIdsCount);
            this.groupBox5.Controls.Add(this.label10);
            this.groupBox5.Controls.Add(this.lblTimeSpent);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Controls.Add(this.listboxLattesIDS);
            this.groupBox5.Location = new System.Drawing.Point(423, 6);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(197, 389);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Lattes Download list";
            // 
            // lblIdsCount
            // 
            this.lblIdsCount.AutoSize = true;
            this.lblIdsCount.Location = new System.Drawing.Point(98, 341);
            this.lblIdsCount.Name = "lblIdsCount";
            this.lblIdsCount.Size = new System.Drawing.Size(16, 17);
            this.lblIdsCount.TabIndex = 10;
            this.lblIdsCount.Text = "0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(6, 341);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(97, 17);
            this.label10.TabIndex = 9;
            this.label10.Text = "Quantidade:";
            // 
            // lblTimeSpent
            // 
            this.lblTimeSpent.AutoSize = true;
            this.lblTimeSpent.Location = new System.Drawing.Point(142, 364);
            this.lblTimeSpent.Name = "lblTimeSpent";
            this.lblTimeSpent.Size = new System.Drawing.Size(23, 17);
            this.lblTimeSpent.TabIndex = 8;
            this.lblTimeSpent.Text = "0s";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(6, 364);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(136, 17);
            this.label9.TabIndex = 7;
            this.label9.Text = "Tempo decorrido:";
            // 
            // listboxLattesIDS
            // 
            this.listboxLattesIDS.FormattingEnabled = true;
            this.listboxLattesIDS.ItemHeight = 16;
            this.listboxLattesIDS.Location = new System.Drawing.Point(6, 22);
            this.listboxLattesIDS.Name = "listboxLattesIDS";
            this.listboxLattesIDS.Size = new System.Drawing.Size(185, 308);
            this.listboxLattesIDS.TabIndex = 6;
            // 
            // btnStartRobot
            // 
            this.btnStartRobot.Location = new System.Drawing.Point(5, 401);
            this.btnStartRobot.Name = "btnStartRobot";
            this.btnStartRobot.Size = new System.Drawing.Size(313, 41);
            this.btnStartRobot.TabIndex = 5;
            this.btnStartRobot.Text = "INICIAR ROBOT !";
            this.btnStartRobot.UseVisualStyleBackColor = true;
            this.btnStartRobot.Click += new System.EventHandler(this.btnStartRobot_Click);
            // 
            // pgrDownloads
            // 
            this.pgrDownloads.Location = new System.Drawing.Point(7, 26);
            this.pgrDownloads.Name = "pgrDownloads";
            this.pgrDownloads.Size = new System.Drawing.Size(398, 24);
            this.pgrDownloads.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pgrDownloads.TabIndex = 7;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.lblPercentage);
            this.groupBox6.Controls.Add(this.lblProgress);
            this.groupBox6.Controls.Add(this.pgrDownloads);
            this.groupBox6.Location = new System.Drawing.Point(6, 314);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(411, 81);
            this.groupBox6.TabIndex = 9;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Progresso";
            // 
            // lblPercentage
            // 
            this.lblPercentage.AutoSize = true;
            this.lblPercentage.Location = new System.Drawing.Point(368, 56);
            this.lblPercentage.Name = "lblPercentage";
            this.lblPercentage.Size = new System.Drawing.Size(0, 17);
            this.lblPercentage.TabIndex = 9;
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Location = new System.Drawing.Point(6, 56);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(0, 17);
            this.lblProgress.TabIndex = 8;
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabPage1);
            this.tabControlMain.Controls.Add(this.tabPage2);
            this.tabControlMain.Location = new System.Drawing.Point(2, 3);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(634, 479);
            this.tabControlMain.TabIndex = 10;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnStopRobot);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.groupBox6);
            this.tabPage1.Controls.Add(this.btnStartRobot);
            this.tabPage1.Controls.Add(this.groupBox5);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(626, 450);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Robot";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnStopRobot
            // 
            this.btnStopRobot.Enabled = false;
            this.btnStopRobot.Location = new System.Drawing.Point(324, 401);
            this.btnStopRobot.Name = "btnStopRobot";
            this.btnStopRobot.Size = new System.Drawing.Size(296, 41);
            this.btnStopRobot.TabIndex = 10;
            this.btnStopRobot.Text = "STOP ROBOT!";
            this.btnStopRobot.UseVisualStyleBackColor = true;
            this.btnStopRobot.Click += new System.EventHandler(this.btnStopRobot_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox4);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(626, 450);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Pesquisar curriculos";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lblSearching);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.cbLimitSearch);
            this.groupBox4.Controls.Add(this.lnkResultCount);
            this.groupBox4.Controls.Add(this.btnDownloadSelects);
            this.groupBox4.Controls.Add(this.dataGridResultSearch);
            this.groupBox4.Controls.Add(this.btnSearchLattes);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.txtSearchName);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Location = new System.Drawing.Point(6, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(613, 441);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Pesquisar curriculo lattes";
            // 
            // lblSearching
            // 
            this.lblSearching.AutoSize = true;
            this.lblSearching.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearching.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblSearching.Location = new System.Drawing.Point(208, 229);
            this.lblSearching.Name = "lblSearching";
            this.lblSearching.Size = new System.Drawing.Size(180, 20);
            this.lblSearching.TabIndex = 15;
            this.lblSearching.Text = "Procurando, aguarde....";
            this.lblSearching.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(529, 50);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(63, 17);
            this.label8.TabIndex = 9;
            this.label8.Text = "registros";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(458, 26);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 17);
            this.label7.TabIndex = 8;
            this.label7.Text = "Limite";
            // 
            // cbLimitSearch
            // 
            this.cbLimitSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLimitSearch.FormattingEnabled = true;
            this.cbLimitSearch.Items.AddRange(new object[] {
            "25",
            "50",
            "100",
            "200",
            "500",
            "700",
            "999"});
            this.cbLimitSearch.Location = new System.Drawing.Point(461, 45);
            this.cbLimitSearch.Name = "cbLimitSearch";
            this.cbLimitSearch.Size = new System.Drawing.Size(65, 24);
            this.cbLimitSearch.TabIndex = 7;
            this.cbLimitSearch.SelectedIndexChanged += new System.EventHandler(this.cbLimitSearch_SelectedIndexChanged);
            // 
            // lnkResultCount
            // 
            this.lnkResultCount.AutoSize = true;
            this.lnkResultCount.Location = new System.Drawing.Point(7, 381);
            this.lnkResultCount.Name = "lnkResultCount";
            this.lnkResultCount.Size = new System.Drawing.Size(104, 17);
            this.lnkResultCount.TabIndex = 6;
            this.lnkResultCount.TabStop = true;
            this.lnkResultCount.Text = "exibindo 0 de 0";
            this.lnkResultCount.Visible = false;
            // 
            // btnDownloadSelects
            // 
            this.btnDownloadSelects.Enabled = false;
            this.btnDownloadSelects.Location = new System.Drawing.Point(6, 404);
            this.btnDownloadSelects.Name = "btnDownloadSelects";
            this.btnDownloadSelects.Size = new System.Drawing.Size(306, 29);
            this.btnDownloadSelects.TabIndex = 5;
            this.btnDownloadSelects.Text = "Adicionar selecionados à lista de download";
            this.btnDownloadSelects.UseVisualStyleBackColor = true;
            this.btnDownloadSelects.Click += new System.EventHandler(this.btnDownloadSelects_Click);
            // 
            // dataGridResultSearch
            // 
            this.dataGridResultSearch.AllowUserToAddRows = false;
            this.dataGridResultSearch.AllowUserToDeleteRows = false;
            this.dataGridResultSearch.AllowUserToResizeRows = false;
            this.dataGridResultSearch.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridResultSearch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridResultSearch.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnLattesID,
            this.kcodeColumn,
            this.nameColumn,
            this.columnEduc});
            this.dataGridResultSearch.Location = new System.Drawing.Point(7, 95);
            this.dataGridResultSearch.Name = "dataGridResultSearch";
            this.dataGridResultSearch.ReadOnly = true;
            this.dataGridResultSearch.RowHeadersVisible = false;
            this.dataGridResultSearch.RowTemplate.Height = 24;
            this.dataGridResultSearch.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridResultSearch.Size = new System.Drawing.Size(600, 283);
            this.dataGridResultSearch.TabIndex = 4;
            this.dataGridResultSearch.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridResultSearch_CellDoubleClick);
            // 
            // columnLattesID
            // 
            this.columnLattesID.HeaderText = "Código Lattes";
            this.columnLattesID.Name = "columnLattesID";
            this.columnLattesID.ReadOnly = true;
            this.columnLattesID.Width = 125;
            // 
            // kcodeColumn
            // 
            this.kcodeColumn.HeaderText = "Antigo ID";
            this.kcodeColumn.Name = "kcodeColumn";
            this.kcodeColumn.ReadOnly = true;
            // 
            // nameColumn
            // 
            this.nameColumn.HeaderText = "Nome";
            this.nameColumn.Name = "nameColumn";
            this.nameColumn.ReadOnly = true;
            this.nameColumn.Width = 150;
            // 
            // columnEduc
            // 
            this.columnEduc.HeaderText = "Educação";
            this.columnEduc.Name = "columnEduc";
            this.columnEduc.ReadOnly = true;
            this.columnEduc.Width = 700;
            // 
            // btnSearchLattes
            // 
            this.btnSearchLattes.Location = new System.Drawing.Point(367, 43);
            this.btnSearchLattes.Name = "btnSearchLattes";
            this.btnSearchLattes.Size = new System.Drawing.Size(88, 27);
            this.btnSearchLattes.TabIndex = 3;
            this.btnSearchLattes.Text = "Pesquisar";
            this.btnSearchLattes.UseVisualStyleBackColor = true;
            this.btnSearchLattes.Click += new System.EventHandler(this.btnSearchLattes_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(7, 77);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(189, 17);
            this.label6.TabIndex = 2;
            this.label6.Text = "Resultados encontrados:";
            // 
            // txtSearchName
            // 
            this.txtSearchName.Location = new System.Drawing.Point(10, 45);
            this.txtSearchName.Multiline = true;
            this.txtSearchName.Name = "txtSearchName";
            this.txtSearchName.Size = new System.Drawing.Size(352, 22);
            this.txtSearchName.TabIndex = 1;
            this.txtSearchName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearchName_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(4, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(139, 17);
            this.label5.TabIndex = 0;
            this.label5.Text = " Nome da pessoa:";
            // 
            // LattesRobotGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(636, 483);
            this.Controls.Add(this.tabControlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "LattesRobotGUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LattesRobot";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.tabControlMain.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridResultSearch)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnOutputFolder;
        private System.Windows.Forms.TextBox txtOutputPath;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rdTxtInput;
        private System.Windows.Forms.RadioButton rdFileInput;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtSeparatorChar;
        private System.Windows.Forms.RadioButton rdSeparatorChar;
        private System.Windows.Forms.RadioButton rdSeparatorLine;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.FolderBrowserDialog folderOuputDialog;
        private System.Windows.Forms.TextBox txtIdsList;
        private System.Windows.Forms.Button btnStartRobot;
        private System.Windows.Forms.RadioButton rdXmlOutput;
        private System.Windows.Forms.RadioButton rdZipOutput;
        private System.Windows.Forms.OpenFileDialog openFileInputDialog;
        private System.Windows.Forms.ListBox listboxLattesIDS;
        private System.Windows.Forms.Label lblSelectedFile;
        private System.Windows.Forms.Button btnOpenFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar pgrDownloads;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnSearchLattes;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSearchName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dataGridResultSearch;
        private System.Windows.Forms.Button btnDownloadSelects;
        private System.Windows.Forms.LinkLabel lnkResultCount;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbLimitSearch;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblPercentage;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnLattesID;
        private System.Windows.Forms.DataGridViewTextBoxColumn kcodeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnEduc;
        private System.Windows.Forms.Label lblSearching;
        private System.Windows.Forms.Button btnStopRobot;
        private System.Windows.Forms.Label lblTimeSpent;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblIdsCount;
        private System.Windows.Forms.Label label10;
    }
}

