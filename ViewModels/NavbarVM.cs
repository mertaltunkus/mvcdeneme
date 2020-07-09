using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCdeneme2.Models.EntityFramework;

namespace MVCdeneme2.ViewModels
{
    public class NavbarVM
    {
        public IEnumerable<Ders> Dersler { get; set; }
        public IEnumerable<Sinif> Siniflar { get; set; }
        public string username { get; set; }
    }
}