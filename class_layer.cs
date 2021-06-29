
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sysForm=System.Windows.Forms;
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
using MgdApp= Autodesk.AutoCAD.ApplicationServices.Application;
//using Autodesk.Windows;
using System.Timers;
using System.Threading;
using Autodesk.AutoCAD.ApplicationServices;


[assembly: CommandClass(typeof(zjy_cad_chajian.zjy_cad_function))]
namespace zjy_cad_chajian
{

    //为了传递变量
    public class MyEventArgs : EventArgs
    {
       private Entity ent;
       public  MyEventArgs(Entity ent)
        {
            this.Ent = ent;
        }

        public Entity Ent { get => ent; set => ent = value; }
    }

    //构建自己的类,为了给entity 存放 timer
    public  class zjyEntity
    {
        Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
        Database db = HostApplicationServices.WorkingDatabase;
        Document middoc = cadSer.Application.DocumentManager.MdiActiveDocument;

        private Dictionary<Entity, sysForm.Timer> entTimerDic = new Dictionary<Entity, sysForm.Timer>();
        private List<Entity> timerManageEntList(sysForm.Timer manageTimer)
        {
            List<Entity> entList = new List<Entity>();
            foreach (Entity tmp in entTimerDic.Keys)
            {
                if (entTimerDic[tmp] == manageTimer)
                {
                    entList.Add(tmp);
                }
            }
            return entList;
        }


        //设置委托
        private  delegate void ChangeVisualEvent<Object,EventArgs,Entity>(Object obj, EventArgs e);
        //private event ChangeVisualEvent<Object, EventArgs> change_visual_event;
        private void ChageVisuale(Object obj, EventArgs args, Entity ent)
        {
            //MyEventArgs myEventArgs = new MyEventArgs(ent);
            using (var doc_lock = cadSer.Application.DocumentManager.MdiActiveDocument.LockDocument())
            {
                using (Transaction trans = db.TransactionManager.StartTransaction())
                {
                    ent = (Entity)trans.GetObject(ent.Id, OpenMode.ForWrite);
                    ent.Visible = !ent.Visible;
                    trans.Commit();
                }
            }
            ed.UpdateScreen();
          //  ed.WriteMessage(System.DateTime.Now.Millisecond.ToString() + "\n");

            //throw new NotImplementedException();
        }
        private EventHandler ChageVisualeEH(Object obj, MyEventArgs args)
        {
            Entity ent = args.Ent;
            EventHandler aa = new EventHandler((obj1,e)=>ChageVisuale(obj1,e,ent));
            //Entity ent = args.Ent;
            //using (var doc_lock = cadSer.Application.DocumentManager.MdiActiveDocument.LockDocument())
            //{
            //    using (Transaction trans = db.TransactionManager.StartTransaction())
            //    {
            //        ent = (Entity)trans.GetObject(ent.Id, OpenMode.ForWrite);
            //        ent.Visible = !ent.Visible;
            //        trans.Commit();
            //    }
            //}
            //ed.UpdateScreen();
            //ed.WriteMessage(System.DateTime.Now.Millisecond.ToString() + "\n");

            //throw new NotImplementedException();
            return aa;
        }
        
        /// <summary>
        /// Twink是闪动;TwinkStop是停止闪动
        /// </summary>
        /// <param name="ent_list"></param>
        /// <param name="timer"></param>
        /// <param name="timer_interval"></param>
        //一组对象一个timer
        public void Twink(List<Entity> ent_list, sysForm.Timer timer, int timer_interval = 500)
        {
            timer.Interval = timer_interval;
            timer.Start();
            timer.Enabled = true;
            foreach (Entity tmp in ent_list)
            {
    
                timer.Tick += new EventHandler((obj, e) => ChageVisuale(obj, e, tmp));

                if (!entTimerDic.Keys.Contains(tmp)) {entTimerDic.Add(tmp,timer); }
                
            }
        }
        //一组中的每一个对象分别为一个timer
        public void Twink(List<Entity> ent_list, int timer_interval = 500)
        {
            foreach(Entity tmp in ent_list)
            {
                Thread.Sleep(100);
                sysForm.Timer timer = new sysForm.Timer();
                timer.Interval = timer_interval;
                timer.Start();
                timer.Enabled = true;
                MyEventArgs eventArgs = new MyEventArgs(tmp);

                EventHandler eventHandler = ChageVisualeEH(new object(), eventArgs);
                // timer.Tick += eventHandler;
                timer.Tick += new EventHandler((obj, e) => ChageVisuale(obj, e, tmp));
                // timer.Tick += (obj, e) => ChageVisuale(obj, e, tmp);
                if (!entTimerDic.Keys.Contains(tmp)) { entTimerDic.Add(tmp, timer); }
            }
        }

        //isGroup时true时就讲所选择的对象所在组的所有对象都停止闪动
        //isGroup=false为只是讲所选的对象停止闪动.
        public void TwinkStop(List<Entity> ent_list, bool isGroup)
        {
            //isgroup 为true
            if (isGroup)
            {
                List<sysForm.Timer> timer_list = new List<sysForm.Timer>();
                foreach (Entity tmpEnt in entTimerDic.Keys)
                {
                    if (ent_list.Contains(tmpEnt))
                    { timer_list.Add(entTimerDic[tmpEnt]); }

                }
                foreach (sysForm.Timer tmpT in timer_list)
                {
                    tmpT.Stop();
                    tmpT.Enabled = false;
                    tmpT.Dispose();
  Change_entity_Show(timerManageEntList(tmpT));
                    foreach (Entity ent in timerManageEntList(tmpT))
                    {
                        entTimerDic.Remove(ent);
                    }
                  
                }
            }
            else //isgroup false
            {
                List<sysForm.Timer> timer_list = new List<sysForm.Timer>();
                foreach (Entity ent in ent_list)
                {
                    sysForm.Timer timer = entTimerDic[ent];
                    if (!timer_list.Contains(timer)) { timer_list.Add(timer); }
                    //MyEventArgs eventArgs = new MyEventArgs(ent);
                    //EventHandler eventHandler = ChageVisualeEH(new Object(), eventArgs);
                    //timer.Tick += eventHandler;
                    timer.Tick += new EventHandler((obj, e) => ChageVisuale(obj, e, ent));
                    // timer.Tick -= (obj, e) => ChageVisuale(obj, e, ent);
                    ed.WriteMessage("isGroup==false\n");
                    Change_entity_Show(new List<Entity>() { ent });
                }
                foreach (sysForm.Timer timer in timer_list)
                {
                    if (!entTimerDic.Values.Contains(timer)) {
                        timer.Stop();
                        timer.Enabled = false;
                        timer.Dispose();
                        Change_entity_Show(timerManageEntList(timer));
                    }
                }
            }
        }

        //为了让停止闪动时任然可见
        internal void Change_entity_Show(List<Entity> ent_list)
        {
            foreach (Entity ent in ent_list)
            {
                using (var @lock = middoc.LockDocument())
                {
                    using (var trans = middoc.TransactionManager.StartTransaction())
                    {
                        Entity entt = (Entity)trans.GetObject(ent.Id, OpenMode.ForWrite);
                        entt.Visible = true;
                        trans.Commit();
                    }
                }
            }
        }
        //给闪动组添加对象
        public void Add(List<Entity> Slist,List<Entity>Tlist)
        {
            List<sysForm.Timer> timer_M_list = new List<sysForm.Timer>();
            foreach (Entity tmpT in Tlist)
            {
                if (!timer_M_list.Contains(entTimerDic[tmpT]))
                {
                    timer_M_list.Add(entTimerDic[tmpT]);
                }
            }
            if (timer_M_list.Count > 1) { ed.WriteMessage("目标闪烁组超过两个,重新选择\n"); }
            else if (timer_M_list.Count < 1) { ed.WriteMessage("未选择闪烁组,重新选择\n"); }
            else
            {
                sysForm.Timer timer2 = timer_M_list[0];
                List<Entity> time_ent_list = timerManageEntList(timer2);

                timer2.Stop();

                foreach (Entity ent_2 in Slist)
                {
                    if (!entTimerDic.Keys.Contains(ent_2))
                    {
                        entTimerDic.Add(ent_2,timer2);
                        MyEventArgs eventArgs = new MyEventArgs(ent_2);
                        EventHandler eventHandler = ChageVisualeEH(new Object(), eventArgs);
                        timer2.Tick += eventHandler;
                        // timer2.Tick += (obj, e) => ChageVisuale(obj, e, ent_2);
                    }
                }

                timer2.Start();

                

            }
        }

    }
  

    public class zjy_cad_function
    {
        public List<Entity> dboArr_To_EntList(DBObject[] dboArr)
        {
            List<Entity> ent_list = new List<Entity>();
            foreach (DBObject tmp in dboArr)
            {
                ent_list.Add((Entity)tmp);
            }
            return ent_list;
        }

        #region 闪烁测试

        //[CommandMethod("csa")]
        //public void csa()
        //{
        //    //  Thread.CurrentThread.IsBackground = true;
        //    Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
        //    Database db = HostApplicationServices.WorkingDatabase;
        //    var middoc = cadSer.Application.DocumentManager.MdiActiveDocument;
        //  List<Entity> entList_select=dboArr_To_EntList( GetObject_Array());

        //    //一组中的每一个对象分别为一个timer
        //    zjyEntity.Twink(entList_select);
        //    // zjyEntity.Twink(timer);
        //}

        //[CommandMethod("csb")]
        //public void csb()
        //{
        //    //  Thread.CurrentThread.IsBackground = true;
        //    Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
        //    Database db = HostApplicationServices.WorkingDatabase;
        //    var middoc = cadSer.Application.DocumentManager.MdiActiveDocument;

        //    List<Entity> entList_select = dboArr_To_EntList(GetObject_Array());

        //    //一组为一个timer
        //    zjyEntity.Twink(entList_select,new sysForm.Timer());
        //}
        //[CommandMethod("csc")]
        //public void csc()
        //{
        //    //  Thread.CurrentThread.IsBackground = true;
        //    Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
        //    Database db = HostApplicationServices.WorkingDatabase;
        //    var middoc = cadSer.Application.DocumentManager.MdiActiveDocument;
        //    List<Entity> entList_select = dboArr_To_EntList(GetObject_Array());


          
        //    // isGroup时true时就讲所选择的对象所在组的所有对象都停止闪动
        ////isGroup=false为只是讲所选的对象停止闪动.
        //    zjyEntity.TwinkStop(entList_select, true);
        //}

        //[CommandMethod("csd")]
        //public void csd()
        //{
        //    //  Thread.CurrentThread.IsBackground = true;
        //    Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
        //    Database db = HostApplicationServices.WorkingDatabase;
        //    var middoc = cadSer.Application.DocumentManager.MdiActiveDocument;
        //    List<Entity> entList_select = dboArr_To_EntList(GetObject_Array());
        //    // isGroup时true时就讲所选择的对象所在组的所有对象都停止闪动
        //    //isGroup=false为只是讲所选的对象停止闪动.
        //    zjyEntity.TwinkStop(entList_select, false);
        //}
        //[CommandMethod("cse")]
        //public void cse()
        //{
        //    //  Thread.CurrentThread.IsBackground = true;
        //    Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
        //    Database db = HostApplicationServices.WorkingDatabase;
        //    var middoc = cadSer.Application.DocumentManager.MdiActiveDocument;
        //    List<Entity> entList_select_obj = dboArr_To_EntList(GetObject_Array());
        //    List<Entity> entList_select_tar = dboArr_To_EntList(GetObject_Array());
        //    zjyEntity.Add(entList_select_obj, entList_select_tar);
        //}


        //[CommandMethod("za")]
        //public void za()
        //{
        //    //  Thread.CurrentThread.IsBackground = true;
        //    Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
        //    Database db = HostApplicationServices.WorkingDatabase;
        //    var middoc = cadSer.Application.DocumentManager.MdiActiveDocument;
        //    PromptEntityResult select_set = ed.GetEntity("选择一个对象");
        //    ObjectId ent_id = select_set.ObjectId;
        //    Entity ent = (Entity)id_To_Object(ent_id);
        //   // zjyEntity.Twink(ent);
        //   // zjyEntity.Twink(timer);
        //}

        //zjyEntity zjyEntity = new zjyEntity();

        //[CommandMethod("zaa")]
        //public void zaa()
        //{
        //    //  Thread.CurrentThread.IsBackground = true;
        //    Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
        //    Database db = HostApplicationServices.WorkingDatabase;
        //    var middoc = cadSer.Application.DocumentManager.MdiActiveDocument;
        //    PromptEntityResult select_set = ed.GetEntity("选择一个对象");
        //    ObjectId ent_id = select_set.ObjectId;
        //    Entity ent = (Entity)id_To_Object(ent_id);
        //    //    zjyEntity.Twink(ent,new sysForm.Timer());
        //    //  sysForm.Timer timer = new sysForm.Timer();

        //   // zjyEntity.Stop_Twink(new List<Entity>() { ent }); 
        //}






        //[CommandMethod("aaaaa")]
        //public void zaaaa()
        //{
        //  //  Thread.CurrentThread.IsBackground = true;
        //    Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
        //    Database db = HostApplicationServices.WorkingDatabase;
        //    var middoc = cadSer.Application.DocumentManager.MdiActiveDocument;

        //    PromptEntityResult select_set = ed.GetEntity("选择一个对象");
        //    ObjectId ent_id = select_set.ObjectId;


        //    Entity ent = (Entity)id_To_Object(ent_id);
        //    //ent.Twink();


        //    PromptEntityResult select_set_1 = ed.GetEntity("选择一个对象");
        //    ObjectId ent_id_1 = select_set.ObjectId;
        //    Entity ent_1 = (Entity)id_To_Object(ent_id);

        //    System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
       
        //}









        //bool ztt = true;
        //[CommandMethod("ssss")]
        //public void ShanShuo()
        //{
        //    Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
        //    Database db = HostApplicationServices.WorkingDatabase;
          
        //    PromptEntityResult select_set = ed.GetEntity("选择一个对象");
        //    ObjectId ent_id = select_set.ObjectId;

        //    int i = 1;
        //    while (ztt)
        //    {
        //        ed.WriteMessage($"\n{i}\n");
        //        duixiangxianshi(ent_id);
        //        //Thread.Sleep(1000);
        //        ed.WriteMessage(System.DateTime.Now.Millisecond.ToString() + "\n");
        //        //Thread.Sleep(500);
        //        Thread.CurrentThread.Join(100);
        //        // Thread.m
        //        // Thread.CurrentThread.Suspend();
        //        if (i == 200)
        //        {
        //            ztt = false;
        //        }
        //        i++;

        //    }
        //    ztt = true;

        //}



        private void duixiangxianshi(ObjectId objectId)
        {
            Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
            Database db = HostApplicationServices.WorkingDatabase;
            Entity ent;
            using (var doc_lock = cadSer.Application.DocumentManager.MdiActiveDocument.LockDocument())
            {

                using (Transaction trans = db.TransactionManager.StartTransaction())
                {
                    ent = (Entity)id_To_Object(objectId, trans);

                    ent.Visible = !ent.Visible;


                    trans.Commit();
                }

            }
            ent.Draw();
            ed.UpdateScreen();

        }


        Mutex mutex = new Mutex();
        // ObjectId objectId=User_Con
        public void duixiangxianshi_1(ObjectId objectId)
        {
            //mutex.WaitOne();
            Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;

            Database db = HostApplicationServices.WorkingDatabase;

            Entity ent;

            using (var doc_lock = cadSer.Application.DocumentManager.MdiActiveDocument.LockDocument())
            {
                //   MessageBox.Show("+++s+++++++++++++++++\n");

                using (Transaction trans = db.TransactionManager.StartTransaction())

                {
                    //lock (trans)
                    //{
                        ent = (Entity)id_To_Object(objectId, trans);

                        ent.Visible = !ent.Visible;


                        trans.Commit();
                    //}
                }
            }

            ed.UpdateScreen();
            // ed = null;
            //  db.Dispose();

            //  mutex.ReleaseMutex();



        }




        #endregion;

        [CommandMethod("Pasj")]
        public void Pallete()
        {
            Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
            UserControl userControl = new User_Control_Sheji();
            PaletteSet ps = new cadWin.PaletteSet("设计")
            {
                Visible = true,
                Style = cadWin.PaletteSetStyles.ShowCloseButton,
                Dock = cadWin.DockSides.None,
                // ps.AutoRollUp = true;
                // ps.Name = "zjy";
                // ps.TitleBarLocation = cadWin.PaletteSetTitleBarLocation.Right;
                Size = new System.Drawing.Size(144, 1000),
                // ps.Anchored=
                // ps.AutoRollUp = cadWin.DropTarget;


                //ps.Dock = cadWin.DockSides.None;
                MinimumSize = new System.Drawing.Size(140, 1000)
            };

            // ps.Size = new System.Drawing.Size(144, 1000);
            ps.Add("设计", userControl);
            ed.WriteMessage(Thread.CurrentThread.ManagedThreadId.ToString() + ":Pasj函数线程\n");
            //  ps.Dispose();
        }
        //Action a;



      
        private static PaletteSet ps = null;

        [CommandMethod("Pacneg")]
        public void Pallete_Fu()
        {
            if (ps != null)
            {

                ps.Close(); ps.Dispose();
            }
         
            UserControl userControl = new User_Control_fuzhu();


         
            ps = new cadWin.PaletteSet("设计辅助")
            {

                Visible = true,
                Style = cadWin.PaletteSetStyles.ShowCloseButton | cadWin.PaletteSetStyles.ShowTabForSingle,
                // Location= cadWin.PaletteSetTitleBarLocation.Left;
                Dock = cadWin.DockSides.None,
                // Dock = cadWin.DockSides.Left,
                // ps.AutoRollUp = true;
                // ps.Name = "zjy";
                // ps.TitleBarLocation = cadWin.PaletteSetTitleBarLocation.Right;
                Size = new System.Drawing.Size(500, 1000),
                // ps.Anchored=
                // ps.AutoRollUp = cadWin.DropTarget;


                //ps.Dock = cadWin.DockSides.None;
                MinimumSize = new System.Drawing.Size(500, 1000)
            };


            // ps.Size = new System.Drawing.Size(144, 1000);
            // ps.Add("辅助功能", userControl);


         //   ps.Add("1", userControl1);
            ps.Add("辅助功能333", userControl);
            //MessageBox.Show("55555555");
            //ps.Close();
            //a = ps.Close;
            //ps_list.Add(ps);
            //   a = ps.Close;
            // string[] string_a = cadWin.PaletteSet;

          //  Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
         //   ed.WriteMessage(Thread.CurrentThread.ManagedThreadId.ToString() + "面板线程\n");
        }


        public void random_ceshi()
        {
            // Random rdm = new Random();
            for (int i = 0; i < 20001; i++)
            {
                Random rdm = new Random(Guid.NewGuid().GetHashCode());

                //rdm.Next(1, 255);
                //  double radius = rdm.Next(0, i)*0.1;
                //  double r =0.0001; //Math.Pow((Math.E), (i * 0.1)));

                //double angel = i;
                // double radius =r*0.01;// rdm.Next(0, i) * 0.1;
                // double x = r * Math.Cos(i * 180 / 3.14);// + rdm.Next(-i, i )*0.1;
                // double y = r * Math.Sin(i * 180 / 3.14);// + rdm.Next(-i, i) * 0.1;
                // double r = 0 + 10 ^ (i); //Math.Pow((Math.E), (i * 0.1)));
                //此处的5可以调整螺旋线的根数
                double angel = 360 * (i * 0.001) * 5;
                double r = Math.Exp(0.01 * angel);
                r = r + r * rdm.Next(-10, 10) * 0.01;
                double radius = r * (1 + rdm.Next(-10, 10) * 0.05) * 0.02;// + rdm.Next(-i, i)* r*0.0001;
                double x = r * Math.Cos(i * 180 / 3.14) + r * rdm.Next(-10, 10) * 0.001;// + rdm.Next(-i, i )*0.1;
                double y = r * Math.Sin(i * 180 / 3.14) + r * rdm.Next(-10, 10) * 0.001;


                double z = 0;
                short color_index = (short)rdm.Next(1, 255);
                Circle cir = cir_zjy(radius, x, y, z);
                ToModelSpace(cir, color_index);

                // cir.Visible=true;
                // cir.Draw();
                // ToModelSpace();
            }

        }

        public void ToModelSpace(Entity ent, short color_index)
        {
            using (var doc_lock = cadSer.Application.DocumentManager.MdiActiveDocument.LockDocument())
            {
                Database db = HostApplicationServices.WorkingDatabase;
                ObjectId entId;
                using (Transaction trans = db.TransactionManager.StartTransaction())
                {
                    BlockTable bt = (BlockTable)trans.GetObject(db.BlockTableId, OpenMode.ForRead);
                    BlockTableRecord btr = (BlockTableRecord)trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);
                    ent.ColorIndex = color_index;
                    entId = btr.AppendEntity(ent);
                    trans.AddNewlyCreatedDBObject(ent, true);

                    trans.Commit();
                    //ent.
                }
            }
        }

        public static Circle cir_zjy(double radius, double x, double y, double z)
        {
            //public Circle(Point3d center, Vector3d normal, double radius);
            Point3d center = new Point3d(x, y, z);
            Vector3d v = new Vector3d(0, 0, 1);
            return new Circle(center, v, radius);

        }

        //------------------------命令输入函数-----------------------------------------------
        //------------------------命令输入函数-----------------------------------------------  
        #region   
        //采用invoke acedCmd方法输入命令
        [System.Security.SuppressUnmanagedCodeSecurity]
        [DllImport("acad.exe", EntryPoint = "acedCmd", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        extern static private int acedCmd(IntPtr resbuf);

        [CommandMethod("ceshiaaa")]
        public void cad_command(string cadCommand)
        {

            ResultBuffer args = new ResultBuffer(new TypedValue((int)LispDataType.Text, cadCommand));
            acedCmd(args.UnmanagedObject);
            args.Dispose();
        }

        ////采用doucument  sendstringto Execute方法输入命令;
        //调用win系统命令，设定cad界面为当前焦点
        [DllImport("user32.dll", EntryPoint = "SetFocus")]
        public static extern int SetFocus(IntPtr hwnd);

        public void SetFocus()
        {
            SetFocus(Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Window.Handle);
        }

        public void cad_command_document(string cadCommand)
        {

            var doucument = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            doucument.SendStringToExecute(cadCommand, false, false, true);
            // SetFocus(Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Window.Handle);

        }

        //需要引用cad com库 type library，在什么cad版本运行插件就需要引用该版本的type library库

        //com方法输入cad命令
        //public void cad_command_com(string cadCommand)
        //{

        //    AcadApplication app = cad_app.Application.AcadApplication as AcadApplication;

        //    app.ActiveDocument.SendCommand(cadCommand);
        //}
        #endregion
        //------------------------命令输入函数-----------------------------------------------
        //------------------------命令输入函数----------------------------------------------- 


        //获取模型空间的所有物体，返回选择集Collection,是物体对象object，不是objectid




        //-------------------------获取模型空间的所有物体------------------------------------
        //-------------------------获取模型空间的所有物体------------------------------------
        //获取模型空间的所有物体，返回选择集Collection,是物体对象object，不是objectid
        public DBObjectCollection Get_All_Object_WorkingSpace()
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
        /// <summary>
        /// //////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        /// <returns></returns>
        /// 
        public DBObjectCollection GetAllObjects()
            {
            DBObjectCollection dBObjectCollection = new DBObjectCollection();
            DBObjectCollection modelObjects = Get_All_Object_WorkingSpace();
            DBObjectCollection paperObjects = Get_All_Object_LayoutSpace();
            foreach (DBObject tmp in modelObjects)
            {
                dBObjectCollection.Add(tmp);
                    }
            foreach (DBObject tmp in paperObjects)
            {
                dBObjectCollection.Add(tmp);
            }
            return dBObjectCollection;
        }
        public DBObjectCollection Get_All_Object_LayoutSpace()
        {
            Database db = HostApplicationServices.WorkingDatabase; //;)// ;

            //  {
            DBObjectCollection dbCollection = new DBObjectCollection();///)
            //{
            var middoc = cadSer.Application.DocumentManager.MdiActiveDocument;
            var doc_lock = cadSer.Application.DocumentManager.MdiActiveDocument.LockDocument();



            Editor ed = middoc.Editor; //模型空间

            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                BlockTable blocktable_1 = trans.GetObject(db.BlockTableId, OpenMode.ForWrite) as BlockTable;
                foreach (var btrr_1 in blocktable_1)
                {
                    BlockTableRecord btr = (BlockTableRecord)trans.GetObject(btrr_1, OpenMode.ForWrite);
                    if (btr.IsLayout)
                    {

                   //   DBObject dBObject=  trans.GetObject(btr.ObjectId, OpenMode.ForWrite);

                        BlockTableRecord dBObject = (BlockTableRecord)trans.GetObject(btr.ObjectId, OpenMode.ForWrite);

                        foreach(ObjectId tmp in dBObject)
                        {
                            dbCollection.Add(id_To_Object(tmp));
                        }


                    }
                }

                trans.Commit();


                doc_lock.Dispose();

                return dbCollection;



            }
        }
        //-------------------------获取模型空间的所有物体------------------------------------
        //-------------------------获取模型空间的所有物体------------------------------------



        //-------------------------获取模型空间的层与颜色筛选物体----------------------------
        //-------------------------获取模型空间的层与颜色筛选物体----------------------------

        #region
        ////获得在模型空间所有在层LayerName中的对象， 是dbobject   collection，不是objectID
        public DBObjectCollection GetObjects(string LayerName)
        {
            //using (DBObjectCollection dBObjectCollection = Get_All_Object_WorkingSpace())
            //{

            DBObjectCollection dBObjectCollection = Get_All_Object_WorkingSpace();
            return GetObjects(LayerName, dBObjectCollection);
            //}
        }

        //获得在某一选择集中是在层LayerName中的对象 是dbobject   collection，不是objectID
        public DBObjectCollection GetObjects(string LayerName, DBObjectCollection dbCollection_AllObjects)
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


        ////获得在模型空间所有wei颜色Color_Index的物体， 是dbobject   collection，不是objectID
        public DBObjectCollection GetObjects(int Color_Index)
        {
            return GetObjects(Color_Index, Get_All_Object_WorkingSpace());
        }

        ////获得在某一选择集中所有为颜色Color_Index的物体， 是dbobject   collection，不是objectID
        public DBObjectCollection GetObjects(int Color_Index, DBObjectCollection dbCollection_AllObjects)
        {

            DBObjectCollection dbCollection = new DBObjectCollection();//) { 

            // DBObjectCollection dbCollection_AllObjects = Get_All_Object_WorkingSpace();
            Color ent_Color = Color.FromColorIndex(ColorMethod.ByAci, (Int16)Color_Index);

            foreach (DBObject dbo in dbCollection_AllObjects)
            {
                //DBObject dbo = id_To_Object(objid);
                // Entity dbo_to_Entiy = (Entity)dbo;
                color_RGB color_rgb_a = new color_RGB(ent_Color);
                color_RGB color_rgb_b = new color_RGB(dbo);

                if (color_rgb_b.CompareTo(color_rgb_a) == 1)
                {
                    if (dbCollection.Contains(dbo) == false)
                    {
                        dbCollection.Add(dbo);
                    }
                }



            }
            ent_Color.Dispose();
            // ent_Color.Dispose();

            return dbCollection;
            // }
        }



        ////获得在某一选择集中所有为颜色Color_Index的物体且属于层LayerName的对象， 是dbobject   collection，不是objectID
        public DBObjectCollection GetObjects(string LayerName, int Color_Index, DBObjectCollection dbCollection_AllObjects)
        {
            DBObjectCollection dbCollection = new DBObjectCollection();//)

            // {  // DBObjectCollection dbCollection_AllObjects = Get_All_Object_WorkingSpace();

            foreach (DBObject dbo in dbCollection_AllObjects)
            {
                // DBObject dbo = id_To_Object(objid);
                Entity dbo_to_Entiy = (Entity)dbo;
                Color ent_Color = Color.FromColorIndex(ColorMethod.ByAci, (Int16)Color_Index);
                color_RGB color_rgb_a = new color_RGB(ent_Color);
                color_RGB color_rgb_b = new color_RGB(dbo);
                if (color_rgb_b.CompareTo(color_rgb_a) == 1 && dbo_to_Entiy.Layer == LayerName)
                {
                    if (!dbCollection.Contains(dbo))
                    {
                        dbCollection.Add(dbo);
                    }
                }
                // dbo.Dispose();
                //   dbo_to_Entiy.Dispose();
                //  ent_Color.Dispose();

            }

            return dbCollection;
            // }/
        }

        ////获得模型空间中所有为颜色Color_Index的物体且属于层LayerName的对象， 是dbobject   collection，不是objectID
        public DBObjectCollection GetObjects(string LayerName, int Color_Index)
        {
            // DBObjectCollection dbCollection = new DBObjectCollection();

            // DBObjectCollection dbCollection_AllObjects = Get_All_Object_WorkingSpace();


            return GetObjects(LayerName, Color_Index, Get_All_Object_WorkingSpace());
        }

        ////获得模型空间中所有与某一物体的颜色Color_Index和层LayerName相同的对象， 是dbobject   collection，不是objectID
        ///leixing=0,按层layername过滤
        ///leixing=1,按颜色colorIndex过滤
        ///leixing=2,按层与颜色过滤
        public DBObjectCollection GetObjects(DBObject dbo, Int16 leixing)
        {
            DBObjectCollection dbCollection = new DBObjectCollection();
            // DBObjectCollection dbCollection_AllObjects = Get_All_Object_WorkingSpace();


            Entity ent = (Entity)dbo;
            Int16 i = leixing;
            switch (i)
            {
                case 0:
                    dbCollection = GetObjects(ent.Layer, Get_All_Object_WorkingSpace());
                    break;
                case 1:
                    dbCollection = GetObjects((short)ent.ColorIndex, Get_All_Object_WorkingSpace());
                    break;
                case 2:
                    dbCollection = GetObjects(ent.Layer, (short)ent.ColorIndex, Get_All_Object_WorkingSpace());
                    break;
                default:
                    break;


            }

            //Get_All_Object_WorkingSpace().Dispose();
            //ent.Dispose();

            return dbCollection;
            //dbCollection.Dispose();
            //if(leixing==0){return GetObjects(ent.Layer, (short)ent.ColorIndex, Get_All_Object_WorkingSpace());}

        }

        ////获得模型空间中所有与某一物体的颜色Color_Index和层LayerName相同的对象， 是dbobject   collection，不是objectID，
        ///该物体是通过选择得到
        ///

        //获取与对象组[] leixing匹配的对象collection
        public DBObjectCollection GetObjects(DBObject[] dbo_array, Int16 leixing)
        {
            DBObjectCollection dbCollection = new DBObjectCollection();

            //  DBObjectCollection dbCollection_AllObjects = Get_All_Object_WorkingSpace();
            //var result = ed.GetEntity("选择一个实体");
            foreach (DBObject dbo in dbo_array)
            {
                // Entity ent = (Entity)id_To_Object(dbo);

                DBObjectCollection dbo_coll_1 = GetObjects(dbo, leixing);

                foreach (DBObject dboo in dbo_coll_1)
                {
                    //if (dbCollection.Contains(dboo)==false)
                    //{ 
                    dbCollection.Add(dboo);
                    //  }

                }

                // dbCollection.Add(dbo);
                //dbo.Dispose();
            }
            //dbCollection_AllObjects.Dispose();
            // Entity ent = (Entity)id_To_Object(result.ObjectId);
            //return GetObjects(ent.Layer, ent.ColorIndex, Get_All_Object_WorkingSpace());
            return dbCollection;
        }

        //获取选择级selectset中的 leixing匹配的对象collection
        public DBObjectCollection GetObjects(Int16 leixing)
        {
            //DBObjectCollection dbCollection = new DBObjectCollection();

            // Entity ent = (Entity)id_To_Object(result.ObjectId);
            //return GetObjects(ent.Layer, ent.ColorIndex, Get_All_Object_WorkingSpace());
            return GetObjects(GetObject_Array(), leixing);
        }

        public DBObjectCollection GetObjects(List<string> layer_list)
        {
            DBObjectCollection dbo_co = new DBObjectCollection();//)
                                                                 //  { 

            foreach (string layer_name in layer_list)
            {
                //  DBObjectCollection dbo_co
                foreach (DBObject dbo in GetObjects(layer_name))
                {
                    if (!dbo_co.Contains(dbo)) dbo_co.Add(dbo);


                }
            }
            return dbo_co;
            //  }
        }

        public DBObjectCollection GetObjects(List<DBObject> dbo_list, Int16 leixing)
        {
            DBObjectCollection dbo_co = new DBObjectCollection();//)

            // {
            foreach (DBObject dbo in dbo_list)
            {
                DBObjectCollection dbo_col_temp = GetObjects(dbo, 2);
                //  DBObjectCollection dbo_co
                foreach (DBObject dbo_t in dbo_col_temp)
                {
                    if (!dbo_co.Contains(dbo)) dbo_co.Add(dbo);
                }
            }



            return dbo_co;
            //}
        }


        ////获得模型空间中所有与某一物体的颜色Color_Index和层LayerName相同的对象， 是dbobject   collection，不是objectID，
        ///该物体是通过选择得到
        ///leixing=0,按层layername过滤
        ///leixing=1,按颜色colorIndex过滤
        ///leixing=2,按层与颜色过滤

        //public DBObjectCollection GetObjects(Int16 leixing)
        //{
        //    // DBObjectCollection dbCollection = new DBObjectCollection();

        //    // DBObjectCollection dbCollection_AllObjects = Get_All_Object_WorkingSpace();
        //    var result = ed.GetEntity("选择一个实体");

        //    //Entity ent =id_To_Object(result.ObjectId);
        //    return GetObjects(id_To_Object(result.ObjectId), leixing);
        //}

        #endregion
        //-------------------------获取模型空间的层与颜色筛选物体----------------------------
        //-------------------------获取模型空间的层与颜色筛选物体----------------------------

        /// <summary>
        /// ///颜色索引值0 为 bylock;索引值为256为bylayer
        /// </summary>
        /// <returns></returns>

        //获得所有图层名
        public List<string> All_LayerName()
        {
            Database db = HostApplicationServices.WorkingDatabase;
            Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;

            List<string> layerName_list = new List<string>();
            var doc_lock = cadSer.Application.DocumentManager.MdiActiveDocument.LockDocument();
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                // 

                LayerTable lb = (LayerTable)trans.GetObject(db.LayerTableId, OpenMode.ForWrite);
                //int i = 1;
                // ed.WriteMessage("\n");
                foreach (ObjectId ltr in lb)
                {
                    LayerTableRecord ltr1 = (LayerTableRecord)trans.GetObject(ltr, OpenMode.ForRead);
                    //ltr1.Color = Color.FromColorIndex(ColorMethod.ByColor, ColorIndex);
                    //ed.WriteMessage(ltr1.Name+"第"+i++.ToString()+"\n");
                    //ed.WriteMessage(i++.ToString()+"\n");
                    //if (ltr1.Name == "")
                    //{
                    //    ltr1.Color= Color.FromColorIndex(ColorMethod.ByColor, 7);
                    //    ed.WriteMessage("空图层\n");
                    //}
                    //  MessageBox.Show(ltr1.Name);
                    layerName_list.Add(ltr1.Name);
                    ed.WriteMessage(ltr1.Name + "\n");
                    //ltr1.Dispose();

                }
                //ed.WriteMessage(i.ToString() + "完成\n");


                trans.Commit();
                // lb.Dispose();
                //
            }
            doc_lock.Dispose();
            return layerName_list;

        }

        //修改List<string> layername的颜色
        public void Change_Layer_Color(List<string> layer_list, short Index_color)
        {
            Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
            Database db = HostApplicationServices.WorkingDatabase;
            using (var doc_lock = cadSer.Application.DocumentManager.MdiActiveDocument.LockDocument())
            {
                using (Transaction trans = db.TransactionManager.StartTransaction())
                {
                    LayerTable lb = (LayerTable)trans.GetObject(db.LayerTableId, OpenMode.ForWrite);
                    //LayerTableRecord ltr1 = (LayerTableRecord)trans.GetObject(lb.ObjectId, OpenMode.ForWrite);

                    foreach (ObjectId objid in lb)
                    {

                        LayerTableRecord ltr1 = (LayerTableRecord)id_To_Object(objid, trans);
                        // ed.WriteMessage(ltr1.Name + ":" + ltr1.Color + "\n");
                        if (layer_list.Contains(ltr1.Name))
                        {
                            ltr1.Color = Color.FromColorIndex(ColorMethod.ByAci, Index_color);
                            ed.WriteMessage(ltr1.Name + ":" + ltr1.Color + "\n");
                        }


                    }

                    trans.Commit();
                    // lb.Dispose();
                }
            }
            db.Dispose();
            ed = null;
        }

        //修改 层layername的颜色
        public void Change_Layer_Color(string layer_name, short Index_color)
        {
            using (var doc_lock = cadSer.Application.DocumentManager.MdiActiveDocument.LockDocument())
            {
                Database db = HostApplicationServices.WorkingDatabase;
                using (Transaction trans = db.TransactionManager.StartTransaction())
                {
                    LayerTable lb = (LayerTable)trans.GetObject(db.LayerTableId, OpenMode.ForWrite);
                    //LayerTableRecord ltr1 = (LayerTableRecord)trans.GetObject(lb.ObjectId, OpenMode.ForWrite);

                    foreach (ObjectId objid in lb)
                    {

                        LayerTableRecord ltr1 = (LayerTableRecord)id_To_Object(objid, trans);
                        // ed.WriteMessage(ltr1.Name + ":" + ltr1.Color + "\n");
                        if (layer_name == ltr1.Name)
                        {
                            ltr1.Color = Color.FromColorIndex(ColorMethod.ByAci, Index_color);
                            // ed.WriteMessage(ltr1.Name + ":" + ltr1.Color + "\n");
                        }
                        // ltr1.Dispose();

                    }

                    trans.Commit();


                    //  lb.Dispose();
                }
                db.Dispose();
            }
        }
        //修改所选的图层颜色
        public void Change_Layer_Color(short Index_color)
        {
            Change_Layer_Color(GetObject_Array(), Index_color);
        }
        //改变实体所在层的颜色
        public void Change_Layer_Color(Entity ent, short Index_color)
        {
            Change_Layer_Color(Layer_Name(ent), Index_color);
        }
        //改变各个实体所在层的颜色
        public void Change_Layer_Color(DBObject[] dBObjects, short Index_color)
        {
            foreach (DBObject dBObject in dBObjects)
            {
                Change_Layer_Color((Entity)dBObject, Index_color);
            }
        }
        //改变所有层的颜色
        public void Change_All_layer_Color(short Index_color)
        {
            Change_Layer_Color(All_LayerName(), Index_color);
        }

        //在layername中的显示状态
        public void Object_Layer_show_hide(string layer_name, bool off)//关为ture
        {
            //var doc_lock=cadSer.Application.DocumentManager.MdiActiveDocument.LockDocument();

            //using (Transaction trans = db.TransactionManager.StartTransaction())
            //{
            //    LayerTable lb = (LayerTable)trans.GetObject(db.LayerTableId, OpenMode.ForWrite);
            //    //LayerTableRecord ltr1 = (LayerTableRecord)trans.GetObject(lb.ObjectId, OpenMode.ForWrite);

            //    foreach (ObjectId objid in lb)
            //    {

            //        LayerTableRecord ltr1 = (LayerTableRecord)id_To_Object(objid, trans);
            //        // ed.WriteMessage(ltr1.Name + ":" + ltr1.Color + "\n");
            //        if (layer_name == ltr1.Name)
            //        {

            //            ltr1.IsOff = off;
            //            // ed.WriteMessage(ltr1.Name + ":" + ltr1.Color + "\n");
            //        }

            //    }

            //    trans.Commit();
            //}

            //doc_lock.Dispose();

            List<string> layer_name_list = new List<string>
            {
                layer_name
            };
            Object_Layer_show_hide(layer_name_list, off);

        }
        //在List layername中的显示状态
        public void Object_Layer_show_hide(List<string> layer_list, bool off)//关为ture

        {
            Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
            Database db = HostApplicationServices.WorkingDatabase;
            var doc_lock = cadSer.Application.DocumentManager.MdiActiveDocument.LockDocument();

            using (Transaction trans = db.TransactionManager.StartTransaction())
            {

                // var doc_lock = cadSer.Application.DocumentManager.MdiActiveDocument.LockDocument()


                LayerTable lb = (LayerTable)trans.GetObject(db.LayerTableId, OpenMode.ForWrite);
                //LayerTableRecord ltr1 = (LayerTableRecord)trans.GetObject(lb.ObjectId, OpenMode.ForWrite);

                foreach (ObjectId objid in lb)
                {
                    try
                    {


                        LayerTableRecord ltr1 = (LayerTableRecord)trans.GetObject(objid, OpenMode.ForWrite);


                        // ed.WriteMessage(ltr1.Name + ":" + ltr1.Color + "\n");
                        ed.WriteMessage(ltr1.Name + ":" + ltr1.Color.ColorValue + "\n");
                        if (layer_list.Contains(ltr1.Name))
                        {
                            ltr1.IsOff = off;
                        }
                        else if (off == false)
                        {
                            ltr1.IsOff = true;
                        }
                    }
                    catch { }

                }

                trans.Commit();

            }

            doc_lock.Dispose();
            db.Dispose();
            //ed = null;

        }
        //实体所在层中的显示状态
        public void Object_Layer_show_hide(Entity ent, bool off)
        {
            List<string> layer_name_list = new List<string>
            {
                ent.Layer
            };
            Object_Layer_show_hide(layer_name_list, off);
        }
        //实体数组所在层中的显示状态
        public void Object_Layer_show_hide(DBObject[] dBObjects, bool off)
        {

            List<string> layer_name_list = new List<string>();

            foreach (DBObject dBObject in dBObjects)
            {
                string layer_name = ((Entity)dBObject).Layer;
                if (!layer_name_list.Contains(layer_name))
                {
                    layer_name_list.Add(layer_name);
                }
            }


            Object_Layer_show_hide(layer_name_list, off);
            //layer_name_list.Clear();

        }
        //选择级selectset中实体所在层中的显示状态
        public void Object_Layer_show_hide(bool off)
        {

            Object_Layer_show_hide(GetObject_Array(), off);
            //  Object_Layer_show_hide(Layer_Name(ent), off);
        }
        //所有层中的显示状态
        public void Object_All_Layer_show_hide(bool off)
        {

            Object_Layer_show_hide(All_LayerName(), off);
            //  Object_Layer_show_hide(Layer_Name(ent), off);
        }



        //修改单个实体颜色
        public void Change_entity_Color(Entity ent, short Index_color)
        {
            Database db = HostApplicationServices.WorkingDatabase;
            using (var doc_lock = cadSer.Application.DocumentManager.MdiActiveDocument.LockDocument())
            {
                using (Transaction trans = db.TransactionManager.StartTransaction())
                {

                    BlockTable bt = (BlockTable)id_To_Object(ent.Id, trans);
                    ent.Color = Color.FromColorIndex(ColorMethod.ByAci, Index_color);


                    trans.Commit();
                }
            }
            db.Dispose();
        }

        //修改单个实体颜色为实际颜色,由随层或随块转为颜色
        public void Change_entity_ToColor(Entity ent)
        {
            Database db = HostApplicationServices.WorkingDatabase;
            // Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
            using (var doc_lock = cadSer.Application.DocumentManager.MdiActiveDocument.LockDocument())
            {

                using (Transaction trans = db.TransactionManager.StartTransaction())
                {
                    Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
                    //  BlockTable bt = (BlockTable)id_To_Object(ent.Id, trans);
                    short color_index;
                    //Console.WriteLine(ent.Color.ToString()+"\n");
                    // Console.ReadLine();
                    ed.WriteMessage(ent.Color.ToString() + "\n");

                    if (ent.Color.ToString().ToLower() == "bylayer")
                    {
                        Entity entt = (Entity)id_To_Object(ent.Id, trans);
                        // ed.WriteMessage(ent.Color.ColorName);
                        color_index = ((LayerTableRecord)trans.GetObject(ent.LayerId, OpenMode.ForRead)).Color.ColorIndex;

                        entt.Color = Color.FromColorIndex(ColorMethod.ByAci, color_index);
                        ed.WriteMessage(ent.Color.ToString() + "==" + color_index.ToString() + "\n");
                    }
                    if (ent.Color.ToString().ToLower() == "byblock")
                    {
                        Entity entt = (Entity)id_To_Object(ent.Id, trans);
                        // color_index = 7;
                        entt.Color = Color.FromColorIndex(ColorMethod.ByAci, (short)7);
                    }
                    //ent.Color = Color.FromColorIndex(ColorMethod.ByAci, color_index);


                    trans.Commit();
                    // color_index = null;
                }
            }
            db.Dispose();
        }
        //修改所有实体颜色为实际颜色,由随层或随块转为颜色
        public void Change_entity_ToColor(DBObjectCollection all_dbo_col)
        {
            foreach (DBObject dbo in all_dbo_col)
            {
                Change_entity_ToColor((Entity)dbo);
            }
        }


        //修改选择集Collection实体颜色
        public void Change_entity_Color(DBObjectCollection dboCollection, short Index_color)
        {
            Database db = HostApplicationServices.WorkingDatabase;
            using (var doc_lock = cadSer.Application.DocumentManager.MdiActiveDocument.LockDocument())
            {
                foreach (DBObject dbo in dboCollection)
                {
                    Entity ent = (Entity)dbo;
                    using (Transaction trans = db.TransactionManager.StartTransaction())
                    {
                        BlockTable bt = (BlockTable)id_To_Object(db.BlockTableId, trans);//trans.GetObject(db.BlockTableId,OpenMode.ForWrite);

                        Entity btr = (Entity)id_To_Object(ent.Id, trans);//trans.GetObject(ent.Id,OpenMode.ForWrite  );
                        try
                        {
                            ent.Color = Color.FromColorIndex(ColorMethod.ByAci, Index_color);
                        }
                        catch { }



                        trans.Commit();

                        // bt.Dispose();
                        //   btr.Dispose();
                    }
                    ent.Dispose();
                }
            }
            db.Dispose();
        }
        //改变 dbobject[]对象 颜色
        public void Change_entity_Color(DBObject[] dbo_array, short Index_color)
        {
            Database db = HostApplicationServices.WorkingDatabase;
            using (var doc_lock = cadSer.Application.DocumentManager.MdiActiveDocument.LockDocument())
            {
                foreach (DBObject dbo in dbo_array)
                {
                    Entity ent = (Entity)dbo;
                    // BlockTable block_table=
                    using (Transaction trans = db.TransactionManager.StartTransaction())
                    {
                        BlockTable bt = (BlockTable)id_To_Object(db.BlockTableId, trans);//trans.GetObject(db.BlockTableId,OpenMode.ForWrite);

                        Entity btr = (Entity)id_To_Object(ent.Id, trans);//trans.GetObject(ent.Id,OpenMode.ForWrite  );
                        ent.Color = Color.FromColorIndex(ColorMethod.ByAci, Index_color);
                        trans.Commit();

                        bt.Dispose();
                        btr.Dispose();
                    }
                    ent.Dispose();
                }
            }
            db.Dispose();
        }
        //改变 选择级selectset中的所有对象
        public void Change_entity_Color(short Index_color)
        {
            DBObject[] dbo_array = GetObject_Array();
            List<string> layer_name = Layer_Name(dbo_array);
            DBObject[] dbo_layer_array = id_To_Object(dboCollection_To_id(GetObjects(layer_name)));
            Change_entity_Color(dbo_layer_array, Index_color);
        }


        //修改单个实体颜色与状态
        //show_hide 0为显示 1不显示 3现因不改变
        //index_color=999,则颜色不修改
        //index_color=256,随层;index_color=0,随块

        public void Change_entity_Show_Hide(List<Entity> ent_list, short Index_color, int show_hide)
        {
            //Database db = HostApplicationServices.WorkingDatabase;
            //using (var doc_lock = cadSer.Application.DocumentManager.MdiActiveDocument.LockDocument())
            //{
            //    var object_arr = id_To_Object(dboCollection_To_id(Get_All_Object_WorkingSpace()));
            //    using (Transaction trans = db.TransactionManager.StartTransaction())
            //    {
            //        if (show_hide == 0)
            //        {
            //            foreach (Entity ent in object_arr)
            //            {
            //                BlockTable btt = (BlockTable)id_To_Object(ent.Id, trans);
            //                if (ent_list.Contains(ent))
            //                {
            //                    ent.Visible = false;
            //                }
            //                else
            //                {
            //                    ent.Visible = true;
            //                }
            //            }

            //        }
            //        if (show_hide == 2)//隐藏
            //        {
            //            foreach (Entity ent in ent_list)
            //            {
            //                BlockTable bt = (BlockTable)id_To_Object(ent.Id, trans);


            //                ent.Visible = false;

            //            }
            //        }
            //        if (show_hide == 3)
            //        {
            //            foreach (Entity ent in ent_list)
            //            {
            //                BlockTable bt = (BlockTable)id_To_Object(ent.Id, trans);


            //                ent.Color = Color.FromColorIndex(ColorMethod.ByAci, Index_color);

            //            }
            //        }
            //        trans.Commit();
            //    }
            //}
        }
        //所有对象显示
        public void Change_All_entity_Show_Hide(bool isShow)
        {
            Database db = HostApplicationServices.WorkingDatabase;
            using (var doc_lock = cadSer.Application.DocumentManager.MdiActiveDocument.LockDocument())
            {
                // var object_arr = id_To_Object(dboCollection_To_id(Get_All_Object_WorkingSpace()));
                var object_arr = id_To_Object(dboCollection_To_id(Get_All_Object_WorkingSpace()));
                using (Transaction trans = db.TransactionManager.StartTransaction())
                {

                    foreach (DBObject ent in object_arr)
                    {
                        //Entity entt = ent as Entity;
                        try
                        {
                            Entity btt = (Entity)id_To_Object(ent.Id, trans);

                            btt.Visible = isShow;
                        }
                        catch { };
                    }

                    trans.Commit();
                }
            }
        }

        //修改实体List dbobject显示状态
        public void Change_entity_Show_Hide(List<DBObject> dbo_list, bool isShow)
        {
            Database db = HostApplicationServices.WorkingDatabase;
            using (var doc_lock = cadSer.Application.DocumentManager.MdiActiveDocument.LockDocument())
            {

                //var object_arr = id_To_Object(dboCollection_To_id(Get_All_Object_WorkingSpace()));
                var object_arr = id_To_Object(dboCollection_To_id(GetObjects(dbo_list.ToArray(), (Int16)0)));
                using (Transaction trans = db.TransactionManager.StartTransaction())
                {
                    if (isShow == true)
                    {
                        //  Change_All_entity_Show_Hide(false);
                        foreach (DBObject ent in object_arr)
                        {
                            Entity entt = ent as Entity;
                            try
                            {
                                Entity btt = (Entity)id_To_Object(ent.Id, trans);
                                // Entity btt = ent as Entity;
                                if (dbo_list.Contains(ent) && entt.Visible)
                                {
                                    btt.Visible = true;
                                }
                                else
                                {
                                    btt.Visible = false;
                                }
                            }
                            catch { };
                        }

                    }
                    if (isShow == false)//隐藏
                    {
                        foreach (DBObject ent in dbo_list)
                        {
                            //  Entity entt = ent as Entity;
                            Entity btt = (Entity)id_To_Object(ent.Id, trans);

                            //  Entity btt = ent as Entity;
                            btt.Visible = false;


                        }
                    }
                    trans.Commit();
                }
            }
        }

        //所选物体可visuable
        public void Change_entity_Show_Hide_1(List<DBObject> dbo_list)
        {
            Database db = HostApplicationServices.WorkingDatabase;
            using (var doc_lock = cadSer.Application.DocumentManager.MdiActiveDocument.LockDocument())
            {

                //var object_arr = id_To_Object(dboCollection_To_id(Get_All_Object_WorkingSpace()));
                var object_arr = id_To_Object(dboCollection_To_id(GetObjects(dbo_list.ToArray(), (Int16)0)));
                using (Transaction trans = db.TransactionManager.StartTransaction())
                {

                    foreach (DBObject ent in dbo_list)
                    {
                        //  Entity entt = ent as Entity;
                        Entity btt = (Entity)id_To_Object(ent.Id, trans);

                        //  Entity btt = ent as Entity;
                        btt.Visible = true;


                    }

                    trans.Commit();
                }
            }
        }


        //修改实体List dbobject显示状态,leixing
        public void Change_entity_Show_Hide(List<DBObject> dbo_list, Int16 leixing, bool isShow)
        {
            Database db = HostApplicationServices.WorkingDatabase;

            using (var doc_lock = cadSer.Application.DocumentManager.MdiActiveDocument.LockDocument())
            {
                List<string> list_string = Layer_Name(dbo_list.ToArray());
                //foreach (DBObject dbo in dbo_list)
                //{
                //    string layername = ((Entity)dbo).Layer;
                //    if (list_string.Contains(layername)==false)
                //    { list_string.Add(layername); }
                //}
                DBObjectCollection dbo_layer_object_coll = GetObjects(list_string);
                //var object_arr = id_To_Object(dboCollection_To_id(Get_All_Object_WorkingSpace()));
                var object_arr = id_To_Object(dboCollection_To_id(GetObjects(dbo_list.ToArray(), leixing)));
                var all_object_layer = id_To_Object(dboCollection_To_id(dbo_layer_object_coll));

                using (Transaction trans = db.TransactionManager.StartTransaction())
                {
                    if (isShow == true)
                    {
                        //  Change_All_entity_Show_Hide(false);
                        foreach (DBObject ent in all_object_layer)
                        {
                            Entity entt = ent as Entity;
                            try
                            {
                                Entity btt = (Entity)id_To_Object(ent.Id, trans);
                                // Entity btt = ent as Entity;
                                if (object_arr.Contains(ent) && entt.Visible)
                                {
                                    btt.Visible = true;
                                }
                                else
                                {
                                    btt.Visible = false;
                                }
                            }
                            catch { };
                        }

                    }
                    if (isShow == false)//隐藏
                    {
                        foreach (DBObject ent in object_arr)
                        {
                            //  Entity entt = ent as Entity;
                            Entity btt = (Entity)id_To_Object(ent.Id, trans);

                            //  Entity btt = ent as Entity;
                            btt.Visible = false;


                        }
                    }
                    trans.Commit();
                }
            }
        }
        //修改实体 s数组 dbobject显示状态
        public void Change_entity_Show_Hide(DBObject[] dbo_arr, bool isShow)
        {
            //List<Entity>
            Change_entity_Show_Hide(dbo_arr.ToList<DBObject>(), isShow);
        }
        //修改实体 s数组 dbobject显示状态
        public void Change_entity_Show_Hide(DBObject[] dbo_arr, Int16 leixing, bool isShow)
        {
            //List<Entity>
            Change_entity_Show_Hide(dbo_arr.ToList<DBObject>(), leixing, isShow);
        }

        //修改选择集对象示状态
        public void Change_entity_Show_Hide(Int16 leixing, bool isShow)
        {
            //List<Entity>
            Change_entity_Show_Hide(GetObject_Array(), leixing, isShow);
        }

        //修改选择集对象示状态
        public void Change_entity_Show_Hide(bool isShow)
        {
            //List<Entity>
            Change_entity_Show_Hide(GetObject_Array(), isShow);
        }





        //   [CommandMethod("allbylayer")]
        public void Change_all_Color(short Index_color)
        {
          //  Change_entity_Color(Get_All_Object_WorkingSpace(), Index_color);
            Change_entity_Color(GetAllObjects(), Index_color);
        }
        //    [CommandMethod("allbyblock")]
        //public void Change_all_Color_byblock()
        //{
        //    Change_entity_Color(Get_All_Object_WorkingSpace(), 0);
        //}

        public void Change_all_To_Color()
        {
            Change_entity_ToColor(GetAllObjects());
        }

        public void Change_object_layer_all_To_Color()
        {
            DBObject[] dbo_array = GetObject_Array();
            List<string> layer_name = Layer_Name(dbo_array);

            Change_entity_ToColor(GetObjects(layer_name));
        }

        public void Change_dbo_coll_RadomColor(DBObjectCollection dbo_all)
        {

            //  DBObjectCollection dbo_all = Get_All_Object_WorkingSpace();
            Database db = HostApplicationServices.WorkingDatabase;

            using (var doc_lock = cadSer.Application.DocumentManager.MdiActiveDocument.LockDocument())
            {

                using (Transaction trans = db.TransactionManager.StartTransaction())
                {
                    foreach (DBObject dbo in dbo_all)
                    {
                        Entity ent = (Entity)trans.GetObject(dbo.Id, OpenMode.ForWrite);
                        Random rdm = new Random(Guid.NewGuid().GetHashCode());
                        // short  rdm = (short)rdm.Next(1,255);
                        ent.ColorIndex = (short)rdm.Next(1, 255);
                    }
                    trans.Commit();
                }
            }
        }

        public void Change_all_object_randomColor()
        {
            //  Change_dbo_coll_RadomColor(Get_All_Object_WorkingSpace());
          //  GetAllObjects
                Change_dbo_coll_RadomColor(GetAllObjects());
        }

        public void Change_selct_layer_object_randomColor()
        {
            List<string> layer = Layer_Name(GetObject_Array());
            //DBObjectCollection dbo_col = GetObjects(layer);
            Change_dbo_coll_RadomColor(GetObjects(layer));
        }

        public void Change_dbo_layer_RadomColor(List<string> layer_list)
        {

            //  DBObjectCollection dbo_all = Get_All_Object_WorkingSpace();
            Database db = HostApplicationServices.WorkingDatabase;

            using (var doc_lock = cadSer.Application.DocumentManager.MdiActiveDocument.LockDocument())
            {

                using (Transaction trans = db.TransactionManager.StartTransaction())
                {
                    LayerTable lt = trans.GetObject(db.LayerTableId, OpenMode.ForRead) as LayerTable;
                    foreach (string layername in layer_list)
                    {

                        LayerTableRecord layer = (LayerTableRecord)trans.GetObject(lt[layername], OpenMode.ForWrite);
                        Random rdm = new Random(Guid.NewGuid().GetHashCode());
                        // short  rdm = (short)rdm.Next(1,255);
                        layer.Color = Color.FromColorIndex(ColorMethod.ByAci, (short)rdm.Next(1, 255)); //(short)rdm.Next(1, 255);
                    }
                    trans.Commit();
                }
            }
        }

        public void Change_object_Layer_RandomColor()
        {
            List<string> layer = Layer_Name(GetObject_Array());
            Change_dbo_layer_RadomColor(layer);
        }

        public void Change_All_Layer_RandomColor()
        {
            List<string> layer = Layer_Name(Get_All_Object_WorkingSpace());
            Change_dbo_layer_RadomColor(layer);
        }


        //[CommandMethod("ccc")]
        //public void alllayer1()
        //{

        //    Change_entity_Color(251);
        //}



        [CommandMethod("alllayer")]
        public void alllayer()
        {
            var list_layer = All_LayerName();
            Change_Layer_Color(list_layer, 251);
        }
        [CommandMethod("sss")]
        public void slayer()
        {
            Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
            // string layername = Layer_Name(((Entity)id_To_Object(ed.GetEntity("选择一个实体\n").ObjectId)));
            short index = (short)ed.GetInteger("输入索引值\n").Value;

            Change_entity_Color(index);
        }




        //由dbObject Colletion 转为 SelectionSet
        public void SelctionSet(DBObjectCollection dboCollection)
        {
            Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
            ed.SetImpliedSelection(dboCollection_To_id(dboCollection));
        }


        //由objectID 得到Object,只读
        public DBObject id_To_Object(ObjectId objid)
        {
            Database db = HostApplicationServices.WorkingDatabase;

            DBObject dbo;
            var doc_lock = cadSer.Application.DocumentManager.MdiActiveDocument.LockDocument();
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {

                dbo = trans.GetObject(objid, OpenMode.ForWrite);

                trans.Commit();
            }

            doc_lock.Dispose();
            return dbo;
        }


        public DBObject id_To_Object(ObjectId objid, Transaction trans)
        {
            DBObject dbo = trans.GetObject(objid, OpenMode.ForWrite);//只写

            return dbo;
        }

        //由objectID[] 得到Object[]
        public DBObject[] id_To_Object(ObjectId[] objid_array)
        {
            List<DBObject> dbo_list = new List<DBObject>();

            foreach (ObjectId objId in objid_array)
            {
                dbo_list.Add(id_To_Object(objId));

            }

            return dbo_list.ToArray();
            // dbo_list = null;
        }

        //由dbobject  collection得到ObjectID[]
        public ObjectId[] dboCollection_To_id(DBObjectCollection dbo_collection)
        {
            List<ObjectId> objid = new List<ObjectId>();
            foreach (DBObject dbo in dbo_collection)
            {
                objid.Add(dbo.ObjectId);
            }

            return objid.ToArray();
        }

        //dbobject  得到 objectid
        public ObjectId object_To_id(DBObject dbo)
        {
            return ((Entity)dbo).ObjectId;
        }

        //dbobject [] 得到 objectid[]
        public ObjectId[] object_To_id(DBObject[] dbo_array)
        {
            List<ObjectId> objId_list = new List<ObjectId>();

            foreach (DBObject dbo in dbo_array)
            {
                objId_list.Add(dbo.ObjectId);
            }

            return objId_list.ToArray();
        }


        //由dbobject[] array 转 dbocollection

        public DBObjectCollection dbo_arr_to_dboCollection(DBObject[] abi_arr)
        {
            DBObjectCollection dbo_collection = new DBObjectCollection();
            foreach (DBObject dbo in abi_arr)
            {
                dbo_collection.Add(dbo);
            }
            return dbo_collection;
        }
        //objectID[] array 转 dbocollection
        public DBObjectCollection dboID_arr_to_dboCollection(ObjectId[] abid_arr)
        {
            DBObjectCollection dbo_collection = new DBObjectCollection();
            foreach (ObjectId dbo in abid_arr)
            {
                dbo_collection.Add(id_To_Object(dbo));
            }
            return dbo_collection;
        }



        [CommandMethod("getc")]
        public void getlayername()
        {
            Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
            Database db = HostApplicationServices.WorkingDatabase;
            //PromptSelectionResult select_set = ed.GetSelection();
            var select_entity = ed.GetEntity("选取对象\n");
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                Entity ent_select = (Entity)trans.GetObject(select_entity.ObjectId, OpenMode.ForRead);

                ed.WriteMessage(ent_select.Color.ColorValue + "\n");


                ed.WriteMessage(EntityColor.LookUpAci(ent_select.Color.ColorValue.R, ent_select.Color.ColorValue.G, ent_select.Color.ColorValue.B).ToString());

                //ed.WriteMessage(ent_select.Color.EntityColor.TrueColor+ "\n" );
                //ed.WriteMessage( ent_select.EntityColor.TrueColor + "\n");
                // ed.WriteMessage(ent_select.EntityColor.ColorMethod.GetType()+"\n");
                // ed.WriteMessage(ent_select.EntityColor.GetType().ToString()+"\n");
                //ed.WriteMessage(ent_select.EntityColor.Green+ "\n");
                // EntityColor ent_color = Color.FromColor();
                ed.WriteMessage(ent_select.Color.ColorMethod.ToString() + "\n");

                trans.Commit();

            }

        }
        [CommandMethod("bc")]
        public void color_compare()
        {
            Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
            var entity_1 = (Entity)id_To_Object(ed.GetEntity("第一个\n").ObjectId);
            var entity_2 = (Entity)id_To_Object(ed.GetEntity("第2个\n").ObjectId);
            ed.WriteMessage(entity_1.Color.CompareTo(entity_2).ToString() + "\n");

           // ed.WriteMessage((entity_1.EntityColor == entity_2.EntityColor).ToString() + "\n");
        }
        [CommandMethod("bcc")]
        public void color_compare1()
        {
            Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;

            Color ent_Color = Color.FromColorIndex(ColorMethod.ByAci, 100);
            ed.WriteMessage(ent_Color.ColorValue + "\n");


            Color ent_Color_1 = Color.FromRgb(100, 100, 100);
            ed.WriteMessage(ent_Color_1.ColorIndex + "\n");

            ed.WriteMessage(ent_Color.ColorValue + "\n");

            ed.WriteMessage("========================================");

        }



        [CommandMethod("gs")]
        public void GetSelctionSet()
        {
            Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
            PromptSelectionResult select_set = ed.GetSelection();
            SelectionSet ss = select_set.Value;
            // var select_entity = ed.GetEntity("选取对象\n");
            //using (Transaction trans = db.TransactionManager.StartTransaction())
            //{
            //    Entity ent_select = (Entity)trans.GetObject(select_entity.ObjectId, OpenMode.ForRead);

            ed.WriteMessage("共选择对象个数：" + ss.Count.ToString() + "个\n");
            foreach (ObjectId obj_id in ss.GetObjectIds())
            {
                Entity ent = (Entity)id_To_Object(obj_id);
                ed.WriteMessage(ent.ColorIndex.ToString() + ",entitycolor:" + ent.EntityColor.ColorIndex.ToString() + "\n");
            }
            //}
            ed.WriteMessage(Thread.CurrentThread.ManagedThreadId.ToString() + ":GetSelctionSet函数线程\n");

        }



        //------------------------选择级selection函数-----------------------------------------------
        //------------------------选择级selection函数-----------------------------------------------  
        #region
        //根据selection选择集结果得到ObjectId[]
        public ObjectId[] GetObjectId_Array(PromptSelectionResult psr)
        {
            //  PromptSelectionResult select_set = ed.GetSelection();
            //  SelectionSet ss = select_set.Value;
            return psr.Value.GetObjectIds();
        }
        //获取selection选择集的ObjectId[]，有交互选择过程
        public ObjectId[] GetObjectId_Array()
        {
            Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
            try { ObjectId[] aa = ed.GetSelection().Value.GetObjectIds(); 
            return aa;
            }
            catch { ed.WriteMessage("选择为空!\n"); return null; }            
        }
        [CommandMethod("zjy")]
        public void GetObjectId_Array_1()
        {
            Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
            try
            {
                List<ObjectId> objectIds = ed.GetSelection().Value.GetObjectIds().ToList<ObjectId>();
                // PromptSelectionResult select_set = ed.GetSelection();
                // SelectionSet ss = select_set.Value;
                // try
                //  {
                //  PromptSelectionOptions prompt = new PromptSelectionOptions();
                //objectIds = select_set.Value.GetObjectIds().ToList<ObjectId>();
                // }
                // catch { ed.WriteMessage("无选择集:\n"); };

                ed.WriteMessage(objectIds.Count.ToString() + "\n");

            }
            catch
            {
                ed.WriteMessage("未选择对象\n");

            }
            ed.WriteMessage(Thread.CurrentThread.ManagedThreadId.ToString() + ":zjy函数线程\n");
        }

        //根据selection选择级结果得到Object[]
        public DBObject[] GetObject_Array(PromptSelectionResult psr)
        {
            return id_To_Object(GetObjectId_Array(psr));
        }
        //获取selection选择集的Object[]，有交互选择过程
        public DBObject[] GetObject_Array()
        {
            return id_To_Object(GetObjectId_Array());
        }



        #endregion

        //------------------------选择级selection函数-----------------------------------------------
        //------------------------选择级selection函数----------------------------------------------- 













        //所以图层都设置为 cad中配色系统的某一颜色值
        //索引7为白色，索引251为淡色
        public void All_LayerChange_Color(short ColorIndex)
        {
            Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                LayerTable lb = (LayerTable)trans.GetObject(db.LayerTableId, OpenMode.ForWrite);
                //int i = 1;
                ed.WriteMessage("\n");
                foreach (ObjectId ltr in lb)
                {
                    LayerTableRecord ltr1 = (LayerTableRecord)trans.GetObject(ltr, OpenMode.ForWrite);
                    ltr1.Color = Color.FromColorIndex(ColorMethod.ByColor, ColorIndex);
                    //ed.WriteMessage(ltr1.Name+"第"+i++.ToString()+"\n");
                    //ed.WriteMessage(i++.ToString()+"\n");
                    //if (ltr1.Name == "")
                    //{
                    //    ltr1.Color= Color.FromColorIndex(ColorMethod.ByColor, 7);
                    //    ed.WriteMessage("空图层\n");
                    //}
                    //  MessageBox.Show(ltr1.Name);
                }
                //ed.WriteMessage(i.ToString() + "完成\n");
                trans.Commit();
            }
        }
        //指定名称的图层都设置为 cad中配色系统的某一颜色值
        public void LayerChangeColor(string layerName, short ColorIndex)
        {
            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                LayerTable lb = (LayerTable)trans.GetObject(db.LayerTableId, OpenMode.ForWrite);
                foreach (ObjectId ltr in lb)
                {
                    LayerTableRecord ltr1 = (LayerTableRecord)trans.GetObject(ltr, OpenMode.ForWrite);
                    if (ltr1.Name == layerName)
                    {
                        ltr1.Color = Color.FromColorIndex(ColorMethod.ByColor, ColorIndex);
                        //  MessageBox.Show(ltr1.Name);
                    }
                }
                trans.Commit();
            }
        }
        //选定一对象获取图层名
        public string Layer_Name(Entity entity)
        {
            Database db = HostApplicationServices.WorkingDatabase;
            string layer_name;
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                // trans.se
                LayerTableRecord ltr1 = (LayerTableRecord)trans.GetObject(entity.LayerId, OpenMode.ForRead);
                layer_name = ltr1.Name;
                //  MessageBox.Show("所选对象所在的层为" + layer_name);
                trans.Commit();
            }
            return layer_name;
        }

        public List<string> Layer_Name(DBObject[] dBObjects)
        {
            List<string> layer_name_list = new List<string>();

            foreach (DBObject dbo in dBObjects)
            {
                string layername = Layer_Name((Entity)dbo);
                if (layer_name_list.Contains(layername) == false)
                {
                    layer_name_list.Add(layername);
                }
            }

            return layer_name_list;
        }

        public List<string> Layer_Name(DBObjectCollection dbo_col)
        {
            List<string> layer_name_list = new List<string>();

            foreach (DBObject dbo in dbo_col)
            {
                string layername = Layer_Name((Entity)dbo);
                if (layer_name_list.Contains(layername) == false)
                {
                    layer_name_list.Add(layername);
                }
            }

            return layer_name_list;
        }

        ////获取模型空间的所以物体，返回选择集Collection,是物体对象object，不是objectid
        //public DBObjectCollection Get_All_Object_WorkingSpace()
        //{
        //    DBObjectCollection dbCollection = new DBObjectCollection();
        //    using (Transaction trans = db.TransactionManager.StartTransaction())
        //    {
        //        BlockTable bt = (BlockTable)trans.GetObject(db.BlockTableId, OpenMode.ForWrite);

        //        ObjectId obj_id = bt[BlockTableRecord.ModelSpace];
        //        BlockTableRecord btr = (BlockTableRecord)trans.GetObject(obj_id, OpenMode.ForWrite);

        //        foreach (ObjectId objid in btr)
        //        {

        //            DBObject dbo = trans.GetObject(objid, OpenMode.ForRead);
        //            dbCollection.Add(dbo);

        //        }

        //        trans.Commit();
        //    }
        //    return dbCollection;

        //}

        //根据选择集Collection 得到工作空间的选择，同 cad命令中的select方法,此处的dbo_collection,是物体对象object，不是objectid
        public void SetSectionSet(DBObjectCollection dbo_Collection)
        {
            Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;

            List<ObjectId> list_obj_id = new List<ObjectId>();
            foreach (DBObject objid in dbo_Collection)
            {

                list_obj_id.Add(objid.ObjectId);
            }
            ObjectId[] obj_id_array = list_obj_id.ToArray();
            ed.SetImpliedSelection(obj_id_array);
        }

        public void SetSectionSet(ObjectId[] dboId_array)
        {
            Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;

            ed.SetImpliedSelection(dboId_array);
        }

        public void SetSectionSet(DBObject[] dbo_array)
        {
            Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;

            ed.SetImpliedSelection(object_To_id(dbo_array));
        }

        [CommandMethod("layerSelect")]
        public void SetSectionSet()
        {
            Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
            ObjectId[] objId_array = dboCollection_To_id(GetObjects((Int16)0));
            ed.SetImpliedSelection(objId_array);
        }
        //leixing=1为选择与对象的层和颜色相匹配的所有对象,暂不加入
        public void SetSectionSet(Int16 leixing)
        {
            Editor ed = cadSer.Application.DocumentManager.MdiActiveDocument.Editor;
            DBObject[] dbo_array_sel = GetObject_Array();
            DBObjectCollection dbo_col = GetObjects(dbo_array_sel, leixing);
            ObjectId[] objId_array = dboCollection_To_id(dbo_col);
            ed.SetImpliedSelection(objId_array);
        }
    }
    class color_RGB : IComparable<color_RGB>
    {
        private byte red;
        private byte green;
        private byte blue;
        public color_RGB(int Color_index)
        {
            Color ent_Color = Color.FromColorIndex(ColorMethod.ByAci, (Int16)Color_index);
            var color_value = ent_Color.ColorValue;
            Red = color_value.R;
            Green = color_value.G;
            Blue = color_value.B;
        }

        public color_RGB(Color color)
        {
            var color_value = color.ColorValue;
            Red = color_value.R;
            Green = color_value.G;
            Blue = color_value.B;
        }

        public color_RGB(EntityColor ent_color)
        {
            //var color_value =ent_color.
            Red = ent_color.Red;
            Green = ent_color.Green;
            Blue = ent_color.Blue;
        }
        public color_RGB(Entity entity)
        {
            //var color_value =ent_color.
            // color_RGB(entity.Color);

            var color_value = entity.Color.ColorValue;
            Red = color_value.R;
            Green = color_value.G;
            Blue = color_value.B;

        }
        public color_RGB(DBObject dbo)
        {
            //var color_value =ent_color.
            // color_RGB(entity.Color);
            Entity entity = (Entity)dbo;
            var color_value = entity.Color.ColorValue;
            Red = color_value.R;
            Green = color_value.G;
            Blue = color_value.B;

        }

        public byte Red { get => red; set => red = value; }
        public byte Green { get => green; set => green = value; }
        public byte Blue { get => blue; set => blue = value; }

        public int CompareTo(color_RGB objrgb2)
        {
            int a = 0;
            if (this.red == objrgb2.red && this.green == objrgb2.green && this.blue == objrgb2.blue)
            { a = 1; }
            return a;
        }


    }


}