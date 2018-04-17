using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCommercialEntity.Models.MesExceptions;
using WebCommercialEntity.Models;
using System.Data;
namespace WebCommercialEntity.Controllers
{
    public class CommandeController : Controller
    {
        private Service unS;
        // GET: Commande

        public ActionResult Index()
        {
            DataTable mesCommandes = null;
            try
            {
                unS = Service.getInstance();
                mesCommandes = unS.ListClientsParVendeur();
            }
            catch (MonException e)
            {
                ModelState.AddModelError("Erreur", "Erreur lors de la récupération des clients : " + e.Message);
                return View("Error");
            }

            return View(mesCommandes);
        }

        public ActionResult Modifier(string id)
        {
            CommerceViewModel cvm = new CommerceViewModel();
            try
            {
                unS = Service.getInstance();
                cvm.cs = unS.RechercheUneCommande(id);
                cvm.lesVendeurs = unS.ListVendeurs();
                cvm.lesClients = unS.ListClients();
                return View(cvm);
            }
            catch (MonException e)
            {
                return HttpNotFound();

            }
        }

        [HttpPost]
        public ActionResult Modifier()
        {
            try
            {
                // utilisation possible de Request
                // String s= Request["SOCIETE"];
                //  String v = Request["VILLE_CL"];
                commandes uneC = new commandes();
                uneC.FACTURE = Request["optradio"];
                uneC.NO_CLIENT = Request["liste_client"];
                uneC.NO_COMMAND = Request["NO_COMMAND"];
                uneC.NO_VENDEUR = Request["liste_vendeur"];
                uneC.DATE_CDE = Convert.ToDateTime(Request["DATE_COMMANDE"]);
                unS = Service.getInstance();
                unS.ModifierCommande(uneC);
                return View();
            }
            catch (MonException e)
            {
                return HttpNotFound();
            }
        }
    }
}