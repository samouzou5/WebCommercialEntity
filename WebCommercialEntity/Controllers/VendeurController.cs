using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCommercialEntity.Models;

namespace WebCommercialEntity.Controllers
{
    public class VendeurController : Controller
    {
        private Service unS;

        // GET: Vendeur
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult InfoVendeurConnecte()
        {
            try
            {
                unS = Service.GetInstance();
                vendeur infoVendeurLogue = unS.RechercheUnVendeur((string)Session["login"]);
                return View(infoVendeurLogue);
            }
            catch (Exception)
            {
                return HttpNotFound();
            }
        }
    }
}