namespace zjy_CAD
{
    partial class UserControl_batch_Plot
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lbl_fuzhu = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.rdbtn_90 = new System.Windows.Forms.RadioButton();
            this.rdbtn_00 = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.cbx_plot_device = new System.Windows.Forms.ComboBox();
            this.btn_layout_pingmian_plot = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_model_plot = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btn_print_excel_word = new System.Windows.Forms.Button();
            this.rdb_excel_endpage_auto = new System.Windows.Forms.RadioButton();
            this.rdb_excel_endpage_1 = new System.Windows.Forms.RadioButton();
            this.btn_file_cad_excel_word_plot = new System.Windows.Forms.Button();
            this.btn_file_cad_plot = new System.Windows.Forms.Button();
            this.btn_file_Path = new System.Windows.Forms.Button();
            this.tbx_SavePath = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_fuzhu
            // 
            this.lbl_fuzhu.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbl_fuzhu.CausesValidation = false;
            this.lbl_fuzhu.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_fuzhu.Location = new System.Drawing.Point(21, 9);
            this.lbl_fuzhu.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_fuzhu.Name = "lbl_fuzhu";
            this.lbl_fuzhu.Size = new System.Drawing.Size(204, 28);
            this.lbl_fuzhu.TabIndex = 11;
            this.lbl_fuzhu.Text = "批量打印 ";
            this.lbl_fuzhu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.rdbtn_90);
            this.groupBox1.Controls.Add(this.rdbtn_00);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbx_plot_device);
            this.groupBox1.Location = new System.Drawing.Point(21, 86);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(222, 80);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "公共打印参数设置";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "打印机纸方向";
            this.label2.Visible = false;
            // 
            // rdbtn_90
            // 
            this.rdbtn_90.AutoSize = true;
            this.rdbtn_90.Checked = true;
            this.rdbtn_90.Location = new System.Drawing.Point(153, 55);
            this.rdbtn_90.Name = "rdbtn_90";
            this.rdbtn_90.Size = new System.Drawing.Size(47, 16);
            this.rdbtn_90.TabIndex = 3;
            this.rdbtn_90.TabStop = true;
            this.rdbtn_90.Text = "纵向";
            this.rdbtn_90.UseVisualStyleBackColor = true;
            this.rdbtn_90.Visible = false;
            // 
            // rdbtn_00
            // 
            this.rdbtn_00.AutoSize = true;
            this.rdbtn_00.Location = new System.Drawing.Point(93, 55);
            this.rdbtn_00.Name = "rdbtn_00";
            this.rdbtn_00.Size = new System.Drawing.Size(47, 16);
            this.rdbtn_00.TabIndex = 2;
            this.rdbtn_00.TabStop = true;
            this.rdbtn_00.Text = "横向";
            this.rdbtn_00.UseVisualStyleBackColor = true;
            this.rdbtn_00.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "打印机选择";
            // 
            // cbx_plot_device
            // 
            this.cbx_plot_device.FormattingEnabled = true;
            this.cbx_plot_device.Items.AddRange(new object[] {
            "DocuCentre-V 3060",
            "pdfFactory Pro",
            "DocuCentre-V C2265"});
            this.cbx_plot_device.Location = new System.Drawing.Point(82, 20);
            this.cbx_plot_device.Name = "cbx_plot_device";
            this.cbx_plot_device.Size = new System.Drawing.Size(134, 20);
            this.cbx_plot_device.TabIndex = 0;
            this.cbx_plot_device.Text = "pdfFactory Pro";
            // 
            // btn_layout_pingmian_plot
            // 
            this.btn_layout_pingmian_plot.Location = new System.Drawing.Point(37, 11);
            this.btn_layout_pingmian_plot.Name = "btn_layout_pingmian_plot";
            this.btn_layout_pingmian_plot.Size = new System.Drawing.Size(157, 42);
            this.btn_layout_pingmian_plot.TabIndex = 13;
            this.btn_layout_pingmian_plot.Text = "布局空间平面图打印";
            this.btn_layout_pingmian_plot.UseVisualStyleBackColor = true;
            this.btn_layout_pingmian_plot.Click += new System.EventHandler(this.btn_layout_pingmian_plot_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.groupBox2.Controls.Add(this.btn_model_plot);
            this.groupBox2.Controls.Add(this.btn_layout_pingmian_plot);
            this.groupBox2.Location = new System.Drawing.Point(21, 175);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(222, 100);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "单cad打印";
            // 
            // btn_model_plot
            // 
            this.btn_model_plot.Location = new System.Drawing.Point(37, 55);
            this.btn_model_plot.Name = "btn_model_plot";
            this.btn_model_plot.Size = new System.Drawing.Size(157, 42);
            this.btn_model_plot.TabIndex = 14;
            this.btn_model_plot.Text = "模型空间图纸打印";
            this.btn_model_plot.UseVisualStyleBackColor = true;
            this.btn_model_plot.Click += new System.EventHandler(this.btn_model_plot_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.groupBox3.Controls.Add(this.btn_print_excel_word);
            this.groupBox3.Controls.Add(this.rdb_excel_endpage_auto);
            this.groupBox3.Controls.Add(this.rdb_excel_endpage_1);
            this.groupBox3.Controls.Add(this.btn_file_cad_excel_word_plot);
            this.groupBox3.Controls.Add(this.btn_file_cad_plot);
            this.groupBox3.Location = new System.Drawing.Point(21, 280);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(222, 184);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "文件夹cad打印";
            // 
            // btn_print_excel_word
            // 
            this.btn_print_excel_word.Location = new System.Drawing.Point(36, 48);
            this.btn_print_excel_word.Name = "btn_print_excel_word";
            this.btn_print_excel_word.Size = new System.Drawing.Size(157, 37);
            this.btn_print_excel_word.TabIndex = 40;
            this.btn_print_excel_word.Text = "打印 word excel文件";
            this.btn_print_excel_word.UseVisualStyleBackColor = true;
            this.btn_print_excel_word.Click += new System.EventHandler(this.btn_print_excel_word_Click);
            // 
            // rdb_excel_endpage_auto
            // 
            this.rdb_excel_endpage_auto.AutoSize = true;
            this.rdb_excel_endpage_auto.Location = new System.Drawing.Point(17, 156);
            this.rdb_excel_endpage_auto.Name = "rdb_excel_endpage_auto";
            this.rdb_excel_endpage_auto.Size = new System.Drawing.Size(191, 16);
            this.rdb_excel_endpage_auto.TabIndex = 39;
            this.rdb_excel_endpage_auto.Text = "Excel每个sheet按自动页数打印";
            this.rdb_excel_endpage_auto.UseVisualStyleBackColor = true;
            // 
            // rdb_excel_endpage_1
            // 
            this.rdb_excel_endpage_1.AutoSize = true;
            this.rdb_excel_endpage_1.Checked = true;
            this.rdb_excel_endpage_1.Location = new System.Drawing.Point(16, 134);
            this.rdb_excel_endpage_1.Name = "rdb_excel_endpage_1";
            this.rdb_excel_endpage_1.Size = new System.Drawing.Size(167, 16);
            this.rdb_excel_endpage_1.TabIndex = 38;
            this.rdb_excel_endpage_1.TabStop = true;
            this.rdb_excel_endpage_1.Text = "Excel每个sheet只打印一页";
            this.rdb_excel_endpage_1.UseVisualStyleBackColor = true;
            // 
            // btn_file_cad_excel_word_plot
            // 
            this.btn_file_cad_excel_word_plot.Location = new System.Drawing.Point(37, 84);
            this.btn_file_cad_excel_word_plot.Name = "btn_file_cad_excel_word_plot";
            this.btn_file_cad_excel_word_plot.Size = new System.Drawing.Size(157, 42);
            this.btn_file_cad_excel_word_plot.TabIndex = 37;
            this.btn_file_cad_excel_word_plot.Text = "打印dwg,word,excel文件";
            this.btn_file_cad_excel_word_plot.UseVisualStyleBackColor = true;
            this.btn_file_cad_excel_word_plot.Click += new System.EventHandler(this.btn_file_cad_excel_word_plot_Click);
            // 
            // btn_file_cad_plot
            // 
            this.btn_file_cad_plot.Location = new System.Drawing.Point(37, 15);
            this.btn_file_cad_plot.Name = "btn_file_cad_plot";
            this.btn_file_cad_plot.Size = new System.Drawing.Size(157, 35);
            this.btn_file_cad_plot.TabIndex = 13;
            this.btn_file_cad_plot.Text = "打印dwg文件";
            this.btn_file_cad_plot.UseVisualStyleBackColor = true;
            this.btn_file_cad_plot.Click += new System.EventHandler(this.btn_file_cad_plot_Click);
            // 
            // btn_file_Path
            // 
            this.btn_file_Path.Location = new System.Drawing.Point(168, 55);
            this.btn_file_Path.Name = "btn_file_Path";
            this.btn_file_Path.Size = new System.Drawing.Size(63, 23);
            this.btn_file_Path.TabIndex = 36;
            this.btn_file_Path.Text = "路径选择";
            this.btn_file_Path.UseVisualStyleBackColor = true;
            this.btn_file_Path.Click += new System.EventHandler(this.btn_file_Path_Click);
            // 
            // tbx_SavePath
            // 
            this.tbx_SavePath.Location = new System.Drawing.Point(25, 57);
            this.tbx_SavePath.Name = "tbx_SavePath";
            this.tbx_SavePath.Size = new System.Drawing.Size(119, 21);
            this.tbx_SavePath.TabIndex = 35;
            this.tbx_SavePath.Text = "D:\\zjy";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Enabled = false;
            this.richTextBox1.Location = new System.Drawing.Point(15, 482);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(216, 60);
            this.richTextBox1.TabIndex = 37;
            this.richTextBox1.Text = "说明:对于平面图和平纵缩略图直接从布局打印，其他图纸从模型空间打印";
            // 
            // UserControl_batch_Plot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lbl_fuzhu);
            this.Controls.Add(this.btn_file_Path);
            this.Controls.Add(this.tbx_SavePath);
            this.Name = "UserControl_batch_Plot";
            this.Size = new System.Drawing.Size(248, 727);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_fuzhu;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbx_plot_device;
        private System.Windows.Forms.RadioButton rdbtn_90;
        private System.Windows.Forms.RadioButton rdbtn_00;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_layout_pingmian_plot;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_model_plot;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btn_file_cad_plot;
        private System.Windows.Forms.Button btn_file_Path;
        private System.Windows.Forms.TextBox tbx_SavePath;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btn_file_cad_excel_word_plot;
        private System.Windows.Forms.RadioButton rdb_excel_endpage_auto;
        private System.Windows.Forms.RadioButton rdb_excel_endpage_1;
        private System.Windows.Forms.Button btn_print_excel_word;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}
