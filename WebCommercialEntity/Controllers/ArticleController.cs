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
        // récupère la liste des articles
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

        //vue partielle retournant les listes des articles en vue de la mise à jour des prix
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

        //méthode appelée lors de la mise à jour des prix via requête Ajax
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

        //Affiche la vue permettant d'augmenter le prix des articles via un pourcentage
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

        
    }
}