using ERP_SHARED.Auth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_LOGIN.Authorization.Models
{
    public interface IUser
    {
        User Login(string username, string password);
        List<User> GetUsers();
    }
}
