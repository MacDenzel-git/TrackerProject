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

namespace BusinessLogicLayer.Services.SupplierServiceContainer
{

    public class SupplierService : ISupplierService
    {
        private readonly GenericRepository<Supplier> _supplier;
        public SupplierService(GenericRepository<Supplier> supplier)
        {
            _supplier = supplier;
        }
        public async Task<OutputHandler> Create(SupplierDTO supplierDTO)
        {
            try
            {
                var mapped = new AutoMapper<SupplierDTO, Supplier>().MapToObject(supplierDTO);
                mapped.CreatedDate = DateTime.Now;
                mapped.CreatedBy = supplierDTO.LoggedInUsername;
                var result = await _supplier.Create(mapped);

                return result;
            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);
            }

        }

        //Code for deleting record
        public async Task<OutputHandler> Delete(int supplierId)
        {

            try
            {
                var output = await _supplier.Delete(x => x.SupplierId == supplierId);
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

        public async Task<SupplierDTO> GetSupplier(int supplierId)
        {
            var output = await _supplier.GetSingleItem(x => x.SupplierId == supplierId);
            return new AutoMapper<Supplier, SupplierDTO>().MapToObject(output);
        }

        public async Task<IEnumerable<SupplierDTO>> GetAllSuppliers()
        {
            var output = await _supplier.GetAll();
            return new AutoMapper<Supplier, SupplierDTO>().MapToList(output);
        }

        public async Task<OutputHandler> Update(SupplierDTO supplierDTO)
        {
            try
            {
                var mapped = new AutoMapper<SupplierDTO, Supplier>().MapToObject(supplierDTO);
                mapped.ModifiedBy = supplierDTO.LoggedInUsername;
                mapped.ModifiedDate = DateTime.Now;


                var result = await _supplier.Update(mapped);
                return result;

            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);
            }
        }


    }

}
