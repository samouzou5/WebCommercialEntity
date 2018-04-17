using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilitaires
{
    public class MonException : Exception
    {
        private string utilisateur;
        private string application;
        private string systeme;
        private string support = "Si l'erreur persiste, relevez les messages ci-dessus et prenez contact avec le support technique.";
        public MonException()
        {
            utilisateur = application = systeme = "";
        }
        public string Utilisateur
        {
            get { return utilisateur; }
            set { utilisateur = value + "\r\n"; }
        }

        public string Application
        { 
            get { return application; }
            set { application = "Origine de l'erreur : " + value + "\r\n"; }
        }
        public string Systeme
        {
            get { return  systeme; }
            set {  systeme = "Erreur système : " + value + "\r\n"; }
        }

        public MonException(string u, string a, string s)
        {
             utilisateur =  application =  systeme = "";
            if (u != "")
                 utilisateur = u + "\r\n";
            if (a != "")
                 application = "Origine de l'erreur : " + a + "\r\n";
            if (s != "")
                 systeme = "Erreur système : " + s + "\r\n";
        }
            public string MessageUtilisateur()
            {
                return ( utilisateur);
            }
            public string MessageApplication()
            {
                return ( application);
            }
            public string MessageSysteme()
            {
                return ( systeme);
            }
            public string MessageSupport()
            {
                return ( support);
            }
            public string Messages()
            {
                if ( systeme == "")
                     support = "";
                return ( utilisateur +  systeme +  support);
            }
        }
    }


