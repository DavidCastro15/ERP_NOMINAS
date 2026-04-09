using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS
{
    public static class AppConstants
    {
        public const string AppName = "ERP_NOMINAS";
        public const string CompanyName = "AZUCARERA SAN JOSE DE ABAJO, S.A DE C.V";
        public const string ServerIp = "192.168.2.250";
        public const string User = "sa";
        public static readonly string Password = ConfigurationManager.AppSettings["DB_PASSWORD"]; //MANERA MAS RAPIDA MENOS SEGURA !!USAR AppConfig para version final
    }
}
