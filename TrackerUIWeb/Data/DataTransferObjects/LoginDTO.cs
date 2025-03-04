using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DataTransferObjects
{
    public class LoginDTO
    {

        public string Username { get; set; }
        public string Password { get; set; }
        public int? ShopId { get; set; }
        public string? Role { get; set; }
        public string? ShopName { get; set; }

        public int? UserId { get; set; }
        public string StatusMessage { get; set; }
    }
}
