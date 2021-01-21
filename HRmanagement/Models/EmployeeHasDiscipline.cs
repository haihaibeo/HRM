using System;
using System.Collections.Generic;

namespace HRmanagement.Models
{
    public partial class EmployeeHasDiscipline
    {
        public string EmployeeId { get; set; }
        public string EmployeeRecordbookId { get; set; }
        public string EmployeePassportId { get; set; }
        public string DisciplineId { get; set; }

        public virtual Discipline Discipline { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
