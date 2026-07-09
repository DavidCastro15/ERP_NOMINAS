using System;
using System.Data;
using System.Data.SqlClient;

namespace ERP_SHARED.Conexion
{
    public class ConnectorSql
    {
        // Campos privados que mantendrán la conexión viva en memoria
        private static SqlConnection _asis;
        private static SqlConnection _nomi;
        private static SqlConnection _auth;

        // Propiedad estática para asignar la base de datos que mandó el Login
        private static string _nameBd = "azsja_nomina"; // Valor por defecto
        public static string NameBd
        {
            get => _nameBd;
            set
            {
                _nameBd = value;
                // reinicializamos los objetos de conexión con la nueva ruta.
                InitializeConnections();
            }
        }

        // Inicializador de conexiones (Se ejecuta de forma automática)
        private static void InitializeConnections()
        {
            _asis = new SqlConnection($"Data Source={AppConstants.ServerIp};Initial Catalog=erpnomina;Persist Security Info=True;User ID={AppConstants.User};Password={AppConstants.Password};MultipleActiveResultSets=True");

            _nomi = new SqlConnection($"Data Source={AppConstants.ServerIp};Initial Catalog={_nameBd};Persist Security Info=True;User ID={AppConstants.User};Password={AppConstants.Password};MultipleActiveResultSets=True;Max Pool Size=10024;Connection Timeout=999");

            _auth = new SqlConnection($"Data Source={AppConstants.ServerIp};Initial Catalog=azsja_nombase;Persist Security Info=True;User ID={AppConstants.User};Password={AppConstants.Password};MultipleActiveResultSets=True;Max Pool Size=10024;Connection Timeout=999");
        }

        public SqlConnection asis => _asis;
        public SqlConnection nomi => _nomi;
        public SqlConnection auth => _auth;

        public ConnectorSql()
        {
            if (_nomi == null || _auth == null || string.IsNullOrEmpty(_auth.ConnectionString))
            {
                InitializeConnections();
            }
        }

        public void OpenAsistencia()
        {
            if (_asis != null && _asis.State == ConnectionState.Closed) _asis.Open();
        }

        public void CloseAsistencia()
        {
            if (_asis != null && _asis.State == ConnectionState.Open) _asis.Close();
        }

        public void OpenNomina()
        {
            if (_nomi != null && _nomi.State == ConnectionState.Closed) _nomi.Open();
        }

        public void CloseNomina()
        {
            if (_nomi != null && _nomi.State == ConnectionState.Open) _nomi.Close();
        }

        public void OpenAuth()
        {
            if (_auth == null || string.IsNullOrEmpty(_auth.ConnectionString))
            {
                InitializeConnections();
            }
            if (_auth.State == ConnectionState.Closed) _auth.Open();
        }

        public void CloseAuth()
        {
            if (_auth != null && _auth.State == ConnectionState.Open) _auth.Close();
        }

    }
}
