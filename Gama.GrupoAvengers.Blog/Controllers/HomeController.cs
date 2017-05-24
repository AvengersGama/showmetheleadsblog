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
        public ActionResult Index([Bind(Include = "Name,Lastname,Email,LeadType,Company,Occupation")] BlogLead blogLead)
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