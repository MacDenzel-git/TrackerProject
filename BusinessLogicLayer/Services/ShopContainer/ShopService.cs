using AllinOne.DataHandlers.ErrorHandler;
using AllinOne.DataHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;
using DataAccessLayer.DataTransferObjects;
using DataAccessLayer.GenericRepository;

namespace BusinessLogicLayer.Services.ShopServiceContainer
{

    public class ShopService : IShopService
    {
        private readonly GenericRepository<Shop> _shop;
        public ShopService(GenericRepository<Shop> shop)
        {
            _shop = shop;
        }
        public async Task<OutputHandler> Create(ShopDTO shopDTO)
        {
            try
            {
                var mapped = new AutoMapper<ShopDTO, Shop>().MapToObject(shopDTO);
                mapped.CreatedDate = DateTime.Now;
                mapped.CreatedBy = shopDTO.LoggedInUsername;
                var result = await _shop.Create(mapped);

                return result;
            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);
            }

        }

        //Code for deleting record
        public async Task<OutputHandler> DeleteRequest(ShopDTO shop)
        {

            try
            {
                //var shop = await GetShop(shopDTO.ShopId);
                shop.DeletedBy  = shop.LoggedInUsername;
                shop.IsDeleted = true;
                var mapped = new AutoMapper<ShopDTO, Shop>().MapToObject(shop);
                var output = await _shop.Update(mapped);
                if (output.IsErrorOccured)
                {
                    return output;
                }
                return new OutputHandler
                {
                    IsErrorOccured = false,
                    Message = StandardMessages.GetSuccessfulMessage() //assign message to the error
                };
            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);
            }
        }


        public async Task<OutputHandler> DeleteApprove(ShopDTO shopDTO)
        {

            try
            {
                var shop = await GetShop(shopDTO.ShopId);
                //shop.IsDeleted = true;
                // shop.DeleteApprover = shopDTO.LoggedInUsername;
                //shop.DateDeleted = DateTime.Now;
                var mapped = new AutoMapper<ShopDTO, Shop>().MapToObject(shop);
                var output = await _shop.Update(mapped);
                if (output.IsErrorOccured)
                {
                    return output;
                }
                return new OutputHandler
                {
                    IsErrorOccured = false,
                    Message = StandardMessages.GetSuccessfulMessage() //assign message to the error
                };
            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);
            }
        }


        public async Task<ShopDTO> GetShop(int shopId)
        {
            var output = await _shop.GetSingleItem(x => x.ShopId == shopId);
            return new AutoMapper<Shop, ShopDTO>().MapToObject(output);
        }

        public async Task<IEnumerable<ShopDTO>> GetAllShops()
        {
            var output = await _shop.GetAll();
            return new AutoMapper<Shop, ShopDTO>().MapToList(output);
        }

        public async Task<OutputHandler> Update(ShopDTO shopDTO)
        {
            try
            {
                var mapped = new AutoMapper<ShopDTO, Shop>().MapToObject(shopDTO);
                mapped.ModifiedBy = shopDTO.LoggedInUsername;
                mapped.ModifiedDate = DateTime.Now;


                var result = await _shop.Update(mapped);
                return result;

            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);
            }
        }


    }

}
