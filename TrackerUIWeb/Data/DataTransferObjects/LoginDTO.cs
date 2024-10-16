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
        public int Grade { get; set; }
       
        public int UserId { get; set; }
     }
}
