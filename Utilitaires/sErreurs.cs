using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilitaires
{
    public struct sErreurs
    {
        private String messageUtilisateur, messageApplication;
        public sErreurs(String mu, String ma)
        {
            this.messageUtilisateur = mu;
            this.messageApplication = ma;
        }
        public String MessageUtilisateur()
        {
            return (this.messageUtilisateur);
        }
        public String MessageApplication()
        {
            return (this.messageApplication);
        }
    }


}
