using System;
using System.Collections.Generic;

namespace HRmanagement.Models
{
    public partial class Award
    {
        public Award()
        {
            EmployeeHasAward = new HashSet<EmployeeHasAward>();
        }

        public string Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<EmployeeHasAward> EmployeeHasAward { get; set; }
    }
}
