using System;
using System.Collections.Generic;

namespace HRmanagement.Models
{
    public partial class Passport
    {
        public Passport()
        {
            Employee = new HashSet<Employee>();
        }

        public string Id { get; set; }
        public int? Number { get; set; }
        public string Serie { get; set; }
        public DateTime? DataIssue { get; set; }

        public virtual ICollection<Employee> Employee { get; set; }
    }
}
