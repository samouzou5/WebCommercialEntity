using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebCommercialEntity.Models;

namespace WebCommercialEntity.Models
{
    public class DetailCde
    {

        private articles art;
        private int qte_cdee;
        private string livree;
        private Double total;
        private List<DetailCde> detailCdes;
        private commandes com;
        /// <summary>
        /// Initialisation
        /// </summary>


        public DetailCde(articles a, int q, string l, Double t)
        {
            this.art = a;
            this.qte_cdee = q;
            this.total = t;
            this.Livree = l;
        }

        public DetailCde(articles a, int q, string l, Double t, commandes c)
        {
            this.art = a;
            this.qte_cdee = q;
            this.total = t;
            this.Livree = l;
            this.com = c;
        }

        public DetailCde()
        {
        }

        public articles Art
        {
            get { return art; }
            set { art = value; }
        }
        public int Qte_cdee
        {
            get { return qte_cdee; }
            set { qte_cdee = value; }
        }
        public string Livree
        {
            get { return livree; }
            set { livree = value; }
        }
        public Double Total
        {
            get { return total; }
            set { total = value; }
        }

        public List<DetailCde> DetailCdes
        {
            get { return detailCdes; }
            set { detailCdes = value; }
        }

        public commandes Com { get => com; set => com = value; }
    }
}
