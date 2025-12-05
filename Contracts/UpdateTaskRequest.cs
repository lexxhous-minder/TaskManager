using System.ComponentModel.DataAnnotations;
using TaskManager.Models;

namespace TaskManager.Contracts
{
    public class UpdateTaskRequest
    {
        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public int TaskTypeId { get; set; }

        public DateTime? DueDate { get; set; }

        [Range(1, 5)]
        public int? Priority { get; set; }
    }
}
