using DataAccessLayer.GenericRepository;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataAccessLayer.UnitOfWorkContainer
{

    public class UnityOfWork : IDisposable
    {
        private TrackerDbContext context = new TrackerDbContext();
        private GenericRepository<InventoryTransaction> inventoryTransactionRepository;
        private GenericRepository<ShopProduct> shopProductRepository;
       
        private IDbContextTransaction _transaction;

        public GenericRepository<InventoryTransaction> InventoryTransactionRepository
        {
            get
            {
                if (this.inventoryTransactionRepository == null)
                {
                    this.inventoryTransactionRepository = new GenericRepository<InventoryTransaction>(context);
                }
                return inventoryTransactionRepository;
            }

        }


        public GenericRepository<ShopProduct> ShopProductRepository
        {
            get
            {
                if (this.shopProductRepository == null)
                {
                    this.shopProductRepository = new GenericRepository<ShopProduct>(context);
                }
                return shopProductRepository;
            }
        }






        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        //protected virtual void Dispose(bool disposing)
        //{
        //    if (!this.disposed)
        //    {
        //        if (disposing)
        //        {
        //            context.Dispose();
        //        }
        //    }
        //    this.disposed = true;
        //}
        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public void BeginTransaction()
        {
            _transaction = context.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _transaction?.Commit();
        }

        public void RollbackTransaction()
        {
            _transaction?.Rollback();
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            context.Dispose();
        }


    }
}
