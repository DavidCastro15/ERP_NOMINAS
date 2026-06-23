using ERP_LOGIN.Authorization.Models;
using ERP_SHARED.Conexion;
using ERP_SHARED.GlobalFunctions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_LOGIN.Authorization.Repositories
{
    public class PermissionsRepository : IPermissions
    {
        ConnectorSql Conex = new ConnectorSql();
        Utilities Util = new Utilities();
        SqlCommand cmd = new SqlCommand();

        public List<string> GetNameMenuXUser(int idUser)
        {
            var list = new List<string>();

            try
            {

                using (cmd = new SqlCommand($@"SELECT c.nombre_formulario FROM Permisos p 
                                            INNER JOIN Catalogos c ON p.IdCatalogo = c.id 
                                            WHERE p.IdUsuario = @idUser", Conex.auth))
                {
                    cmd.Parameters.AddWithValue("@idUser", idUser);
                    Conex.OpenAuth();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            list.Add(reader["nombre_formulario"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Error al obtener los catalogos: " + ex.Message);
            }
            finally
            {

                Conex.CloseAuth();
            }

            return list;

        }

        public List<Catalog> GetPermissionsXUser(int idUser)
        {
            var list = new List<Catalog>();

            try
            {

                using (cmd = new SqlCommand($@"SELECT c.id, c.nombre_formulario, c.nombre_mostrar,
                                                CASE WHEN p.IdUsuario IS NOT NULL THEN 1 ELSE 0 END AS HassAccess
                                                FROM Catalogos c
                                                LEFT JOIN Permisos p ON c.id = p.IdCatalogo AND p.IdUsuario = @idUser", Conex.auth))
                {
                    cmd.Parameters.AddWithValue("@idUser", idUser);
                    Conex.OpenAuth();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            list.Add(new Catalog
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                NameCatalog = reader["nombre_formulario"].ToString(),
                                Description = reader["nombre_mostrar"].ToString(),
                                IsEnable = Convert.ToBoolean(reader["HassAccess"])
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Error al obtener los detalles de permisos de los usuarios: " + ex.Message);
            }
            finally
            {

                Conex.CloseAuth();
            }

            return list;
        }

        public bool SavePermissions(int idUser, List<int> idsCatalogsSelects)
        {

            Conex.OpenAuth();


            SqlTransaction tra = Conex.nomi.BeginTransaction();

            try
            {
                string queryU = "DELETE FROM permisos WHERE IdUsuario  = @idUser";
                using (var cmdDetail = new SqlCommand(queryU, Conex.auth, tra))
                {
                    cmdDetail.Parameters.AddWithValue("@idUser", idUser);
                    cmdDetail.ExecuteNonQuery();
                }


                string queryP = "INSERT INTO permisos (IdUsuario, IdCatalogo) VALUES (@idUser, @idCat)";

                foreach (int idCat in idsCatalogsSelects)
                {
                    using (cmd = new SqlCommand(queryP, Conex.asis, tra))
                    {
                        cmd.Parameters.AddWithValue("@idUser", idUser);
                        cmd.Parameters.AddWithValue("@idCat", idCat);
                        cmd.ExecuteNonQuery();
                    }
                }

                tra.Commit();
                return true;
            }
            catch (Exception)
            {
                tra.Rollback();
                throw;
            }
            finally
            {
                Conex.auth.Close();
            }

        }
    }
}
