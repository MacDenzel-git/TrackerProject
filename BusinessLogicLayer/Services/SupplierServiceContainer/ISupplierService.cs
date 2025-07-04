﻿using AllinOne.DataHandlers;
using DataAccessLayer.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.SupplierServiceContainer
{
    public interface ISupplierService
    {
        Task<OutputHandler> Create(SupplierDTO supplierDTO);
        Task<OutputHandler> Update(SupplierDTO supplierDTO);
        Task<OutputHandler> DeleteApprove(SupplierDTO supplierDTO);
        Task<OutputHandler> DeleteRequest(SupplierDTO supplierDTO);
        Task<IEnumerable<SupplierDTO>> GetAllSuppliers();
        Task<SupplierDTO> GetSupplier(int supplierId);
    }
}
