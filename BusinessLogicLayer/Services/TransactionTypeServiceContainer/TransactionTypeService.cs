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

namespace BusinessLogicLayer.Services.TransactionTypeServiceContainer
{

    public class TransactionTypeService : ITransactionTypeService
    {
        private readonly GenericRepository<TransactionType> _transactionType;
        public TransactionTypeService(GenericRepository<TransactionType> transactionType)
        {
            _transactionType = transactionType;
        }
        public async Task<OutputHandler> Create(TransactionTypeDTO transactionTypeDTO)
        {
            try
            {
                var mapped = new AutoMapper<TransactionTypeDTO, TransactionType>().MapToObject(transactionTypeDTO);
                mapped.CreatedDate = DateTime.Now;
                mapped.CreatedBy = transactionTypeDTO.LoggedInUsername;
                var result = await _transactionType.Create(mapped);

                return result;
            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);
            }

        }

        public async Task<OutputHandler> DeleteRequest(TransactionTypeDTO transactionTypeDTO)
        {

            try
            {
                var transactionType = await GetTransactionType(transactionTypeDTO.TransactionTypeId);
                transactionType.DeletedBy = transactionTypeDTO.LoggedInUsername;

                var mapped = new AutoMapper<TransactionTypeDTO, TransactionType>().MapToObject(transactionType);
                var output = await _transactionType.Update(mapped);
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


        public async Task<OutputHandler> DeleteApprove(TransactionTypeDTO transactionTypeDTO)
        {

            try
            {
                var transactionType = await GetTransactionType(transactionTypeDTO.TransactionTypeId);
                transactionType.IsDeleted = true;
                transactionType.DeleteApprover = transactionTypeDTO.LoggedInUsername;
                transactionType.DateDeleted = DateTime.Now;
                var mapped = new AutoMapper<TransactionTypeDTO, TransactionType>().MapToObject(transactionType);
                var output = await _transactionType.Update(mapped);
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


        public async Task<TransactionTypeDTO> GetTransactionType(int transactionTypeId)
        {
            var output = await _transactionType.GetSingleItem(x => x.TransactionTypeId == transactionTypeId);
            return new AutoMapper<TransactionType, TransactionTypeDTO>().MapToObject(output);
        }

        public async Task<IEnumerable<TransactionTypeDTO>> GetAllTransactionTypes()
        {
            var output = await _transactionType.GetAll();
            return new AutoMapper<TransactionType, TransactionTypeDTO>().MapToList(output);
        }

        public async Task<OutputHandler> Update(TransactionTypeDTO transactionTypeDTO)
        {
            try
            {
                var mapped = new AutoMapper<TransactionTypeDTO, TransactionType>().MapToObject(transactionTypeDTO);
                mapped.ModifiedBy = transactionTypeDTO.LoggedInUsername;
                mapped.ModifiedDate = DateTime.Now;


                var result = await _transactionType.Update(mapped);
                return result;

            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);
            }
        }


    }

}
