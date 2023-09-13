using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Task_Application.Models
{
    public partial class Employee
    {
        public Employee()
        {
            Tasks = new HashSet<Task>();
        }

        [Key]
        public int EmployeeId { get; set; }
        [StringLength(50)]
        public string? FullName { get; set; }
        [StringLength(50)]
        public string? JobTitle { get; set; }
        [StringLength(50)]
        public string? Password { get; set; }
        [StringLength(50)]
        public string? Email { get; set; }

        [InverseProperty("AssignedNavigation")]
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
