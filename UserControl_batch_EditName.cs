using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using zjy;

namespace zjy_CAD
{
    public partial class UserControl_batch_EditName : UserControl
    {
        public UserControl_batch_EditName()
        {
            InitializeComponent();
        }

        //int _77 = 09;

        private void btn_file_Path_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbPath = new FolderBrowserDialog();
            fbPath.ShowDialog();
            if (fbPath.SelectedPath == "") return;
            tbx_SavePath.Text = fbPath.SelectedPath;
        }





        private void btn_changeName_dwg_Click(object sender, EventArgs e)
        {
            zjyCAD.zjy_Change_Project_Name_dwgwordexcel(tbx_SavePath.Text.Trim(), tbx_old_projectName.Text.Trim(), tbx_new_projectName.Text.Trim(), 0);
            MessageBox.Show("完成");
        }

        private void btn_changeName_all_Click(object sender, EventArgs e)
        {
            zjyCAD.zjy_Change_Project_Name_dwgwordexcel(tbx_SavePath.Text.Trim(), tbx_old_projectName.Text.Trim(), tbx_new_projectName.Text.Trim(), 2);
            MessageBox.Show("完成");
        }

        private void btn_changeName_wordexcel_Click(object sender, EventArgs e)
        {
            zjyCAD.zjy_Change_Project_Name_dwgwordexcel(tbx_SavePath.Text.Trim(), tbx_old_projectName.Text.Trim(), tbx_new_projectName.Text.Trim(), 1);
            MessageBox.Show("完成");
        }

        private void btn_open_dwg_Click(object sender, EventArgs e)
        {
            zjyCAD.zjy_Change_Project_Name_open_dwg( tbx_new_projectName.Text.Trim());
            MessageBox.Show("完成");
        }
    }
}
