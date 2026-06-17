using ERP_NOMINAS.Conexion;
using ERP_NOMINAS.GlobalFunctions;
using ERP_NOMINAS.Models.Tunneling;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Repositorys
{
    public class TunnelRepository : ITunnel
    {
        ConnectorSql Conex = new ConnectorSql();
        Utilities Util = new Utilities();
        SqlCommand cmd = new SqlCommand();

        public List<TunnelReport> GetDataReport(int PayWeek)
        {
            List<TunnelReport> list = new List<TunnelReport>();

            try
            {
                 
                using (cmd = new SqlCommand($@"SELECT t.id_empleado,CONCAT(e.nombre,' ',e.apellido_paterno,'',e.apellido_materno) AS n_completo, dias,tarifa,importe,semana_pago
                                                              FROM topos t
                                                              INNER JOIN empleados e
                                                              ON t.id_empleado = e.numero_empleado
                                                              WHERE t.semana_pago = {PayWeek}
                                                              ORDER BY t.id_empleado ASC", Conex.nomi))

                {
                    Conex.OpenNomina();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            list.Add(ShowDataReport(reader));
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Error al obtener los empleados: " + ex.Message);
            }
            finally
            {

                Conex.CloseNomina();
            }

            return list;
        }

        public List<Tunnel> GetTunnels()
        {
            List<Tunnel> list = new List<Tunnel>();

            try
            {

                using (cmd = new SqlCommand($@"SELECT t.id,t.id_empleado,CONCAT(e.nombre,' ',e.apellido_paterno,'',e.apellido_materno) AS n_completo, dias,tarifa,importe,semana_pago
                                                              FROM topos t
                                                              INNER JOIN empleados e
                                                              ON t.id_empleado = e.numero_empleado
                                                              ORDER BY t.id DESC", Conex.nomi))

                {
                    Conex.OpenNomina();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            list.Add(ShowDataGrid(reader));
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Error al obtener los empleados: " + ex.Message);
            }
            finally
            {

                Conex.CloseNomina();
            }

            return list;
        }

        public List<Tunnel> FilterByValue(string Name)
        {
            string column = "CONCAT(e.nombre, ' ', e.apellido_paterno, ' ', e.apellido_materno)";
            return ExecuteFilter(column, Name);
        }

        public List<Tunnel> FilterByValue(int PayWeek)
        {
            string column = "t.semana_pago";
            return ExecuteFilter(column, PayWeek.ToString());
        }

        private List<Tunnel> ExecuteFilter(string column, string param)
        {
            List<Tunnel> list = new List<Tunnel>();

            string query = $@"SELECT t.id,t.id_empleado,CONCAT(e.nombre,' ',e.apellido_paterno,'',e.apellido_materno) AS n_completo,t.dias,t.tarifa,t.importe,t.semana_pago
                            FROM topos t
                            INNER JOIN empleados e ON t.id_empleado = e.numero_empleado 
                            WHERE {column} LIKE @param";

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, Conex.nomi))
                {
                    cmd.Parameters.AddWithValue("@param", $"%{param}%");
                    Conex.OpenNomina();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(ShowDataGrid(reader));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener los empleados: " + ex.Message);
            }
            finally
            {
                Conex.CloseNomina();
            }

            return list;
        }

        private Tunnel ShowDataGrid(SqlDataReader reader)
        {
            return new Tunnel
            {
                Id = Convert.ToInt32(reader["id"]),
                NumberEmployee = Convert.ToInt32(reader["id_empleado"]),
                FullName = Convert.ToString(reader["n_completo"]),
                Days = Convert.ToInt32(reader["dias"]),
                PayRate = Convert.ToDecimal(reader["tarifa"]),
                Amount = Convert.ToDecimal(reader["importe"]),
                PayWeek = Convert.ToInt32(reader["semana_pago"])
            };
        }

        private TunnelReport ShowDataReport(SqlDataReader reader)
        {
            return new TunnelReport
            {
                NumberEmployee = Convert.ToInt32(reader["id_empleado"]),
                FullName = Convert.ToString(reader["n_completo"]),
                Days = Convert.ToInt32(reader["dias"]),
                PayRate = Convert.ToDecimal(reader["tarifa"]),
                Amount = Convert.ToDecimal(reader["importe"]),
                PayWeek = Convert.ToInt32(reader["semana_pago"])
            };
        }
    }
}
