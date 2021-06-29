namespace zjy_CAD
{
    partial class zjy_SHowWindow
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbx_CanKaoBL = new System.Windows.Forms.TextBox();
            this.tbx_CentrelBL = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btn_pianchajisuan = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.tbx_TargetBL = new System.Windows.Forms.TextBox();
            this.tbx_SourceBL = new System.Windows.Forms.TextBox();
            this.lb_sourceBL = new System.Windows.Forms.Label();
            this.tbx_NorthDelta = new System.Windows.Forms.TextBox();
            this.tbx_EastDelta = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cbx_Is80 = new System.Windows.Forms.CheckBox();
            this.btn_KMLRead = new System.Windows.Forms.Button();
            this.lb_KMLPath = new System.Windows.Forms.Label();
            this.tbx_KMLPath = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btn_outputCAD = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbx_KMLfileName = new System.Windows.Forms.TextBox();
            this.tbx_minLineSegmentLength = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_CADtoKML = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_KMLPath = new System.Windows.Forms.Button();
            this.tbx_SaveKMLPath = new System.Windows.Forms.TextBox();
            this.lbl_fuzhu = new System.Windows.Forms.Label();
            this.btn_googlePath_import = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btn_googlePath_selectPath = new System.Windows.Forms.Button();
            this.tbx_googlePath = new System.Windows.Forms.TextBox();
            this.btn_GoogleRegions = new System.Windows.Forms.Button();
            this.rtbx_googleregion = new System.Windows.Forms.RichTextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.tbx_CentrelBL);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(11, 40);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(215, 88);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "中央经纬度";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.tbx_CanKaoBL);
            this.groupBox2.Location = new System.Drawing.Point(14, 41);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(184, 40);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "辅助";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "参考经度";
            // 
            // tbx_CanKaoBL
            // 
            this.tbx_CanKaoBL.Location = new System.Drawing.Point(101, 14);
            this.tbx_CanKaoBL.Name = "tbx_CanKaoBL";
            this.tbx_CanKaoBL.Size = new System.Drawing.Size(65, 21);
            this.tbx_CanKaoBL.TabIndex = 6;
            this.tbx_CanKaoBL.Text = "112.5";
            this.tbx_CanKaoBL.TextChanged += new System.EventHandler(this.tbx_CanKaoBL_TextChanged);
            // 
            // tbx_CentrelBL
            // 
            this.tbx_CentrelBL.Location = new System.Drawing.Point(111, 14);
            this.tbx_CentrelBL.Name = "tbx_CentrelBL";
            this.tbx_CentrelBL.Size = new System.Drawing.Size(65, 21);
            this.tbx_CentrelBL.TabIndex = 4;
            this.tbx_CentrelBL.Text = "111";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "中央子午线经度";
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.groupBox4.Controls.Add(this.groupBox5);
            this.groupBox4.Controls.Add(this.tbx_NorthDelta);
            this.groupBox4.Controls.Add(this.tbx_EastDelta);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.cbx_Is80);
            this.groupBox4.Location = new System.Drawing.Point(14, 198);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(184, 196);
            this.groupBox4.TabIndex = 19;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "80坐标系(单位：米)";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btn_pianchajisuan);
            this.groupBox5.Controls.Add(this.label11);
            this.groupBox5.Controls.Add(this.tbx_TargetBL);
            this.groupBox5.Controls.Add(this.tbx_SourceBL);
            this.groupBox5.Controls.Add(this.lb_sourceBL);
            this.groupBox5.Location = new System.Drawing.Point(6, 96);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(170, 97);
            this.groupBox5.TabIndex = 18;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "辅助计算平移偏差";
            // 
            // btn_pianchajisuan
            // 
            this.btn_pianchajisuan.Location = new System.Drawing.Point(12, 20);
            this.btn_pianchajisuan.Name = "btn_pianchajisuan";
            this.btn_pianchajisuan.Size = new System.Drawing.Size(87, 23);
            this.btn_pianchajisuan.TabIndex = 25;
            this.btn_pianchajisuan.Text = "偏差计算";
            this.btn_pianchajisuan.UseVisualStyleBackColor = true;
            this.btn_pianchajisuan.Click += new System.EventHandler(this.btn_pianchajisuan_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 52);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 12);
            this.label11.TabIndex = 18;
            this.label11.Text = "目标经纬度";
            // 
            // tbx_TargetBL
            // 
            this.tbx_TargetBL.Location = new System.Drawing.Point(72, 49);
            this.tbx_TargetBL.Name = "tbx_TargetBL";
            this.tbx_TargetBL.Size = new System.Drawing.Size(88, 21);
            this.tbx_TargetBL.TabIndex = 19;
            this.tbx_TargetBL.Text = "110,35";
            // 
            // tbx_SourceBL
            // 
            this.tbx_SourceBL.Location = new System.Drawing.Point(72, 71);
            this.tbx_SourceBL.Name = "tbx_SourceBL";
            this.tbx_SourceBL.Size = new System.Drawing.Size(88, 21);
            this.tbx_SourceBL.TabIndex = 21;
            this.tbx_SourceBL.Text = "110,36";
            // 
            // lb_sourceBL
            // 
            this.lb_sourceBL.AutoSize = true;
            this.lb_sourceBL.Location = new System.Drawing.Point(6, 74);
            this.lb_sourceBL.Name = "lb_sourceBL";
            this.lb_sourceBL.Size = new System.Drawing.Size(65, 12);
            this.lb_sourceBL.TabIndex = 20;
            this.lb_sourceBL.Text = "现在经纬度";
            // 
            // tbx_NorthDelta
            // 
            this.tbx_NorthDelta.Location = new System.Drawing.Point(106, 23);
            this.tbx_NorthDelta.Name = "tbx_NorthDelta";
            this.tbx_NorthDelta.Size = new System.Drawing.Size(60, 21);
            this.tbx_NorthDelta.TabIndex = 7;
            this.tbx_NorthDelta.Text = "10";
            // 
            // tbx_EastDelta
            // 
            this.tbx_EastDelta.Location = new System.Drawing.Point(106, 50);
            this.tbx_EastDelta.Name = "tbx_EastDelta";
            this.tbx_EastDelta.Size = new System.Drawing.Size(60, 21);
            this.tbx_EastDelta.TabIndex = 17;
            this.tbx_EastDelta.Text = "110";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(28, 26);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 6;
            this.label7.Text = "正北偏差";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(28, 53);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 16;
            this.label8.Text = "正东偏差";
            // 
            // cbx_Is80
            // 
            this.cbx_Is80.AutoSize = true;
            this.cbx_Is80.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbx_Is80.Location = new System.Drawing.Point(20, 74);
            this.cbx_Is80.Name = "cbx_Is80";
            this.cbx_Is80.Size = new System.Drawing.Size(72, 16);
            this.cbx_Is80.TabIndex = 2;
            this.cbx_Is80.Text = "80坐标系";
            this.cbx_Is80.UseVisualStyleBackColor = true;
            // 
            // btn_KMLRead
            // 
            this.btn_KMLRead.Location = new System.Drawing.Point(8, 60);
            this.btn_KMLRead.Name = "btn_KMLRead";
            this.btn_KMLRead.Size = new System.Drawing.Size(105, 23);
            this.btn_KMLRead.TabIndex = 22;
            this.btn_KMLRead.Text = "选取文件夹路径";
            this.btn_KMLRead.UseVisualStyleBackColor = true;
            this.btn_KMLRead.Click += new System.EventHandler(this.btn_KMLRead_Click);
            // 
            // lb_KMLPath
            // 
            this.lb_KMLPath.AutoSize = true;
            this.lb_KMLPath.Location = new System.Drawing.Point(6, 17);
            this.lb_KMLPath.Name = "lb_KMLPath";
            this.lb_KMLPath.Size = new System.Drawing.Size(83, 12);
            this.lb_KMLPath.TabIndex = 20;
            this.lb_KMLPath.Text = "KML所在路径：";
            // 
            // tbx_KMLPath
            // 
            this.tbx_KMLPath.Location = new System.Drawing.Point(8, 33);
            this.tbx_KMLPath.Name = "tbx_KMLPath";
            this.tbx_KMLPath.Size = new System.Drawing.Size(172, 21);
            this.tbx_KMLPath.TabIndex = 21;
            this.tbx_KMLPath.Text = "D:\\mapdownload";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.groupBox3.Controls.Add(this.btn_outputCAD);
            this.groupBox3.Controls.Add(this.lb_KMLPath);
            this.groupBox3.Controls.Add(this.btn_KMLRead);
            this.groupBox3.Controls.Add(this.tbx_KMLPath);
            this.groupBox3.Location = new System.Drawing.Point(11, 134);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(215, 121);
            this.groupBox3.TabIndex = 23;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "KML_to_CAD";
            // 
            // btn_outputCAD
            // 
            this.btn_outputCAD.Location = new System.Drawing.Point(42, 85);
            this.btn_outputCAD.Name = "btn_outputCAD";
            this.btn_outputCAD.Size = new System.Drawing.Size(122, 31);
            this.btn_outputCAD.TabIndex = 23;
            this.btn_outputCAD.Text = "导入CAD";
            this.btn_outputCAD.UseVisualStyleBackColor = true;
            this.btn_outputCAD.Click += new System.EventHandler(this.btn_outputCAD_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // groupBox6
            // 
            this.groupBox6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.groupBox6.Controls.Add(this.label5);
            this.groupBox6.Controls.Add(this.tbx_KMLfileName);
            this.groupBox6.Controls.Add(this.tbx_minLineSegmentLength);
            this.groupBox6.Controls.Add(this.groupBox4);
            this.groupBox6.Controls.Add(this.label4);
            this.groupBox6.Controls.Add(this.btn_CADtoKML);
            this.groupBox6.Controls.Add(this.label1);
            this.groupBox6.Controls.Add(this.btn_KMLPath);
            this.groupBox6.Controls.Add(this.tbx_SaveKMLPath);
            this.groupBox6.Location = new System.Drawing.Point(11, 271);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(215, 400);
            this.groupBox6.TabIndex = 24;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "CAD_to_KML";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 113);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 12);
            this.label5.TabIndex = 24;
            this.label5.Text = "KML文件名称：";
            // 
            // tbx_KMLfileName
            // 
            this.tbx_KMLfileName.Location = new System.Drawing.Point(7, 128);
            this.tbx_KMLfileName.Name = "tbx_KMLfileName";
            this.tbx_KMLfileName.Size = new System.Drawing.Size(172, 21);
            this.tbx_KMLfileName.TabIndex = 25;
            this.tbx_KMLfileName.Text = "zjy.kml";
            // 
            // tbx_minLineSegmentLength
            // 
            this.tbx_minLineSegmentLength.Location = new System.Drawing.Point(83, 20);
            this.tbx_minLineSegmentLength.Name = "tbx_minLineSegmentLength";
            this.tbx_minLineSegmentLength.Size = new System.Drawing.Size(70, 21);
            this.tbx_minLineSegmentLength.TabIndex = 20;
            this.tbx_minLineSegmentLength.Text = "5";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 19;
            this.label4.Text = "线段精度";
            // 
            // btn_CADtoKML
            // 
            this.btn_CADtoKML.Location = new System.Drawing.Point(8, 155);
            this.btn_CADtoKML.Name = "btn_CADtoKML";
            this.btn_CADtoKML.Size = new System.Drawing.Size(122, 35);
            this.btn_CADtoKML.TabIndex = 23;
            this.btn_CADtoKML.Text = "导出KML";
            this.btn_CADtoKML.UseVisualStyleBackColor = true;
            this.btn_CADtoKML.Click += new System.EventHandler(this.btn_CADtoKML_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 20;
            this.label1.Text = "KML所在路径：";
            // 
            // btn_KMLPath
            // 
            this.btn_KMLPath.Location = new System.Drawing.Point(0, 87);
            this.btn_KMLPath.Name = "btn_KMLPath";
            this.btn_KMLPath.Size = new System.Drawing.Size(118, 23);
            this.btn_KMLPath.TabIndex = 22;
            this.btn_KMLPath.Text = "选取KML保存文件路径";
            this.btn_KMLPath.UseVisualStyleBackColor = true;
            this.btn_KMLPath.Click += new System.EventHandler(this.btn_KMLPath_Click);
            // 
            // tbx_SaveKMLPath
            // 
            this.tbx_SaveKMLPath.Location = new System.Drawing.Point(7, 60);
            this.tbx_SaveKMLPath.Name = "tbx_SaveKMLPath";
            this.tbx_SaveKMLPath.Size = new System.Drawing.Size(173, 21);
            this.tbx_SaveKMLPath.TabIndex = 21;
            this.tbx_SaveKMLPath.Text = "D:\\zjy";
            // 
            // lbl_fuzhu
            // 
            this.lbl_fuzhu.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbl_fuzhu.CausesValidation = false;
            this.lbl_fuzhu.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_fuzhu.Location = new System.Drawing.Point(24, 9);
            this.lbl_fuzhu.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_fuzhu.Name = "lbl_fuzhu";
            this.lbl_fuzhu.Size = new System.Drawing.Size(167, 28);
            this.lbl_fuzhu.TabIndex = 25;
            this.lbl_fuzhu.Text = "CAD与KML的互导";
            this.lbl_fuzhu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_googlePath_import
            // 
            this.btn_googlePath_import.Location = new System.Drawing.Point(60, 85);
            this.btn_googlePath_import.Name = "btn_googlePath_import";
            this.btn_googlePath_import.Size = new System.Drawing.Size(95, 30);
            this.btn_googlePath_import.TabIndex = 26;
            this.btn_googlePath_import.Text = "导入CAD";
            this.btn_googlePath_import.UseVisualStyleBackColor = true;
            this.btn_googlePath_import.Click += new System.EventHandler(this.btn_googlePath_import_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.groupBox7.Controls.Add(this.label6);
            this.groupBox7.Controls.Add(this.btn_googlePath_selectPath);
            this.groupBox7.Controls.Add(this.btn_googlePath_import);
            this.groupBox7.Controls.Add(this.tbx_googlePath);
            this.groupBox7.Location = new System.Drawing.Point(258, 134);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(215, 121);
            this.groupBox7.TabIndex = 24;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Google地图导入";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 20;
            this.label6.Text = "地图路径：";
            // 
            // btn_googlePath_selectPath
            // 
            this.btn_googlePath_selectPath.Location = new System.Drawing.Point(8, 60);
            this.btn_googlePath_selectPath.Name = "btn_googlePath_selectPath";
            this.btn_googlePath_selectPath.Size = new System.Drawing.Size(105, 23);
            this.btn_googlePath_selectPath.TabIndex = 22;
            this.btn_googlePath_selectPath.Text = "选取文件夹路径";
            this.btn_googlePath_selectPath.UseVisualStyleBackColor = true;
            this.btn_googlePath_selectPath.Click += new System.EventHandler(this.btn_googlePath_selectPath_Click);
            // 
            // tbx_googlePath
            // 
            this.tbx_googlePath.Location = new System.Drawing.Point(8, 33);
            this.tbx_googlePath.Name = "tbx_googlePath";
            this.tbx_googlePath.Size = new System.Drawing.Size(172, 21);
            this.tbx_googlePath.TabIndex = 21;
            this.tbx_googlePath.Text = "D:\\mapdownload";
            // 
            // btn_GoogleRegions
            // 
            this.btn_GoogleRegions.Location = new System.Drawing.Point(266, 271);
            this.btn_GoogleRegions.Name = "btn_GoogleRegions";
            this.btn_GoogleRegions.Size = new System.Drawing.Size(158, 65);
            this.btn_GoogleRegions.TabIndex = 26;
            this.btn_GoogleRegions.Text = "生成Google地图区域";
            this.btn_GoogleRegions.UseVisualStyleBackColor = true;
            this.btn_GoogleRegions.Click += new System.EventHandler(this.btn_GoogleRegions_Click);
            // 
            // rtbx_googleregion
            // 
            this.rtbx_googleregion.Location = new System.Drawing.Point(492, 58);
            this.rtbx_googleregion.Name = "rtbx_googleregion";
            this.rtbx_googleregion.Size = new System.Drawing.Size(351, 593);
            this.rtbx_googleregion.TabIndex = 27;
            this.rtbx_googleregion.Text = "";
            // 
            // zjy_SHowWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rtbx_googleregion);
            this.Controls.Add(this.btn_GoogleRegions);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.lbl_fuzhu);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Name = "zjy_SHowWindow";
            this.Size = new System.Drawing.Size(890, 683);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbx_CanKaoBL;
        private System.Windows.Forms.TextBox tbx_CentrelBL;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btn_pianchajisuan;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbx_TargetBL;
        private System.Windows.Forms.TextBox tbx_SourceBL;
        private System.Windows.Forms.Label lb_sourceBL;
        private System.Windows.Forms.TextBox tbx_NorthDelta;
        private System.Windows.Forms.TextBox tbx_EastDelta;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox cbx_Is80;
        private System.Windows.Forms.Button btn_KMLRead;
        private System.Windows.Forms.Label lb_KMLPath;
        private System.Windows.Forms.TextBox tbx_KMLPath;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btn_outputCAD;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button btn_CADtoKML;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_KMLPath;
        private System.Windows.Forms.TextBox tbx_SaveKMLPath;
        private System.Windows.Forms.TextBox tbx_minLineSegmentLength;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbx_KMLfileName;
        private System.Windows.Forms.Label lbl_fuzhu;
        private System.Windows.Forms.Button btn_googlePath_import;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btn_googlePath_selectPath;
        private System.Windows.Forms.TextBox tbx_googlePath;
        private System.Windows.Forms.Button btn_GoogleRegions;
        private System.Windows.Forms.RichTextBox rtbx_googleregion;
    }
}
