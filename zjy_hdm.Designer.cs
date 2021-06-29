namespace zjy_CAD
{
    partial class zjy_hdm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(zjy_hdm));
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.cbx_DL = new System.Windows.Forms.CheckBox();
            this.rtbx_DL = new System.Windows.Forms.RichTextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tbx_LuxianDirection_delta = new System.Windows.Forms.TextBox();
            this.tbx_HDM_caiji_limit = new System.Windows.Forms.TextBox();
            this.lb_sourceBL = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbx_startstack = new System.Windows.Forms.TextBox();
            this.label_startstack = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbx_fileName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_KMLPath = new System.Windows.Forms.Button();
            this.tbx_SavePath = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_Create_stackSequence = new System.Windows.Forms.Button();
            this.tbx_Road_Length = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbx_SamplingJianJU = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.rtbx_stack_sequence = new System.Windows.Forms.RichTextBox();
            this.btn_adjust_road_start = new System.Windows.Forms.Button();
            this.btn_CreateHDM_Pline3D = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_Create_dmx_hdm_file = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tbx_pline3d_xdata_showstack = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbx_pline3d_xdata_stack = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_change_pline3D_stack_xdata = new System.Windows.Forms.Button();
            this.btn_Show_all_pline3d_xdata_stack = new System.Windows.Forms.Button();
            this.btn_showstack_ed = new System.Windows.Forms.Button();
            this.btn_delete_all_pline3d_xdata_stack = new System.Windows.Forms.Button();
            this.btn_CheckGCD = new System.Windows.Forms.Button();
            this.btn_checkAndmodify = new System.Windows.Forms.Button();
            this.lbl_fuzhu = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.tbx_hdm_test_attitude_limit = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btn_test_hdm_attitude = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbx_DL
            // 
            this.cbx_DL.AutoSize = true;
            this.cbx_DL.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbx_DL.Location = new System.Drawing.Point(10, 85);
            this.cbx_DL.Name = "cbx_DL";
            this.cbx_DL.Size = new System.Drawing.Size(48, 16);
            this.cbx_DL.TabIndex = 26;
            this.cbx_DL.Text = "断链";
            this.cbx_DL.UseVisualStyleBackColor = true;
            this.cbx_DL.CheckedChanged += new System.EventHandler(this.cbx_DL_CheckedChanged);
            // 
            // rtbx_DL
            // 
            this.rtbx_DL.Enabled = false;
            this.rtbx_DL.Location = new System.Drawing.Point(8, 107);
            this.rtbx_DL.Name = "rtbx_DL";
            this.rtbx_DL.Size = new System.Drawing.Size(222, 63);
            this.rtbx_DL.TabIndex = 27;
            this.rtbx_DL.Text = "401断链1 = 500.000000 = 800.000000\n402断链2 = 1000.000000 = 300.000000\n403断链3 = 500.0" +
    "00000 = 800.000000";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(4, 23);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(82, 11);
            this.label11.TabIndex = 28;
            this.label11.Text = "路线方向偏差限";
            // 
            // tbx_LuxianDirection_delta
            // 
            this.tbx_LuxianDirection_delta.Location = new System.Drawing.Point(126, 20);
            this.tbx_LuxianDirection_delta.Name = "tbx_LuxianDirection_delta";
            this.tbx_LuxianDirection_delta.Size = new System.Drawing.Size(68, 20);
            this.tbx_LuxianDirection_delta.TabIndex = 29;
            this.tbx_LuxianDirection_delta.Text = "5";
            // 
            // tbx_HDM_caiji_limit
            // 
            this.tbx_HDM_caiji_limit.Location = new System.Drawing.Point(126, 42);
            this.tbx_HDM_caiji_limit.Name = "tbx_HDM_caiji_limit";
            this.tbx_HDM_caiji_limit.Size = new System.Drawing.Size(68, 20);
            this.tbx_HDM_caiji_limit.TabIndex = 31;
            this.tbx_HDM_caiji_limit.Text = "50";
            // 
            // lb_sourceBL
            // 
            this.lb_sourceBL.AutoSize = true;
            this.lb_sourceBL.Location = new System.Drawing.Point(4, 45);
            this.lb_sourceBL.Name = "lb_sourceBL";
            this.lb_sourceBL.Size = new System.Drawing.Size(115, 11);
            this.lb_sourceBL.TabIndex = 30;
            this.lb_sourceBL.Text = "横断面单侧采集范围限";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.groupBox1.Controls.Add(this.tbx_startstack);
            this.groupBox1.Controls.Add(this.label_startstack);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.tbx_fileName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btn_KMLPath);
            this.groupBox1.Controls.Add(this.tbx_SavePath);
            this.groupBox1.Controls.Add(this.tbx_LuxianDirection_delta);
            this.groupBox1.Controls.Add(this.cbx_DL);
            this.groupBox1.Controls.Add(this.rtbx_DL);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.lb_sourceBL);
            this.groupBox1.Controls.Add(this.tbx_HDM_caiji_limit);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 8F);
            this.groupBox1.Location = new System.Drawing.Point(214, 34);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(243, 294);
            this.groupBox1.TabIndex = 32;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "可选参数设置";
            // 
            // tbx_startstack
            // 
            this.tbx_startstack.Location = new System.Drawing.Point(126, 67);
            this.tbx_startstack.Name = "tbx_startstack";
            this.tbx_startstack.Size = new System.Drawing.Size(67, 20);
            this.tbx_startstack.TabIndex = 38;
            this.tbx_startstack.Text = "0";
            // 
            // label_startstack
            // 
            this.label_startstack.AutoSize = true;
            this.label_startstack.Location = new System.Drawing.Point(6, 70);
            this.label_startstack.Name = "label_startstack";
            this.label_startstack.Size = new System.Drawing.Size(49, 11);
            this.label_startstack.TabIndex = 37;
            this.label_startstack.Text = "起始桩号";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 249);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 11);
            this.label5.TabIndex = 35;
            this.label5.Text = "文件名称：";
            // 
            // tbx_fileName
            // 
            this.tbx_fileName.Location = new System.Drawing.Point(10, 264);
            this.tbx_fileName.Name = "tbx_fileName";
            this.tbx_fileName.Size = new System.Drawing.Size(170, 20);
            this.tbx_fileName.TabIndex = 36;
            this.tbx_fileName.Text = "zjy";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 181);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 11);
            this.label2.TabIndex = 32;
            this.label2.Text = "dmx与hdm存放路径：";
            // 
            // btn_KMLPath
            // 
            this.btn_KMLPath.Location = new System.Drawing.Point(5, 223);
            this.btn_KMLPath.Name = "btn_KMLPath";
            this.btn_KMLPath.Size = new System.Drawing.Size(88, 23);
            this.btn_KMLPath.TabIndex = 34;
            this.btn_KMLPath.Text = "设置存放路径";
            this.btn_KMLPath.UseVisualStyleBackColor = true;
            this.btn_KMLPath.Click += new System.EventHandler(this.btn_KMLPath_Click);
            // 
            // tbx_SavePath
            // 
            this.tbx_SavePath.Location = new System.Drawing.Point(10, 196);
            this.tbx_SavePath.Name = "tbx_SavePath";
            this.tbx_SavePath.Size = new System.Drawing.Size(171, 20);
            this.tbx_SavePath.TabIndex = 33;
            this.tbx_SavePath.Text = "D:\\zjy";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.btn_Create_stackSequence);
            this.groupBox2.Controls.Add(this.tbx_Road_Length);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.tbx_SamplingJianJU);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.rtbx_stack_sequence);
            this.groupBox2.Font = new System.Drawing.Font("宋体", 8F);
            this.groupBox2.Location = new System.Drawing.Point(12, 34);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(197, 173);
            this.groupBox2.TabIndex = 33;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "桩号序列生成";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 154);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(139, 11);
            this.label4.TabIndex = 35;
            this.label4.Text = "注:只用于生成HDM辅助线。";
            // 
            // btn_Create_stackSequence
            // 
            this.btn_Create_stackSequence.Location = new System.Drawing.Point(84, 117);
            this.btn_Create_stackSequence.Name = "btn_Create_stackSequence";
            this.btn_Create_stackSequence.Size = new System.Drawing.Size(109, 31);
            this.btn_Create_stackSequence.TabIndex = 34;
            this.btn_Create_stackSequence.Text = "生成序列";
            this.btn_Create_stackSequence.UseVisualStyleBackColor = true;
            this.btn_Create_stackSequence.Click += new System.EventHandler(this.btn_Create_stackSequence_Click);
            // 
            // tbx_Road_Length
            // 
            this.tbx_Road_Length.Location = new System.Drawing.Point(141, 45);
            this.tbx_Road_Length.Name = "tbx_Road_Length";
            this.tbx_Road_Length.Size = new System.Drawing.Size(52, 20);
            this.tbx_Road_Length.TabIndex = 28;
            this.tbx_Road_Length.Text = "20";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(82, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 11);
            this.label1.TabIndex = 27;
            this.label1.Text = "路线总长";
            // 
            // tbx_SamplingJianJU
            // 
            this.tbx_SamplingJianJU.Location = new System.Drawing.Point(141, 72);
            this.tbx_SamplingJianJU.Name = "tbx_SamplingJianJU";
            this.tbx_SamplingJianJU.Size = new System.Drawing.Size(52, 20);
            this.tbx_SamplingJianJU.TabIndex = 26;
            this.tbx_SamplingJianJU.Text = "20";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(82, 75);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 11);
            this.label6.TabIndex = 25;
            this.label6.Text = "采样间距";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 18);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(49, 11);
            this.label9.TabIndex = 24;
            this.label9.Text = "桩号序列";
            // 
            // rtbx_stack_sequence
            // 
            this.rtbx_stack_sequence.Location = new System.Drawing.Point(7, 33);
            this.rtbx_stack_sequence.Name = "rtbx_stack_sequence";
            this.rtbx_stack_sequence.Size = new System.Drawing.Size(71, 115);
            this.rtbx_stack_sequence.TabIndex = 23;
            this.rtbx_stack_sequence.Text = "";
            // 
            // btn_adjust_road_start
            // 
            this.btn_adjust_road_start.BackColor = System.Drawing.Color.White;
            this.btn_adjust_road_start.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_adjust_road_start.Location = new System.Drawing.Point(10, 244);
            this.btn_adjust_road_start.Name = "btn_adjust_road_start";
            this.btn_adjust_road_start.Size = new System.Drawing.Size(199, 34);
            this.btn_adjust_road_start.TabIndex = 35;
            this.btn_adjust_road_start.Text = "设置路线起点(不是必须，但需验证)";
            this.btn_adjust_road_start.UseVisualStyleBackColor = false;
            this.btn_adjust_road_start.Click += new System.EventHandler(this.btn_adjust_road_start_Click);
            // 
            // btn_CreateHDM_Pline3D
            // 
            this.btn_CreateHDM_Pline3D.BackColor = System.Drawing.Color.White;
            this.btn_CreateHDM_Pline3D.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_CreateHDM_Pline3D.Location = new System.Drawing.Point(10, 280);
            this.btn_CreateHDM_Pline3D.Name = "btn_CreateHDM_Pline3D";
            this.btn_CreateHDM_Pline3D.Size = new System.Drawing.Size(199, 34);
            this.btn_CreateHDM_Pline3D.TabIndex = 36;
            this.btn_CreateHDM_Pline3D.Text = "生成HDM辅助线(必须)";
            this.btn_CreateHDM_Pline3D.UseVisualStyleBackColor = false;
            this.btn_CreateHDM_Pline3D.Click += new System.EventHandler(this.btn_CreateHDM_Pline3D_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.button1.Font = new System.Drawing.Font("宋体", 8F);
            this.button1.Location = new System.Drawing.Point(325, 458);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(103, 30);
            this.button1.TabIndex = 37;
            this.button1.Text = "删除HDM辅助线";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_Create_dmx_hdm_file
            // 
            this.btn_Create_dmx_hdm_file.BackColor = System.Drawing.Color.White;
            this.btn_Create_dmx_hdm_file.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Create_dmx_hdm_file.Location = new System.Drawing.Point(10, 316);
            this.btn_Create_dmx_hdm_file.Name = "btn_Create_dmx_hdm_file";
            this.btn_Create_dmx_hdm_file.Size = new System.Drawing.Size(199, 34);
            this.btn_Create_dmx_hdm_file.TabIndex = 38;
            this.btn_Create_dmx_hdm_file.Text = "生成dmx与hdm文件";
            this.btn_Create_dmx_hdm_file.UseVisualStyleBackColor = false;
            this.btn_Create_dmx_hdm_file.Click += new System.EventHandler(this.btn_Create_dmx_hdm_file_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.groupBox3.Controls.Add(this.tbx_pline3d_xdata_showstack);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.tbx_pline3d_xdata_stack);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.btn_change_pline3D_stack_xdata);
            this.groupBox3.Font = new System.Drawing.Font("宋体", 8F);
            this.groupBox3.Location = new System.Drawing.Point(214, 334);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(243, 66);
            this.groupBox3.TabIndex = 39;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "横断面辅助线桩号修改";
            // 
            // tbx_pline3d_xdata_showstack
            // 
            this.tbx_pline3d_xdata_showstack.Location = new System.Drawing.Point(103, 15);
            this.tbx_pline3d_xdata_showstack.Name = "tbx_pline3d_xdata_showstack";
            this.tbx_pline3d_xdata_showstack.Size = new System.Drawing.Size(51, 20);
            this.tbx_pline3d_xdata_showstack.TabIndex = 39;
            this.tbx_pline3d_xdata_showstack.Text = "20";
            this.tbx_pline3d_xdata_showstack.TextChanged += new System.EventHandler(this.tbx_pline3d_xdata_showstack_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 11);
            this.label7.TabIndex = 37;
            this.label7.Text = "显示桩号";
            // 
            // tbx_pline3d_xdata_stack
            // 
            this.tbx_pline3d_xdata_stack.Location = new System.Drawing.Point(103, 38);
            this.tbx_pline3d_xdata_stack.Name = "tbx_pline3d_xdata_stack";
            this.tbx_pline3d_xdata_stack.Size = new System.Drawing.Size(51, 20);
            this.tbx_pline3d_xdata_stack.TabIndex = 36;
            this.tbx_pline3d_xdata_stack.Text = "20";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 11);
            this.label3.TabIndex = 35;
            this.label3.Text = "实际桩号(非断链)";
            // 
            // btn_change_pline3D_stack_xdata
            // 
            this.btn_change_pline3D_stack_xdata.Location = new System.Drawing.Point(160, 15);
            this.btn_change_pline3D_stack_xdata.Name = "btn_change_pline3D_stack_xdata";
            this.btn_change_pline3D_stack_xdata.Size = new System.Drawing.Size(70, 46);
            this.btn_change_pline3D_stack_xdata.TabIndex = 35;
            this.btn_change_pline3D_stack_xdata.Text = "修改桩号";
            this.btn_change_pline3D_stack_xdata.UseVisualStyleBackColor = true;
            this.btn_change_pline3D_stack_xdata.Click += new System.EventHandler(this.btn_change_pline3D_stack_xdata_Click);
            // 
            // btn_Show_all_pline3d_xdata_stack
            // 
            this.btn_Show_all_pline3d_xdata_stack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btn_Show_all_pline3d_xdata_stack.Font = new System.Drawing.Font("宋体", 8F);
            this.btn_Show_all_pline3d_xdata_stack.Location = new System.Drawing.Point(217, 426);
            this.btn_Show_all_pline3d_xdata_stack.Name = "btn_Show_all_pline3d_xdata_stack";
            this.btn_Show_all_pline3d_xdata_stack.Size = new System.Drawing.Size(129, 30);
            this.btn_Show_all_pline3d_xdata_stack.TabIndex = 37;
            this.btn_Show_all_pline3d_xdata_stack.Text = "显示所有辅助线桩号";
            this.btn_Show_all_pline3d_xdata_stack.UseVisualStyleBackColor = false;
            this.btn_Show_all_pline3d_xdata_stack.Click += new System.EventHandler(this.btn_Show_all_pline3d_xdata_stack_Click);
            // 
            // btn_showstack_ed
            // 
            this.btn_showstack_ed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btn_showstack_ed.Font = new System.Drawing.Font("宋体", 8F);
            this.btn_showstack_ed.Location = new System.Drawing.Point(348, 426);
            this.btn_showstack_ed.Name = "btn_showstack_ed";
            this.btn_showstack_ed.Size = new System.Drawing.Size(80, 30);
            this.btn_showstack_ed.TabIndex = 38;
            this.btn_showstack_ed.Text = "显示桩号";
            this.btn_showstack_ed.UseVisualStyleBackColor = false;
            this.btn_showstack_ed.Click += new System.EventHandler(this.btn_showstack_ed_Click);
            // 
            // btn_delete_all_pline3d_xdata_stack
            // 
            this.btn_delete_all_pline3d_xdata_stack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btn_delete_all_pline3d_xdata_stack.Font = new System.Drawing.Font("宋体", 8F);
            this.btn_delete_all_pline3d_xdata_stack.Location = new System.Drawing.Point(217, 458);
            this.btn_delete_all_pline3d_xdata_stack.Name = "btn_delete_all_pline3d_xdata_stack";
            this.btn_delete_all_pline3d_xdata_stack.Size = new System.Drawing.Size(107, 30);
            this.btn_delete_all_pline3d_xdata_stack.TabIndex = 40;
            this.btn_delete_all_pline3d_xdata_stack.Text = "删除辅助线桩号";
            this.btn_delete_all_pline3d_xdata_stack.UseVisualStyleBackColor = false;
            this.btn_delete_all_pline3d_xdata_stack.Click += new System.EventHandler(this.btn_delete_all_pline3d_xdata_stack_Click);
            // 
            // btn_CheckGCD
            // 
            this.btn_CheckGCD.BackColor = System.Drawing.Color.White;
            this.btn_CheckGCD.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_CheckGCD.Location = new System.Drawing.Point(10, 209);
            this.btn_CheckGCD.Name = "btn_CheckGCD";
            this.btn_CheckGCD.Size = new System.Drawing.Size(99, 34);
            this.btn_CheckGCD.TabIndex = 41;
            this.btn_CheckGCD.Text = "检查高程点数据正确性(建议)";
            this.btn_CheckGCD.UseVisualStyleBackColor = false;
            this.btn_CheckGCD.Click += new System.EventHandler(this.btn_CheckGCD_Click);
            // 
            // btn_checkAndmodify
            // 
            this.btn_checkAndmodify.BackColor = System.Drawing.Color.White;
            this.btn_checkAndmodify.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_checkAndmodify.Location = new System.Drawing.Point(110, 209);
            this.btn_checkAndmodify.Name = "btn_checkAndmodify";
            this.btn_checkAndmodify.Size = new System.Drawing.Size(99, 34);
            this.btn_checkAndmodify.TabIndex = 42;
            this.btn_checkAndmodify.Text = "检查高程点数据正确性并修改";
            this.btn_checkAndmodify.UseVisualStyleBackColor = false;
            this.btn_checkAndmodify.Click += new System.EventHandler(this.btn_checkAndmodify_Click);
            // 
            // lbl_fuzhu
            // 
            this.lbl_fuzhu.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbl_fuzhu.CausesValidation = false;
            this.lbl_fuzhu.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_fuzhu.Location = new System.Drawing.Point(54, 3);
            this.lbl_fuzhu.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_fuzhu.Name = "lbl_fuzhu";
            this.lbl_fuzhu.Size = new System.Drawing.Size(292, 28);
            this.lbl_fuzhu.TabIndex = 43;
            this.lbl_fuzhu.Text = "根据采集的横断数据生成纬地.hdm文件";
            this.lbl_fuzhu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.richTextBox1.Location = new System.Drawing.Point(463, 34);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(282, 454);
            this.richTextBox1.TabIndex = 36;
            this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
            // 
            // tbx_hdm_test_attitude_limit
            // 
            this.tbx_hdm_test_attitude_limit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.tbx_hdm_test_attitude_limit.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbx_hdm_test_attitude_limit.Location = new System.Drawing.Point(329, 403);
            this.tbx_hdm_test_attitude_limit.Name = "tbx_hdm_test_attitude_limit";
            this.tbx_hdm_test_attitude_limit.Size = new System.Drawing.Size(31, 21);
            this.tbx_hdm_test_attitude_limit.TabIndex = 41;
            this.tbx_hdm_test_attitude_limit.Text = "10";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.label8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(212, 407);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(113, 12);
            this.label8.TabIndex = 40;
            this.label8.Text = "横断面测试高程限值";
            // 
            // btn_test_hdm_attitude
            // 
            this.btn_test_hdm_attitude.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btn_test_hdm_attitude.Font = new System.Drawing.Font("宋体", 8F);
            this.btn_test_hdm_attitude.Location = new System.Drawing.Point(363, 397);
            this.btn_test_hdm_attitude.Name = "btn_test_hdm_attitude";
            this.btn_test_hdm_attitude.Size = new System.Drawing.Size(64, 30);
            this.btn_test_hdm_attitude.TabIndex = 44;
            this.btn_test_hdm_attitude.Text = "测试";
            this.btn_test_hdm_attitude.UseVisualStyleBackColor = false;
            this.btn_test_hdm_attitude.Click += new System.EventHandler(this.btn_test_hdm_attitude_Click);
            // 
            // zjy_hdm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Controls.Add(this.btn_test_hdm_attitude);
            this.Controls.Add(this.tbx_hdm_test_attitude_limit);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.lbl_fuzhu);
            this.Controls.Add(this.btn_checkAndmodify);
            this.Controls.Add(this.btn_CheckGCD);
            this.Controls.Add(this.btn_delete_all_pline3d_xdata_stack);
            this.Controls.Add(this.btn_Show_all_pline3d_xdata_stack);
            this.Controls.Add(this.btn_showstack_ed);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btn_Create_dmx_hdm_file);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_CreateHDM_Pline3D);
            this.Controls.Add(this.btn_adjust_road_start);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "zjy_hdm";
            this.Size = new System.Drawing.Size(755, 501);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.CheckBox cbx_DL;
        private System.Windows.Forms.RichTextBox rtbx_DL;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbx_LuxianDirection_delta;
        private System.Windows.Forms.TextBox tbx_HDM_caiji_limit;
        private System.Windows.Forms.Label lb_sourceBL;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_Create_stackSequence;
        private System.Windows.Forms.TextBox tbx_Road_Length;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbx_SamplingJianJU;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.RichTextBox rtbx_stack_sequence;
        private System.Windows.Forms.Button btn_adjust_road_start;
        private System.Windows.Forms.Button btn_CreateHDM_Pline3D;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btn_Create_dmx_hdm_file;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbx_fileName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_KMLPath;
        private System.Windows.Forms.TextBox tbx_SavePath;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btn_Show_all_pline3d_xdata_stack;
        private System.Windows.Forms.TextBox tbx_pline3d_xdata_stack;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_change_pline3D_stack_xdata;
        private System.Windows.Forms.TextBox tbx_startstack;
        private System.Windows.Forms.Label label_startstack;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_showstack_ed;
        private System.Windows.Forms.Button btn_delete_all_pline3d_xdata_stack;
        private System.Windows.Forms.Button btn_CheckGCD;
        private System.Windows.Forms.Button btn_checkAndmodify;
        private System.Windows.Forms.Label lbl_fuzhu;
        private System.Windows.Forms.TextBox tbx_pline3d_xdata_showstack;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TextBox tbx_hdm_test_attitude_limit;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btn_test_hdm_attitude;
    }
}
