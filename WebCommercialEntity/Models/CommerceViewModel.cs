using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebCommercialEntity.Models
{
    public class CommerceViewModel
    {
        public IEnumerable<vendeur> lesVendeurs { get; set; }
        public commandes cs { get; set; }
        public IEnumerable<clientel> lesClients { get; set; }

        public List<articles> articles { get; set; }

    }
}