﻿using System;
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
        //méthode affichant la liste des commandes effectuées
        public ActionResult Index()
        {
            DataTable mesCommandes = null;
            try
            {
                unS = Service.GetInstance();
                mesCommandes = unS.ListClientsParVendeur();
            }
            catch (MonException e)
            {
                ModelState.AddModelError("Erreur", "Erreur lors de la récupération des clients : " + e.Message);
                return View("Error");
            }

            return View(mesCommandes);
        }

        //affiche le détail d'une commande sélectionnée au clic
        public ActionResult Detail(String id)
        {
            DetailCde detailCde = new DetailCde();
            try
            {
                unS = Service.GetInstance();
                detailCde = unS.GetDetailCommande(id);
                return View(detailCde);
            }catch (MonException e)
            {
                return HttpNotFound();
            }
        }

        //affiche la page de modification d'une commande avec les données associées au numéro de commande en paramètre
        public ActionResult Modifier(string id)
        {
            CommerceViewModel cvm = new CommerceViewModel();
            try
            {
                unS = Service.GetInstance();
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

        //Action Result appelée pour la modification d'une commande en base de données
        [HttpPost]
        public ActionResult Modifier()
        {
            try
            {
                // utilisation possible de Request
                // String s= Request["SOCIETE"];
                //  String v = Request["VILLE_CL"];

                //récupération des données modifiées par l'utilisateur puis mise à jour en base de données
                commandes uneC = new commandes();
                CommerceViewModel commerceView = new CommerceViewModel();
                uneC.FACTURE = Request["optradio"];
                uneC.NO_CLIENT = Request["liste_client"];
                uneC.NO_COMMAND = Request["NO_COMMAND"];
                uneC.NO_VENDEUR = Request["liste_vendeur"];
                uneC.DATE_CDE = Convert.ToDateTime(Request["DATE_COMMANDE"]);
                unS = Service.GetInstance();
                unS.ModifierCommande(uneC);
                commerceView.cs = uneC;
                commerceView.lesClients = unS.ListClients();
                commerceView.lesVendeurs = unS.ListVendeurs();
                TempData["update"] = true;
                return View(commerceView);
            }
            catch (MonException e)
            {
                return HttpNotFound();
            }
        }

        //Affiche la page contenant le formulaire pour ajouter une commande
        public ActionResult Ajouter()
        {
            CommerceViewModel cvm = new CommerceViewModel();
            try
            {
                unS = Service.GetInstance();
                cvm.articles = unS.ListArticles();
                cvm.lesVendeurs = unS.ListVendeurs();
                cvm.lesClients = unS.ListClients();
                return View(cvm);
            }
            catch (MonException e)
            {
                return HttpNotFound();
            }
        }

        //méthode qui permet d'ajouter une commande en récupérant les données du formulaire
        [HttpPost]
        public ActionResult Ajouter(bool? error)
        {
            commandes newCommande = new commandes();
            try
            {
                newCommande.NO_CLIENT = Request["liste_client"];
                newCommande.NO_VENDEUR = Request["liste_vendeur"];
                newCommande.NO_COMMAND = Request["noCommand"];
                newCommande.FACTURE = Request["optradio"];
                newCommande.DATE_CDE = Convert.ToDateTime(Request["DATE_COMMANDE"]);
                unS = Service.GetInstance();
                unS.AjouterCommande(newCommande);
                TempData["addCommand"] = true;
                return RedirectToAction("Index","Commande");
            }
            catch(MonException e)
            {
                error = true;
                return HttpNotFound();
            }
        }

        //méthode qui permet la suppression d'une commande avec l'id commande en paramètre
        public ActionResult Supprimer(String id)
        {
            try
            {
                unS = Service.GetInstance();
                unS.SupprimerCommande(id);
                TempData["success"] = "Suppression effectuée pour la commande " + id;
                return RedirectToAction("Index", "Commande");
            }
            catch (Exception)
            {
                return HttpNotFound();
            }
        }
        
        //méthode qui va traiter les données lorsque l'url /Commande/Rechercher est appelée (après envoi du formulaire)
        [HttpPost]
        public ActionResult Rechercher(String date_debut,String date_fin)
        {
            try
            {
                DataTable dt = new DataTable();
                unS = Service.GetInstance();
                dt = unS.RechercheCommandesSurPeriode(date_debut, date_fin);
                return Rechercher(dt,date_debut,date_fin);
            }
            catch (Exception)
            {
                return HttpNotFound();
            }
        }

        //permet de rechercher des commandes situées entre 2 dates (ddeb : date début et dfin : date fin)
        public ActionResult Rechercher(DataTable dt,String ddeb, String dfin)
        {
            if(ddeb != null && dfin != null) { 
            if (dt.Rows.Count == 0)
            {
                TempData["rechercheVide"] = true;
                TempData["recherche"] = true;
            }
            else if (dt.Rows.Count > 0)
            {
                TempData["rechercheVide"] = false;
                TempData["recherche"] = true;
            }
            TempData["dateDeb"] = ddeb;
            TempData["dateFin"] = dfin;
            }
            else
            {
                TempData["rechercheVide"] = false;
                TempData["recherche"] = false;
            }
            return View(dt);
        }
    }
}