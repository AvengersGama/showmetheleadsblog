using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Gama.GrupoAvengers.Blog;
using System.Net.Mail;

namespace Gama.GrupoAvengers.Blog.Controllers
{
    [RoutePrefix("blog")]
    public class BlogLeadsController : Controller
    {
        private avengersblogEntities db = new avengersblogEntities();

        // GET: BlogLeads
        public ActionResult Index()
        {
            return View(db.BlogLeads.ToList());
        }

        public ActionResult Thankyou()
        {
            return RedirectToAction("Index", "Home");
            //return View();

        }


        // GET: BlogLeads/Create
        [Route("ganhar-ebook")]
        public ActionResult Create()
        {
            return View();
        }


        // POST: BlogLeads/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("ganhar-ebook")]
        public ActionResult Create([Bind(Include = "Name,Lastname,Email,LeadType,Company,Occupation")] BlogLead blogLead)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("pagueway@gmail.com");
                mail.To.Add(blogLead.Email);
                mail.Subject = "Download E-Book";
                mail.Body = "<h3>Olá, Tudo bem?</h3><br><p>Seu E-Book está disponivel no link abaixo.</p>" +
                    "<br> <a href='http://pagueway.com.br/Assets/Como_Montar_Seu_Ecommerce-Ebook.pdf'>Download E-Book</a>" +
                    "<br> Equipe Pagueway";
                mail.IsBodyHtml = true;
                //mail.Attachments.Add(new Attachment("~/Assets/Como_Montar_Seu_Ecommerce-Ebook.pdf"));

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("pagueway@gmail.com", "gama123456");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }


            switch (blogLead.LeadType)
            {
                case "1":
                    blogLead.LeadIs = "Empreendedor e já possui uma loja virtual";
                    blogLead.LeadType = "b2b";
                    break;
                case "2":
                    blogLead.LeadIs = "Empreendedor e deseja vender seus produtos/serviços na internet";
                    blogLead.LeadType = "b2b";

                    break;
                case "3":
                    blogLead.LeadIs = "Empreendedor e deseja ampliar sua conversão de vendas e reduzir custos";
                    blogLead.LeadType = "b2b";

                    break;
                case "4":
                    blogLead.LeadIs = "Não empreendedor e deseja apenas ter conhecimento sobre e-commerce";
                    blogLead.LeadType = "b2c";

                    break;
                default:
                    break;
            }

            blogLead.ClientIP = Request.UserHostAddress;
            blogLead.RegistrationDate = DateTime.UtcNow.AddHours(-3).ToString("yyyy-MM-dd HH:mm:ss");


            if (ModelState.IsValid)
            {
                db.BlogLeads.Add(blogLead);
                db.SaveChanges();
                return RedirectToAction("Thankyou", "BlogLeads");
            }

            return View();
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
