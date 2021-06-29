using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using zjy;
using Autodesk.AutoCAD.EditorInput;
using cadSer = Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;

namespace zjy_CAD
{
    public partial class zjy_SHowWindow : UserControl
    {
        public zjy_SHowWindow()
        {
            InitializeComponent();
        }

        private void btn_KMLRead_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbPath = new FolderBrowserDialog();
            fbPath.ShowDialog();
            if (fbPath.SelectedPath == "") return;
            tbx_KMLPath.Text = fbPath.SelectedPath;
        }

        private void btn_outputCAD_Click(object sender, EventArgs e)
        {
            Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
            string MKLDirctory = tbx_KMLPath.Text.Trim();
            List<string> ContentList = new List<string>();
            string[] AllFiles = Directory.GetFiles(MKLDirctory);

            Dictionary<string, List<zPointXY>> PointsDic = new Dictionary<string, List<zPointXY>>();

            List<KeyValuePair<string, zPointXY>> pointInfo = new List<KeyValuePair<string, zPointXY>>();

            foreach (string tmp in AllFiles)
            {
                if (tmp.Contains(".kml"))
                {
                    string keyDic = Path.GetFileNameWithoutExtension(tmp);

                    string[] allLines = File.ReadAllLines(tmp);
                    int nline = 0;
                    for (int linestringIndex = 0; linestringIndex < allLines.Length; linestringIndex++)
                    {
                        string tmpstr = keyDic;
                        //默认<LineString><Point>为单独一行
                        if (allLines[linestringIndex].Contains("<LineString>"))
                        {
                            List<zPointXY> pointList = new List<zPointXY>();
                            ++nline;
                            while (true)
                            {
                                linestringIndex++;

                                //   if (linestringIndex >= allLines.Length) break;

                                if (allLines[linestringIndex].Contains("<coordinates>") && allLines[linestringIndex].Contains("</coordinates>"))
                                {
                                    string tmpstr_1 = allLines[linestringIndex].Trim();
                                    string[] abctmp = tmpstr_1.Split(new char[] { '>', '<','/' }, StringSplitOptions.RemoveEmptyEntries);
                                    string useStr = abctmp[1];
                                   
                                    string[] multiCoord = useStr.Trim().Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                                    foreach (string single in multiCoord)
                                    {
                                        string[] points = single.Trim().Split(',');
                                        zPointXY point = new zPointXY(Convert.ToDouble(points[0]), Convert.ToDouble(points[1]));
                                        pointList.Add(point);
                                    }

                                    if (keyDic.Contains("反"))
                                    {
                                        pointList.Reverse();
                                    }

                                    tmpstr = keyDic + "_" + nline.ToString();
                                    PointsDic.Add(tmpstr, pointList);

                                    break;
                                }

                                if (allLines[linestringIndex].Contains("<coordinates>"))
                                {
                                    while (true)
                                    {
                                        linestringIndex++;

                                        if (allLines[linestringIndex].Contains("</coordinates>"))
                                        {
                                            if (keyDic.Contains("反"))
                                            {
                                                pointList.Reverse();
                                            }

                                            tmpstr = keyDic + "_" + nline.ToString();
                                            PointsDic.Add(tmpstr, pointList);


                                            break;
                                        }


                                        string[] multiCoord = allLines[linestringIndex].Trim().Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                                        foreach (string single in multiCoord)
                                        {
                                            string[] points = single.Trim().Split(',');
                                            zPointXY point = new zPointXY(Convert.ToDouble(points[0]), Convert.ToDouble(points[1]));
                                            pointList.Add(point);
                                        }

                                    }



                                }

                                if (allLines[linestringIndex].Contains("</LineString>"))
                                {
                                    break;
                                }
                            }
                        }


                        int pointposition = 0;
                        if (allLines[linestringIndex].Contains("<Point>"))
                        {
                            pointposition = linestringIndex;

                            zPointXY pointXY = new zPointXY();
                            string pointName = "";
                            //获取点坐标
                            while (true)
                            {
                                linestringIndex++;
                                if (allLines[linestringIndex].Contains("<coordinates>") && allLines[linestringIndex].Contains("</coordinates>"))
                                {
                                    string tmpstr_1 = allLines[linestringIndex].Trim();
                                    string[] abctmp = tmpstr_1.Split(new char[] { '>', '<' }, StringSplitOptions.RemoveEmptyEntries);
                                    string useStr = abctmp[1];
                                    string[] points = useStr.Trim().Split(',');
                                    pointXY = new zPointXY(Convert.ToDouble(points[0]), Convert.ToDouble(points[1]));
                                    break;
                                }
                                if (allLines[linestringIndex].Contains("<coordinates>"))
                                {

                                    while (true)
                                    {
                                        linestringIndex++;

                                        if (allLines[linestringIndex].Contains("</coordinates>"))
                                        {

                                            break;
                                        }

                                        if (allLines[linestringIndex] == "") continue;


                                        string[] points = allLines[linestringIndex].Trim().Split(',');
                                        pointXY = new zPointXY(Convert.ToDouble(points[0]), Convert.ToDouble(points[1]));
                                    }
                                    break;
                                }
                                if (allLines[linestringIndex].Contains("</Point>"))
                                {
                                    break;
                                }
                            }
                            //获取点名称
                            while (true)
                            {
                                pointposition--;
                                if (allLines[pointposition].Contains("<Placemark>"))
                                {
                                    break;
                                }
                                if (allLines[pointposition].Contains("<name>") && allLines[pointposition].Contains("</name>"))
                                {
                                    string tmpstr_1 = allLines[pointposition].Trim();
                                    string[] abctmp = tmpstr_1.Split(new char[] { '>', '<' }, StringSplitOptions.RemoveEmptyEntries);
                                    pointName = abctmp[1];

                                    break;
                                }
                            }

                            pointInfo.Add(new KeyValuePair<string, zPointXY>(pointName, pointXY));
                        }
                    }

                }
            }

            ed.WriteMessage("读取完成\n");


            double centerLongitude = Convert.ToDouble(tbx_CentrelBL.Text.Trim());


     
            foreach (var tmp in PointsDic)
            {
                ed.WriteMessage("绘制polyline\n");



                zjyCAD.zPolylineCreate(zjyCAD.WGS84_BLToXY(tmp.Value, centerLongitude));


                zjyCAD.zCreatMtext(Path.GetFileName(tmp.Key), zjyCAD.WGS84_BLToXY(tmp.Value[0], centerLongitude), 50, 800, "zjy_kml_file");


            }
            foreach (var tmp in pointInfo)
            {
                zjyCAD.zCreatMtext(Path.GetFileName(tmp.Key), zjyCAD.WGS84_BLToXY(tmp.Value, centerLongitude), 50, 800, "zjy_point_label");
            }

        }

        private void btn_pianchajisuan_Click(object sender, EventArgs e)
        {
            try
            {
                double centrueL = Convert.ToDouble(tbx_CentrelBL.Text);
                string[] soureStr = tbx_SourceBL.Text.Trim().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                string[] targetStr = tbx_TargetBL.Text.Trim().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                zPointXY sourcePoint = new zPointXY(Convert.ToDouble(soureStr[0].Trim()), Convert.ToDouble(soureStr[1].Trim()));
                zPointXY targetPoint = new zPointXY(Convert.ToDouble(targetStr[0].Trim()), Convert.ToDouble(targetStr[1].Trim()));

                zPointXY tmp = zjyCAD.WGS84_BLToXY(targetPoint, centrueL)-zjyCAD.WGS84_BLToXY(sourcePoint, centrueL)  ;
                tbx_EastDelta.Text = Math.Round(tmp.x, 3).ToString();
                tbx_NorthDelta.Text = Math.Round(tmp.y, 3).ToString();
            }
            catch
            {

            }
        }

        private void btn_CADtoKML_Click(object sender, EventArgs e)
        {
            Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
            //两直线合并误差
            double error_len = Convert.ToDouble(tbx_minLineSegmentLength.Text.Trim());
          

            try
            {
                double centerLongitude = Convert.ToDouble(tbx_CentrelBL.Text.Trim());
                bool is80 = cbx_Is80.Checked;

                double estDelta = Convert.ToDouble(tbx_EastDelta.Text);
                double northDelta = Convert.ToDouble(tbx_NorthDelta.Text);


                Database db = HostApplicationServices.WorkingDatabase;
                var middoc = cadSer.Application.DocumentManager.MdiActiveDocument;

                List<LineStringKML> lineListKML = new List<LineStringKML>();
                List<TextKML> textListKML = new List<TextKML>();
             

                ObjectId[] objectIdsArr = ed.GetSelection().Value.GetObjectIds();
                ed.WriteMessage("==================================================\n");
                // ed.WriteMessage(objectIds.Count.ToString() + "\n");
                var doc_lock = middoc.LockDocument();
                DBObject dbo;

                //提取polyline line spline circel arc ellipse polyline3d mtext dbtext
                foreach (ObjectId tempObjectId in objectIdsArr)
                {
                    using (Transaction trans = db.TransactionManager.StartTransaction())
                    {
                        dbo = trans.GetObject(tempObjectId, OpenMode.ForRead);
                        #region 所有类型
                        //if (dbo.GetType().Equals(typeof(Polyline)) || dbo.GetType().Equals(typeof(Line)) || dbo.GetType().Equals(typeof(Spline)) || dbo.GetType().Equals(typeof(Circle)) || dbo.GetType().Equals(typeof(Arc)) || dbo.GetType().Equals(typeof(Ellipse)) || dbo.GetType().Equals(typeof(Polyline3d)))
                        //{
                        //    Curve dbo_bR = (Curve)dbo;

                        //    double length = dbo_bR.GetDistanceAtParameter(dbo_bR.EndParam);
                        //    LineStringKML lineStringKML = new LineStringKML();

                        //    for (double i = 0; i <length; i+=error_len)
                        //    {
                        //        Point3d tmpPoint = dbo_bR.GetPointAtDist(i);

                        //        lineStringKML.Add(new zPointXY(tmpPoint.X, tmpPoint.Y));
                        //    }


                        //       Point3d tmpPoint_1 = dbo_bR.GetPointAtParameter(dbo_bR.EndParam);

                        //        lineStringKML.Add(new zPointXY(tmpPoint_1.X, tmpPoint_1.Y));



                        //    lineListKML.Add(lineStringKML);
                        //}
                        #endregion
                        if ( dbo.GetType().Equals(typeof(Line)))
                        {
                            Curve dbo_bR = (Curve)dbo;

                         
                            LineStringKML lineStringKML = new LineStringKML();
                            Point3d tmpPoint_0 = dbo_bR.StartPoint;

                            lineStringKML.Add(new zPointXY(tmpPoint_0.X, tmpPoint_0.Y));


                            Point3d tmpPoint_1 = dbo_bR.EndPoint;

                            lineStringKML.Add(new zPointXY(tmpPoint_1.X, tmpPoint_1.Y));

                            lineListKML.Add(lineStringKML);
                        }
                     
                        if (dbo.GetType().Equals(typeof(Polyline)) )
                        {
                            Polyline dbo_bR = (Polyline)dbo;
                            LineStringKML lineStringKML = new LineStringKML();
                            // foreach(var tmp in dbo_bR.para)
                            double length = dbo_bR.GetDistanceAtParameter(dbo_bR.EndParam);
                          

                            for (int i = 0; i < dbo_bR.EndParam; i ++)
                            {
                                // Point3d tmpPoint = dbo_bR.GetPointAtParameter(i);

                                if (dbo_bR.GetSegmentType(i) == SegmentType.Line)
                                {                                 
                                    Point3d tmpPoint_0 = dbo_bR.GetPointAtParameter(i);
                                    lineStringKML.Add(new zPointXY(tmpPoint_0.X, tmpPoint_0.Y));                              
                                }
                                else if (dbo_bR.GetSegmentType(i) == SegmentType.Arc)
                                {
                                    for(double isegment=i;isegment<i+1; isegment+=0.05)
                                    {
                                        Point3d tmpPoint_0 = dbo_bR.GetPointAtParameter(isegment);
                                        lineStringKML.Add(new zPointXY(tmpPoint_0.X, tmpPoint_0.Y));
                                    }
                                }
                               
                            }

                            Point3d tmpPoint_1 = dbo_bR.GetPointAtParameter(dbo_bR.EndParam);

                            lineStringKML.Add(new zPointXY(tmpPoint_1.X, tmpPoint_1.Y));

                            if (dbo_bR.Closed)
                            {
                                Point3d tmpPoint_0 = dbo_bR.GetPointAtParameter(dbo_bR.StartParam);
                                lineStringKML.Add(new zPointXY(tmpPoint_0.X, tmpPoint_0.Y));
                            }

                       
                            lineListKML.Add(lineStringKML);
                        }

                        if ( dbo.GetType().Equals(typeof(Circle)) || dbo.GetType().Equals(typeof(Arc)) || dbo.GetType().Equals(typeof(Ellipse)) || dbo.GetType().Equals(typeof(Polyline2d)))
                        {
                            Curve dbo_bR = (Curve)dbo;

                            double length = dbo_bR.GetDistanceAtParameter(dbo_bR.EndParam);
                            LineStringKML lineStringKML = new LineStringKML();

                            for (double i = dbo_bR.StartParam; i < dbo_bR.EndParam; i += (dbo_bR.EndParam- dbo_bR.StartParam)/20.0)
                            {
                                Point3d tmpPoint = dbo_bR.GetPointAtParameter(i);

                                lineStringKML.Add(new zPointXY(tmpPoint.X, tmpPoint.Y));
                            }


                            Point3d tmpPoint_1 = dbo_bR.GetPointAtParameter(dbo_bR.EndParam);

                            lineStringKML.Add(new zPointXY(tmpPoint_1.X, tmpPoint_1.Y));

                            if (dbo_bR.Closed)
                            {
                                Point3d tmpPoint_0 = dbo_bR.GetPointAtParameter(dbo_bR.StartParam);
                                lineStringKML.Add(new zPointXY(tmpPoint_0.X, tmpPoint_0.Y));
                            }

                            lineListKML.Add(lineStringKML);
                        }


                        if (dbo.GetType().Equals(typeof(Polyline3d)))
                        {
                            Polyline3d dbo_bR = (Polyline3d)dbo;
                            LineStringKML lineStringKML = new LineStringKML();
                            if (dbo_bR.PolyType == Poly3dType.SimplePoly)
                            {

                                for (int i = 0; i < dbo_bR.EndParam; i++)
                                {
                                    Point3d tmpPoint_0 = dbo_bR.GetPointAtParameter(i);
                                    lineStringKML.Add(new zPointXY(tmpPoint_0.X, tmpPoint_0.Y));
                                }
                            }
                            else
                            {
                                double length = dbo_bR.GetDistanceAtParameter(dbo_bR.EndParam);
                                for (double i = 0; i < length; i += error_len)
                                {
                                    Point3d tmpPoint = dbo_bR.GetPointAtDist(i);

                                    lineStringKML.Add(new zPointXY(tmpPoint.X, tmpPoint.Y));
                                }


                            
                            }
                            Point3d tmpPoint_1 = dbo_bR.GetPointAtParameter(dbo_bR.EndParam);

                            lineStringKML.Add(new zPointXY(tmpPoint_1.X, tmpPoint_1.Y));

                            if (dbo_bR.Closed)
                            {
                                Point3d tmpPoint_0 = dbo_bR.GetPointAtParameter(dbo_bR.StartParam);
                                lineStringKML.Add(new zPointXY(tmpPoint_0.X, tmpPoint_0.Y));
                            }
                            lineListKML.Add(lineStringKML);
                        }


                        if ( dbo.GetType().Equals(typeof(Spline)))
                        {
                            Spline dbo_bR = (Spline)dbo;
                           

                            double length = dbo_bR.GetDistanceAtParameter(dbo_bR.EndParam);
                            LineStringKML lineStringKML = new LineStringKML();

                            for (double i = 0; i < length; i += error_len)
                            {
                                Point3d tmpPoint = dbo_bR.GetPointAtDist(i);

                                lineStringKML.Add(new zPointXY(tmpPoint.X, tmpPoint.Y));
                            }


                            Point3d tmpPoint_1 = dbo_bR.GetPointAtParameter(dbo_bR.EndParam);

                            lineStringKML.Add(new zPointXY(tmpPoint_1.X, tmpPoint_1.Y));

                            if (dbo_bR.Closed)
                            {
                                Point3d tmpPoint_0 = dbo_bR.GetPointAtParameter(dbo_bR.StartParam);
                                lineStringKML.Add(new zPointXY(tmpPoint_0.X, tmpPoint_0.Y));
                            }
                            lineListKML.Add(lineStringKML);
                        }

                        if ( dbo.GetType().Equals(typeof(DBText)))
                        {
                            DBText dbtext = (DBText)dbo;
                           // string name = dbtext.TextString;
                           // Point3d point3D = dbtext.Position;
                            TextKML tmpkml = new TextKML(dbtext.TextString,new zPointXY(dbtext.Position.X,dbtext.Position.Y));
                            textListKML.Add(tmpkml);
                        }
                        if (dbo.GetType().Equals(typeof(MText)))
                        {
                            MText dbtext = (MText)dbo;
                           // string name = dbtext.Text;
                            //Point3d point3D = dbtext.Location;
                            TextKML tmpkml = new TextKML(dbtext.Text.Replace('\r',' '), new zPointXY(dbtext.Location.X, dbtext.Location.Y));
                            textListKML.Add(tmpkml);
                        }

                        trans.Commit();
                    }

                }

                doc_lock.Dispose();

                ed.WriteMessage("===============读取完成=======================\n");

                //foreach(var tmp in  lineListKML )
                //{
                //    zjyCAD.zPolylineCreate(tmp.pointList,"zjy_assiaaaaaaaaaaaa");
                //}

                //foreach(var tmp in textListKML)
                //{
                //    zjyCAD.zCreatText(tmp.text, tmp.pointXY, 10, 0.8, "zjy_assiaaaaaaaaaaaa");
                //}

                string pmPath =tbx_SaveKMLPath.Text.Trim();
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
                    "       <color>ff000000</color>",
                    "       <width>4</width>",
                    "   </LineStyle>",               
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
                //生成kml文件
                List<string> kmlFile = new List<string>();
                kmlFile.AddRange(xmlhead);

                //生成路线信息图像


                foreach (var tmp in lineListKML)
                {
                    List<string> roadList = new List<string>()
                    {
                        "   <Placemark>",
                        "       <styleUrl>#yellowLineGreenPoly</styleUrl>",
                        "       <LineString>",
                        "       <tessellate>1</tessellate>",
                        "       <coordinates>"
                    };
                    foreach (zPointXY linePoint in tmp.pointList)
                    {
                        zPointXY xy = linePoint;
                        if(is80)
                        {
                            xy = linePoint + new zPointXY(estDelta,northDelta);
                        }
                      
                        zPointXY tmp_BL= zjyCAD.WGS84_XYToBL(xy, centerLongitude);

                        roadList.Add("      " + tmp_BL.y.ToString() + "," + tmp_BL.x.ToString() + ",0.0000");
                    }
                    roadList.Add("      </coordinates>");
                    roadList.Add("      </LineString>");
                    roadList.Add("  </Placemark>");
                    kmlFile.AddRange(roadList);
                }






                //生成标签信息
                List<string> roadBQList = new List<string>();
                foreach (var tmp in textListKML)
                {
                    zPointXY xy = tmp.pointXY;
                    if (is80)
                    {
                        xy = tmp.pointXY + new zPointXY(estDelta, northDelta);
                    }

                  
                    zPointXY tmp_BL = zjyCAD.WGS84_XYToBL(xy, centerLongitude);
                    roadBQList.Add("    <Placemark>");
                    roadBQList.Add("        <name>" +tmp.text + "</name>");
                    roadBQList.Add("        <Point>");
                    roadBQList.Add("        <coordinates>" + tmp_BL.y.ToString() + "," + tmp_BL.x.ToString() + ",0.0000" + "</coordinates>");
                    roadBQList.Add("        </Point>");
                   // roadBQList.Add("        <markerStyle>-2</markerStyle>");
                    roadBQList.Add("     </Placemark>");
                }
                kmlFile.AddRange(roadBQList);



                kmlFile.AddRange(xmlend);
                if (!Directory.Exists(pmPath))
                {
                    Directory.CreateDirectory(pmPath);
                }

                File.WriteAllLines(pmPath+"/"+tbx_KMLfileName.Text.Trim(), kmlFile.ToArray());
            }

            catch
            {
                ed.WriteMessage("出错\n");
            }

        }

        private void tbx_CanKaoBL_TextChanged(object sender, EventArgs e)
        {
            if (tbx_CanKaoBL.Text.Trim() == "")
            {
                tbx_CentrelBL.Text = "111";
                return;
            }
            double blRef = Convert.ToDouble(tbx_CanKaoBL.Text.Trim()) - 1.5;
            double blCentre = (Math.Floor(blRef / 3) + 1) * 3;
            tbx_CentrelBL.Text = blCentre.ToString();
        }

        private void btn_KMLPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbPath = new FolderBrowserDialog();
            fbPath.ShowDialog();
            if (fbPath.SelectedPath == "") return;
            tbx_SaveKMLPath.Text = fbPath.SelectedPath;
        }

    

        private void btn_googlePath_selectPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbPath = new FolderBrowserDialog();
            fbPath.ShowDialog();
            if (fbPath.SelectedPath == "") return;
            tbx_googlePath.Text = fbPath.SelectedPath;
        }

        private void btn_googlePath_import_Click(object sender, EventArgs e)
        {
            zjyCAD.Ceshi(tbx_googlePath.Text.Trim());
        }

        private void btn_GoogleRegions_Click(object sender, EventArgs e)
        {
              List<string> regionsList=  zjyCAD.GetLimitRegions(double.Parse( tbx_CentrelBL.Text.Trim()));
            rtbx_googleregion.Lines = regionsList.ToArray();
        }

        
    }
    public class LineStringKML
    {
        public List<zPointXY> pointList = new List<zPointXY>();
        public LineStringKML()
        {

        }
        public void Add(zPointXY pointxy)
        {
            pointList.Add(pointxy);
        }
    }

    public class TextKML
    {
        public string text = "";
        public zPointXY pointXY = new zPointXY();
        public TextKML()
        {

        }
        public TextKML(string _text,zPointXY _point)
        {
            text = _text;
            pointXY = _point;
        }

    }

}
