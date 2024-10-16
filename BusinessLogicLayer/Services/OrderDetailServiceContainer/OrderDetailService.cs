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
using BusinessLogicLayer.Services.OrderDetailServiceContainer;
 
namespace BusinessLogicLayer.Services.OrderDetailServiceContainer
{

    public class OrderDetailService : IOrderDetailService
    {
        private readonly GenericRepository<OrderDetail> _orderDetail;
        public OrderDetailService(GenericRepository<OrderDetail> orderDetail)
        {
            _orderDetail = orderDetail;
        }
        public async Task<OutputHandler> Create(OrderDetailDTO orderDetailDTO)
        {
            try
            {
                var mapped = new AutoMapper<OrderDetailDTO, OrderDetail>().MapToObject(orderDetailDTO);
                mapped.CreatedDate = DateTime.Now;
                mapped.CreatedBy = orderDetailDTO.LoggedInUsername;
                var result = await _orderDetail.Create(mapped);

                return result;
            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);
            }

        }

        //Code for deleting record
        public async Task<OutputHandler> Delete(int orderDetailId)
        {

            try
            {
                var output = await _orderDetail.Delete(x => x.OrderDetailId == orderDetailId);
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

        public async Task<OrderDetailDTO> GetOrderDetail(int orderDetailId)
        {
            var output = await _orderDetail.GetSingleItem(x => x.OrderDetailId == orderDetailId);
            return new AutoMapper<OrderDetail, OrderDetailDTO>().MapToObject(output);
        }

        public async Task<IEnumerable<OrderDetailDTO>> GetAllOrderDetails()
        {
            var output = await _orderDetail.GetAll();
            return new AutoMapper<OrderDetail, OrderDetailDTO>().MapToList(output);
        }

        public async Task<OutputHandler> Update(OrderDetailDTO orderDetailDTO)
        {
            try
            {
                var mapped = new AutoMapper<OrderDetailDTO, OrderDetail>().MapToObject(orderDetailDTO);
                mapped.ModifiedBy = orderDetailDTO.LoggedInUsername;
                mapped.ModifiedDate = DateTime.Now;


                var result = await _orderDetail.Update(mapped);
                return result;

            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);
            }
        }


    }

}
