using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using cad_app=Autodesk.AutoCAD.ApplicationServices;


namespace zjy_cad_chajian
{
    public partial class User_Control_Sheji : UserControl
    {
        public User_Control_Sheji()
        {
            InitializeComponent();
        }


        zjy_cad_function zjy_cad_fun = new zjy_cad_function(); 

        private void btn_jdpm_Click(object sender, EventArgs e)
        {

            zjy_cad_fun.cad_command_document("jdpm ");
        }

        private void btn_xiaodao_Click(object sender, EventArgs e)
        {

            zjy_cad_fun.cad_command_document("HWIZARD ");
        }

        private void btn_shumo_shuju_Click(object sender, EventArgs e)
        {

            zjy_cad_fun.cad_command_document("HDTM_IMPORT ");
        }

        private void btn_sanjiaogouwang_Click(object sender, EventArgs e)
        {
            zjy_cad_fun.cad_command_document("hdtm_build_net ");

            zjy_cad_fun.cad_command_document("HDTM_show ");


        }

        private void btn_sm_input_Click(object sender, EventArgs e)
        {

            zjy_cad_fun.cad_command_document("HDTM_open ");
        }

        private void btn_sm_zdm_Click(object sender, EventArgs e)
        {

            zjy_cad_fun.cad_command_document("HDTM_GETDMX ");
        }

        private void btn_sm_hdm_Click(object sender, EventArgs e)
        {

            zjy_cad_fun.cad_command_document("HDTM_GETHDM ");
        }

        private void btm_sj_zdm_Click(object sender, EventArgs e)
        {
            zjy_cad_fun.cad_command_document("zdmsj ");
        }

        private void btn_ljsj_Click(object sender, EventArgs e)
        {
            zjy_cad_fun.cad_command_document("ljsj ");
        }

        private void btn_hdm_ht_Click(object sender, EventArgs e)
        {
            zjy_cad_fun.cad_command_document("HDM_NEW ");
        }

        private void bt_sm_save_Click(object sender, EventArgs e)
        {
            zjy_cad_fun.cad_command_document("HDTM_SAVE ");
        }

        private void btn_road_del_Click(object sender, EventArgs e)
        {

            zjy_cad_fun.cad_command_document("DEL_ROAD ");
        }

        private void btn_control_canshu_Click(object sender, EventArgs e)
        {
            zjy_cad_fun.cad_command_document("CTREDIT ");
        }

    }
}
