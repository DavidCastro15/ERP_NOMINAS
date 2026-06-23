using ERP_LOGIN.Authorization.Models;
using ERP_SHARED.Auth.Models;
using ERP_SHARED.Conexion;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_LOGIN.Authorization.Repositories
{
    public class LoginRepository : IUser
    {
        ConnectorSql Conex = new ConnectorSql();
        SqlCommand cmd = new SqlCommand();

        public List<User> GetUsers()
        {
            throw new NotImplementedException();
        }

        public User Login(string username, string password)
        {
            using (cmd = new SqlCommand($@"SELECT ub.id,ub.usuario,ub.contrasena,ub.acceso_sistema,ub.estatus,ub.id_role,r.nombre AS nombre_rol
                                            FROM usuarios_base ub
                                            INNER JOIN roles r
                                            ON ub.id_role = r.id
                                            WHERE ub.usuario = @user AND ub.contrasena = @pwd", Conex.auth))
            {

                cmd.Parameters.AddWithValue("@user", username);
                cmd.Parameters.AddWithValue("@pwd", password);

                Conex.OpenAuth();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {

                    if (reader.Read())
                    {
                        return new User
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Username = Convert.ToString(reader["usuario"]),
                            IdRole = Convert.ToInt32(reader["id_role"]),
                            NameRole = Convert.ToString(reader["nombre_rol"])
                        };
                    }
                }
            }

            return null;
        }

        List<ERP_SHARED.Auth.Models.User> IUser.GetUsers()
        {
            throw new NotImplementedException();
        }

        ERP_SHARED.Auth.Models.User IUser.Login(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
