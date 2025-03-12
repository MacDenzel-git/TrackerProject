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
        private readonly TrackerDbContext _context;
        private GenericRepository<InventoryTransaction> inventoryTransactionRepository;
        private GenericRepository<ShopProduct> shopProductRepository;
        private GenericRepository<JournalEntry> journalEntryRepository;
        private GenericRepository<CartItem> cartItemRepository;
       
        private IDbContextTransaction _transaction;
        public UnityOfWork(TrackerDbContext context)
        {
            this._context = context;
        }
        public GenericRepository<InventoryTransaction> InventoryTransactionRepository
        {
            get
            {
                if (this.inventoryTransactionRepository == null)
                {
                    this.inventoryTransactionRepository = new GenericRepository<InventoryTransaction>(_context);
                }
                return inventoryTransactionRepository;
            }

        }
           public GenericRepository<JournalEntry> JournalEntryRepository
        {
            get
            {
                if (this.journalEntryRepository == null)
                {
                    this.journalEntryRepository = new GenericRepository<JournalEntry>(_context);
                }
                return journalEntryRepository;
            }

        }
         
        public GenericRepository<CartItem> CartItemsRepository
        {
            get
            {
                if (this.cartItemRepository == null)
                {
                    this.cartItemRepository = new GenericRepository<CartItem>(_context);
                }
                return cartItemRepository;
            }

        }

        


        public GenericRepository<ShopProduct> ShopProductRepository
        {
            get
            {
                if (this.shopProductRepository == null)
                {
                    this.shopProductRepository = new GenericRepository<ShopProduct>(_context);
                }
                return shopProductRepository;
            }
        }






        public void Save()
        {
            _context.SaveChanges();
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
            _context.SaveChanges();
        }

        public void BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
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
            _context.Dispose();
        }


    }
}
