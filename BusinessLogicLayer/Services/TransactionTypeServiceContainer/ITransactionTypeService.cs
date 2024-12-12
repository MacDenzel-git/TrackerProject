using AllinOne.DataHandlers;
using DataAccessLayer.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.TransactionTypeServiceContainer
{
    public interface ITransactionTypeService
    {
        Task<OutputHandler> Create(TransactionTypeDTO transactionTypeDTO);
        Task<OutputHandler> Update(TransactionTypeDTO transactionTypeDTO);
        Task<OutputHandler> DeleteApprove(TransactionTypeDTO transactionTypeDTO);
        Task<OutputHandler> DeleteRequest(TransactionTypeDTO transactionTypeDTO);
        Task<IEnumerable<TransactionTypeDTO>> GetAllTransactionTypes();
        //Task<TransactionTypeDTO> GetTransactionType(int transactionTypeId);
    }
}
