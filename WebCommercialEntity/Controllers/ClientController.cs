using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCommercialEntity.Models.MesExceptions;
using WebCommercialEntity.Models;


namespace WebCommercialEntity.Controllers
{
    public class ClientController : Controller
    {
        private Service unS;
        // GET: Client
        public ActionResult Index()
        {
            IEnumerable<clientel> mesClients = null;
            try
            {
                Service unS = Service.getInstance();
                mesClients = unS.ListClients();
            }
            catch (MonException e)
            {
                ModelState.AddModelError("Erreur", "Erreur lors de la récupération des clients : " + e.Message);
                return View("Error");
            }

            return View(mesClients);
        }

        // GET: Commande/Edit/5
        public ActionResult Modifier(string id)
        {
            try
            {
                 unS = Service.getInstance();
                clientel unCl = unS.RechercheUnClient(id);
                return View(unCl);
            }
            catch (MonException e)
            {
                return HttpNotFound();

            }
        }

        [HttpPost]
        public ActionResult Modifier(clientel unC)
        {
            try
            {
                // utilisation possible de Request
               // String s= Request["SOCIETE"];
               //  String v = Request["VILLE_CL"];
                unS = Service.getInstance();
                unS.ModifierClient(unC);
                return View();
            }
            catch (MonException e)
            {
                return HttpNotFound();
            }
        }
    }
}