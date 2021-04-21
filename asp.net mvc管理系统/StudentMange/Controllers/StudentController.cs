using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.ModelBinding;
using static StudentMange.Models.Student;
using StudentMange.Models;
using System.Net;
using System.Data.Entity;
using static Models.EasyTree;
using Models;
using Core;
using Common;
using Common.App.Common;

namespace StudentMange.Controllers
{
    public class StudentController : Controller

    {
        ValidationErrors errors = new ValidationErrors();
        private StudentDBContent db = new StudentDBContent();
        private TreeContext tree = new TreeContext();
        public StudentIBLL.StudentIBLL m_bll = new StudentBLL.StudentBLL();
        public TreeIBLL.TreeIBLL treebll = new TreeBLL.TreeBLL();
        // GET: Student

        public ActionResult Index()

        {
            
            var u = Session["Username"];
            
             ViewBag.username =Request.Cookies["user"].Values["Username"];


            return View();

        }
        public ActionResult ScoreQuery()
        {
            return View();
        }
        public ActionResult GetTrees()
        {   string json = string.Empty;
            json = @"[{
                    ""id"":1,
                    ""text"":""用户列表"",
                    ""children"":[{
                        ""id"":11,
                        ""text"":""普通用户"",
                        ""state"":""closed"",
                        ""children"":[{
                            ""id"":111,
                            ""text"":""用户添加""
                        },{
                            ""id"":112,
                            ""text"":""用户功能""
                        }]
                    },{
                        ""id"":12,
                        ""text"":""超级用户"",
                        ""state"":""closed"",
                        ""children"":[{
                            ""id"":121,
                            ""text"":""用户添加""
                        },{
                            ""id"":122,
                            ""text"":""用户功能""
                        }]
                    },{
                        ""id"":13,
                        ""text"":""学生""
                    },{
                        ""id"":14,
                        ""text"":""老师""
                    },{
                        ""id"":15,
                        ""text"":""管理者""
                    }]
                },{      ""id"":2,
                    ""text"":""权限管理""
                     ""children"":[{
                        ""id"":21,
                        ""text"":""用户权限"",
                        ""state"":""closed"",
                        ""children"":[{
                            ""id"":211,
                            ""text"":""老师权限""
                        },{
                            ""id"":212,
                            ""text"":""开发者权限""
                        }]
                    }]}]
                
}]";
            return Content(json);
        }
        [HttpPost]
        public  JsonResult GetTree()
        {
            List<EasyTree> list = treebll.GetList(0);
            List<EasyTree> trees = new List<EasyTree>();
            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    EasyTree TreeModel = new EasyTree();
                    TreeModel.children = new List<EasyTree>();
                    TreeModel.id = item.id;
                    TreeModel.attributes = item.attributes;
                    TreeModel.text = item.text;
                    TreeModel.state = "open";
                    TreeModel.ischecked = true;
                    var treeson = getTree(item.id);
                    if (treeson != null && treeson.Count > 0)
                    {
                        TreeModel.children.AddRange(treeson);
                    }
                    trees.Add(TreeModel);
                }
            }
            var json = trees.ToArray();

            return Json(json, JsonRequestBehavior.AllowGet);
        }
       
        public List<EasyTree> getTree(int pid)
        {
            List<EasyTree> modelList = new List<EasyTree>();
            var list = treebll.GetList(pid);
            if (list.Count > 0)
            {
                foreach (var item in list)
                {
                    EasyTree model = new EasyTree();
                    model.children = new List<EasyTree>();
                    model.id = item.id;
                    model.text = item.text;
                    model.state = "closed";
                    model.ischecked = true;
                    model.attributes = item.attributes;
                    var sonTree = getTree(item.id);
                    if (sonTree != null)
                    {
                        model.children.AddRange(sonTree);
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        

        // GET: Movies/Create
        public ActionResult Creat()
        {
            return View();//
        }

        // POST: Movies/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性；有关
        // 更多详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]//有效的校验码，防止被恶意攻击
        public JsonResult Creat([Bind(Include = "Name,Id,Myclass,Chinesescore,Mathscore,Englishscore,Computerscore")] Student student)//模型绑定，将穿过来的数据封装成一个实体
        {
            
            
               var r=m_bll.Create(ref errors,student);
                if (r == true)

                {
                    LogHandler.WriteServiceLog("虚拟用户", "Id:" + student.Id + ",Name:" + student.Name, "成功", "创建", "样例程序");
                    return Json(JsonHandler.CreateMessage(1, "插入成功"), JsonRequestBehavior.AllowGet);

            }
            else
            { string ErrorCol = errors.Error;
            LogHandler.WriteServiceLog("虚拟用户", "Id:" + student.Id + ",Name:" + student.Name + "," + ErrorCol, "失败", "创建", "样例程序");

                return Json(JsonHandler.CreateMessage(0, "插入失败" + ErrorCol), JsonRequestBehavior.AllowGet);

            }
                //db.Students.Add(student);将实体传入数据库
                //db.SaveChanges();//保存修改
                //重定向去INDEX视图
           
           
        }

        public JsonResult School()
        {
            List<School> s = new List<School>
            {
                new School{id=1,name="华东师范大学"},
                new School{id=2,name="上海交通大学"},
                new School{id=3,name="复旦大学"}
            };

            s.ToArray();


            return Json(s, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Dgraid(string searchstr)
        {
            int pageIndex = Convert.ToInt32(Request.Params["page"]);//easyUI默认传当前页码,固定参数为page
            int pageSize = Convert.ToInt32(Request.Params["rows"]);//easyUI默认传每页数据量,固定参数为rows
            
            int start = (pageIndex-1) * pageSize;
            int end = pageIndex * pageSize - 1;
            var students = from s in db.Students
                           select s;
            
            if (!String.IsNullOrEmpty(searchstr))
            {

                students = students.Where(s => s.Name.Contains(searchstr) || s.Myclass.Contains(searchstr));
            }
            
            var array = students.ToArray();
            var need = array.Skip(start).Take(pageSize);
            var json = new
            {
                total = students.ToList().Count,
                rows = need
            };
            
            
            return Json(json, JsonRequestBehavior.AllowGet);
        }//数据表格分页
        public JsonResult College(int id)
        {
            List<College> c = new List<College>();
            switch (id)
            {
                case 1: c.Add(new Models.College { id = 1, name = "教育学院" });
                    c.Add(new Models.College { id = 2, name = "计算机学院" });
                    break;
                case 2:c.Add(new Models.College { id = 3, name = "生命学院" });
                    c.Add(new Models.College { id = 4, name = "外语学院" });
                    break;
                case 3:
                    c.Add(new Models.College { id = 5, name = "建筑工程" });
                    c.Add(new Models.College { id = 6, name = "马克思学院" });
                    break;
            }
            c.ToArray();
            return Json(c, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Class(int id)
        {
            List<Class> b = new List<Class>();
            switch (id)
            {
                case 1:
                    b.Add(new Models.Class { id = "教育1", name = "教育1" });
                    b.Add(new Models.Class { id = "教育2", name = "教育2" });
                    break;
                case 2:
                    b.Add(new Models.Class { id = "计算机1", name = "计算机1" });
                    b.Add(new Models.Class { id = "计算机2", name = "计算机2" });
                    break;
                case 3:
                    b.Add(new Models.Class { id = "生命1", name = "生命1" });
                    b.Add(new Models.Class { id = "生命2", name = "生命2" });
                    break;
                case 4:
                    b.Add(new Models.Class { id = "外语1", name = "外语1" });
                    b.Add(new Models.Class { id = "外语2", name = "外语2" });
                    break;
                case 5:
                    b.Add(new Models.Class { id = "建筑1", name = "建筑1" });
                    b.Add(new Models.Class { id = "建筑2", name = "建筑2" });
                    break;
                case 6:
                    b.Add(new Models.Class { id = "马克思1", name = "马克思1" });
                    b.Add(new Models.Class { id = "马克思2", name = "马克思2" });
                    break;
            }
            b.ToArray();
            return Json(b, JsonRequestBehavior.AllowGet);
        }
        public string Delete(int Id)
        {
            string result="";
            if (m_bll.IsExists(Id))
            {
                if (m_bll.Delete(Id))
                {
                    result = "success";
                }
                else
                {
                    result = "error";
                }
            }
            
            return result;

        }
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            /* if (Id == null)
             {
                 return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
             }*/
            /* Student s = db.Students.Find(Id);
             if (s == null)
             {
                 return HttpNotFound();
             }*/
            Student s= null;
            if (m_bll.IsExists(Id))
            {
                s = m_bll.GetById(Id);

            }
            return View(s);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Edit([Bind(Include = "Name,Id,Myclass,Chinesescore,Mathscore,Englishscore,Computerscore")] Student s)
        {
            if (ModelState.IsValid)
            {
                /*db.Entry(s).State = EntityState.Modified;//用Modified修改整个对象，如果是修改其中的一个值，状态应为应该为UnChange然后在传进去一个ID,进而改对应的值
                db.SaveChanges();
                return RedirectToAction("Index");*/
                if (m_bll.IsExists(s.Id))
                {
                    if (m_bll.Edit(s))
                    {
                        return Json(1, JsonRequestBehavior.AllowGet);
                    }
                    
                       
                    

                }

            }
            return Json(0, JsonRequestBehavior.AllowGet);
        }


    }
         
      
       
    
}