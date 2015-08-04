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
    public class HomeController : Controller
    {
        private TaskManDb db = new TaskManDb();

        // GET: Home
        public ActionResult Index()
        {
            List<TaskList> TempStore = db.TaskLists.ToList();

            if (TempStore.Count != 0)
            {
                return View(TempStore.OrderBy(task => task.Name).ToList());
            }
            else
            {
                return RedirectToAction("Create", "Home");
            }
        }

        // GET: Home/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TaskList tList = db.TaskLists.Single(x => x.ID == id);
            //TaskList taskList = db.TaskLists.Find(id);
            if (tList == null)
            {
                return HttpNotFound();
            }
            else
            {
                tList.Task = (from e in db.Tasks
                                 where e.TaskListID.Equals(id)
                                 select e).ToList();
            }
            return View(tList);
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] TaskList tlist)
        {
            if (ModelState.IsValid)
            {
                db.TaskLists.Add(tlist);
                db.SaveChanges();
                return RedirectToAction("Details", new { tlist.ID });
            }

            return View(tlist);
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskList taskList = db.TaskLists.Find(id);
            if (taskList == null)
            {
                return HttpNotFound();
            }
            return View(taskList);
        }

        // POST: Home/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] TaskList taskList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taskList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(taskList);
        }

        // GET: Home/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskList taskList = db.TaskLists.Find(id);
            if (taskList == null)
            {
                return HttpNotFound();
            }
            return View(taskList);
        }

        // POST: Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TaskList taskList = db.TaskLists.Find(id);
            db.TaskLists.Remove(taskList);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
