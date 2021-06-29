using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Text;
using System.Runtime.InteropServices;
using cadSer = Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.DatabaseServices;
using cadDataBaseServices= Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Windows;
using Autodesk.AutoCAD.Colors;
using cadWin = Autodesk.AutoCAD.Windows;
using System.IO;
using Autodesk.AutoCAD.Geometry;
using MgdApp = Autodesk.AutoCAD.ApplicationServices.Application;
using System.Timers;
using System.Threading;
using Autodesk.AutoCAD.ApplicationServices;
//using Autodesk.AutoCAD.
using Autodesk.AutoCAD.Publishing;


using Autodesk.AutoCAD.PlottingServices;
using System.Text.RegularExpressions;
using zjy_CAD;
using System.Windows.Forms;
using Application = Autodesk.AutoCAD.ApplicationServices.Application;
using System.Diagnostics;
using zjy_cad_chajian;

using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;
//using System.Runtime.InteropServices;



[assembly: CommandClass(typeof(zjy.zjyCAD))]
namespace zjy
{
    class zjyCAD
    {
       // string border_LayerName = "zjyPrint";

        /// <summary>
        /// ///////////////////////////////////////////////////////////////////////////////////////
        /// ///////////////////////////////////////////////////////////////////////////////////////
        /// //////////////////////////////WGS84=高斯投影=web墨卡托///////////////////////////////
        /// ///////////////////////////////////////////////////////////////////////////////////////
        /// ///////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        // Directory.get
        [CommandMethod("arrnum")]
        public void ArrNum()
        {
            Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
          //  double jianju = ed.GetDouble("输入间距：\n").Value;
          // Point3d startpt= ed.GetPoint("选取点").Value;
            double jianju = 1.1;
            double textHeight = 0.2;
            double circleR =0.2;
            int maxNum = 500;
            double y = 0;
            for(int i=0;i<maxNum;i++)
            {
                y -= jianju;
                zCircleCreate(new zPointXY(0, y), circleR);
                zCreatMtext(i.ToString(), new zPointXY(10, y), textHeight, textHeight * 2,"zjy_assitant");
            }
        }


        [CommandMethod("Pafu")]
        public void Pallete()
        {
            Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
            zjy_SHowWindow showcontrol = new zjy_SHowWindow();
            zjy_hdm hdmcontrol = new zjy_hdm();
            User_Control_fuzhu layercontrol = new User_Control_fuzhu();
            User_Control_Sheji designcontrol = new User_Control_Sheji();
            UserControl_batch_Plot batch_Plot = new UserControl_batch_Plot();
            UserControl_batch_EditName batch_EditName = new UserControl_batch_EditName();
            PaletteSet ps = new cadWin.PaletteSet("辅助信息")
            {
                Visible = true,
                Style = cadWin.PaletteSetStyles.ShowCloseButton,
                Dock = cadWin.DockSides.None,
                // ps.AutoRollUp = true;
                // ps.Name = "zjy";
                // ps.TitleBarLocation = cadWin.PaletteSetTitleBarLocation.Right;
                Size = new System.Drawing.Size(500, 1000),
                // ps.Anchored=
                // ps.AutoRollUp = cadWin.DropTarget;


                //ps.Dock = cadWin.DockSides.None;
                MinimumSize = new System.Drawing.Size(20, 1000)
            };


            // ps.Size = new System.Drawing.Size(144, 1000);
            ps.Add("CAD_KML", showcontrol);
            ps.Add("生成hdm与dmx", hdmcontrol);
            ps.Add("层功能", layercontrol);
            ps.Add("纬地设计",designcontrol);
            ps.Add("批量打印",batch_Plot);
            ps.Add("批量修改项目名称",batch_EditName);
            //   ed.WriteMessage(Thread.CurrentThread.ManagedThreadId.ToString() + ":Pasj函数线程\n");
            //  ps.Dispose();
        }





        // [CommandMethod("hhd")]
        public static void CreateHDMPline(double _qiexian_err,double _hdm_err, List<double> _stackList)
        {
            double qiexian_err = _qiexian_err;
            double hdm_err = _hdm_err;
            List<double> stackList = _stackList;



            Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
          
          
                Database db = HostApplicationServices.WorkingDatabase;
                var middoc = cadSer.Application.DocumentManager.MdiActiveDocument;

                        
                ObjectId plineID=    ed.GetEntity(new PromptEntityOptions("\n选取路线的合成多段线!!!")).ObjectId;
                var doc_lock = middoc.LockDocument();
            
                DBObject dbo;
                using (Transaction trans = db.TransactionManager.StartTransaction())
                {
                    dbo = trans.GetObject(plineID, OpenMode.ForRead);
                    
                    while (!dbo.GetType().Equals(typeof(Polyline)))
                    {
                        ed.WriteMessage("\n刚才选取的不是Pline,请重新选择!!!");
                        plineID = ed.GetEntity(new PromptEntityOptions("\n选取路线的合成多段线!!!\n")).ObjectId;
                      dbo = trans.GetObject(plineID, OpenMode.ForRead);
                    }

                #region 测试可删除

                //ObjectId plineID_1 = ed.GetEntity(new PromptEntityOptions("\n选取多段线!!!")).ObjectId;

                //Curve dbo_1 = trans.GetObject(plineID_1, OpenMode.ForRead) as Curve;
                //Point3dCollection point3DCollection1 = new Point3dCollection();
                //dbo_1.IntersectWith((Curve)dbo, Intersect.OnBothOperands, point3DCollection1, 0, 0);

                //((Curve)dbo_1).IntersectWith((Curve)dbo, Intersect.OnBothOperands, new Plane(new Point3d(0, 0, 0), new Vector3d(0, 0, 1)), point3DCollection1, 0, 0);
                //foreach (Point3d aa in point3DCollection1)
                //{
                //    ed.WriteMessage(aa.X.ToString() + "___" + aa.Y.ToString() + "___" + aa.Z.ToString() + "\n");
                //}


                //Polyline3d pt11 = new Polyline3d();
                //if (point3DCollection1.Count == 1)
                //{
                //    Point3d pt1 = point3DCollection1[0] + new Vector3d(0, 0, 100000);

                //    Point3d pt2 = point3DCollection1[0] + new Vector3d(0, 0, -100000);
                //    point3DCollection1.Clear();
                //    point3DCollection1.Add(pt1);
                //    point3DCollection1.Add(pt2);
                //    pt11 = new Polyline3d(Poly3dType.SimplePoly, point3DCollection1, false);
                //}
                //foreach (Point3d aa in point3DCollection1)
                //{
                //    ed.WriteMessage(aa.X.ToString() + "___" + aa.Y.ToString() + "___" + aa.Z.ToString() + "\n");
                //}

                //point3DCollection1.Clear();
                //((Curve)dbo_1).IntersectWith((Curve)pt11, Intersect.OnBothOperands, point3DCollection1, 0, 0);

                //foreach (Point3d aa in point3DCollection1)
                //{
                //    ed.WriteMessage(aa.X.ToString() + "___" + aa.Y.ToString() + "___" + aa.Z.ToString() + "\n");
                //}



                //ed.WriteMessage(point3DCollection1.Count.ToString());

                #endregion

                List<Point3d> allGCD_List = new List<Point3d>();

                ed.WriteMessage("\n选择需要生成的横断面高程点\n");
                List<ObjectId> allgcd = ed.GetSelection().Value.GetObjectIds().ToList<ObjectId>();
                foreach (ObjectId tmpid in allgcd)
                {
                    DBObject dBObject = tmpid.GetObject(OpenMode.ForRead);
                    if (dBObject.GetType().Equals(typeof(BlockReference)))
                    {
                        try
                        {
                            BlockReference dboRefer = (BlockReference)dBObject;

                            allGCD_List.Add(dboRefer.Position);
                        }
                        catch
                        {
                            ed.WriteMessage("无效块信息\n");
                        }
                    }
                }





                Polyline pline = (Polyline)dbo;
                //    List<Point3d> allGCD_List_Used = new List<Point3d>();
                Dictionary<double, zHDMPoint> hdmPointDic = new Dictionary<double, zHDMPoint>();

                // for (double len = 0; len <= pline.Length; len += 20)
              foreach(double len in stackList)
                {
                    if (len>pline.Length)
                    {
                        ed.WriteMessage("\nCAD多段线长度小于实际路线,请稍微延长CAD多段线!!!\n");
                        return;
                    }
                  //  double len=
                    Point3d p0 = pline.GetPointAtDist(len);

                    Point3d p0_2D = new Point3d(p0.X, p0.Y, 0);

                    Vector3d qiexiangVector = new Vector3d(pline.GetFirstDerivative(p0).X, pline.GetFirstDerivative(p0).Y, 0);
                    qiexiangVector=qiexiangVector/qiexiangVector.Length;

                    Vector3d nomalVector = qiexiangVector.RotateBy(-Math.PI/2, new Vector3d(0, 0, 1));
                    SortedDictionary<double, Point3d> dmPointSortedDic = new SortedDictionary<double, Point3d>();
                    foreach (Point3d gcdPoint3d in allGCD_List)
                    {
                        Vector3d tmp2D = new Vector3d(gcdPoint3d.X, gcdPoint3d.Y, 0);
                        if (Math.Abs(tmp2D.DotProduct(qiexiangVector) - p0_2D.GetAsVector().DotProduct(qiexiangVector)) < qiexian_err && Math.Abs(tmp2D.DotProduct(nomalVector) - p0_2D.GetAsVector().DotProduct(nomalVector)) < hdm_err)
                        {
                            double key_ = tmp2D.DotProduct(nomalVector) - p0_2D.GetAsVector().DotProduct(nomalVector);
                            if (!dmPointSortedDic.ContainsKey(key_))
                            {
                                dmPointSortedDic.Add(tmp2D.DotProduct(nomalVector) - p0_2D.GetAsVector().DotProduct(nomalVector), gcdPoint3d);
                            }
                        }
                    }

                    if (dmPointSortedDic.Count > 0)
                    {
                        hdmPointDic.Add(len, new zHDMPoint(dmPointSortedDic));
                    }
                    foreach (Point3d dmpt in dmPointSortedDic.Values)
                    {
                        allGCD_List.Remove(dmpt);
                    }


                }
                #region
                //Point3dCollection tmppoint3dCollection = new Point3dCollection();
                //pline.GetStretchPoints(tmppoint3dCollection);

                //zPolyLine3DCreate(tmppoint3dCollection, 500, "zjy_pline3d");

                //ed.WriteMessage(pline.StartParam.ToString());
                //ed.WriteMessage(pline.EndParam.ToString());



                //// zPointXY startPoint =new zPointXY( pline.GetPointAtParameter(1).X, pline.GetPointAtParameter(1).Y);
                ////zPointXY endPoint = new zPointXY((pline.GetPointAtParameter(1)+vector3D*5).X, (pline.GetPointAtParameter(1) + vector3D * 5).Y);

                //for (int i = 0; i < pline.EndParam; i++)
                //{
                //    Vector3d vector3D = pline.GetFirstDerivative(i);
                //    Line linetmp = new Line(pline.GetPointAtParameter(i), (pline.GetPointAtParameter(i) + vector3D));
                //    linetmp.Color = Color.FromColorIndex(ColorMethod.ByAci, 1);
                //    ToModelSpace(linetmp, db);
                //}




                //ObjectId[] gcdHDM = ed.GetSelection().Value.GetObjectIds();
                //foreach(ObjectId tmpid in gcdHDM)
                //{

                //}

                #endregion

                foreach (var tmp in hdmPointDic)
                {
                    Point3dCollection point3DCollection = new Point3dCollection(tmp.Value.dmPointSortedDic.Values.ToArray());
                    zPolyLine3DCreate(point3DCollection, tmp.Key, "zjy_hdm_HDM");
                }

                trans.Commit();
               
            }

            doc_lock.Dispose();


            ed.WriteMessage("\n采集的横断面辅助线已经生成，请检查以及修改!!!\n");


        

        }

        //[CommandMethod("qqw")]
        //public static void aaa()
        //{
        //    Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;


        //    Database db = HostApplicationServices.WorkingDatabase;
        //    var middoc = cadSer.Application.DocumentManager.MdiActiveDocument;

        //    var doc_lock = middoc.LockDocument();

        //    DBObject dbo;
        //    using (Transaction trans = db.TransactionManager.StartTransaction())
        //    {
        //        DBObjectCollection gcdColletion = zjyCAD.GetObjects("GCD");

        //        foreach (DBObject tmpid in gcdColletion)
        //        {

        //            if (tmpid.GetType().Equals(typeof(BlockReference)))
        //            {
        //                BlockReference dboRefer = (BlockReference)tmpid;

        //                foreach (ObjectId var in dboRefer.AttributeCollection)
        //                {
        //                    AttributeReference dboooo = trans.GetObject(var, OpenMode.ForWrite) as AttributeReference;
        //                    double showtag =Convert.ToDouble( dboooo.TextString.Trim());
        //                    if(Maht.)
        //                }

        //            }
        //        }

        //        trans.Commit();
        //    }

        //    doc_lock.Dispose();

        //}

        // [CommandMethod("hdd")]
        public static void CreadHDM(string _path,string _filename, double _startstack, List<KeyValuePair<double, double>> _dLList)
        {
            string path = _path;
            string filename = _filename;
            double startstack = _startstack;
            List<KeyValuePair<double, double>> dLList = _dLList;



            List<string> outputHdm = new List<string>();
            outputHdm.Add("HINTCAD5.83_HDM_SHUJU");


            SortedDictionary<double, double> zdm_dic = new SortedDictionary<double, double>();



            Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;


            Database db = HostApplicationServices.WorkingDatabase;
            var middoc = cadSer.Application.DocumentManager.MdiActiveDocument;

            DBObjectCollection objects_GCD = GetObjects("zjy_hdm_HDM");




            ObjectId plineID = ed.GetEntity(new PromptEntityOptions("\n选取路线的合成多段线!!!\n")).ObjectId;
            var doc_lock = middoc.LockDocument();

            DBObject dbo;
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                dbo = trans.GetObject(plineID, OpenMode.ForRead);

                while (!dbo.GetType().Equals(typeof(Polyline)))
                {
                    ed.WriteMessage("\n刚才选取的不是Pline,请重新选择!!!");
                    plineID = ed.GetEntity(new PromptEntityOptions("\n选取路线的合成多段线!!!\n")).ObjectId;
                    dbo = trans.GetObject(plineID, OpenMode.ForRead);
                }

                Polyline shejixian = (Polyline)dbo;
                SortedDictionary<double, Polyline3d> hdm_pline3d_dic = new SortedDictionary<double, Polyline3d>();

                //完成辅助hdm线的排序
                foreach (DBObject gcd_tmp in objects_GCD)
                {
                    if (gcd_tmp.GetType().Equals(typeof(Polyline3d)))
                    {
                        TypedValue[] typevalueArr = gcd_tmp.XData.AsArray();
                        if (typevalueArr.Length > 0)
                        {
                            try
                            {
                                hdm_pline3d_dic.Add(Convert.ToDouble(typevalueArr[1].Value.ToString()), (Polyline3d)gcd_tmp);
                            }
                            catch
                            {
                                ed.WriteMessage("\n在桩号 "+ typevalueArr[1].Value.ToString()+" 处存在多个横断面线请检查!!!\n");
                            }
                        }
                     
                    }
                }



                

                foreach (var polyline3d_tmp in hdm_pline3d_dic)
                {
                    double stack = polyline3d_tmp.Key;

                    Point3d p0 = shejixian.GetPointAtDist(stack);

                    Point3d p0_2D = new Point3d(p0.X, p0.Y, 0);

                    Vector3d qiexiangVector = new Vector3d(shejixian.GetFirstDerivative(p0).X, shejixian.GetFirstDerivative(p0).Y, 0);
                    qiexiangVector = qiexiangVector / qiexiangVector.Length;

                    Vector3d nomalVector = qiexiangVector.RotateBy(-Math.PI / 2, new Vector3d(0, 0, 1));


                    double attitude_center=0;
                    //获得中桩高程
                    Point3dCollection polyline3d_hdm_pt = new Point3dCollection();
                    polyline3d_tmp.Value.GetStretchPoints(polyline3d_hdm_pt);

                    Point3dCollection intersectPoint = new Point3dCollection();

                    for(int ptsnum=0;ptsnum<polyline3d_hdm_pt.Count-1;ptsnum++)
                    {
                        Vector3d v1 = polyline3d_hdm_pt[ptsnum] - p0_2D;
                        Vector3d v2 = polyline3d_hdm_pt[ptsnum + 1] - p0_2D;
                       
                       if(Math.Abs( v1.DotProduct(nomalVector))< 0.00000000001)
                        {
                            intersectPoint.Add(polyline3d_hdm_pt[ptsnum]);
                        }else if(Math.Abs(v2.DotProduct(nomalVector)) < 0.00000000001)
                        {
                            intersectPoint.Add(polyline3d_hdm_pt[ptsnum+1]);
                        }
                        else if (v1.DotProduct(nomalVector) * v2.DotProduct(nomalVector) < 0)
                        {
                            double aa = v1.DotProduct(nomalVector);
                            double bb = v2.DotProduct(nomalVector);
                            double xx = polyline3d_hdm_pt[ptsnum].X - (polyline3d_hdm_pt[ptsnum].X - polyline3d_hdm_pt[ptsnum + 1].X) * aa / (aa+bb);
                            double yy = polyline3d_hdm_pt[ptsnum].Y - (polyline3d_hdm_pt[ptsnum].Y - polyline3d_hdm_pt[ptsnum + 1].Y) * aa / (aa + bb);
                            double zz = polyline3d_hdm_pt[ptsnum].Z - (polyline3d_hdm_pt[ptsnum].Z - polyline3d_hdm_pt[ptsnum + 1].Z) * aa / (aa + bb);

                            intersectPoint.Add(new Point3d(xx,yy,zz));

                        }
                    }

                    //Plane normalHDM = new Plane(p0_2D,nomalVector);
                    //Point3d facePt1 = p0_2D + qiexiangVector*1000;

                    //Point3d facePt2 = p0_2D - qiexiangVector * 1000;
                    //Point3d facePt3 = facePt2 + new Vector3d(0,0,10000);
                    //Point3d facePt4 = facePt1 + new Vector3d(0, 0, 10000);
                    //Face face = new Face(facePt1, facePt2, facePt3, facePt4, true, true, true, true);


                    //  ToModelSpace(face, db);
                    // polyline3d_tmp.Value.IntersectWith(new Face(facePt1,facePt2,facePt3,facePt4,true, true, true, true), Intersect.OnBothOperands, intersectPoint, 0, 0);
                    // ed.WriteMessage("\n============" + qiexiangVector.X + "," + qiexiangVector.Y + "," + qiexiangVector.Z);
                    //  face.IntersectWith(polyline3d_tmp.Value, Intersect.OnBothOperands, intersectPoint, 0, 0);

                    if (intersectPoint.Count == 1)
                    {

                        attitude_center = intersectPoint[0].Z;
                        if (!zdm_dic.ContainsKey(stack))
                        {
                            zdm_dic.Add(stack, attitude_center);
                        }
                    }
                    else if (intersectPoint.Count > 1)
                    { ed.WriteMessage("\n横断面线与设计线有 多 个交点，请检查横断面数据是否存在错误：桩号位置" + stack.ToString()); }
                    else
                    {
                        ed.WriteMessage("\n横断面线与设计线 无 交点：桩号位置" + stack.ToString());
                    }

                    #region
                    //polyline3d_tmp.Value.IntersectWith(shejixian, Intersect.OnBothOperands, new Plane(new Point3d(0, 0, 0), new Vector3d(0, 0, 1)), intersectPoint, 0, 0);

                    //if (intersectPoint.Count == 1)
                    //{
                    //    Point3d pt1 = intersectPoint[0] + new Vector3d(0, 0, 100000);

                    //    Point3d pt2 = intersectPoint[0] + new Vector3d(0, 0, -100000);
                    //    intersectPoint.Clear();

                    //    intersectPoint.Add(pt1);
                    //    intersectPoint.Add(pt2);
                    //  Polyline3d  pline_z_assitant = new Polyline3d(Poly3dType.SimplePoly, intersectPoint, false);

                    //    intersectPoint.Clear();
                    //    polyline3d_tmp.Value.IntersectWith(pline_z_assitant, Intersect.OnBothOperands,  intersectPoint, 0, 0);


                    //    if (intersectPoint.Count == 1)
                    //    {
                    //        attitude_center = intersectPoint[0].Z;
                    //        if (!zdm_dic.ContainsKey(stack))
                    //        {
                    //            zdm_dic.Add(stack, attitude_center);
                    //        }

                    //    }else
                    //    {
                    //        ed.WriteMessage("\n无法找到中桩桩号" + stack.ToString());
                    //    }

                    //}else
                    //{
                    //    ed.WriteMessage("\n横断面线与设计线无交点：桩号位置"+stack.ToString());
                    //}
                    // else { return; }

                    #endregion





                    Point3dCollection point3D_Collection = new Point3dCollection();
                    polyline3d_tmp.Value.GetStretchPoints(point3D_Collection);



                    SortedDictionary<double, Point3d> left_from_sjx = new SortedDictionary<double, Point3d>();
                    SortedDictionary<double, Point3d> right_from_sjx = new SortedDictionary<double, Point3d>();
                    //完成左右侧的整理
                    foreach (Point3d point_tmp in point3D_Collection)
                    {
                        Point3d tmp_2d = new Point3d(point_tmp.X, point_tmp.Y, 0);
                        double juli_fromCentre = tmp_2d.GetAsVector().DotProduct(nomalVector) - p0_2D.GetAsVector().DotProduct(nomalVector);
                        if (juli_fromCentre >= 0 && (!right_from_sjx.ContainsKey(juli_fromCentre)))
                        {
                            right_from_sjx.Add(juli_fromCentre, point_tmp);
                        }
                        else if (juli_fromCentre < 0 && (!left_from_sjx.ContainsKey(-juli_fromCentre)))
                        {
                            left_from_sjx.Add(-juli_fromCentre, point_tmp);
                        }
                    }
                    left_from_sjx.Add(0.0, new Point3d(p0.X,p0.Y ,attitude_center));
                    right_from_sjx.Add(0.0, new Point3d(p0.X, p0.Y, attitude_center));



                    string stackStr = (Math.Round(stack, 3) + startstack).ToString();

                    if (dLList.Count > 0)
                    {
                        stackStr = DLStackFromStack(Math.Round(stack, 3) + startstack, dLList).ToString();
                    }

                    ////输出道HDM文件

                    //桩号
                    //  outputHdm.Add(Math.Round(stackStr, 3).ToString());
                    outputHdm.Add(stackStr);
                    //左侧
                    StringBuilder string_left = new StringBuilder();
                    
                    string_left.Append((left_from_sjx.Count-1).ToString());

                    
                    for (int i=0;i<left_from_sjx.Count-1;i++)
                    {
                       

                        double juli = left_from_sjx.Keys.ToArray()[i+1] - left_from_sjx.Keys.ToArray()[i];
                        double gaochengDelat = left_from_sjx.Values.ToArray()[i + 1].Z - left_from_sjx.Values.ToArray()[i].Z;
                        string tmp = "\t " + Math.Round(juli, 3).ToString() + "\t " + Math.Round(gaochengDelat, 3).ToString();
                        string_left.Append(tmp);

                    }

                    outputHdm.Add(string_left.ToString());


                    //右侧
                    StringBuilder string_right = new StringBuilder();

                    string_right.Append((right_from_sjx.Count - 1).ToString());


                    for (int i = 0; i < right_from_sjx.Count - 1; i++)
                    {
                        double juli = right_from_sjx.Keys.ToArray()[i + 1] - right_from_sjx.Keys.ToArray()[i];
                        double gaochengDelat = right_from_sjx.Values.ToArray()[i + 1].Z - right_from_sjx.Values.ToArray()[i].Z;
                        string tmp = "\t " + Math.Round(juli, 3).ToString() + "\t " + Math.Round(gaochengDelat, 3).ToString();
                        string_right.Append(tmp);

                    }

                    outputHdm.Add(string_right.ToString());





                }//横断面循环结束






               

                trans.Commit();

            }

            doc_lock.Dispose();

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
           ed.WriteMessage(path + "/" + filename + ".hdm");
            File.WriteAllLines(path+"/"+filename+".hdm", outputHdm.ToArray());

            List<string> zdm = new List<string>();
            zdm.Add("HINTCAD5.83_DMX_SHUJU");
            foreach(var tmp in zdm_dic)
            {
                //添加断链数据

                string stackStr = (Math.Round(tmp.Key, 3) + startstack).ToString();

                if (dLList.Count > 0)
                {
                    stackStr = DLStackFromStack(Math.Round(tmp.Key, 3) + startstack, dLList).ToString();
                }

                string abc = stackStr + " \t" + Math.Round(tmp.Value, 3).ToString();
                zdm.Add(abc);
            }

            File.WriteAllLines(path + "/" + filename + ".dmx", zdm.ToArray());

        }

        public static void zTestHDM_Attitude(double z_limit)
        {
            Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
            Database db = HostApplicationServices.WorkingDatabase;
            var middoc = cadSer.Application.DocumentManager.MdiActiveDocument;

            DBObjectCollection objects_GCD = GetObjects("zjy_hdm_HDM");
            ed.WriteMessage("\n"+objects_GCD.Count.ToString()+"\n");
            var doc_lock = middoc.LockDocument();


            foreach(DBObject dbo in objects_GCD)
            {
                if(dbo is Polyline3d)
                {
                    Point3dCollection hdmPoints = new Point3dCollection();
                    (dbo as Polyline3d).GetStretchPoints(hdmPoints);
                    foreach(Point3d hdmPointtmp in hdmPoints)
                    {
                        if(hdmPointtmp.Z<z_limit)
                        {
                            zCircleCreate(hdmPointtmp, 5);
                        }
                    }
                }
            }

  


          

            doc_lock.Dispose();
        }

        public static void Ceshi(string path)
        {
            string[] path_all_name_txt = Directory.GetFiles(path, "(*.txt)");
            string[] path_all_name = Directory.GetFiles(path );//; Directory.GetFiles(path+"\\", "(*.png|*.jpg|*.bmp)");


            foreach (string tmp_file in path_all_name)
            {
                if (!(Path.GetExtension(tmp_file) == ".jpg" || Path.GetExtension(tmp_file) == ".bmp" || Path.GetExtension(tmp_file) == ".png")) continue;

                string pathname_txt = Path.GetDirectoryName(tmp_file) + "\\" + Path.GetFileNameWithoutExtension(tmp_file) + ".txt";
                string imagepath = tmp_file;

                string txtpath = pathname_txt;

                Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
                Database db = HostApplicationServices.WorkingDatabase;
                var middoc = cadSer.Application.DocumentManager.MdiActiveDocument;

                double centre = 111;
                string[] lines = File.ReadAllLines(txtpath);

                Point3d rightTop;
                Point3d leftBottom;

                string[] righttop_str = lines[1].Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string[] leftBottom_str = lines[0].Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                zPointXY righttop_1 = WGS84_BLToXY(new zPointXY(Convert.ToDouble(righttop_str[0].Trim()), Convert.ToDouble(righttop_str[1].Trim())), centre);

                zPointXY leftBottom_1 = WGS84_BLToXY(new zPointXY(Convert.ToDouble(leftBottom_str[0].Trim()), Convert.ToDouble(leftBottom_str[1].Trim())), centre);
                zPointXY rightbottom_1 = WGS84_BLToXY(new zPointXY(Convert.ToDouble(righttop_str[0].Trim()), Convert.ToDouble(leftBottom_str[1].Trim())), centre);
                zPointXY leftTop_1 = WGS84_BLToXY(new zPointXY(Convert.ToDouble(leftBottom_str[0].Trim()), Convert.ToDouble(righttop_str[1].Trim())), centre);

                double image_true_width = (righttop_1 - leftBottom_1).x;
                double image_true_height = (righttop_1 - leftBottom_1).y;
                double image_length = Math.Sqrt(Math.Pow((righttop_1 - leftBottom_1).x, 2.0) + Math.Pow((righttop_1 - leftBottom_1).y, 2.0));

                var doc_lock = middoc.LockDocument();
                ed.WriteMessage("开始加载\n");
                using (Transaction trs = db.TransactionManager.StartTransaction())
                {
                    BlockTable bt = (BlockTable)trs.GetObject(HostApplicationServices.WorkingDatabase.BlockTableId, OpenMode.ForWrite);
                    //BlockTable 
                    BlockTableRecord btr = (BlockTableRecord)trs.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);


                    ////创建imagedef
                    ObjectId imageId = RasterImageDef.GetImageDictionary(db);
                    if (imageId.IsNull)
                    {
                        imageId = RasterImageDef.CreateImageDictionary(db);
                    }

                    DBDictionary imageDiconary = trs.GetObject(imageId, OpenMode.ForWrite) as DBDictionary;

                    RasterImageDef imageDef = new RasterImageDef();
                    ed.WriteMessage(imagepath + "    imagepath\n");
                    imageDef.SourceFileName = imagepath;


                    imageDef.Load();
                    string imagename = RasterImageDef.SuggestName(imageDiconary, imagepath);
                    ed.WriteMessage(imagename + "    imagename\n");
                    ObjectId idEnntry = imageDiconary.SetAt(imagename, imageDef);
                    trs.AddNewlyCreatedDBObject(imageDef, true);

                    RasterImage image = new RasterImage();

                    image.ImageDefId = imageDef.ObjectId;
                    ObjectId imageid = btr.AppendEntity(image);

                    RasterImage.EnableReactors(true);
                    image.AssociateRasterDef(imageDef);
                    ed.WriteMessage("\nimage宽度+" + image.Width.ToString());
                    ed.WriteMessage("\nimage宽度+" + image.Height.ToString());
                    //image.Orientation = new CoordinateSystem3d(new Point3d(leftBottom_1.x, leftBottom_1.y, 0), Vector3d.XAxis * image_true_width, Vector3d.YAxis * image_true_height );

                    image.Orientation = new CoordinateSystem3d(new Point3d(leftBottom_1.x, leftBottom_1.y, 0), new Vector3d((rightbottom_1 - leftBottom_1).x, (rightbottom_1 - leftBottom_1).y, 0), new Vector3d((leftTop_1 - leftBottom_1).x, (leftTop_1 - leftBottom_1).y, 0));

                    trs.AddNewlyCreatedDBObject(image, true);

                    //



                    trs.Commit();
                }
                doc_lock.Dispose();



            }
        }


        public static List<string> GetLimitRegions(double centrle)
        {
            List<string> regionList = new List<string>();

            //  zjy_cad_chajian.zjyEntity . GetObject_Array();
            zjy_cad_function zjy_cad_function_0 = new zjy_cad_function();
            DBObject[] dbObjects=    zjy_cad_function_0.GetObject_Array();
            foreach(DBObject dbo in dbObjects)
            {
                if(dbo is Polyline)
                {
                    Polyline polyline = dbo as Polyline;
                    Point3dCollection point3DCollection = new Point3dCollection();
                    polyline.GetStretchPoints(point3DCollection);
                    List<double> log_tmp = new List<double>();
                    List<double> lat_tmp = new List<double>();
                    //List<zPointXY> zPointBLList = new List<zPointXY>();

                    foreach(Point3d tmppoint in point3DCollection)
                    {
                     zPointXY zPointBL=   WGS84_XYToBL(new zPointXY(tmppoint.X,tmppoint.Y),centrle);
                        log_tmp.Add(zPointBL.y);
                        lat_tmp.Add(zPointBL.x);
                       // zPointBLList.Add(zPointBL);
                    }

                    string tmp = log_tmp.Max().ToString() + "," + lat_tmp.Max().ToString() + "," +log_tmp.Min().ToString()+","+lat_tmp.Min().ToString();
                    regionList.Add(tmp);

                }
            }

            return regionList;
        }

        public static String DLStackFromStack(double stack, List<KeyValuePair<double, double>> dLList)
        {
            string dlStack = "";
            if (dLList.Count == 0) return Convert.ToString(stack);
            int i = 0;
            for (i = 0; i < dLList.Count; i++)
            {
                if (dLList[i].Key < stack)
                {
                    stack += dLList[i].Value - dLList[i].Key;
                }
                else
                {

                    break;
                }
            }
            dlStack = (char)('A' + i) + stack.ToString();

            return dlStack;
        }
        public static double StackFromDLStack(string dlStack, List<KeyValuePair<double, double>> dLList)
        {
            double stack = 0;
            dlStack = dlStack.Trim();
            char firstChar = dlStack[0];
            int index = 0;
            bool isleft = false;


            if (firstChar >= 'A' && firstChar <= 'Z')
            {
                index = firstChar - 'A';
                stack = Convert.ToDouble(dlStack.Substring(1));
                if (stack < 0.0)
                {
                    isleft = true;
                    stack *= -1.0;
                }

                for (int i = 0; i < index; i++)
                {
                    stack -= (dLList[i].Value - dLList[i].Key);
                }

                if (isleft == true)
                {
                    stack *= -1.0;
                }

            }
            else//有缺陷
            {
                stack = Convert.ToDouble(dlStack);
            }
            return stack;
        }
        #region  public void RealKML()
        [Obsolete("废弃")]
        [CommandMethod("brt")]
        public void RealKML()
        {
            Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
            string MKLDirctory = @"E:\kml文件读取";
            List<string> ContentList = new List<string>();
            string[] AllFiles = Directory.GetFiles(MKLDirctory);
            Dictionary<string, List<zPointXY>> PointsDic = new Dictionary<string, List<zPointXY>>();

            //List<string> liststrBL = new List<string>();

            foreach (string tmp in AllFiles)
            {
                if (tmp.Contains(".kml"))
                {
                 //   Console.WriteLine(tmp);
                    XmlDocument xmlD = new XmlDocument();
                    xmlD.Load(tmp);
                    XmlNode node = xmlD.DocumentElement; //SelectSingleNode("Document/name");
                    string keyDic = null;
                    foreach (XmlNode a in node.ChildNodes[0].ChildNodes)
                    {

                        if (a.Name == "name")
                        {
                            keyDic = tmp;
                        }
                        if (a.Name == "Folder")
                        {
                            //Console.WriteLine(a.ChildNodes[0]);

                            XmlNodeList nodeList = a.ChildNodes;
                            foreach (XmlNode i in nodeList)
                            {
                                if (i.ChildNodes[0].InnerText == "线路追踪路径")
                                {
                                    string zb = i.ChildNodes[4].ChildNodes[1].InnerText;
                                    string[] zbArr = zb.Trim().Split('\n');
                                    List<zPointXY> pointList = new List<zPointXY>();
                                    //  ed.WriteMessage(zb + "\n");
                                    foreach (string str in zbArr)
                                    {
                                        string[] points = str.Trim().Split(',');
                                        zPointXY point = new zPointXY(Convert.ToDouble(points[0]), Convert.ToDouble(points[1]));
                                        pointList.Add(point);
                                    }
                                    if (keyDic.Contains("反"))
                                    {
                                        pointList.Reverse();
                                    }
                                    if (!PointsDic.Keys.Contains(keyDic))
                                    {
                                        PointsDic.Add(keyDic, pointList);
                                    }

                                }
                            }


                        }
                    }

                }
            }
            ed.WriteMessage("读取完成\n");


            double centerLongitude = 114;



           foreach (var tmp in PointsDic)
            {
                ed.WriteMessage("绘制polyline\n");
               // zPolylineCreate(JW84ToWebMercatorXY(tmp.Value));


             //   zCircleCreate(JW84ToWebMercatorXY(tmp.Value[0]), 60);

               // zCircleCreate(JW84ToWebMercatorXY(tmp.Value[tmp.Value.Count - 1]), 40);
              //  zCircleCreate(JW84ToWebMercatorXY(tmp.Value[tmp.Value.Count - 1]), 20);

                //zCreatMtext(Path.GetFileName(tmp.Key), JW84ToWebMercatorXY(tmp.Value[0]), 50, 400);



                zPolylineCreate(WGS84_BLToXY(tmp.Value, centerLongitude));

                zCircleCreate(WGS84_BLToXY(tmp.Value[0], centerLongitude), 60);

                zCircleCreate(WGS84_BLToXY(tmp.Value[tmp.Value.Count - 1], centerLongitude), 40);
                zCircleCreate(WGS84_BLToXY(tmp.Value[tmp.Value.Count - 1], centerLongitude), 20);


               // zPolylineCreate(WGS84_BLToXY_1(tmp.Value));

                zCreatMtext(Path.GetFileName(tmp.Key), WGS84_BLToXY(tmp.Value[0], centerLongitude), 50, 400,"zjy_KML_file");

              //  List<string> xyTobl_list = new List<string>();

               // List<zPointXY> xytobl = WGS84_XYToBL(tmp.Value);

                //foreach(var tmp1 in xytobl )
                //{
                //    xyTobl_list.Add(Math.Round( tmp1.x,15).ToString()+"\t"+Math.Round( tmp1.y,15).ToString());
                //}
                //string path_1 = @"d:\zhy_" + ii.ToString() + ".zjy";
                //File.WriteAllLines(path_1, xyTobl_list.ToArray());


            }

            //List<zPointXY> liststrBL =PointsDic.Values;
            //List<string> xyTobl_list = new List<string>();

            //List<zPointXY> xytobl = WGS84_XYToBL()

            
            //   Console.ReadLine();
        }
       
        [CommandMethod("kkm")]
        public void CreatKMLFromPline()
        {
            Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
            //两直线合并误差
         
            double centerLongitude = 111;
            try
            {
                Database db = HostApplicationServices.WorkingDatabase;
                var middoc = cadSer.Application.DocumentManager.MdiActiveDocument;
                List<Polyline> polyLineList = new List<Polyline>();

                ObjectId[] objectIdsArr = ed.GetSelection().Value.GetObjectIds();
                ed.WriteMessage("==================================================\n");
                // ed.WriteMessage(objectIds.Count.ToString() + "\n");
                var doc_lock = middoc.LockDocument();
                DBObject dbo;
                //将选择的登高线保存到polyLineList中，如果所选择的线不是polyline，而是polyline2d则
                //通过提取点重新生成polyline
                foreach (ObjectId tempObjectId in objectIdsArr)
                {
                    using (Transaction trans = db.TransactionManager.StartTransaction())
                    {
                        dbo = trans.GetObject(tempObjectId, OpenMode.ForRead);
                        //trans.Commit();
                        if (dbo.GetType().Equals(typeof(Polyline)))
                        {
                            ed.WriteMessage("找到Polyline\n");
                            Polyline dbo_bR = (Polyline)dbo;
                            polyLineList.Add(dbo_bR);
                        }
                        else
                        {

    
                        }
                      
                        trans.Commit();
                    }

                }

                doc_lock.Dispose();

                ed.WriteMessage("===============读取完成=======================\n");

                List<Point3d> plineList = new List<Point3d>();

                Point3dCollection point3Ds_append = new Point3dCollection();

                foreach (var tmppline in polyLineList)
                {
                    ed.WriteMessage(polyLineList.Count() + "选取的多段线个数");
                    tmppline.GetStretchPoints(point3Ds_append);
                    ed.WriteMessage("\n完成=================="+point3Ds_append.Count+"============================");
                    int ii = 0;
                    foreach (Point3d tmp in point3Ds_append)
                    {
                        plineList.Add(tmp);
                        ed.WriteMessage("第" + (ii++));
                    }
                    ed.WriteMessage("\n完成==================ggg============================");
                }

                ed.WriteMessage("\n完成============    ==============================");


                List<zPointXY> zPoints = new List<zPointXY>();
                foreach (var tmpP3d in plineList)
                {
                    zPointXY tmp = new zPointXY(tmpP3d.X, tmpP3d.Y);

                    zPoints.Add(WGS84_XYToBL(tmp, centerLongitude));
                }

                string pmPatth = @"D:\zjy";
                List<string> xmlhead = new List<string>() {
            "<?xml version=\"1.0\" encoding=\"UTF-8\"?>",
         //  "<kml xmlns=\"http://www.opengis.net/kml/2.2\" xmlns:gx=\"http://www.google.com/kml/ext/2.2\" xmlns:kml=\"http://www.opengis.net/kml/2.2\" xmlns:atom=\"http://www.w3.org/2005/Atom\">",
          " <kml xmlns=\"http://earth.google.com/kml/2.1\">",
            "<Document>",
            "<name>zjy</name>",
           // "<open>1</open>",
           // "<description>zjy生成</description>",
            "<Style id=\"yellowLineGreenPoly\" >",
            "   <LineStyle>",
            "       <color>7f00ffff</color>",
            "       <width>4</width>",
            "   </LineStyle>",
            "   <PolyStyle>",
            "       <color>7f00ff00</color>",
            "   </PolyStyle>",
            "   </Style>",
            "<Folder>",
          "<name>zjy</name>",
            "       <visibility>1</visibility>",};

                List<string> xmlend = new List<string>()
            {
                "</Folder>",
                "</Document>",
                "</kml>"
            };


                //生成路线信息图像
                List<string> roadList = new List<string>()
                    {
                        "   <Placemark>",
                       // "       <name>线路追踪路径</name>",
                       // "       <visibility>1</visibility>",
                       // "       <description>路线信息</description>",
                        "       <styleUrl>#yellowLineGreenPoly</styleUrl>",
                        "       <LineString>",
                        "       <tessellate>1</tessellate>",
                        "       <coordinates>"
                    };

                foreach (var tmp in zPoints)
                {
                    roadList.Add("      " + tmp.y.ToString() + "," + tmp.x.ToString() + ",0.0000");
                }
                roadList.Add("      </coordinates>");
                roadList.Add("      </LineString>");
                roadList.Add("  </Placemark>");





                ////生成标签信息
                //List<string> roadBQList = new List<string>();
                //int i = 1;
                //foreach (var tmp in zxBLList)
                //{
                //    roadBQList.Add("    <Placemark>");
                //    roadBQList.Add("        <name>" + (i++).ToString() + "</name>");
                //    roadBQList.Add("        <Point>");
                //    roadBQList.Add("        <coordinates>" + tmp.X.ToString() + "," + tmp.Y.ToString() + ",0.0000" + "</coordinates>");
                //    roadBQList.Add("        </Point>");
                //    roadBQList.Add("        <markerStyle>-2</markerStyle>");
                //    roadBQList.Add("     </Placemark>");
                //}


                List<string> kmlFile = new List<string>();
                kmlFile.AddRange(xmlhead);
                kmlFile.AddRange(roadList);
                //  kmlFile.AddRange(roadBQList);
                kmlFile.AddRange(xmlend);

                File.WriteAllLines(pmPatth + ".kml", kmlFile.ToArray());
            }

            catch
            {
                ed.WriteMessage("出错\n");
            }

        }


        public static void BLListtoKML(List<string> listStr)
        {
         //   string pmPatth = @"D:\zjy";
         //   //zjy_cad_roadinfo road = new zjy_cad_roadinfo(pmPatth, 20);
         //   Dictionary<string, ClassZjyRoadInfo.Vector2D> xyList = new Dictionary<string, Vector2D>();
         //   //   string[] readEveryStack = File.ReadAllLines(path);
         //   int index = 1;
         //   foreach (var tmp in listStr)
         //   {
         //       if (tmp.Trim() == "") continue;
         //       string[] lineStr = tmp.Split(new char[] { '\t', ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
         //       double bb = Convert.ToDouble(lineStr[1]);
         //       double ll = Convert.ToDouble(lineStr[0]);
         //       xyList.Add((index++).ToString(), new Vector2D(ll, bb));

         //   }

         //   List<string> xmlhead = new List<string>() {
         //   "<?xml version=\"1.0\" encoding=\"UTF-8\"?>",
         ////  "<kml xmlns=\"http://www.opengis.net/kml/2.2\" xmlns:gx=\"http://www.google.com/kml/ext/2.2\" xmlns:kml=\"http://www.opengis.net/kml/2.2\" xmlns:atom=\"http://www.w3.org/2005/Atom\">",
         // " <kml xmlns=\"http://earth.google.com/kml/2.1\">",
         //   "<Document>",
         //   "<name>zjy</name>",
         //  // "<open>1</open>",
         //  // "<description>zjy生成</description>",
         //   "<Style id=\"yellowLineGreenPoly\" >",
         //   "   <LineStyle>",
         //   "       <color>7f00ffff</color>",
         //   "       <width>4</width>",
         //   "   </LineStyle>",
         //   "   <PolyStyle>",
         //   "       <color>7f00ff00</color>",
         //   "   </PolyStyle>",
         //   "   </Style>",
         //   "<Folder>",
         // "<name>zjy</name>",
         //   "       <visibility>1</visibility>",};

         //   List<string> xmlend = new List<string>()
         //   {
         //       "</Folder>",
         //       "</Document>",
         //       "</kml>"
         //   };

         //   List<Vector2D> zxBLList = xyList.Values.ToList();


         //   //生成路线信息图像
         //   List<string> roadList = new List<string>()
         //   {
         //       "   <Placemark>",
         //      // "       <name>线路追踪路径</name>",
         //      // "       <visibility>1</visibility>",
         //      // "       <description>路线信息</description>",
         //       "       <styleUrl>#yellowLineGreenPoly</styleUrl>",
         //       "       <LineString>",
         //       "       <tessellate>1</tessellate>",
         //       "       <coordinates>"
         //   };

         //   foreach (var tmp in zxBLList)
         //   {
         //       roadList.Add("      " + tmp.X.ToString() + "," + tmp.Y.ToString() + ",0.0000");
         //   }
         //   roadList.Add("      </coordinates>");
         //   roadList.Add("      </LineString>");
         //   roadList.Add("  </Placemark>");





         //   //生成标签信息
         //   List<string> roadBQList = new List<string>();
         //   int i = 1;
         //   foreach (var tmp in zxBLList)
         //   {
         //       roadBQList.Add("    <Placemark>");
         //       roadBQList.Add("        <name>" + (i++).ToString() + "</name>");
         //       roadBQList.Add("        <Point>");
         //       roadBQList.Add("        <coordinates>" + tmp.X.ToString() + "," + tmp.Y.ToString() + ",0.0000" + "</coordinates>");
         //       roadBQList.Add("        </Point>");
         //       roadBQList.Add("        <markerStyle>-2</markerStyle>");
         //       roadBQList.Add("     </Placemark>");
         //   }


         //   List<string> kmlFile = new List<string>();
         //   kmlFile.AddRange(xmlhead);
         //   kmlFile.AddRange(roadList);
         //   kmlFile.AddRange(roadBQList);
         //   kmlFile.AddRange(xmlend);

         //   File.WriteAllLines(pmPatth + ".kml", kmlFile.ToArray());

        }

        //WGS84经纬度坐标 转为   高斯投影,可以用======
        //与专业做地形图的图，与通过经纬度经换算后的坐标相同。


        public static zPointXY WGS84_BLToXY(zPointXY zBL, double centureL)
        {
            //centureL 为中央子午线经度
            //  Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
            //纬度
            double BB = zBL.y;
            //经纬度-中央子午线经度
            double LL = zBL.x - centureL;
            double pi = Math.PI;
            double BBr = BB * pi / 180;
            double LLr = LL * pi / 180;
            double sinB = Math.Sin(BBr);
            double cosB = Math.Cos(BBr);
            double t = Math.Tan(BBr);
            //ed.WriteMessage("BB："+BB.ToString()+"\n");
            //ed.WriteMessage("LL："+LL.ToString()+"\n");
            //ed.WriteMessage("sinB："+sinB.ToString()+"\n");
            double a = 6378137.0;
            double b = 6356752.3142;
            double e2 = 1 - (b * b) / (a * a);
            double yita2 = ((a * a) / (b * b) - 1) * cosB * cosB;
            //ed.WriteMessage("e2："+e2.ToString()+"\n");
            //ed.WriteMessage("yita2："+yita2.ToString()+"\n");
            //double pi = Math.PI;

            double W = Math.Sqrt(1 - e2 * sinB * sinB);
            //ed.WriteMessage("WWWWWW："+W.ToString()+"\n");
            double N = a / W;
            //ed.WriteMessage("NNNNNNNN："+N.ToString()+"\n");
            double m0 = a * (1 - e2);
            double m2 = 3.0 / 2.0 * e2 * m0;
            double m4 = 5.0 / 4.0 * e2 * m2;
            double m6 = 7.0 / 6.0 * e2 * m4;
            double m8 = 9.0 / 8.0 * e2 * m6;
            double a0 = m0 + m2 / 2 + 3.0 / 8 * m4 + 5.0 / 16 * m6 + 35.0 / 128 * m8;
            double a2 = m2 / 2 + m4 / 2 + 15.0 / 32 * m6 + 7.0 / 16 * m8;
            double a4 = m4 / 8 + 3.0 / 16 * m6 + 7.0 / 32 * m8;
            double a6 = m6 / 32 + m8 / 16;
            double a8 = m8 / 128;
            //中央子午线经度在纬度为0的坐标，相对于0经度
            double zY = a * centureL * pi / 180.0;
            //X向为纬度方向
            //ed.WriteMessage("a0a0a0a0a0a0："+a0.ToString()+"\n");
            double X = a0 * BBr - sinB * cosB * ((a2 - a4 + a6) + (2 * a4 - 16.0 / 3 * a6) * sinB * sinB + 16.0 / 3 * a6 * sinB * sinB * sinB * sinB);
            //ed.WriteMessage("XXXX坐标："+X.ToString()+"\n");

            double x = X + N / 2 * t * cosB * cosB * LLr * LLr;
            x += N / 24 * t * (5.0 - t * t + 9 * yita2 + 4 * yita2 * yita2) * Math.Pow(cosB, 4) * Math.Pow(LLr, 4);
            x += N / 720 * t * (61.0 - 58.0 * t * t + Math.Pow(t, 4)) * Math.Pow(cosB, 6) * Math.Pow(LLr, 6);

            //Y向为经度方向
            double y = N * cosB * LLr + N / 6 * (1 - t * t + yita2) * cosB * cosB * cosB * LLr * LLr * LLr;
            y += N / 120 * (5 - 18 * t * t + Math.Pow(t, 4) + 14 * yita2 - 58 * yita2 * t * t) * Math.Pow(cosB, 5) * Math.Pow(LLr, 5);
            y += 500000;
            //ed.WriteMessage("y坐标："+x.ToString()+"\n");
            //ed.WriteMessage("x坐标："+y.ToString()+"\n");
            return new zPointXY(y, x);

        }


        public static zPointXY WGS84_XYToBL(zPointXY zBL, double centureL )
        {
            //xx为测量的坐标系
            double xx = zBL.y;
            double yy = zBL.x;

            if (zBL.x > 999999)
            {

                yy -= Math.Truncate(zBL.x / 1000000) * 1000000;
            }
            yy = zBL.x - 500000;

            double a = 6378137.0;
            double b = 6356752.3142;
            double e2 = 1 - (b * b) / (a * a);
            //double yita2 = ((a * a) / (b * b) - 1) * cosB * cosB;

            double m0 = a * (1 - e2);
            double m2 = 3.0 / 2.0 * e2 * m0;
            double m4 = 5.0 / 4.0 * e2 * m2;
            double m6 = 7.0 / 6.0 * e2 * m4;
            double m8 = 9.0 / 8.0 * e2 * m6;
            double a0 = m0 + m2 / 2 + 3.0 / 8 * m4 + 5.0 / 16 * m6 + 35.0 / 128 * m8;
            double a2 = m2 / 2 + m4 / 2 + 15.0 / 32 * m6 + 7.0 / 16 * m8;
            double a4 = m4 / 8 + 3.0 / 16 * m6 + 7.0 / 32 * m8;
            double a6 = m6 / 32 + m8 / 16;
            double a8 = m8 / 128;


            //double Bi = xx / a0;
            //double Bi_1 = 0;
            //while(Math.Abs(Bi_1-Bi)<Math.Pow(1,-10))
            //{
            //   // double Bi = Bi;

            //    //double fBi= a0 * BBr - sinB * cosB * ((a2 - a4 + a6) + (2 * a4 - 16.0 / 3 * a6) * sinB * sinB + 16.0 / 3 * a6 * sinB * sinB * sinB * sinB);
            //    double fBi = a0 * Bi - 0.5 * a2 * Math.Sin(6.0 * Bi) + a4 / 4 * Math.Sin(4 * Bi) - a6 / 6 * Math.Sin(6 * Bi) + a8 / 8 * Math.Sin(8 * Bi);
            //    Bi_1 = (xx - fBi) / a0;
            //}
            double Bi = 0;
            double Bi_1 = xx / a0;

            while (Math.Abs(Bi_1 - Bi) * Math.Pow(10, 20) > 1)
            {
                Bi = Bi_1;
                //double  = Bi;
                //sinB = Math.Sin(Bi);
                // cosB = Math.Cos(Bi);

                //double fBi = - sinB * cosB * ((a2 - a4 + a6) + (2 * a4 - 16.0 / 3 * a6) * sinB * sinB + 16.0 / 3 * a6 * sinB * sinB * sinB * sinB);
                //double fBi = a0 * Bi - 0.5 * a2 * Math.Sin(6.0 * Bi) + a4 / 4 * Math.Sin(4 * Bi) - a6 / 6 * Math.Sin(6 * Bi) + a8 / 8 * Math.Sin(8 * Bi);
                double fBi = -0.5 * a2 * Math.Sin(2.0 * Bi) + a4 / 4 * Math.Sin(4 * Bi) - a6 / 6 * Math.Sin(6 * Bi) + a8 / 8 * Math.Sin(8 * Bi);

                Bi_1 = (xx - fBi) / a0;
            }
            double BBr = Bi_1;
            double t = Math.Tan(BBr);

            double sinB = Math.Sin(BBr);
            double cosB = Math.Cos(BBr);


            double M = a * (1 - e2) * Math.Pow((1 - e2 * sinB * sinB), -1.5);

            double N = a * Math.Pow((1 - e2 * sinB * sinB), -0.5);

            double yita2 = ((a * a) / (b * b) - 1) * cosB * cosB;

            double muti = 1000000;
            double B = BBr * muti - t * muti / (2 * M * N) * yy * yy;
            B += t * muti / (24 * M * N * N * N) * (5 + 3 * t * t + yita2 - 9 * yita2 * t * t) * Math.Pow(yy, 4);
            B -= t * muti / (750 * M * N * N * N * N * N) * (61 + 90 * t * t + 45 * t * t * t * t) * Math.Pow(yy, 6);
            B = B * 180.0 / Math.PI / muti;

            double L = 1 * muti / (N * cosB) * yy - 1 * muti / (6 * N * N * N * cosB) * (1 + 2 * t * t + yita2) * yy * yy * yy;
            L += 1 * muti / (120 * N * N * N * N * N * cosB) * (5 + 28 * t * t + 24 * t * t * t * t + 6 * yita2 + 8 * yita2 * t * t) * Math.Pow(yy, 4);
            L = L * 180.0 / Math.PI / muti;
            L += centureL;
            return new zPointXY(B, L);

        }
        public static List<zPointXY> WGS84_BLToXY(List<zPointXY> jwList, double centureL)
        {
            List<zPointXY> zPList = new List<zPointXY>();
            foreach (zPointXY i in jwList)
            {
                zPList.Add(WGS84_BLToXY(i, centureL));
            }
            return zPList;
        }

        public static List<zPointXY> WGS84_XYToBL(List<zPointXY> jwList, double centureL)
        {
            List<zPointXY> zPList = new List<zPointXY>();
            foreach (zPointXY i in jwList)
            {
                zPList.Add(WGS84_XYToBL(i, centureL));
            }
            return zPList;
        }


        //WGS84经纬度坐标 转为   弧长 坐标,类似于墨卡托,非平面类型
        //直接采用弧长表示坐标系，角度是发生了变化的。
        public static zPointXY WGS84_BLToXY_1(zPointXY zBL)
        {
            //centureL 为中央子午线经度
            //  Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
            //纬度
            double BB = zBL.y;
            //经纬度-中央子午线经度
            double LL = zBL.x;
            double pi = Math.PI;
            double BBr = BB * pi / 180;
            //double LLr = LL*pi/180;
            double sinB = Math.Sin(BBr);
            double cosB = Math.Cos(BBr);
            //double t = Math.Tan(BBr);
            //ed.WriteMessage("BB："+BB.ToString()+"\n");
            //ed.WriteMessage("LL："+LL.ToString()+"\n");
            //ed.WriteMessage("sinB："+sinB.ToString()+"\n");
            double a = 6378137.0;
            double b = 6356752.3142;
            double e2 = 1 - (b * b) / (a * a);
            //double yita2 = ((a*a)/(b*b)-1)*cosB*cosB;
            //ed.WriteMessage("e2："+e2.ToString()+"\n");
            //ed.WriteMessage("yita2："+yita2.ToString()+"\n");
            //double pi = Math.PI;

            double W = Math.Sqrt(1 - e2 * sinB * sinB);
            //ed.WriteMessage("WWWWWW："+W.ToString()+"\n");
            //double N = a/W;
            //ed.WriteMessage("NNNNNNNN："+N.ToString()+"\n");
            double m0 = a * (1 - e2);
            double m2 = 3.0 / 2.0 * e2 * m0;
            double m4 = 5.0 / 4.0 * e2 * m2;
            double m6 = 7.0 / 6.0 * e2 * m4;
            double m8 = 9.0 / 8.0 * e2 * m6;
            double a0 = m0 + m2 / 2 + 3.0 / 8 * m4 + 5.0 / 16 * m6 + 35.0 / 128 * m8;
            double a2 = m2 / 2 + m4 / 2 + 15.0 / 32 * m6 + 7.0 / 16 * m8;
            double a4 = m4 / 8 + 3.0 / 16 * m6 + 7.0 / 32 * m8;
            double a6 = m6 / 32 + m8 / 16;
            double a8 = m8 / 128;
            //中央子午线经度在纬度为0的坐标，相对于0经度
            double zY = a * LL * pi / 180.0;
            //X向为纬度方向
            //ed.WriteMessage("a0a0a0a0a0a0："+a0.ToString()+"\n");
            double X = a0 * BBr - sinB * cosB * ((a2 - a4 + a6) + (2 * a4 - 16.0 / 3 * a6) * sinB * sinB + 16.0 / 3 * a6 * sinB * sinB * sinB * sinB);
            //ed.WriteMessage("XXXX坐标："+X.ToString()+"\n");

            double x = X;//+N/2*t*cosB*cosB*LLr*LLr;
            //x+=N/24*t*(5.0-t*t+9*yita2+4*yita2*yita2)*Math.Pow(cosB,4)*Math.Pow(LLr,4);
            //x+=N/720*t*(61.0-58.0*t*t+Math.Pow(t,4))*Math.Pow(cosB,6)*Math.Pow(LLr,6);

            //Y向为经度方向
            //double y = N*cosB*LLr+N/6*(1-t*t+yita2)*cosB*cosB*cosB*LLr*LLr*LLr;
            //y+=N/120*(5-18*t*t+Math.Pow(t,4)+14*yita2-58*yita2*t*t)*Math.Pow(cosB,5)*Math.Pow(LLr,5);
            double y = zY;
            //ed.WriteMessage("y坐标："+x.ToString()+"\n");
            //ed.WriteMessage("x坐标："+y.ToString()+"\n");
            return new zPointXY(y, x);

        }

        public List<zPointXY> WGS84_BLToXY_1(List<zPointXY> jwList)
        {
            List<zPointXY> zPList = new List<zPointXY>();
            foreach (zPointXY i in jwList)
            {
                zPList.Add(WGS84_BLToXY_1(i));
            }
            return zPList;
        }


        public static void zCreatMtext(string textString, zPointXY location, double height, double width,string layerName)
        {
            MText txt = new MText();
            txt.Location = new Point3d(location.x, location.y, 0);
            txt.TextHeight = height;
            txt.Width = width;
            txt.Contents = textString;
            //txt.
            Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
            Database db = HostApplicationServices.WorkingDatabase;
            var middoc = cadSer.Application.DocumentManager.MdiActiveDocument;
            var doc_lock = middoc.LockDocument();
            //  Polyline polyline = new Polyline();


            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
             
                LayerCreate(db, trans, layerName,false);

                txt.Layer = layerName;


                ToModelSpace(txt, db);
                trans.Commit();
            }
        }
        public static void zCreatText(string textString, zPointXY location, double height, double width_Factor, string layerName)
        {
            DBText txt = new DBText();
            txt.Position = new Point3d(location.x, location.y, 0);
            txt.Height = height;
            txt.WidthFactor = width_Factor;
            txt.TextString = textString;
          
            Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
            Database db = HostApplicationServices.WorkingDatabase;
            var middoc = cadSer.Application.DocumentManager.MdiActiveDocument;
            var doc_lock = middoc.LockDocument();
            //  Polyline polyline = new Polyline();


            using (Transaction trans = db.TransactionManager.StartTransaction())
            {

                LayerCreate(db, trans, layerName,false);

                txt.Layer = layerName;


                ToModelSpace(txt, db);
                trans.Commit();
            }
        }

        public static void zCircleCreate(zPointXY zPoint, double r, string layerName = "zjy_Temp_Biaozhu")
        {

            Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
            Database db = HostApplicationServices.WorkingDatabase;
            var middoc = cadSer.Application.DocumentManager.MdiActiveDocument;
            var doc_lock = middoc.LockDocument();
            //  Polyline polyline = new Polyline();


            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
               // ed.WriteMessage("起终点\n");
                Point3d point3D = new Point3d(zPoint.x, zPoint.y, 0);

                Circle cir = new Circle(point3D, new Vector3d(0, 0, 1), r);
                //是否存在层，无则新建
                //为了放辅助的线
                //string layerName = "zjy_Temp_Biaozhu";
                cir.Color = Color.FromColorIndex(ColorMethod.ByAci, 2);
                LayerCreate(db, trans, layerName,false);

                cir.Layer = layerName;


                ToModelSpace(cir, db);
                trans.Commit();
            }

        }
        public static void zCircleCreate(Point3d point3d, double r, string layerName = "zjy_Temp_Biaozhu")
        {

            zCircleCreate(new zPointXY(point3d.X, point3d.Y), r, layerName);
            

        }
        public static void zCircleCreate(Point2d point2d, double r, string layerName = "zjy_Temp_Biaozhu")
        {

            zCircleCreate(new zPointXY(point2d.X, point2d.Y), r, layerName);


        }

        public static void zPolyLine3DCreate(Point3dCollection point3dCollection,double stack,String layername)
        {

            Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
            Database db = HostApplicationServices.WorkingDatabase;
            var middoc = cadSer.Application.DocumentManager.MdiActiveDocument;
            var doc_lock = middoc.LockDocument();
            //    Polyline polyline = new Polyline();
            try
            {
               
                using (Transaction trans = db.TransactionManager.StartTransaction())
                {

                    BlockTable bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                    BlockTableRecord modelSpace = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;
                
                
                    Polyline3d polyline3D = new Polyline3d(Poly3dType.SimplePoly, point3dCollection, false);

                    //是否存在层，无则新建
                    //为了放辅助的线
                    string appname = "zjy_1_1_1";
                    string layerName = layername;
                    LayerCreate(db, trans, layerName,false);
                    polyline3D.Layer = layerName;




                    //ObjectId returnId = ToModelSpace(polyline3D, db);


                    RegAppTable apptb1 = trans.GetObject(db.RegAppTableId, OpenMode.ForWrite) as RegAppTable;
                    if (!apptb1.Has(appname))
                    {
                        RegAppTableRecord app = new RegAppTableRecord();
                        app.Name = appname;
                        apptb1.Add(app);
                        trans.AddNewlyCreatedDBObject(app, true);
                    
                    }


                    //Entity polyline3D_1 = (Entity)trans.GetObject(returnId, OpenMode.ForWrite);


                    ResultBuffer resultBuffer = new ResultBuffer();
                    resultBuffer.Add(new TypedValue((int)DxfCode.ExtendedDataRegAppName, appname));
                    resultBuffer.Add(new TypedValue((int)DxfCode.ExtendedDataReal, stack));
                    //  polyline3D_1.XData = resultBuffer;
                    polyline3D.XData = resultBuffer;

                  //  ed.WriteMessage(polyline3D.XData.AsArray()[0].Value.ToString());

                    ToModelSpace(polyline3D, db);
                    trans.Commit();
                }
            }catch(System.Exception e)
            {
                ed.WriteMessage("\n" + e.Message + "\n");
            }
        }
        public static void zPolylineCreate(List<zPointXY> list)
        {
            Database db = HostApplicationServices.WorkingDatabase;
            var middoc = cadSer.Application.DocumentManager.MdiActiveDocument;
            var doc_lock = middoc.LockDocument();
            Polyline polyline = new Polyline();

            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                int i = 0;
                foreach (zPointXY point3 in list)
                {
                    Point2d point2D = new Point2d(point3.x, point3.y);
                    polyline.AddVertexAt(i, point2D, 0, 2, 2);
                    i++;
                };

                //是否存在层，无则新建
                //为了放辅助的线
                string layerName = "zjy_Temp_Polyline";
                LayerCreate(db, trans, layerName,false);
                polyline.Layer = layerName;


                ToModelSpace(polyline, db);
                trans.Commit();
            }

        }

        //public static void zCreatLayer(string layername)
        //{

        //    Database db = HostApplicationServices.WorkingDatabase;
        //    var middoc = cadSer.Application.DocumentManager.MdiActiveDocument;
        //    var doc_lock = middoc.LockDocument();
        

        //    using (Transaction trans = db.TransactionManager.StartTransaction())
        //    {
              
        //        LayerCreate(db, trans, layername, false);
                     
        //        trans.Commit();
        //    }

        //}
        public static void zCreatLayer(string layername,bool isPlot)
        {

            Database db = HostApplicationServices.WorkingDatabase;
            var middoc = cadSer.Application.DocumentManager.MdiActiveDocument;
            var doc_lock = middoc.LockDocument();


            using (Transaction trans = db.TransactionManager.StartTransaction())
            {

                LayerCreate(db, trans, layername, isPlot);

                trans.Commit();
            }

        }

        public static void zPolylineCreate(List<zPointXY> list,string layerName)
        {
            Database db = HostApplicationServices.WorkingDatabase;
            var middoc = cadSer.Application.DocumentManager.MdiActiveDocument;
            var doc_lock = middoc.LockDocument();
            Polyline polyline = new Polyline();

            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                int i = 0;
                foreach (zPointXY point3 in list)
                {
                    Point2d point2D = new Point2d(point3.x, point3.y);
                    polyline.AddVertexAt(i, point2D, 0, 2, 2);
                    i++;
                };

                //是否存在层，无则新建
                //为了放辅助的线
              //  string layerName = "zjy_Temp_Polyline";
                LayerCreate(db, trans, layerName,false);
                polyline.Layer = layerName;


                ToModelSpace(polyline, db);
                trans.Commit();
            }

        }
        public zPointXY JW84ToWebMercatorXY(zPointXY jw)
        {
            zPointXY point = new zPointXY();
            double r = 6378137.0;
            point.x = r * jw.x / 180.0 * Math.PI;
            point.y = r * Math.Log(Math.Tan(Math.PI / 4 + jw.y / 180.0 / 2 * Math.PI));
            return point;
        }
        public List<zPointXY> JW84ToWebMercatorXY(List<zPointXY> jwList)
        {
            List<zPointXY> zPList = new List<zPointXY>();
            foreach (zPointXY i in jwList)
            {
                zPList.Add(JW84ToWebMercatorXY(i));
            }
            return zPList;
        }


        private static bool LayerCreate(Database db, Transaction trans, string layername,bool isplottable)
        {
            LayerTable lt = (LayerTable)trans.GetObject(db.LayerTableId, OpenMode.ForWrite);
            string layerName = layername;
            if (!lt.Has(layerName))
            {
                LayerTableRecord ltr = new LayerTableRecord();
                ltr.Name = layerName;
                lt.Add(ltr);
                ltr.Color = Color.FromColorIndex(ColorMethod.ByAci, 7);
                ltr.IsPlottable = isplottable;
                trans.AddNewlyCreatedDBObject(ltr, true);
                return true;
            }
            return false;
        }

        #endregion


        /// <summary>
        /// ///////////////////////////////////////////////////////////////////////////////////////
        /// ///////////////////////////////////////////////////////////////////////////////////////
        /// //////////////////////////////登高线合并////////////////////////////////////////////
        /// ///////////////////////////////////////////////////////////////////////////////////////
        /// ///////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        #region
        [CommandMethod("brt1")]
        public void PolyLineJoin()
        {
            Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
            //两直线合并误差
            double _err_ = 1.0;
            try
            {
                Database db = HostApplicationServices.WorkingDatabase;
                var middoc = cadSer.Application.DocumentManager.MdiActiveDocument;
                List<Polyline> polyLineList = new List<Polyline>();

                ObjectId[] objectIdsArr = ed.GetSelection().Value.GetObjectIds();
                ed.WriteMessage("==================================================\n");
                // ed.WriteMessage(objectIds.Count.ToString() + "\n");
                var doc_lock = middoc.LockDocument();
                DBObject dbo;
                //将选择的登高线保存到polyLineList中，如果所选择的线不是polyline，而是polyline2d则
                //通过提取点重新生成polyline
                foreach (ObjectId tempObjectId in objectIdsArr)
                {
                    using (Transaction trans = db.TransactionManager.StartTransaction())
                    {
                        dbo = trans.GetObject(tempObjectId, OpenMode.ForRead);
                        //trans.Commit();
                        if (dbo.GetType().Equals(typeof(Polyline)))
                        {
                            ed.WriteMessage("找到Polyline\n");
                            Polyline dbo_bR = (Polyline)dbo;
                            polyLineList.Add(dbo_bR);
                        }
                        else if (dbo.GetType().Equals(typeof(Polyline2d)))
                        {
                            Polyline2d dbo_bR = (Polyline2d)dbo;

                            dbo_bR.UpgradeOpen();
                            dbo_bR.ConvertToPolyType(Poly2dType.FitCurvePoly);

                            ed.WriteMessage("找到Polyline2D\n");
                            Polyline polyline = new Polyline();

                            Point3dCollection point3DCollection = new Point3dCollection();
                            dbo_bR.GetStretchPoints(point3DCollection);

                            int i = 0;
                            foreach (Point3d point3 in point3DCollection)
                            {
                                Point2d point2D = new Point2d(point3.X, point3.Y);
                                polyline.AddVertexAt(i, point2D, 0, dbo_bR.DefaultStartWidth, dbo_bR.DefaultEndWidth);
                                i++;
                            };
                            polyline.LayerId = dbo_bR.LayerId;


                            ToModelSpace(polyline, db);
                            RemoveModelSpace(dbo.Id, db);

                        }
                        //  ed.WriteMessage(dbo.GetType().ToString() + "\n");
                        trans.Commit();
                    }

                }
                ed.WriteMessage("===============读取完成=======================\n");

                List<Polyline> polyLineListForUse = new List<Polyline>();
                List<int> closedIndex = new List<int>();
                int closedIndexInt = 0;
                foreach (Polyline polyline1 in polyLineList)
                {
                    if (polyline1.Closed)
                    {
                        polyLineListForUse.Add(polyline1);
                        closedIndex.Add(closedIndexInt);
                    }
                    closedIndexInt++;
                }
                //将所有的闭合曲线放到polyLineListForUse,
                //此时polyLineList中没有闭合曲线
                foreach (int temp in closedIndex)
                {
                    polyLineList.RemoveAt(temp);
                    // RemoveModelSpace(polyLineList[temp].Id, db);
                }




                //合并polyline
                while (polyLineList.Count > 0)
                {
                    ed.WriteMessage("=======未处理的条数{0}======================\n", polyLineList.Count);
                    if (polyLineList.Count == 1)
                    {
                        polyLineListForUse.Add(polyLineList[0]);
                        break;
                    }
                    if (polyLineList[0].Closed)
                    {
                        polyLineListForUse.Add(polyLineList[0]);
                        polyLineList.RemoveAt(0);
                        continue;
                    }


                    while (true)
                    {
                        List<int> usedIndexList = new List<int>();
                        for (int j = 1; j < polyLineList.Count(); j++)
                        {
                            //ed.WriteMessage("===============nn======================\n");
                            Polyline _temp = polyLineList[0];
                            Polyline _temp_1 = polyLineList[0];
                            // ed.WriteMessage("==============tempPL====================\n");
                            if (Compare(ref _temp, polyLineList[j], db, _err_))
                            {
                                usedIndexList.Add(j);
                                RemoveModelSpace(_temp_1.ObjectId, db);
                                polyLineList[0] = _temp;
                            }
                        }
                        //删除已使用的
                        if (usedIndexList.Count == 0)
                        {
                            break;
                        }
                        else
                        {
                            List<Polyline> _zjyTemp = new List<Polyline>();

                            for (int _i = 0; _i < polyLineList.Count; _i++)
                            {
                                if (usedIndexList.Contains(_i))
                                {
                                    RemoveModelSpace(polyLineList[_i].ObjectId, db);
                                    continue;
                                }
                                _zjyTemp.Add(polyLineList[_i]);

                            }
                            usedIndexList.Clear();
                            polyLineList = _zjyTemp;
                        }
                    }
                    polyLineListForUse.Add(polyLineList[0]);
                    polyLineList.RemoveAt(0);

                }

                ed.WriteMessage("合并后的总登高线数：{0}", polyLineListForUse.Count);

                //给已合并的曲线标记为一种颜色
                using (Transaction trans = db.TransactionManager.StartTransaction())
                {
                    foreach (Polyline pLTemp in polyLineListForUse)
                    {
                        Polyline pL = trans.GetObject(pLTemp.ObjectId, OpenMode.ForWrite) as Polyline;
                        pL.Color = Color.FromColorIndex(ColorMethod.ByAci, 5);
                    }
                    trans.Commit();
                }


            }

            catch
            {
                ed.WriteMessage("出错\n");
            }


        }

        //删除对象
        public static bool  RemoveModelSpace(ObjectId objId, Database db)
        {
            Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;

            if (!objId.IsNull)
            {
                using (Transaction trans = db.TransactionManager.StartTransaction())
                {
                    BlockTable bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;

                    Entity ent = trans.GetObject(objId, OpenMode.ForWrite, true) as Entity;
                    if (ent != null)
                    {
                        ent.Erase(true);

                    }


                    trans.Commit();
                }
                return true;
            }
            return false;
        }
        //保存对象到cad数据库
        public static ObjectId ToModelSpace(Entity ent, Database db)
        {
            ObjectId objectId = new ObjectId();
            Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
            try
            {
                if (ent != null)
                {
                    using (Transaction trans = db.TransactionManager.StartTransaction())
                    {
                        BlockTable bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                        BlockTableRecord modelSpace = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;
                      
           
                        try
                        {
                            objectId= modelSpace.AppendEntity(ent);
                            trans.AddNewlyCreatedDBObject(ent, true);
                        }
                        catch
                        {
                        }
                        //modelSpace.is

                        trans.Commit();
                    }
                   // return true;
                }
            }

            catch { ed.WriteMessage("在保存到数据库时出错\n"); }
            return objectId;
        }
        //比较两polyline是否端点是否在误差距离范围内。
        //如果在距离范围内形成一个新的polyline,然后删除其原来的polyline,然后返回true；否则返回false.
        public bool Compare(ref Polyline p_source, Polyline p_append, Database db, double err = 1.0)
        {
            //Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;

            Point3dCollection point3Ds_append = new Point3dCollection();

            p_append.GetStretchPoints(point3Ds_append);

            Point3dCollection point3DSource = new Point3dCollection();
            p_source.GetStretchPoints(point3DSource);

            int index = point3DSource.Count;
            double width = p_append.GetEndWidthAt(0);

            double error = err;
            List<Point3d> pointList = new List<Point3d>();
            #region 
            if (point3Ds_append[0].DistanceTo(point3DSource[0]) <= error)
            {
                for (int i = point3Ds_append.Count - 1; i >= 0; i--)
                {
                    pointList.Add(point3Ds_append[i]);
                }
                foreach (Point3d pt in point3DSource)
                {
                    pointList.Add(pt);
                }
            }
            else if (point3Ds_append[0].DistanceTo(point3DSource[point3DSource.Count - 1]) <= error)
            {
                foreach (Point3d pt in point3DSource)
                {
                    pointList.Add(pt);
                }
                foreach (Point3d pt in point3Ds_append)
                {
                    pointList.Add(pt);
                }

            }
            else if (point3Ds_append[point3Ds_append.Count - 1].DistanceTo(point3DSource[0]) <= error)
            {
                foreach (Point3d pt in point3Ds_append)
                {
                    pointList.Add(pt);
                }
                foreach (Point3d pt in point3DSource)
                {
                    pointList.Add(pt);
                }
            }
            else if (point3Ds_append[point3Ds_append.Count - 1].DistanceTo(point3DSource[point3DSource.Count - 1]) <= error)
            {
                foreach (Point3d pt in point3DSource)
                {
                    pointList.Add(pt);
                }
                for (int i = point3Ds_append.Count - 1; i >= 0; i--)
                {
                    pointList.Add(point3Ds_append[i]);
                }

            }
            else
            {
                return false;
            }

            Polyline pp = new Polyline();
            int index_pp = 0;
            foreach (Point3d temP in pointList)
            {
                Point2d point2 = new Point2d(temP.X, temP.Y);
                pp.AddVertexAt(index_pp, point2, 0.0, width, width);
            }
            // p_source.remo
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                string _layerName = p_source.Layer;
                //Polyline pSourse = trans.GetObject(p_source.ObjectId,OpenMode.ForWrite) as Polyline;
                p_source = pp;

                BlockTable bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                BlockTableRecord modelSpace = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;
                p_source.Layer = _layerName;
                modelSpace.AppendEntity(p_source);
                trans.AddNewlyCreatedDBObject(p_source, true);
                trans.Commit();
            }
            return true;
            #endregion
            #region
            //using(Transaction trans = db.TransactionManager.StartTransaction())
            //{
            //    Polyline pSourse = trans.GetObject(p_source.ObjectId, OpenMode.ForWrite) as Polyline;
            //    Polyline pAppend = trans.GetObject(p_append.ObjectId, OpenMode.ForWrite) as Polyline;
            //    // ed.WriteMessage("===============Compare=======================\n");
            //    // ed.WriteMessage("==============={0}=======================\n", pSourse.EndPoint.DistanceTo(pAppend.StartPoint));

            //    Polyline pp = new Polyline();
            //    //if (point3Ds[0].DistanceTo(point3DSource[0]) <= error)
            //    //{

            //    //}

            //    if (point.DistanceTo(pAppend.StartPoint) <= error)
            //    {
            //        foreach (Point3d point3D in point3Ds_append)
            //        {

            //            // ed.WriteMessage("===============bb=======================\n");
            //            Point2d point2 = new Point2d(point3D.X, point3D.Y);
            //            pSourse.AddVertexAt(index++, point2, 0.0, width, width);
            //              ed.WriteMessage("===============bb1=======================\n");
            //        }
            //        RemoveModelSpace(pAppend.ObjectId,db);
            //        trans.Commit();
            //        return pSourse;
            //    }
            //    else if (pSourse.EndPoint.DistanceTo(pAppend.EndPoint) <= error)
            //    {
            //      //  ed.WriteMessage("cc\n");
            //        //ed.WriteMessage("pAppend.EndPoint：{0}\n", pAppend.EndPoint.ToString());
            //        //ed.WriteMessage("point3Ds[point3Ds.Count-1]：{0}\n", point3Ds[point3Ds.Count-1].ToString());


            //        //ed.WriteMessage("pSourse.StartPoint：{0}\n", pSourse.StartPoint.ToString());
            //        //ed.WriteMessage("pSourse.endpoint：{0}\n", pSourse.EndPoint.ToString());
            //        //  if()
            //        for (int i = point3Ds_append.Count - 1; i >= 0; i--)
            //        {
            //            ed.WriteMessage("cc1\n");
            //            Point2d point2 = new Point2d(point3Ds_append[i].X,point3Ds_append[i].Y);
            //           // ed.WriteMessage("ed:{0}\n", index);
            //            pSourse.AddVertexAt(index++, point2, 0.0, width, width);
            //           // ed.WriteMessage("point2：{0}\n", point2.ToString());
            //            //ed.WriteMessage("pSourse.endpoint：{0}\n", pSourse.EndPoint.ToString());

            //        }
            //        //RemoveModelSpace(pAppend.ObjectId,db);
            //        trans.Commit();
            //        return pSourse;
            //    }
            //    else if (pSourse.StartPoint.DistanceTo(pAppend.EndPoint) <= error)
            //    {
            //        //ed.WriteMessage("===============e=======================\n");
            //        foreach (Point3d point3D in point3DSource)
            //        {
            //            ed.WriteMessage("===============ee=======================\n");
            //            Point2d point2 = new Point2d(point3D.X, point3D.Y);
            //            pAppend.AddVertexAt(index++, point2, 0.0, width, width);
            //        }
            //        ed.WriteMessage("===============eeee=======================\n");
            //        // RemoveModelSpace(pSourse.ObjectId,db);
            //        pSourse=pAppend;
            //        trans.Commit();
            //       // pSourse=pAppend;
            //        return pSourse;
            //    }
            //    else if (pSourse.StartPoint.DistanceTo(pAppend.StartPoint) <= error)
            //    {
            //        Polyline pL = new Polyline();
            //        int index_ss = 0;
            //       // ed.WriteMessage("===============bb======================\n");
            //        for (int i = point3Ds_append.Count - 1; i >= 0; i--)
            //        {
            //            pL.AddVertexAt(index_ss++, new Point2d(point3Ds_append[i].X,point3Ds_append[i].Y), 0.0, width, width);
            //        }
            //        foreach (Point3d pt in point3DSource)
            //        {
            //            pL.AddVertexAt(index_ss++, new Point2d(pt.X, pt.Y), 0.0, width, width);
            //        }

            //        pSourse = pL;
            //        //RemoveModelSpace(pAppend.ObjectId,db);
            //        trans.Commit();
            //        return pSourse;
            //    }
            //   // ed.WriteMessage("bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb");
            //    trans.Commit();
            //}
            //return null;
            #endregion
        }

        [CommandMethod("brt2")]
        public void PolyLineElevation()
        {
            Document midoc = cadSer.Application.DocumentManager.MdiActiveDocument;
            Editor ed = midoc.Editor;
            Database db = HostApplicationServices.WorkingDatabase;
            //PromptPointResult selectResultStart = ed.GetPoint("选择起点:\n");
            //Point3d startP3d = selectResultStart.Value;
            //PromptPointResult selectResultEnd = ed.GetPoint("选择终点:\n");
            //Point3d endP3d = selectResultEnd.Value;
            PromptEntityResult selectResult = ed.GetEntity("选择用于登高线赋值的辅助直线,必须为line:\n");

            //List<KeyValuePair<Polyline, Point3d>> dgxPolyLine = new List<KeyValuePair<Polyline, Point3d>>();

            using (var trs = db.TransactionManager.StartTransaction())
            {
                Entity obj = (Entity)trs.GetObject(selectResult.ObjectId, OpenMode.ForRead);
                if (!(obj is Line))
                {
                    ed.WriteMessage("不是Line，重新选择!\n");
                    return;

                }


                BlockTable bt = (BlockTable)trs.GetObject(db.BlockTableId, OpenMode.ForWrite);
                ObjectId obj_id = bt[BlockTableRecord.ModelSpace];
                BlockTableRecord btr = (BlockTableRecord)trs.GetObject(obj_id, OpenMode.ForWrite);
                //DBObjectCollection dboAllObject = trs.GetAllObjects();
                List<KeyValuePair<Polyline, Point3d>> dgxPolyLine = new List<KeyValuePair<Polyline, Point3d>>();

                Line zlineOBJ = (Line)obj;

                foreach (ObjectId tmpObjID in btr)
                {
                    DBObject tmpObj = trs.GetObject(tmpObjID, OpenMode.ForWrite);
                    if ((tmpObj is Polyline))//&&(tmpObj.Layer.ToUpper() == "SQX" || tmpObj.Layer.ToUpper() == "JQX"))//只设置在SQX和JQX的图层里)
                    {
                        Polyline zpolyLineTmp = (Polyline)tmpObj;
                        if (zpolyLineTmp.Layer.ToUpper() == "SQX" || zpolyLineTmp.Layer.ToUpper() == "JQX")
                        {
                            Point3dCollection intersectPoints = new Point3dCollection();
                            zpolyLineTmp.IntersectWith(zlineOBJ, Intersect.OnBothOperands, new Plane(new Point3d(0, 0, 0), new Vector3d(0, 0, 1)), intersectPoints, 0, 0);
                            if (intersectPoints.Count == 0) continue;
                            //TODO:
                            //只考虑有一个交点,有多个交点时也需要按与起始点的距离排序
                            //找到最近的点
                            //List<KeyValuePair< Point3d>> dgxPolyLine = new List<KeyValuePair<Polyline, Point3d>>();


                            List<Point3d> intersectPointsList = new List<Point3d>();
                            //将相交点平面化
                            for (int zsort_i = 0; zsort_i < intersectPoints.Count ; zsort_i++)
                            {
                                        Point3d tmp_swap = intersectPoints[zsort_i];
                                intersectPoints[zsort_i] = new Point3d(tmp_swap.X,tmp_swap.Y,0);
                                intersectPointsList.Add(new Point3d(tmp_swap.X, tmp_swap.Y, 0));
                                //zCreatMtext("与原点距离:" + intersectPoints[zsort_i].DistanceTo(new Point3d(zlineOBJ.StartPoint.X, zlineOBJ.StartPoint.Y, 0)).ToString(), new zPointXY(tmp_swap.X, tmp_swap.Y-2), 2, 20);
                            }

                            Point3d zStartP3d_tmp = new Point3d(zlineOBJ.StartPoint.X, zlineOBJ.StartPoint.Y, 0);

                            intersectPointsList.Sort((Point3d aa, Point3d bb) =>
                            {
                                if (zStartP3d_tmp.DistanceTo(aa) > zStartP3d_tmp.DistanceTo(bb))
                                {
                                    return 1;
                                }
                                else if (zStartP3d_tmp.DistanceTo(aa) < zStartP3d_tmp.DistanceTo(bb))
                                {
                                    return -1;
                                }
                                else
                                {
                                    return 0;
                                }
                            }
                                );
                            //冒泡排序?


                            //无法排序不知道什么原因
                            //Point3d zStartP3d_tmp = new Point3d(zlineOBJ.StartPoint.X, zlineOBJ.StartPoint.Y, 0);
                            //Point3d zEndP3d_tmp = new Point3d(zlineOBJ.EndPoint.X, zlineOBJ.EndPoint.Y, 0);
                            for (int zsort_i=0;zsort_i<intersectPoints.Count-1;zsort_i++)
                            {
                                for(int zsort_j=zsort_i;zsort_j<intersectPoints.Count-1;zsort_j++)
                                {
                                    if(intersectPoints[zsort_j].DistanceTo(zStartP3d_tmp)>intersectPoints[zsort_j + 1].DistanceTo(zStartP3d_tmp))
                                    {
                                        //ed.WriteMessage("节点距离1："+ intersectPoints[zsort_j].DistanceTo(zStartP3d_tmp).ToString()+"\n");
                                        //ed.WriteMessage("节点距离2：" + intersectPoints[zsort_j+1].DistanceTo(zStartP3d_tmp).ToString() + "\n");
                                        Point3d tmp_swap = intersectPoints[zsort_j];
                                        intersectPoints[zsort_j] = intersectPoints[zsort_j + 1];
                                        intersectPoints[zsort_j + 1] = tmp_swap;
                                    }
                                }
                            }


                            //ed.WriteMessage("节点个数：" + intersectPoints.Count.ToString()+ "\n");
                            //ed.WriteMessage("相交点的z1坐标:"+intersectPoints[0].Z.ToString()+"\n");
                            dgxPolyLine.Add(new KeyValuePair<Polyline, Point3d>(zpolyLineTmp, intersectPointsList[0]));
                        }
                    }
                }


                //double zDgj =Math.Floor(Math.Abs(zlineOBJ.EndPoint.Z - zlineOBJ.StartPoint.Z) / dgxPolyLine.Count);
                double zDgj = zlineOBJ.EndPoint.Z - zlineOBJ.StartPoint.Z;


                //登高距 
                List<int> dgsList = new List<int> { 100,50, 20, 10, 5, 2, 1 };
                for (int  zTmp_i=0;zTmp_i<dgsList.Count; zTmp_i++)
                {
                    //double aaaa = Math.Floor(Math.Abs(zDgj) / zTmp_i);
                    //double bbbb= zTmp_i * 1.0;
                    //if (Math.Abs(zDgj) / dgsList[zTmp_i]> dgxPolyLine.Count-1&& Math.Abs(zDgj) / dgsList[zTmp_i+1]<dgsList.Count+1)
                    if (Math.Abs(zDgj) / dgsList[zTmp_i]> dgxPolyLine.Count+1)
                    {
                        zDgj = dgsList[zTmp_i-1] * Math.Sign(zDgj);
                        break;
                    }

                }

                if (zDgj - Math.Floor(zDgj) > 0.0001)
                {
                    ed.WriteMessage("等高距列表错误,重新设置\n");
                    return;
                }



                double zStartElevation = 0;
                Point3d zStartP3d = new Point3d(zlineOBJ.StartPoint.X, zlineOBJ.StartPoint.Y, 0);
                Point3d zEndP3d = new Point3d(zlineOBJ.EndPoint.X, zlineOBJ.EndPoint.Y, 0);
                dgxPolyLine.Sort((KeyValuePair<Polyline, Point3d> aa, KeyValuePair<Polyline, Point3d> bb) =>
                {
                    if (zStartP3d.DistanceTo(aa.Value) > zStartP3d.DistanceTo(bb.Value))
                    {
                        return 1;
                    }
                    else if (zStartP3d.DistanceTo(aa.Value) < zStartP3d.DistanceTo(bb.Value))
                    {
                        return -1;
                    }
                    else
                    {
                        return 0;
                    }
                });

                int i_sort = 0;
                foreach (var tmp in dgxPolyLine)
                {
                    zCircleCreate(new zPointXY(tmp.Value.X, tmp.Value.Y), 1);
                    zCreatMtext("顺序:" + (i_sort++).ToString(), new zPointXY(tmp.Value.X, tmp.Value.Y), 2, 10, "zjy_dgx_info");
                }

                if (zDgj > 0)
                {
                    //zDgj = 5.0;
                    zStartElevation = (Math.Floor(zlineOBJ.StartPoint.Z / zDgj)) * zDgj;
                }
                else
                {
                    //zDgj = -5.0;
                    zStartElevation = Math.Floor(zlineOBJ.StartPoint.Z / Math.Abs(zDgj) + 1) * Math.Abs(zDgj);
                }

                double dgj_differ = zDgj;
                foreach (var tmp_KVP in dgxPolyLine)
                {
                    Polyline pline = (Polyline)trs.GetObject(tmp_KVP.Key.ObjectId, OpenMode.ForWrite);
                    pline.Color = Color.FromColorIndex(ColorMethod.ByAci, 8);
                    pline.Elevation = zStartElevation + dgj_differ;
                    ed.WriteMessage("zDgj:" + zDgj.ToString() + "\n");
                    dgj_differ += zDgj;
                    ed.WriteMessage("pline.Elevation:" + pline.Elevation.ToString() + "\n");

                    zCreatMtext(pline.Elevation.ToString(), new zPointXY(tmp_KVP.Value.X, tmp_KVP.Value.Y+2), 2, 10, "zjy_dgx_info");

                }
                trs.Commit();   //trs.C/*o*/mmit();
            }//obj is Line
               
            

         

        }

        #endregion



        /// <summary>
        /// ///////////////////////////////////////////////////////////////////////////////////////
        /// ///////////////////////////////////////////////////////////////////////////////////////
        /// //////////////////////////////批量打印////////////////////////////////////////////
        /// ///////////////////////////////////////////////////////////////////////////////////////
        /// ///////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        //bool ISContinuePrint = true;

        //打印布局中的平面图
        //[CommandMethod("ppt")]
        [CommandMethod("ppt", CommandFlags.Session)]
        public void zjyPrint()
        {
            string zjyProjectName = @"E:\古县项目20200331-修改 - 副本\";
            string[] dirList = Directory.GetDirectories(zjyProjectName);//,@"*.dwg");

            //Regex rr = new Regex(@"[^0-9]");

            List<string> dirUseList = new List<string>();
            foreach (string dirTmp in dirList)
            {

                //if(rr.IsMatch(dirTmp))
                {
                    dirUseList.Add(dirTmp);
                }
            }
            dirUseList.Sort();

            foreach (string ii in dirUseList)
            {
                string[] dwgFile = Directory.GetFiles(ii, "*.dwg");
                foreach (string _tmp_dwg in dwgFile)
                {
                    var middoc = cadSer.Application.DocumentManager.Open(_tmp_dwg, false);
                    var db = HostApplicationServices.WorkingDatabase;

                    HostApplicationServices.WorkingDatabase = middoc.Database;

                    #region
                    //dbb.ReadDwgFile (_tmp_dwg,FileShare.Read,false,null);

                    //middoc.Window.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    //HostApplicationServices.WorkingDatabase = dbb;
                    //zjy_event(dbb,db);
                    //可以正常关闭了
                    #endregion

                    zjy_event();
                    //middoc.CloseAndDiscard();
                    HostApplicationServices.WorkingDatabase = db;

                }

                #region  打印代码段
                //Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
                //    ed.WriteMessage(ii+"\n");
                //Database db =middoc.Database ;//HostApplicationServices.WorkingDatabase;

                //LayoutManager lb = LayoutManager.Current;
                //    List<Layout> zLayoutList = new List<Layout>();
                //    using(Transaction trs = db.TransactionManager.StartTransaction())
                //    {

                //        //BlockTableRecord brt =trs.GetObject(db.get)
                //        ObjectId paperId = SymbolUtilityServices.GetBlockPaperSpaceId(db);
                //        // BlockTable bt=trs.GetObject()
                //        //  BlockTableRecord btr = trs.GetObject(paperId,OpenMode.ForRead) as BlockTableRecord;
                //        BlockTable bt = db.BlockTableId.GetObject(OpenMode.ForRead) as BlockTable;

                //        //List<Layout> zLayoutList = new List<Layout>();
                //        foreach(ObjectId tempObj in bt)
                //        {
                //            BlockTableRecord btr = tempObj.GetObject(OpenMode.ForRead) as BlockTableRecord;
                //            if(btr.IsLayout)
                //            {
                //                Layout la = trs.GetObject(btr.LayoutId,OpenMode.ForRead) as Layout;
                //                if(la.LayoutName.Contains("平面图"))
                //                {
                //                    zLayoutList.Add(la);
                //                    // ed.WriteMessage(la.LayoutName+"\n");
                //                }

                //            }

                //        }

                //        //zLayoutList.Sort(zLayoutCompare);


                //        trs.Commit();
                //    }

                //    zLayoutList.Sort((Layout x,Layout y) =>
                //    {
                //        if(x.LayoutName.CompareTo(y.LayoutName)<0)
                //        {
                //            return -1;
                //        }
                //        else if(x.LayoutName.CompareTo(y.LayoutName)>0)
                //        {
                //            return 1;
                //        }
                //        return 0;

                //    });

                //    foreach(Layout _tmpL in zLayoutList)
                //    {
                //        ed.WriteMessage("开始打印布局页："+_tmpL.LayoutName+"\n");
                //    }
                //    ed.WriteMessage("============================================"+"\n");

                //    short bgPlot = (short)Application.GetSystemVariable("BACKGROUNDPLOT");
                //    Application.SetSystemVariable("BACKGROUNDPLOT",0);
                //    var dloc = middoc.LockDocument();

                //    using(Transaction trs = db.TransactionManager.StartTransaction())
                //    {
                //        //int i = 0;
                //        //foreach(Layout ly in zLayoutList)
                //        //{
                //        //    LayoutPlot(trs,ly,@"d:\123_"+(i++));
                //        //}
                //        while(true)
                //        {
                //            if(PlotFactory.ProcessPlotState==ProcessPlotState.NotPlotting)
                //            {
                //                foreach(Layout ly in zLayoutList)
                //                {
                //                    LayoutPlot(trs,ly,middoc,@"d:\123_"+ly);
                //                }
                //                break;
                //            }
                //            Thread.Sleep(500);
                //        }
                //    }

                //    dloc.Dispose();
                //    Application.SetSystemVariable("BACKGROUNDPLOT",bgPlot);
                #endregion
                #region

                //using(PlotEngine pe = PlotFactory.CreatePublishEngine())
                //{
                //    using(PlotProgressDialog ppd = new PlotProgressDialog(false,1,true))

                //    {
                //        ppd.set_PlotMsgString(PlotMessageIndex.DialogTitle,"Custom Plot Progress");
                //        ppd.set_PlotMsgString(PlotMessageIndex.CancelJobButtonMessage,"Cancel Job");
                //        ppd.set_PlotMsgString(PlotMessageIndex.CancelSheetButtonMessage,"Cancel Sheet");
                //        ppd.set_PlotMsgString(PlotMessageIndex.SheetSetProgressCaption,"Sheet Set Progress");
                //        ppd.set_PlotMsgString(PlotMessageIndex.SheetProgressCaption,"Sheet Progress");
                //        ppd.LowerPlotProgressRange=0;
                //        ppd.UpperPlotProgressRange=100;
                //        ppd.PlotProgressPos=0;

                //        //最后让我们来启动打印                

                //        ppd.OnBeginPlot();

                //        ppd.IsVisible=false;

                //        pe.BeginPlot(ppd,null);

                //        foreach(Layout _tmpL in zLayoutList)
                //        {

                //            ed.WriteMessage("开始打印布局页："+_tmpL.LayoutName+"\n");
                //            lb.CurrentLayout=_tmpL.LayoutName;
                //            //cadSer.Application.DocumentManager.MdiActiveDocument
                //            ed.WriteMessage(_tmpL.LayoutName+"\n");

                //            PlotInfo zPInfo = new PlotInfo();
                //            zPInfo.Layout=_tmpL.ObjectId;

                //            PlotSettings ps = new PlotSettings(_tmpL.ModelType);

                //            ps.CopyFrom(_tmpL);

                //            PlotSettingsValidator psv = PlotSettingsValidator.Current;

                //            psv.SetPlotType(ps,Autodesk.AutoCAD.DatabaseServices.PlotType.Extents);
                //            psv.SetPlotCentered(ps,true);
                //            psv.SetUseStandardScale(ps,true);
                //            psv.SetPlotConfigurationName(ps,"pdfFactory Pro","A3");
                //            zPInfo.OverrideSettings=ps;

                //            PlotInfoValidator piv = new PlotInfoValidator();
                //            piv.MediaMatchingPolicy=MatchingPolicy.MatchEnabled;
                //            piv.Validate(zPInfo);
                //            if(true)//PlotFactory.ProcessPlotState==ProcessPlotState.NotPlotting)
                //            {


                //                pe.BeginDocument(zPInfo,middoc.Name,null,1,true,"d:\\123"+_tmpL.LayoutName);


                //                PlotPageInfo ppi = new PlotPageInfo();

                //                pe.BeginPage(ppi,zPInfo,true,null);
                //                pe.BeginGenerateGraphics(null);
                //                pe.EndGenerateGraphics(null);

                //                // 结束图纸              

                //                pe.EndPage(null);

                //                //ppd.SheetProgressPos=100;

                //                ppd.OnEndSheet();
                //            }
                //        }



                //    }

                //    //PlotInfo zPInfo = new PlotInfo();

                //}

                #endregion
                #region
                //while(true)
                //{
                //    if(true)//PlotFactory.ProcessPlotState==ProcessPlotState.NotPlotting)
                //    {
                //        using(PlotEngine pe = PlotFactory.CreatePublishEngine())
                //        {
                //            PlotProgressDialog ppd = new PlotProgressDialog(false,1,true);
                //            using(ppd)

                //            {
                //                foreach(Layout _tmpL in zLayoutList)
                //            {

                //                ed.WriteMessage("开始打印布局页："+_tmpL.LayoutName+"\n");
                //                lb.CurrentLayout=_tmpL.LayoutName;
                //                //cadSer.Application.DocumentManager.MdiActiveDocument
                //                ed.WriteMessage(_tmpL.LayoutName+"\n");

                //                PlotInfo zPInfo = new PlotInfo();
                //                zPInfo.Layout=_tmpL.ObjectId;

                //                PlotSettings ps = new PlotSettings(_tmpL.ModelType);

                //                ps.CopyFrom(_tmpL);

                //                PlotSettingsValidator psv = PlotSettingsValidator.Current;
                //                psv.SetPlotType(ps,Autodesk.AutoCAD.DatabaseServices.PlotType.Extents);
                //                psv.SetPlotCentered(ps,true);
                //                //psv.SetUseStandardScale(ps,true);

                //                psv.SetPlotConfigurationName(ps,"pdfFactory Pro","A3");

                //                zPInfo.OverrideSettings=ps;

                //                PlotInfoValidator piv = new PlotInfoValidator();
                //                piv.MediaMatchingPolicy=MatchingPolicy.MatchEnabled;
                //                piv.Validate(zPInfo);
                //                    #region
                //                    ppd.set_PlotMsgString(PlotMessageIndex.DialogTitle,"Custom Plot Progress");

                //                    ppd.set_PlotMsgString(PlotMessageIndex.CancelJobButtonMessage,"Cancel Job");

                //                    ppd.set_PlotMsgString(PlotMessageIndex.CancelSheetButtonMessage,"Cancel Sheet");

                //                    ppd.set_PlotMsgString(PlotMessageIndex.SheetSetProgressCaption,"Sheet Set Progress");

                //                    ppd.set_PlotMsgString(PlotMessageIndex.SheetProgressCaption,"Sheet Progress");

                //                    ppd.LowerPlotProgressRange=0;

                //                    ppd.UpperPlotProgressRange=100;

                //                    ppd.PlotProgressPos=0;
                //                    #endregion
                //                    ////最后让我们来启动打印                

                //                    ppd.OnBeginPlot();

                //                    ppd.IsVisible=true;

                //                    pe.BeginPlot(ppd,null);

                //                    // 我们将打印一个单文档              

                //                    pe.BeginDocument(zPInfo,middoc.Name,null,1,false,"d:\\123");

                //                    //只包含一个图纸               

                //                    ppd.OnBeginSheet();

                //                    ppd.LowerSheetProgressRange=0;

                //                    ppd.UpperSheetProgressRange=100;

                //                    ppd.SheetProgressPos=0;

                //                    PlotPageInfo ppi = new PlotPageInfo();

                //                    pe.BeginPage(ppi,zPInfo,true,null);

                //                    pe.BeginGenerateGraphics(null); pe.EndGenerateGraphics(null);

                //                    // 结束图纸              

                //                    pe.EndPage(null);

                //                    ppd.SheetProgressPos=100;

                //                    ppd.OnEndSheet();

                //                    // 结束文档  
                //                    pe.EndDocument(null);
                //                    ppd.OnEndPlot();



                //                }




                //                pe.EndPlot(null);
                //                //pe.EndDocument(null);

                //                // 结束打印               

                //                //ppd.PlotProgressPos=100;

                //                // ppd.OnEndPlot();

                //                //pe.EndPlot(null);
                //                //Thread.Sleep(1000);
                //            }
                //         }

                //        break;
                //    }
                //    Thread.Sleep(1000);
                //}
                #endregion
                #region
                //while(true)
                //{
                //if(PlotFactory.ProcessPlotState==ProcessPlotState.NotPlotting)//PlotFactory.ProcessPlotState==ProcessPlotState.NotPlotting)
                //{

                //foreach(Layout _tmpL in zLayoutList)
                //{

                //    ed.WriteMessage("开始打印布局页："+_tmpL.LayoutName+"\n");
                //    lb.CurrentLayout=_tmpL.LayoutName;
                //    //cadSer.Application.DocumentManager.MdiActiveDocument
                //    //ed.WriteMessage(_tmpL.LayoutName+"\n");

                //    using(PlotInfo zPInfo = new PlotInfo())
                //    {
                //        zPInfo.Layout=_tmpL.ObjectId;

                //        using(PlotSettings ps = new PlotSettings(_tmpL.ModelType))
                //        {

                //            ps.CopyFrom(_tmpL);

                //            PlotSettingsValidator psv = PlotSettingsValidator.Current;
                //            psv.SetPlotType(ps,Autodesk.AutoCAD.DatabaseServices.PlotType.Extents);
                //            psv.SetPlotCentered(ps,true);
                //            psv.SetUseStandardScale(ps,true);
                //            psv.SetPlotRotation(ps,PlotRotation.Degrees090);

                //            psv.SetPlotConfigurationName(ps,"pdfFactory Pro","A3");

                //            zPInfo.OverrideSettings=ps;

                //            using(PlotInfoValidator piv = new PlotInfoValidator())
                //            {
                //                piv.MediaMatchingPolicy=MatchingPolicy.MatchEnabled;
                //                piv.Validate(zPInfo);
                //                #region
                //                using(PlotEngine pe = PlotFactory.CreatePublishEngine())
                //                {
                //                    using(PlotProgressDialog ppd = new PlotProgressDialog(false,1,false))
                //                    {
                //                        //ppd.set_PlotMsgString(PlotMessageIndex.DialogTitle,"Custom Plot Progress");

                //                        //ppd.set_PlotMsgString(PlotMessageIndex.CancelJobButtonMessage,"Cancel Job");

                //                        //ppd.set_PlotMsgString(PlotMessageIndex.CancelSheetButtonMessage,"Cancel Sheet");

                //                        //ppd.set_PlotMsgString(PlotMessageIndex.SheetSetProgressCaption,"Sheet Set Progress");

                //                        //ppd.set_PlotMsgString(PlotMessageIndex.SheetProgressCaption,"Sheet Progress");

                //                        //ppd.LowerPlotProgressRange=0;

                //                        //ppd.UpperPlotProgressRange=100;

                //                        //ppd.PlotProgressPos=0;
                //                        //#endregion
                //                        //////最后让我们来启动打印                

                //                        //ppd.OnBeginPlot();

                //                        //ppd.IsVisible=true;

                //                        pe.BeginPlot(null,null);

                //                        // 我们将打印一个单文档              

                //                        pe.BeginDocument(zPInfo,middoc.Name,null,1,true,"d:\\123");

                //                        //只包含一个图纸               

                //                        //ppd.OnBeginSheet();

                //                        //ppd.LowerSheetProgressRange=0;

                //                        //ppd.UpperSheetProgressRange=100;

                //                        //ppd.SheetProgressPos=0;

                //                        using(PlotPageInfo ppi = new PlotPageInfo())
                //                        {
                //                            pe.BeginPage(ppi,zPInfo,true,null);
                //                        }


                //                        pe.BeginGenerateGraphics(null);
                //                        pe.EndGenerateGraphics(null);

                //                        // 结束图纸              

                //                        pe.EndPage(null);

                //                        //ppd.SheetProgressPos=100;

                //                        //ppd.OnEndSheet();

                //                        // 结束文档  
                //                        pe.EndDocument(null);
                //                        //ppd.OnEndPlot();
                //                        pe.EndPlot(null);
                //                    }

                //                }
                //            }
                //            _tmpL.CopyFrom(ps);
                //        }
                //    }





                //    //pe.EndDocument(null);

                //    // 结束打印               

                //    //ppd.PlotProgressPos=100;

                //    // ppd.OnEndPlot();

                //    //pe.EndPlot(null);
                //    //Thread.Sleep(1000);

                //    //    }
                //    //    break;
                //}
                //}
                //Thread.Sleep(1000);
                //}
                #endregion


            }
        }

        [CommandMethod("ppt2", CommandFlags.Session)]
        public void zjy_event()//(Database workDB,Database currenDB)//(object sender,DocumentCollectionEventArgs e)
        {

            Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;

            Document middoc = cadSer.Application.DocumentManager.MdiActiveDocument;
            Database db = HostApplicationServices.WorkingDatabase;

            LayoutManager lb = LayoutManager.Current;
            List<Layout> zLayoutList = new List<Layout>();
            using (Transaction trs = db.TransactionManager.StartTransaction())
            {
                ObjectId paperId = SymbolUtilityServices.GetBlockPaperSpaceId(db);
                BlockTable bt = db.BlockTableId.GetObject(OpenMode.ForRead) as BlockTable;

                foreach (ObjectId tempObj in bt)
                {
                    BlockTableRecord btr = tempObj.GetObject(OpenMode.ForRead) as BlockTableRecord;
                    if (btr.IsLayout)
                    {
                        Layout la = trs.GetObject(btr.LayoutId, OpenMode.ForRead) as Layout;
                        if (la.LayoutName.Contains("平面图"))
                        {
                            zLayoutList.Add(la);
                        }

                    }

                }

                trs.Commit();
            }

            zLayoutList.Sort((Layout x, Layout y) =>
            {
                if (x.LayoutName.CompareTo(y.LayoutName) < 0)
                {
                    return -1;
                }
                else if (x.LayoutName.CompareTo(y.LayoutName) > 0)
                {
                    return 1;
                }
                return 0;

            });

            foreach (Layout _tmpL in zLayoutList)
            {
                ed.WriteMessage("开始打印布局页：" + _tmpL.LayoutName + "\n");
            }
            ed.WriteMessage("============================================" + "\n");

            short bgPlot = (short)Application.GetSystemVariable("BACKGROUNDPLOT");
            Application.SetSystemVariable("BACKGROUNDPLOT", 0);
            var dloc = middoc.LockDocument();

            using (Transaction trs = db.TransactionManager.StartTransaction())
            {

                foreach (Layout ly in zLayoutList)
                {
                    LayoutPlot(trs, ly, @"d:\123_" + ly.LayoutName);
                    //ed.WriteMessage("aaaaaaaaaaaaaaaaaaaaaa+\n");
                }
                //ISContinuePrint=false;

            }

            dloc.Dispose();
            Application.SetSystemVariable("BACKGROUNDPLOT", bgPlot);

            //cadSer.Application.DocumentManager.DocumentActivated-=zjy_event;

        }

        #region 
        private void LayoutPlot(Transaction tr, Layout lo, string filename)//, Database workDB)
        {
            //Database db =DockSides. ;//cadSer.Application.DocumentManager.MdiActiveDocument.Database;
            var doc = cadSer.Application.DocumentManager.MdiActiveDocument;
            Database db = HostApplicationServices.WorkingDatabase;
            Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;

            //HostApplicationServices.WorkingDatabase = db;

            using (Transaction acTrans = db.TransactionManager.StartTransaction())
            {
                DBDictionary plSettings = acTrans.GetObject(db.PlotSettingsDictionaryId,
                                                            OpenMode.ForRead) as DBDictionary;



                // List each named page setup
                foreach (DBDictionaryEntry item in plSettings)
                {
                    ed.WriteMessage("\n  " + item.Key);
                }

                // Abort the changes to the database
                acTrans.Abort();
            }


            LayoutManager.Current.CurrentLayout = lo.LayoutName;

            //var plotDeviceName = "pdfFactory Pro";

            var plotDeviceName = "DocuCentre-V 3060";


            string mediaName = "A3";
            //var styleSheetName = "monochrome.stb";


            using (var ps = new PlotSettings(lo.ModelType))
            {

                ps.CopyFrom(lo);

                  PlotRotation plotRotation = PlotRotation.Degrees090;
                //PlotRotation plotRotation = PlotRotation.Degrees000;
                //PlotConfigManager.RefreshList(RefreshCode.All);
                PlotConfigManager.SetCurrentConfig(plotDeviceName);
                //PlotConfigManager.CurrentConfig.RefreshMediaNameList();



                var psv = PlotSettingsValidator.Current;
                var device_list = psv.GetPlotDeviceList();
                foreach (string i in device_list)
                {
                    ed.WriteMessage("===" + i + "==========\n");
                }
                var devece_size = psv.GetCanonicalMediaNameList(ps);
                foreach (string i in devece_size)
                {
                    ed.WriteMessage("===" + i + "==========\n");
                }
                //psv.SetPlotWindowArea(ps,ext);
                //psv.SetPlotType(ps,Autodesk.AutoCAD.DatabaseServices.PlotType.Window);
                psv.SetPlotType(ps, Autodesk.AutoCAD.DatabaseServices.PlotType.Extents);
                psv.SetPlotConfigurationName(ps, plotDeviceName, mediaName);
                psv.SetUseStandardScale(ps,true);
                psv.SetPlotCentered(ps, true);
                psv.SetStdScaleType(ps, StdScaleType.ScaleToFit);
                psv.SetPlotRotation(ps, plotRotation);

                var pi = new PlotInfo();
                pi.Layout = lo.Id;
                pi.OverrideSettings = ps;



                var piv = new PlotInfoValidator();
                piv.MediaMatchingPolicy = MatchingPolicy.MatchEnabled;
                piv.Validate(pi);

                var ppi = new PlotPageInfo();



                using (PlotEngine pe = PlotFactory.CreatePublishEngine())
                {
                    pe.BeginPlot(null, null);
                    //pe.BeginDocument(pi,doc.Name,null,1,false,filename);
                    pe.BeginDocument(pi, db.Filename, null, 1, false, filename);
                    pe.BeginPage(ppi, pi, true, null);
                    pe.BeginGenerateGraphics(null);
                    pe.EndGenerateGraphics(null);
                    pe.EndPage(null);
                    pe.EndDocument(null);
                    pe.EndPlot(null);
                }

            }
        }
        #endregion

        //[CommandMethod("ppt1")]
        //public void zjyAutoPrint_OpenedDrawing()
        //{
        //    var middoc = cadSer.Application.DocumentManager.MdiActiveDocument;
        //    var db = HostApplicationServices.WorkingDatabase;
        //    using (Transaction trs = db.TransactionManager.StartTransaction())
        //    {
        //        ObjectId paperId = SymbolUtilityServices.GetBlockPaperSpaceId(db);
        //        BlockTable bt = db.BlockTableId.GetObject(OpenMode.ForRead) as BlockTable;
        //        foreach (ObjectId tempObj in bt)
        //        {
        //            BlockTableRecord btr = tempObj.GetObject(OpenMode.ForRead) as BlockTableRecord;
        //            if (btr.IsLayout)
        //            {
        //                Layout la = trs.GetObject(btr.LayoutId, OpenMode.ForRead) as Layout;
        //                if (la.LayoutName.Contains("平面图"))
        //                {
        //                    LayoutPlot(middoc, la);
        //                }

        //            }

        //        }

        //        trs.Commit();
        //    }
        //}
        private void LayoutPlot(Document midoc, Layout lo)
        {

            Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
            Database db = midoc.Database;
            HostApplicationServices.WorkingDatabase = db;
            LayoutManager.Current.CurrentLayout = lo.LayoutName;


            var plotDeviceName = "pdfFactory Pro";
            //var plotDeviceName = "DocuCentre-V 3060";
            string mediaName = "A3";
            using (var ps = new PlotSettings(lo.ModelType))
            {
                ps.CopyFrom(lo);
                //有时需要改
              //   PlotRotation plotRotation = PlotRotation.Degrees090;
               PlotRotation plotRotation = PlotRotation.Degrees000;
               // PlotConfigManager.SetCurrentConfig(plotDeviceName);

                var psv = PlotSettingsValidator.Current;


       //psv.SetPlotWindowArea(ps, new Extents2d(-75,-200,4125,2770));
                //psv.SetPlotType(ps, Autodesk.AutoCAD.DatabaseServices.PlotType.Window);

                psv.SetPlotType(ps, Autodesk.AutoCAD.DatabaseServices.PlotType.Extents);

                psv.SetPlotConfigurationName(ps, plotDeviceName, mediaName);
                psv.SetStdScaleType(ps, StdScaleType.ScaleToFit);
                psv.SetUseStandardScale(ps,true);
                psv.SetPlotCentered(ps, true);
            //    psv.set
                psv.SetPlotPaperUnits(ps, PlotPaperUnit.Millimeters);

               // psv.SetStdScaleType(ps, StdScaleType.ScaleToFit);
              // psv.setplotsca
              psv.SetPlotRotation(ps, plotRotation);

                var pi = new PlotInfo();
                pi.Layout = lo.Id;
                pi.OverrideSettings = ps;

                var piv = new PlotInfoValidator();
                piv.MediaMatchingPolicy = MatchingPolicy.MatchEnabled;
                piv.Validate(pi);
                var ppi = new PlotPageInfo();

                using (PlotEngine pe = PlotFactory.CreatePublishEngine())
                {
                    pe.BeginPlot(null, null);
                    //pe.BeginDocument(pi,doc.Name,null,1,false,filename);
                    pe.BeginDocument(pi, db.Filename, null, 1, false, null);
                    pe.BeginPage(ppi, pi, true, null);
                    pe.BeginGenerateGraphics(null);
                    pe.EndGenerateGraphics(null);
                    pe.EndPage(null);
                    pe.EndDocument(null);
                    pe.EndPlot(null);
                }

            }
        }

       

        //打印Model中的图纸 ，图纸的图框在zjyPrint图层内。
        [CommandMethod("ppt1")]
        public void zjyAutoPrint_OpenedDrawing()
        {
            //打印边框置于该层
            string borderLayerName = "zjyPrint";

            var middoc = cadSer.Application.DocumentManager.MdiActiveDocument;
            var db = HostApplicationServices.WorkingDatabase;
            Editor ed = middoc.Editor;

            //Dictionary<Polyline, Extents2d> borderList = new Dictionary<Polyline, Extents2d>();
            List<Extents2d> borderList = new List<Extents2d>();
            using (Transaction trs = db.TransactionManager.StartTransaction())
            {
                BlockTable bt = (BlockTable)trs.GetObject(HostApplicationServices.WorkingDatabase.BlockTableId, OpenMode.ForRead);
                //BlockTable 
                BlockTableRecord btr = (BlockTableRecord)trs.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForRead);

                foreach (ObjectId tmpOid in btr)
                {
                    //try
                    //{
                    DBObject dBObject = (DBObject)trs.GetObject(tmpOid, OpenMode.ForRead);
                    if (dBObject is Polyline)
                    {
                        Polyline pline = dBObject as Polyline;
                        if (pline.Layer == borderLayerName)
                        {

                            //if(dbo)
                            Point3dCollection pc = new Point3dCollection();
                            pline.GetStretchPoints(pc);

                            List<double> xList = new List<double>();
                            List<double> yList = new List<double>();
                            foreach (Point3d tmp_p3d in pc)
                            {
                                xList.Add(tmp_p3d.X);
                                yList.Add(tmp_p3d.Y);
                            }
                            xList.Sort();
                            yList.Sort();
                            foreach (double i in xList)
                            {
                                ed.WriteMessage(i.ToString() + "\n");
                            }
                            Extents2d extents2D = new Extents2d(xList[0], yList[0], xList[yList.Count - 1], yList[yList.Count - 1]);
                            //borderList.Add(pline, extents2D);
                            borderList.Add(extents2D);
                        }
                    }
                    else if (dBObject is BlockReference)
                    {
                        try {
                            BlockReference blockReference = dBObject as BlockReference;
                            //if (blockReference.Name!="123") continue;
                            BlockTableRecord btr_bb = (BlockTableRecord)trs.GetObject(bt[blockReference.Name], OpenMode.ForRead);
                            //范围写错了
                            Matrix3d trsm = blockReference.BlockTransform;
                            Point3d blockInsertP3d = blockReference.Position;

                            //var aaa=blockReference.
                            foreach (var ii in btr_bb)
                            {
                                DBObject dBObject_1 = (DBObject)trs.GetObject(ii, OpenMode.ForRead);
                                if (dBObject_1 is Polyline)
                                {
                                    Polyline pline = dBObject_1 as Polyline;
                                    if (pline.Layer == borderLayerName)
                                    {

                                        //if(dbo)
                                        Point3dCollection pc = new Point3dCollection();
                                        pline.GetStretchPoints(pc);

                                        List<double> xList = new List<double>();
                                        List<double> yList = new List<double>();
                                        foreach (Point3d tmp_p3d in pc)
                                        {

                                            Point3d _p3d = tmp_p3d.Add(blockReference.BlockTransform.Translation);

                                            xList.Add(_p3d.X);
                                            yList.Add(_p3d.Y);
                                        }
                                        xList.Sort();
                                        yList.Sort();
                                        foreach (double i in xList)
                                        {
                                            ed.WriteMessage(i.ToString() + "\n");
                                        }
                                        Extents2d extents2D = new Extents2d(new Point2d(xList[0], yList[0]), new Point2d(xList[yList.Count - 1], yList[yList.Count - 1]));
                                        //borderList.Add(pline, extents2D);
                                        borderList.Add(extents2D);
                                    }
                                    ed.WriteMessage(ii.ToString() + "：====================\n");
                                }

                            }
                        }
                        catch
                        {
                            ed.WriteMessage("无效块信息\n");
                        }
                        }

                    //}catch {

                    //};
                }

                trs.Commit();
            }
            short bgPlot = (short)Application.GetSystemVariable("BACKGROUNDPLOT");
            Application.SetSystemVariable("BACKGROUNDPLOT", 0);


            //对打印范围排序
            #region
            //borderList.Sort((Extents2d a, Extents2d b) =>
            //{

            //    //int xLimit =0 ;// (int)(a.MaxPoint.X - a.MinPoint.X)/2;
            //    //int yLimit = 0;// (int)(a.MaxPoint.Y - a.MinPoint.Y)/2;
            //    ////if( a.MinPoint.Y-b.MinPoint.Y<yLimit||  a.MinPoint.X - b.MinPoint.X> yLimit)
            //    ////{
            //    ////    return 1;
            //    ////}else if(a.MinPoint.Y - b.MinPoint.Y == 0)
            //    ////{
            //    ////    if(a.MinPoint.X-b.MinPoint.X==0)
            //    ////    { return 0; }
            //    ////    else if(a.MinPoint.X - b.MinPoint.X<0)
            //    ////    {
            //    ////        return -1;
            //    ////    }
            //    ////    else { return 1; }
            //    ////}
            //    ////else//(a.MinPoint.Y - b.MinPoint.Y >0)
            //    ////{
            //    ////    return 0;
            //    ////}
            //    ////return 
            //    ///
            //    double xLimit = (a.MaxPoint.X - a.MinPoint.X);
            //    double yLimit = (a.MaxPoint.Y - a.MinPoint.Y);
            //    //if (a.MinPoint.Y - b.MinPoint.Y > yLimit)
            //    //{
            //    //    return 1;
            //    //}
            //    //else if (Math.Abs( a.MinPoint.Y - b.MinPoint.Y )< yLimit)
            //    //{
            //    //    if (a.MinPoint.X - b.MinPoint.X >= xLimit)
            //    //    { return 1; }
            //    //    else if (Math.Abs( a.MinPoint.X - b.MinPoint.X )<1)
            //    //    {
            //    //        return 0;
            //    //    }
            //    //    else { return -1; }
            //    //}
            //    //else//(a.MinPoint.Y - b.MinPoint.Y >0)
            //    //{
            //    //    return 0;
            //    //}
            //    if (b.MinPoint.Y - a.MinPoint.Y > yLimit)
            //    {
            //        return -1;

            //    }
            //    else if (Math.Abs(a.MinPoint.Y - b.MinPoint.Y) < yLimit)
            //    {
            //        if (b.MinPoint.X - a.MinPoint.X >= xLimit)
            //        { return -1; }
            //        else { return 0; }
            //    }
            //    else//(a.MinPoint.Y - b.MinPoint.Y >0)
            //    {
            //        return 0;
            //    }

            //});
            #endregion

            for (int sort_i = 0; sort_i < borderList.Count - 2; sort_i++)
            {
                for (int sort_j = sort_i; sort_j < borderList.Count - 2; sort_j++)
                {
                    Point2d aP2 = borderList[sort_j].MinPoint;
                    Point2d bP2 = borderList[sort_j + 1].MinPoint;
                    double xLimit = borderList[sort_j].MaxPoint.X - borderList[sort_j].MinPoint.X;
                    double yLimit = borderList[sort_j].MaxPoint.Y - borderList[sort_j].MinPoint.Y;
                    if (bP2.Y - aP2.Y >= yLimit || (Math.Abs(bP2.Y - aP2.Y) < xLimit * 0.8 && aP2.X - bP2.X >= xLimit))
                    {
                        Extents2d changeTmp = borderList[sort_j];
                        borderList[sort_j] = borderList[sort_j + 1];
                        borderList[sort_j + 1] = changeTmp;
                    }
                }
            }


            //显示打印顺序
            int count_tmpp = 0;
            foreach (var tmpp in borderList)
            {
                zPointXY zPXY = new zPointXY(tmpp.MinPoint.X, tmpp.MinPoint.Y);
                zCircleCreate(zPXY, tmpp.MinPoint.X / 20);
                zCreatMtext("打印顺序:" + count_tmpp++, zPXY,50,50, "zjy_print_info");
            }
            //打印
            foreach (Extents2d tmpE2d in borderList)
            {
                Extents2d extents2D = new Extents2d(tmpE2d.MinPoint.X - 1760, tmpE2d.MinPoint.Y - 1188, tmpE2d.MaxPoint.X + 1600, tmpE2d.MaxPoint.Y + 1188);
                LayoutPlot(middoc, extents2D);
            }

            Application.SetSystemVariable("BACKGROUNDPLOT", bgPlot);
        }

        private void LayoutPlot(Document midoc, Extents2d extents2D)
        {
            //Layout layout=LayoutManager.Current
            Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
            Database db = midoc.Database;
            HostApplicationServices.WorkingDatabase = db;
            Layout lo = new Layout();
            //获取模型空间layout
            using (Transaction trs = db.TransactionManager.StartTransaction())
            {
                ObjectId paperId = SymbolUtilityServices.GetBlockPaperSpaceId(db);
                BlockTable bt = db.BlockTableId.GetObject(OpenMode.ForRead) as BlockTable;

                foreach (ObjectId tempObj in bt)
                {
                    BlockTableRecord btr = tempObj.GetObject(OpenMode.ForRead) as BlockTableRecord;
                    if (btr.IsLayout)
                    {
                        Layout la = trs.GetObject(btr.LayoutId, OpenMode.ForRead) as Layout;
                        if (la.LayoutName.Contains("Model"))
                        {
                            lo = la;
                            break;
                        }

                    }

                }

                trs.Commit();
            }


            LayoutManager.Current.CurrentLayout = lo.LayoutName;


           // var plotDeviceName = "pdfFactory Pro";
            var plotDeviceName = "DocuCentre-V 3060";
            string mediaName = "A3";
            using (var ps = new PlotSettings(lo.ModelType))
            {
                ps.CopyFrom(lo);
                //有时需要改
                PlotRotation plotRotation = PlotRotation.Degrees090;

                PlotConfigManager.SetCurrentConfig(plotDeviceName);

                var psv = PlotSettingsValidator.Current;


                psv.SetPlotWindowArea(ps, extents2D);
                psv.SetPlotType(ps, Autodesk.AutoCAD.DatabaseServices.PlotType.Window);

                //psv.SetPlotType(ps, Autodesk.AutoCAD.DatabaseServices.PlotType.Extents);

                psv.SetPlotConfigurationName(ps, plotDeviceName, mediaName);
                psv.SetUseStandardScale(ps, true);
                psv.SetPlotCentered(ps, true);
                psv.SetStdScaleType(ps, StdScaleType.ScaleToFit);
                psv.SetPlotRotation(ps, plotRotation);

                var pi = new PlotInfo();
                pi.Layout = lo.Id;
                pi.OverrideSettings = ps;

                var piv = new PlotInfoValidator();
                piv.MediaMatchingPolicy = MatchingPolicy.MatchEnabled;
                piv.Validate(pi);
                var ppi = new PlotPageInfo();

                using (PlotEngine pe = PlotFactory.CreatePublishEngine())
                {
                    pe.BeginPlot(null, null);
                    //pe.BeginDocument(pi,doc.Name,null,1,false,filename);
                    pe.BeginDocument(pi, db.Filename, null, 1, false, null);
                    pe.BeginPage(ppi, pi, true, null);
                    pe.BeginGenerateGraphics(null);
                    pe.EndGenerateGraphics(null);
                    pe.EndPage(null);
                    pe.EndDocument(null);
                    pe.EndPlot(null);
                }

            }
        }



        /// <summary>
        /// 批量打印函数
        /// </summary>
        /// <returns></returns>

        //打印布局空间_名称为平面图
        public static void LayoutPMT_Plot(string deviceStr,int direction_0_90)//(Database workDB,Database currenDB)//(object sender,DocumentCollectionEventArgs e)
        {

            Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;

            Document middoc = cadSer.Application.DocumentManager.MdiActiveDocument;
            Database db = HostApplicationServices.WorkingDatabase;

            LayoutManager lb = LayoutManager.Current;
            List<Layout> zLayoutList = new List<Layout>();
            using (Transaction trs = db.TransactionManager.StartTransaction())
            {
                ObjectId paperId = SymbolUtilityServices.GetBlockPaperSpaceId(db);
                BlockTable bt = db.BlockTableId.GetObject(OpenMode.ForRead) as BlockTable;

                foreach (ObjectId tempObj in bt)
                {
                    BlockTableRecord btr = tempObj.GetObject(OpenMode.ForRead) as BlockTableRecord;
                    if (btr.IsLayout)
                    {
                        Layout la = trs.GetObject(btr.LayoutId, OpenMode.ForRead) as Layout;
                        if (la.LayoutName.Contains("图"))
                        {
                            zLayoutList.Add(la);
                        }
                        

                    }

                }

                trs.Commit();
            }

            zLayoutList.Sort((Layout x, Layout y) =>
            {
                if (x.LayoutName.CompareTo(y.LayoutName) < 0)
                {
                    return -1;
                }
                else if (x.LayoutName.CompareTo(y.LayoutName) > 0)
                {
                    return 1;
                }
                return 0;

            });

            foreach (Layout _tmpL in zLayoutList)
            {
                ed.WriteMessage("开始打印布局页：" + _tmpL.LayoutName + "\n");
            }
            ed.WriteMessage("============================================" + "\n");

            short bgPlot = (short)Application.GetSystemVariable("BACKGROUNDPLOT");
            Application.SetSystemVariable("BACKGROUNDPLOT", 0);
            var dloc = middoc.LockDocument();

            using (Transaction trs = db.TransactionManager.StartTransaction())
            {

                foreach (Layout ly in zLayoutList)
                {
                    LayoutPlot_update(trs, ly, @"d:\123_" + ly.LayoutName,deviceStr,direction_0_90);
                    //ed.WriteMessage("aaaaaaaaaaaaaaaaaaaaaa+\n");
                }
                //ISContinuePrint=false;

            }

            dloc.Dispose();
            Application.SetSystemVariable("BACKGROUNDPLOT", bgPlot);

            //cadSer.Application.DocumentManager.DocumentActivated-=zjy_event;

        }
        private static void LayoutPlot_update(Transaction tr, Layout lo, string filename, string deviceStr, int direction_0_90)//, Database workDB)
        {
            //Database db =DockSides. ;//cadSer.Application.DocumentManager.MdiActiveDocument.Database;
            var doc = cadSer.Application.DocumentManager.MdiActiveDocument;
            Database db = HostApplicationServices.WorkingDatabase;
            Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;

            //HostApplicationServices.WorkingDatabase = db;
           
            using (Transaction acTrans = db.TransactionManager.StartTransaction())
            {
                DBDictionary plSettings = acTrans.GetObject(db.PlotSettingsDictionaryId,
                                                            OpenMode.ForRead) as DBDictionary;

                // List each named page setup
                foreach (DBDictionaryEntry item in plSettings)
                {
                    ed.WriteMessage("\n  " + item.Key);
                }

                // Abort the changes to the database
                acTrans.Abort();
            }


            LayoutManager.Current.CurrentLayout = lo.LayoutName;

            //var plotDeviceName = "pdfFactory Pro";

            var plotDeviceName = deviceStr;// "DocuCentre-V 3060";


            string mediaName = "A3";
            //var styleSheetName = "monochrome.stb";


            using (var ps = new PlotSettings(lo.ModelType))
            {

                ps.CopyFrom(lo);
                ps.PlotPlotStyles = false;
               
                //PlotRotation plotRotation = PlotRotation.Degrees090;
                //if (direction_0_90==0)
                //{
                //     plotRotation = PlotRotation.Degrees000;
                //}
                //else if(direction_0_90==90)
                //{
                //     plotRotation = PlotRotation.Degrees090;
                //}else
                //{
                //    plotRotation = PlotRotation.Degrees090;
                //}

                PlotRotation plotRotation = PlotRotation.Degrees270;


                //PlotRotation plotRotation = PlotRotation.Degrees000;
                //PlotConfigManager.RefreshList(RefreshCode.All);
                PlotConfigManager.SetCurrentConfig(plotDeviceName);
                //PlotConfigManager.CurrentConfig.RefreshMediaNameList();

               // AcadPlotConfiguration acadPlotConfiguration

                var psv = PlotSettingsValidator.Current;
                var device_list = psv.GetPlotDeviceList();
                foreach (string i in device_list)
                {
                    ed.WriteMessage("===" + i + "==========\n");
                }
                var devece_size = psv.GetCanonicalMediaNameList(ps);
                foreach (string i in devece_size)
                {
                    ed.WriteMessage("===" + i + "==========\n");
                }
                //psv.SetPlotWindowArea(ps,ext);
                //psv.SetPlotType(ps,Autodesk.AutoCAD.DatabaseServices.PlotType.Window);
                psv.SetPlotType(ps, Autodesk.AutoCAD.DatabaseServices.PlotType.Extents);
                psv.SetPlotConfigurationName(ps, plotDeviceName, mediaName);
                psv.SetUseStandardScale(ps, true);
                psv.SetPlotCentered(ps, true);
                psv.SetStdScaleType(ps, StdScaleType.ScaleToFit);
                psv.SetPlotRotation(ps, plotRotation);
              var aaa=   psv.GetPlotStyleSheetList();
                //  foreach(var sss in aaa)
                //   {
                //    string aa = sss;
                // }

               
               // psv.SetCurrentStyleSheet(ps, "acad.ctb");
               // psv.SetCurrentStyleSheet(ps,"");
            
                var pi = new PlotInfo();
                pi.Layout = lo.Id;
                pi.OverrideSettings = ps;


                var piv = new PlotInfoValidator();
                piv.MediaMatchingPolicy = MatchingPolicy.MatchEnabled;
                piv.Validate(pi);

                var ppi = new PlotPageInfo();



                using (PlotEngine pe = PlotFactory.CreatePublishEngine())
                {
                    
                    pe.BeginPlot(null, null);
                    //pe.BeginDocument(pi,doc.Name,null,1,false,filename);
                    pe.BeginDocument(pi, db.Filename, null, 1, false, filename);
                    pe.BeginPage(ppi, pi, true, null);
                    pe.BeginGenerateGraphics(null);
                    pe.EndGenerateGraphics(null);
                    pe.EndPage(null);
                    pe.EndDocument(null);
                    pe.EndPlot(null);
                }

            }
        }
        //打印模型空间
        public static void Model_Plot(string deviceStr, int direction_0_90)
        {
            try
            {
                //打印边框置于该层
                string borderLayerName = "zjyPrint";

                var middoc = cadSer.Application.DocumentManager.MdiActiveDocument;
                var db = HostApplicationServices.WorkingDatabase;
                Editor ed = middoc.Editor;

                //Dictionary<Polyline, Extents2d> borderList = new Dictionary<Polyline, Extents2d>();
                List<Extents2d> borderList = new List<Extents2d>();
                using (Transaction trs = db.TransactionManager.StartTransaction())
                {
                    BlockTable bt = (BlockTable)trs.GetObject(HostApplicationServices.WorkingDatabase.BlockTableId, OpenMode.ForRead);
                    //BlockTable 
                    BlockTableRecord btr = (BlockTableRecord)trs.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForRead);

                    foreach (ObjectId tmpOid in btr)
                    {
                        //try
                        //{
                                DBObject dBObject = (DBObject)trs.GetObject(tmpOid, OpenMode.ForRead);
                            if (dBObject is Polyline)
                            {
                                Polyline pline = dBObject as Polyline;
                                if (pline.Layer == borderLayerName)
                                {

                                    //if(dbo)
                                    Point3dCollection pc = new Point3dCollection();
                                    pline.GetStretchPoints(pc);

                                    List<double> xList = new List<double>();
                                    List<double> yList = new List<double>();
                                    foreach (Point3d tmp_p3d in pc)
                                    {
                                        xList.Add(tmp_p3d.X);
                                        yList.Add(tmp_p3d.Y);
                                    }
                                    xList.Sort();
                                    yList.Sort();
                                    foreach (double i in xList)
                                    {
                                        ed.WriteMessage(i.ToString() + "\n");
                                    }
                                    Extents2d extents2D = new Extents2d(xList[0], yList[0], xList[yList.Count - 1], yList[yList.Count - 1]);
                                    //borderList.Add(pline, extents2D);
                                    borderList.Add(extents2D);
                                }
                            }
                            else if (dBObject is BlockReference)
                            {
                                try
                                {
                                    BlockReference blockReference = dBObject as BlockReference;
                                    //if (blockReference.Name!="123") continue;
                                    BlockTableRecord btr_bb = (BlockTableRecord)trs.GetObject(bt[blockReference.Name], OpenMode.ForRead);
                                    //范围写错了
                                    Matrix3d trsm = blockReference.BlockTransform;
                                    Point3d blockInsertP3d = blockReference.Position;
                                    Extents3d extents3D = blockReference.GeometricExtents;

                                    //var aaa=blockReference.
                                    foreach (var ii in btr_bb)
                                    {
                                        DBObject dBObject_1 = (DBObject)trs.GetObject(ii, OpenMode.ForRead);
                                        if (dBObject_1 is Polyline)
                                        {
                                            Polyline pline = dBObject_1 as Polyline;
                                            if (pline.Layer == borderLayerName)
                                            {

                                                //if(dbo)
                                                Point3dCollection pc = new Point3dCollection();
                                                pline.GetStretchPoints(pc);

                                                List<double> xList = new List<double>();
                                                List<double> yList = new List<double>();
                                                foreach (Point3d tmp_p3d in pc)
                                                {

                                                    Point3d _p3d = tmp_p3d.Add(blockReference.BlockTransform.Translation);

                                                    xList.Add(_p3d.X);
                                                    yList.Add(_p3d.Y);
                                                }
                                                xList.Sort();
                                                yList.Sort();
                                                foreach (double i in xList)
                                                {
                                                    ed.WriteMessage(i.ToString() + "\n");
                                                }
                                                #region
                                                //    double xlength = xList[yList.Count - 1] - xList[0];
                                                // double ylength = yList[yList.Count - 1] - yList[0];
                                                // Scale3d scacleblock = blockReference.ScaleFactors;
                                                //Matrix3d abc = trsm;//.Inverse();
                                                //  Vector3d minPt = new Vector3d(xList[0], yList[0], 0);
                                                // Vector3d maxPt = new Vector3d(xList[yList.Count - 1], yList[yList.Count - 1], 0);
                                                //  minPt =  abc*minPt;
                                                //  maxPt = abc * maxPt;
                                                //  zCircleCreate(new zPointXY(blockInsertP3d.X,blockInsertP3d.Y), 50);

                                                //    Extents2d extents2D = new Extents2d(new Point2d(minPt.X,minPt.Y), new Point2d(maxPt.X,maxPt.Y));

                                                //  Extents2d extents2D = new Extents2d(new Point2d(xList[0], yList[0]), new Point2d(xList[yList.Count - 1], yList[yList.Count - 1]));
                                                //borderList.Add(pline, extents2D);
                                                #endregion
                                                Extents2d extents2D = new Extents2d(extents3D.MinPoint.X, extents3D.MinPoint.Y, extents3D.MaxPoint.X, extents3D.MaxPoint.Y);
                                                borderList.Add(extents2D);
                                            }
                                            ed.WriteMessage(ii.ToString() + "：====================\n");
                                        }

                                    }

                                }
                                catch
                                {
                                    ed.WriteMessage("块无信息\n");
                                }
                            }

                        //}
                        //catch
                        //{
                        //    ed.WriteMessage("无效块\n");
                        //};
                    }

                    trs.Commit();
                }
                short bgPlot = (short)Application.GetSystemVariable("BACKGROUNDPLOT");
                Application.SetSystemVariable("BACKGROUNDPLOT", 0);


                #region
                //排序
                //for (int sort_i = 0; sort_i < borderList.Count -1; sort_i++)
                //{

                //    for (int sort_j = sort_i; sort_j < borderList.Count -1; sort_j++)
                //    {
                //        Point2d aP2 = borderList[sort_j].MinPoint;
                //        Point2d bP2 = borderList[sort_j + 1].MinPoint;
                //        double xLimit = borderList[sort_j].MaxPoint.X - borderList[sort_j].MinPoint.X;
                //        double yLimit = borderList[sort_j].MaxPoint.Y - borderList[sort_j].MinPoint.Y;
                //        if (bP2.Y - aP2.Y >= yLimit || (Math.Abs(bP2.Y - aP2.Y) < yLimit * 0.8 && aP2.X - bP2.X >= xLimit))
                //        {
                //            Extents2d changeTmp = borderList[sort_j];
                //            borderList[sort_j] = borderList[sort_j + 1];
                //            borderList[sort_j + 1] = changeTmp;
                //        }
                //    }
                //}
                #endregion


                for (int sort_i = 0; sort_i < borderList.Count - 1; sort_i++)
                {

                    for (int sort_j = sort_i + 1; sort_j < borderList.Count; sort_j++)
                    {
                        Point2d aP2 = borderList[sort_i].MinPoint;
                        Point2d aP4 = borderList[sort_i].MaxPoint;
                        Point2d bP2 = borderList[sort_j].MinPoint;
                        Point2d bP4 = borderList[sort_j].MaxPoint;
                        double xLimit = borderList[sort_j].MaxPoint.X - borderList[sort_j].MinPoint.X;
                        double yLimit = borderList[sort_i].MaxPoint.Y - borderList[sort_i].MinPoint.Y;


                        #region
                        //if (bP2.Y - aP2.Y >= yLimit - 0.002 || (Math.Abs(bP2.Y - aP2.Y) < yLimit * 0.95 && aP2.X - bP2.X >= xLimit - 0.002))
                        //if (bP2.Y - aP2.Y >= yLimit)
                        //{ 

                        //Extents2d changeTmp = borderList[sort_i];
                        //borderList[sort_i] = borderList[sort_j];
                        //borderList[sort_j] = changeTmp;

                        //}
                        //else if((((aP2.Y >= bP2.Y && aP2.Y <= bP4.Y) || (aP4.Y >= bP2.Y && aP4.Y <= bP4.Y)) && aP2.X - bP2.X >= xLimit))
                        //{
                        //    Extents2d changeTmp = borderList[sort_i];
                        //    borderList[sort_i] = borderList[sort_j];
                        //    borderList[sort_j] = changeTmp;
                        //}
                        #endregion

                        double err = 0.00001;

                        if (aP2.Y - bP4.Y >= -err)
                        {
                            continue;
                        }
                        else if (bP2.Y - aP4.Y >= -err)
                        {
                            Extents2d changeTmp = borderList[sort_i];
                            borderList[sort_i] = borderList[sort_j];
                            borderList[sort_j] = changeTmp;
                        }
                        else if (aP2.X - bP4.X >= -err)
                        {
                            Extents2d changeTmp = borderList[sort_i];
                            borderList[sort_i] = borderList[sort_j];
                            borderList[sort_j] = changeTmp;
                        }

                        #region
                        //if (bP2.Y - aP2.Y >= yLimit-err)
                        //{

                        //    Extents2d changeTmp = borderList[sort_i];
                        //    borderList[sort_i] = borderList[sort_j];
                        //    borderList[sort_j] = changeTmp;

                        //}
                        //else if(aP2.X - bP2.X >= xLimit-err)
                        //{
                        //    Extents2d changeTmp = borderList[sort_i];
                        //    borderList[sort_i] = borderList[sort_j];
                        //    borderList[sort_j] = changeTmp;
                        //}
                        //else if ((((aP2.Y >= bP2.Y && aP2.Y <= bP4.Y) ) && aP2.X - bP2.X >= xLimit))
                        //{
                        //    Extents2d changeTmp = borderList[sort_i];
                        //    borderList[sort_i] = borderList[sort_j];
                        //    borderList[sort_j] = changeTmp;
                        //}
                        //else if ((( (aP4.Y >= bP2.Y && aP4.Y <= bP4.Y)) && aP2.X - bP2.X >= xLimit))
                        //{
                        //    Extents2d changeTmp = borderList[sort_i];
                        //    borderList[sort_i] = borderList[sort_j];
                        //    borderList[sort_j] = changeTmp;
                        //}


                        //double err = 0.00001;
                        //if (bP2.Y - aP2.Y >=yLimit-err)
                        //{

                        //    Extents2d changeTmp = borderList[sort_i];
                        //    borderList[sort_i] = borderList[sort_j];
                        //    borderList[sort_j] = changeTmp;

                        //}
                        //else if ( aP2.X - bP2.X >= xLimit- err && ((aP2.Y > bP2.Y-err && aP2.Y < bP4.Y-err) || (aP4.Y > bP2.Y-err && aP4.Y < bP4.Y-err)))
                        //{
                        //    Extents2d changeTmp = borderList[sort_i];
                        //    borderList[sort_i] = borderList[sort_j];
                        //    borderList[sort_j] = changeTmp;
                        //}
                        #endregion

                    }
                }




                //显示打印顺序
                int count_tmpp = 0;
                foreach (var tmpp in borderList)
                {
                    zPointXY zPXY = new zPointXY(tmpp.MinPoint.X, tmpp.MinPoint.Y);
                    zCircleCreate(zPXY, 50);
                    zCreatMtext("打印顺序:" + count_tmpp++, zPXY, 50, 50, "zjy_print_info");
                }
                //打印
                foreach (Extents2d tmpE2d in borderList)
                {
                    Extents2d extents2D = new Extents2d(tmpE2d.MinPoint.X, tmpE2d.MinPoint.Y, tmpE2d.MaxPoint.X, tmpE2d.MaxPoint.Y);
                    ModelPlot(middoc, extents2D, deviceStr, direction_0_90);
                }

                Application.SetSystemVariable("BACKGROUNDPLOT", bgPlot);
            }
            catch { }
        }
        private static void ModelPlot(Document midoc, Extents2d extents2D, string deviceStr, int direction_0_90)
        {
            //Layout layout=LayoutManager.Current
            Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
            Database db = midoc.Database;
            HostApplicationServices.WorkingDatabase = db;
            Layout lo = new Layout();
            
            //获取模型空间layout
            using (Transaction trs = db.TransactionManager.StartTransaction())
            {
                ObjectId paperId = SymbolUtilityServices.GetBlockPaperSpaceId(db);
                BlockTable bt = db.BlockTableId.GetObject(OpenMode.ForRead) as BlockTable;

                foreach (ObjectId tempObj in bt)
                {
                    BlockTableRecord btr = tempObj.GetObject(OpenMode.ForRead) as BlockTableRecord;
                    if (btr.IsLayout)
                    {
                        Layout la = trs.GetObject(btr.LayoutId, OpenMode.ForRead) as Layout;
                        if (la.LayoutName.Contains("Model"))
                        {
                            lo = la;
                            break;
                        }

                    }

                }

                trs.Commit();
            }


            LayoutManager.Current.CurrentLayout = lo.LayoutName;

            
            // var plotDeviceName = "pdfFactory Pro";
            var plotDeviceName = deviceStr;//"DocuCentre-V 3060";
            string mediaName = "A3";
            using (var ps = new PlotSettings(lo.ModelType))
            {
                ps.CopyFrom(lo);
                //有时需要改
                ps.PlotPlotStyles = false;

                //PlotRotation plotRotation = PlotRotation.Degrees090;
                //if (direction_0_90 == 0)
                //{
                //    plotRotation = PlotRotation.Degrees000;
                //}
                //else if (direction_0_90 == 90)
                //{
                //    plotRotation = PlotRotation.Degrees090;
                //}
                //else if (direction_0_90 == 180)
                //{
                //    plotRotation = PlotRotation.Degrees180;
                //}
                //else if (direction_0_90 == 270)
                //{
                //    plotRotation = PlotRotation.Degrees270;
                //}




                PlotRotation plotRotation = PlotRotation.Degrees270;

                PlotConfigManager.SetCurrentConfig(plotDeviceName);

                var psv = PlotSettingsValidator.Current;


                psv.SetPlotWindowArea(ps, extents2D);
                psv.SetPlotType(ps, Autodesk.AutoCAD.DatabaseServices.PlotType.Window);
             
                //psv.SetPlotType(ps, Autodesk.AutoCAD.DatabaseServices.PlotType.Extents);

                psv.SetPlotConfigurationName(ps, plotDeviceName, mediaName);
                psv.SetUseStandardScale(ps, true);
                psv.SetPlotCentered(ps, true);
                psv.SetStdScaleType(ps, StdScaleType.ScaleToFit);
                psv.SetPlotRotation(ps, plotRotation);
                // var ssss = psv.GetPlotStyleSheetList();

                 psv.SetCurrentStyleSheet(ps, "acad.ctb");



                var pi = new PlotInfo();
                pi.Layout = lo.Id;
                pi.OverrideSettings = ps;

                var piv = new PlotInfoValidator();
                piv.MediaMatchingPolicy = MatchingPolicy.MatchEnabled;
                piv.Validate(pi);
                var ppi = new PlotPageInfo();

                using (PlotEngine pe = PlotFactory.CreatePublishEngine())
                {
                    pe.BeginPlot(null, null);
                    //pe.BeginDocument(pi,doc.Name,null,1,false,filename);
                    pe.BeginDocument(pi, db.Filename, null, 1, false, null);
                    pe.BeginPage(ppi, pi, true, null);
                    pe.BeginGenerateGraphics(null);
                    pe.EndGenerateGraphics(null);
                    pe.EndPage(null);
                    pe.EndDocument(null);
                    pe.EndPlot(null);
                }

            }
        }

        //打印目录下的所有目录里的dwg文件。 
        public static void zjyCADFilePrint(string filepath, string deviceStr, int direction_0_90)
        {
            string zjyProjectName = filepath;//@"E:\古县项目20200331-修改 - 副本\";
         //   string[] dirList = Directory.GetDirectories(zjyProjectName);//,@"*.dwg");

            //Regex rr = new Regex(@"[^0-9]");

          //  List<string> dirUseList =  Sort_Name_As_Windows(dirList.ToList<string>());
            //foreach (string dirTmp in dirList)
            //{

            //    //if(rr.IsMatch(dirTmp))
            //  //  {
            //        dirUseList.Add(dirTmp);
            //   // }
            //}
            //dirUseList.Sort();

            #region
            //foreach (string ii in dirUseList)
            //{
            //    PlotDwgInPath(deviceStr, direction_0_90, ii);
            //    #region
            //    //string[] dwgFile = Directory.GetFiles(ii, "*.dwg");
            //    //foreach (string _tmp_dwg in dwgFile)
            //    //{
            //    //    var middoc = cadSer.Application.DocumentManager.Open(_tmp_dwg, false);
            //    //    var db = HostApplicationServices.WorkingDatabase;

            //    //    HostApplicationServices.WorkingDatabase = middoc.Database;

            //    //    //开始打印文件
            //    //    //zjy_event();
            //    //    if (_tmp_dwg.Contains("平面图"))
            //    //    {
            //    //        LayoutPMT_Plot(deviceStr, direction_0_90);
            //    //    }
            //    //    else
            //    //    {
            //    //        Model_Plot(deviceStr, direction_0_90);
            //    //    }

            //    //    HostApplicationServices.WorkingDatabase = db;
            //    //}
            //    #endregion
            //}
            #endregion
            //打印父目录中的dwg
            PlotDwgInPath(deviceStr, direction_0_90, zjyProjectName);
        }

        private static void PlotDwgInPath(string deviceStr, int direction_0_90, string zjyProjectName)
        {
            string[] dwgFile_1 = Directory.GetFiles(zjyProjectName, "*.dwg");
            List<string> files = Sort_Name_As_Windows(dwgFile_1.ToList<string>());

            foreach (string _tmp_dwg in files)
            {
                zjyCadPrint(deviceStr, direction_0_90, _tmp_dwg);
            }

           
        }

        public static void zjyCAD_Word_Excel_FilePrint(string filepath, string deviceStr, int direction_0_90,bool _excel_Is_1)
        {
            string printName = deviceStr;//pdfFactory Pro  
                                                //   ExcelPrint(@"G:\新建文件夹\Desktop\111\1.xlsx", printName, -1);
                                                //  WordPrint(@"G:\新建文件夹\Desktop\111\2.docx", printName);

            string path = filepath;
          

            int excelEndPage = 1;
            if(_excel_Is_1 == false)
            {
                excelEndPage = -1;
            }


            if (Directory.Exists(path))
            {

                List<string> files = Sort_Name_As_Windows(Directory.GetFiles(path).ToList<string>());



                foreach (string filetmp in files)
                {
                   // Console.WriteLine(Path.GetFileName(filetmp) + "\n");
                    string filename = Path.GetExtension(filetmp).ToLower();
                    if (filename == ".xlsx" || filename == ".xls")
                    {
                        OutPrint_txt(filetmp, "\n开始打印文件:" + Path.GetFileName(filetmp) + ".");
                        ExcelPrint(filetmp, printName, excelEndPage);
                        OutPrint_txt(filetmp,"成功打印文件:"+Path.GetFileName(filetmp)+".\n");
                    }
                    else if (filename == ".docx" || filename == ".doc")
                    {
                        OutPrint_txt(filetmp, "\n开始打印文件:" + Path.GetFileName(filetmp) + ".");
                        WordPrint(filetmp, printName);
                        OutPrint_txt(filetmp, "成功打印文件:" + Path.GetFileName(filetmp) + ".\n");
                    }
                    else if(filename == ".dwg")
                    {
                        OutPrint_txt(filetmp, "\n开始打印文件:" + Path.GetFileName(filetmp) + ".");
                        zjyCadPrint(printName,direction_0_90,filetmp);
                        OutPrint_txt(filetmp, "成功打印文件:" + Path.GetFileName(filetmp) + ".\n");
                    }
                }
            }

        }

        public static void zjy_Word_Excel_FilePrint(string filepath, string deviceStr, int direction_0_90, bool _excel_Is_1)
        {
            string printName = deviceStr;//pdfFactory Pro  
                                         //   ExcelPrint(@"G:\新建文件夹\Desktop\111\1.xlsx", printName, -1);
                                         //  WordPrint(@"G:\新建文件夹\Desktop\111\2.docx", printName);

            string path = filepath;


            int excelEndPage = 1;
            if (_excel_Is_1 == false)
            {
                excelEndPage = -1;
            }


            if (Directory.Exists(path))
            {

                List<string> files = Sort_Name_As_Windows(Directory.GetFiles(path).ToList<string>());



                foreach (string filetmp in files)
                {
                    // Console.WriteLine(Path.GetFileName(filetmp) + "\n");
                    string filename = Path.GetExtension(filetmp).ToLower();
                    if (filename==".xlsx" || filename== ".xls")
                    {
                        OutPrint_txt(filetmp, "\n开始打印文件:" + Path.GetFileName(filetmp) + ".");
                        ExcelPrint(filetmp, printName, excelEndPage);
                        OutPrint_txt(filetmp, "成功打印文件:" + Path.GetFileName(filetmp) + ".\n");
                    }
                    else if (filename== ".docx" || filename == ".doc")
                    {
                        OutPrint_txt(filetmp, "\n开始打印文件:" + Path.GetFileName(filetmp) + ".");
                        WordPrint(filetmp, printName);
                        OutPrint_txt(filetmp, "成功打印文件:" + Path.GetFileName(filetmp) + ".\n");
                    }
                    //else if (filename.Contains("dwg"))
                    //{
                    //    OutPrint_txt(filetmp, "\n开始打印文件:" + Path.GetFileName(filetmp) + ".\n");
                    //    zjyCadPrint(printName, direction_0_90, filetmp);
                    //    OutPrint_txt(filetmp, "\n成功打印文件:" + Path.GetFileName(filetmp) + ".\n");
                    //}
                }
            }

        }


        public static void zjy_Change_Project_Name_dwgwordexcel(string path,string old_name,string new_name,int isContainDwg)//0 dwg 1 wordexcel 2 all
        {
            string findstr = old_name;// "万荣县三光线柳家院至桥上村过沟土桥改造工程";
            string replacestr = new_name;// "项目名称";

            //string replacestr = "万荣县三光线柳家院至桥上村过沟土桥改造工程";
            //string findstr = "项目名称";
            if (Directory.Exists(path))
            {

                List<string> files = Sort_Name_As_Windows(Directory.GetFiles(path).ToList<string>());



                foreach (string filetmp in files)
                {
                    // Console.WriteLine(Path.GetFileName(filetmp) + "\n");
                    string filename = Path.GetExtension(filetmp).ToLower();
                    if (isContainDwg == 1 || isContainDwg==2)
                    {
                        if (filename == ".xlsx" || filename == ".xls")
                        {
                            OutPrint_txt(filetmp, "-----------------\n" + Path.GetFileName(filetmp) + "开始改项目名称!!----------------\n");
                            //string path = @"G:\新建文件夹\Desktop\111\1.xlsx";
                            Excel.Application excelApp = new Excel.Application();
                            excelApp.Visible = true;

                            excelApp.DisplayAlerts = false;
                            excelApp.AskToUpdateLinks = false;
                            //  Excel.Workbook excelWB = excelApp.Workbooks.Add(path);
                            Excel.Workbook excelWB = excelApp.Workbooks.Open(Filename: filetmp);//,ReadOnly:false);//,Notify:false,UpdateLinks:0,IgnoreReadOnlyRecommended:true);

                            Excel.Sheets excelSheets = excelWB.Sheets;

                            foreach (Excel.Worksheet tmp in excelSheets)
                            {
                                if ((int)tmp.Visible > -1) continue;
                                // Console.WriteLine(tmp.Name + "\n");

                                tmp.UsedRange.Replace(findstr, replacestr);

                                if (tmp.PageSetup.LeftFooter.Contains(findstr))
                                {
                                    tmp.PageSetup.LeftFooter = replacestr;
                                }
                                if (tmp.PageSetup.CenterFooter.Contains(findstr))
                                {
                                    tmp.PageSetup.CenterFooter = replacestr;
                                }
                                if (tmp.PageSetup.RightFooter.Contains(findstr))
                                {
                                    tmp.PageSetup.RightFooter = replacestr;
                                }

                                if (tmp.PageSetup.LeftHeader.Contains(findstr))
                                {
                                    tmp.PageSetup.LeftHeader = replacestr;
                                }
                                if (tmp.PageSetup.CenterHeader.Contains(findstr))
                                {
                                    tmp.PageSetup.CenterHeader = replacestr;
                                }
                                if (tmp.PageSetup.RightHeader.Contains(findstr))
                                {
                                    tmp.PageSetup.RightHeader = replacestr;
                                }






                            }
                            excelWB.Save();
                            excelWB.Close(false);

                            excelApp.Quit();
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
                            OutPrint_txt(filetmp, "-----------------\n" + Path.GetFileName(filetmp) + "成功!!----------------\n");
                        }
                        if (filename == ".docx" || filename == ".doc")
                        {
                            OutPrint_txt(filetmp, "-----------------\n" + Path.GetFileName(filetmp) + "开始改项目名称!!----------------\n");
                            Word.Application wordApp = new Word.Application();
                            wordApp.Visible = true;

                            Word.Document wordDoc = wordApp.Documents.OpenNoRepairDialog(filetmp);

                            //替换 主内容
                            wordApp.Selection.Find.Execute(FindText: findstr, ReplaceWith: replacestr, Replace: Word.WdReplace.wdReplaceAll);


                            foreach (Word.Section tmp in wordDoc.Sections)
                            {
                                //替换 页眉
                                tmp.Headers[Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range.Find.Execute(FindText: findstr, ReplaceWith: replacestr, Replace: Word.WdReplace.wdReplaceAll);
                                //替换 页角
                                tmp.Footers[Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range.Find.Execute(FindText: findstr, ReplaceWith: replacestr, Replace: Word.WdReplace.wdReplaceAll);
                                //

                            }


                            wordDoc.Save();

                            wordDoc.Close(false);
                            wordApp.Quit();
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(wordApp);
                            OutPrint_txt(filetmp, "-----------------\n" + Path.GetFileName(filetmp) + "成功!!----------------\n");

                        }
                    }
                   if((isContainDwg==0||isContainDwg==2)&& filename == ".dwg")
                    {
                        OutPrint_txt(filetmp, "-----------------\n" + Path.GetFileName(filetmp) + "开始改项目名称!!----------------\n");
                        zjy_Change_Project_Name_dwg(filetmp, new_name);
                        OutPrint_txt(filetmp, "-----------------\n" + Path.GetFileName(filetmp) + "成功!!----------------\n");
                    }

                }

            }
        }

        public static void zjy_Change_Project_Name_dwg(string path,string new_name)
        {

            
               
            try {
                Autodesk.AutoCAD.ApplicationServices.Document middoc;
                Autodesk.AutoCAD.DatabaseServices.Database db;
                middoc = cadSer.Application.DocumentManager.Open(path, false);
                db = HostApplicationServices.WorkingDatabase;
            string proNameLayer = "zjy_项目名称";

                string proName = new_name;



                //  DBObjectCollection dbCollection = new DBObjectCollection();

                var doc_lock = middoc.LockDocument();

                Editor ed = middoc.Editor; //模型空间

           // if (Path.GetFileName(path).Contains("平面图") || Path.GetFileName(path).Contains("平纵缩"))
           // {
                using (Transaction trans = db.TransactionManager.StartTransaction())
                {

                    BlockTable bt = (BlockTable)trans.GetObject(db.BlockTableId, OpenMode.ForWrite);


                    ObjectId obj_id = bt[BlockTableRecord.ModelSpace];
                    BlockTableRecord btr = (BlockTableRecord)trans.GetObject(obj_id, OpenMode.ForWrite);

                    foreach (ObjectId objid in btr)
                    {

                        DBObject dbo = trans.GetObject(objid, OpenMode.ForWrite);
                        // ed.WriteMessage(dbo.GetType().ToString() + "\n");

                        if (dbo is AttributeDefinition)
                        {
                            AttributeDefinition dbo_to_Entiy = (AttributeDefinition)dbo;

                            if (dbo_to_Entiy.Layer == proNameLayer)
                            {

                                dbo_to_Entiy.Tag = proName;
                            }
                        }
                        else if (dbo is DBText)
                        {

                            DBText dbo_to_Entiy = (DBText)dbo;
                            if (dbo_to_Entiy.Layer == proNameLayer)
                            {

                                dbo_to_Entiy.TextString = proName;
                            }


                        }
                        else if (dbo is MText)
                        {
                            MText dbo_to_Entiy = (MText)dbo;
                            if (dbo_to_Entiy.Layer == proNameLayer)
                            {

                                dbo_to_Entiy.Contents = proName;
                            }

                        }

                        else if (dbo is BlockReference)
                        {
                            BlockReference dbo_to_Entiy;
                            BlockTableRecord btr_bb;
                            try
                            {
                                dbo_to_Entiy = (BlockReference)dbo;
                                btr_bb = (BlockTableRecord)trans.GetObject(bt[dbo_to_Entiy.Name], OpenMode.ForWrite);


                                foreach (var ii in btr_bb)
                                {
                                    DBObject dBObject_1 = (DBObject)trans.GetObject(ii, OpenMode.ForWrite);

                                    if (dBObject_1 is AttributeDefinition)
                                    {
                                        AttributeDefinition dbo_to_Entiy1 = (AttributeDefinition)dBObject_1;

                                        if (dbo_to_Entiy1.Layer == proNameLayer)
                                        {

                                            dbo_to_Entiy1.TextString = proName;
                                        }
                                    }
                                    else if (dBObject_1 is DBText)
                                    {
                                        DBText dbo_to_Entiy1 = (DBText)dBObject_1;
                                        if (dbo_to_Entiy1.Layer == proNameLayer)
                                        {

                                            dbo_to_Entiy1.TextString = proName;
                                        }


                                    }

                                    else if (dBObject_1 is AttributeDefinition)
                                    {
                                        AttributeDefinition dbo_to_Entiy1 = (AttributeDefinition)dBObject_1;

                                        if (dbo_to_Entiy1.Layer == proNameLayer)
                                        {

                                            dbo_to_Entiy1.TextString = proName;
                                        }

                                    }

                                }
                            }
                            catch
                            {
                                OutPrint_txt(path, "-----------------\n" + Path.GetFileName(path) + "图元存在问题----------------\n");
                            }
                        }

                    }



                    trans.Commit();

                }
        //    }
                //图纸空间
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                BlockTable blocktable_1 = trans.GetObject(db.BlockTableId, OpenMode.ForWrite) as BlockTable;
                foreach(var btrr_1 in blocktable_1)
                {
                   BlockTableRecord btr=(BlockTableRecord)trans.GetObject(btrr_1, OpenMode.ForWrite);
                    if(btr.IsLayout)
                    {
                        //foreach (ObjectId lytmp in btr)
                        //{
                          

                            foreach (ObjectId objid in btr)
                            {

                                DBObject dbo = trans.GetObject(objid, OpenMode.ForWrite);

                             //   ed.WriteMessage(dbo.GetType().ToString() + "\n");
                             //   OutPrint_txt(path, dbo.GetType().ToString() + "\n");


                                if (dbo is AttributeDefinition)
                                {
                                    AttributeDefinition dbo_to_Entiy = (AttributeDefinition)dbo;

                                    if (dbo_to_Entiy.Layer == proNameLayer)
                                    {

                                        dbo_to_Entiy.Tag = proName;
                                    }
                                }
                                else if (dbo is DBText)
                                {

                                    DBText dbo_to_Entiy = (DBText)dbo;
                                    if (dbo_to_Entiy.Layer == proNameLayer)
                                    {

                                        dbo_to_Entiy.TextString = proName;
                                    }


                                }
                                else if (dbo is MText)
                                {
                                    MText dbo_to_Entiy = (MText)dbo;
                                    if (dbo_to_Entiy.Layer == proNameLayer)
                                    {

                                        dbo_to_Entiy.Contents = proName;
                                    }

                                }

                                else if (dbo is BlockReference)
                                {
                                    BlockReference dbo_to_Entiy;
                                    BlockTableRecord btr_bb;
                                    try
                                    {
                                        dbo_to_Entiy = (BlockReference)dbo;
                                        btr_bb = (BlockTableRecord)trans.GetObject(dbo_to_Entiy.BlockId, OpenMode.ForWrite);


                                        foreach (var ii in btr_bb)
                                        {
                                            DBObject dBObject_1 = (DBObject)trans.GetObject(ii, OpenMode.ForWrite);

                                            if (dBObject_1 is AttributeDefinition)
                                            {
                                                AttributeDefinition dbo_to_Entiy1 = (AttributeDefinition)dBObject_1;

                                                if (dbo_to_Entiy1.Layer == proNameLayer)
                                                {

                                                    dbo_to_Entiy1.TextString = proName;
                                                }
                                            }
                                            else if (dBObject_1 is DBText)
                                            {
                                                DBText dbo_to_Entiy1 = (DBText)dBObject_1;
                                                if (dbo_to_Entiy1.Layer == proNameLayer)
                                                {

                                                    dbo_to_Entiy1.TextString = proName;
                                                }


                                            }

                                            else if (dBObject_1 is AttributeDefinition)
                                            {
                                                AttributeDefinition dbo_to_Entiy1 = (AttributeDefinition)dBObject_1;

                                                if (dbo_to_Entiy1.Layer == proNameLayer)
                                                {

                                                    dbo_to_Entiy1.TextString = proName;
                                                }

                                            }

                                        }
                                    }
                                    catch
                                    {
                                        OutPrint_txt(path, "-----------------\n" + Path.GetFileName(path) + "图元存在问题----------------\n");
                                    }
                                }

                            }

                        //}
                    }
                }


               // BlockTableRecord btrr =db.blocktable// db.BlockTableId.GetObject(OpenMode.ForWrite) as BlockTableRecord;
                    
                    #region

                //foreach (ObjectId lytmp in btrr)
                //{
                //    DBObject dboo = trans.GetObject(lytmp, OpenMode.ForWrite);
                //    if (dboo is Layout == false)
                //    {
                //        continue;
                //    }
                //    LayoutManager.Current.CurrentLayout = ((Layout)dboo).LayoutName;
                ////}
                

                ////{
                //    BlockTable bt = (BlockTable)trans.GetObject(db.BlockTableId, OpenMode.ForWrite);
                    

                //    ObjectId obj_id = bt[BlockTableRecord.PaperSpace];
                //    BlockTableRecord btr = (BlockTableRecord)trans.GetObject(obj_id, OpenMode.ForWrite);



                //    // BlockTableRecord btr = db.BlockTableId.GetObject(OpenMode.ForWrite) as BlockTableRecord;


                //    foreach (ObjectId objid in btr)
                //    {

                //        DBObject dbo = trans.GetObject(objid, OpenMode.ForWrite);

                //        ed.WriteMessage(dbo.GetType().ToString() + "\n");
                //        OutPrint_txt(path, dbo.GetType().ToString() + "\n");


                //        if (dbo is AttributeDefinition)
                //        {
                //            AttributeDefinition dbo_to_Entiy = (AttributeDefinition)dbo;

                //            if (dbo_to_Entiy.Layer == proNameLayer)
                //            {

                //                dbo_to_Entiy.Tag = proName;
                //            }
                //        }
                //        else if (dbo is DBText)
                //        {

                //            DBText dbo_to_Entiy = (DBText)dbo;
                //            if (dbo_to_Entiy.Layer == proNameLayer)
                //            {

                //                dbo_to_Entiy.TextString = proName;
                //            }


                //        }
                //        else if (dbo is MText)
                //        {
                //            MText dbo_to_Entiy = (MText)dbo;
                //            if (dbo_to_Entiy.Layer == proNameLayer)
                //            {

                //                dbo_to_Entiy.Contents = proName;
                //            }

                //        }

                //        else if (dbo is BlockReference)
                //        {
                //            BlockReference dbo_to_Entiy;
                //            BlockTableRecord btr_bb;
                //            try
                //            {
                //                dbo_to_Entiy = (BlockReference)dbo;
                //                btr_bb = (BlockTableRecord)trans.GetObject(bt[dbo_to_Entiy.Name], OpenMode.ForWrite);


                //                foreach (var ii in btr_bb)
                //                {
                //                    DBObject dBObject_1 = (DBObject)trans.GetObject(ii, OpenMode.ForWrite);

                //                    if (dBObject_1 is AttributeDefinition)
                //                    {
                //                        AttributeDefinition dbo_to_Entiy1 = (AttributeDefinition)dBObject_1;

                //                        if (dbo_to_Entiy1.Layer == proNameLayer)
                //                        {

                //                            dbo_to_Entiy1.TextString = proName;
                //                        }
                //                    }
                //                    else if (dBObject_1 is DBText)
                //                    {
                //                        DBText dbo_to_Entiy1 = (DBText)dBObject_1;
                //                        if (dbo_to_Entiy1.Layer == proNameLayer)
                //                        {

                //                            dbo_to_Entiy1.TextString = proName;
                //                        }


                //                    }

                //                    else if (dBObject_1 is AttributeDefinition)
                //                    {
                //                        AttributeDefinition dbo_to_Entiy1 = (AttributeDefinition)dBObject_1;

                //                        if (dbo_to_Entiy1.Layer == proNameLayer)
                //                        {

                //                            dbo_to_Entiy1.TextString = proName;
                //                        }

                //                    }

                //                }
                //            }
                //            catch
                //            {
                //                OutPrint_txt(path, "-----------------\n" + Path.GetFileName(path) + "图元存在问题----------------\n");
                //            }
                //        }

                //    }

                //}

                //图纸空间

                #endregion
                trans.Commit();

            }

            doc_lock.Dispose();
            middoc.CloseAndSave(path);



        }
            catch
            {
               OutPrint_txt(path, "-----------------\n" + Path.GetFileName(path) + "存在问题!请检查!!\n----------------\n");


            }

        }

      public static void   zjy_Change_Project_Name_open_dwg(string new_name)
        {
            Autodesk.AutoCAD.ApplicationServices.Document middoc=cadSer.Application.DocumentManager.MdiActiveDocument;
            Autodesk.AutoCAD.DatabaseServices.Database db= HostApplicationServices.WorkingDatabase;

            string proNameLayer = "zjy_项目名称";

            string proName = new_name;

            var doc_lock = middoc.LockDocument();

            Editor ed = middoc.Editor; //模型空间
            string path = middoc.Name;
            ed.WriteMessage("\n当前文件名称"+path+"\n");
           // if (Path.GetFileName(path).Contains("平面图") || Path.GetFileName(path).Contains("平纵缩"))
            //{
                using (Transaction trans = db.TransactionManager.StartTransaction())
                {

                    BlockTable bt = (BlockTable)trans.GetObject(db.BlockTableId, OpenMode.ForWrite);


                    ObjectId obj_id = bt[BlockTableRecord.ModelSpace];
                    BlockTableRecord btr = (BlockTableRecord)trans.GetObject(obj_id, OpenMode.ForWrite);

                    foreach (ObjectId objid in btr)
                    {

                        DBObject dbo = trans.GetObject(objid, OpenMode.ForWrite);
                        // ed.WriteMessage(dbo.GetType().ToString() + "\n");

                        if (dbo is AttributeDefinition)
                        {
                            AttributeDefinition dbo_to_Entiy = (AttributeDefinition)dbo;

                            if (dbo_to_Entiy.Layer == proNameLayer)
                            {

                                dbo_to_Entiy.Tag = proName;
                            }
                        }
                        else if (dbo is DBText)
                        {

                            DBText dbo_to_Entiy = (DBText)dbo;
                            if (dbo_to_Entiy.Layer == proNameLayer)
                            {

                                dbo_to_Entiy.TextString = proName;
                            }


                        }
                        else if (dbo is MText)
                        {
                            MText dbo_to_Entiy = (MText)dbo;
                            if (dbo_to_Entiy.Layer == proNameLayer)
                            {

                                dbo_to_Entiy.Contents = proName;
                            }

                        }

                        else if (dbo is BlockReference)
                        {
                            BlockReference dbo_to_Entiy;
                            BlockTableRecord btr_bb;
                            try
                            {
                                dbo_to_Entiy = (BlockReference)dbo;
                                btr_bb = (BlockTableRecord)trans.GetObject(bt[dbo_to_Entiy.Name], OpenMode.ForWrite);


                                foreach (var ii in btr_bb)
                                {
                                    DBObject dBObject_1 = (DBObject)trans.GetObject(ii, OpenMode.ForWrite);

                                    if (dBObject_1 is AttributeDefinition)
                                    {
                                        AttributeDefinition dbo_to_Entiy1 = (AttributeDefinition)dBObject_1;

                                        if (dbo_to_Entiy1.Layer == proNameLayer)
                                        {

                                            dbo_to_Entiy1.TextString = proName;
                                        }
                                    }
                                    else if (dBObject_1 is DBText)
                                    {
                                        DBText dbo_to_Entiy1 = (DBText)dBObject_1;
                                        if (dbo_to_Entiy1.Layer == proNameLayer)
                                        {

                                            dbo_to_Entiy1.TextString = proName;
                                        }


                                    }

                                    else if (dBObject_1 is AttributeDefinition)
                                    {
                                        AttributeDefinition dbo_to_Entiy1 = (AttributeDefinition)dBObject_1;

                                        if (dbo_to_Entiy1.Layer == proNameLayer)
                                        {

                                            dbo_to_Entiy1.TextString = proName;
                                        }

                                    }

                                }
                            }
                            catch
                            {
                                OutPrint_txt(path, "-----------------\n" + Path.GetFileName(path) + "图元存在问题----------------\n");
                            }
                        }

                    }



                    trans.Commit();

                }
           // }
            //图纸空间
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                BlockTable blocktable_1 = trans.GetObject(db.BlockTableId, OpenMode.ForWrite) as BlockTable;
                foreach (var btrr_1 in blocktable_1)
                {
                    BlockTableRecord btr = (BlockTableRecord)trans.GetObject(btrr_1, OpenMode.ForWrite);
                    if (btr.IsLayout)
                    {
                        //foreach (ObjectId lytmp in btr)
                        //{


                        foreach (ObjectId objid in btr)
                        {

                            DBObject dbo = trans.GetObject(objid, OpenMode.ForWrite);

                            //   ed.WriteMessage(dbo.GetType().ToString() + "\n");
                            //   OutPrint_txt(path, dbo.GetType().ToString() + "\n");


                            if (dbo is AttributeDefinition)
                            {
                                AttributeDefinition dbo_to_Entiy = (AttributeDefinition)dbo;

                                if (dbo_to_Entiy.Layer == proNameLayer)
                                {

                                    dbo_to_Entiy.Tag = proName;
                                }
                            }
                            else if (dbo is DBText)
                            {

                                DBText dbo_to_Entiy = (DBText)dbo;
                                if (dbo_to_Entiy.Layer == proNameLayer)
                                {

                                    dbo_to_Entiy.TextString = proName;
                                }


                            }
                            else if (dbo is MText)
                            {
                                MText dbo_to_Entiy = (MText)dbo;
                                if (dbo_to_Entiy.Layer == proNameLayer)
                                {

                                    dbo_to_Entiy.Contents = proName;
                                }

                            }

                            else if (dbo is BlockReference)
                            {
                                BlockReference dbo_to_Entiy;
                                BlockTableRecord btr_bb;
                                try
                                {
                                    dbo_to_Entiy = (BlockReference)dbo;
                                    btr_bb = (BlockTableRecord)trans.GetObject(dbo_to_Entiy.BlockId, OpenMode.ForWrite);


                                    foreach (var ii in btr_bb)
                                    {
                                        DBObject dBObject_1 = (DBObject)trans.GetObject(ii, OpenMode.ForWrite);

                                        if (dBObject_1 is AttributeDefinition)
                                        {
                                            AttributeDefinition dbo_to_Entiy1 = (AttributeDefinition)dBObject_1;

                                            if (dbo_to_Entiy1.Layer == proNameLayer)
                                            {

                                                dbo_to_Entiy1.TextString = proName;
                                            }
                                        }
                                        else if (dBObject_1 is DBText)
                                        {
                                            DBText dbo_to_Entiy1 = (DBText)dBObject_1;
                                            if (dbo_to_Entiy1.Layer == proNameLayer)
                                            {

                                                dbo_to_Entiy1.TextString = proName;
                                            }


                                        }

                                        else if (dBObject_1 is AttributeDefinition)
                                        {
                                            AttributeDefinition dbo_to_Entiy1 = (AttributeDefinition)dBObject_1;

                                            if (dbo_to_Entiy1.Layer == proNameLayer)
                                            {

                                                dbo_to_Entiy1.TextString = proName;
                                            }

                                        }

                                    }
                                }
                                catch
                                {
                                    OutPrint_txt(path, "-----------------\n" + Path.GetFileName(path) + "图元存在问题----------------\n");
                                }
                            }
                        
                        }

                        //}
                    }
                }

             
                trans.Commit();
               

            }

            doc_lock.Dispose();
           
           
        }




        public static void OutPrint_txt(string filetmp,string message)
        {
            string print_out_info_path = Path.GetDirectoryName(filetmp) + "\\print_info_z.txt";

            //using (StreamWriter sw = new StreamWriter(print_out_info_path))
            //{
            //    sw.(message);
            //}
            using(StreamWriter file =File.AppendText(print_out_info_path))
            {
                file.WriteLine(message);
            }
        }

        public static  void zjyCadPrint(string deviceStr, int direction_0_90, string dwg_File)
        {

           
            try
            {
                Autodesk.AutoCAD.ApplicationServices.Document middoc;
                 Autodesk.AutoCAD.DatabaseServices.Database db;
                 middoc = cadSer.Application.DocumentManager.Open(dwg_File, false);
                 db = HostApplicationServices.WorkingDatabase;

                HostApplicationServices.WorkingDatabase = middoc.Database;

                //开始打印文件
                //zjy_event();
                /////////
                ///注意之后通过文件名称中特定字符进行判别模型空间或布局平面
              //  if (dwg_File.Contains("路线平面")|| (dwg_File.Contains("平")&& dwg_File.Contains("纵")&& dwg_File.Contains("缩")) || (dwg_File.Contains("平面")&& dwg_File.Contains("总体")))
              //  {
                    LayoutPMT_Plot(deviceStr, direction_0_90);//对于  布局图  通过 布局名字判别。
               // }
               // else
                //{
                    Model_Plot(deviceStr, direction_0_90);
               // }

                HostApplicationServices.WorkingDatabase = db;
                middoc.CloseAndDiscard();
            }catch
            {
                OutPrint_txt(dwg_File, "\n-----------------\n" + Path.GetFileName(dwg_File) + "存在问题!请检查!!\n----------------\n");


            }
        }


        public static List<string> Sort_Name_As_Windows(List<string> files)
        {
            [DllImport("shlwapi.dll", CharSet = CharSet.Unicode)]
            static extern int StrCmpLogicalW(string psz1, string psz2);

            files.Sort((a, b) => StrCmpLogicalW(a, b));
            return files;
        }

        public static void ExcelPrint(string path, string printName, int endPage)
        {
            //string path = @"G:\新建文件夹\Desktop\111\1.xlsx";
            Excel.Application excelApp = new Excel.Application();
            excelApp.Visible = true;

            excelApp.DisplayAlerts = false;
            excelApp.AskToUpdateLinks = false;
            //  Excel.Workbook excelWB = excelApp.Workbooks.Add(path);
            Excel.Workbook excelWB = excelApp.Workbooks.Open(Filename: path);//,Notify:false,UpdateLinks:0,IgnoreReadOnlyRecommended:true);
  
            Excel.Sheets excelSheets = excelWB.Sheets;

            foreach (Excel.Worksheet tmp in excelSheets)
            {
                if ((int)tmp.Visible > -1) continue;
                Console.WriteLine(tmp.Name + "\n");
                if (endPage > 0)
                {
                    tmp.PrintOutEx(1, endPage, 1, false, printName);//, false, false);


                }
                else if (tmp.PageSetup.Pages.Count > 0)
                {
                    // if(tmp.PageSetup.Pages.Count>0)

                    tmp.PrintOutEx(1, tmp.PageSetup.Pages.Count, 1, false, printName);//, false, false);
                }


            }



            excelWB.Close(false);

            excelApp.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
        }

        public static void WordPrint(string path, string printName)
        {

            Word.Application wordApp = new Word.Application();
            wordApp.Visible = true;

            Word.Document wordDoc = wordApp.Documents.OpenNoRepairDialog(path);

            wordApp.ActivePrinter = printName;
            wordDoc.PrintOut();

            wordDoc.Close(false);
            wordApp.Quit();

        }

    

        public static void EditProjectName(string proName)
        {
            string proNameLayer = "zjy_项目名称";

            Database db = HostApplicationServices.WorkingDatabase; 

       
          //  DBObjectCollection dbCollection = new DBObjectCollection();
          
            var doc_lock = cadSer.Application.DocumentManager.MdiActiveDocument.LockDocument();
           
            Editor ed= cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {

                BlockTable bt = (BlockTable)trans.GetObject(db.BlockTableId, OpenMode.ForWrite);

                ObjectId obj_id = bt[BlockTableRecord.ModelSpace];
                BlockTableRecord btr = (BlockTableRecord)trans.GetObject(obj_id, OpenMode.ForWrite);

                foreach (ObjectId objid in btr)
                {

                    DBObject dbo = trans.GetObject(objid, OpenMode.ForWrite);
                    ed.WriteMessage(   dbo.GetType().ToString()+"\n");

                    if (dbo is AttributeDefinition)
                    {
                        AttributeDefinition dbo_to_Entiy = (AttributeDefinition)dbo;

                        if (dbo_to_Entiy.Layer == proNameLayer)
                        {

                            dbo_to_Entiy.Tag = proName;
                        }
                    }else 
                        if (dbo is DBText)
                    {

                        DBText dbo_to_Entiy = (DBText)dbo;
                        if (dbo_to_Entiy.Layer == proNameLayer)
                        {

                            dbo_to_Entiy.TextString= proName;
                        }
                        
             
                    }
                    else if (dbo is MText)
                    {
                        MText dbo_to_Entiy = (MText)dbo;
                        if (dbo_to_Entiy.Layer == proNameLayer)
                        {

                            dbo_to_Entiy.Contents = proName;
                        }
                       
                    }
                    
                    else if (dbo is BlockReference)
                    {
                        try
                        {
                            BlockReference dbo_to_Entiy = (BlockReference)dbo;
                            BlockTableRecord btr_bb = (BlockTableRecord)trans.GetObject(bt[dbo_to_Entiy.Name], OpenMode.ForWrite);


                            foreach (var ii in btr_bb)
                            {
                                DBObject dBObject_1 = (DBObject)trans.GetObject(ii, OpenMode.ForWrite);

                                if (dBObject_1 is AttributeDefinition)
                                {
                                    AttributeDefinition dbo_to_Entiy1 = (AttributeDefinition)dBObject_1;

                                    if (dbo_to_Entiy1.Layer == proNameLayer)
                                    {

                                        dbo_to_Entiy1.TextString = proName;
                                    }
                                }
                                else if (dBObject_1 is DBText)
                                {
                                    DBText dbo_to_Entiy1 = (DBText)dBObject_1;
                                    if (dbo_to_Entiy1.Layer == proNameLayer)
                                    {

                                        dbo_to_Entiy1.TextString = proName;
                                    }


                                }

                                else if (dBObject_1 is AttributeDefinition)
                                {
                                    AttributeDefinition dbo_to_Entiy1 = (AttributeDefinition)dBObject_1;

                                    if (dbo_to_Entiy1.Layer == proNameLayer)
                                    {

                                        dbo_to_Entiy1.TextString = proName;
                                    }

                                }

                            }

                        }catch
                        {
                            ed.WriteMessage("无效块\n");
                        }
                        }

                }

                trans.Commit();
                
            }
            doc_lock.Dispose();
         
        }

        //public static DBObjectCollection Get_All_Object_WorkingSpace()
        //{
        //    Database db = HostApplicationServices.WorkingDatabase; //;)// ;

        //    //  {
        //    DBObjectCollection dbCollection = new DBObjectCollection();///)
        //    //{
        //    var doc_lock = cadSer.Application.DocumentManager.MdiActiveDocument.LockDocument();
        //    //DBObjectCollection dbCollection = new DBObjectCollection();
        //    using (Transaction trans = db.TransactionManager.StartTransaction())
        //    {

        //        BlockTable bt = (BlockTable)trans.GetObject(db.BlockTableId, OpenMode.ForWrite);

        //        ObjectId obj_id = bt[BlockTableRecord.ModelSpace];
        //        BlockTableRecord btr = (BlockTableRecord)trans.GetObject(obj_id, OpenMode.ForRead);

        //        foreach (ObjectId objid in btr)
        //        {

        //            DBObject dbo = trans.GetObject(objid, OpenMode.ForRead);
        //            dbCollection.Add(dbo);
        //            // dbo.Dispose();

        //        }

        //        trans.Commit();
        //    }
        //    doc_lock.Dispose();
        //    //db.Dispose();
        //    //dbCollection.Dispose();
        //    return dbCollection;
        //    //}
        //    //  }


        //}





        //-------------------------获取模型空间的所有物体------------------------------------
        public static DBObjectCollection Get_All_Object_WorkingSpace()
        {
            Database db = HostApplicationServices.WorkingDatabase; //;)// ;

            //  {
            DBObjectCollection dbCollection = new DBObjectCollection();///)
            //{
            var doc_lock = cadSer.Application.DocumentManager.MdiActiveDocument.LockDocument();
            //DBObjectCollection dbCollection = new DBObjectCollection();
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {

                BlockTable bt = (BlockTable)trans.GetObject(db.BlockTableId, OpenMode.ForWrite);

                ObjectId obj_id = bt[BlockTableRecord.ModelSpace];
                BlockTableRecord btr = (BlockTableRecord)trans.GetObject(obj_id, OpenMode.ForRead);

                foreach (ObjectId objid in btr)
                {

                    DBObject dbo = trans.GetObject(objid, OpenMode.ForRead);
                    dbCollection.Add(dbo);
                    // dbo.Dispose();

                }

                trans.Commit();
            }
            doc_lock.Dispose();
            //db.Dispose();
            //dbCollection.Dispose();
            return dbCollection;
            //}
            //  }


        }
        //-------------------------获取模型空间的所有物体------------------------------------



        //-------------------------获取模型空间的层与颜色筛选物体----------------------------
        //-------------------------获取模型空间的层与颜色筛选物体----------------------------

        #region
        ////获得在模型空间所有在层LayerName中的对象， 是dbobject   collection，不是objectID
        public static DBObjectCollection GetObjects(string LayerName)
        {
            //using (DBObjectCollection dBObjectCollection = Get_All_Object_WorkingSpace())
            //{

            DBObjectCollection dBObjectCollection = Get_All_Object_WorkingSpace();
            return GetObjects(LayerName, dBObjectCollection);
            //}
        }

        //获得在某一选择集中是在层LayerName中的对象 是dbobject   collection，不是objectID
        public static DBObjectCollection GetObjects(string LayerName, DBObjectCollection dbCollection_AllObjects)
        {
            DBObjectCollection dbCollection = new DBObjectCollection();//)
                                                                       // {

            // DBObjectCollection dbCollection_AllObjects = Get_All_Object_WorkingSpace();

            foreach (DBObject dbo in dbCollection_AllObjects)
            {
                //DBObject dbo = id_To_Object(objid);
                Entity dbo_to_Entiy = (Entity)dbo;
                if (dbo_to_Entiy.Layer == LayerName)
                {
                    if (dbCollection.Contains(dbo) == false)
                    {
                        dbCollection.Add(dbo);
                    }
                }
                // dbo.Dispose();
                // dbo_to_Entiy.Dispose();
            }


            return dbCollection;
            //}

        }
        #endregion

    }

    public class zPointXY
    {
        public zPointXY(double xx, double yy)
        {
            x = xx; y = yy;
        }
        public zPointXY() { }
        public double x { set; get; }
        public double y { set; get; }
        public static zPointXY operator +(zPointXY a, zPointXY b)
        {
            return new zPointXY(a.x + b.x, a.y + b.y);
        }
        public static zPointXY operator -(zPointXY a, zPointXY b)
        {
            return new zPointXY(a.x- b.x, a.y - b.y);
        }
        public static double zLength(zPointXY a, zPointXY b)
        {
            return Math.Sqrt((a.x-b.x)* (a.x - b.x)+(a.y - b.y) *( a.y - b.y));
        }

    }
   public class zHDMPoint
    {
        public zHDMPoint() { }
        public zHDMPoint(SortedDictionary<double, Point3d> _dmPointSortedDic) {

            dmPointSortedDic = _dmPointSortedDic;
        }
        public SortedDictionary<double, Point3d> dmPointSortedDic = new SortedDictionary<double, Point3d>();
       
    }

}

