using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCommercialEntity.Models;
using WebCommercialEntity.Models.MesExceptions;

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
        public ActionResult ListeArticlePartial(string value = "1")
        {
            try
            {
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

        //vue modifier un article
        public ActionResult Modifier(string id)
        {
            try
            {
                unS = Service.GetInstance();
                articles unCl = unS.RechercheUnArticle(id);
                return View(unCl);
            }
            catch (MonException e)
            {
                return HttpNotFound();

            }
        }

        [HttpPost]
        public ActionResult Modifier(articles a)
        {
            try
            {
                unS = Service.GetInstance();
                unS.ModifierArticle(a);
                return RedirectToAction("Index", "Article");
            }catch(MonException e)
            {
                return HttpNotFound();
            }
        }


    }
}