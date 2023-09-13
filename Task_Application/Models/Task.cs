using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Task_Application.Models
{
    public partial class Task
    {
        [Key]
        [StringLength(50)]
        public string TaskId { get; set; } = null!;
        [StringLength(100)]
        public string? TaskDescription { get; set; }
        [StringLength(50)]
        public string? IsDone { get; set; }
        [StringLength(50)]
        public string? Deadline { get; set; }
        public int? Assigned { get; set; }

        [ForeignKey("Assigned")]
        [InverseProperty("Tasks")]
        public virtual Employee? AssignedNavigation { get; set; }
    }
}
