using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Gama.GrupoAvengers.Blog;

namespace Gama.GrupoAvengers.Blog.Controllers
{
    //[RoutePrefix("blog")]
    public class BlogLeadsController : Controller
    {
        private avengersblogEntities db = new avengersblogEntities();

        // GET: BlogLeads
        public ActionResult Index()
        {
            return View(db.BlogLeads.ToList());
        }


        // GET: BlogLeads/Create
        //[Route("ganhar-ebook")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: BlogLeads/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Email,Company,Lastname")] BlogLead blogLead)
        {

            blogLead.ClientIP = Request.UserHostAddress;
            blogLead.RegistrationDate = DateTime.UtcNow.AddHours(-3).ToString("yyyy-MM-dd HH:mm:ss");



            if (ModelState.IsValid)
            {
                if(blogLead.Lastname == null) // TODO: TROCAR FULLB
                {
                    blogLead.Lastname = "Não Informado";
                }

                db.BlogLeads.Add(blogLead);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(blogLead);
        }

        // GET: BlogLeads/Edit/5
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

        // POST: BlogLeads/Edit/5
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

        // GET: BlogLeads/Delete/5
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

        // POST: BlogLeads/Delete/5
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
    }
}
