using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class TaskType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public TaskType() {}
        public TaskType(string name)
        {
            Name = name.ToUpper(); 
        }
    }
}
