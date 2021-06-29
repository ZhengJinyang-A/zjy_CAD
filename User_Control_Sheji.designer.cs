namespace zjy_cad_chajian
{
    partial class User_Control_Sheji
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
            this.btn_jdpm = new System.Windows.Forms.Button();
            this.btn_xiaodao = new System.Windows.Forms.Button();
            this.btn_shumo_shuju = new System.Windows.Forms.Button();
            this.gBx_shumo = new System.Windows.Forms.GroupBox();
            this.btn_sm_hdm = new System.Windows.Forms.Button();
            this.btn_sm_zdm = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.bt_sm_save = new System.Windows.Forms.Button();
            this.btn_sanjiaogouwang = new System.Windows.Forms.Button();
            this.btn_sm_input = new System.Windows.Forms.Button();
            this.btm_sj_zdm = new System.Windows.Forms.Button();
            this.btn_ljsj = new System.Windows.Forms.Button();
            this.btn_hdm_ht = new System.Windows.Forms.Button();
            this.performanceCounter1 = new System.Diagnostics.PerformanceCounter();
            this.设计 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_road_del = new System.Windows.Forms.Button();
            this.btn_control_canshu = new System.Windows.Forms.Button();
            this.gBx_shumo.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.performanceCounter1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_jdpm
            // 
            this.btn_jdpm.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn_jdpm.Location = new System.Drawing.Point(3, 47);
            this.btn_jdpm.Name = "btn_jdpm";
            this.btn_jdpm.Size = new System.Drawing.Size(122, 25);
            this.btn_jdpm.TabIndex = 1;
            this.btn_jdpm.Text = "交点平面";
            this.btn_jdpm.UseVisualStyleBackColor = true;
            this.btn_jdpm.Click += new System.EventHandler(this.btn_jdpm_Click);
            // 
            // btn_xiaodao
            // 
            this.btn_xiaodao.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn_xiaodao.Location = new System.Drawing.Point(3, 78);
            this.btn_xiaodao.Name = "btn_xiaodao";
            this.btn_xiaodao.Size = new System.Drawing.Size(122, 25);
            this.btn_xiaodao.TabIndex = 2;
            this.btn_xiaodao.Text = "设计向导";
            this.btn_xiaodao.UseVisualStyleBackColor = true;
            this.btn_xiaodao.Click += new System.EventHandler(this.btn_xiaodao_Click);
            // 
            // btn_shumo_shuju
            // 
            this.btn_shumo_shuju.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn_shumo_shuju.Location = new System.Drawing.Point(0, 3);
            this.btn_shumo_shuju.Name = "btn_shumo_shuju";
            this.btn_shumo_shuju.Size = new System.Drawing.Size(50, 61);
            this.btn_shumo_shuju.TabIndex = 3;
            this.btn_shumo_shuju.Text = "三维数据输入";
            this.btn_shumo_shuju.UseVisualStyleBackColor = true;
            this.btn_shumo_shuju.Click += new System.EventHandler(this.btn_shumo_shuju_Click);
            // 
            // gBx_shumo
            // 
            this.gBx_shumo.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.gBx_shumo.Controls.Add(this.btn_sm_hdm);
            this.gBx_shumo.Controls.Add(this.btn_sm_zdm);
            this.gBx_shumo.Controls.Add(this.splitContainer1);
            this.gBx_shumo.Location = new System.Drawing.Point(3, 109);
            this.gBx_shumo.Name = "gBx_shumo";
            this.gBx_shumo.Size = new System.Drawing.Size(122, 249);
            this.gBx_shumo.TabIndex = 4;
            this.gBx_shumo.TabStop = false;
            this.gBx_shumo.Text = "数模";
            // 
            // btn_sm_hdm
            // 
            this.btn_sm_hdm.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn_sm_hdm.Location = new System.Drawing.Point(3, 219);
            this.btn_sm_hdm.Name = "btn_sm_hdm";
            this.btn_sm_hdm.Size = new System.Drawing.Size(113, 25);
            this.btn_sm_hdm.TabIndex = 7;
            this.btn_sm_hdm.Text = "横断地面线";
            this.btn_sm_hdm.UseVisualStyleBackColor = true;
            this.btn_sm_hdm.Click += new System.EventHandler(this.btn_sm_hdm_Click);
            // 
            // btn_sm_zdm
            // 
            this.btn_sm_zdm.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn_sm_zdm.Location = new System.Drawing.Point(3, 191);
            this.btn_sm_zdm.Name = "btn_sm_zdm";
            this.btn_sm_zdm.Size = new System.Drawing.Size(113, 25);
            this.btn_sm_zdm.TabIndex = 6;
            this.btn_sm_zdm.Text = "纵断地面线";
            this.btn_sm_zdm.UseVisualStyleBackColor = true;
            this.btn_sm_zdm.Click += new System.EventHandler(this.btn_sm_zdm_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(6, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.bt_sm_save);
            this.splitContainer1.Panel1.Controls.Add(this.btn_shumo_shuju);
            this.splitContainer1.Panel1.Controls.Add(this.btn_sanjiaogouwang);
            this.splitContainer1.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btn_sm_input);
            this.splitContainer1.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer1.Size = new System.Drawing.Size(113, 166);
            this.splitContainer1.SplitterDistance = 52;
            this.splitContainer1.TabIndex = 5;
            // 
            // bt_sm_save
            // 
            this.bt_sm_save.Location = new System.Drawing.Point(-1, 110);
            this.bt_sm_save.Name = "bt_sm_save";
            this.bt_sm_save.Size = new System.Drawing.Size(50, 56);
            this.bt_sm_save.TabIndex = 5;
            this.bt_sm_save.Text = "保存数模";
            this.bt_sm_save.UseVisualStyleBackColor = true;
            this.bt_sm_save.Click += new System.EventHandler(this.bt_sm_save_Click);
            // 
            // btn_sanjiaogouwang
            // 
            this.btn_sanjiaogouwang.Location = new System.Drawing.Point(0, 60);
            this.btn_sanjiaogouwang.Name = "btn_sanjiaogouwang";
            this.btn_sanjiaogouwang.Size = new System.Drawing.Size(50, 51);
            this.btn_sanjiaogouwang.TabIndex = 4;
            this.btn_sanjiaogouwang.Text = "三角构网";
            this.btn_sanjiaogouwang.UseVisualStyleBackColor = true;
            this.btn_sanjiaogouwang.Click += new System.EventHandler(this.btn_sanjiaogouwang_Click);
            // 
            // btn_sm_input
            // 
            this.btn_sm_input.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn_sm_input.Location = new System.Drawing.Point(3, 3);
            this.btn_sm_input.Name = "btn_sm_input";
            this.btn_sm_input.Size = new System.Drawing.Size(50, 160);
            this.btn_sm_input.TabIndex = 5;
            this.btn_sm_input.Text = "数模输入";
            this.btn_sm_input.UseVisualStyleBackColor = true;
            this.btn_sm_input.Click += new System.EventHandler(this.btn_sm_input_Click);
            // 
            // btm_sj_zdm
            // 
            this.btm_sj_zdm.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btm_sj_zdm.Location = new System.Drawing.Point(6, 364);
            this.btm_sj_zdm.Name = "btm_sj_zdm";
            this.btm_sj_zdm.Size = new System.Drawing.Size(116, 25);
            this.btm_sj_zdm.TabIndex = 5;
            this.btm_sj_zdm.Text = "纵断面设计";
            this.btm_sj_zdm.UseVisualStyleBackColor = true;
            this.btm_sj_zdm.Click += new System.EventHandler(this.btm_sj_zdm_Click);
            // 
            // btn_ljsj
            // 
            this.btn_ljsj.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn_ljsj.Location = new System.Drawing.Point(6, 395);
            this.btn_ljsj.Name = "btn_ljsj";
            this.btn_ljsj.Size = new System.Drawing.Size(116, 25);
            this.btn_ljsj.TabIndex = 6;
            this.btn_ljsj.Text = "路基设计";
            this.btn_ljsj.UseVisualStyleBackColor = true;
            this.btn_ljsj.Click += new System.EventHandler(this.btn_ljsj_Click);
            // 
            // btn_hdm_ht
            // 
            this.btn_hdm_ht.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn_hdm_ht.Location = new System.Drawing.Point(6, 426);
            this.btn_hdm_ht.Name = "btn_hdm_ht";
            this.btn_hdm_ht.Size = new System.Drawing.Size(116, 25);
            this.btn_hdm_ht.TabIndex = 7;
            this.btn_hdm_ht.Text = "横断面绘图";
            this.btn_hdm_ht.UseVisualStyleBackColor = true;
            this.btn_hdm_ht.Click += new System.EventHandler(this.btn_hdm_ht_Click);
            // 
            // 设计
            // 
            this.设计.BackColor = System.Drawing.Color.WhiteSmoke;
            this.设计.CausesValidation = false;
            this.设计.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.设计.Location = new System.Drawing.Point(4, 16);
            this.设计.Name = "设计";
            this.设计.Size = new System.Drawing.Size(122, 28);
            this.设计.TabIndex = 8;
            this.设计.Text = "纬地设计";
            this.设计.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(1, 472);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 28);
            this.label1.TabIndex = 9;
            this.label1.Text = "辅助";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_road_del
            // 
            this.btn_road_del.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn_road_del.Location = new System.Drawing.Point(6, 512);
            this.btn_road_del.Name = "btn_road_del";
            this.btn_road_del.Size = new System.Drawing.Size(116, 25);
            this.btn_road_del.TabIndex = 10;
            this.btn_road_del.Text = "删除路线";
            this.btn_road_del.UseVisualStyleBackColor = true;
            this.btn_road_del.Click += new System.EventHandler(this.btn_road_del_Click);
            // 
            // btn_control_canshu
            // 
            this.btn_control_canshu.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn_control_canshu.Location = new System.Drawing.Point(6, 543);
            this.btn_control_canshu.Name = "btn_control_canshu";
            this.btn_control_canshu.Size = new System.Drawing.Size(116, 25);
            this.btn_control_canshu.TabIndex = 11;
            this.btn_control_canshu.Text = "控制参数";
            this.btn_control_canshu.UseVisualStyleBackColor = true;
            this.btn_control_canshu.Click += new System.EventHandler(this.btn_control_canshu_Click);
            // 
            // User_Control_Sheji
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.Controls.Add(this.btn_control_canshu);
            this.Controls.Add(this.btn_road_del);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.设计);
            this.Controls.Add(this.btn_hdm_ht);
            this.Controls.Add(this.btn_ljsj);
            this.Controls.Add(this.btm_sj_zdm);
            this.Controls.Add(this.gBx_shumo);
            this.Controls.Add(this.btn_xiaodao);
            this.Controls.Add(this.btn_jdpm);
            this.Name = "User_Control_Sheji";
            this.Size = new System.Drawing.Size(139, 720);
            this.gBx_shumo.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
          //  ((System.ComponentModel.ISupportInitialize)(this.performanceCounter1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btn_jdpm;
        private System.Windows.Forms.Button btn_xiaodao;
        private System.Windows.Forms.Button btn_shumo_shuju;
        private System.Windows.Forms.GroupBox gBx_shumo;
        private System.Windows.Forms.Button btn_sm_hdm;
        private System.Windows.Forms.Button btn_sm_zdm;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btn_sanjiaogouwang;
        private System.Windows.Forms.Button btn_sm_input;
        private System.Windows.Forms.Button btm_sj_zdm;
        private System.Windows.Forms.Button btn_ljsj;
        private System.Windows.Forms.Button btn_hdm_ht;
        private System.Diagnostics.PerformanceCounter performanceCounter1;
        private System.Windows.Forms.Button bt_sm_save;
        private System.Windows.Forms.Label 设计;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_road_del;
        private System.Windows.Forms.Button btn_control_canshu;
    }
}
