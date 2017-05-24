using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Gama.GrupoAvengers.Blog.Controllers
{
    public class HomeController : Controller
    {
        private avengersblogEntities db = new avengersblogEntities();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index([Bind(Include = "Name,Lastname,Email,LeadType,Company")] BlogLead blogLead)
        {

            //try
            //{
            //    SmtpClient client = new SmtpClient();
            //client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //client.EnableSsl = true;
            //client.Host = "smtp.gmail.com";
            //client.Port = 587;

            //// setup Smtp authentication
            //System.Net.NetworkCredential credentials =
            //    new System.Net.NetworkCredential("avengers10gama@gmail.com", "gama123456");
            //client.UseDefaultCredentials = false;
            //client.Credentials = credentials;

            //MailMessage msg = new MailMessage();
            //msg.From = new MailAddress("avengers10gama@gmail.com");
            //msg.To.Add(blogLead.Email);

            //msg.Subject = "E-Book - Blog Pagueway - Ecommerce";
            //msg.IsBodyHtml = true;
            //msg.Body = "\n\n  Olá,\n\nSegue abaixo o link para download. \n\n https://goo.gl/sxWXWf ";

            //    client.Send(msg);
               
            //}
            //catch (Exception ex)
            //{
            //    Response.Write("Could not send the e-mail - error: " + ex.Message);

            //}


            blogLead.ClientIP = Request.UserHostAddress;
            blogLead.RegistrationDate = DateTime.UtcNow.AddHours(-3).ToString("yyyy-MM-dd HH:mm:ss");
 

            if (ModelState.IsValid)
            {
                db.BlogLeads.Add(blogLead);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(blogLead);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}