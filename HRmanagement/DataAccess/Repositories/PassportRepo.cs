using HRmanagement.Interfaces;
using HRmanagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRmanagement.DataAccess.Repositories
{
    public class PassportRepo : IRepository<Passport>
    {
        private readonly HrmContext context;

        public PassportRepo(HrmContext context)
        {
            this.context = context;
        }
        public void Add(Passport item)
        {
            context.Add(item);
        }

        public Task<List<Passport>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Passport> GetByIdAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
