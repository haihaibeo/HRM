using System;
using System.Collections.Generic;

namespace HRmanagement.Models
{
    public partial class Employee
    {
        public Employee()
        {
            EmployeeHasAward = new HashSet<EmployeeHasAward>();
            EmployeeHasDiscipline = new HashSet<EmployeeHasDiscipline>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string TaxNumber { get; set; }
        public string PensionNumber { get; set; }
        public string Position { get; set; }
        public string Degree { get; set; }
        public string AwardName { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public string DepartmentId { get; set; }
        public string PositionId { get; set; }
        public string RecordbookId { get; set; }
        public string PassportId { get; set; }

        public virtual Department Department { get; set; }
        public virtual Passport Passport { get; set; }
        public virtual Position PositionNavigation { get; set; }
        public virtual Recordbook Recordbook { get; set; }
        public virtual ICollection<EmployeeHasAward> EmployeeHasAward { get; set; }
        public virtual ICollection<EmployeeHasDiscipline> EmployeeHasDiscipline { get; set; }
    }
}
