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
    public partial class UserControl_batch_Plot : UserControl
    {
        public UserControl_batch_Plot()
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

        private void btn_layout_pingmian_plot_Click(object sender, EventArgs e)
        {
            int direct = 0;
            if(rdbtn_00.Checked==true)
                {
                direct = 0;
            }else if(rdbtn_90.Checked==true)
            {
                direct = 90;
            }
            zjyCAD.LayoutPMT_Plot(cbx_plot_device.Text.Trim(), direct);
            MessageBox.Show("完成");

        }

        private void btn_model_plot_Click(object sender, EventArgs e)
        {
            int direct = 0;
            if (rdbtn_00.Checked == true)
            {
                direct = 0;
            }
            else if (rdbtn_90.Checked == true)
            {
                direct = 90;
            }
            zjyCAD.Model_Plot(cbx_plot_device.Text.Trim(), direct);
            MessageBox.Show("完成");
        }

        private void btn_file_cad_plot_Click(object sender, EventArgs e)
        {
            int direct = 0;
            if (rdbtn_00.Checked == true)
            {
                direct = 0;
            }
            else if (rdbtn_90.Checked == true)
            {
                direct = 90;
            }
            zjyCAD.zjyCADFilePrint(tbx_SavePath.Text.Trim(),cbx_plot_device.Text.Trim(), direct);

            MessageBox.Show("完成");


        }

        private void btn_file_cad_excel_word_plot_Click(object sender, EventArgs e)
        {
            int direct = 0;
            if (rdbtn_00.Checked == true)
            {
                direct = 0;
            }
            else if (rdbtn_90.Checked == true)
            {
                direct = 90;
            }

           
            zjyCAD.zjyCAD_Word_Excel_FilePrint(tbx_SavePath.Text.Trim(), cbx_plot_device.Text.Trim(), direct,rdb_excel_endpage_1.Checked);
            MessageBox.Show("完成");
        }

        private void btn_print_excel_word_Click(object sender, EventArgs e)
        {
            int direct = 0;
            if (rdbtn_00.Checked == true)
            {
                direct = 0;
            }
            else if (rdbtn_90.Checked == true)
            {
                direct = 90;
            }


            zjyCAD.zjy_Word_Excel_FilePrint(tbx_SavePath.Text.Trim(), cbx_plot_device.Text.Trim(), direct, rdb_excel_endpage_1.Checked);
            MessageBox.Show("完成");
        }





    }
}
