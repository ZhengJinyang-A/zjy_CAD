namespace zjy_cad_chajian
{
    partial class User_Control_fuzhu
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
            this.btn_layer_show = new System.Windows.Forms.Button();
            this.btn_layer_hide = new System.Windows.Forms.Button();
            this.btn_layer_select_all = new System.Windows.Forms.Button();
            this.btn_object_layer_object_show = new System.Windows.Forms.Button();
            this.btn_object_hide = new System.Windows.Forms.Button();
            this.btn_all_show = new System.Windows.Forms.Button();
            this.btn_object_layer_color = new System.Windows.Forms.Button();
            this.btn_object_layer_object_color = new System.Windows.Forms.Button();
            this.btn_layer_all_color = new System.Windows.Forms.Button();
            this.btn_all_layer_object_color = new System.Windows.Forms.Button();
            this.tbx_color_index = new System.Windows.Forms.TextBox();
            this.btn_color_bylayer = new System.Windows.Forms.Button();
            this.btn_color_byblock = new System.Windows.Forms.Button();
            this.btn_object_layer_object_byblock = new System.Windows.Forms.Button();
            this.btn_object_layer_object_bylayer = new System.Windows.Forms.Button();
            this.btn_all_hide = new System.Windows.Forms.Button();
            this.rd_btn_7 = new System.Windows.Forms.RadioButton();
            this.rdBtn_251 = new System.Windows.Forms.RadioButton();
            this.rdBtn_1 = new System.Windows.Forms.RadioButton();
            this.rdBtn_2 = new System.Windows.Forms.RadioButton();
            this.rdBtn_3 = new System.Windows.Forms.RadioButton();
            this.rdBtn_6 = new System.Windows.Forms.RadioButton();
            this.rdBtn_5 = new System.Windows.Forms.RadioButton();
            this.rdBtn_4 = new System.Windows.Forms.RadioButton();
            this.rd_btn_Index = new System.Windows.Forms.RadioButton();
            this.btn_object_layer_color_object_show = new System.Windows.Forms.Button();
            this.btn_all_object_show = new System.Windows.Forms.Button();
            this.btn_object_color_layer_objects = new System.Windows.Forms.Button();
            this.gbx_Color = new System.Windows.Forms.GroupBox();
            this.btn_object_layer_object_Random_Color = new System.Windows.Forms.Button();
            this.btn_object_layer_Random_Color = new System.Windows.Forms.Button();
            this.btn_all_object_Random_Color = new System.Windows.Forms.Button();
            this.btn_all_Random_Color = new System.Windows.Forms.Button();
            this.btn_object_layer_object_color_1 = new System.Windows.Forms.Button();
            this.btn_color_color = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_Create_zjy_layer = new System.Windows.Forms.Button();
            this.gbx_Color.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_fuzhu
            // 
            this.lbl_fuzhu.BackColor = System.Drawing.Color.White;
            this.lbl_fuzhu.CausesValidation = false;
            this.lbl_fuzhu.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_fuzhu.Location = new System.Drawing.Point(71, 9);
            this.lbl_fuzhu.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_fuzhu.Name = "lbl_fuzhu";
            this.lbl_fuzhu.Size = new System.Drawing.Size(122, 28);
            this.lbl_fuzhu.TabIndex = 10;
            this.lbl_fuzhu.Text = "图层操作";
            this.lbl_fuzhu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_layer_show
            // 
            this.btn_layer_show.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn_layer_show.Location = new System.Drawing.Point(0, 25);
            this.btn_layer_show.Margin = new System.Windows.Forms.Padding(4);
            this.btn_layer_show.Name = "btn_layer_show";
            this.btn_layer_show.Size = new System.Drawing.Size(122, 25);
            this.btn_layer_show.TabIndex = 9;
            this.btn_layer_show.Text = "对象层显示";
            this.btn_layer_show.UseVisualStyleBackColor = true;
            this.btn_layer_show.Click += new System.EventHandler(this.btn_layer_show_Click);
            // 
            // btn_layer_hide
            // 
            this.btn_layer_hide.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn_layer_hide.Location = new System.Drawing.Point(0, 58);
            this.btn_layer_hide.Margin = new System.Windows.Forms.Padding(4);
            this.btn_layer_hide.Name = "btn_layer_hide";
            this.btn_layer_hide.Size = new System.Drawing.Size(122, 25);
            this.btn_layer_hide.TabIndex = 13;
            this.btn_layer_hide.Text = "对象层隐藏";
            this.btn_layer_hide.UseVisualStyleBackColor = true;
            this.btn_layer_hide.Click += new System.EventHandler(this.btn_layer_hide_Click);
            // 
            // btn_layer_select_all
            // 
            this.btn_layer_select_all.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn_layer_select_all.Location = new System.Drawing.Point(4, 32);
            this.btn_layer_select_all.Margin = new System.Windows.Forms.Padding(4);
            this.btn_layer_select_all.Name = "btn_layer_select_all";
            this.btn_layer_select_all.Size = new System.Drawing.Size(122, 25);
            this.btn_layer_select_all.TabIndex = 14;
            this.btn_layer_select_all.Text = "选择对象层全部对象";
            this.btn_layer_select_all.UseVisualStyleBackColor = true;
            this.btn_layer_select_all.Click += new System.EventHandler(this.btn_layer_select_all_Click);
            // 
            // btn_object_layer_object_show
            // 
            this.btn_object_layer_object_show.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn_object_layer_object_show.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_object_layer_object_show.Location = new System.Drawing.Point(130, 25);
            this.btn_object_layer_object_show.Margin = new System.Windows.Forms.Padding(0);
            this.btn_object_layer_object_show.Name = "btn_object_layer_object_show";
            this.btn_object_layer_object_show.Size = new System.Drawing.Size(146, 25);
            this.btn_object_layer_object_show.TabIndex = 15;
            this.btn_object_layer_object_show.Text = "对象层选对象显示";
            this.btn_object_layer_object_show.UseVisualStyleBackColor = true;
            this.btn_object_layer_object_show.Click += new System.EventHandler(this.btn_object_show_Click);
            // 
            // btn_object_hide
            // 
            this.btn_object_hide.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn_object_hide.Location = new System.Drawing.Point(130, 58);
            this.btn_object_hide.Margin = new System.Windows.Forms.Padding(4);
            this.btn_object_hide.Name = "btn_object_hide";
            this.btn_object_hide.Size = new System.Drawing.Size(146, 25);
            this.btn_object_hide.TabIndex = 16;
            this.btn_object_hide.Text = "选对象隐藏";
            this.btn_object_hide.UseVisualStyleBackColor = true;
            this.btn_object_hide.Click += new System.EventHandler(this.btn_object_hide_Click);
            // 
            // btn_all_show
            // 
            this.btn_all_show.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn_all_show.Location = new System.Drawing.Point(0, 87);
            this.btn_all_show.Margin = new System.Windows.Forms.Padding(4);
            this.btn_all_show.Name = "btn_all_show";
            this.btn_all_show.Size = new System.Drawing.Size(122, 25);
            this.btn_all_show.TabIndex = 17;
            this.btn_all_show.Text = "所有显示";
            this.btn_all_show.UseVisualStyleBackColor = true;
            this.btn_all_show.Click += new System.EventHandler(this.btn_layer_all_show_Click);
            // 
            // btn_object_layer_color
            // 
            this.btn_object_layer_color.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn_object_layer_color.Location = new System.Drawing.Point(7, 22);
            this.btn_object_layer_color.Margin = new System.Windows.Forms.Padding(4);
            this.btn_object_layer_color.Name = "btn_object_layer_color";
            this.btn_object_layer_color.Size = new System.Drawing.Size(122, 25);
            this.btn_object_layer_color.TabIndex = 18;
            this.btn_object_layer_color.Text = "对象层颜色";
            this.btn_object_layer_color.UseVisualStyleBackColor = true;
            this.btn_object_layer_color.Click += new System.EventHandler(this.btn_object_layer_color_Click);
            // 
            // btn_object_layer_object_color
            // 
            this.btn_object_layer_object_color.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn_object_layer_object_color.Location = new System.Drawing.Point(134, 22);
            this.btn_object_layer_object_color.Margin = new System.Windows.Forms.Padding(4);
            this.btn_object_layer_object_color.Name = "btn_object_layer_object_color";
            this.btn_object_layer_object_color.Size = new System.Drawing.Size(122, 25);
            this.btn_object_layer_object_color.TabIndex = 19;
            this.btn_object_layer_object_color.Text = "对象层对象颜色";
            this.btn_object_layer_object_color.UseVisualStyleBackColor = true;
            this.btn_object_layer_object_color.Click += new System.EventHandler(this.btn_object_layer_object_color_Click);
            // 
            // btn_layer_all_color
            // 
            this.btn_layer_all_color.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn_layer_all_color.Location = new System.Drawing.Point(7, 55);
            this.btn_layer_all_color.Margin = new System.Windows.Forms.Padding(4);
            this.btn_layer_all_color.Name = "btn_layer_all_color";
            this.btn_layer_all_color.Size = new System.Drawing.Size(122, 25);
            this.btn_layer_all_color.TabIndex = 20;
            this.btn_layer_all_color.Text = "所有层颜色";
            this.btn_layer_all_color.UseVisualStyleBackColor = true;
            this.btn_layer_all_color.Click += new System.EventHandler(this.btn_layer_all_color_Click);
            // 
            // btn_all_layer_object_color
            // 
            this.btn_all_layer_object_color.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn_all_layer_object_color.Location = new System.Drawing.Point(134, 55);
            this.btn_all_layer_object_color.Margin = new System.Windows.Forms.Padding(4);
            this.btn_all_layer_object_color.Name = "btn_all_layer_object_color";
            this.btn_all_layer_object_color.Size = new System.Drawing.Size(122, 25);
            this.btn_all_layer_object_color.TabIndex = 21;
            this.btn_all_layer_object_color.Text = "所有层对象颜色";
            this.btn_all_layer_object_color.UseVisualStyleBackColor = true;
            this.btn_all_layer_object_color.Click += new System.EventHandler(this.btn_all_layer_object_color_Click);
            // 
            // tbx_color_index
            // 
            this.tbx_color_index.Location = new System.Drawing.Point(36, 132);
            this.tbx_color_index.Name = "tbx_color_index";
            this.tbx_color_index.Size = new System.Drawing.Size(33, 21);
            this.tbx_color_index.TabIndex = 24;
            // 
            // btn_color_bylayer
            // 
            this.btn_color_bylayer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn_color_bylayer.Location = new System.Drawing.Point(7, 172);
            this.btn_color_bylayer.Margin = new System.Windows.Forms.Padding(4);
            this.btn_color_bylayer.Name = "btn_color_bylayer";
            this.btn_color_bylayer.Size = new System.Drawing.Size(122, 25);
            this.btn_color_bylayer.TabIndex = 25;
            this.btn_color_bylayer.Tag = "";
            this.btn_color_bylayer.Text = "所有对象随层";
            this.btn_color_bylayer.UseVisualStyleBackColor = true;
            this.btn_color_bylayer.Click += new System.EventHandler(this.btn_color_bylayer_Click);
            // 
            // btn_color_byblock
            // 
            this.btn_color_byblock.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn_color_byblock.Location = new System.Drawing.Point(7, 204);
            this.btn_color_byblock.Margin = new System.Windows.Forms.Padding(4);
            this.btn_color_byblock.Name = "btn_color_byblock";
            this.btn_color_byblock.Size = new System.Drawing.Size(122, 25);
            this.btn_color_byblock.TabIndex = 26;
            this.btn_color_byblock.Tag = "";
            this.btn_color_byblock.Text = "所有对象随块";
            this.btn_color_byblock.UseVisualStyleBackColor = true;
            this.btn_color_byblock.Click += new System.EventHandler(this.btn_color_byblock_Click);
            // 
            // btn_object_layer_object_byblock
            // 
            this.btn_object_layer_object_byblock.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn_object_layer_object_byblock.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_object_layer_object_byblock.Location = new System.Drawing.Point(137, 205);
            this.btn_object_layer_object_byblock.Margin = new System.Windows.Forms.Padding(4);
            this.btn_object_layer_object_byblock.Name = "btn_object_layer_object_byblock";
            this.btn_object_layer_object_byblock.Size = new System.Drawing.Size(139, 25);
            this.btn_object_layer_object_byblock.TabIndex = 28;
            this.btn_object_layer_object_byblock.Tag = "";
            this.btn_object_layer_object_byblock.Text = "对象层所有对象随块";
            this.btn_object_layer_object_byblock.UseVisualStyleBackColor = true;
            this.btn_object_layer_object_byblock.Click += new System.EventHandler(this.btn_object_layer_object_byblock_Click);
            // 
            // btn_object_layer_object_bylayer
            // 
            this.btn_object_layer_object_bylayer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn_object_layer_object_bylayer.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_object_layer_object_bylayer.Location = new System.Drawing.Point(137, 172);
            this.btn_object_layer_object_bylayer.Margin = new System.Windows.Forms.Padding(4);
            this.btn_object_layer_object_bylayer.Name = "btn_object_layer_object_bylayer";
            this.btn_object_layer_object_bylayer.Size = new System.Drawing.Size(139, 25);
            this.btn_object_layer_object_bylayer.TabIndex = 27;
            this.btn_object_layer_object_bylayer.Tag = "";
            this.btn_object_layer_object_bylayer.Text = "对象层所有对象随层";
            this.btn_object_layer_object_bylayer.UseVisualStyleBackColor = true;
            this.btn_object_layer_object_bylayer.Click += new System.EventHandler(this.btn_object_layer_object_bylayer_Click);
            // 
            // btn_all_hide
            // 
            this.btn_all_hide.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn_all_hide.Location = new System.Drawing.Point(0, 120);
            this.btn_all_hide.Margin = new System.Windows.Forms.Padding(4);
            this.btn_all_hide.Name = "btn_all_hide";
            this.btn_all_hide.Size = new System.Drawing.Size(122, 25);
            this.btn_all_hide.TabIndex = 33;
            this.btn_all_hide.Text = "所有隐藏";
            this.btn_all_hide.UseVisualStyleBackColor = true;
            this.btn_all_hide.Click += new System.EventHandler(this.btn_layer_all_hide_Click);
            // 
            // rd_btn_7
            // 
            this.rd_btn_7.AutoSize = true;
            this.rd_btn_7.Location = new System.Drawing.Point(13, 87);
            this.rd_btn_7.Name = "rd_btn_7";
            this.rd_btn_7.Size = new System.Drawing.Size(47, 16);
            this.rd_btn_7.TabIndex = 34;
            this.rd_btn_7.TabStop = true;
            this.rd_btn_7.Text = "白色";
            this.rd_btn_7.UseVisualStyleBackColor = true;
            // 
            // rdBtn_251
            // 
            this.rdBtn_251.AutoSize = true;
            this.rdBtn_251.Location = new System.Drawing.Point(75, 87);
            this.rdBtn_251.Name = "rdBtn_251";
            this.rdBtn_251.Size = new System.Drawing.Size(41, 16);
            this.rdBtn_251.TabIndex = 35;
            this.rdBtn_251.TabStop = true;
            this.rdBtn_251.Text = "251";
            this.rdBtn_251.UseVisualStyleBackColor = true;
            // 
            // rdBtn_1
            // 
            this.rdBtn_1.AutoSize = true;
            this.rdBtn_1.Location = new System.Drawing.Point(135, 87);
            this.rdBtn_1.Name = "rdBtn_1";
            this.rdBtn_1.Size = new System.Drawing.Size(47, 16);
            this.rdBtn_1.TabIndex = 36;
            this.rdBtn_1.TabStop = true;
            this.rdBtn_1.Text = "红色";
            this.rdBtn_1.UseVisualStyleBackColor = true;
            // 
            // rdBtn_2
            // 
            this.rdBtn_2.AutoSize = true;
            this.rdBtn_2.Location = new System.Drawing.Point(199, 87);
            this.rdBtn_2.Name = "rdBtn_2";
            this.rdBtn_2.Size = new System.Drawing.Size(47, 16);
            this.rdBtn_2.TabIndex = 37;
            this.rdBtn_2.TabStop = true;
            this.rdBtn_2.Text = "黄色";
            this.rdBtn_2.UseVisualStyleBackColor = true;
            // 
            // rdBtn_3
            // 
            this.rdBtn_3.AutoSize = true;
            this.rdBtn_3.Location = new System.Drawing.Point(13, 109);
            this.rdBtn_3.Name = "rdBtn_3";
            this.rdBtn_3.Size = new System.Drawing.Size(47, 16);
            this.rdBtn_3.TabIndex = 38;
            this.rdBtn_3.TabStop = true;
            this.rdBtn_3.Text = "绿色";
            this.rdBtn_3.UseVisualStyleBackColor = true;
            // 
            // rdBtn_6
            // 
            this.rdBtn_6.AutoSize = true;
            this.rdBtn_6.Location = new System.Drawing.Point(199, 112);
            this.rdBtn_6.Name = "rdBtn_6";
            this.rdBtn_6.Size = new System.Drawing.Size(47, 16);
            this.rdBtn_6.TabIndex = 41;
            this.rdBtn_6.TabStop = true;
            this.rdBtn_6.Text = "洋红";
            this.rdBtn_6.UseVisualStyleBackColor = true;
            // 
            // rdBtn_5
            // 
            this.rdBtn_5.AutoSize = true;
            this.rdBtn_5.Location = new System.Drawing.Point(135, 112);
            this.rdBtn_5.Name = "rdBtn_5";
            this.rdBtn_5.Size = new System.Drawing.Size(47, 16);
            this.rdBtn_5.TabIndex = 40;
            this.rdBtn_5.TabStop = true;
            this.rdBtn_5.Text = "蓝色";
            this.rdBtn_5.UseVisualStyleBackColor = true;
            // 
            // rdBtn_4
            // 
            this.rdBtn_4.AutoSize = true;
            this.rdBtn_4.Location = new System.Drawing.Point(77, 112);
            this.rdBtn_4.Name = "rdBtn_4";
            this.rdBtn_4.Size = new System.Drawing.Size(47, 16);
            this.rdBtn_4.TabIndex = 39;
            this.rdBtn_4.TabStop = true;
            this.rdBtn_4.Text = "青色";
            this.rdBtn_4.UseVisualStyleBackColor = true;
            // 
            // rd_btn_Index
            // 
            this.rd_btn_Index.AutoSize = true;
            this.rd_btn_Index.Location = new System.Drawing.Point(13, 134);
            this.rd_btn_Index.Name = "rd_btn_Index";
            this.rd_btn_Index.Size = new System.Drawing.Size(14, 13);
            this.rd_btn_Index.TabIndex = 42;
            this.rd_btn_Index.TabStop = true;
            this.rd_btn_Index.UseVisualStyleBackColor = true;
            // 
            // btn_object_layer_color_object_show
            // 
            this.btn_object_layer_color_object_show.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn_object_layer_color_object_show.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_object_layer_color_object_show.Location = new System.Drawing.Point(130, 120);
            this.btn_object_layer_color_object_show.Margin = new System.Windows.Forms.Padding(4);
            this.btn_object_layer_color_object_show.Name = "btn_object_layer_color_object_show";
            this.btn_object_layer_color_object_show.Size = new System.Drawing.Size(146, 25);
            this.btn_object_layer_color_object_show.TabIndex = 43;
            this.btn_object_layer_color_object_show.Text = "对象(层+颜色)对象显示";
            this.btn_object_layer_color_object_show.UseVisualStyleBackColor = true;
            this.btn_object_layer_color_object_show.Click += new System.EventHandler(this.btn_object_layer_color_object_show_Click);
            // 
            // btn_all_object_show
            // 
            this.btn_all_object_show.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn_all_object_show.Location = new System.Drawing.Point(130, 87);
            this.btn_all_object_show.Margin = new System.Windows.Forms.Padding(4);
            this.btn_all_object_show.Name = "btn_all_object_show";
            this.btn_all_object_show.Size = new System.Drawing.Size(146, 25);
            this.btn_all_object_show.TabIndex = 44;
            this.btn_all_object_show.Text = "所有物体可见";
            this.btn_all_object_show.UseVisualStyleBackColor = true;
            this.btn_all_object_show.Click += new System.EventHandler(this.btn_all_object_show_Click_1);
            // 
            // btn_object_color_layer_objects
            // 
            this.btn_object_color_layer_objects.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn_object_color_layer_objects.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_object_color_layer_objects.Location = new System.Drawing.Point(130, 32);
            this.btn_object_color_layer_objects.Margin = new System.Windows.Forms.Padding(0);
            this.btn_object_color_layer_objects.Name = "btn_object_color_layer_objects";
            this.btn_object_color_layer_objects.Size = new System.Drawing.Size(146, 25);
            this.btn_object_color_layer_objects.TabIndex = 45;
            this.btn_object_color_layer_objects.Text = "对象(层+颜色)选择对象";
            this.btn_object_color_layer_objects.UseVisualStyleBackColor = true;
            this.btn_object_color_layer_objects.Click += new System.EventHandler(this.btn_object_color_layer_objects_Click);
            // 
            // gbx_Color
            // 
            this.gbx_Color.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.gbx_Color.Controls.Add(this.btn_object_layer_object_Random_Color);
            this.gbx_Color.Controls.Add(this.btn_object_layer_Random_Color);
            this.gbx_Color.Controls.Add(this.btn_all_object_Random_Color);
            this.gbx_Color.Controls.Add(this.btn_all_Random_Color);
            this.gbx_Color.Controls.Add(this.btn_object_layer_object_color_1);
            this.gbx_Color.Controls.Add(this.btn_color_color);
            this.gbx_Color.Controls.Add(this.btn_object_layer_color);
            this.gbx_Color.Controls.Add(this.btn_layer_all_color);
            this.gbx_Color.Controls.Add(this.rd_btn_Index);
            this.gbx_Color.Controls.Add(this.btn_object_layer_object_color);
            this.gbx_Color.Controls.Add(this.rdBtn_6);
            this.gbx_Color.Controls.Add(this.btn_all_layer_object_color);
            this.gbx_Color.Controls.Add(this.rdBtn_5);
            this.gbx_Color.Controls.Add(this.btn_color_bylayer);
            this.gbx_Color.Controls.Add(this.rdBtn_4);
            this.gbx_Color.Controls.Add(this.btn_object_layer_object_bylayer);
            this.gbx_Color.Controls.Add(this.rdBtn_3);
            this.gbx_Color.Controls.Add(this.btn_object_layer_object_byblock);
            this.gbx_Color.Controls.Add(this.rdBtn_2);
            this.gbx_Color.Controls.Add(this.btn_color_byblock);
            this.gbx_Color.Controls.Add(this.rdBtn_1);
            this.gbx_Color.Controls.Add(this.rd_btn_7);
            this.gbx_Color.Controls.Add(this.rdBtn_251);
            this.gbx_Color.Controls.Add(this.tbx_color_index);
            this.gbx_Color.Location = new System.Drawing.Point(4, 312);
            this.gbx_Color.Name = "gbx_Color";
            this.gbx_Color.Size = new System.Drawing.Size(285, 345);
            this.gbx_Color.TabIndex = 46;
            this.gbx_Color.TabStop = false;
            this.gbx_Color.Text = "颜色功能";
            // 
            // btn_object_layer_object_Random_Color
            // 
            this.btn_object_layer_object_Random_Color.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn_object_layer_object_Random_Color.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_object_layer_object_Random_Color.Location = new System.Drawing.Point(137, 278);
            this.btn_object_layer_object_Random_Color.Margin = new System.Windows.Forms.Padding(4);
            this.btn_object_layer_object_Random_Color.Name = "btn_object_layer_object_Random_Color";
            this.btn_object_layer_object_Random_Color.Size = new System.Drawing.Size(139, 25);
            this.btn_object_layer_object_Random_Color.TabIndex = 46;
            this.btn_object_layer_object_Random_Color.Tag = "";
            this.btn_object_layer_object_Random_Color.Text = "对象层对象随机颜色";
            this.btn_object_layer_object_Random_Color.UseVisualStyleBackColor = true;
            this.btn_object_layer_object_Random_Color.Click += new System.EventHandler(this.btn_object_layer_object_Random_Color_Click);
            // 
            // btn_object_layer_Random_Color
            // 
            this.btn_object_layer_Random_Color.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn_object_layer_Random_Color.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_object_layer_Random_Color.Location = new System.Drawing.Point(138, 311);
            this.btn_object_layer_Random_Color.Margin = new System.Windows.Forms.Padding(4);
            this.btn_object_layer_Random_Color.Name = "btn_object_layer_Random_Color";
            this.btn_object_layer_Random_Color.Size = new System.Drawing.Size(138, 25);
            this.btn_object_layer_Random_Color.TabIndex = 45;
            this.btn_object_layer_Random_Color.Tag = "";
            this.btn_object_layer_Random_Color.Text = "对象层随机颜色";
            this.btn_object_layer_Random_Color.UseVisualStyleBackColor = true;
            this.btn_object_layer_Random_Color.Click += new System.EventHandler(this.btn_object_layer_Random_Color_Click);
            // 
            // btn_all_object_Random_Color
            // 
            this.btn_all_object_Random_Color.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn_all_object_Random_Color.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_all_object_Random_Color.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_all_object_Random_Color.Location = new System.Drawing.Point(10, 278);
            this.btn_all_object_Random_Color.Margin = new System.Windows.Forms.Padding(4);
            this.btn_all_object_Random_Color.Name = "btn_all_object_Random_Color";
            this.btn_all_object_Random_Color.Size = new System.Drawing.Size(122, 25);
            this.btn_all_object_Random_Color.TabIndex = 44;
            this.btn_all_object_Random_Color.Tag = "";
            this.btn_all_object_Random_Color.Text = "所有对象随机颜色";
            this.btn_all_object_Random_Color.UseVisualStyleBackColor = true;
            this.btn_all_object_Random_Color.Click += new System.EventHandler(this.btn_all_object_Random_Color_Click);
            // 
            // btn_all_Random_Color
            // 
            this.btn_all_Random_Color.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn_all_Random_Color.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_all_Random_Color.Location = new System.Drawing.Point(8, 311);
            this.btn_all_Random_Color.Margin = new System.Windows.Forms.Padding(4);
            this.btn_all_Random_Color.Name = "btn_all_Random_Color";
            this.btn_all_Random_Color.Size = new System.Drawing.Size(122, 25);
            this.btn_all_Random_Color.TabIndex = 43;
            this.btn_all_Random_Color.Tag = "";
            this.btn_all_Random_Color.Text = "所有层随机颜色";
            this.btn_all_Random_Color.UseVisualStyleBackColor = true;
            this.btn_all_Random_Color.Click += new System.EventHandler(this.btn_all_Random_Color_Click);
            // 
            // btn_object_layer_object_color_1
            // 
            this.btn_object_layer_object_color_1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn_object_layer_object_color_1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_object_layer_object_color_1.Location = new System.Drawing.Point(137, 237);
            this.btn_object_layer_object_color_1.Margin = new System.Windows.Forms.Padding(4);
            this.btn_object_layer_object_color_1.Name = "btn_object_layer_object_color_1";
            this.btn_object_layer_object_color_1.Size = new System.Drawing.Size(139, 25);
            this.btn_object_layer_object_color_1.TabIndex = 29;
            this.btn_object_layer_object_color_1.Tag = "";
            this.btn_object_layer_object_color_1.Text = "对象层所有对象颜色";
            this.btn_object_layer_object_color_1.UseVisualStyleBackColor = true;
            this.btn_object_layer_object_color_1.Click += new System.EventHandler(this.btn_object_layer_object_color_1_Click);
            // 
            // btn_color_color
            // 
            this.btn_color_color.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn_color_color.Location = new System.Drawing.Point(7, 237);
            this.btn_color_color.Margin = new System.Windows.Forms.Padding(4);
            this.btn_color_color.Name = "btn_color_color";
            this.btn_color_color.Size = new System.Drawing.Size(122, 25);
            this.btn_color_color.TabIndex = 27;
            this.btn_color_color.Tag = "";
            this.btn_color_color.Text = "所有对象颜色";
            this.btn_color_color.UseVisualStyleBackColor = true;
            this.btn_color_color.Click += new System.EventHandler(this.btn_color_color_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.groupBox1.Controls.Add(this.btn_layer_show);
            this.groupBox1.Controls.Add(this.btn_layer_hide);
            this.groupBox1.Controls.Add(this.btn_object_layer_object_show);
            this.groupBox1.Controls.Add(this.btn_all_object_show);
            this.groupBox1.Controls.Add(this.btn_object_hide);
            this.groupBox1.Controls.Add(this.btn_object_layer_color_object_show);
            this.groupBox1.Controls.Add(this.btn_all_show);
            this.groupBox1.Controls.Add(this.btn_all_hide);
            this.groupBox1.Location = new System.Drawing.Point(4, 66);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(285, 165);
            this.groupBox1.TabIndex = 47;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "显示功能";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.groupBox2.Controls.Add(this.btn_object_color_layer_objects);
            this.groupBox2.Controls.Add(this.btn_layer_select_all);
            this.groupBox2.Location = new System.Drawing.Point(4, 237);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(285, 70);
            this.groupBox2.TabIndex = 48;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "选择功能";
            // 
            // btn_Create_zjy_layer
            // 
            this.btn_Create_zjy_layer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn_Create_zjy_layer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btn_Create_zjy_layer.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Create_zjy_layer.Location = new System.Drawing.Point(17, 673);
            this.btn_Create_zjy_layer.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Create_zjy_layer.Name = "btn_Create_zjy_layer";
            this.btn_Create_zjy_layer.Size = new System.Drawing.Size(251, 46);
            this.btn_Create_zjy_layer.TabIndex = 47;
            this.btn_Create_zjy_layer.Tag = "";
            this.btn_Create_zjy_layer.Text = "创建 自动打印边框图层 和 项目名称层";
            this.btn_Create_zjy_layer.UseVisualStyleBackColor = false;
            this.btn_Create_zjy_layer.Click += new System.EventHandler(this.btn_Create_zjy_layer_Click);
            // 
            // User_Control_fuzhu
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.btn_Create_zjy_layer);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbx_Color);
            this.Controls.Add(this.lbl_fuzhu);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "User_Control_fuzhu";
            this.Size = new System.Drawing.Size(295, 738);
            this.gbx_Color.ResumeLayout(false);
            this.gbx_Color.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbl_fuzhu;
        private System.Windows.Forms.Button btn_layer_show;
        private System.Windows.Forms.Button btn_layer_hide;
        private System.Windows.Forms.Button btn_layer_select_all;
        private System.Windows.Forms.Button btn_object_layer_object_show;
        private System.Windows.Forms.Button btn_object_hide;
        private System.Windows.Forms.Button btn_all_show;
        private System.Windows.Forms.Button btn_object_layer_color;
        private System.Windows.Forms.Button btn_object_layer_object_color;
        private System.Windows.Forms.Button btn_layer_all_color;
        private System.Windows.Forms.Button btn_all_layer_object_color;
        private System.Windows.Forms.TextBox tbx_color_index;
        private System.Windows.Forms.Button btn_color_bylayer;
        private System.Windows.Forms.Button btn_color_byblock;
        private System.Windows.Forms.Button btn_object_layer_object_byblock;
        private System.Windows.Forms.Button btn_object_layer_object_bylayer;
        private System.Windows.Forms.Button btn_all_hide;
        private System.Windows.Forms.RadioButton rd_btn_7;
        private System.Windows.Forms.RadioButton rdBtn_251;
        private System.Windows.Forms.RadioButton rdBtn_1;
        private System.Windows.Forms.RadioButton rdBtn_2;
        private System.Windows.Forms.RadioButton rdBtn_3;
        private System.Windows.Forms.RadioButton rdBtn_6;
        private System.Windows.Forms.RadioButton rdBtn_5;
        private System.Windows.Forms.RadioButton rdBtn_4;
        private System.Windows.Forms.RadioButton rd_btn_Index;
        private System.Windows.Forms.Button btn_object_layer_color_object_show;
        private System.Windows.Forms.Button btn_all_object_show;
        private System.Windows.Forms.Button btn_object_color_layer_objects;
        private System.Windows.Forms.GroupBox gbx_Color;
        private System.Windows.Forms.Button btn_object_layer_object_color_1;
        private System.Windows.Forms.Button btn_color_color;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_object_layer_object_Random_Color;
        private System.Windows.Forms.Button btn_object_layer_Random_Color;
        private System.Windows.Forms.Button btn_all_object_Random_Color;
        private System.Windows.Forms.Button btn_all_Random_Color;
        private System.Windows.Forms.Button btn_Create_zjy_layer;
    }
}
