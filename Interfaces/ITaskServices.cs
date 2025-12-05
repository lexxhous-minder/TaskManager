using TaskManager.Contracts;

namespace TaskManager.Interfaces
{
    public interface ITaskServices
    {
        Task<IEnumerable<Models.Task>> GetAllAsync();
        Task<Models.Task> GetByIdAsync(Guid id);
        Task CreateTaskAsync(CreateTaskRequest request);
        Task UpdateTaskAsync(Guid id, UpdateTaskRequest request);
        Task DeleteTaskAsync(Guid id);
    }
}
