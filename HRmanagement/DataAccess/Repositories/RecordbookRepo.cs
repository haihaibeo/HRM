using HRmanagement.Interfaces;
using HRmanagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRmanagement.DataAccess.Repositories
{
    public class RecordbookRepo : IRepository<Recordbook>
    {
        private readonly HrmContext context;

        public RecordbookRepo(HrmContext context)
        {
            this.context = context;
        }

        public void Add(Recordbook item)
        {
            context.Add(item);
        }

        public Task<List<Recordbook>> GetAllAsync()
        {
            return context.Recordbook.ToListAsync();
        }

        public Task<Recordbook> GetByIdAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync()
        {
            return context.SaveChangesAsync();
        }
    }
}
