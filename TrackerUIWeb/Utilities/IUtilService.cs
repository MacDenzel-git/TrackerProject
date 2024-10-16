using DataAccessLayer.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 using static TrackerUIWeb.Utilities.UtilService;

namespace TrackerUIWeb.Utilities
{
    public interface IUtilService
    {
        //Task<LoginDTO> Login(string username, string password);
        Task<BrowserDimension> GetDimensions();
        Task<bool> IsUserAuthenticated();
    }
}
