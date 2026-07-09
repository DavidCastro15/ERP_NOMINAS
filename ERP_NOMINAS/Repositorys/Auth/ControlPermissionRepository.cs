using ERP_NOMINAS.Models.Auth;
using ERP_SHARED.Conexion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Repositorys.Auth
{
    public class ControlPermissionRepository : IControlPermission
    {
        ConnectorSql Conex = new ConnectorSql();
        SqlCommand cmd = new SqlCommand();

        public int CreateUser(ControlPermission model)
        {
            int res = 0;
            int idNewUser = 0;

            Conex.OpenNomina();

            string queryValid = "SELECT COUNT(1) FROM azsja_nombase.dbo.usuarios_base WHERE usuario = @usuario";
            using (SqlCommand cmdCheck = new SqlCommand(queryValid, Conex.nomi))
            {
                cmdCheck.Parameters.AddWithValue("@usuario", model.NameUser);
                if (Convert.ToInt32(cmdCheck.ExecuteScalar()) > 0) return 0;
            }

            SqlTransaction transaction = Conex.nomi.BeginTransaction();
            try
            {
                string queryUser = @"INSERT INTO azsja_nombase.dbo.usuarios_base (usuario, contrasena, id_role) 
                                        VALUES (@usuario, @contrasena, @id_role);
                                        SELECT SCOPE_IDENTITY();";

                using (SqlCommand cmdUser = new SqlCommand(queryUser, Conex.nomi, transaction))
                {
                    cmdUser.Parameters.AddWithValue("@usuario", model.NameUser);
                    cmdUser.Parameters.AddWithValue("@contrasena", model.Password);
                    cmdUser.Parameters.AddWithValue("@id_role", model.IdRole);
                    idNewUser = Convert.ToInt32(cmdUser.ExecuteScalar());
                }

                if (model.Catalogs != null && model.Catalogs.Count > 0)
                {
                    string queryInsert = "INSERT INTO azsja_nombase.dbo.permisos (IdUsuario, IdCatalogo) VALUES (@idUsuario, @idCatalogo)";
                    using (SqlCommand cmdIns = new SqlCommand(queryInsert, Conex.nomi, transaction))
                    {
                        cmdIns.Parameters.Add("@idUsuario", SqlDbType.Int);
                        cmdIns.Parameters.Add("@idCatalogo", SqlDbType.Int);

                        foreach (var catalog in model.Catalogs)
                        {
                            cmdIns.Parameters["@idUsuario"].Value = idNewUser;
                            cmdIns.Parameters["@idCatalogo"].Value = catalog.Id;
                            cmdIns.ExecuteNonQuery();
                        }
                    }
                }

                transaction.Commit();
                res = 1;
            }
            catch
            {
                transaction.Rollback();
                idNewUser = 0;
                res = 0;
            }
            return res;
        }

        public int DeleteUser(int Id)
        {
            int res = 0;
            Conex.OpenNomina();

            SqlTransaction transaction = Conex.nomi.BeginTransaction();
            try
            {
                string query = @"DELETE FROM azsja_nombase.dbo.permisos WHERE IdUsuario = @Id;
                         DELETE FROM azsja_nombase.dbo.usuarios_base WHERE id = @Id;";

                using (cmd = new SqlCommand(query, Conex.nomi, transaction))
                {
                    cmd.Parameters.AddWithValue("@Id", Id);

                    cmd.ExecuteNonQuery();
                }

                transaction.Commit();
                res = 1; 
            }
            catch (Exception ex)
            {
                
                transaction.Rollback();
                res = 0;
                throw new Exception("Error al borrar el usuaro" + ex.Message);
            }

            return res;
        }

        public ControlPermission GetControlPermission(int IdUser)
        {
            ControlPermission permission = null;

            using (cmd = new SqlCommand($@"SELECT ub.id, ub.usuario, ub.contrasena, ub.id_role, p.IdCatalogo, c.nombre_mostrar
                                FROM azsja_nombase.dbo.usuarios_base ub
                                INNER JOIN azsja_nombase.dbo.permisos p ON ub.id = p.IdUsuario
                                INNER JOIN azsja_nombase.dbo.catalogos c ON p.IdCatalogo = c.id
                                WHERE ub.id = @id", Conex.nomi))
            {
                cmd.Parameters.AddWithValue("@id", IdUser);
                Conex.OpenNomina();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    // 2. Usamos un bucle while para leer todas las filas devueltas
                    while (reader.Read())
                    {
                        // 3. Si es la primera fila, instanciamos el objeto principal
                        if (permission == null)
                        {
                            permission = new ControlPermission
                            {
                                IdUser = IdUser,
                                NameUser = reader["usuario"].ToString(),
                                Password = reader["contrasena"].ToString(),
                                IdRole = Convert.ToInt32(reader["id_role"]),
                                NameRole = "", // Nota: Tu Query actual no trae el nombre del rol, solo el ID
                                Catalogs = new List<Catalog>()
                            };
                        }

                        // 4. Por cada fila, agregamos el catálogo a la lista
                        permission.Catalogs.Add(new Catalog
                        {
                            Id = Convert.ToInt32(reader["IdCatalogo"]),
                            Name = reader["nombre_mostrar"].ToString()
                        });
                    }
                }
            }

            return permission;
        }

        public List<ControlPermission> GetControlPermissions()
        {
            List<ControlPermission> list = new List<ControlPermission>();
            try
            {

                using (cmd = new SqlCommand(@"SELECT ub.id, ub.usuario, ub.contrasena, ub.id_role, rl.nombre
                                             FROM azsja_nombase.dbo.usuarios_base ub
                                             INNER JOIN azsja_nombase.dbo.roles rl
                                             ON ub.id_role = rl.id", Conex.nomi))
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

                throw new Exception("Error al obtener los usuarios: " + ex.Message);
            }
            finally
            {

                Conex.CloseNomina();
            }

            return list;      
        }

        public int UpdateUser(ControlPermission model)
        {
            int res = 0;

            Conex.OpenNomina();

            SqlTransaction transaction = Conex.nomi.BeginTransaction();

            try
            {
                string queryUser = @"UPDATE azsja_nombase.dbo.usuarios_base 
                                SET usuario = @usuario, contrasena = @contrasena, id_role = @id_role 
                                WHERE id = @id";

                using (SqlCommand cmdUser = new SqlCommand(queryUser, Conex.nomi, transaction))
                {
                    cmdUser.Parameters.AddWithValue("@usuario", model.NameUser);
                    cmdUser.Parameters.AddWithValue("@contrasena", model.Password);
                    cmdUser.Parameters.AddWithValue("@id_role", model.IdRole);
                    cmdUser.Parameters.AddWithValue("@id", model.IdUser);
                    cmdUser.ExecuteNonQuery();
                }

                string queryDeletePermissions = "DELETE FROM azsja_nombase.dbo.permisos WHERE IdUsuario = @idUsuario";
                using (SqlCommand cmdDel = new SqlCommand(queryDeletePermissions, Conex.nomi, transaction))
                {
                    cmdDel.Parameters.AddWithValue("@idUsuario", model.IdUser);
                    cmdDel.ExecuteNonQuery();
                }

                if (model.Catalogs != null && model.Catalogs.Count > 0)
                {
                    string queryInsertPermissions = @"INSERT INTO azsja_nombase.dbo.permisos (IdUsuario, IdCatalogo) 
                                          VALUES (@idUsuario, @idCatalogo)";

                    using (SqlCommand cmdIns = new SqlCommand(queryInsertPermissions, Conex.nomi, transaction))
                    {
                        cmdIns.Parameters.Add("@idUsuario", SqlDbType.Int);
                        cmdIns.Parameters.Add("@idCatalogo", SqlDbType.Int);

                        foreach (var catalog in model.Catalogs)
                        {
                            cmdIns.Parameters["@idUsuario"].Value = model.IdUser;
                            cmdIns.Parameters["@idCatalogo"].Value = catalog.Id;

                            cmdIns.ExecuteNonQuery();
                        }
                    }
                }

                transaction.Commit();
                res = 1;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                res = 0;
                throw new Exception("Error al borrar el usuaro" + ex.Message);
            }
            finally
            {
                Conex.CloseNomina(); 
            }

            return res;
        }

        private ControlPermission ShowDataGrid(SqlDataReader reader)
        {
            return new ControlPermission
            {
                IdUser = Convert.ToInt32(reader["id"]),
                NameUser = Convert.ToString(reader["usuario"]),
                Password = Convert.ToString(reader["contrasena"]),
                IdRole = Convert.ToInt32(reader["id_role"]),
                NameRole = Convert.ToString(reader["nombre"])
            };
        }

        public List<Catalog> GetCatalogs()
        {
            List<Catalog> lista = new List<Catalog>();

            using (SqlCommand cmd = new SqlCommand("SELECT id, nombre_mostrar FROM azsja_nombase.dbo.catalogos", Conex.nomi))
            {
                Conex.OpenNomina();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Catalog
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Name = reader["nombre_mostrar"].ToString().Trim()
                        });
                    }
                }
            }

            return lista;
        }
    }
}
