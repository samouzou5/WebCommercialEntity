﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCommercialEntity.Models;
using WebCommercialEntity.Models.MesExceptions;

namespace WebCommercialEntity.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index(bool? error)
        {
            if (error == true)
            {
                TempData["error"] = true;
                    
            }
            return View();
        }

        [HttpPost]
        public ActionResult CheckUser(String username,String password)
        {
            utilisateur u = new utilisateur();
            try
            {
                Service unS = Service.getInstance();
                u = unS.RechercheUnUtilisateur(username);
                if(u == null)
                {
                    return RedirectToAction("Index", new { @error = true });
                }
                if(u.motDePasse == password)
                {
                    Session["login"] = username;
                    Session["nom"] = u.nom;
                    Session["prenom"] = u.prenom;
                    return RedirectToAction("Index","Commande");
                }
                else
                {
                    return RedirectToAction("Index", new { @error = true });
                }

            }
            catch (MonException e)
            {
                ModelState.AddModelError("Erreur", "Erreur lors de la récupération des clients : " + e.Message);
                return View("Error");
            }
        }

        public ActionResult Success()
        {
            return View();
        }

        public ActionResult Logoff()
        {
            Session["login"] = null;
            Session["nom"] = null;
            Session["prenom"] = null;
            return RedirectToAction("Index", "Login");
        }
    }
}