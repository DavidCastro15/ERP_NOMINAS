using ClosedXML.Excel;
using ERP_NOMINAS.Conexion;
using ERP_NOMINAS.GlobalFunctions;
using ERP_NOMINAS.Models.Category;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERP_NOMINAS.Repositorys
{
    public class CategoryRepository : ICategory
    {
        ConnectorSql Conex = new ConnectorSql();
        Utilities Util = new Utilities();
        SqlCommand cmd = new SqlCommand();

        public int CreateCategory(Category c)
        {

            string query = "INSERT INTO categorias(id_categoria,nombre_categoria,salario,tipo,alimentos,alturas_temp,salario_turno1,salario_turno2,salario_turno3,salario_promedio,salario_promedio_t1y2,escalafon,prioridad,clasificacion,herramienta)" +
            "VALUES(@id_categoria, @nombre_categoria, @salario, @tipo, @alimentos, @alturas_temp, @salario_turno1, @salario_turno2, @salario_turno3, @salario_promedio, @salario_promedio_t1y2, @escalafon, @prioridad, @clasificacion, @herramienta)";

            using (cmd = new SqlCommand(query, Conex.nomi))
            {
                // Uso de parámetros para evitar inyección SQL
                cmd.Parameters.AddWithValue("@id_categoria", c.IdCategory);
                cmd.Parameters.AddWithValue("@nombre_categoria", c.Name);
                cmd.Parameters.AddWithValue("@salario", c.Salary);
                cmd.Parameters.AddWithValue("@tipo", c.Type);
                cmd.Parameters.AddWithValue("@alimentos", c.Food);
                cmd.Parameters.AddWithValue("@alturas_temp", c.HeightsTemperatures);
                cmd.Parameters.AddWithValue("@salario_turno1", c.SalaryTurn1);
                cmd.Parameters.AddWithValue("@salario_turno2", c.SalaryTurn2);
                cmd.Parameters.AddWithValue("@salario_turno3", c.SalaryTurn3);
                cmd.Parameters.AddWithValue("@salario_promedio", c.AverageSalary);
                cmd.Parameters.AddWithValue("@salario_promedio_t1y2", c.AverageSalaryT1xT2);
                cmd.Parameters.AddWithValue("@escalafon", c.Ranking);
                cmd.Parameters.AddWithValue("@prioridad", c.Priority);
                cmd.Parameters.AddWithValue("@clasificacion", c.Classified);
                cmd.Parameters.AddWithValue("@herramienta", c.ToolWear);

                Conex.OpenNomina();
                return cmd.ExecuteNonQuery(); // Devuelve 1 si fue exitoso
            }

        }

        public int DeleteCategory(int IdCategory)
        {
            string query = "DELETE FROM categorias WHERE id_categoria = '" + IdCategory + @"'";
            using (cmd = new SqlCommand(query, Conex.nomi))
            {
                Conex.OpenNomina();
                return cmd.ExecuteNonQuery();
            }
        }

        public List<Category> GetCategories()
        {
            List<Category> list = new List<Category>();

            try
            {
                
                using ( cmd = new SqlCommand("SELECT * FROM categorias ORDER BY id_categoria ASC", Conex.nomi))
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
               
                throw new Exception("Error al obtener las categorías: " + ex.Message);
            }
            finally
            {
            
                Conex.CloseNomina();
            }

            return list;
        }

        public Category GetCategory(int IdCategory)
        {
            using (cmd = new SqlCommand("SELECT * FROM categorias WHERE id_categoria = @id", Conex.nomi))
            {
               
                cmd.Parameters.AddWithValue("@id", IdCategory);

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
         
        public List<Category> ShowFilterSearch(string Name)
        {
            
            List<Category> list = new List<Category>();

            try
            {
                
                using (cmd = new SqlCommand("SELECT * FROM categorias WHERE nombre_categoria LIKE '%" + Name + @"%'", Conex.nomi))
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

                throw new Exception("Error al obtener las categorías: " + ex.Message);
            }
            finally
            {

                Conex.CloseNomina();
            }

            return list;

        }

        public int UpdateCategory(Category c)
        {
            string query = "UPDATE categorias SET nombre_categoria=@nombre_categoria,salario=@salario,tipo=@tipo,alimentos=@alimentos,alturas_temp=@alturas_temp,salario_turno1=@salario_turno1,salario_turno2=@salario_turno2,salario_turno3=@salario_turno3, " +
                                                           " salario_promedio = @salario_promedio,salario_promedio_t1y2 = @salario_promedio_t1y2,escalafon = @escalafon,prioridad = @prioridad,clasificacion = @clasificacion,herramienta = @herramienta WHERE id_categoria = @id_categoria";

            using (cmd = new SqlCommand(query, Conex.nomi))
            {
                
                cmd.Parameters.AddWithValue("@id_categoria", c.IdCategory);
                cmd.Parameters.AddWithValue("@nombre_categoria", c.Name);
                cmd.Parameters.AddWithValue("@salario", c.Salary);
                cmd.Parameters.AddWithValue("@tipo", c.Type);
                cmd.Parameters.AddWithValue("@alimentos", c.Food);
                cmd.Parameters.AddWithValue("@alturas_temp", c.HeightsTemperatures);
                cmd.Parameters.AddWithValue("@salario_turno1", c.SalaryTurn1);
                cmd.Parameters.AddWithValue("@salario_turno2", c.SalaryTurn2);
                cmd.Parameters.AddWithValue("@salario_turno3", c.SalaryTurn3);
                cmd.Parameters.AddWithValue("@salario_promedio", c.AverageSalary);
                cmd.Parameters.AddWithValue("@salario_promedio_t1y2", c.AverageSalaryT1xT2);
                cmd.Parameters.AddWithValue("@escalafon", c.Ranking);
                cmd.Parameters.AddWithValue("@prioridad", c.Priority);
                cmd.Parameters.AddWithValue("@clasificacion", c.Classified);
                cmd.Parameters.AddWithValue("@herramienta", c.ToolWear);

                Conex.OpenNomina();
                return cmd.ExecuteNonQuery(); // Devuelve 1 si fue exitoso
            }
        }

        public List<CategoryReport> GetDataPrintCategories()
        {
            List<CategoryReport> list = new List<CategoryReport>();

            try
            {

                using (cmd = new SqlCommand("SELECT * FROM categorias ORDER BY id_categoria ASC", Conex.nomi))
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

                throw new Exception("Error al obtener las categorías: " + ex.Message);
            }
            finally
            {

                Conex.CloseNomina();
            }

            return list;
        }

        public int CheckNextId()
        {
            using (cmd = new SqlCommand("SELECT ISNULL(MAX(id_categoria), 0) + 1 FROM categorias", Conex.nomi))
           
            {
                Conex.OpenNomina();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }           

        }

        public void ExportToExcel(List<Category> list, string filePath)
        {
            using (var workbook = new XLWorkbook())
            {
                var ws = workbook.Worksheets.Add("Categorias");

                // 1. Definir tus encabezados personalizados manualmente
                string[] headers = {
            "ID", "CATEGORÍA", "SALARIO BASE", "TURNO 1", "TURNO 2", "TURNO 3",
            "SALARIO PROMEDIO", "SALARIO PROMEDIO T1-T2", "ALIMENTOS", "ALT/TEMP",
            "TIPO", "RANK", "PRIORIDAD", "CLASIF", "DESGASTE"
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
                    ws.Cell(row, 1).Value = item.IdCategory;
                    ws.Cell(row, 2).Value = item.Name;
                    ws.Cell(row, 3).Value = item.Salary;
                    ws.Cell(row, 4).Value = item.SalaryTurn1;
                    ws.Cell(row, 5).Value = item.SalaryTurn2;
                    ws.Cell(row, 6).Value = item.SalaryTurn3;
                    ws.Cell(row, 7).Value = item.AverageSalary;
                    ws.Cell(row, 8).Value = item.AverageSalaryT1xT2;
                    ws.Cell(row, 9).Value = item.Food;
                    ws.Cell(row, 10).Value = item.HeightsTemperatures;
                    ws.Cell(row, 11).Value = item.Type;
                    ws.Cell(row, 12).Value = item.Ranking;
                    ws.Cell(row, 13).Value = item.Priority;
                    ws.Cell(row, 14).Value = item.Classified;
                    ws.Cell(row, 15).Value = item.ToolWear;
                    row++;
                }

                // 4. Formato de moneda para salarios (Columnas C a H)
                ws.Columns("C:H").Style.NumberFormat.Format = "$ #,##0.00";

                // Ajustar columnas
                ws.Columns().AdjustToContents();

                workbook.SaveAs(filePath);
            }
        }

        private static Category ShowDataGrid(SqlDataReader reader)
        {
            return new Category
            {
                IdCategory = Convert.ToInt32(reader["id_categoria"]),
                Name = Convert.ToString(reader["nombre_categoria"]),
                Salary = Convert.ToDecimal(reader["salario"]),
                Type = Convert.ToString(reader["tipo"]),
                Food = Convert.ToString(reader["alimentos"]),
                HeightsTemperatures = Convert.ToString(reader["alturas_temp"]),
                SalaryTurn1 = Convert.ToDecimal(reader["salario_turno1"]),
                SalaryTurn2 = Convert.ToDecimal(reader["salario_turno2"]),
                SalaryTurn3 = Convert.ToDecimal(reader["salario_turno3"]),
                AverageSalary = Convert.ToDecimal(reader["salario_promedio"]),
                AverageSalaryT1xT2 = Convert.ToDecimal(reader["salario_promedio_t1y2"]),
                Ranking = Convert.ToInt32(reader["escalafon"]),
                Priority = Convert.ToInt32(reader["prioridad"] is DBNull ? null : (int?)reader["prioridad"]),
                Classified = Convert.ToString(reader["clasificacion"] as string ?? "N/A"),
                ToolWear = Convert.ToString(reader["herramienta"]),

            };
        }

        private static CategoryReport ShowDataReport(SqlDataReader reader)
        {
            return new CategoryReport
            {
                IdCategory = Convert.ToInt32(reader["id_categoria"]),
                Name = Convert.ToString(reader["nombre_categoria"]),
                Salary = Convert.ToDecimal(reader["salario"]),
                Type = Convert.ToString(reader["tipo"]),
                Food = Convert.ToString(reader["alimentos"]),
                HeightsTemperatures = Convert.ToString(reader["alturas_temp"]),
                SalaryTurn1 = Convert.ToDecimal(reader["salario_turno1"]),
                SalaryTurn2 = Convert.ToDecimal(reader["salario_turno2"]),
                SalaryTurn3 = Convert.ToDecimal(reader["salario_turno3"]),
                AverageSalary = Convert.ToDecimal(reader["salario_promedio"]),
                AverageSalaryT1xT2 = Convert.ToDecimal(reader["salario_promedio_t1y2"]),
                Ranking = Convert.ToInt32(reader["escalafon"]),
                Priority = Convert.ToInt32(reader["prioridad"] is DBNull ? null : (int?)reader["prioridad"]),
                Classified = Convert.ToString(reader["clasificacion"] as string ?? "N/A"),
                ToolWear = Convert.ToString(reader["herramienta"]),

            };
        }


    }
}
