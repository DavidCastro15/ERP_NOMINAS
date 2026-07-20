using ERP_SHARED.Conexion;
using ERP_SHARED.GlobalFunctions;
using ERP_NOMINAS.Models.Attendance;
using ERP_NOMINAS.Models.Cycles;
using ERP_NOMINAS.Models.Employees;
using ERP_NOMINAS.Models.Templates;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Repositorys
{
    public class AttendanceRepository : IAttend
    {
        ConnectorSql Conex = new ConnectorSql();
        Utilities Util = new Utilities();
        SqlCommand cmd = new SqlCommand();
        CycleRepository _repoCycle = new CycleRepository();
        EmployeeRepository _repoEmployee = new EmployeeRepository();
        TemplateRepository _repoTemplate = new TemplateRepository();

        public int ChangeTurn(int T)
        {
            string query = "UPDATE asistencia SET turno_trabajado = @turno";

            using (cmd = new SqlCommand(query, Conex.nomi))
            {

                cmd.Parameters.AddWithValue("@turno", T);

                Conex.OpenNomina();
                return cmd.ExecuteNonQuery(); // Devuelve 1 si fue exitoso
            }
        }

        public List<Attend> FilterByValue(string NameEmployee)
        {
            string column = "CONCAT(nombre, ' ',apellidos)";
            return ExecuteFilter(column, NameEmployee.ToString());
        }

        public List<Attend> FilterByValue(int NumberEmployee)
        {
            string column = "id_empleado";
            return ExecuteFilter(column, NumberEmployee.ToString());
        }

        private List<Attend> ExecuteFilter(string column, string param)
        {
            List<Attend> list = new List<Attend>();

            try
            {
                using (cmd = new SqlCommand($"SELECT * FROM asistencia WHERE {column} LIKE '%" + param + @"%'", Conex.nomi))
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

        public List<Attend> GetAttends()
        {
            List<Attend> list = new List<Attend>();

            try
            {

                using (cmd = new SqlCommand("SELECT * FROM asistencia ORDER BY id_empleado ASC", Conex.nomi))
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

        public List<Attend> GetDataFilterAttend(DateTime entryDate,TimeSpan entryTime, TimeSpan departureTime)
        {
            
            List<Attend> list = new List<Attend>();

            if (ImportListAttend(entryDate, entryTime, departureTime) == 0)
            {
                return list;
            }
            try
            {

                using (cmd = new SqlCommand("SELECT * FROM asistencia ORDER BY id_empleado", Conex.nomi))
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

                throw new Exception("Error al obtener las asistencias: " + ex.Message);
            }
            finally
            {

                Conex.CloseNomina();
            }

            return list;
        }

        public int CreateAttendanceList(string Status, string Period, int PayWeek, DateTime dateList, int Shift)
        {
          int n_control = CheckNextNumControl();

          string query = "INSERT INTO listas_encabezado(id_nomina,numero_control,fecha,turno,periodo,estatus,semana_pago) VALUES(@id_nomina,@numero_control,@fecha,@turno,@periodo,@estatus,@semana_pago)";

            using (cmd = new SqlCommand(query, Conex.nomi))
            {

                cmd.Parameters.AddWithValue("@id_nomina", 1);
                cmd.Parameters.AddWithValue("@numero_control", n_control);
                cmd.Parameters.AddWithValue("@fecha", dateList);
                cmd.Parameters.AddWithValue("@turno", Shift);
                cmd.Parameters.AddWithValue("@periodo", Period);
                cmd.Parameters.AddWithValue("@estatus", Status);
                cmd.Parameters.AddWithValue("@semana_pago", PayWeek);
                Conex.OpenNomina();

                int result = cmd.ExecuteNonQuery();
       
                if (result > 0) createDetailList(n_control, Status);

                return result;
            }     
        }

        private int createDetailList(int NumberControl,string Status)
        {
            string query = "INSERT INTO listas_detalle (numero_control,asistencia,estatus,id_nomina,numero_empleado,uso,id_categoria,categoria_requerida,hora_entrada,hora_salida,turno_periodo,turno_trabajado) "+
                           "SELECT @numero_control,@asistencia,@estatus,@id_nomina,id_empleado,uso_trabajado,categoria_original,categoria_trabajada,inicio,fin,turno_original,turno_trabajado FROM asistencia WHERE estatus =1";

            using (cmd = new SqlCommand(query, Conex.nomi))
            {
                // Uso de parámetros para evitar inyección SQL
                cmd.Parameters.AddWithValue("@numero_control", NumberControl);
                cmd.Parameters.AddWithValue("@estatus", Status);
                cmd.Parameters.AddWithValue("@asistencia", 1);
                cmd.Parameters.AddWithValue("@id_nomina", 1);

                Conex.OpenNomina();
                return cmd.ExecuteNonQuery(); // Devuelve 1 si fue exitoso
            }
        }

        public int GetListAttendByDetails(int ControlNumber)
        {

            string query = $@"TRUNCATE TABLE asistencia 
         INSERT INTO asistencia(id_empleado, nombre, apellidos, inicio, fin, fecha, tipo_empleado, 
         uso_original, uso_trabajado, turno_original, turno_trabajado, categoria_original, categoria_trabajada,estatus) 

            SELECT ld.numero_empleado AS id_empleado, 
            e.nombre, 
            CONCAT(e.apellido_paterno, ' ', e.apellido_materno) AS apellidos, 
            CAST('00:00:00' AS TIME) AS inicio, 
            CAST('00:00:00' AS TIME) AS fin, 
            CAST(GETDATE() AS DATETIME) AS fecha, 
            e.tipo, 
            ld.uso AS uso_original, 
            ld.uso AS uso_trabajado, 
            ld.turno_periodo AS turno_original, 
            ld.turno_trabajado AS turno_trabajado, 
            ld.id_categoria AS categoria_original, 
            ld.categoria_requerida AS categoria_trabajada,
            1
            FROM listas_detalle ld 
            INNER JOIN empleados e 
            ON ld.numero_empleado = e.numero_empleado 
            WHERE numero_control = { ControlNumber} ";

            using (cmd = new SqlCommand(query, Conex.nomi))
            {
                Conex.OpenNomina();
                return cmd.ExecuteNonQuery(); // Devuelve 1 si fue exitoso
            }
        }

        public int UpdateListAttendByDetails(int ControlNumber)
        {
            string query = @"
                            MERGE INTO asistencia AS destino
                            USING (
                                SELECT 
                                    e.numero_empleado AS id_empleado,
                                    ld.uso AS uso_trabajado,
                                    ld.turno_trabajado AS turno_trabajado,
                                    ld.categoria_requerida AS categoria_trabajada
                                FROM listas_detalle ld
                                INNER JOIN empleados e ON ld.numero_empleado = e.numero_empleado
                                WHERE ld.numero_control = @numeroControl
                            ) AS origen
                            ON (destino.id_empleado = origen.id_empleado) -- Quitamos la fecha temporalmente para probar
                            WHEN MATCHED THEN
                                UPDATE SET 
                                    destino.uso_trabajado = origen.uso_trabajado,
                                    destino.turno_trabajado = origen.turno_trabajado,
                                    destino.categoria_trabajada = origen.categoria_trabajada; ";

//            MERGE INTO asistencia AS destino
//USING(
//    SELECT
//        e.numero_empleado AS id_empleado,
//        e.nombre,
//        CONCAT(e.apellido_paterno, ' ', e.apellido_materno) AS apellidos,
//        CAST('00:00:00' AS TIME) AS inicio,
//        CAST('00:00:00' AS TIME) AS fin,
//        CAST(GETDATE() AS DATETIME) AS fecha,
//        e.tipo AS tipo_empleado,
//        ld.uso AS uso_original,
//        ld.uso AS uso_trabajado,
//        ld.turno_periodo AS turno_original,
//        ld.turno_trabajado AS turno_trabajado,
//        ld.id_categoria AS categoria_original,
//        ld.categoria_requerida AS categoria_trabajada
//    FROM listas_detalle ld
//    INNER JOIN empleados e ON ld.numero_empleado = e.numero_empleado
//    WHERE ld.numero_control = @numeroControl
//) AS origen
//ON(destino.id_empleado = origen.id_empleado AND CAST(destino.fecha AS DATE) = CAST(GETDATE() AS DATE))

//WHEN MATCHED THEN
//    UPDATE SET
//        destino.uso_trabajado = CASE
//            WHEN origen.uso_trabajado IS NULL OR origen.uso_trabajado = 0 THEN destino.uso_trabajado
//            ELSE origen.uso_trabajado
//        END,


//        destino.turno_trabajado = CASE
//            WHEN origen.turno_trabajado IS NULL OR origen.turno_trabajado = '' OR origen.turno_trabajado = '0' THEN destino.turno_trabajado
//            ELSE origen.turno_trabajado
//        END,


//        destino.categoria_trabajada = CASE
//            WHEN origen.categoria_trabajada IS NULL OR origen.categoria_trabajada = 0 THEN destino.categoria_trabajada
//            ELSE origen.categoria_trabajada
//        END;

            //WHEN NOT MATCHED THEN
            //        INSERT(id_empleado, nombre, apellidos, inicio, fin, fecha, tipo_empleado,
            //                uso_original, uso_trabajado, turno_original, turno_trabajado,
            //                categoria_original, categoria_trabajada, estatus)
            //        VALUES(origen.id_empleado, origen.nombre, origen.apellidos, origen.inicio, origen.fin, origen.fecha, origen.tipo_empleado,
            //                origen.uso_original, origen.uso_trabajado, origen.turno_original, origen.turno_trabajado,
            //                origen.categoria_original, origen.categoria_trabajada, 1)

            using (cmd = new SqlCommand(query, Conex.nomi))
            {
                cmd.Parameters.AddWithValue("@numeroControl", ControlNumber);
                Conex.OpenNomina();
                return cmd.ExecuteNonQuery(); // Devuelve 1 si fue exitoso
            }
        }

        public Attend GetAttend(int NumberEmployee)
        {
            using (cmd = new SqlCommand("SELECT * FROM asistencia WHERE id_empleado = @id_empleado", Conex.nomi))
            {

                cmd.Parameters.AddWithValue("@id_empleado", NumberEmployee);

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

        public int AddAttendEmployee(Attend a)
        {
            Employee getEmplo = _repoEmployee.GetEmployeeByNumber(a.NumberEmployee);
            Cycle getCycl = _repoCycle.GetCycle();
            _repoTemplate.Cycle = getCycl._Cycle == "Zafra" ? "plantilla_zafra" : "plantilla_reparacion";

            Template getTempla = _repoTemplate.GetTemplate(a.NumberEmployee, _repoTemplate.Cycle);

            string query = "INSERT INTO asistencia(id_empleado,nombre,apellidos,categoria_original,categoria_trabajada,uso_original,uso_trabajado,turno_original,turno_trabajado,fecha,inicio,fin,tipo_empleado,estatus) " +
                           "VALUES (@id_empleado,@nombre,@apellidos,@categoria_original,@categoria_trabajada,@uso_original,@uso_trabajado,@turno_original,@turno_trabajado,@fecha,@inicio,@fin,@tipo_empleado,1)";

            using (cmd = new SqlCommand(query, Conex.nomi))
            {
                // Uso de parámetros para evitar inyección SQL //REVISAR COMO PASASR EL NUMERO DE EMPLEADO ME FALTA ESO
                cmd.Parameters.AddWithValue("@id_empleado", a.NumberEmployee);
                cmd.Parameters.AddWithValue("@nombre", getEmplo.Name);
                cmd.Parameters.AddWithValue("@apellidos", $"{getEmplo.LastnameFather} {getEmplo.LastnameMother}");
                cmd.Parameters.AddWithValue("@categoria_original", getEmplo.CategoryHarvest);
                cmd.Parameters.AddWithValue("@categoria_trabajada", a.CategoryWorked);
                cmd.Parameters.AddWithValue("@uso_original", getTempla.Use);
                cmd.Parameters.AddWithValue("@uso_trabajado", a.UseWorked);
                cmd.Parameters.AddWithValue("@turno_original", getTempla.ShiftPeriod);
                cmd.Parameters.AddWithValue("@turno_trabajado", a.TurnWorked);
                cmd.Parameters.AddWithValue("@fecha", a.EntryDate);
                cmd.Parameters.AddWithValue("@inicio", a.EntryTime);
                cmd.Parameters.AddWithValue("@fin", a.DepartureTime);
                cmd.Parameters.AddWithValue("@tipo_empleado", getEmplo.Type);

                Conex.OpenNomina();
                return cmd.ExecuteNonQuery(); // Devuelve 1 si fue exitoso
            }
        }

        public int UpdateAtttend(Attend a)
        {
            string query = "UPDATE asistencia SET categoria_trabajada=@categoria_trabajada,uso_trabajado=@uso_trabajado,turno_trabajado=@turno_trabajado,fecha=@fecha,estatus=1 WHERE id_empleado=@id_empleado";

            using (cmd = new SqlCommand(query, Conex.nomi))
            {
                // Uso de parámetros para evitar inyección SQL //REVISAR COMO PASASR EL NUMERO DE EMPLEADO ME FALTA ESO
                cmd.Parameters.AddWithValue("@id_empleado", a.NumberEmployee);
                cmd.Parameters.AddWithValue("@categoria_trabajada", a.CategoryWorked);
                cmd.Parameters.AddWithValue("@uso_trabajado", a.UseWorked);
                cmd.Parameters.AddWithValue("@turno_trabajado", a.TurnWorked);
                cmd.Parameters.AddWithValue("@fecha", a.EntryDate);
                Conex.OpenNomina();
                return cmd.ExecuteNonQuery(); // Devuelve 1 si fue exitoso
            }
        }

        private int ImportListAttend(DateTime entryDate, TimeSpan entryTime, TimeSpan departureTime)
        {
            string query = $@"
        DECLARE @horaActual AS TIME; 
        SET @horaActual = DATEADD(MINUTE, 5, GETDATE()); 

        DECLARE @ciclo AS VARCHAR(50); 
        SET @ciclo = (SELECT TOP 1 ciclo FROM azsja_nomina.dbo.ciclo); 
        TRUNCATE TABLE azsja_nomina.dbo.asistencia;

        INSERT INTO azsja_nomina.dbo.asistencia 
        SELECT DISTINCT 
            t1.NUMEROEMPLEADO, 
            t1.NOMBRE, 
            CONCAT(t1.APELLIDOPATERNO, ' ', t1.APELLIDOMATERNO) AS apellidos, 
            NULL, 
            'Ent', 
            t2.hora_entrada AS 'hora_entrada', 
            t2.hora_salida AS 'hora_salida', 
            t2.fecha_entrada AS 'fecha_entrada', 
            NULL, 
            NULL, 
            NULL, 
            CONCAT(t1.CLASIFICACION, ' ', t1.TIPO) AS 'tipo_empleado', 
            CASE 
                WHEN @ciclo = 'Zafra' THEN IIF(pu_zafra.uso IS NULL, 0, pu_zafra.uso) 
                ELSE IIF(pu_rep.uso IS NULL, 0, pu_rep.uso) 
            END AS uso_original, 
            CASE 
                WHEN @ciclo = 'Zafra' THEN IIF(pu_zafra.uso IS NULL, 0, pu_zafra.uso) 
                ELSE IIF(pu_rep.uso IS NULL, 0, pu_rep.uso) 
            END AS uso_trabajado, 
            NULL, 
            NULL, 
            t2.turno AS 'turno_original', 
            t2.turno AS 'turno_trabajado', 
            CASE 
                WHEN @ciclo = 'Zafra' THEN e.categoria_zafra 
                ELSE e.categoria_reparacion 
            END AS categoria_original, 
            CASE 
                WHEN @ciclo = 'Zafra' THEN e.categoria_zafra 
                ELSE e.categoria_reparacion 
            END AS categoria_trabajada, 
            NULL, 
            NULL, 
            NULL, 
            NULL, 
            NULL, 
            1
        FROM empleados t1 
        INNER JOIN incidencias t2 ON t2.numeroempleado = t1.NUMEROEMPLEADO 
        INNER JOIN azsja_nomina.dbo.empleados e ON e.numero_empleado = t1.NUMEROEMPLEADO 
        LEFT JOIN azsja_nomina.dbo.plantilla_zafra pu_zafra ON e.numero_empleado = pu_zafra.numero_empleado 
        LEFT JOIN azsja_nomina.dbo.plantilla_reparacion pu_rep ON e.numero_empleado = pu_rep.numero_empleado 
        WHERE t1.CLASIFICACION LIKE 'SINDICALIZADO' 
          AND t2.fecha_entrada = @fechaEntrada 
          AND t2.hora_entrada BETWEEN @horaInicio AND @horaFin 
          AND t2.estatus = 1;";

            using (SqlCommand cmd = new SqlCommand(query, Conex.asis))
            {
                // 2. Asignamos los tipos de datos nativos de SQL Server y pasamos las variables de C#
                cmd.Parameters.Add("@fechaEntrada", System.Data.SqlDbType.Date).Value = entryDate;
                cmd.Parameters.Add("@horaInicio", System.Data.SqlDbType.Time).Value = entryTime;
                cmd.Parameters.Add("@horaFin", System.Data.SqlDbType.Time).Value = departureTime;

                Conex.OpenAsistencia();
                return cmd.ExecuteNonQuery();
            }
        }

        public int GetTop1EmployeeEnable()
        {

            using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 numero_empleado FROM empleados WHERE estatus = 'Activo'", Conex.nomi))
            {
                Conex.OpenNomina();
                object result = cmd.ExecuteScalar();

                return result != null ? Convert.ToInt32(result) : 0;
            }
        }

        public int DeleteAttend(int NumberEmployee)
        {
            string query = $"DELETE FROM asistencia WHERE id_empleado = {NumberEmployee}";
            using (cmd = new SqlCommand(query, Conex.nomi))
            {
                Conex.OpenNomina();
                return cmd.ExecuteNonQuery();
            }
        }

        public int AddUnionCommittee()
        {
            string query = @"
                            DECLARE @ciclo AS VARCHAR(50);
                            SET @ciclo = (SELECT TOP 1 ciclo FROM ciclo);

                            INSERT INTO asistencia (
                                id_empleado, 
                                nombre, 
                                apellidos, 
                                inicio, 
                                fin, 
                                fecha, 
                                tipo_empleado, 
                                uso_original, 
                                uso_trabajado, 
                                turno_original, 
                                turno_trabajado, 
                                categoria_original, 
                                categoria_trabajada
                            )
                            SELECT 
                                cs.numero_empleado,
                                e.nombre,
                                CONCAT(e.apellido_paterno, ' ', e.apellido_materno),
                                CAST('00:00:00' AS TIME),
                                CAST('00:00:00' AS TIME),
                                CAST(GETDATE() AS DATETIME),
                                e.tipo,
                                cs.uso,
                                cs.uso,
                                1,
                                1,
                                IIF(@ciclo = 'Zafra', e.categoria_zafra, e.categoria_reparacion),
                                IIF(@ciclo = 'Zafra', cs.categoria_zafra, cs.categoria_reparacion)
                            FROM comite_sindical cs
                            INNER JOIN empleados e ON cs.numero_empleado = e.numero_empleado;";

            using (cmd = new SqlCommand(query, Conex.nomi))
            {
                Conex.OpenNomina();
                return cmd.ExecuteNonQuery(); // Devuelve 1 si fue exitoso
            }
        }

        public int CheckNextNumControl()
        {

            using (cmd = new SqlCommand("SELECT ISNULL(MAX(numero_control), 0) + 1 FROM listas_encabezado", Conex.nomi))

            {
                Conex.OpenNomina();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int ChechNextNumberEmployee(int currentNumber = 0)
        {
            // Si recibimos 0, buscamos desde el inicio (mínimo), si no, desde el actual
            string sql = currentNumber == 0
                ? "SELECT TOP 1 id_empleado FROM asistencia ORDER BY id_empleado ASC"
                : "SELECT TOP 1 id_empleado FROM asistencia WHERE id_empleado > @current ORDER BY id_empleado ASC";

            using (SqlCommand cmd = new SqlCommand(sql, Conex.nomi))
            {
                if (currentNumber > 0)
                {
                    cmd.Parameters.AddWithValue("@current", currentNumber);
                }

                Conex.OpenNomina();
                object result = cmd.ExecuteScalar();

                // Si result es null (no hay más empleados), devolvemos 0
                return (result == null || result == DBNull.Value) ? 0 : Convert.ToInt32(result);
            }
        }

        public List<AttendReport> GetAttendDataReport()
        {
            List<AttendReport> list = new List<AttendReport>();

            try
            {

                using (cmd = new SqlCommand(@"SELECT id_empleado,CONCAT(nombre,' ',apellidos) AS n_completo,estado_asistencia,fecha,inicio,uso_trabajado,turno_trabajado,categoria_trabajada,estatus                                             
                                             FROM asistencia WHERE estatus =1 ORDER BY id_empleado", Conex.nomi))
                {
                    Conex.OpenNomina();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            list.Add(ViewReportData(reader));
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

        private Attend ShowDataGrid(SqlDataReader reader)
        {
            return new Attend
            {
                Id = Convert.ToInt32(reader["id"]),
                NumberEmployee = Convert.ToInt32(reader["id_empleado"]),
                FullName = Convert.ToString($"{reader["nombre"]} {reader["apellidos"]}"),
                CategoryOriginal = Convert.ToInt32(reader["categoria_original"]),
                CategoryWorked = Convert.ToInt32(reader["categoria_trabajada"]),
                UseOriginal = Convert.ToInt32(reader["uso_original"]),
                UseWorked = Convert.ToInt32(reader["uso_trabajado"]),
                TurnOriginal = Convert.ToInt32(reader["turno_original"]),
                TurnWorked = Convert.ToInt32(reader["turno_trabajado"]),
                EntryDate = Convert.ToDateTime(reader["fecha"]),
                EntryTime = reader["inicio"] == DBNull.Value ? DateTime.Today : DateTime.Today.Add((TimeSpan)reader["inicio"]),
                DepartureTime = reader["fin"] == DBNull.Value ? DateTime.Today : DateTime.Today.Add((TimeSpan)reader["fin"]),
                TypeEmployee = Convert.ToString(reader["tipo_empleado"]),
                Status = Convert.ToString(reader["estatus"])
            };
        }

        private AttendReport ViewReportData(SqlDataReader reader)
        {
            return new AttendReport
            {
                IdEmployee = Convert.ToInt32(reader["id_empleado"]),
                FullName = Convert.ToString(reader["n_completo"]),
                State = Convert.ToString(reader["estado_asistencia"]),
                Date = Convert.ToDateTime(reader["fecha"]),
                EntryTime = reader["inicio"] == DBNull.Value ? DateTime.Today : DateTime.Today.Add((TimeSpan)reader["inicio"]),
                UseWorked = Convert.ToInt32(reader["uso_trabajado"]),
                ShiftWorked = Convert.ToInt32(reader["turno_trabajado"]),
                CategoryWorked = Convert.ToInt32(reader["categoria_trabajada"]),
                Status = Convert.ToString(reader["estatus"])
            };
        }

        public int UpdateStatus(int NumberEmployee, bool Stat)
        {
            string st = Stat ? "1" : "0";

            string query = "UPDATE asistencia SET estatus = @st WHERE id_empleado = @id";

            using (SqlCommand cmd = new SqlCommand(query, Conex.nomi))
            {
                // 3. Añadimos los parámetros de forma segura
                cmd.Parameters.AddWithValue("@st", st);
                cmd.Parameters.AddWithValue("@id", NumberEmployee);

                Conex.OpenNomina();

                return cmd.ExecuteNonQuery();
            }
        }

        public int UpdateAllStatus()
        {

            string query = "UPDATE asistencia SET estatus = IIF((SELECT TOP 1 estatus FROM asistencia )  = '1' ,'0','1')";

            using (SqlCommand cmd = new SqlCommand(query, Conex.nomi))
            {
                Conex.OpenNomina();

                return cmd.ExecuteNonQuery();
            }
        }
    }
}
