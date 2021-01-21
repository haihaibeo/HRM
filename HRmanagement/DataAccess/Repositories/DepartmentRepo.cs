using HRmanagement.Interfaces;
using HRmanagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRmanagement.DataAccess.Repositories
{
    public class DepartmentRepo : IRepository<Department>
    {
        private HrmContext context;

        public DepartmentRepo(HrmContext context)
        {
            this.context = context;
        }
        public void Add(Department item)
        {
            throw new NotImplementedException();
        }

        public Task<List<Department>> GetAllAsync()
        {
            return context.Department.ToListAsync();
        }

        public Task<Department> GetByIdAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync()
        {
            return context.SaveChangesAsync();
        }
    }
}
