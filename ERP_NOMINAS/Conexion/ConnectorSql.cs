using System;
using System.Data.SqlClient;

namespace ERP_NOMINAS.Conexion
{
    public class ConnectorSql
    {
        private string separators = "$";
        private string commands = Environment.CommandLine; // equivalente a Command() en VB
        private string[] args;

        private string ip_servidor = "192.168.2.250";
        //private string base_datos_2 = "erp_nomina";
        // private string ip_servidor = "187.157.242.105";
        // private string base_datos = "azsja_nomina_pruebas"; // PRUEBAS
        private string user = "sa";
        private string pwd = "AsjaEvol19";

        private string base_datos;

        public SqlConnection asis;
        public SqlConnection nomi;
        public SqlConnection conexion3;

        public ConnectorSql()
        {
            args = commands.Split(separators.ToCharArray());
            base_datos = args.Length > 1 ? args[1].ToString() : "azsja_nomina";

            asis = new SqlConnection(
                $"Data Source={ip_servidor};Initial Catalog=erpnomina;Persist Security Info=True;User ID={user};Password={pwd};MultipleActiveResultSets=True");

            nomi = new SqlConnection(
                $"Data Source={ip_servidor};Initial Catalog={base_datos};Persist Security Info=True;User ID={user};Password={pwd};MultipleActiveResultSets=True;Max Pool Size=10024;Connection Timeout=999");

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
