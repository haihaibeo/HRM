using HRmanagement.DataAccess.Repositories;
using HRmanagement.Interfaces;
using HRmanagement.Models;
using System.Threading.Tasks;

namespace HRmanagement
{
    public interface IDbRepo
    {
        IEmployeeRepo Employees { get; }
        IRepository<Department> Departments { get; }
        IRepository<Recordbook> Recordbooks { get; }
        IRepository<Position> Positions { get; }
        IRepository<Passport> Passports { get; }
        Task<int> SaveChangesAsync();
    }
}