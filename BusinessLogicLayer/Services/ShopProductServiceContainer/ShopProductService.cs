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
 using BusinessLogicLayer.Services.OrderServiceContainer;
using BusinessLogicLayer.Services.OrderDetailServiceContainer;
 
namespace BusinessLogicLayer.Services.ShopProductServiceContainer
{

    public class ShopProductService : IShopProductService
    {
        private readonly GenericRepository<ShopProduct> _shopProduct;
        public ShopProductService(GenericRepository<ShopProduct> shopProduct)
        {
            _shopProduct = shopProduct;
        }
        public async Task<OutputHandler> Create(ShopProductDTO shopProductDTO)
        {
            try
            {
                var isExist = await _shopProduct.AnyAsync(x => x.ShopId == shopProductDTO.ShopId && x.ProductId == shopProductDTO.ProductId);
                if (isExist)
                {
                    return new OutputHandler
                    {
                        IsErrorOccured = true,
                        Message = "Product already added for this shop"
                    };
                }
                var mapped = new AutoMapper<ShopProductDTO, ShopProduct>().MapToObject(shopProductDTO);
                mapped.QuantityInStock = 0;
                mapped.Price = 0;
                mapped.CreatedDate = DateTime.Now;
                mapped.CreatedBy = shopProductDTO.LoggedInUsername;
                var result = await _shopProduct.Create(mapped);

                return result;
            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);
            }

        }

        //Code for deleting record
        public async Task<OutputHandler> DeleteRequest(ShopProductDTO shopProductDTO)
        {

            try
            {
                var shopProduct = await GetShopProduct(shopProductDTO.ShopProductID);
                shopProduct.DeletedBy = shopProductDTO.LoggedInUsername;

                var mapped = new AutoMapper<ShopProductDTO, ShopProduct>().MapToObject(shopProduct);
                var output = await _shopProduct.Update(mapped);
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


        public async Task<OutputHandler> DeleteApprove(ShopProductDTO shopProductDTO)
        {

            try
            {
                var shopProduct = await GetShopProduct(shopProductDTO.ShopProductID);
                shopProduct.IsDeleted = true;
                shopProduct.DeletedApprover = shopProductDTO.LoggedInUsername;
                shopProduct.DateDeleted = DateTime.Now;
                var mapped = new AutoMapper<ShopProductDTO, ShopProduct>().MapToObject(shopProduct);
                var output = await _shopProduct.Update(mapped);
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


        public async Task<ShopProductDTO> GetShopProduct(int shopProductId)
        {
            var output = await _shopProduct.GetSingleItem(x => x.ShopProductId == shopProductId);
            return new AutoMapper<ShopProduct, ShopProductDTO>().MapToObject(output);
        }

        public async Task<IEnumerable<ShopProductDTO>> GetAllShopProducts(int shopId)
        {
            var output = await _shopProduct.GetListAsync(x => x.ShopId == shopId && x.IsDeleted != true);
            return new AutoMapper<ShopProduct, ShopProductDTO>().MapToList(output);
        }

        public async Task<OutputHandler> Update(ShopProductDTO shopProductDTO)
        {
            try
            {
                var mapped = new AutoMapper<ShopProductDTO, ShopProduct>().MapToObject(shopProductDTO);
                mapped.ModifiedBy = shopProductDTO.LoggedInUsername;
                mapped.ModifiedDate = DateTime.Now;


                var result = await _shopProduct.Update(mapped);
                return result;

            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);
            }
        }

        public async Task<ShopProductDTO> GetShopProductByBranch(int shopProductId, string shopId)
        {
            return new AutoMapper<ShopProduct, ShopProductDTO>().MapToObject(await _shopProduct.GetSingleItem(x => x.ShopProductId == shopProductId));
        }
        
        public async Task<ShopProductDTO> GetShopProductByBranch(int shopProductId, int shopId)
        {
            return new AutoMapper<ShopProduct, ShopProductDTO>().MapToObject(await _shopProduct.GetSingleItem(x => x.ShopProductId == shopProductId && x.ShopId == shopId));
        }
    }

}
