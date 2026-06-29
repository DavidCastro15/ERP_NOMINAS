using ERP_NOMINAS.Models.Deductions.ChildSupport;
using ERP_SHARED.Conexion;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Repositorys.Deductions
{
    public class ChildSupportRepository : ICSupport
    {
        ConnectorSql Conex = new ConnectorSql();
        SqlCommand cmd = new SqlCommand();

        public int CreateCSupport(CSupport s)
        {
            string[] fullNameBeneficiaryName = s.BeneficiaryName.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string query = @"INSERT INTO pension_alimenticia(id_nomina,numero_empleado,cuota_pension,porcpension,nombre_beneficiario,paterno_beneficiario,materno_beneficiario,cuenta_bancaria) 
                                                      VALUES(@id_nomina, @numero_empleado, @cuota_pension, @porcpension, @nombre_beneficiario, @paterno_beneficiario, @materno_beneficiario, @cuenta_bancaria)";

            using (cmd = new SqlCommand(query, Conex.nomi))
            {
                cmd.Parameters.AddWithValue("@id_nomina", 1);
                cmd.Parameters.AddWithValue("@numero_empleado", s.NumberEmployee);
                cmd.Parameters.AddWithValue("@cuota_pension", s.SupportAmount);
                cmd.Parameters.AddWithValue("@porcpension", s.SupportPercentage);
                cmd.Parameters.AddWithValue("@nombre_beneficiario", fullNameBeneficiaryName[0]);
                cmd.Parameters.AddWithValue("@paterno_beneficiario", fullNameBeneficiaryName[1]);
                cmd.Parameters.AddWithValue("@materno_beneficiario", fullNameBeneficiaryName[2]);
                cmd.Parameters.AddWithValue("@cuenta_bancaria", s.BankAccount);

                Conex.OpenNomina();
                return cmd.ExecuteNonQuery();
            }

        }

        public int DeleteCSupport(int Id)
        {
            string query = "DELETE FROM pension_alimenticia WHERE id = '" + Id + @"'";
            using (cmd = new SqlCommand(query, Conex.nomi))
            {
                Conex.OpenNomina();
                return cmd.ExecuteNonQuery();
            }
        }

        public List<CSupport> FilterByValue(string Name)
        {
            string column = "CONCAT(e.nombre, ' ', e.apellido_paterno, ' ', e.apellido_materno)";
            return ExecuteFilter(column, Name);
        }

        public List<CSupport> FilterByValue(int NumberEmployee)
        {
            string column = "p.numero_empleado";
            return ExecuteFilter(column, NumberEmployee.ToString());
        }

        private List<CSupport> ExecuteFilter(string column, string param)
        {
            List<CSupport> list = new List<CSupport>();

            string query = $@"SELECT p.id,p.id_nomina,e.numero_empleado,CONCAT(e.nombre,' ',e.apellido_paterno,' ',e.apellido_materno) as n_completo,p.cuota_pension,p.porcpension,
                                    CONCAT(p.nombre_beneficiario,' ',p.paterno_beneficiario,' ',p.materno_beneficiario) as n_beneficiario, p.cuenta_bancaria 
                                    FROM pension_alimenticia p
                                    INNER JOIN empleados e
                                    on p.numero_empleado = e.numero_empleado
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

        public CSupport GetCSupport(int Id)
        {
            using (cmd = new SqlCommand($@"SELECT p.id, e.numero_empleado, CONCAT(e.nombre, ' ', e.apellido_paterno, ' ', e.apellido_materno) as n_completo, p.cuota_pension, p.porcpension,
                                    CONCAT(p.nombre_beneficiario, ' ', p.paterno_beneficiario, ' ', p.materno_beneficiario) as n_beneficiario, p.cuenta_bancaria
                                    FROM pension_alimenticia p
                                    INNER JOIN empleados e
                                    ON p.numero_empleado = e.numero_empleado WHERE p.id = @id", Conex.nomi))
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

        public List<CSupport> GetCSupports()
        {
            List<CSupport> list = new List<CSupport>();

            try
            {

                using (cmd = new SqlCommand(@"SELECT p.id,e.numero_empleado,CONCAT(e.nombre,' ',e.apellido_paterno,' ',e.apellido_materno) as n_completo,p.cuota_pension,p.porcpension,
                                    CONCAT(p.nombre_beneficiario, ' ', p.paterno_beneficiario, ' ', p.materno_beneficiario) as n_beneficiario, p.cuenta_bancaria
                                    FROM pension_alimenticia p
                                    INNER JOIN empleados e
                                    ON p.numero_empleado = e.numero_empleado", Conex.nomi))
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

        public List<CSupportReport> GetDataReport(int PayWeek)
        {
            List<CSupportReport> list = new List<CSupportReport>();

            try
            {

                using (cmd = new SqlCommand(@"SELECT pa.numero_empleado as num_empleado,CONCAT(pa.nombre_beneficiario,' ',pa.paterno_beneficiario,' ',pa.materno_beneficiario) as nombre_beneficiario,pa.cuenta_bancaria as cuenta_banco,rsi.id_pension,
                                sum(iif(rsi.id_concepto=52,rsi.importe,0)) as importe_pagar
                                FROM resultado_sind_indiv_det_final rsi
                                INNER JOIN pension_alimenticia pa
                                ON pa.id = rsi.id_pension
                                INNER JOIN empleados e
                                ON e.numero_empleado = pa.numero_empleado
                                WHERE e.estatus = 'Activo' AND (pa.porcpension >0 OR pa.cuota_pension > 0)  AND semana_pago = @payWeek
                                GROUP  BY pa.numero_empleado,pa.nombre_beneficiario,pa.paterno_beneficiario,pa.materno_beneficiario,pa.cuenta_bancaria,rsi.id_pension
                                ORDER BY pa.numero_empleado ASC", Conex.nomi))
                {
                    cmd.Parameters.AddWithValue("@payWeek", PayWeek);
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

                throw new Exception("Error al obtener las pensiones: " + ex.Message);
            }
            finally
            {

                Conex.CloseNomina();
            }

            return list;
        }

        public int UpdateCSupport(CSupport s)
        {
            string[] fullNameBeneficiaryName = s.BeneficiaryName.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            string query = @"UPDATE pension_alimenticia SET cuota_pension=@cuota_pension,porcpension=@porcpension,nombre_beneficiario=@nombre_beneficiario,
                                        paterno_beneficiario = @paterno_beneficiario,materno_beneficiario = @materno_beneficiario,cuenta_bancaria = @cuenta_bancaria
                                        WHERE id = @id";

            using (cmd = new SqlCommand(query, Conex.nomi))
            {

                cmd.Parameters.AddWithValue("@id", s.Id);
                cmd.Parameters.AddWithValue("@cuota_pension", s.SupportAmount);
                cmd.Parameters.AddWithValue("@porcpension", s.SupportPercentage);
                cmd.Parameters.AddWithValue("@nombre_beneficiario", fullNameBeneficiaryName[0]);
                cmd.Parameters.AddWithValue("@paterno_beneficiario", fullNameBeneficiaryName[1]);
                cmd.Parameters.AddWithValue("@materno_beneficiario", fullNameBeneficiaryName[2]);
                cmd.Parameters.AddWithValue("@cuenta_bancaria", s.BankAccount);

                Conex.OpenNomina();
                return cmd.ExecuteNonQuery(); // Devuelve 1 si fue exitoso
            }
        }

        private CSupport ShowDataGrid(SqlDataReader reader)
        {
            return new CSupport
            {
                Id = Convert.ToInt32(reader["id"]),
                NumberEmployee = Convert.ToInt32(reader["numero_empleado"]),
                FullName = Convert.ToString(reader["n_completo"]),
                SupportAmount = Convert.ToDecimal(reader["cuota_pension"]),
                SupportPercentage = Convert.ToDecimal(reader["porcpension"]),
                BeneficiaryName = Convert.ToString(reader["n_beneficiario"]),
                BankAccount = Convert.ToString(reader["cuenta_bancaria"])
            };
        }

        private CSupportReport ShowDataReport(SqlDataReader reader)
        {
            return new CSupportReport
            {
                NumberEmployee = Convert.ToInt32(reader["numero_empleado"]),
                BeneficiaryName = Convert.ToString(reader["nombre_beneficiario"]),
                AccountBank = Convert.ToString(reader["cuenta_beneficiario"]),
                Amount = Convert.ToDecimal(reader["importe"])
            };
        }
    }
}