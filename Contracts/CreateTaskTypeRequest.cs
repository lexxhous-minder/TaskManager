using System.ComponentModel.DataAnnotations;

namespace TaskManager.Contracts
{
    public class CreateTaskTypeRequest
    {
        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Description { get; set; }
    }
}
