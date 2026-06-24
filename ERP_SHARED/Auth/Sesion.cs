using ERP_SHARED.Auth;
using ERP_SHARED.Auth.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_SHARED.Auth
{
    public static class Sesion
    {
        public static User UserIsLoggin { get; set; } 
        public static List<string> AuthorizedCatalogs { get; set; }
        public static string DataBaseName { get; set; }
    }

    public class SessionPackage
    {
        public User User { get; set; }
        public List<string> Permissions { get; set; }
        public string DataBaseEnable { get; set; }
        
    }
}
