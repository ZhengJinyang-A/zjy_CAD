using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.EditorInput;
using cadSer = Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using zjy;
using Autodesk.AutoCAD.GraphicsInterface;
using Polyline = Autodesk.AutoCAD.DatabaseServices.Polyline;

namespace zjy_CAD
{
    public partial class zjy_hdm : UserControl
    {
        public zjy_hdm()
        {
            InitializeComponent();
        }

        private void cbx_DL_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_DL.Checked == true)
            {
                rtbx_DL.Enabled = true;
            }
            else
            {
                rtbx_DL.Enabled = false;

            }
        }

        private void btn_Create_stackSequence_Click(object sender, EventArgs e)
        {
            double road_length = Convert.ToDouble(tbx_Road_Length.Text.Trim());
            double jianju = Convert.ToDouble(tbx_SamplingJianJU.Text.Trim());
            List<string> stackList = new List<string>();

            for(double i=0;i<road_length;i+=jianju)
            {
                stackList.Add(Math.Round(i,3).ToString());
            }
            stackList.Add(road_length.ToString());
            rtbx_stack_sequence.Lines = stackList.ToArray();
        }

        private void btn_KMLPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbPath = new FolderBrowserDialog();
            fbPath.ShowDialog();
            if (fbPath.SelectedPath == "") return;
            tbx_SavePath.Text = fbPath.SelectedPath;
        }

        private void btn_adjust_road_start_Click(object sender, EventArgs e)
        {
            Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;


            Database db = HostApplicationServices.WorkingDatabase;
            var middoc = cadSer.Application.DocumentManager.MdiActiveDocument;


            ObjectId plineID = ed.GetEntity(new PromptEntityOptions("\n选取多段线!!!")).ObjectId;
            var doc_lock = middoc.LockDocument();

            DBObject dbo;
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                dbo = trans.GetObject(plineID, OpenMode.ForWrite);

                while (!dbo.GetType().Equals(typeof(Polyline)))
                {
                    ed.WriteMessage("\n刚才选取的不是Pline,请重新选择!!!");
                    plineID = ed.GetEntity(new PromptEntityOptions("\n选取多段线!!!")).ObjectId;
                    dbo = trans.GetObject(plineID, OpenMode.ForWrite);
                }

          

                Point3d  poindstart= ed.GetPoint(new PromptPointOptions("\n选取起点")).Value;

                Polyline polyline = (Polyline)dbo;

                Vector3d tmp_vector = polyline.StartPoint.GetAsVector()- poindstart.GetAsVector();
                tmp_vector = new Vector3d(tmp_vector.X,tmp_vector.Y,0);

                if(tmp_vector.Length>1.0)
                {
                    polyline.ReverseCurve();
                    zjyCAD.ToModelSpace(polyline, db);
                }

             


                trans.Commit();
            }
            doc_lock.Dispose();
        }

        private void btn_CreateHDM_Pline3D_Click(object sender, EventArgs e)
        {
            //断链数据
            List<KeyValuePair<double, double>> dLList = new List<KeyValuePair<double, double>>();
            if (cbx_DL.Checked == true)
            {
                string[] dlStrArr = rtbx_DL.Lines;
                if (dlStrArr.Length > 0)
                {
                    foreach (string dlStr in dlStrArr)
                    {
                        if (dlStr != "")
                        {
                            string[] tmp = dlStr.Split(new char[] { '=', ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                            dLList.Add(new KeyValuePair<double, double>(Convert.ToDouble(tmp[1]), Convert.ToDouble(tmp[2])));
                        }
                    }
                }
            }



            double err_qiexianglimit = Convert.ToDouble(tbx_LuxianDirection_delta.Text.Trim());
            double err_normallimit = Convert.ToDouble(tbx_HDM_caiji_limit.Text.Trim());
           
            List<double> stackList = new List<double>();
            string[] startArr = rtbx_stack_sequence.Lines;


            foreach(string tmp in startArr)
            {
                if(tmp.Trim()!="")
                {
                    if (dLList.Count > 0)
                    {
                        stackList.Add(zjyCAD.StackFromDLStack(tmp.Trim(),dLList));
                    }
                    else
                    {
                        stackList.Add(Convert.ToDouble(tmp.Trim()));
                    }
                }
            }

            if (stackList.Count < 1) return;
           zjyCAD. CreateHDMPline(err_qiexianglimit,err_normallimit,stackList);
        }

        private void btn_Create_dmx_hdm_file_Click(object sender, EventArgs e)
        {
            //断链数据
            List<KeyValuePair<double, double>> dLList = new List<KeyValuePair<double, double>>();
            if (cbx_DL.Checked == true)
            {
                string[] dlStrArr = rtbx_DL.Lines;
                if (dlStrArr.Length > 0)
                {
                    foreach (string dlStr in dlStrArr)
                    {
                        if (dlStr != "")
                        {
                            string[] tmp = dlStr.Split(new char[] { '=', ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                            dLList.Add(new KeyValuePair<double, double>(Convert.ToDouble(tmp[1]), Convert.ToDouble(tmp[2])));
                        }
                    }
                }
            }

            string path = tbx_SavePath.Text.Trim();
            string filename = tbx_fileName.Text.Trim();
            double startstack = Convert.ToDouble(tbx_startstack.Text.Trim());

            zjyCAD.CreadHDM(path, filename,startstack ,dLList);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Database db = HostApplicationServices.WorkingDatabase;
            var middoc = cadSer.Application.DocumentManager.MdiActiveDocument;
            var doc_lock = middoc.LockDocument();
            DBObjectCollection dBObjectCollection = zjyCAD.GetObjects("zjy_hdm_HDM");
            foreach(DBObject tmp in dBObjectCollection)
            {
                zjyCAD.RemoveModelSpace(tmp.ObjectId,db);
            }
            doc_lock.Dispose();
        }

        private void btn_change_pline3D_stack_xdata_Click(object sender, EventArgs e)
        {
            double stack = Convert.ToDouble(tbx_pline3d_xdata_stack.Text.Trim());
            Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;


            Database db = HostApplicationServices.WorkingDatabase;
            var middoc = cadSer.Application.DocumentManager.MdiActiveDocument;


            string appname = "zjy_1_1_1";


            ObjectId plineID = ed.GetEntity(new PromptEntityOptions("\n选取HDM辅助线!!!")).ObjectId;
            var doc_lock = middoc.LockDocument();

            DBObject dbo;
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                dbo = trans.GetObject(plineID, OpenMode.ForWrite);

                while (!dbo.GetType().Equals(typeof(Polyline3d)))
                {
                    ed.WriteMessage("\n刚才选取的不是HDM辅助线,请重新选择!!!");
                    plineID = ed.GetEntity(new PromptEntityOptions("\n选取HDM辅助线!!!")).ObjectId;
                    dbo = trans.GetObject(plineID, OpenMode.ForWrite);
                }

                Polyline3d pline3d_modify = (Polyline3d)dbo;
                pline3d_modify.Layer = "zjy_hdm_HDM";


                if (pline3d_modify.XData != null&&pline3d_modify.XData.AsArray().Length > 0)
                {
                    ed.WriteMessage("\n修改之前的桩号为:"+pline3d_modify.XData.AsArray()[1].Value. ToString() + "\n");
                }
                SortedDictionary<double, Polyline3d> hdm_pline3d_dic = new SortedDictionary<double, Polyline3d>();

                RegAppTable apptb1 = trans.GetObject(db.RegAppTableId, OpenMode.ForWrite) as RegAppTable;
                if (!apptb1.Has(appname))
                {
                    RegAppTableRecord app = new RegAppTableRecord();
                    app.Name = appname;
                    apptb1.Add(app);
                    trans.AddNewlyCreatedDBObject(app, true);

                }



                ResultBuffer resultBuffer = new ResultBuffer();
             
                 resultBuffer.Add(new TypedValue((int)DxfCode.ExtendedDataRegAppName, appname));
                resultBuffer.Add(new TypedValue((int)DxfCode.ExtendedDataReal, stack));
               
                pline3d_modify.XData = resultBuffer;

                zjyCAD.ToModelSpace(pline3d_modify,db);

           trans.Commit();
            }
        }

        private void btn_showstack_ed_Click(object sender, EventArgs e)
        {
            //double stack = Convert.ToDouble(tbx_pline3d_xdata_stack.Text.Trim());

            //断链数据
            List<KeyValuePair<double, double>> dLList = new List<KeyValuePair<double, double>>();
            if (cbx_DL.Checked == true)
            {
                string[] dlStrArr = rtbx_DL.Lines;
                if (dlStrArr.Length > 0)
                {
                    foreach (string dlStr in dlStrArr)
                    {
                        if (dlStr != "")
                        {
                            string[] tmp = dlStr.Split(new char[] { '=', ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                            dLList.Add(new KeyValuePair<double, double>(Convert.ToDouble(tmp[1]), Convert.ToDouble(tmp[2])));
                        }
                    }
                }
            }


                Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;


            Database db = HostApplicationServices.WorkingDatabase;
            var middoc = cadSer.Application.DocumentManager.MdiActiveDocument;




            ObjectId plineID = ed.GetEntity(new PromptEntityOptions("\n选取HDM辅助线!!!")).ObjectId;
            var doc_lock = middoc.LockDocument();

            DBObject dbo;
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                dbo = trans.GetObject(plineID, OpenMode.ForRead);

                while (!dbo.GetType().Equals(typeof(Polyline3d)))
                {
                    ed.WriteMessage("\n刚才选取的不是HDM辅助线,请重新选择!!!");
                    plineID = ed.GetEntity(new PromptEntityOptions("\n选取HDM辅助线!!!")).ObjectId;
                    dbo = trans.GetObject(plineID, OpenMode.ForRead);
                }

                Polyline3d pline3d_modify = (Polyline3d)dbo;
                string stack = pline3d_modify.XData.AsArray()[1].Value.ToString();
                if (pline3d_modify.XData != null && pline3d_modify.XData.AsArray().Length > 0)
                {
                    ed.WriteMessage("\n实际桩号为:" + stack + "\n");
                }
                if (dLList.Count > 0)
                {
                    stack = zjyCAD.DLStackFromStack(Math.Round(Convert.ToDouble(stack), 3) , dLList);
                    if (pline3d_modify.XData != null && pline3d_modify.XData.AsArray().Length > 0)
                    {
                        ed.WriteMessage("\n断链桩号为:" + stack + "\n");
                    }
                }
             
              
                trans.Commit();
            }
        }

        private void btn_delete_all_pline3d_xdata_stack_Click(object sender, EventArgs e)
        {
            Database db = HostApplicationServices.WorkingDatabase;
            var middoc = cadSer.Application.DocumentManager.MdiActiveDocument;
            var doc_lock = middoc.LockDocument();
            DBObjectCollection dBObjectCollection = zjyCAD.GetObjects("zjy_pline3d_stack");
            foreach (DBObject tmp in dBObjectCollection)
            {
                zjyCAD.RemoveModelSpace(tmp.ObjectId, db);
            }
            doc_lock.Dispose();
            
        }

        private void btn_CheckGCD_Click(object sender, EventArgs e)
        {
            Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;


            Database db = HostApplicationServices.WorkingDatabase;
            var middoc = cadSer.Application.DocumentManager.MdiActiveDocument;
    
            var doc_lock = middoc.LockDocument();

       
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
              DBObjectCollection gcdColletion=  zjyCAD.GetObjects("GCD");
          
                foreach (DBObject tmpid in gcdColletion)
                {
                 
                    if (tmpid.GetType().Equals(typeof(BlockReference)))
                    {
                        try
                        {
                            BlockReference dboRefer = (BlockReference)tmpid;

                            foreach (ObjectId var in dboRefer.AttributeCollection)
                            {
                                AttributeReference dboooo = trans.GetObject(var, OpenMode.ForWrite) as AttributeReference;
                                double showtag = Convert.ToDouble(dboooo.TextString.Trim());
                                if (Math.Abs(Math.Round(dboRefer.Position.Z, 3) - Math.Round(showtag, 3)) > 0.02)
                                {
                                    zjyCAD.zCircleCreate(new zPointXY(dboRefer.Position.X, dboRefer.Position.Y), 10, "zjy_gcd_check");
                                }
                            }
                        }catch
                        {
                            ed.WriteMessage("无效块信息\n");
                        }
                   
                    }
                }

                trans.Commit();
            }

            doc_lock.Dispose();


         
        }

        private void btn_checkAndmodify_Click(object sender, EventArgs e)
        {
            Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;


            Database db = HostApplicationServices.WorkingDatabase;
            var middoc = cadSer.Application.DocumentManager.MdiActiveDocument;

            var doc_lock = middoc.LockDocument();

            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                DBObjectCollection gcdColletion = zjyCAD.GetObjects("GCD");

                foreach (DBObject tmpid in gcdColletion)
                {

                    if (tmpid.GetType().Equals(typeof(BlockReference)))
                    {
                        try
                        {
                            BlockReference dboRefer = (BlockReference)tmpid;

                            //  foreach (ObjectId var in dboRefer.AttributeCollection)
                            // {
                            ObjectId var = dboRefer.AttributeCollection[0];
                            AttributeReference dboooo = trans.GetObject(var, OpenMode.ForWrite) as AttributeReference;
                            double showtag = Convert.ToDouble(dboooo.TextString.Trim());
                            if (Math.Abs(Math.Round(dboRefer.Position.Z, 3) - Math.Round(showtag, 3)) > 0.02)
                            {
                                zjyCAD.zCircleCreate(new zPointXY(dboRefer.Position.X, dboRefer.Position.Y), 10, "zjy_gcd_check");

                                dboooo.TextString = Math.Round(dboRefer.Position.Z, 3).ToString();

                            }
                        }catch
                        {
                            ed.WriteMessage("无效块信息\n");
                        }
                        //}

                    }
                }

                trans.Commit();
            }

            doc_lock.Dispose();

        }

        private void btn_Show_all_pline3d_xdata_stack_Click(object sender, EventArgs e)
        {
            //断链数据
            List<KeyValuePair<double, double>> dLList = new List<KeyValuePair<double, double>>();
            if (cbx_DL.Checked == true)
            {
                string[] dlStrArr = rtbx_DL.Lines;
                if (dlStrArr.Length > 0)
                {
                    foreach (string dlStr in dlStrArr)
                    {
                        if (dlStr != "")
                        {
                            string[] tmp = dlStr.Split(new char[] { '=', ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                            dLList.Add(new KeyValuePair<double, double>(Convert.ToDouble(tmp[1]), Convert.ToDouble(tmp[2])));
                        }
                    }
                }
            }



            Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
          Database db = HostApplicationServices.WorkingDatabase;
            var middoc = cadSer.Application.DocumentManager.MdiActiveDocument;

            DBObjectCollection objects_pline3d =zjyCAD. GetObjects("zjy_hdm_HDM");
   
            var doc_lock = middoc.LockDocument();

 
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
               
                //完成辅助hdm线的排序
                foreach (DBObject pline3d_tmp in objects_pline3d)
                {
                    if(pline3d_tmp.GetType().Equals(typeof(Polyline3d)))
                    {
                        Curve curvet_tmp = (Curve)pline3d_tmp;
                        if (curvet_tmp.XData == null) continue;

                        string stack = curvet_tmp.XData.AsArray()[1].Value.ToString();
                        zjyCAD.zCreatText(stack, new zPointXY(curvet_tmp.EndPoint.X, curvet_tmp.EndPoint.Y), 3.5, 0.8, "zjy_pline3d_stack");
                        if (dLList.Count>0)
                        {
                            stack = zjyCAD.DLStackFromStack(Math.Round(Convert.ToDouble( stack), 3) , dLList);
                            zjyCAD.zCreatText(stack, new zPointXY(curvet_tmp.StartPoint.X, curvet_tmp.StartPoint.Y), 3.5, 0.8, "zjy_pline3d_stack");
                        }
                        
                    }
                }

                trans.Commit();

            }

            doc_lock.Dispose();

        }

        private void tbx_pline3d_xdata_showstack_TextChanged(object sender, EventArgs e)
        {
            //断链数据
            List<KeyValuePair<double, double>> dLList = new List<KeyValuePair<double, double>>();
            if (cbx_DL.Checked == true)
            {
                string[] dlStrArr = rtbx_DL.Lines;
                if (dlStrArr.Length > 0)
                {
                    foreach (string dlStr in dlStrArr)
                    {
                        if (dlStr != "")
                        {
                            string[] tmp = dlStr.Split(new char[] { '=', ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                            dLList.Add(new KeyValuePair<double, double>(Convert.ToDouble(tmp[1]), Convert.ToDouble(tmp[2])));
                        }
                    }
                }
            }


              string showstack = tbx_pline3d_xdata_showstack.Text.Trim();

            try
            {
                tbx_pline3d_xdata_stack.Text = zjyCAD.StackFromDLStack(showstack, dLList).ToString();
            }
            catch { }

               
            
            
        }

        private void btn_test_hdm_attitude_Click(object sender, EventArgs e)
        {
            zjyCAD.zTestHDM_Attitude(Convert.ToDouble(tbx_hdm_test_attitude_limit.Text.Trim()));
        }
    }
}
