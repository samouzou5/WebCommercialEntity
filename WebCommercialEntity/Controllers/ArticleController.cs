using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCommercialEntity.Models;

namespace WebCommercialEntity.Controllers
{
    public class ArticleController : Controller
    {
        private Service unS;
        // GET: Article
        public ActionResult Index()
        {
            try
            {
                unS = Service.GetInstance();
                System.Data.DataTable lesArticles = unS.GetLesArticles();

                return View(lesArticles);
            }
            catch (Exception e)
            {

                return HttpNotFound();
            }
        }

        public ActionResult ListeArticlePartial()
        {
            try
            {
                unS = Service.GetInstance();
                System.Data.DataTable lesArticles = unS.GetLesArticles();

                return PartialView(lesArticles);
            }
            catch (Exception e)
            {

                return HttpNotFound();
            }
        }

        [HttpPost]
        public ActionResult ListeArticlePartial(string value)
        {
            try
            {
                string valeur = Request["pourcentage"];
                unS = Service.GetInstance();
                unS.ModifierPrixArticles(value);
                return Json(new { valeur = value });
            }
            catch (Exception)
            {
                return HttpNotFound();
            }

        }

        public ActionResult AugmenterArticle()
        {
            try
            {
                return View();
            }
            catch (Exception)
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        public ActionResult Augmenter()
        {
            try
            {
                string valeur = Request["pourcentage"];
                unS = Service.GetInstance();
                unS.ModifierPrixArticles(valeur);
                return PartialView();
            }
            catch (Exception)
            {
                return HttpNotFound();
            }
        }
    }
}