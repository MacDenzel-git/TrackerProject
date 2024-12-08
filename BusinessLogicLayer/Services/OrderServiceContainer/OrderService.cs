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

namespace BusinessLogicLayer.Services.OrderServiceContainer
{

    public class OrderService : IOrderService
    {
        private readonly GenericRepository<Order> _order;
        public OrderService(GenericRepository<Order> order)
        {
            _order = order;
        }
        public async Task<OutputHandler> Create(OrderDTO orderDTO)
        {
            try
            {
                var mapped = new AutoMapper<OrderDTO, Order>().MapToObject(orderDTO);
                mapped.CreatedDate = DateTime.Now;
                mapped.CreatedBy = orderDTO.LoggedInUsername;
                var result = await _order.Create(mapped);

                return result;
            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);
            }

        }

        //Code for deleting record
        public async Task<OutputHandler> DeleteRequest(OrderDTO orderDTO)
        {

            try
            {
                var order = await GetOrder(orderDTO.OrderId);
                order.DeletedBy = orderDTO.LoggedInUsername;

                var mapped = new AutoMapper<OrderDTO, Order>().MapToObject(order);
                var output = await _order.Update(mapped);
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


        public async Task<OutputHandler> DeleteApprove(OrderDTO orderDTO)
        {

            try
            {
                var order = await GetOrder(orderDTO.OrderId);
                order.IsDeleted = true;
                order.DeleteApprover = orderDTO.LoggedInUsername;
                order.DateDeleted = DateTime.Now;
                var mapped = new AutoMapper<OrderDTO, Order>().MapToObject(order);
                var output = await _order.Update(mapped);
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

        public async Task<OrderDTO> GetOrder(int orderId)
        {
            var output = await _order.GetSingleItem(x => x.OrderId == orderId);
            return new AutoMapper<Order, OrderDTO>().MapToObject(output);
        }

        public async Task<IEnumerable<OrderDTO>> GetAllOrders()
        {
            var output = await _order.GetAll();
            return new AutoMapper<Order, OrderDTO>().MapToList(output);
        }

        public async Task<OutputHandler> Update(OrderDTO orderDTO)
        {
            try
            {
                var mapped = new AutoMapper<OrderDTO, Order>().MapToObject(orderDTO);
                mapped.ModifiedBy = orderDTO.LoggedInUsername;
                mapped.ModifiedDate = DateTime.Now;


                var result = await _order.Update(mapped);
                return result;

            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);
            }
        }


    }

}
