using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gama.GrupoAvengers.Blog.Models
{
    public class Leads
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string ClientIP { get; set; }
        public string Type { get; set; }
        public string RegistrationDate { get; set; }

    }
}