using AllinOne.DataHandlers;
using DataAccessLayer.DataTransferObjects;
using DataAccessLayer.GenericRepository;
using DataAccessLayer.Models;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.DataProtection;
using System;
using System.Collections;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;

namespace BusinessLogicLayer.Services.AccountServiceContainer
{
    public class AccountService : IAccountService
    {
        private readonly IDataProtector _protector;
        private readonly GenericRepository<UserRole> _userRolesRepository;
        private readonly GenericRepository<User> _userRepository;
        private readonly GenericRepository<Shop> _shopRepository;
        public AccountService(IDataProtectionProvider provider, GenericRepository<UserRole> userRolesRepository, GenericRepository<User> userRepository, GenericRepository<Shop> shopRepository)
        {
            _userRolesRepository = userRolesRepository;
            _userRepository = userRepository;
            _protector = provider.CreateProtector("MyPurpose");
            _shopRepository = shopRepository;
        }
        public Tuple<byte[],string> EncryptData(string plainText)
        {
            // cryptographically strong random bytes.
            byte[] salt = RandomNumberGenerator.GetBytes(128 / 8); // divide by 8 to convert bits to bytes
            Debug.WriteLine($"Salt: {Convert.ToBase64String(salt)}");

            // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: plainText!,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            return new Tuple<byte[], string>(salt,hashed);
        }
         
        
        public string GetHash(string salt, string password)
        {
            // cryptographically strong random bytes.
            //byte[] salt = RandomNumberGenerator.GetBytes(128 / 8); // divide by 8 to convert bits to bytes
            //Console.WriteLine($"Salt: {Convert.ToBase64String(salt)}");
           // byte[] bytes = Encoding.ASCII.GetBytes(salt);
            byte[] bytes = Convert.FromBase64String(salt);
            // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password!,
                salt: bytes,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            return  hashed;
        }




        // Method to decrypt the encrypted data
        public string DecryptData(string encryptedData)
        {
            try
            {
                return _protector.Unprotect(encryptedData);
            }
            catch (Exception ex)
            {
                // If decryption fails (e.g., data is tampered or invalid), handle the exception
                return $"Decryption failed: {ex.Message}";
            }
        }

        public async Task<OutputHandler> CreateUser(UserDTO user)
        {

            var isExist = await _userRepository.AnyAsync(x => x.UserName == user.UserName);
            if (isExist)
            {
                return new OutputHandler
                {
                    IsErrorOccured = true,
                    Message = "Username Already Exist"
                };
            }
            var tuple = EncryptData(user.PasswordHash);
            user.PasswordHash = tuple.Item2.ToString();
            user.Salt= Convert.ToBase64String(tuple.Item1);
            //user.Salt = tuple.Item1.ToString();
            var mapped = new AutoMapper<UserDTO, User>().MapToObject(user);

            var output = await _userRepository.Create(mapped);
            return output;
        }

        public async Task<OutputHandler?> EditUser(UserDTO user)
        {
            var tuple = EncryptData(user.PasswordHash);
            user.PasswordHash = tuple.Item2.ToString();
            user.Salt = Convert.ToBase64String(tuple.Item1);
            var mapped = new AutoMapper<UserDTO, User>().MapToObject(user);
            var output = await _userRepository.Update(mapped);
            return output;
        }

        

        public async Task<LoginDTO> Login(string username, string password)
        {
            //get salt

           
            var output = await _userRepository.GetSingleItem(x => x.UserName == username );
            if (output is null)
            {
                return null;
            }
            else
            {
                //get salt
                string hash = GetHash(output.Salt, password);
                 if (output.PasswordHash == hash)
                {
                    //if (output.TriggerPasswordChange)
                    //{
                    //    return new OutputHandler
                    //    {
                    //        IsErrorOccured = false,
                    //        Message = "RESET"
                    //    };
                    //}

                    var Role = await _userRolesRepository.GetSingleItem(x => x.RoleId == output.RoleId);
                    var shop = await _shopRepository.GetSingleItem(x=> x.ShopId == output.ShopId);
                    return new LoginDTO
                    {
                        ShopId = output.ShopId,
                        Role = Role.RoleDescription,
                        ShopName = shop.ShopName,
                        Username = username,
                        StatusMessage = "Success",
                        UserId = output.Id
                    };
                }
                return new LoginDTO { StatusMessage = "Failed"};
            }

                     //return new AutoMapper<User,UserDTO>().MapToObject(output);
        }

        public  async Task<OutputHandler> ResetPassword(string username)
        {
            var output = await _userRepository.GetSingleItem(x => x.UserName == username);
            if (output is null)
            {
                return new OutputHandler { IsErrorOccured = true, Message = "Wrong username or Password" };
            }
            else
            {
                output.LockoutEnabled = output.LockoutEnabled ? false : true;
                
                var result = await _userRepository.Update(output);
                return result;
            }
             
        }

        public async Task<OutputHandler> UnlockUser(string username)
        {
            var output = await _userRepository.GetSingleItem(x => x.UserName == username);
            if (output is null)
            {
               return new OutputHandler { IsErrorOccured = true, Message = "Wrong username or Password" };
            }
            else
            {
                output.LockoutEnabled = output.LockoutEnabled ? false : true;
                var result =await _userRepository.Update(output);
                return result;
            }
        }

        public async Task<IEnumerable<UserRole>> GetRoles()
        {
            var roles = await _userRolesRepository.GetAll();
            return roles;
        }
    }
}
