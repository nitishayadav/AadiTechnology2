using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using LoginTask.Models;


namespace LoginTask.Controllers
{
    public class HomeController : Controller
    {
        Repository repository = null;
        public HomeController()
        {
            repository = new Repository();
        }
        public ActionResult Index()
        {
            var myKey = (UserModel)TempData.Peek("mykey");

            return View(myKey);
        }
        public ActionResult GetProfile(int id)
        {
            var result = repository.GetUserProfile(id);
            return View(result);
        }
        public ActionResult GetCourseDetail()
        {
            var result = repository.CourseDetails();
            return View(result);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CourseModel model)
        {
            if (ModelState.IsValid)
            {
                int id = repository.AddCourse(model);
                if (id > 0)
                {
                    ModelState.Clear();
                    // ViewBag.Issuccess = "Data Added";
                    return RedirectToAction("GetCourseDetail", "Home");
                }
            }

            return View();
        }

        public ActionResult EditCourse(int id)
        {
            var course = repository.GetCourseName(id);
            return View(course);
        }
        [HttpPost]
        public ActionResult EditCourse(CourseModel model)
        {
            if (ModelState.IsValid)
            {
                repository.UpdateCourse(model.Id, model);
                return RedirectToAction("GetCourseDetail");
            }
            return View();

        }

        public ActionResult Delete(int id)
        {
            repository.DeleteCourse(id);
            return RedirectToAction("GetCourseDetail");
        }

        public ActionResult CreateTopic()
        {
            var result = repository.GetCourseName();
            ViewBag.data = result;
            return View();
        }
        [HttpPost]
        public ActionResult CreateTopic(TopicModel model)
        {
            if (ModelState.IsValid)
            {
                var content = repository.AddTopic(model);
                return RedirectToAction("TopicList");

            }
            return View();
        }

        public ActionResult TopicList()
        {
            var result = repository.TopicDetailsList();
            return View(result);
        }

        public ActionResult EditTopic(int topicId)
        {
            var topic = repository.GetTopicName(topicId);
            return View(topic);
        }
        [HttpPost]
        public ActionResult EditTopic(TopicModel model)
        {
            if (ModelState.IsValid)
            {
                repository.UpdateTopic(model.TopicId, model);
                return RedirectToAction("TopicList");
            }
            return View();

        }

        public ActionResult DeleteTopic(int id)
        {
            ViewBag.error = "Cannot Delete topic as this topic has its subtopic";
            repository.DeleteTopicName(id);
           
            return RedirectToAction("TopicList");
        }


        public ActionResult CreateTask()
        {
            var result = repository.GetCourseName();
            ViewBag.data = result;
            var topicName = repository.GetTopicName();
            ViewBag.Tname = topicName;
            var SubtopicName = repository.GetSubTopicName();
            ViewBag.SubTname = SubtopicName;
            return View();
        }
        [HttpPost]

        public ActionResult CreateTask(DailyTask1Model model)
        {
            if (ModelState.IsValid)
            {
                var content = repository.AddTask(model);
                return RedirectToAction("TaskList");

            }
            return View();
        }


        public ActionResult CreateSubTopic()
        {
            var course = repository.GetCourseName();
            ViewBag.content = course;
            var result = repository.GetTopicName();
            ViewBag.data = result;
            return View();
        }
        [HttpPost]

        public ActionResult CreateSubTopic(SubTopicModel model)
        {
            if (ModelState.IsValid)
            {
                var content = repository.AddSubTopic(model);
                return RedirectToAction("SubTopicList");

            }
            return View();
        }
        public ActionResult SubTopicList()
        {
            var result = repository.SubTopicDetailsList();
            return View(result);
        }
        public ActionResult DeleteSubTopic(int id)
        {
            repository.DeleteSubTopicName(id);
            return RedirectToAction("SubTopicList");
        }

        public ActionResult EditSubTopic(int SubtopicId)
        {
            var Subtopic = repository.GetSubTopicName(SubtopicId);
            return View(Subtopic);
        }
        [HttpPost]
        public ActionResult EditSubTopic(SubTopicModel model)
        {
            if (ModelState.IsValid)
            {
                repository.UpdateSubTopic(model.Id, model);
                return RedirectToAction("SubTopicList");
            }
            return View();

        }

        public ActionResult EditTask(int id)
        {
            var task = repository.GetTaskName(id);
            return View(task);
        }
        [HttpPost]

        public ActionResult EditTask(DailyTask1Model model)
        {
            if (ModelState.IsValid)
            {
                repository.UpdateTask(model.Id, model);
                return RedirectToAction("TaskList");
            }
            return View();

        }
        public JsonResult TopicListByCourseId(int CourseId)
        {
            var list = repository.GetTopicListByCourseId(CourseId);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SubTopicListByTopicId(int TopicId)
        {
            var list = repository.GetSubTopicListByTopicId(TopicId);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCustomerDetail()
        {
            var result = repository.CustomerDetails();
            return View(result);
        }
        public ActionResult GetManufacturerDetail()
        {
            var result = repository.manufacturerDetails();
            return View(result);
        }
        public ActionResult GetOwnerDetail()
        {
            var result = repository.ownerDetails();
            return View(result);
        }

        public ActionResult ExcelView()
        {
            return View(this.repository.TaskDetailsList());
        }
        public ActionResult ExportToExcel()
        {
            var gv = new GridView();
            gv.DataSource = this.repository.TaskDetailsList();
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=DemoExcel2.xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter objStringWriter = new StringWriter();
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
            gv.RenderControl(objHtmlTextWriter);
            Response.Output.Write(objStringWriter.ToString());
            Response.Flush();
            Response.End();
            return View("ExcelView");
        }

        public ActionResult TaskList()
        {
            var result = repository.TaskDetailsList();
            return View(result);
        }


        [HttpPost]
         public JsonResult NewChart()
          {
            var result = repository.CourseChartQuery();
            //List<object> iData = new List<object>();
            ////Creating sample data    
            //DataTable dt = new DataTable();
            //dt.Columns.Add("Course_Name", System.Type.GetType("System.String"));
            //dt.Columns.Add("Count", System.Type.GetType("System.Int32"));

            //DataRow dr = dt.NewRow();
            //dr["Course_Name"] = CourseName;
            //dr["Count"] = count;
            //dt.Rows.Add(dr);
            //foreach (DataColumn dc in dt.Columns)
            //{
            //    List<object> x = new List<object>();
            //    x = (from DataRow drr in dt.Rows select drr[dc.ColumnName]).ToList();
            //    iData.Add(x);
            //}
            //ViewBag.ChartData = iData;
            //Source data returned as JSON    
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult KendoUIGrid()
        {
            return View();
        }

        //public ActionResult ChartView()
        //{
        //    return View();
        //}
    }
}