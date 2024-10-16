using Blazored.SessionStorage;
using DataAccessLayer.DataTransferObjects;
//using Blazored.SessionStorage;
using Microsoft.JSInterop;

namespace TrackerUIWeb.Utilities
{
    public class UtilService : IUtilService
    {
        
        ISessionStorageService _httpContextAccessor; private readonly IJSRuntime _js;
        public UtilService(ISessionStorageService httpContextAccessor,  IJSRuntime js)
        {
             
            _httpContextAccessor = httpContextAccessor;
            _js = js;
        }

        public class BrowserDimension
        {
            public int Width { get; set; }
            public int Height { get; set; }
        }

        public async Task<BrowserDimension> GetDimensions()
        {
            return await _js.InvokeAsync<BrowserDimension>("getDimensions");
        }
        public async Task<bool> IsUserAuthenticated( )
        {
            
            var user = await _httpContextAccessor.GetItemAsync<string>("LoggedInUser");
            //var user = false;
            if (string.IsNullOrEmpty(user))
            {
                return false;
            }
            else
            {
               return true;
            }
        }
      
        //public async Task<LoginDTO> Login(string username, string password)
        //{

        //    Cryptography cryptography = new Cryptography();
        //    password = cryptography.EncryptText(password);
        //    //var output = await _user.GetSingleItem(x => x.Username == username && x.Password == password);
        //    //if (output is null)
        //    //{
        //    //    return null;
        //    //}
 

        //    if (output != null)
        //    {
        //        return new LoginDTO
        //        {
        //            Username = output.Username,
        //            UserId = output.UserId,
        //            Grade = output.Grade
        //        };
        //    }
        //    else
        //    {
        //        return new LoginDTO { }
        //            ;
        //    }





        //}
    }
}
