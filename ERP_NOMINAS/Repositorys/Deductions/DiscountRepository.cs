using ERP_NOMINAS.Models.Deductions.VariousDiscounts;
using ERP_SHARED.Conexion;
using ERP_SHARED.GlobalFunctions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Repositorys.Deductions
{
    public class DiscountRepository : IDiscount
    {

        ConnectorSql Conex = new ConnectorSql();
        Utilities Util = new Utilities();
        SqlCommand cmd = new SqlCommand();

        public int CreateDiscount(Discount d)
        {
            string query = @"INSERT INTO descuentos_detalle(id_concepto,numero_empleado,importe_inicial,numero_pagos,descuento,saldo_pendiente,descuentos_acumulados,fecha_captura,comentarios) 
                                                      VALUES(@id_concepto,@numero_empleado,@importe_inicial,@numero_pagos,@descuento,@saldo_pendiente,@descuentos_acumulados,@fecha_captura,@comentarios)";

            using (cmd = new SqlCommand(query, Conex.nomi))
            {
                cmd.Parameters.AddWithValue("@id_concepto", d.IdConcept);
                cmd.Parameters.AddWithValue("@numero_empleado", d.NumberEmployee);
                cmd.Parameters.AddWithValue("@importe_inicial", d.InitialAmount);
                cmd.Parameters.AddWithValue("@numero_pagos", d.NumberPayments);
                cmd.Parameters.AddWithValue("@descuento", d._Discount);
                cmd.Parameters.AddWithValue("@saldo_pendiente", d.OutstandingBalance);
                cmd.Parameters.AddWithValue("@descuentos_acumulados", d.AccumulatedDiscounts);
                cmd.Parameters.AddWithValue("@fecha_captura", DateTime.Now);
                cmd.Parameters.AddWithValue("@comentarios", d.Comments);

                Conex.OpenNomina();
                return cmd.ExecuteNonQuery(); // Devuelve 1 si fue exitoso
            }
        }

        public int DeleteDiscount(int Id)
        {
            string query = $"DELETE FROM descuentos_detalle WHERE id = {Id} ";
            using (cmd = new SqlCommand(query, Conex.nomi))
            {
                Conex.OpenNomina();
                return cmd.ExecuteNonQuery();
            }
        }

        public List<Discount> FilterByConcept(int IdConcept)
        {
            List<Discount> list = new List<Discount>();

            string query = $@"SELECT dd.id,dd.id_concepto,dd.numero_empleado,CONCAT(e.nombre,' ',e.apellido_paterno,' ',e.apellido_materno) AS n_completo,
                                                            dd.importe_inicial,dd.numero_pagos,dd.descuento,dd.saldo_pendiente,dd.descuentos_acumulados,dd.fecha_captura,dd.comentarios,ultimo_importe_pagado
                                                            FROM descuentos_detalle dd
                                                            INNER JOIN empleados e
                                                            ON dd.numero_empleado = e.numero_empleado                                                           
                                                            WHERE id_concepto LIKE @param 
                                                            ORDER BY dd.id";

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, Conex.nomi))
                {
                    cmd.Parameters.AddWithValue("@param", $"%{IdConcept}%");
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

        public List<Discount> FilterByValue(string Name)
        {
            string column = "CONCAT(e.nombre, ' ', e.apellido_paterno, ' ', e.apellido_materno)";
            return ExecuteFilter(column, Name);
        }

        public List<Discount> FilterByValue(int NumberEmployee)
        {
            string column = "dd.numero_empleado";
            return ExecuteFilter(column, NumberEmployee.ToString());
        }

        private List<Discount> ExecuteFilter(string column, string param)
        {
            List<Discount> list = new List<Discount>();

            string query = $@"SELECT dd.id,dd.id_concepto,dd.numero_empleado,CONCAT(e.nombre,' ',e.apellido_paterno,' ',e.apellido_materno) AS n_completo,
                                                            dd.importe_inicial,dd.numero_pagos,dd.descuento,dd.saldo_pendiente,dd.descuentos_acumulados,dd.fecha_captura,dd.comentarios,ultimo_importe_pagado
                                                            FROM descuentos_detalle dd
                                                            INNER JOIN empleados e
                                                            ON dd.numero_empleado = e.numero_empleado                                                           
                                                            WHERE {column} LIKE @param 
                                                            ORDER BY dd.id";

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

        public Discount GetDiscount(int Id)
        {
            using (cmd = new SqlCommand(@"SELECT dd.id,dd.id_concepto,dd.numero_empleado,CONCAT(e.nombre,' ',e.apellido_paterno,' ',e.apellido_materno) AS n_completo,
                                                            dd.importe_inicial,dd.numero_pagos,dd.descuento,dd.saldo_pendiente,dd.descuentos_acumulados,dd.fecha_captura,dd.comentarios,ultimo_importe_pagado
                                                            FROM descuentos_detalle dd
                                                            INNER JOIN empleados e
                                                            ON dd.numero_empleado = e.numero_empleado                                                           
                                                            WHERE dd.id = @id", Conex.nomi))
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

        public List<Discount> GetDiscounts()
        {
            List<Discount> list = new List<Discount>();

            try
            {

                using (cmd = new SqlCommand(@"SELECT dd.id,dd.id_concepto,dd.numero_empleado,CONCAT(e.nombre,' ',e.apellido_paterno,' ',e.apellido_materno) AS n_completo,
                                                            dd.importe_inicial,dd.numero_pagos,dd.descuento,dd.saldo_pendiente,dd.descuentos_acumulados,dd.id_subclasificacion,dd.fecha_captura,dd.comentarios,ultimo_importe_pagado
                                                            FROM descuentos_detalle dd
                                                            INNER JOIN empleados e
                                                            ON dd.numero_empleado = e.numero_empleado
                                                            ORDER BY dd.id", Conex.nomi))
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

        public List<DiscountReport> GetDataReport(int IdConcept)
        {
            List<DiscountReport> list = new List<DiscountReport>();

            try
            {

                using (cmd = new SqlCommand(@"SELECT dd.id,dd.id_concepto,dd.numero_empleado,CONCAT(e.nombre,' ',e.apellido_paterno,' ',e.apellido_materno) as n_completo ,dd.descuento,dd.saldo_pendiente 
                                                FROM descuentos_detalle dd
                                                INNER JOIN empleados e
                                                ON dd.numero_empleado = e.numero_empleado
                                                WHERE dd.id_concepto = @IdConcept
                                                ORDER BY dd.numero_empleado", Conex.nomi))
                {
                    cmd.Parameters.AddWithValue("@IdConcept", IdConcept);
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

        public int UpdateDiscount(Discount d)
        {
            string query = @"UPDATE descuentos_detalle SET importe_inicial=@importe_inicial,numero_pagos=@numero_pagos,descuento=@descuento,saldo_pendiente=@saldo_pendiente,descuentos_acumulados=@descuentos_acumulados,
                                                                    comentarios=@comentarios WHERE id=@id";

            using (cmd = new SqlCommand(query, Conex.nomi))
            {

                cmd.Parameters.AddWithValue("@id", d.Id);
                cmd.Parameters.AddWithValue("@importe_inicial", d.InitialAmount);
                cmd.Parameters.AddWithValue("@numero_pagos", d.NumberPayments);
                cmd.Parameters.AddWithValue("@descuento", d._Discount);
                cmd.Parameters.AddWithValue("@saldo_pendiente", d.OutstandingBalance);
                cmd.Parameters.AddWithValue("@descuentos_acumulados", d.AccumulatedDiscounts);
                cmd.Parameters.AddWithValue("@comentarios", d.Comments);

                Conex.OpenNomina();
                return cmd.ExecuteNonQuery();
            }
        }

        private Discount ShowDataGrid(SqlDataReader reader)
        {
            return new Discount
            {
                Id = Convert.ToInt32(reader["id"]),
                IdConcept = Convert.ToInt32(reader["id_concepto"]),
                NumberEmployee = Convert.ToInt32(reader["numero_empleado"]),
                FullName = Convert.ToString(reader["n_completo"]),
                InitialAmount = Convert.ToDecimal(reader["importe_inicial"]),
                NumberPayments = Convert.ToInt32(reader["numero_pagos"]),
                _Discount = Convert.ToDecimal(reader["descuento"]),
                OutstandingBalance = Convert.ToDecimal(reader["saldo_pendiente"]),
                AccumulatedDiscounts = Convert.ToDecimal(reader["descuentos_acumulados"]),
                CaptureDate = Convert.ToDateTime(reader["fecha_captura"]),
                Comments = Convert.ToString(reader["comentarios"]),
                LastAmountPaid = reader["ultimo_importe_pagado"] == DBNull.Value
            ? (decimal?)null
            : Convert.ToDecimal(reader["ultimo_importe_pagado"])
            };
        }

        private DiscountReport ShowDataReport(SqlDataReader reader)
        {
            return new DiscountReport
            {
                Id = Convert.ToInt32(reader["id"]),
                IdConcept = Convert.ToInt32(reader["id_concepto"]),
                NumberEmployee = Convert.ToInt32(reader["numero_empleado"]),
                FullName = Convert.ToString(reader["n_completo"]),
                Discount = Convert.ToDecimal(reader["descuento"]),
                OutstandingBalance = Convert.ToDecimal(reader["saldo_pendiente"]),             
            };
        }
    }
}
