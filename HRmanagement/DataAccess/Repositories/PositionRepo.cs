using HRmanagement.Interfaces;
using HRmanagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRmanagement.DataAccess.Repositories
{
    public class PositionRepo : IRepository<Position>
    {
        private readonly HrmContext context;

        public PositionRepo(HrmContext context)
        {
            this.context = context;
        }

        public void Add(Position item)
        {
            throw new NotImplementedException();
        }

        public Task<List<Position>> GetAllAsync()
        {
            return context.Position.ToListAsync();
        }

        public Task<Position> GetByIdAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync()
        {
            return context.SaveChangesAsync();
        }
    }
}
