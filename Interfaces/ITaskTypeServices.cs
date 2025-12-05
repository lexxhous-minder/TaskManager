using TaskManager.Contracts;

namespace TaskManager.Interfaces
{
    public interface ITaskTypeServices
    {
        Task<IEnumerable<Models.TaskType>> GetAllAsync();
        Task<Models.TaskType> GetByIdAsync(int id);
        Task CreateTaskTypeAsync(CreateTaskTypeRequest request);
        Task UpdateTaskTypeAsync(int id, UpdateTaskTypeRequest request);
        Task DeleteTaskTypeAsync(int id);
    }
}
