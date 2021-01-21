using System;
using System.Collections.Generic;

namespace HRmanagement.Models
{
    public partial class Recordbook
    {
        public Recordbook()
        {
            Employee = new HashSet<Employee>();
        }

        public string Id { get; set; }
        public string Number { get; set; }
        public DateTime? DateIssue { get; set; }
        public int? EnrollmentCount { get; set; }
        public int? DismissalCount { get; set; }
        public int? TransferCount { get; set; }
        public int? Workload { get; set; }
        public DateTime? ContractStart { get; set; }
        public DateTime? ContractEnd { get; set; }

        public virtual ICollection<Employee> Employee { get; set; }
    }
}
