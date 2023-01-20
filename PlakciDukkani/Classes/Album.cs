using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlakciDukkani.Classes
{
    public class Album
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Sanatci { get; set; }
        public int CikisYili { get; set; }
        public decimal Fiyat { get; set; }
        public double İndirimOrani { get; set; }
        public bool DevamMi { get; set; }
    }
}
