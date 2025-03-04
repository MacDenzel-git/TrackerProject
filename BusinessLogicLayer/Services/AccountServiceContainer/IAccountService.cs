using AllinOne.DataHandlers;
using Azure.Identity;
using DataAccessLayer.DataTransferObjects;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.AccountServiceContainer
{
    public interface IAccountService
    {
        Task<OutputHandler> CreateUser(UserDTO user);
        Task<OutputHandler> EditUser(UserDTO user);
        Task<OutputHandler> UnlockUser(string username);
        Task<OutputHandler> ResetPassword(string username);
        Task<LoginDTO> Login(string username, string password);
        Task<IEnumerable<UserRole>> GetRoles();

    }
}
