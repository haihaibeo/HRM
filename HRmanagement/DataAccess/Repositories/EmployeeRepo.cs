
using HRmanagement.Interfaces;
using HRmanagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRmanagement.DataAccess.Repositories
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly HrmContext context;

        public EmployeeRepo(HrmContext context)
        {
            this.context = context;
        }

        public void Add(Employee item)
        {
            context.Add(item);
        }

        public void ChangePosition(string empId, string posId)
        {
            var emp = context.Employee.FirstOrDefault(e => e.Id == empId);
            var pos = context.Position.FirstOrDefault(pos => pos.Id == posId);
            if (emp != null && pos != null)
                emp.PositionId = posId;
        }

        public Task<List<Employee>> GetAllAsync()
        {
            return context.Employee.ToListAsync();
        }

        public async Task<List<Discipline>> GetAllDisc(string empId)
        {
            var empHasDisc = await context.EmployeeHasDiscipline.Where(ehs => ehs.EmployeeId == empId).ToListAsync();
            var allDisc = await context.Discipline.ToListAsync();
            var foundDisc = new List<Discipline>();
            foreach(var d in allDisc)
                foreach(var ehd in empHasDisc)
                    if (d.Id == ehd.DisciplineId) foundDisc.Add(d);

            return foundDisc;
        } 

        public Task<Employee> GetByIdAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync()
        {
            return context.SaveChangesAsync();
        }
    }

    public interface IEmployeeRepo : IRepository<Employee>
    {
        Task<List<Discipline>> GetAllDisc(string empId);
        void ChangePosition(string empId, string posId);
    }
}
