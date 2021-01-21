using System;
using System.Collections.Generic;

namespace HRmanagement.Models
{
    public partial class EmployeeHasAward
    {
        public string EmployeeId { get; set; }
        public string EmployeeRecordbookId { get; set; }
        public string EmployeePassportId { get; set; }
        public string AwardId { get; set; }

        public virtual Award Award { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
