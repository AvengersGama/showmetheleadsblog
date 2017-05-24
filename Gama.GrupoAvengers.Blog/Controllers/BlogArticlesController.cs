using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gama.GrupoAvengers.Blog.Controllers
{
    [RoutePrefix("artigos")]
    public class BlogArticlesController : Controller
    {

        // GET: BlogArticles/Post1
        [Route("a-evolucao-das-ferramentas-de-e-commerce")]
        public ActionResult a_evolucao_das_ferramentas_de_e_commerce()
        {
            return View();
        }
        [Route("como-vender-os-produtos-da-sua-loja-pela-internet")]

        // GET: BlogArticles/Post2
        public ActionResult como_vender_os_produtos_da_sua_loja_pela_internet()
        {
            return View();
        }

        // GET: BlogArticles/Post3
        [Route("o-sucesso-dos-e-commerces-em-datas-especiais")]
        public ActionResult o_sucesso_dos_e_commerces_em_datas_especiais()
        {
            return View();
        }

        // GET: BlogArticles/Post4
        [Route("por-que-muitas-lojas-fisicas-ainda-nao-vendem-online")]
        public ActionResult por_que_muitas_lojas_fisicas_ainda_nao_vendem_online()
        {
            return View();
        }
        [Route("tendencias-do-e-commerce")]
        // GET: BlogArticles/Post5
        public ActionResult tendencias_do_e_commerce()
        {
            return View();
        }
        // GET: BlogArticles/Post6
        [Route("varejista-voce-ja-pensou-nos-beneficios-de-vender-em-ambiente-online")]
        public ActionResult varejista_voce_ja_pensou_nos_beneficios_de_vender_em_ambiente_online()
        {
            return View();
        }
        // GET: BlogArticles/Post7
        public ActionResult Post7()
        {
            return View();
        }
    }
}