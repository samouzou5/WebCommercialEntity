using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WebCommercialEntity.Models.MesExceptions;


namespace WebCommercialEntity.Models
{
    public class Service

    {
        

        private static Service instance;
        private static CommercialEntities unCommercial;
        /// <summary>
        /// Obtenir le singleton et le créer s'il n'existe pas
        /// </summary>
        public static Service GetInstance()
        {
            if (Service.instance == null)
            {
                Service.instance = new Service();
                // on définit un contexte commun à toutes les requêtes
                unCommercial = new CommercialEntities();
            }
            return Service.instance;
        }
        // on rend le constructeur privé
        private Service()
        { }

        public commandes RechercheUneCommande(string id)
        {
            Serreurs er = new Serreurs("Erreur sur recherche d'une commande.",
                "Commandes.RechercheUneCommande()");
            commandes uneCommande;
            try
            {
                uneCommande = (from c in unCommercial.commandes
                            where c.NO_COMMAND == id
                            select c).FirstOrDefault();
                return uneCommande;
            }
            catch (Exception e)
            {
                throw new MonException(er.MessageUtilisateur(),
                    er.MessageApplication(), e.Message);
            }
        }




        ///
        ///  Classe Clientel
        /// <summary>
        /// Lire un utilisateur sur son ID
        /// </summary>
        /// <param name="numCli">N° de l'utilisateur à lire</param>
        /// 

        //recherche un utilisateur
        public utilisateur RechercheUnUtilisateur(String login)
        {
            Serreurs er = new Serreurs("Erreur sur la recherche d'un utilisateur.", "Utilisateur.RechercheUnUtilisateur()");
            utilisateur unUtilisateur;
            try
            {
                unUtilisateur = (from u in unCommercial.utilisateur
                                 where u.idLogin == login
                                 select u).FirstOrDefault();
                return unUtilisateur;
            }
            catch (Exception e)
            {
                throw new MonException(er.MessageUtilisateur(),
                    er.MessageApplication(), e.Message);
            }
        }
        public clientel RechercheUnClient(String numCli)
        {

            Serreurs er = new Serreurs("Erreur sur recherche d'un client.",
                "Client.RechercheUnClient()");
            clientel unclient;
            try
            {
                unclient = (from c in unCommercial.clientel
                            where c.NO_CLIENT == numCli
                            select c).FirstOrDefault();
                return unclient;
            }
            catch (Exception e)
            {
                throw new MonException(er.MessageUtilisateur(),
                    er.MessageApplication(), e.Message);
            }

        }

        /// <summary>
        /// Lister les clients de la base
        /// </summary>
        /// <returns>Liste de numéros de clients</returns>
        public List<String> LectureNoClient()
        {

            Serreurs er = new Serreurs("Erreur sur lecture du client.",
                 "Clientel.LectureNoClient()");
            try
            {
                var mesNumeros = (from c in unCommercial.clientel
                                  select c.NO_CLIENT).Distinct();
                return mesNumeros.ToList<String>();
            }
            catch (Exception e)
            {
                throw new MonException(er.MessageUtilisateur(),
                    er.MessageApplication(), e.Message);
            }
        }

        /// <summary>
        /// Lister les clients de la base
        /// </summary>
        /// <returns>Liste des clients</returns>
        public List<clientel> ListClients()
        {

            Serreurs er = new Serreurs("Erreur sur lecture des clients.",
                 "Clientel.ListeClients()");
            try
            {
                var mesClients = (from c in unCommercial.clientel
                                   orderby c.NOM_CL
                                  select c);
                return mesClients.ToList<clientel>();
            }
            catch (Exception e)
            {
                throw new MonException(er.MessageUtilisateur(),
                    er.MessageApplication(), e.Message);
            }
        }

        public List<vendeur> ListVendeurs()
        {
            Serreurs er = new Serreurs("Erreur sur lecture des vendeurs.",
                 "Vendeur.ListVendeurs()");
            try
            {
                var mesVendeurs = (from v in unCommercial.vendeur
                                  orderby v.NO_VENDEUR
                                  select v);
                return mesVendeurs.ToList<vendeur>();
            }
            catch (Exception e)
            {
                throw new MonException(er.MessageUtilisateur(),
                    er.MessageApplication(), e.Message);
            }
        }

        public List<articles> ListArticles()
        {
            Serreurs er = new Serreurs("Erreur sur lecture des articles.",
                 "Articles.ListArticles()");
            try
            {
                var mesArticles = (from a in unCommercial.articles
                                   orderby a.NO_ARTICLE
                                   select a);
                return mesArticles.ToList<articles>();
            }
            catch (Exception e)
            {
                throw new MonException(er.MessageUtilisateur(),
                    er.MessageApplication(), e.Message);
            }
        }

        /// <summary>
        /// Modifier un clent de la base
        /// </summary>
        /// <returns>modifier Client</returns>
        public void ModifierClient(clientel unC )
        {

            Serreurs er = new Serreurs("Erreur sur la modification d'un client",
                 "modifierClient");
            // requête de sélection pour une mise à jour .

            var unclient = from c in unCommercial.clientel
                           where c.NO_CLIENT == unC.NO_CLIENT
                           select c;

            // On modifie les données 
            foreach (clientel  ligne  in unclient)
            {
                ligne.NOM_CL = unC.NOM_CL;
                ligne.PRENOM_CL = unC.PRENOM_CL;
                ligne.SOCIETE = unC.SOCIETE;
                ligne.ADRESSE_CL = unC.ADRESSE_CL;
                ligne.CODE_POST_CL = unC.CODE_POST_CL;
                ligne.VILLE_CL = unC.VILLE_CL;
            }

            // On enregistre les modifications dans la base de données .
            try
            {
                unCommercial.SaveChanges();
            }
            catch (Exception e)
            {
                throw new MonException(er.MessageUtilisateur(),
                    er.MessageApplication(), e.Message);
            }
        }


        /// <summary>
        /// Lister les commandes  par Vendeur de la base
        /// </summary>
        /// <returns>Liste de commandes</returns>
        public DataTable ListClientsParVendeur()
        {

            DataTable dt = new DataTable();
            Serreurs er = new Serreurs("Erreur sur lecture du client.",
                 "Clientel.LectureNoClient()");
            try

            {
                dt.Columns.Add("NO_COMMAND", typeof(String));
                dt.Columns.Add("DATE_CDE", typeof(DateTime));
                dt.Columns.Add("NOM_CL", typeof(String));
                dt.Columns.Add("SOCIETE", typeof(String));
                dt.Columns.Add("VILLE_CL", typeof(String));
                dt.Columns.Add("NOM_VEND", typeof(String));


                var req =  from c  in unCommercial.commandes
                           join cl in unCommercial.clientel on c.NO_CLIENT equals cl.NO_CLIENT
                           join  ve in unCommercial.vendeur  on c.NO_VENDEUR equals ve.NO_VENDEUR
                           select new { c.NO_COMMAND,c.DATE_CDE,cl.NOM_CL,cl.SOCIETE,cl.VILLE_CL,ve.NOM_VEND };
                foreach (var res in req)
                {
                    
                    dt.Rows.Add(res.NO_COMMAND, res.DATE_CDE, res.NOM_CL, res.SOCIETE, res.VILLE_CL, res.NOM_VEND);
                }
                return dt;
            }
            catch (Exception e)
            {
                throw new MonException(er.MessageUtilisateur(),
                    er.MessageApplication(), e.Message);
            }
        }

        public void ModifierCommande(commandes uneC)
        {

            Serreurs er = new Serreurs("Erreur sur la modification d'une commande",
                 "modifierCommande");
            // requête de sélection pour une mise à jour .

            var unecommande = from v in unCommercial.commandes
                           where v.NO_COMMAND == uneC.NO_COMMAND
                           select v;

            // On modifie les données 
            foreach (commandes ligne in unecommande)
            {
                ligne.NO_COMMAND = uneC.NO_COMMAND;
                ligne.DATE_CDE = uneC.DATE_CDE;
                ligne.NO_CLIENT = uneC.NO_CLIENT;
                ligne.NO_VENDEUR = uneC.NO_VENDEUR;
                ligne.FACTURE = uneC.FACTURE;
            }

            // On enregistre les modifications dans la base de données .
            try
            {
                unCommercial.SaveChanges();
            }
            catch (Exception e)
            {
                throw new MonException(er.MessageUtilisateur(),
                    er.MessageApplication(), e.Message);
            }
        }

        //permet de récupérer tous les articles associés à une commande donnée
        public DetailCde GetDetailCommande(String no_cmd)
        {
            Serreurs er = new Serreurs("Erreur sur le détail d'une commande",
                 "Commande.getDetailCommande()");
            DetailCde dc = new DetailCde();
            commandes comm = new commandes();
            try
            {
                comm = this.RechercheUneCommande(no_cmd);
                dc.DetailCdes = (from a in unCommercial.articles
                                  from de in unCommercial.detail_cde
                                  where a.NO_ARTICLE == de.NO_ARTICLE
                                 && de.NO_COMMAND == no_cmd
                                  select new DetailCde
                                  {
                                      Art = a,
                                      Qte_cdee = (int)de.QTE_CDEE,
                                      Livree = de.LIVREE,
                                      Total = (Double)(de.QTE_CDEE * a.PRIX_ART),
                                  }).ToList();
                dc.Com = comm;
                return dc; 
            }

            catch (MonException e)
            {
                throw new MonException(er.MessageUtilisateur(),
                    er.MessageApplication(), e.Message);
            }
        }

        public void AjouterCommande(commandes c)
        {
            Serreurs er = new Serreurs("Erreur sur l'ajout d'une commande",
                 "Commande.AjoutCommande()");
            try
            { 
                unCommercial.commandes.Add(c);
                unCommercial.SaveChanges();
            }
            catch (MonException e)
            {
                throw new MonException(er.MessageUtilisateur(),
                    er.MessageApplication(), e.Message);
            }
        }

        public void SupprimerCommande(String no_cmd)
        {
            Serreurs er = new Serreurs("Erreur sur la suppression de commande.", "Commande.SupprimerCommande()");
            commandes uneCde;
            try
            {

                uneCde = (from c in unCommercial.commandes
                          where c.NO_COMMAND == no_cmd
                          select c).FirstOrDefault();
                unCommercial.commandes.Remove(uneCde);
                unCommercial.SaveChanges();
            }
            catch (Exception e)
            {
                throw new MonException(e.Message, "requête suppression commande", e.Message);
            }

        }


    }
}