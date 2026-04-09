using System;
using System.Data.SqlClient;

namespace ERP_NOMINAS.Conexion
{
    public class ConnectorSql
    {
        private string separators = "$";
        private string commands = Environment.CommandLine; 
        private string[] args;
 
        //private string base_datos_2 = "erp_nomina";
        // private string ip_servidor = "187.157.242.105";
        // private string base_datos = "azsja_nomina_pruebas"; // PRUEBAS

        private string DB;

        public SqlConnection asis;
        public SqlConnection nomi;
        public SqlConnection conexion3;

        public ConnectorSql()
        {
            args = commands.Split(separators.ToCharArray());
            DB = args.Length > 1 ? args[1].ToString() : "azsja_nomina";

            asis = new SqlConnection(
                $"Data Source={AppConstants.ServerIp};Initial Catalog=erpnomina;Persist Security Info=True;User ID={AppConstants.User};Password={AppConstants.Password};MultipleActiveResultSets=True");

            nomi = new SqlConnection(
                $"Data Source={AppConstants.ServerIp};Initial Catalog={DB};Persist Security Info=True;User ID={AppConstants.User};Password={AppConstants.Password};MultipleActiveResultSets=True;Max Pool Size=10024;Connection Timeout=999");

            //conexion3 = new SqlConnection(
            //    $"Data Source={ip_servidor};Initial Catalog={base_datos_2};Persist Security Info=True;User ID={user};Password={pwd};MultipleActiveResultSets=True;Max Pool Size=10024;Connection Timeout=999");
        }

        public void OpenAsistencia()
        {
            if (asis.State == System.Data.ConnectionState.Closed)
            {
                asis.Open();
            }
        }

        public void CloseAsistencia()
        {
            if (asis.State == System.Data.ConnectionState.Open)
            {
                asis.Close();
            }
        }

        public void OpenNomina()
        {
            if (nomi.State == System.Data.ConnectionState.Closed)
            {
                nomi.Open();
            }
        }

        public void CloseNomina()
        {
            if (nomi.State == System.Data.ConnectionState.Open)
            {
                nomi.Close();
            }
        }

       

    }


}
