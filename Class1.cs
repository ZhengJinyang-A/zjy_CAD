using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Text;
using System.Runtime.InteropServices;
using cadSer = Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.DatabaseServices;
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

[assembly: CommandClass(typeof(zjy.zjyCAD))]
namespace zjy
{
    class zjyCAD
    {


        /// <summary>
        /// ///////////////////////////////////////////////////////////////////////////////////////
        /// ///////////////////////////////////////////////////////////////////////////////////////
        /// //////////////////////////////WGS84=高斯投影=web墨卡托///////////////////////////////
        /// ///////////////////////////////////////////////////////////////////////////////////////
        /// ///////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        // Directory.get
        #region  public void RealKML()
        [CommandMethod("brt")]
        public void RealKML()
        {
            Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
            string MKLDirctory = @"C:\Users\Administrator\Documents\WeChat Files\zhengjinyangwo\FileStorage\File\2020-03";
            List<string> ContentList = new List<string>();
            string[] AllFiles = Directory.GetFiles(MKLDirctory);
            Dictionary<string, List<zPointXY>> PointsDic = new Dictionary<string, List<zPointXY>>();
            foreach (string tmp in AllFiles)
            {
                if (tmp.Contains(".kml"))
                {
                    Console.WriteLine(tmp);
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
            foreach (var tmp in PointsDic)
            {
                ed.WriteMessage("绘制polyline\n");
                zPolylineCreate(JW84ToWebMercatorXY(tmp.Value));

                zCircleCreate(JW84ToWebMercatorXY(tmp.Value[0]), 60);

                zCircleCreate(JW84ToWebMercatorXY(tmp.Value[tmp.Value.Count - 1]), 40);
                zCircleCreate(JW84ToWebMercatorXY(tmp.Value[tmp.Value.Count - 1]), 20);

                zCreatMtext(Path.GetFileName(tmp.Key), JW84ToWebMercatorXY(tmp.Value[0]), 50, 400);



                zPolylineCreate(WGS84_BLToXY(tmp.Value));

                zCircleCreate(WGS84_BLToXY(tmp.Value[0]),60);

                zCircleCreate(WGS84_BLToXY(tmp.Value[tmp.Value.Count-1]),40);
                zCircleCreate(WGS84_BLToXY(tmp.Value[tmp.Value.Count-1]),20);

                zPolylineCreate(WGS84_BLToXY_1(tmp.Value));
            }

            //   Console.ReadLine();
        }

       //WGS84经纬度坐标 转为   高斯投影
        public zPointXY WGS84_BLToXY(zPointXY zBL,double centureL=111)
        {
            //centureL 为中央子午线经度
          //  Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
           //纬度
            double BB = zBL.y;
            //经纬度-中央子午线经度
            double LL = zBL.x-centureL;
            double pi = Math.PI;
            double BBr = BB * pi/ 180;
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
            double yita2 = ((a * a )/( b * b )- 1) * cosB * cosB;
            //ed.WriteMessage("e2："+e2.ToString()+"\n");
            //ed.WriteMessage("yita2："+yita2.ToString()+"\n");
            //double pi = Math.PI;

            double W = Math.Sqrt(1 - e2 * sinB * sinB);
            //ed.WriteMessage("WWWWWW："+W.ToString()+"\n");
            double N = a / W;
            //ed.WriteMessage("NNNNNNNN："+N.ToString()+"\n");
            double m0 = a*(1-e2);
            double m2 = 3.0/2.0*e2*m0;
            double m4 = 5.0/4.0*e2*m2;
            double m6 = 7.0/6.0*e2*m4;
            double m8 = 9.0/8.0*e2*m6;
            double a0 = m0+m2/2+3.0/8*m4+5.0/16*m6+35.0/128*m8;
            double a2 = m2/2+m4/2+15.0/32*m6+7.0/16*m8;
            double a4 = m4/8+3.0/16*m6+7.0/32*m8;
            double a6 = m6/32+m8/16;
            double a8 = m8/128;
            //中央子午线经度在纬度为0的坐标，相对于0经度
            double zY = a*centureL*pi/180.0;
        //X向为纬度方向
            //ed.WriteMessage("a0a0a0a0a0a0："+a0.ToString()+"\n");
            double X = a0*BBr-sinB*cosB*((a2-a4+a6)+(2*a4-16.0/3*a6)*sinB*sinB+16.0/3*a6*sinB*sinB*sinB*sinB);
            //ed.WriteMessage("XXXX坐标："+X.ToString()+"\n");

            double x = X+N/2*t*cosB*cosB*LLr*LLr;
            x+=N/24*t*(5.0-t*t+9*yita2+4*yita2*yita2)*Math.Pow(cosB,4)*Math.Pow(LLr,4);
            x+=N/720*t*(61.0-58.0*t*t+Math.Pow(t,4))*Math.Pow(cosB,6)*Math.Pow(LLr,6);
           
            //Y向为经度方向
            double y = N*cosB*LLr+N/6*(1-t*t+yita2)*cosB*cosB*cosB*LLr*LLr*LLr;
            y+=N/120*(5-18*t*t+Math.Pow(t,4)+14*yita2-58*yita2*t*t)*Math.Pow(cosB,5)*Math.Pow(LLr,5);
            y+=500000;
            //ed.WriteMessage("y坐标："+x.ToString()+"\n");
            //ed.WriteMessage("x坐标："+y.ToString()+"\n");
            return new zPointXY(y,x);

        }

        public List<zPointXY> WGS84_BLToXY(List<zPointXY> jwList,double centureL = 111)
        {
            List<zPointXY> zPList = new List<zPointXY>();
            foreach(zPointXY i in jwList)
            {
                zPList.Add(WGS84_BLToXY(i,centureL));
            }
            return zPList;
        }


        //WGS84经纬度坐标 转为   弧长 坐标,类似于墨卡托,非平面类型
        public zPointXY WGS84_BLToXY_1(zPointXY zBL)
        {
            //centureL 为中央子午线经度
            //  Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
            //纬度
            double BB = zBL.y;
            //经纬度-中央子午线经度
            double LL = zBL.x;
            double pi = Math.PI;
            double BBr = BB*pi/180;
            //double LLr = LL*pi/180;
            double sinB = Math.Sin(BBr);
            double cosB = Math.Cos(BBr);
            //double t = Math.Tan(BBr);
            //ed.WriteMessage("BB："+BB.ToString()+"\n");
            //ed.WriteMessage("LL："+LL.ToString()+"\n");
            //ed.WriteMessage("sinB："+sinB.ToString()+"\n");
            double a = 6378137.0;
            double b = 6356752.3142;
            double e2 = 1-(b*b)/(a*a);
            //double yita2 = ((a*a)/(b*b)-1)*cosB*cosB;
            //ed.WriteMessage("e2："+e2.ToString()+"\n");
            //ed.WriteMessage("yita2："+yita2.ToString()+"\n");
            //double pi = Math.PI;

            double W = Math.Sqrt(1-e2*sinB*sinB);
            //ed.WriteMessage("WWWWWW："+W.ToString()+"\n");
            //double N = a/W;
            //ed.WriteMessage("NNNNNNNN："+N.ToString()+"\n");
            double m0 = a*(1-e2);
            double m2 = 3.0/2.0*e2*m0;
            double m4 = 5.0/4.0*e2*m2;
            double m6 = 7.0/6.0*e2*m4;
            double m8 = 9.0/8.0*e2*m6;
            double a0 = m0+m2/2+3.0/8*m4+5.0/16*m6+35.0/128*m8;
            double a2 = m2/2+m4/2+15.0/32*m6+7.0/16*m8;
            double a4 = m4/8+3.0/16*m6+7.0/32*m8;
            double a6 = m6/32+m8/16;
            double a8 = m8/128;
            //中央子午线经度在纬度为0的坐标，相对于0经度
            double zY = a*LL*pi/180.0;
            //X向为纬度方向
            //ed.WriteMessage("a0a0a0a0a0a0："+a0.ToString()+"\n");
            double X = a0*BBr-sinB*cosB*((a2-a4+a6)+(2*a4-16.0/3*a6)*sinB*sinB+16.0/3*a6*sinB*sinB*sinB*sinB);
            //ed.WriteMessage("XXXX坐标："+X.ToString()+"\n");

            double x = X;//+N/2*t*cosB*cosB*LLr*LLr;
            //x+=N/24*t*(5.0-t*t+9*yita2+4*yita2*yita2)*Math.Pow(cosB,4)*Math.Pow(LLr,4);
            //x+=N/720*t*(61.0-58.0*t*t+Math.Pow(t,4))*Math.Pow(cosB,6)*Math.Pow(LLr,6);

            //Y向为经度方向
            //double y = N*cosB*LLr+N/6*(1-t*t+yita2)*cosB*cosB*cosB*LLr*LLr*LLr;
            //y+=N/120*(5-18*t*t+Math.Pow(t,4)+14*yita2-58*yita2*t*t)*Math.Pow(cosB,5)*Math.Pow(LLr,5);
            double y=zY;
            //ed.WriteMessage("y坐标："+x.ToString()+"\n");
            //ed.WriteMessage("x坐标："+y.ToString()+"\n");
            return new zPointXY(y,x);

        }

        public List<zPointXY> WGS84_BLToXY_1(List<zPointXY> jwList)
        {
            List<zPointXY> zPList = new List<zPointXY>();
            foreach(zPointXY i in jwList)
            {
                zPList.Add(WGS84_BLToXY_1(i));
            }
            return zPList;
        }


        public void zCreatMtext(string textString, zPointXY location, double height, double width)
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
                // ed.WriteMessage("起终点\n");
                // Point3d point3D = new Point3d(zPoint.x, zPoint.y, 0);

                // Circle cir = new Circle(point3D, new Vector3d(0, 0, 1), r);
                //是否存在层，无则新建
                //为了放辅助的线
                string layerName = "zjy_Temp_Polyline";
                //  txt.Color = Color.FromColorIndex(ColorMethod.ByAci, 3);
                LayerCreate(db, trans, layerName);

                txt.Layer = layerName;


                ToModelSpace(txt, db);
                trans.Commit();
            }
        }
        public void zCircleCreate(zPointXY zPoint, double r)
        {

            Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
            Database db = HostApplicationServices.WorkingDatabase;
            var middoc = cadSer.Application.DocumentManager.MdiActiveDocument;
            var doc_lock = middoc.LockDocument();
            //  Polyline polyline = new Polyline();


            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                ed.WriteMessage("起终点\n");
                Point3d point3D = new Point3d(zPoint.x, zPoint.y, 0);

                Circle cir = new Circle(point3D, new Vector3d(0, 0, 1), r);
                //是否存在层，无则新建
                //为了放辅助的线
                string layerName = "zjy_Temp_Polyline";
                cir.Color = Color.FromColorIndex(ColorMethod.ByAci, 2);
                LayerCreate(db, trans, layerName);

                cir.Layer = layerName;


                ToModelSpace(cir, db);
                trans.Commit();
            }

        }


        public void zPolylineCreate(List<zPointXY> list)
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
                LayerCreate(db, trans, layerName);
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


        private static bool LayerCreate(Database db, Transaction trans, string layername)
        {
            LayerTable lt = (LayerTable)trans.GetObject(db.LayerTableId, OpenMode.ForWrite);
            string layerName = layername;
            if (!lt.Has(layerName))
            {
                LayerTableRecord ltr = new LayerTableRecord();
                ltr.Name = layerName;
                lt.Add(ltr);
                ltr.Color = Color.FromColorIndex(ColorMethod.ByAci, 7);
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
        public void PolyLineElevation()
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
                    ed.WriteMessage("=======未处理的条数{0}======================\n",polyLineList.Count);
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


                    while(true)
                    {
                        List<int> usedIndexList = new List<int>();
                        for (int j = 1; j < polyLineList.Count(); j++)
                        {
                            //ed.WriteMessage("===============nn======================\n");
                            Polyline _temp = polyLineList[0];
                             Polyline _temp_1 = polyLineList[0];
                            // ed.WriteMessage("==============tempPL====================\n");
                            if (Compare( ref _temp,polyLineList[j],db,_err_))
                            {
                              usedIndexList.Add(j);
                                 RemoveModelSpace(_temp_1.ObjectId,db);
                            polyLineList[0]=_temp;
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

                            for(int _i=0;_i<polyLineList.Count;_i++)
                            {
                                if(usedIndexList.Contains(_i))
                                {
                                    RemoveModelSpace(polyLineList[_i].ObjectId,db);
                                continue;
                                }
                                _zjyTemp.Add(polyLineList[_i]);

                            }
                        usedIndexList.Clear();
                        polyLineList=_zjyTemp;
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
        public bool RemoveModelSpace(ObjectId objId, Database db)
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
        public bool ToModelSpace(Entity ent, Database db)
        {
            Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
            try
            {
                if (ent != null)
                {
                    using (Transaction trans = db.TransactionManager.StartTransaction())
                    {
                        BlockTable bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                        BlockTableRecord modelSpace = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;
                        DBObject dBHas;
                        //try
                        //{
                        //    dBHas = trans.GetObject(ent.ObjectId, OpenMode.ForRead);
                        //}
                        //catch
                        //{
                        //    modelSpace.AppendEntity(ent);
                        //    trans.AddNewlyCreatedDBObject(ent, true);
                        //}

                        try
                        {
                            modelSpace.AppendEntity(ent);
                            trans.AddNewlyCreatedDBObject(ent, true);
                        }
                        catch
                        {
                        }
                        //modelSpace.is

                        trans.Commit();
                    }
                    return true;
                }
            }

            catch { ed.WriteMessage("在保存到数据库时出错\n"); return false; }
            return false;
        }
        //比较两polyline是否端点是否在误差距离范围内。
        //如果在距离范围内形成一个新的polyline,然后删除其原来的polyline,然后返回true；否则返回false.
        public bool Compare( ref Polyline p_source, Polyline p_append, Database db,double err=1.0)
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
            if(point3Ds_append[0].DistanceTo(point3DSource[0])<=error)
            {
                for(int i = point3Ds_append.Count-1;i>=0;i--)
                {
                    pointList.Add(point3Ds_append[i]);
                }
                foreach(Point3d pt in point3DSource)
                {
                    pointList.Add(pt);
                }
            }
            else if(point3Ds_append[0].DistanceTo(point3DSource[point3DSource.Count-1])<=error)
            {
                foreach(Point3d pt in point3DSource)
                {
                    pointList.Add(pt);
                }
                foreach(Point3d pt in point3Ds_append)
                {
                    pointList.Add(pt);
                }

            }
            else if(point3Ds_append[point3Ds_append.Count-1].DistanceTo(point3DSource[0])<=error)
            {
                foreach(Point3d pt in point3Ds_append)
                {
                    pointList.Add(pt);
                }
                foreach(Point3d pt in point3DSource)
                {
                    pointList.Add(pt);
                }
            }
            else if(point3Ds_append[point3Ds_append.Count-1].DistanceTo(point3DSource[point3DSource.Count-1])<=error)
            {
                foreach(Point3d pt in point3DSource)
                {
                    pointList.Add(pt);
                }
                for(int i = point3Ds_append.Count-1;i>=0;i--)
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
            foreach(Point3d temP in pointList)
            {
                Point2d point2 = new Point2d(temP.X,temP.Y);
                pp.AddVertexAt(index_pp,point2,0.0,width,width);
            }
            // p_source.remo
            using(Transaction trans = db.TransactionManager.StartTransaction())
            {
                string _layerName = p_source.Layer;
                //Polyline pSourse = trans.GetObject(p_source.ObjectId,OpenMode.ForWrite) as Polyline;
                p_source=pp;

                BlockTable bt = trans.GetObject(db.BlockTableId,OpenMode.ForRead) as BlockTable;
                BlockTableRecord modelSpace = trans.GetObject(bt[BlockTableRecord.ModelSpace],OpenMode.ForWrite) as BlockTableRecord;
                p_source.Layer=_layerName;
                modelSpace.AppendEntity(p_source);
                trans.AddNewlyCreatedDBObject(p_source,true);
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
        #endregion

        /// <summary>
        /// ///////////////////////////////////////////////////////////////////////////////////////
        /// ///////////////////////////////////////////////////////////////////////////////////////
        /// //////////////////////////////批量打印////////////////////////////////////////////
        /// ///////////////////////////////////////////////////////////////////////////////////////
        /// ///////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        //bool ISContinuePrint = true;
        //[CommandMethod("ppt",CommandFlags.Session)]
        [CommandMethod("ppt")]

        public void zjyPrint()
        {
            string zjyProjectName = @"E:\古县项目20200331-修改 - 副本\";
            string[] dirList = Directory.GetDirectories(zjyProjectName);//,@"*.dwg");

            Regex rr = new Regex(@"[^0-9]");

            List<string> dirUseList = new List<string>();
            foreach(string dirTmp in dirList)
            {

                //if(rr.IsMatch(dirTmp))
                {
                    dirUseList.Add(dirTmp);
                }
            }
            dirUseList.Sort();

          

            foreach(string ii in dirUseList)
            {
                string[] dwgFile = Directory.GetFiles(ii,"*.dwg");
                foreach (string _tmp_dwg in dwgFile)
                {



                    //var middoc = cadSer.Application.DocumentManager.Open(_tmp_dwg, false);
                    var db = HostApplicationServices.WorkingDatabase;
                    using (Database dbb = new Database(true, false))
                    {
                        dbb.ReadDwgFile (_tmp_dwg,FileShare.Read,false,null);

                        //middoc.Window.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                        HostApplicationServices.WorkingDatabase = dbb;
                        //HostApplicationServices.WorkingDatabase = middoc.Database;
                        zjy_event(dbb,db);
                        //可以正常关闭了
                        //middoc.CloseAndDiscard();
                        HostApplicationServices.WorkingDatabase = db;
                    }
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

        private void zjy_event(Database workDB,Database currenDB)//(object sender,DocumentCollectionEventArgs e)
        {

            Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage(ii+"\n");
            //Database db = cadSer.Application.DocumentManager.MdiActiveDocument.Database;
            //HostApplicationServices.WorkingDatabase = db;
            //Document middoc = cadSer.Application.DocumentManager.MdiActiveDocument;
            //Editor ed = currenD;

            Database db = workDB;
            //Document middoc = workDoc;

                LayoutManager lb = LayoutManager.Current;
                List<Layout> zLayoutList = new List<Layout>();
                using(Transaction trs = db.TransactionManager.StartTransaction())
                {
                    ObjectId paperId = SymbolUtilityServices.GetBlockPaperSpaceId(db);
                    BlockTable bt = db.BlockTableId.GetObject(OpenMode.ForRead) as BlockTable;

                    foreach(ObjectId tempObj in bt)
                    {
                        BlockTableRecord btr = tempObj.GetObject(OpenMode.ForRead) as BlockTableRecord;
                        if(btr.IsLayout)
                        {
                            Layout la = trs.GetObject(btr.LayoutId,OpenMode.ForRead) as Layout;
                            if(la.LayoutName.Contains("平面图"))
                            {
                                zLayoutList.Add(la);
                            }

                        }

                    }

                    trs.Commit();
                }

                zLayoutList.Sort((Layout x,Layout y) =>
                {
                    if(x.LayoutName.CompareTo(y.LayoutName)<0)
                    {
                        return -1;
                    }
                    else if(x.LayoutName.CompareTo(y.LayoutName)>0)
                    {
                        return 1;
                    }
                    return 0;

                });

                foreach(Layout _tmpL in zLayoutList)
                {
                    ed.WriteMessage("开始打印布局页："+_tmpL.LayoutName+"\n");
                }
                ed.WriteMessage("============================================"+"\n");

                short bgPlot = (short)Application.GetSystemVariable("BACKGROUNDPLOT");
                Application.SetSystemVariable("BACKGROUNDPLOT",0);
                //var dloc = middoc.LockDocument();

                using(Transaction trs = db.TransactionManager.StartTransaction())
                {

                    foreach(Layout ly in zLayoutList)
                    {
                    LayoutPlot(trs, ly, @"d:\123_" + ly.LayoutName,db);
                    //ed.WriteMessage("aaaaaaaaaaaaaaaaaaaaaa+\n");
                }
                    //ISContinuePrint=false;

                }

                //dloc.Dispose();
                Application.SetSystemVariable("BACKGROUNDPLOT",bgPlot);

            //cadSer.Application.DocumentManager.DocumentActivated-=zjy_event;

        }

        private void LayoutPlot(Transaction tr,Layout lo,string filename, Database workDB)
        {
            //Database db = cadSer.Application.DocumentManager.MdiActiveDocument.Database;
            //var doc = cadSer.Application.DocumentManager.MdiActiveDocument;
            //var doc =cadSer.Application.DocumentManager.MdiActiveDocument;
            Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;

            //PlotSettings za = default(PlotSettings);


            //Editor ed = currenDoc.Editor;

            Database db = workDB;
           //Document middoc = db.Filename;

            using (Transaction acTrans = db.TransactionManager.StartTransaction())
            {
                DBDictionary plSettings = acTrans.GetObject(db.PlotSettingsDictionaryId,
                                                            OpenMode.ForRead) as DBDictionary;

                //doc.Editor.WriteMessage("\nPage Setups: ");

                // List each named page setup
                foreach(DBDictionaryEntry item in plSettings)
                {
                   ed.WriteMessage("\n  "+item.Key);
                }

                // Abort the changes to the database
                acTrans.Abort();
            }

            //LayoutManager zLayoutManger =lo.;// LayoutManager.Current;
            //zLayoutManger.CurrentLayout=lo.LayoutName;
            //LayoutManager.Create,true);
            LayoutManager.Current.CurrentLayout=lo.LayoutName;
            //LayoutManager.Current.CurrentLayout=lo.LayoutName;
            //LayoutManager _layoutManger = new LayoutManager();
            //_layoutManger.CurrentLayout
            //LayoutManager
            //LayoutManager layoutManager = db.GetRXClass as layoutManager; //LayoutManager.Create(doc.UnmanagedObject,true) as LayoutManager;
            //LayoutManager layoutManager =LayoutManager.Create(); //LayoutManager.Current;
            //layoutManager.CurrentLayout=lo.LayoutName;
            var plotDeviceName = "pdfFactory Pro";
            //string mediaName = "A3";


            //var plotDeviceName = "DocuCentre-V 3060";


            string mediaName = "A3";
            //var styleSheetName = "monochrome.stb";


            using(var ps = new PlotSettings(lo.ModelType))
            {

                ps.CopyFrom(lo);

                PlotRotation plotRotation = PlotRotation.Degrees090;

                //PlotConfigManager.RefreshList(RefreshCode.All);
                PlotConfigManager.SetCurrentConfig(plotDeviceName);
                //PlotConfigManager.CurrentConfig.RefreshMediaNameList();


              
                var psv = PlotSettingsValidator.Current;
                var device_list = psv.GetPlotDeviceList();
                foreach(string i in device_list)
                {
                    ed.WriteMessage("==="+i+"==========\n");
                }
                var devece_size = psv.GetCanonicalMediaNameList(ps);
                foreach(string i in devece_size)
                {
                    ed.WriteMessage("==="+i+"==========\n");
                }
                //psv.SetPlotWindowArea(ps,ext);
                //psv.SetPlotType(ps,Autodesk.AutoCAD.DatabaseServices.PlotType.Window);
                psv.SetPlotType(ps,Autodesk.AutoCAD.DatabaseServices.PlotType.Extents);
                psv.SetPlotConfigurationName(ps,plotDeviceName,mediaName);
                psv.SetUseStandardScale(ps,true);
                psv.SetPlotCentered(ps,true);

                psv.SetPlotRotation(ps,plotRotation);

                var pi = new PlotInfo();
                pi.Layout=lo.Id;
                pi.OverrideSettings=ps;

                
                //using(var piv = new PlotInfoValidator())
                //piv.MediaMatchingPolicy=MatchingPolicy.MatchEnabled;
                //piv.Validate(pi);
                //PlotSettingsValidator.Current
                var piv = new PlotInfoValidator();
                piv.MediaMatchingPolicy=MatchingPolicy.MatchEnabled;
                piv.Validate(pi);

                var ppi = new PlotPageInfo();

               

                using(PlotEngine pe = PlotFactory.CreatePublishEngine())
                {
                    pe.BeginPlot(null,null);
                    //pe.BeginDocument(pi,doc.Name,null,1,false,filename);
                    pe.BeginDocument(pi, db.Filename,null,1,false,filename);
                    pe.BeginPage(ppi,pi,true,null);
                    pe.BeginGenerateGraphics(null);
                    pe.EndGenerateGraphics(null);
                    pe.EndPage(null);
                    pe.EndDocument(null);
                    pe.EndPlot(null);
                }

            }
        }

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

    }

}

