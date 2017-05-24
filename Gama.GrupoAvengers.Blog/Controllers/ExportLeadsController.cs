using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Gama.GrupoAvengers.Blog;
using System.Text;
using System.IO;
using Gama.GrupoAvengers.Blog.Models;
using System.Globalization;

namespace Gama.GrupoAvengers.Blog.Controllers
{
    public class ExportLeadsController : Controller
    {
        private avengersblogEntities db = new avengersblogEntities();

        // GET: ExportLeads
        public ActionResult Index()
        {
            return View(db.BlogLeads.ToList());
        }

        // GET: ExportLeads/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogLead blogLead = db.BlogLeads.Find(id);
            if (blogLead == null)
            {
                return HttpNotFound();
            }
            return View(blogLead);
        }

        // GET: ExportLeads/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ExportLeads/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Lastname,Email,ClientIP,RegistrationDate")] BlogLead blogLead)
        {
            if (ModelState.IsValid)
            {
                db.BlogLeads.Add(blogLead);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(blogLead);
        }

        // GET: ExportLeads/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogLead blogLead = db.BlogLeads.Find(id);
            if (blogLead == null)
            {
                return HttpNotFound();
            }
            return View(blogLead);
        }

        // POST: ExportLeads/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Lastname,Email,ClientIP,RegistrationDate")] BlogLead blogLead)
        {
            if (ModelState.IsValid)
            {
                db.Entry(blogLead).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blogLead);
        }

        // GET: ExportLeads/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogLead blogLead = db.BlogLeads.Find(id);
            if (blogLead == null)
            {
                return HttpNotFound();
            }
            return View(blogLead);
        }

        // POST: ExportLeads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BlogLead blogLead = db.BlogLeads.Find(id);
            db.BlogLeads.Remove(blogLead);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        public ActionResult All()
        {
            try
            {
                var sb = new StringBuilder();
                sb.AppendLine("email,nome,ip,tipo,data_hora");

                var leads = db.BlogLeads
                    .Select(x => new { Line = x.Email + "," +
                    x.Name + "," +
                    x.ClientIP + "," +
                    x.LeadType + "," +
                    x.RegistrationDate
                    })
                    .ToList();


                foreach (var lead in leads)
                {
                    sb.AppendLine(lead.Line);
                }

                var byteArray = Encoding.UTF8.GetBytes(sb.ToString());
                var stream = new MemoryStream(byteArray);


                return File(stream, "text/plain", "Export - " + DateTime.UtcNow.AddHours(-3) + ".csv");
            }
            catch (Exception ex)
            {

            }

            return null;
        }


    }
}
