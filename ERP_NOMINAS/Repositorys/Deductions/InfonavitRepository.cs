using ERP_NOMINAS.Models.Deductions.Infonavit;
using ERP_SHARED.Conexion;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Repositorys.Deductions
{
    public class InfonavitRepository : IDInfonavit
    {
        ConnectorSql Conex = new ConnectorSql();
        SqlCommand cmd = new SqlCommand();

        public int CreateDInfonavit(DInfonavit d)
        {
            string query = @"INSERT INTO infonavit_descuentos (id_nomina,numero_empleado,cantidad_total,fecha_credito,fecha_ultimo_pago,cantidad_pagada,porcentaje_pago,descuento_periodico,numero_abonos,calcular_porcentaje,comentarios,estatus) 
                                        VALUES(@id_nomina,@numero_empleado,@cantidad_total,@fecha_credito,@fecha_ultimo_pago,@cantidad_pagada,@porcentaje_pago,@descuento_periodico,@numero_abonos,@calcular_porcentaje,@comentarios,@estatus)";

            using (cmd = new SqlCommand(query, Conex.nomi))
            {
                cmd.Parameters.AddWithValue("@id_nomina", 1);
                cmd.Parameters.AddWithValue("@numero_empleado", d.NumberEmployee);
                cmd.Parameters.AddWithValue("@cantidad_total", d.TotalAmount);
                cmd.Parameters.AddWithValue("@fecha_credito", d.CreditDate);
                cmd.Parameters.AddWithValue("@fecha_ultimo_pago", d.LastDatePay);
                cmd.Parameters.AddWithValue("@cantidad_pagada", d.AmountPaid);
                cmd.Parameters.AddWithValue("@porcentaje_pago", d.PercentageToBepaid);
                cmd.Parameters.AddWithValue("@descuento_periodico", d.PeriodicDeduction);
                cmd.Parameters.AddWithValue("@numero_abonos", d.CreditContributions);
                cmd.Parameters.AddWithValue("@calcular_porcentaje", d.CalculatePercentage);
                cmd.Parameters.AddWithValue("@comentarios", d.Comments);
                cmd.Parameters.AddWithValue("@estatus", d.Status);
      
                Conex.OpenNomina();
                return cmd.ExecuteNonQuery();
            }
        }

        public int DeleteInfonavit(int Id)
        {
            string query = "DELETE FROM infonavit_descuentos WHERE id = '" + Id + @"'";
            using (cmd = new SqlCommand(query, Conex.nomi))
            {
                Conex.OpenNomina();
                return cmd.ExecuteNonQuery();
            }
        }

        public List<DInfonavit> FilterByValue(string Name)
        {
            string column = "CONCAT(e.nombre, ' ', e.apellido_paterno, ' ', e.apellido_materno)";
            return ExecuteFilter(column, Name);
        }

        public List<DInfonavit> FilterByValue(int NumberEmployee)
        {
            string column = "i.numero_empleado";
            return ExecuteFilter(column, NumberEmployee.ToString());
        }

        private List<DInfonavit> ExecuteFilter(string column, string param)
        {
            List<DInfonavit> list = new List<DInfonavit>();

            string query = $@"SELECT i.id,i.numero_empleado,CONCAT(e.nombre,' ',e.apellido_paterno,' ',e.apellido_materno) AS n_completo,i.cantidad_total,i.fecha_credito,
                                                            i.fecha_ultimo_pago,i.cantidad_pagada,i.porcentaje_pago,i.descuento_periodico,i.numero_abonos,i.calcular_porcentaje,i.comentarios,i.estatus
                                                            FROM infonavit_descuentos i
                                                            INNER JOIN empleados e
                                                            ON i.numero_empleado = e.numero_empleado
                                                            ORDER BY i.numero_empleado
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

        public DInfonavit GetDInfonavit(int Id)
        {
            using (cmd = new SqlCommand($@"SELECT i.id,i.numero_empleado,CONCAT(e.nombre,' ',e.apellido_paterno,' ',e.apellido_materno) AS n_completo,i.cantidad_total,i.fecha_credito,
                                                            i.fecha_ultimo_pago,i.cantidad_pagada,i.porcentaje_pago,i.descuento_periodico,i.numero_abonos,i.calcular_porcentaje,i.comentarios,i.estatus
                                                            FROM infonavit_descuentos i
                                                            INNER JOIN empleados e
                                                            ON i.numero_empleado = e.numero_empleado 
                                                            WHERE i.id = @id", Conex.nomi))
            {

                cmd.Parameters.AddWithValue("@id", Id);

                Conex.OpenNomina();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {

                    if (reader.Read())
                    {
                        return ShowDataGrid(reader);
                    }
                }
            }

            return null;
        }

        public List<DInfonavit> GetDInfonavits()
        {
            List<DInfonavit> list = new List<DInfonavit>();

            try
            {

                using (cmd = new SqlCommand(@"SELECT i.id,i.numero_empleado,CONCAT(e.nombre,' ',e.apellido_paterno,' ',e.apellido_materno) AS n_completo,i.cantidad_total,i.fecha_credito,
                                                            i.fecha_ultimo_pago,i.cantidad_pagada,i.porcentaje_pago,i.descuento_periodico,i.numero_abonos,i.calcular_porcentaje,i.comentarios,i.estatus
                                                            FROM infonavit_descuentos i
                                                            INNER JOIN empleados e
                                                            ON i.numero_empleado = e.numero_empleado
                                                            ORDER BY i.numero_empleado", Conex.nomi))
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

                throw new Exception("Error al obtener las pensiones: " + ex.Message);
            }
            finally
            {

                Conex.CloseNomina();
            }

            return list;
        }

        public int UpdateDInfonavit(DInfonavit d)
        {
            string query = @"UPDATE infonavit_descuentos SET cantidad_total=@cantidad_total,fecha_credito=@fecha_credito,fecha_ultimo_pago=@fecha_ultimo_pago,cantidad_pagada=@cantidad_pagada,porcentaje_pago=@porcentaje_pago,
                                                            descuento_periodico=@descuento_periodico,numero_abonos=@numero_abonos,comentarios=@comentarios WHERE id=@id";

            using (cmd = new SqlCommand(query, Conex.nomi))
            {

                cmd.Parameters.AddWithValue("@id", d.Id);
                cmd.Parameters.AddWithValue("@cantidad_total", d.TotalAmount);
                cmd.Parameters.AddWithValue("@fecha_credito", d.CreditDate);
                cmd.Parameters.AddWithValue("@fecha_ultimo_pago", d.LastDatePay);
                cmd.Parameters.AddWithValue("@cantidad_pagada", d.AmountPaid);
                cmd.Parameters.AddWithValue("@porcentaje_pago", d.PercentageToBepaid);
                cmd.Parameters.AddWithValue("@descuento_periodico", d.PeriodicDeduction);
                cmd.Parameters.AddWithValue("@numero_abonos", d.CreditContributions);
                cmd.Parameters.AddWithValue("@calcular_porcentaje", d.CalculatePercentage);
                cmd.Parameters.AddWithValue("@comentarios", d.Comments);

                Conex.OpenNomina();
                return cmd.ExecuteNonQuery(); // Devuelve 1 si fue exitoso
            }
        }

        private DInfonavit ShowDataGrid(SqlDataReader reader)
        {
            return new DInfonavit
            {
                Id = Convert.ToInt32(reader["id"]),
                NumberEmployee = Convert.ToInt32(reader["numero_empleado"]),
                FullName = Convert.ToString(reader["n_completo"]),
                TotalAmount = reader["cantidad_total"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["cantidad_total"]),
                CreditDate = reader["fecha_credito"] == DBNull.Value ? new DateTime(1900, 1, 1) : Convert.ToDateTime(reader["fecha_credito"]),
                LastDatePay = reader["fecha_ultimo_pago"] == DBNull.Value ? new DateTime(1900, 1, 1) : Convert.ToDateTime(reader["fecha_ultimo_pago"]),
                AmountPaid = reader["cantidad_pagada"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["cantidad_pagada"]),
                PercentageToBepaid = reader["porcentaje_pago"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["porcentaje_pago"]),
                PeriodicDeduction = reader["descuento_periodico"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["descuento_periodico"]),
                CreditContributions = reader["numero_abonos"] == DBNull.Value ? 0 : Convert.ToInt32(reader["numero_abonos"]),
                CalculatePercentage = reader["calcular_porcentaje"] == DBNull.Value ? string.Empty : Convert.ToString(reader["calcular_porcentaje"]),
                Comments = reader["comentarios"] == DBNull.Value ? string.Empty : Convert.ToString(reader["comentarios"]),
                Status = reader["estatus"] == DBNull.Value ? 0 : Convert.ToInt32(reader["estatus"])
            };
        }
    }
}
