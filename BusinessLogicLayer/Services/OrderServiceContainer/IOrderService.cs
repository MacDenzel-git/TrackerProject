using AllinOne.DataHandlers;
using DataAccessLayer.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.OrderServiceContainer
{
    public interface IOrderService
    {
        Task<OutputHandler> Create(OrderDTO orderDTO);
        Task<OutputHandler> Update(OrderDTO orderDTO);
        Task<OutputHandler> DeleteApprove(OrderDTO orderDTO);
        Task<OutputHandler> DeleteRequest(OrderDTO orderDTO);
        Task<IEnumerable<OrderDTO>> GetAllOrders();
        Task<OrderDTO> GetOrder(int orderId);
    }
}
