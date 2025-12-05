using System.ComponentModel.DataAnnotations;

namespace TaskManager.Contracts
{
    public class UpdateTaskTypeRequest
    {
        [StringLength(30)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
    }
}
