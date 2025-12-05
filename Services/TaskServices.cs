using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskManager.AppDataContext;
using TaskManager.Contracts;
using TaskManager.Interfaces;

namespace TaskManager.Services
{
    public class TaskServices : ITaskServices
    {
        private readonly TaskManagerDbContext _context;
        private readonly ILogger<TaskServices> _logger;
        private readonly IMapper _mapper;

        public TaskServices(TaskManagerDbContext context, ILogger<TaskServices> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task CreateTaskAsync(CreateTaskRequest request)
        {
            try
            {
                var task = _mapper.Map<Models.Task>(request);
                task.CreatedAt = DateTime.UtcNow;
                _context.Tasks.Add(task);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при создании задачи.");
                throw new Exception("Ошибка при создании задачи.");
            }
        }

        public async Task DeleteTaskAsync(Guid id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();

            }
            else
            {
                throw new Exception($"Задача с идентификатором {id} не найдена и не может быть удалена.");
            }
        }

        public async Task<IEnumerable<Models.Task>> GetAllAsync()
        {
            var tasks = await _context.Tasks.ToListAsync();
            if (tasks == null)
                throw new Exception("Задачи не найдены");
            
            return tasks;
        }

        public async Task<Models.Task> GetByIdAsync(Guid id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
                throw new KeyNotFoundException($"Задача с идентификатором {id} не найдена.");
            
            return task;
        }

        public async Task UpdateTaskAsync(Guid id, UpdateTaskRequest request)
        {
            try
            {
                var task = await _context.Tasks.FindAsync(id);
                if (task == null)
                    throw new Exception($"Задача с идентификатором {id} не найдена.");
                
                if (request.Title != null)
                    task.Title = request.Title;
                
                if (request.Description != null)
                    task.Description = request.Description;
                                
                if (request.DueDate != null)
                    task.DueDate = request.DueDate.Value;
                
                if (request.Priority != null)
                    task.Priority = request.Priority.Value;
                
                if (task.TaskTypeId != request.TaskTypeId)
                    task.TaskTypeId = request.TaskTypeId;

                task.UpdatedAt = DateTime.Now.ToUniversalTime();

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Задача с идентификатором {id} не может быть обновлена.");
                throw;
            }
        }
    }
}
