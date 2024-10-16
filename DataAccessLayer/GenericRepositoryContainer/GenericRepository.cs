using AllinOne.DataHandlers;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.GenericRepository
{
    public class GenericRepository<TEntity> : Repository<TEntity> where TEntity : class
    {
        private readonly TrackerDbContext _dBContext;
        public GenericRepository(TrackerDbContext dbContext) : base(dbContext)
        {
            _dBContext = dbContext;
        }
    }
}
