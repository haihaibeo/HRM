using HRmanagement.Interfaces;
using HRmanagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRmanagement.DataAccess.Repositories
{
    public class DbRepo : IDbRepo
    {
        private readonly HrmContext context;
        private EmployeeRepo employeeRepo;
        private DepartmentRepo departmentRepo;
        private RecordbookRepo recordbookRepo;
        private PositionRepo positionRepo;
        private PassportRepo passportRepo;
        public DbRepo(HrmContext context)
        {
            this.context = context;
        }

        public IEmployeeRepo Employees 
        {
            get
            {
                if (employeeRepo == null) employeeRepo = new EmployeeRepo(context);
                return employeeRepo;
            } 
        }

        public IRepository<Department> Departments
        {
            get
            {
                if (departmentRepo == null) departmentRepo = new DepartmentRepo(context);
                return departmentRepo;
            }
        }

        public IRepository<Recordbook> Recordbooks
        {
            get
            {
                if (recordbookRepo == null) recordbookRepo = new RecordbookRepo(context);
                return recordbookRepo;
            }
        }

        public IRepository<Position> Positions
        {
            get
            {
                if (positionRepo == null) positionRepo = new PositionRepo(context);
                return positionRepo;
            }
        }

        public IRepository<Passport> Passports
        {
            get
            {
                if (passportRepo == null) passportRepo = new PassportRepo(context);
                return passportRepo;
            }
        }

        public Task<int> SaveChangesAsync()
        {
            return context.SaveChangesAsync();
        }
    }
}
