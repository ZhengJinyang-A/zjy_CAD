namespace zjy_CAD
{
    partial class UserControl_batch_EditName
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
            this.btn_file_Path = new System.Windows.Forms.Button();
            this.tbx_SavePath = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_changeName_all = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btn_changeName_wordexcel = new System.Windows.Forms.Button();
            this.btn_changeName_dwg = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tbx_old_projectName = new System.Windows.Forms.TextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbx_new_projectName = new System.Windows.Forms.TextBox();
            this.btn_open_dwg = new System.Windows.Forms.Button();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_fuzhu
            // 
            this.lbl_fuzhu.BackColor = System.Drawing.Color.White;
            this.lbl_fuzhu.CausesValidation = false;
            this.lbl_fuzhu.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_fuzhu.Location = new System.Drawing.Point(21, 9);
            this.lbl_fuzhu.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_fuzhu.Name = "lbl_fuzhu";
            this.lbl_fuzhu.Size = new System.Drawing.Size(204, 28);
            this.lbl_fuzhu.TabIndex = 11;
            this.lbl_fuzhu.Text = " 项目名称修改";
            this.lbl_fuzhu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 694);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 16;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // btn_changeName_all
            // 
            this.btn_changeName_all.Enabled = false;
            this.btn_changeName_all.Location = new System.Drawing.Point(6, 76);
            this.btn_changeName_all.Name = "btn_changeName_all";
            this.btn_changeName_all.Size = new System.Drawing.Size(61, 33);
            this.btn_changeName_all.TabIndex = 17;
            this.btn_changeName_all.Text = "修改全部";
            this.btn_changeName_all.UseVisualStyleBackColor = true;
            this.btn_changeName_all.Click += new System.EventHandler(this.btn_changeName_all_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.groupBox4.Controls.Add(this.btn_changeName_wordexcel);
            this.groupBox4.Controls.Add(this.btn_changeName_dwg);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.tbx_old_projectName);
            this.groupBox4.Controls.Add(this.btn_changeName_all);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.tbx_new_projectName);
            this.groupBox4.Location = new System.Drawing.Point(10, 96);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(222, 124);
            this.groupBox4.TabIndex = 37;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "项目名称修改";
            // 
            // btn_changeName_wordexcel
            // 
            this.btn_changeName_wordexcel.Location = new System.Drawing.Point(146, 76);
            this.btn_changeName_wordexcel.Name = "btn_changeName_wordexcel";
            this.btn_changeName_wordexcel.Size = new System.Drawing.Size(74, 33);
            this.btn_changeName_wordexcel.TabIndex = 24;
            this.btn_changeName_wordexcel.Text = "修改word excel";
            this.btn_changeName_wordexcel.UseVisualStyleBackColor = true;
            this.btn_changeName_wordexcel.Click += new System.EventHandler(this.btn_changeName_wordexcel_Click);
            // 
            // btn_changeName_dwg
            // 
            this.btn_changeName_dwg.Location = new System.Drawing.Point(74, 76);
            this.btn_changeName_dwg.Name = "btn_changeName_dwg";
            this.btn_changeName_dwg.Size = new System.Drawing.Size(66, 33);
            this.btn_changeName_dwg.TabIndex = 23;
            this.btn_changeName_dwg.Text = "修改dwg";
            this.btn_changeName_dwg.UseVisualStyleBackColor = true;
            this.btn_changeName_dwg.Click += new System.EventHandler(this.btn_changeName_dwg_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 19;
            this.label3.Text = "旧项目名称:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbx_old_projectName
            // 
            this.tbx_old_projectName.Location = new System.Drawing.Point(82, 20);
            this.tbx_old_projectName.Name = "tbx_old_projectName";
            this.tbx_old_projectName.Size = new System.Drawing.Size(134, 21);
            this.tbx_old_projectName.TabIndex = 18;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(17, 326);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(216, 60);
            this.richTextBox1.TabIndex = 22;
            this.richTextBox1.Text = "说明:对于cad直接修改，非替换。对于word，excel则采用替换的原则，需输旧项目名称。";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.DarkRed;
            this.label4.Location = new System.Drawing.Point(2, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 12);
            this.label4.TabIndex = 21;
            this.label4.Text = "新项目名称:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbx_new_projectName
            // 
            this.tbx_new_projectName.BackColor = System.Drawing.Color.White;
            this.tbx_new_projectName.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbx_new_projectName.Location = new System.Drawing.Point(82, 47);
            this.tbx_new_projectName.Name = "tbx_new_projectName";
            this.tbx_new_projectName.Size = new System.Drawing.Size(133, 21);
            this.tbx_new_projectName.TabIndex = 20;
            this.tbx_new_projectName.Tag = "";
            // 
            // btn_open_dwg
            // 
            this.btn_open_dwg.Location = new System.Drawing.Point(39, 270);
            this.btn_open_dwg.Name = "btn_open_dwg";
            this.btn_open_dwg.Size = new System.Drawing.Size(149, 32);
            this.btn_open_dwg.TabIndex = 25;
            this.btn_open_dwg.Text = "修改当前打开的dwg";
            this.btn_open_dwg.UseVisualStyleBackColor = true;
            this.btn_open_dwg.Click += new System.EventHandler(this.btn_open_dwg_Click);
            // 
            // UserControl_batch_EditName
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.btn_open_dwg);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lbl_fuzhu);
            this.Controls.Add(this.btn_file_Path);
            this.Controls.Add(this.tbx_SavePath);
            this.Name = "UserControl_batch_EditName";
            this.Size = new System.Drawing.Size(246, 395);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_fuzhu;
        private System.Windows.Forms.Button btn_file_Path;
        private System.Windows.Forms.TextBox tbx_SavePath;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btn_changeName_all;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbx_new_projectName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbx_old_projectName;
        private System.Windows.Forms.Button btn_changeName_wordexcel;
        private System.Windows.Forms.Button btn_changeName_dwg;
        private System.Windows.Forms.Button btn_open_dwg;
    }
}
