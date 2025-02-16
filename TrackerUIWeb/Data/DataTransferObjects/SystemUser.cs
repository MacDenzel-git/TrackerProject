using Microsoft.AspNetCore.Identity;

namespace TrackerUIWeb.Data.DataTransferObjects
{
    public class SystemUser : IdentityUser
    {
        public int ShopId { get; set; }
    }
}
