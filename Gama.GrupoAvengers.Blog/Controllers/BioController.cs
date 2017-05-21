using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gama.GrupoAvengers.Blog.Controllers
{
    public class BioController : Controller
    {
        // GET: Bio
        public ActionResult Biography()
        {
            return View();
        }
    }
}