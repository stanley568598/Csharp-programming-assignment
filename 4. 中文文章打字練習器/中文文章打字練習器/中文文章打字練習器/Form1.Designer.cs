namespace 中文文章打字練習器
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.articleBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.articleDataSet = new 中文文章打字練習器.ArticleDataSet();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.管理者ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.增加題目ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.修改題目ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.輸入完成ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.工具ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.字型大小ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.插入特殊符號ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.textBox11 = new System.Windows.Forms.TextBox();
            this.textBox12 = new System.Windows.Forms.TextBox();
            this.textBox13 = new System.Windows.Forms.TextBox();
            this.textBox14 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.articleTableAdapter = new 中文文章打字練習器.ArticleDataSetTableAdapters.ArticleTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.articleBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.articleDataSet)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // textBox1
            // 
            this.textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.articleBindingSource, "題目內容 - 1", true));
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            // 
            // articleBindingSource
            // 
            this.articleBindingSource.DataMember = "Article";
            this.articleBindingSource.DataSource = this.articleDataSet;
            // 
            // articleDataSet
            // 
            this.articleDataSet.DataSetName = "ArticleDataSet";
            this.articleDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // button3
            // 
            resources.ApplyResources(this.button3, "button3");
            this.button3.Name = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            resources.ApplyResources(this.button4, "button4");
            this.button4.Name = "button4";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.管理者ToolStripMenuItem,
            this.工具ToolStripMenuItem});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // 管理者ToolStripMenuItem
            // 
            this.管理者ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.增加題目ToolStripMenuItem,
            this.修改題目ToolStripMenuItem,
            this.輸入完成ToolStripMenuItem});
            this.管理者ToolStripMenuItem.Name = "管理者ToolStripMenuItem";
            resources.ApplyResources(this.管理者ToolStripMenuItem, "管理者ToolStripMenuItem");
            // 
            // 增加題目ToolStripMenuItem
            // 
            this.增加題目ToolStripMenuItem.Name = "增加題目ToolStripMenuItem";
            resources.ApplyResources(this.增加題目ToolStripMenuItem, "增加題目ToolStripMenuItem");
            this.增加題目ToolStripMenuItem.Click += new System.EventHandler(this.增加題目ToolStripMenuItem_Click);
            // 
            // 修改題目ToolStripMenuItem
            // 
            this.修改題目ToolStripMenuItem.Name = "修改題目ToolStripMenuItem";
            resources.ApplyResources(this.修改題目ToolStripMenuItem, "修改題目ToolStripMenuItem");
            this.修改題目ToolStripMenuItem.Click += new System.EventHandler(this.修改題目ToolStripMenuItem_Click);
            // 
            // 輸入完成ToolStripMenuItem
            // 
            this.輸入完成ToolStripMenuItem.Name = "輸入完成ToolStripMenuItem";
            resources.ApplyResources(this.輸入完成ToolStripMenuItem, "輸入完成ToolStripMenuItem");
            this.輸入完成ToolStripMenuItem.Click += new System.EventHandler(this.輸入完成ToolStripMenuItem_Click);
            // 
            // 工具ToolStripMenuItem
            // 
            this.工具ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.字型大小ToolStripMenuItem,
            this.插入特殊符號ToolStripMenuItem});
            this.工具ToolStripMenuItem.Name = "工具ToolStripMenuItem";
            resources.ApplyResources(this.工具ToolStripMenuItem, "工具ToolStripMenuItem");
            // 
            // 字型大小ToolStripMenuItem
            // 
            this.字型大小ToolStripMenuItem.Name = "字型大小ToolStripMenuItem";
            resources.ApplyResources(this.字型大小ToolStripMenuItem, "字型大小ToolStripMenuItem");
            this.字型大小ToolStripMenuItem.Click += new System.EventHandler(this.字型大小ToolStripMenuItem_Click);
            // 
            // 插入特殊符號ToolStripMenuItem
            // 
            this.插入特殊符號ToolStripMenuItem.Name = "插入特殊符號ToolStripMenuItem";
            resources.ApplyResources(this.插入特殊符號ToolStripMenuItem, "插入特殊符號ToolStripMenuItem");
            this.插入特殊符號ToolStripMenuItem.Click += new System.EventHandler(this.插入特殊符號ToolStripMenuItem_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.articleBindingSource, "是否通過", true));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Name = "label3";
            // 
            // textBox2
            // 
            this.textBox2.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.articleBindingSource, "練習紀錄 - 1", true));
            resources.ApplyResources(this.textBox2, "textBox2");
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.ShortcutsEnabled = false;
            // 
            // comboBox1
            // 
            this.comboBox1.DataSource = this.articleBindingSource;
            this.comboBox1.DisplayMember = "識別碼";
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox1, "comboBox1");
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.TabStop = false;
            this.comboBox1.ValueMember = "識別碼";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.articleBindingSource, "題目描述", true));
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Name = "label4";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // textBox3
            // 
            this.textBox3.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.articleBindingSource, "題目內容 - 2", true));
            resources.ApplyResources(this.textBox3, "textBox3");
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            // 
            // textBox4
            // 
            this.textBox4.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.articleBindingSource, "題目內容 - 3", true));
            resources.ApplyResources(this.textBox4, "textBox4");
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            // 
            // textBox5
            // 
            this.textBox5.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.articleBindingSource, "題目內容 - 4", true));
            resources.ApplyResources(this.textBox5, "textBox5");
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            // 
            // textBox6
            // 
            this.textBox6.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.articleBindingSource, "題目內容 - 5", true));
            resources.ApplyResources(this.textBox6, "textBox6");
            this.textBox6.Name = "textBox6";
            this.textBox6.ReadOnly = true;
            // 
            // textBox7
            // 
            this.textBox7.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.articleBindingSource, "題目內容 - 6", true));
            resources.ApplyResources(this.textBox7, "textBox7");
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            // 
            // textBox8
            // 
            resources.ApplyResources(this.textBox8, "textBox8");
            this.textBox8.Name = "textBox8";
            this.textBox8.ReadOnly = true;
            // 
            // textBox9
            // 
            this.textBox9.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.articleBindingSource, "練習紀錄 - 2", true));
            resources.ApplyResources(this.textBox9, "textBox9");
            this.textBox9.Name = "textBox9";
            this.textBox9.ReadOnly = true;
            this.textBox9.ShortcutsEnabled = false;
            // 
            // textBox10
            // 
            this.textBox10.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.articleBindingSource, "練習紀錄 - 3", true));
            resources.ApplyResources(this.textBox10, "textBox10");
            this.textBox10.Name = "textBox10";
            this.textBox10.ReadOnly = true;
            this.textBox10.ShortcutsEnabled = false;
            // 
            // textBox11
            // 
            this.textBox11.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.articleBindingSource, "練習紀錄 - 4", true));
            resources.ApplyResources(this.textBox11, "textBox11");
            this.textBox11.Name = "textBox11";
            this.textBox11.ReadOnly = true;
            this.textBox11.ShortcutsEnabled = false;
            // 
            // textBox12
            // 
            this.textBox12.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.articleBindingSource, "練習紀錄 - 5", true));
            resources.ApplyResources(this.textBox12, "textBox12");
            this.textBox12.Name = "textBox12";
            this.textBox12.ReadOnly = true;
            this.textBox12.ShortcutsEnabled = false;
            // 
            // textBox13
            // 
            this.textBox13.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.articleBindingSource, "練習紀錄 - 6", true));
            resources.ApplyResources(this.textBox13, "textBox13");
            this.textBox13.Name = "textBox13";
            this.textBox13.ReadOnly = true;
            this.textBox13.ShortcutsEnabled = false;
            // 
            // textBox14
            // 
            resources.ApplyResources(this.textBox14, "textBox14");
            this.textBox14.Name = "textBox14";
            this.textBox14.ShortcutsEnabled = false;
            this.textBox14.TextChanged += new System.EventHandler(this.textBox14_TextChanged);
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.ForeColor = System.Drawing.Color.MediumSeaGreen;
            this.label6.Name = "label6";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label7);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBox8);
            this.groupBox3.Controls.Add(this.textBox7);
            this.groupBox3.Controls.Add(this.textBox6);
            this.groupBox3.Controls.Add(this.textBox5);
            this.groupBox3.Controls.Add(this.textBox1);
            this.groupBox3.Controls.Add(this.textBox3);
            this.groupBox3.Controls.Add(this.textBox4);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.textBox14);
            this.groupBox4.Controls.Add(this.textBox13);
            this.groupBox4.Controls.Add(this.textBox12);
            this.groupBox4.Controls.Add(this.textBox11);
            this.groupBox4.Controls.Add(this.textBox9);
            this.groupBox4.Controls.Add(this.textBox10);
            this.groupBox4.Controls.Add(this.textBox2);
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // articleTableAdapter
            // 
            this.articleTableAdapter.ClearBeforeFill = true;
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.articleBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.articleDataSet)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 管理者ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 增加題目ToolStripMenuItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.ToolStripMenuItem 修改題目ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 輸入完成ToolStripMenuItem;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.TextBox textBox11;
        private System.Windows.Forms.TextBox textBox12;
        private System.Windows.Forms.TextBox textBox13;
        private System.Windows.Forms.TextBox textBox14;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ToolStripMenuItem 工具ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 字型大小ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 插入特殊符號ToolStripMenuItem;
        private System.Windows.Forms.FontDialog fontDialog1;
        private ArticleDataSet articleDataSet;
        private System.Windows.Forms.BindingSource articleBindingSource;
        private ArticleDataSetTableAdapters.ArticleTableAdapter articleTableAdapter;
    }
}

