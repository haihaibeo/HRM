using HRmanagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRmanagement
{
    public class EmployeeForDpm
    {
        public EmployeeForDpm(Employee e, Recordbook? rc, Position? ps, List<Discipline> disc)
        {
            this.Id = e.Id;
            this.Name = e.Name;
            this.DepartmentId = e.DepartmentId;
            this.WorkHour = e.Recordbook.Workload;
            this.Position = ps.Name;
            this.PositionId = e.PositionId;
            disc.ForEach(d => Disciplines.Add(d.DisciplineName));
            this.ContractStart = e.Recordbook.ContractStart;
            this.ContractEnd = e.Recordbook.ContractEnd;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string? DepartmentId { get; set; }
        public int? WorkHour { get; set; }
        public string PositionId { get; set; }
        public string Position { get; set; }
        public List<string> Disciplines { get; set; } = new List<string>();
        public DateTime? ContractStart { get; set; }
        public DateTime? ContractEnd { get; set; }
    }
}
