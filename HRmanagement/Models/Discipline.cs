using System;
using System.Collections.Generic;

namespace HRmanagement.Models
{
    public partial class Discipline
    {
        public Discipline()
        {
            EmployeeHasDiscipline = new HashSet<EmployeeHasDiscipline>();
        }

        public string Id { get; set; }
        public string DisciplineName { get; set; }

        public virtual ICollection<EmployeeHasDiscipline> EmployeeHasDiscipline { get; set; }
    }
}
