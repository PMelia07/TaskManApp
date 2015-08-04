using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TaskManApp.Models;

namespace TaskManApp.Controllers
{
    public class TaskController : Controller
    {
        private TaskManDb db = new TaskManDb();

        // GET: Home/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // GET: Tasks/Create
        [HttpGet]
        public ActionResult Create(int tlistid)
        {
            TaskVM tvm = new TaskVM { TaskListID = tlistid };
            //tvm.StartDate = DateTime.Today;
            //tvm.FinishDate = DateTime.Today;
            tvm.StartDate = DateTime.Now;
            tvm.FinishDate = DateTime.Now;
            return View(tvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TaskVM model, int tlistid)
        {
            if (ModelState.IsValid)
            {
                if (model.StartDate.Date < DateTime.Today)
                {
                    ModelState.AddModelError("StartDate", "Start Date must be today or later.");
                    
                    if (model.FinishDate.Date < DateTime.Today)
                    {
                        ModelState.AddModelError("FinishDate", "Fininsh Date must be today or later.");
                    }
                }
                else if (model.FinishDate.Date < DateTime.Today)
                {
                    ModelState.AddModelError("FinishDate", "Finish Date must be today or later.");
                }
                else
                {
                    Task Newtask = new Task();
                    Newtask.TaskName = model.TaskName;
                    Newtask.StartDate = model.StartDate;
                    Newtask.FinishDate = model.FinishDate;
                    Newtask.TaskListID = model.TaskListID = tlistid;
                    db.Tasks.Add(Newtask);
                    db.SaveChanges();
                    return RedirectToAction("Details", "Home", new { id = model.TaskListID });
                }
            }
            TaskVM tvm = new TaskVM { TaskListID = tlistid };
            tvm.StartDate = DateTime.Today;
            tvm.FinishDate = DateTime.Today;
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            Task task = db.Tasks.Single(x => x.ID == id);

            if (task == null)
            {
                return RedirectToAction("Create");
            }

            TaskVM taskvm = new TaskVM
            {
                ID = task.ID,
                TaskName = task.TaskName,
                StartDate = task.StartDate,
                FinishDate = task.FinishDate,
                TaskListID = task.TaskListID,
            };
            return View(taskvm);

        }

        [HttpPost]
        public ActionResult Edit(TaskVM model)
        {
            if (ModelState.IsValid)
            {
                if (model.StartDate.Date < DateTime.Today)
                {
                    ModelState.AddModelError("StartDate", "Start Date must be today or later.");

                    if (model.FinishDate.Date < DateTime.Today)
                    {
                        ModelState.AddModelError("FinishDate", "Fininsh Date must be today or later.");
                    }
                }
                else if (model.FinishDate.Date < DateTime.Today)
                {
                    ModelState.AddModelError("FinishDate", "Finish Date must be today or later.");
                }
                else
                {
                    Task task = db.Tasks.Single(x => x.ID == model.ID);
                    task.ID = model.ID;
                    task.TaskName = model.TaskName;
                    task.StartDate = model.StartDate;
                    task.FinishDate = model.FinishDate;
                    task.TaskListID = model.TaskListID;
                    db.Entry(task).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Details", "Home", new { id = model.TaskListID });
                }
            }

            return View(model);
        }


        // GET: Home/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // POST: Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Task task = db.Tasks.Find(id);
            db.Tasks.Remove(task);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
