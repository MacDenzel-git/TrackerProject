using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DataTransferObjects
{
    public class UserInformationDTO
    {
        public string LoggedInUsername { get; set; }
        public string LoggedInUserRole { get; set; }
    }
}
