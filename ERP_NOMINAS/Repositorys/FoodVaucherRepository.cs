using ClosedXML.Excel;
using ERP_NOMINAS.Conexion;
using ERP_NOMINAS.GlobalFunctions;
using ERP_NOMINAS.Models.FoodVauchers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Repositorys
{
    public class FoodVaucherRepository : IFoodVaucher
    {
        ConnectorSql Conex = new ConnectorSql();
        Utilities Util = new Utilities();
        SqlCommand cmd = new SqlCommand();

        public decimal GetImportFoodVaucher()
        {
            using (cmd = new SqlCommand("SELECT valor_vales_despensa FROM parametros", Conex.nomi))
            {
                Conex.OpenNomina();
                return Convert.ToDecimal(cmd.ExecuteScalar());
            }
        }

        public List<FoodVaucher> GetListAttend(DateTime Start, DateTime End,int WorkedDays)
        {
            List<FoodVaucher> list = new List<FoodVaucher>();

            try
            {

                        using (cmd = new SqlCommand($@"
                                        DECLARE @columns nvarchar(MAX);
                                        DECLARE @columns_isnull nvarchar(MAX);
                                        DECLARE @sql nvarchar(MAX);
                                        DECLARE @sum_columns nvarchar(MAX);
                                        DECLARE @valorDespensa decimal(18,2);

                                        SELECT TOP 1 @valorDespensa = valor_vales_despensa FROM parametros;

                                        WITH Fechas AS (
                                            SELECT CAST('{Start}' AS DATE) AS Fecha
                                            UNION ALL
                                            SELECT DATEADD(DAY, 1, Fecha)
                                            FROM Fechas
                                            WHERE Fecha < CAST('{End}' AS DATE)
                                        )
                                        SELECT 
                                            @columns = STUFF((SELECT ',' + QUOTENAME(LTRIM(Fecha)) 
                                                              FROM Fechas ORDER BY Fecha 
                                                              FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, ''),
                                            @columns_isnull = STUFF((SELECT ', ISNULL(' + QUOTENAME(LTRIM(Fecha)) + ', 0) AS ' + QUOTENAME(LTRIM(Fecha))
                                                                     FROM Fechas ORDER BY Fecha 
                                                                     FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, ''),
                                            @sum_columns = STUFF((SELECT ' + CASE WHEN ' + QUOTENAME(LTRIM(Fecha)) + ' > 0 THEN 1 ELSE 0 END'
                                                                     FROM Fechas 
                                                                     FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 3, '')
                                        OPTION (MAXRECURSION 0);

                                        SET @sql = N'
                                        SELECT 
                                            numero_empleado, 
                                            n_completo, 
                                            @dLaborados AS dias_habiles,
                                            ( ' + @sum_columns + N' ) AS dias_laborados,
                                            CAST((( ' + @sum_columns + N' ) * (@vDespensa / CAST(@dLaborados AS FLOAT))) AS DECIMAL(18,2)) AS importe,
                                            ' + @columns_isnull + N'                                        
                                            
                                        FROM (
                                            SELECT 
                                                ld.numero_empleado, 
                                                CONCAT(e.nombre, '' '', e.apellido_paterno, '' '', e.apellido_materno) as n_completo,
                                                le.fecha AS dias,                                                  
                                                CASE 
                                                    WHEN DATENAME(WEEKDAY, le.fecha) IN (''Sunday'', ''Domingo'') THEN 0
                                                    WHEN f.fecha IS NOT NULL THEN 0
                                                    WHEN ld.asistencia IN (1, 2) THEN 8
                                                    ELSE 0 
                                                END AS s_aplica
                                            FROM azsja_nomina.dbo.listas_detalle ld
                                            INNER JOIN azsja_nomina.dbo.listas_encabezado le ON ld.numero_control = le.numero_control
                                            INNER JOIN azsja_nomina.dbo.empleados e ON e.numero_empleado = ld.numero_empleado
                                            LEFT JOIN festivos f ON le.fecha = f.fecha
                                            WHERE le.fecha BETWEEN ''{Start}'' AND ''{End}''
                                              AND e.estatus = ''Activo''
                                        ) AS T
                                        PIVOT (
                                            SUM(s_aplica)
                                            FOR dias IN(' + @columns + N')
                                        ) AS P
                                        ORDER BY numero_empleado;';

                                        EXEC sp_executesql @sql, 
                                             N'@vDespensa decimal(18,2), @dLaborados int', 
                                             @valorDespensa, 
                                             @diasLaboradosParam;", Conex.nomi))

                {
                    cmd.Parameters.AddWithValue("@diasLaboradosParam", WorkedDays);
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

                throw new Exception("Error al obtener las asistencias: " + ex.Message);
            }
            finally
            {

                Conex.CloseNomina();
            }

            return list;
        }

        public int SaveFoodVauchers(List<FoodVaucher> list, int PayWeek)
        {
            Conex.OpenNomina();

            SqlTransaction tra = Conex.nomi.BeginTransaction();

            try
            {
                string queryFoodV = @"INSERT INTO vales_despensa (
                        nomina, numero_empleado, dias_laborados, importe, semana_pago, 
                        d1, d2, d3, d4, d5, d6, d7, d8, d9, d10, 
                        d11, d12, d13, d14, d15, d16, d17, d18, d19, d20, 
                        d21, d22, d23, d24, d25, d26, d27, d28, d29, d30, d31
                      ) VALUES (
                        @nomina, @numero_empleado, @dias_laborados, @importe, @semana_pago, 
                        @d1, @d2, @d3, @d4, @d5, @d6, @d7, @d8, @d9, @d10, 
                        @d11, @d12, @d13, @d14, @d15, @d16, @d17, @d18, @d19, @d20, 
                        @d21, @d22, @d23, @d24, @d25, @d26, @d27, @d28, @d29, @d30, @d31
                      )";

                using (SqlCommand cmd = new SqlCommand(queryFoodV, Conex.nomi, tra))
                {
                    cmd.Parameters.Add("@nomina", SqlDbType.Int);
                    cmd.Parameters.Add("@numero_empleado", SqlDbType.Int);
                    cmd.Parameters.Add("@dias_laborados", SqlDbType.Int);
                    cmd.Parameters.Add("@importe", SqlDbType.Decimal);
                    cmd.Parameters.Add("@semana_pago", SqlDbType.Int);

                    for (int i = 1; i <= 31; i++)
                    {
                        cmd.Parameters.Add($"@d{i}", SqlDbType.TinyInt); 
                    }

                    foreach (var det in list)
                    {
                        cmd.Parameters["@nomina"].Value = 1;
                        cmd.Parameters["@numero_empleado"].Value = det.NumberEmployee;
                        cmd.Parameters["@dias_laborados"].Value = det.WorkedDays;
                        cmd.Parameters["@importe"].Value = det.Amount;
                        cmd.Parameters["@semana_pago"].Value = PayWeek;

                        for (int i = 1; i <= 31; i++)
                        {
                            var propertyInfo = det.GetType().GetProperty($"D{i}");
                            if (propertyInfo != null)
                            {
                                cmd.Parameters[$"@d{i}"].Value = propertyInfo.GetValue(det);
                            }
                            else
                            {
                                cmd.Parameters[$"@d{i}"].Value = 0;
                            }
                        }

                        cmd.ExecuteNonQuery();
                    }
                }

                tra.Commit();
                return 1;
            }
            catch (Exception)
            {
                tra.Rollback();
                throw;
            }
            finally
            {
                Conex.nomi.Close();
            }
        }

        public List<FoodVaucherReportExcel> GetDataFoodVaucherExcel(int Payweek)
        {
            List<FoodVaucherReportExcel> list = new List<FoodVaucherReportExcel>();

            try
            {

                using (cmd = new SqlCommand($@"SELECT v.*,CONCAT(e.nombre, ' ', e.apellido_paterno, ' ', e.apellido_materno) AS n_completo 
                                                FROM vales_despensa v
                                                INNER JOIN empleados e
                                                ON v.numero_empleado = e.numero_empleado
                                                WHERE v.semana_pago = {Payweek} ORDER BY v.numero_empleado ASC", Conex.nomi))
                {
                    Conex.OpenNomina();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            list.Add(ShowDataExcel(reader));
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Error al obtener los vales de despensa: " + ex.Message);
            }
            finally
            {

                Conex.CloseNomina();
            }

            return list;
        }

        public List<FoodVaucherReport> GetDataReport(int Payweek)
        {
            List<FoodVaucherReport> list = new List<FoodVaucherReport>();

            try
            {

                using (cmd = new SqlCommand($@"SELECT vd.numero_empleado ,CONCAT(e.nombre,' ',e.apellido_paterno,' ',e.apellido_materno) AS n_completo, 
                                            vd.dias_laborados AS d_trabajados, vd.importe
                                            FROM vales_despensa vd
                                            INNER JOIN empleados e
                                            ON e.numero_empleado = vd.numero_empleado
                                            WHERE vd.semana_pago =  {Payweek}                                           
                                            ORDER By vd.numero_empleado", Conex.nomi))
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

                throw new Exception("Error al obtener los vales de despensa: " + ex.Message);
            }
            finally
            {

                Conex.CloseNomina();
            }

            return list;
        }

        public void ExportToExcel(List<FoodVaucherReportExcel> list, string filePath)
        {
            if (list == null) list = new List<FoodVaucherReportExcel>();

            using (var workbook = new XLWorkbook())
            {
                var ws = workbook.Worksheets.Add("Vales Despensa");

                var headers = new List<string> {
            "Id", "No. Empleado", "Nombre Completo", "Días Laborados", "Importe", "Semana Pago"
        };

                for (int d = 1; d <= 31; d++)
                {
                    headers.Add($"Día {d}");
                }

                for (int i = 0; i < headers.Count; i++)
                {
                    ws.Cell(1, i + 1).SetValue(headers[i]);
                }

                var headerRange = ws.Range(1, 1, 1, headers.Count);
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Font.FontColor = XLColor.White;
                headerRange.Style.Fill.BackgroundColor = XLColor.FromHtml("#2C3E50");
                headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                int row = 2;
                foreach (var item in list)
                {
                    ws.Cell(row, 1).SetValue(item.Id);
                    ws.Cell(row, 2).SetValue(item.NumberEmployee);
                    ws.Cell(row, 3).SetValue(item.FullName);
                    ws.Cell(row, 4).SetValue(item.WorkedDays);
                    ws.Cell(row, 5).SetValue(item.Amount);
                    ws.Cell(row, 6).SetValue(item.Payweek);

                    var dias = new int[] {
                item.D1, item.D2, item.D3, item.D4, item.D5, item.D6, item.D7, item.D8, item.D9, item.D10,
                item.D11, item.D12, item.D13, item.D14, item.D15, item.D16, item.D17, item.D18, item.D19, item.D20,
                item.D21, item.D22, item.D23, item.D24, item.D25, item.D26, item.D27, item.D28, item.D29, item.D30, item.D31
            };

                    for (int d = 0; d < 31; d++)
                    {
                        ws.Cell(row, 7 + d).SetValue(dias[d]);
                    }

                    row++;
                }

                int lastRow = row > 2 ? row - 1 : 1;

                if (lastRow > 1)
                {
                    ws.Range(2, 5, lastRow, 5).Style.NumberFormat.Format = "$#,##0.00";
                    ws.Range(2, 4, lastRow, 4).Style.NumberFormat.Format = "#,##0";

                    ws.Range(2, 1, lastRow, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Range(2, 6, lastRow, headers.Count).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                    var dataRange = ws.Range(1, 1, lastRow, headers.Count);
                    dataRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    dataRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                }

                ws.Columns().AdjustToContents();
                workbook.SaveAs(filePath);
            }
        }

        private FoodVaucher ShowDataGrid(SqlDataReader reader)
        {
            byte GetByteSafe(SqlDataReader r, int index)
            {
                if (index < r.FieldCount && !r.IsDBNull(index))
                {
                    return Convert.ToByte(r[index]);
                }
                return 0; 
            }

            return new FoodVaucher
            {

                Id = Convert.ToInt32(0),
                NumberEmployee = Convert.ToInt32(reader["numero_empleado"]),
                FullName = Convert.ToString(reader["n_completo"]),
                BusinessDays = Convert.ToInt32(reader["dias_habiles"]),
                WorkedDays = Convert.ToInt32(reader["dias_laborados"]),
                Amount = Convert.ToDecimal(reader["importe"]) > GetImportFoodVaucher() ? GetImportFoodVaucher() : Convert.ToDecimal(reader["importe"]) ,
                D1 = Convert.ToByte(reader[5]),
                D2 = Convert.ToByte(reader[6]),
                D3 = Convert.ToByte(reader[7]),
                D4 = Convert.ToByte(reader[8]),
                D5 = Convert.ToByte(reader[9]),
                D6 = Convert.ToByte(reader[10]),
                D7 = Convert.ToByte(reader[11]),
                D8 = Convert.ToByte(reader[12]),
                D9 = Convert.ToByte(reader[13]),
                D10 = Convert.ToByte(reader[14]),
                D11 = Convert.ToByte(reader[15]),
                D12 = Convert.ToByte(reader[16]),
                D13 = Convert.ToByte(reader[17]),
                D14 = Convert.ToByte(reader[18]),
                D15 = Convert.ToByte(reader[19]),
                D16 = Convert.ToByte(reader[20]),
                D17 = Convert.ToByte(reader[21]),
                D18 = Convert.ToByte(reader[22]),
                D19 = Convert.ToByte(reader[23]),
                D20 = Convert.ToByte(reader[24]),
                D21 = Convert.ToByte(reader[25]),
                D22 = Convert.ToByte(reader[26]),
                D23 = Convert.ToByte(reader[27]),
                D24 = Convert.ToByte(reader[28]),
                D25 = Convert.ToByte(reader[29]),
                D26 = Convert.ToByte(reader[30]),
                D27 = Convert.ToByte(reader[31]),
                D28 = GetByteSafe(reader,32),
                D29 = GetByteSafe(reader,33),
                D30 = GetByteSafe(reader,34),
                D31 = GetByteSafe(reader,35),
                PayrollId = Convert.ToInt32(1),
                Payweek = Convert.ToInt32(0),
            };
        }

        private FoodVaucherReportExcel ShowDataExcel(SqlDataReader reader)
        {
            // Función auxiliar corregida para retornar int y leer por nombre de columna de forma segura
            int GetIntSafe(SqlDataReader r, string columnName)
            {
                // Verificar si la columna existe en el reader y no es nula
                for (int i = 0; i < r.FieldCount; i++)
                {
                    if (r.GetName(i).Equals(columnName, StringComparison.OrdinalIgnoreCase))
                    {
                        return r.IsDBNull(i) ? 0 : Convert.ToInt32(r[i]);
                    }
                }
                return 0; // Si la columna no viene en el SELECT (ej. D29, D30, D31 en meses cortos)
            }

            return new FoodVaucherReportExcel
            {
                Id = Convert.ToInt32(reader["id"]),
                NumberEmployee = Convert.ToInt32(reader["numero_empleado"]),
                FullName = Convert.ToString(reader["n_completo"]),
                WorkedDays = Convert.ToInt32(reader["dias_laborados"]),

                // Validación de importe
                Amount = Convert.ToDecimal(reader["importe"]) > GetImportFoodVaucher()
                    ? GetImportFoodVaucher()
                    : Convert.ToDecimal(reader["importe"]),

                // CORRECCIÓN DEFINITIVA: Mapeo por nombre de columna convertido a Int32
                D1 = Convert.ToInt32(reader["d1"]),
                D2 = Convert.ToInt32(reader["d2"]),
                D3 = Convert.ToInt32(reader["d3"]),
                D4 = Convert.ToInt32(reader["d4"]),
                D5 = Convert.ToInt32(reader["d5"]),
                D6 = Convert.ToInt32(reader["d6"]),
                D7 = Convert.ToInt32(reader["d7"]),
                D8 = Convert.ToInt32(reader["d8"]),
                D9 = Convert.ToInt32(reader["d9"]),
                D10 = Convert.ToInt32(reader["d10"]),
                D11 = Convert.ToInt32(reader["d11"]),
                D12 = Convert.ToInt32(reader["d12"]),
                D13 = Convert.ToInt32(reader["d13"]),
                D14 = Convert.ToInt32(reader["d14"]),
                D15 = Convert.ToInt32(reader["d15"]),
                D16 = Convert.ToInt32(reader["d16"]),
                D17 = Convert.ToInt32(reader["d17"]),
                D18 = Convert.ToInt32(reader["d18"]),
                D19 = Convert.ToInt32(reader["d19"]),
                D20 = Convert.ToInt32(reader["d20"]),
                D21 = Convert.ToInt32(reader["d21"]),
                D22 = Convert.ToInt32(reader["d22"]),
                D23 = Convert.ToInt32(reader["d23"]),
                D24 = Convert.ToInt32(reader["d24"]),
                D25 = Convert.ToInt32(reader["d25"]),
                D26 = Convert.ToInt32(reader["d26"]),
                D27 = Convert.ToInt32(reader["d27"]),

                // Días finales usando la función segura por si el mes no tiene 31 días
                D28 = GetIntSafe(reader, "d28"),
                D29 = GetIntSafe(reader, "d29"),
                D30 = GetIntSafe(reader, "d30"),
                D31 = GetIntSafe(reader, "d31"),

                //PayrollId = Convert.ToInt32(reader["nomina"]),
                Payweek = Convert.ToInt32(reader["semana_pago"])
            };
        }

        private FoodVaucherReport ShowDataReport(SqlDataReader reader)
        {
            return new FoodVaucherReport
            {
                NumberEmployee = Convert.ToInt32(reader["numero_empleado"]),
                FullName = Convert.ToString(reader["n_completo"]),
                WorkDays = Convert.ToInt32(reader["d_trabajados"]),
                Amount = Convert.ToDecimal(reader["importe"])

            };
        }

    }
}
