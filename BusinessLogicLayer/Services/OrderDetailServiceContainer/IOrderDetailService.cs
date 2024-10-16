using AllinOne.DataHandlers;
using DataAccessLayer.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.OrderDetailServiceContainer
{
    public interface IOrderDetailService
    {
        Task<OutputHandler> Create(OrderDetailDTO orderDetailDTO);
        Task<OutputHandler> Update(OrderDetailDTO orderDetailDTO);
        Task<OutputHandler> Delete(int OrderDetailId);
        Task<IEnumerable<OrderDetailDTO>> GetAllOrderDetails();
        Task<OrderDetailDTO> GetOrderDetail(int orderDetailId);
    }
}
