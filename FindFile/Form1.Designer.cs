namespace FindFile
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.InFoolderBtn = new System.Windows.Forms.Button();
            this.FileNameLbl = new System.Windows.Forms.Label();
            this.FindStartStopBtn = new System.Windows.Forms.Button();
            this.KeySymbLbl = new System.Windows.Forms.Label();
            this.PathTB = new System.Windows.Forms.TextBox();

            this.treeFolders = new BufferedTreeView();

            this.fileNameMaskTB = new System.Windows.Forms.TextBox();
            this.keyWordTB = new System.Windows.Forms.TextBox();
            this.searcMenu = new System.Windows.Forms.GroupBox();
            this.PauseFindBtn = new System.Windows.Forms.Button();
            this.timerLbl = new System.Windows.Forms.Label();
            this.filesCounterLbl = new System.Windows.Forms.Label();
            this.fileNowLBL = new System.Windows.Forms.Label();
            this.progressSearch = new System.Windows.Forms.ProgressBar();
            this.TimerPaint = new System.Windows.Forms.Timer(this.components);
            this.searcMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // InFoolderBtn
            // 
            this.InFoolderBtn.Location = new System.Drawing.Point(15, 9);
            this.InFoolderBtn.Name = "InFoolderBtn";
            this.InFoolderBtn.Size = new System.Drawing.Size(75, 24);
            this.InFoolderBtn.TabIndex = 0;
            this.InFoolderBtn.Text = "Папка";
            this.InFoolderBtn.UseVisualStyleBackColor = true;
            this.InFoolderBtn.Click += new System.EventHandler(this.InFoolderBtn_Click);
            // 
            // FileNameLbl
            // 
            this.FileNameLbl.AutoSize = true;
            this.FileNameLbl.Location = new System.Drawing.Point(12, 39);
            this.FileNameLbl.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.FileNameLbl.Name = "FileNameLbl";
            this.FileNameLbl.Size = new System.Drawing.Size(86, 17);
            this.FileNameLbl.TabIndex = 1;
            this.FileNameLbl.Text = "Имя файла:";
            // 
            // FindStartStopBtn
            // 
            this.FindStartStopBtn.Location = new System.Drawing.Point(337, 9);
            this.FindStartStopBtn.Name = "FindStartStopBtn";
            this.FindStartStopBtn.Size = new System.Drawing.Size(86, 78);
            this.FindStartStopBtn.TabIndex = 2;
            this.FindStartStopBtn.Text = "Поиск";
            this.FindStartStopBtn.UseVisualStyleBackColor = true;
            this.FindStartStopBtn.Click += new System.EventHandler(this.FindStartStopBtn_Click);
            // 
            // KeySymbLbl
            // 
            this.KeySymbLbl.AutoSize = true;
            this.KeySymbLbl.Location = new System.Drawing.Point(12, 70);
            this.KeySymbLbl.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.KeySymbLbl.Name = "KeySymbLbl";
            this.KeySymbLbl.Size = new System.Drawing.Size(111, 17);
            this.KeySymbLbl.TabIndex = 3;
            this.KeySymbLbl.Text = "Найти в файле:";
            // 
            // PathTB
            // 
            this.PathTB.CausesValidation = false;
            this.PathTB.Enabled = false;
            this.PathTB.Location = new System.Drawing.Point(129, 9);
            this.PathTB.Name = "PathTB";
            this.PathTB.Size = new System.Drawing.Size(202, 22);
            this.PathTB.TabIndex = 4;
            // 
            // treeFolders
            // 
            this.treeFolders.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeFolders.Location = new System.Drawing.Point(15, 95);
            this.treeFolders.Name = "treeFolders";
            this.treeFolders.Size = new System.Drawing.Size(408, 284);
            this.treeFolders.TabIndex = 5;
            // 
            // fileNameMaskTB
            // 
            this.fileNameMaskTB.Location = new System.Drawing.Point(129, 38);
            this.fileNameMaskTB.Name = "fileNameMaskTB";
            this.fileNameMaskTB.Size = new System.Drawing.Size(202, 22);
            this.fileNameMaskTB.TabIndex = 6;
            // 
            // keyWordTB
            // 
            this.keyWordTB.Location = new System.Drawing.Point(129, 67);
            this.keyWordTB.Name = "keyWordTB";
            this.keyWordTB.Size = new System.Drawing.Size(201, 22);
            this.keyWordTB.TabIndex = 7;
            // 
            // searcMenu
            // 
            this.searcMenu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.searcMenu.Controls.Add(this.PauseFindBtn);
            this.searcMenu.Controls.Add(this.timerLbl);
            this.searcMenu.Controls.Add(this.filesCounterLbl);
            this.searcMenu.Controls.Add(this.fileNowLBL);
            this.searcMenu.Controls.Add(this.progressSearch);
            this.searcMenu.Location = new System.Drawing.Point(12, 385);
            this.searcMenu.Name = "searcMenu";
            this.searcMenu.Size = new System.Drawing.Size(411, 95);
            this.searcMenu.TabIndex = 8;
            this.searcMenu.TabStop = false;
            this.searcMenu.Text = "Меню поиска";
            this.searcMenu.Visible = false;
            // 
            // PauseFindBtn
            // 
            this.PauseFindBtn.Location = new System.Drawing.Point(295, 16);
            this.PauseFindBtn.Name = "PauseFindBtn";
            this.PauseFindBtn.Size = new System.Drawing.Size(110, 23);
            this.PauseFindBtn.TabIndex = 4;
            this.PauseFindBtn.Text = "Пауза";
            this.PauseFindBtn.UseVisualStyleBackColor = true;
            this.PauseFindBtn.Click += new System.EventHandler(this.PauseFindBtn_Click);
            // 
            // timerLbl
            // 
            this.timerLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.timerLbl.AutoSize = true;
            this.timerLbl.Location = new System.Drawing.Point(341, 43);
            this.timerLbl.Name = "timerLbl";
            this.timerLbl.Size = new System.Drawing.Size(64, 17);
            this.timerLbl.TabIndex = 3;
            this.timerLbl.Text = "00:00:00";
            // 
            // filesCounterLbl
            // 
            this.filesCounterLbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filesCounterLbl.AutoSize = true;
            this.filesCounterLbl.BackColor = System.Drawing.Color.Transparent;
            this.filesCounterLbl.Location = new System.Drawing.Point(183, 69);
            this.filesCounterLbl.Name = "filesCounterLbl";
            this.filesCounterLbl.Size = new System.Drawing.Size(28, 17);
            this.filesCounterLbl.TabIndex = 2;
            this.filesCounterLbl.Text = "0/0";
            // 
            // fileNowLBL
            // 
            this.fileNowLBL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.fileNowLBL.AutoSize = true;
            this.fileNowLBL.Location = new System.Drawing.Point(7, 43);
            this.fileNowLBL.Name = "fileNowLBL";
            this.fileNowLBL.Size = new System.Drawing.Size(152, 17);
            this.fileNowLBL.TabIndex = 1;
            this.fileNowLBL.Text = "Расположение файла";
            // 
            // progressSearch
            // 
            this.progressSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressSearch.Location = new System.Drawing.Point(7, 66);
            this.progressSearch.Name = "progressSearch";
            this.progressSearch.Size = new System.Drawing.Size(398, 23);
            this.progressSearch.TabIndex = 0;
            // 
            // TimerPaint
            // 
            this.TimerPaint.Tick += new System.EventHandler(this.timerPaint_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(438, 492);
            this.Controls.Add(this.searcMenu);
            this.Controls.Add(this.keyWordTB);
            this.Controls.Add(this.fileNameMaskTB);
            this.Controls.Add(this.treeFolders);
            this.Controls.Add(this.PathTB);
            this.Controls.Add(this.KeySymbLbl);
            this.Controls.Add(this.FindStartStopBtn);
            this.Controls.Add(this.FileNameLbl);
            this.Controls.Add(this.InFoolderBtn);
            this.MaximumSize = new System.Drawing.Size(456, 900);
            this.MinimumSize = new System.Drawing.Size(456, 539);
            this.Name = "Form1";
            this.Text = "FIND FILE";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.searcMenu.ResumeLayout(false);
            this.searcMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button InFoolderBtn;
        private System.Windows.Forms.Label FileNameLbl;
        private System.Windows.Forms.Button FindStartStopBtn;
        private System.Windows.Forms.Label KeySymbLbl;
        private System.Windows.Forms.TextBox PathTB;
        private System.Windows.Forms.TreeView treeFolders;
        private System.Windows.Forms.TextBox fileNameMaskTB;
        private System.Windows.Forms.TextBox keyWordTB;
        private System.Windows.Forms.GroupBox searcMenu;
        private System.Windows.Forms.Button PauseFindBtn;
        private System.Windows.Forms.Label timerLbl;
        private System.Windows.Forms.Label filesCounterLbl;
        private System.Windows.Forms.Label fileNowLBL;
        private System.Windows.Forms.ProgressBar progressSearch;
        private System.Windows.Forms.Timer TimerPaint;
    }
}

