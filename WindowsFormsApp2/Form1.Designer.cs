namespace WindowsFormsApp2
{
    partial class Form1
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.DataGrid = new System.Windows.Forms.DataGridView();
            this.open = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.BtnKaydet = new System.Windows.Forms.Button();
            this.LblSure1 = new System.Windows.Forms.Label();
            this.LblSure2 = new System.Windows.Forms.Label();
            this.LblSure3 = new System.Windows.Forms.Label();
            this.LblThread = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // DataGrid
            // 
            this.DataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGrid.Location = new System.Drawing.Point(12, 61);
            this.DataGrid.Name = "DataGrid";
            this.DataGrid.RowTemplate.Height = 24;
            this.DataGrid.Size = new System.Drawing.Size(440, 370);
            this.DataGrid.TabIndex = 0;
            // 
            // open
            // 
            this.open.Location = new System.Drawing.Point(21, 12);
            this.open.Name = "open";
            this.open.Size = new System.Drawing.Size(102, 27);
            this.open.TabIndex = 2;
            this.open.Text = "AÇ";
            this.open.UseVisualStyleBackColor = true;
            this.open.Click += new System.EventHandler(this.open_Click);
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(458, 61);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(257, 372);
            this.listBox1.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(148, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(108, 27);
            this.button1.TabIndex = 4;
            this.button1.Text = "ÇÖZ";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // BtnKaydet
            // 
            this.BtnKaydet.Location = new System.Drawing.Point(283, 12);
            this.BtnKaydet.Name = "BtnKaydet";
            this.BtnKaydet.Size = new System.Drawing.Size(100, 27);
            this.BtnKaydet.TabIndex = 5;
            this.BtnKaydet.Text = "KAYDET";
            this.BtnKaydet.UseVisualStyleBackColor = true;
            this.BtnKaydet.Click += new System.EventHandler(this.BtnKaydet_Click);
            // 
            // LblSure1
            // 
            this.LblSure1.AutoSize = true;
            this.LblSure1.Location = new System.Drawing.Point(12, 441);
            this.LblSure1.Name = "LblSure1";
            this.LblSure1.Size = new System.Drawing.Size(58, 17);
            this.LblSure1.TabIndex = 6;
            this.LblSure1.Text = "Süre 1 :";
            // 
            // LblSure2
            // 
            this.LblSure2.AutoSize = true;
            this.LblSure2.Location = new System.Drawing.Point(12, 469);
            this.LblSure2.Name = "LblSure2";
            this.LblSure2.Size = new System.Drawing.Size(58, 17);
            this.LblSure2.TabIndex = 7;
            this.LblSure2.Text = "Süre 2 :";
            // 
            // LblSure3
            // 
            this.LblSure3.AutoSize = true;
            this.LblSure3.Location = new System.Drawing.Point(12, 496);
            this.LblSure3.Name = "LblSure3";
            this.LblSure3.Size = new System.Drawing.Size(58, 17);
            this.LblSure3.TabIndex = 8;
            this.LblSure3.Text = "Süre 3 :";
            // 
            // LblThread
            // 
            this.LblThread.AutoSize = true;
            this.LblThread.Location = new System.Drawing.Point(458, 38);
            this.LblThread.Name = "LblThread";
            this.LblThread.Size = new System.Drawing.Size(54, 17);
            this.LblThread.TabIndex = 9;
            this.LblThread.Text = "Thread";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 528);
            this.Controls.Add(this.LblThread);
            this.Controls.Add(this.LblSure3);
            this.Controls.Add(this.LblSure2);
            this.Controls.Add(this.LblSure1);
            this.Controls.Add(this.BtnKaydet);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.DataGrid);
            this.Controls.Add(this.open);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DataGrid;
        private System.Windows.Forms.Button open;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button BtnKaydet;
        private System.Windows.Forms.Label LblSure1;
        private System.Windows.Forms.Label LblSure2;
        private System.Windows.Forms.Label LblSure3;
        private System.Windows.Forms.Label LblThread;
    }
}

