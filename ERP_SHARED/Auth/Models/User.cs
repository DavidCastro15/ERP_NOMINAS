using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_SHARED.Auth.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public int IdRole { get; set; }
        public string NameRole { get; set; }
    }
}
