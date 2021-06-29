using Autodesk.AutoCAD.EditorInput;
using System;
using System.Windows.Forms;
using cadSer = Autodesk.AutoCAD.ApplicationServices;
using zjy;

namespace zjy_cad_chajian
{
    public partial class User_Control_fuzhu : UserControl
    { 
        public User_Control_fuzhu()
        {
            InitializeComponent();
            rd_btn_7.Checked = true;

        }

       //readonly Editor ed = zjy_cad_function.ed;

      readonly zjy_cad_function zjy_cad_fun = new zjy_cad_function();

        private short color_index()
        {
            short ii=7;
            if (rd_btn_7.Checked == true) { ii = 7; }
            else if (rdBtn_251.Checked == true) { ii = 251; }
            else if (rdBtn_1.Checked == true) { ii = 1; }
            else if (rdBtn_2.Checked == true) { ii = 2; }
            else if (rdBtn_3.Checked == true) { ii = 3; }
            else if (rdBtn_4.Checked == true) { ii = 4; }
            else if (rdBtn_5.Checked == true) { ii = 5; }
            else if (rdBtn_6.Checked == true) { ii = 6; }
            else if (rd_btn_Index.Checked == true) { ii = (short)Convert.ToInt32(tbx_color_index.Text.Trim()); }
           
            return ii;
        }



        private void btn_layer_show_Click(object sender, EventArgs e)
        {
           // Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
            zjy_cad_fun.SetFocus();
            //zjy_cad_fun.Object_All_Layer_show_hide(true);
            zjy_cad_fun.Object_Layer_show_hide(false);
           // zjy_cad_fun.Change_All_entity_Show_Hide(true);
           // zjy_cad_fun.Object_Layer_show_hide(false);
            //  ed.SwitchToModelSpace();
            // ed.TurnForcedPickOn();
            //cadSer.Application.MainWindow.
            //ed.UpdateScreen();
            //zjy_cad_fun.SetFocus();
        }

        private void btn_layer_hide_Click(object sender, EventArgs e)
        {
            zjy_cad_fun.SetFocus();
            zjy_cad_fun.Object_Layer_show_hide(true);
            //  ed.UpdateScreen();

        }

        private void btn_layer_all_show_Click(object sender, EventArgs e)
        {
            
            zjy_cad_fun.SetFocus();
            zjy_cad_fun.Change_All_entity_Show_Hide(true);
            zjy_cad_fun.Object_All_Layer_show_hide(false);
            Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
            ed.UpdateScreen();

        }

        private void btn_layer_select_all_Click(object sender, EventArgs e)
        {
            zjy_cad_fun.SetFocus();
            zjy_cad_fun.SetSectionSet();
            Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
            ed.UpdateScreen();
            //Dispose();
        }

        private void btn_object_show_Click(object sender, EventArgs e)
        {
            zjy_cad_fun.SetFocus();
            zjy_cad_fun.Change_entity_Show_Hide(true);
            Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
            ed.UpdateScreen();

        }

        private void btn_object_hide_Click(object sender, EventArgs e)
        {
            zjy_cad_fun.SetFocus();
            zjy_cad_fun.Change_entity_Show_Hide(false);
        }

        private void btn_all_object_show_Click(object sender, EventArgs e)
        {
            zjy_cad_fun.SetFocus();
            zjy_cad_fun.Change_All_entity_Show_Hide(true);
            Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
            ed.UpdateScreen();
        }

        private void btn_all_object_hide_Click(object sender, EventArgs e)
        {
            zjy_cad_fun.SetFocus();
            zjy_cad_fun.Change_All_entity_Show_Hide(false);
            Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
            ed.UpdateScreen();
        }

        private void btn_layer_all_hide_Click(object sender, EventArgs e)
        {
            zjy_cad_fun.SetFocus();
            zjy_cad_fun.Change_All_entity_Show_Hide(true);
            zjy_cad_fun.Object_All_Layer_show_hide(true);
            Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
            ed.UpdateScreen();
        }

        private void btn_color_bylayer_Click(object sender, EventArgs e)
        {
            zjy_cad_fun.SetFocus();
            zjy_cad_fun.Change_all_Color(256);
        }

        private void btn_color_byblock_Click(object sender, EventArgs e)
        {
            zjy_cad_fun.SetFocus();
            zjy_cad_fun.Change_all_Color(0);
        }

        private void btn_object_layer_object_bylayer_Click(object sender, EventArgs e)
        {
            zjy_cad_fun.SetFocus();
            zjy_cad_fun.Change_entity_Color(256);
            //Change_entity_Color
        }

        private void btn_object_layer_object_byblock_Click(object sender, EventArgs e)
        {
            zjy_cad_fun.SetFocus();
            zjy_cad_fun.Change_entity_Color(0);
        }

        private void btn_object_layer_color_Click(object sender, EventArgs e)
        {
            zjy_cad_fun.SetFocus();
            zjy_cad_fun.Change_Layer_Color(color_index());
           // Change_Layer_Color
        }

        private void btn_layer_all_color_Click(object sender, EventArgs e)
        {
            zjy_cad_fun.SetFocus();
            zjy_cad_fun.Change_All_layer_Color(color_index());
            Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
            ed.UpdateScreen();
            // Change_All_layer_Color
        }

        private void btn_object_layer_object_color_Click(object sender, EventArgs e)
        {
            zjy_cad_fun.SetFocus();
            zjy_cad_fun.Change_entity_Color(color_index());
            //Change_entity_Color
        }

        private void btn_all_layer_object_color_Click(object sender, EventArgs e)
        {
            zjy_cad_fun.SetFocus();
            zjy_cad_fun.Change_all_Color(color_index());
            Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
            ed.UpdateScreen();
        }

        private void btn_object_layer_color_object_show_Click(object sender, EventArgs e)
        {
            zjy_cad_fun.SetFocus();
            zjy_cad_fun.Change_entity_Show_Hide((Int16)2,true);
            Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
            ed.UpdateScreen();
        }

        private void btn_all_object_show_Click_1(object sender, EventArgs e)
        {
            zjy_cad_fun.Change_All_entity_Show_Hide(true);
            Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
            ed.UpdateScreen();
        }

        private void btn_object_color_layer_objects_Click(object sender, EventArgs e)
        {
            zjy_cad_fun.SetFocus();
            zjy_cad_fun.SetSectionSet((Int16)1);
            //Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.UpdateScreen();
        }

        private void btn_color_color_Click(object sender, EventArgs e)
        {


            zjy_cad_fun.Change_all_To_Color();
            Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
            ed.UpdateScreen();

        }

        private void btn_object_layer_object_color_1_Click(object sender, EventArgs e)
        {
            zjy_cad_fun.Change_object_layer_all_To_Color();
            Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
            ed.UpdateScreen();

        }


        //private void button4_Click(object sender, EventArgs e)
        //{
        //   // Change_all_object_randomColor

        //        zjy_cad_fun.Change_all_object_randomColor();
        //    Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
        //    ed.UpdateScreen();
        //}

        private void btn_object_layer_object_Random_Color_Click(object sender, EventArgs e)
        {


            zjy_cad_fun.Change_selct_layer_object_randomColor();
            Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
            ed.UpdateScreen();
        }

        private void btn_all_object_Random_Color_Click(object sender, EventArgs e)
        {
            //   // Change_all_object_randomColor

            zjy_cad_fun.Change_all_object_randomColor();
            Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
            ed.UpdateScreen();
        }

        private void btn_all_Random_Color_Click(object sender, EventArgs e)
        {

            zjy_cad_fun.Change_All_Layer_RandomColor();
            Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
            ed.UpdateScreen();
        }

        private void btn_object_layer_Random_Color_Click(object sender, EventArgs e)
        {
            zjy_cad_fun.Change_object_Layer_RandomColor();
            Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
            ed.UpdateScreen();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_Create_zjy_layer_Click(object sender, EventArgs e)
        {
            zjyCAD.zCreatLayer("zjy_项目名称",true);
            zjyCAD.zCreatLayer("zjyPrint",false);
        }

  
    }
}
