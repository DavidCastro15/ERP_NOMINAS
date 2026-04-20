using ClosedXML.Excel;
using ERP_NOMINAS.Conexion;
using ERP_NOMINAS.GlobalFunctions;
using ERP_NOMINAS.Models.Employees;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Repositorys
{
    public class EmployeeRepository : IEmployee
    {
        ConnectorSql Conex = new ConnectorSql();
        Utilities Util = new Utilities();
        SqlCommand cmd = new SqlCommand();

        public int CreateEmployee(Employee e)
        {

            string query = " INSERT INTO empleados(id_nomina,numero_empleado,numero_credencial,nombre,apellido_paterno,apellido_materno,rfc,imss,modulo18,categoria_zafra,categoria_reparacion,estatus, "+
                                               " estatus_fecha,tipo,clasificacion,zona,sexo,estado_civil,lugar_nacimiento,fecha_nacimiento,padre_nombre,madre_nombre,domicilio,colonia,codigo_postal,ciudad,municipio,estado, "+
                                               " telefono,escolaridad,profesion,anios_reparacion,anios_zafra, dias_aguinaldo_rep,dias_aguinaldo_zaf,dias_vacaciones_rep,dias_vacaciones_zaf,dias_comision, "+
                                               " declara,fotografia,ausentismo,curp,patron,dias_presencia_ant,dias_puntualidad_ant,dias_presencia_actual ,dias_puntualidad_actual,domingos_laborados_zafra,cuenta_bancaria,clabe_interbancaria,aplicar_cuota_sindical,fecha_plaza) "+
                                               " VALUES(  " +
                                               " @id_nomina, @numero_empleado, @numero_credencial, @nombre, @apellido_paterno, @apellido_materno, @rfc, @imss, @modulo18, @categoria_zafra, @categoria_reparacion, @estatus, " +
                                               " @estatus_fecha, @tipo, @clasificacion, @zona, @sexo, @estado_civil, @lugar_nacimiento, @fecha_nacimiento, @padre_nombre, @madre_nombre, @domicilio, @colonia, @codigo_postal, @ciudad, @municipio, @estado, "+
                                               " @telefono, @escolaridad, @profesion, @anios_reparacion, @anios_zafra, @dias_aguinaldo_rep, @dias_aguinaldo_zaf, @dias_vacaciones_rep, @dias_vacaciones_zaf, @dias_comision, "+
                                               " @declara, @fotografia, @ausentismo, @curp, @patron, @dias_presencia_ant, @dias_puntualidad_ant, @dias_presencia_actual, @dias_puntualidad_actual, @domingos_laborados_zafra, @cuenta_bancaria, @clabe_interbancaria, @aplicar_cuota_sindical, @fecha_plaza) ";
            //,uso_zafra,uso_reparacion
            //, @uso_zafra, @uso_reparacion
            using (cmd = new SqlCommand(query, Conex.nomi))
            {
                // Uso de parámetros para evitar inyección SQL
                cmd.Parameters.AddWithValue("@id_nomina", e.PayrollId);
                cmd.Parameters.AddWithValue("@numero_empleado", e.NumberEmployee);
                cmd.Parameters.AddWithValue("@numero_credencial", e.NumberCredential);
                cmd.Parameters.AddWithValue("@nombre", e.Name);
                cmd.Parameters.AddWithValue("@apellido_paterno", e.LastnameFather);
                cmd.Parameters.AddWithValue("@apellido_materno", e.LastnameMother);
                cmd.Parameters.AddWithValue("@rfc", e.RFC);
                cmd.Parameters.AddWithValue("@imss", e.Imss);
                cmd.Parameters.AddWithValue("@modulo18", e.Module18);
                cmd.Parameters.AddWithValue("@categoria_zafra", e.CategoryHarvest);
                cmd.Parameters.AddWithValue("@categoria_reparacion", e.CategoryRepair);
                cmd.Parameters.AddWithValue("@estatus", e.Status);
                cmd.Parameters.AddWithValue("@estatus_fecha", Convert.ToDateTime(e.StatusDate).ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@tipo", e.Type);
                cmd.Parameters.AddWithValue("@clasificacion", e.Classification);
                cmd.Parameters.AddWithValue("@zona", e.Area);
                cmd.Parameters.AddWithValue("@sexo", e.Sex);
                cmd.Parameters.AddWithValue("@estado_civil", e.MaritalStatus);
                cmd.Parameters.AddWithValue("@lugar_nacimiento", e.PlaceBirth);
                cmd.Parameters.AddWithValue("@fecha_nacimiento", Convert.ToDateTime(e.DateBirth).ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@padre_nombre", e.FatherName);
                cmd.Parameters.AddWithValue("@madre_nombre", e.MotherName);
                cmd.Parameters.AddWithValue("@domicilio", e.Address);
                cmd.Parameters.AddWithValue("@colonia", e.Cologne);
                cmd.Parameters.AddWithValue("@codigo_postal", e.ZipCode);
                cmd.Parameters.AddWithValue("@ciudad", e.City);
                cmd.Parameters.AddWithValue("@municipio", e.Municipality);
                cmd.Parameters.AddWithValue("@estado", e.State);
                cmd.Parameters.AddWithValue("@telefono", e.CellPhone);
                cmd.Parameters.AddWithValue("@escolaridad", e.Schooling);
                cmd.Parameters.AddWithValue("@profesion", e.Profession);
                cmd.Parameters.AddWithValue("@anios_reparacion", e.YearRepair);
                cmd.Parameters.AddWithValue("@anios_zafra", e.YearHarvest);
                cmd.Parameters.AddWithValue("@dias_aguinaldo_rep", e.BonusDaysRepair);
                cmd.Parameters.AddWithValue("@dias_aguinaldo_zaf", e.BonusDaysHarvest);
                cmd.Parameters.AddWithValue("@dias_vacaciones_rep", e.VacationDaysRepair);
                cmd.Parameters.AddWithValue("@dias_vacaciones_zaf", e.VacationDaysHarvest);
                cmd.Parameters.AddWithValue("@dias_comision", e.DaysCommission);
                cmd.Parameters.AddWithValue("@declara", e.Declare);
                cmd.Parameters.AddWithValue("@fotografia", e.Photo);
                cmd.Parameters.AddWithValue("@ausentismo", e.Absenteeism);
                cmd.Parameters.AddWithValue("@curp", e.CURP);
                cmd.Parameters.AddWithValue("@patron", e.Pattern);
                cmd.Parameters.AddWithValue("@dias_presencia_ant", e.PresenceDaysPrevious);
                cmd.Parameters.AddWithValue("@dias_puntualidad_ant", e.PunctualityDaysPrevious);
                cmd.Parameters.AddWithValue("@dias_presencia_actual", e.PresenceDaysCurrent); 
                cmd.Parameters.AddWithValue("@dias_puntualidad_actual", e.PunctualityDaysCurrent);
                cmd.Parameters.AddWithValue("@domingos_laborados_zafra", e.WorkedSundayHarvest);
                cmd.Parameters.AddWithValue("@cuenta_bancaria", e.AccountBank);
                cmd.Parameters.AddWithValue("@clabe_interbancaria", e.InterbankKey);
                cmd.Parameters.AddWithValue("@aplicar_cuota_sindical", e.ApplyUnionDues);
                cmd.Parameters.AddWithValue("@fecha_plaza", Convert.ToDateTime(e.DatePlaza).ToString("yyyy-MM-dd"));
                //cmd.Parameters.AddWithValue("@uso_zafra", e.UsoZafra);
                //cmd.Parameters.AddWithValue("@uso_reparacion", e.UsoReparacion);

                Conex.OpenNomina();
                return cmd.ExecuteNonQuery(); // Devuelve 1 si fue exitoso
            }
        }

        public int DeleteEmployee(int Id)
        {
            string query = "DELETE FROM empleados WHERE id = '" + Id + @"'";
            using (cmd = new SqlCommand(query, Conex.nomi))
            {
                Conex.OpenNomina();
                return cmd.ExecuteNonQuery();
            }
        }

        public bool ExistsId(int Id)
        {
            throw new NotImplementedException();
        }

        public List<Employee> FilterByName(string Name)
        {
            string filtro = (Name ?? "").Trim();
            List<Employee> list = new List<Employee>();

            try
            {

                using (cmd = new SqlCommand($" SELECT * FROM empleados WHERE CONCAT(nombre, ' ', apellido_paterno, ' ', apellido_materno) LIKE '%{Name}%' ", Conex.nomi))
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

                throw new Exception("Error al obtener las plantillas: " + ex.Message);
            }
            finally
            {

                Conex.CloseNomina();
            }

            return list;
        }

        public Employee GetEmployee(int Id)
        {
            using (cmd = new SqlCommand("SELECT * FROM empleados WHERE id = @id", Conex.nomi))
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

        public Employee GetEmployeeByNumber(int NumberEmployee)
        {
            using (cmd = new SqlCommand("SELECT * FROM empleados WHERE numero_empleado = @numero_empleado", Conex.nomi))
            {

                cmd.Parameters.AddWithValue("@numero_empleado", NumberEmployee);

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

        public List<Employee> GetEmployees()
        {
            List<Employee> list = new List<Employee>();

            try
            {

                using (cmd = new SqlCommand("SELECT * FROM empleados WHERE nombre <> 'NULL' AND numero_empleado <> 42 AND numero_empleado <> 18 ORDER BY numero_empleado ASC", Conex.nomi))
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

        public int UpdateEmployee(Employee e)
        {
            string query = "UPDATE empleados Set numero_credencial=@numero_credencial,nombre=@nombre,apellido_paterno=@apellido_paterno,apellido_materno=@apellido_materno,rfc=@rfc,imss=@imss,modulo18=@modulo18,categoria_zafra=@categoria_zafra,categoria_reparacion=@categoria_reparacion,estatus=@estatus, " +
                                                      " estatus_fecha = @estatus_fecha,tipo = @tipo,clasificacion = @clasificacion,zona = @zona,sexo = @sexo,estado_civil = @estado_civil,lugar_nacimiento = @lugar_nacimiento,fecha_nacimiento = @fecha_nacimiento,padre_nombre = @padre_nombre,madre_nombre = @madre_nombre,domicilio = @domicilio,colonia = @colonia,codigo_postal = @codigo_postal,ciudad = @ciudad,municipio = @municipio,estado = @estado, " +
                                                      " telefono = @telefono,escolaridad = @escolaridad,profesion = @profesion,anios_reparacion = @anios_reparacion,anios_zafra = @anios_zafra,dias_aguinaldo_rep = @dias_aguinaldo_rep,dias_aguinaldo_zaf = @dias_aguinaldo_zaf,dias_vacaciones_rep = @dias_vacaciones_rep,dias_vacaciones_zaf = @dias_vacaciones_zaf,dias_comision = @dias_comision, " +
                                                      " declara = @declara,fotografia = @fotografia,ausentismo = @ausentismo,curp = @curp,patron = @patron,dias_presencia_ant = @dias_presencia_ant,dias_puntualidad_ant = @dias_puntualidad_ant,dias_presencia_actual = @dias_presencia_actual,dias_puntualidad_actual = @dias_puntualidad_actual,domingos_laborados_zafra = @domingos_laborados_zafra,cuenta_bancaria = @cuenta_bancaria,clabe_interbancaria = @clabe_interbancaria,aplicar_cuota_sindical = @aplicar_cuota_sindical,fecha_plaza = @fecha_plaza " +
                                                      " WHERE numero_empleado = @numero_empleado ";
            //,uso_zafra,uso_reparacion
            //, @uso_zafra, @uso_reparacion
            using (cmd = new SqlCommand(query, Conex.nomi))
            {
                // Uso de parámetros para evitar inyección SQL
                cmd.Parameters.AddWithValue("@id_nomina", e.PayrollId);
                cmd.Parameters.AddWithValue("@numero_empleado", e.NumberEmployee);
                cmd.Parameters.AddWithValue("@numero_credencial", e.NumberCredential);
                cmd.Parameters.AddWithValue("@nombre", e.Name);
                cmd.Parameters.AddWithValue("@apellido_paterno", e.LastnameFather);
                cmd.Parameters.AddWithValue("@apellido_materno", e.LastnameMother);
                cmd.Parameters.AddWithValue("@rfc", e.RFC);
                cmd.Parameters.AddWithValue("@imss", e.Imss);
                cmd.Parameters.AddWithValue("@modulo18", e.Module18);
                cmd.Parameters.AddWithValue("@categoria_zafra", e.CategoryHarvest);
                cmd.Parameters.AddWithValue("@categoria_reparacion", e.CategoryRepair);
                cmd.Parameters.AddWithValue("@estatus", e.Status);
                cmd.Parameters.AddWithValue("@estatus_fecha", Convert.ToDateTime(e.StatusDate).ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@tipo", e.Type);
                cmd.Parameters.AddWithValue("@clasificacion", e.Classification);
                cmd.Parameters.AddWithValue("@zona", e.Area);
                cmd.Parameters.AddWithValue("@sexo", e.Sex);
                cmd.Parameters.AddWithValue("@estado_civil", e.MaritalStatus);
                cmd.Parameters.AddWithValue("@lugar_nacimiento", e.PlaceBirth);
                cmd.Parameters.AddWithValue("@fecha_nacimiento", Convert.ToDateTime(e.DateBirth).ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@padre_nombre", e.FatherName);
                cmd.Parameters.AddWithValue("@madre_nombre", e.MotherName);
                cmd.Parameters.AddWithValue("@domicilio", e.Address);
                cmd.Parameters.AddWithValue("@colonia", e.Cologne);
                cmd.Parameters.AddWithValue("@codigo_postal", e.ZipCode);
                cmd.Parameters.AddWithValue("@ciudad", e.City);
                cmd.Parameters.AddWithValue("@municipio", e.Municipality);
                cmd.Parameters.AddWithValue("@estado", e.State);
                cmd.Parameters.AddWithValue("@telefono", e.CellPhone);
                cmd.Parameters.AddWithValue("@escolaridad", e.Schooling);
                cmd.Parameters.AddWithValue("@profesion", e.Profession);
                cmd.Parameters.AddWithValue("@anios_reparacion", e.YearRepair);
                cmd.Parameters.AddWithValue("@anios_zafra", e.YearHarvest);
                cmd.Parameters.AddWithValue("@dias_aguinaldo_rep", e.BonusDaysRepair);
                cmd.Parameters.AddWithValue("@dias_aguinaldo_zaf", e.BonusDaysHarvest);
                cmd.Parameters.AddWithValue("@dias_vacaciones_rep", e.VacationDaysRepair);
                cmd.Parameters.AddWithValue("@dias_vacaciones_zaf", e.VacationDaysHarvest);
                cmd.Parameters.AddWithValue("@dias_comision", e.DaysCommission);
                cmd.Parameters.AddWithValue("@declara", e.Declare);
                cmd.Parameters.AddWithValue("@fotografia", e.Photo);
                cmd.Parameters.AddWithValue("@ausentismo", e.Absenteeism);
                cmd.Parameters.AddWithValue("@curp", e.CURP);
                cmd.Parameters.AddWithValue("@patron", e.Pattern);
                cmd.Parameters.AddWithValue("@dias_presencia_ant", e.PresenceDaysPrevious);
                cmd.Parameters.AddWithValue("@dias_puntualidad_ant", e.PunctualityDaysPrevious);
                cmd.Parameters.AddWithValue("@dias_presencia_actual", e.PresenceDaysCurrent);
                cmd.Parameters.AddWithValue("@dias_puntualidad_actual", e.PunctualityDaysCurrent);
                cmd.Parameters.AddWithValue("@domingos_laborados_zafra", e.WorkedSundayHarvest);
                cmd.Parameters.AddWithValue("@cuenta_bancaria", e.AccountBank);
                cmd.Parameters.AddWithValue("@clabe_interbancaria", e.InterbankKey);
                cmd.Parameters.AddWithValue("@aplicar_cuota_sindical", e.ApplyUnionDues);
                cmd.Parameters.AddWithValue("@fecha_plaza", Convert.ToDateTime(e.DatePlaza).ToString("yyyy-MM-dd"));
                //cmd.Parameters.AddWithValue("@uso_zafra", e.UsoZafra);
                //cmd.Parameters.AddWithValue("@uso_reparacion", e.UsoReparacion);

                Conex.OpenNomina();
                return cmd.ExecuteNonQuery(); // Devuelve 1 si fue exitoso
            }
        }

        public void ExportToExcel(List<EmployeeReport> list, string filePath)
        {
            using (var workbook = new XLWorkbook())
            {
                var ws = workbook.Worksheets.Add("Empleados");

                // 1. Definir tus encabezados personalizados manualmente
                string[] headers = {
            "ID", "NUMERO EMPLEADO", "NOMBRE", "APELLIDO PATERNO", "APELLIDO MATERNO", "IMSS",
            "CURP","TIPO", "CLASIFICACION", "CATEGORIA ZAFRA", "CATEGORIA REPARACION", "RFC",
            "ESTATUS"
        };

                // 2. Escribir encabezados en la fila 1
                for (int i = 0; i < headers.Length; i++)
                {
                    var cell = ws.Cell(1, i + 1);
                    cell.Value = headers[i];
                    cell.Style.Font.Bold = true;
                    cell.Style.Fill.BackgroundColor = XLColor.FromHtml("#2c3e50"); // Color elegante
                    cell.Style.Font.FontColor = XLColor.White;
                }

                // 3. Llenar los datos manualmente fila por fila
                int row = 2;
                foreach (var item in list)
                {
                    ws.Cell(row, 1).Value = item.Id;
                    ws.Cell(row, 2).Value = item.NumberEmployee;
                    ws.Cell(row, 3).Value = item.Name;
                    ws.Cell(row, 4).Value = item.LastnameFather;
                    ws.Cell(row, 5).Value = item.LastnameMother;
                    ws.Cell(row, 6).Value = item.Imss;
                    ws.Cell(row, 7).Value = item.CURP;
                    ws.Cell(row, 8).Value = item.Type;
                    ws.Cell(row, 9).Value = item.Classification;
                    ws.Cell(row, 10).Value = item.CategoryHarvest;
                    ws.Cell(row, 11).Value = item.CategoryRepair;
                    ws.Cell(row, 12).Value = item.RFC;
                    ws.Cell(row, 13).Value = item.Status;
                    row++;
                }

                // Ajustar columnas
                ws.Columns().AdjustToContents();

                workbook.SaveAs(filePath);
            }
        }

        public List<EmployeeReport> GetDataPrintEmployees()
        {
            List<EmployeeReport> list = new List<EmployeeReport>();

            try
            {

                using (cmd = new SqlCommand("SELECT * FROM empleados WHERE estatus = 'Activo' ORDER BY numero_empleado ASC", Conex.nomi))
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

        private static Employee ShowDataGrid(SqlDataReader reader)
        {
            return new Employee
            {
                Id = Convert.ToInt32(reader["id"]),
                PayrollId = Convert.ToInt32(reader["id_nomina"]),
                NumberEmployee = Convert.ToInt64(reader["numero_empleado"]),
                NumberCredential = Convert.ToInt32(reader["numero_credencial"]),
                Name = Convert.ToString(reader["nombre"]),
                LastnameFather = Convert.ToString(reader["apellido_paterno"]),
                LastnameMother = Convert.ToString(reader["apellido_materno"]),
                RFC = Convert.ToString(reader["rfc"]),
                Imss = Convert.ToString(reader["imss"]),
                Module18 = Convert.ToInt32(reader["modulo18"]),
                CategoryHarvest = Convert.ToInt32(reader["categoria_zafra"]),
                CategoryRepair = Convert.ToInt32(reader["categoria_reparacion"]),
                Status = Convert.ToString(reader["estatus"]),
                StatusDate = Convert.ToDateTime(reader["estatus_fecha"]),
                Type = Convert.ToString(reader["tipo"]),
                Classification = Convert.ToString(reader["clasificacion"]),
                Area = Convert.ToString(reader["zona"]),
                Sex = Convert.ToString(reader["sexo"]),
                MaritalStatus = Convert.ToString(reader["estado_civil"]),
                PlaceBirth = Convert.ToString(reader["lugar_nacimiento"]),
                DateBirth = Convert.ToDateTime(reader["fecha_nacimiento"]),
                FatherName = Convert.ToString(reader["padre_nombre"]),
                MotherName = Convert.ToString(reader["madre_nombre"]),
                Address = Convert.ToString(reader["domicilio"]),
                Cologne = Convert.ToString(reader["colonia"]),
                ZipCode = Convert.ToString(reader["codigo_postal"]),
                City = Convert.ToString(reader["ciudad"]),
                Municipality = Convert.ToString(reader["municipio"]),
                State = Convert.ToString(reader["estado"]),
                CellPhone = Convert.ToString(reader["telefono"]),
                Schooling = Convert.ToString(reader["escolaridad"]),
                Profession = Convert.ToString(reader["profesion"]),
                YearRepair = Convert.ToInt32(reader["anios_reparacion"]),
                YearHarvest = Convert.ToInt32(reader["anios_zafra"]),
                BonusDaysRepair = Convert.ToInt32(reader["dias_aguinaldo_rep"]),
                BonusDaysHarvest = Convert.ToInt32(reader["dias_aguinaldo_zaf"]),
                VacationDaysRepair = Convert.ToInt32(reader["dias_vacaciones_rep"]),
                VacationDaysHarvest = Convert.ToInt32(reader["dias_vacaciones_zaf"]),
                DaysCommission = Convert.ToInt32(reader["dias_comision"]),
                Declare = Convert.ToString(reader["declara"]),
                Photo = Convert.ToString(reader["fotografia"]),
                Absenteeism = Convert.ToString(reader["ausentismo"]),
                CURP = Convert.ToString(reader["curp"]),
                Pattern = Convert.ToString(reader["patron"]),
                PresenceDaysPrevious = Convert.ToInt32(reader["dias_presencia_ant"]),
                PunctualityDaysPrevious = Convert.ToInt32(reader["dias_puntualidad_ant"]),
                PresenceDaysCurrent = Convert.ToInt32(reader["dias_presencia_actual"]),
                PunctualityDaysCurrent = Convert.ToInt32(reader["dias_presencia_actual"]),
                WorkedSundayHarvest = Convert.ToInt32(reader["domingos_laborados_zafra"]),
                AccountBank = Convert.ToString(reader["cuenta_bancaria"]),
                InterbankKey = Convert.ToString(reader["clabe_interbancaria"]),
                ApplyUnionDues = Convert.ToInt32(reader["aplicar_cuota_sindical"]),
                DatePlaza = Convert.ToDateTime(reader["fecha_plaza"])

            };
        }

        private static EmployeeReport ShowDataReport(SqlDataReader reader)
        {
            return new EmployeeReport
            {
                Id = Convert.ToInt32(reader["id"]),
                NumberEmployee = Convert.ToInt64(reader["numero_empleado"]),         
                Name = Convert.ToString(reader["nombre"]),
                LastnameFather = Convert.ToString(reader["apellido_paterno"]),
                LastnameMother = Convert.ToString(reader["apellido_materno"]),
                RFC = Convert.ToString(reader["rfc"]),
                Imss = Convert.ToString(reader["imss"]),             
                CategoryHarvest = Convert.ToInt32(reader["categoria_zafra"]),
                CategoryRepair = Convert.ToInt32(reader["categoria_reparacion"]),
                Status = Convert.ToString(reader["estatus"]),
                Type = Convert.ToString(reader["tipo"]),
                Classification = Convert.ToString(reader["clasificacion"]),          
                CURP = Convert.ToString(reader["curp"]),
            };
        }
    }
}
