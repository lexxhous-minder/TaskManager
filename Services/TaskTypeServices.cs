using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TaskManager.AppDataContext;
using TaskManager.Contracts;
using TaskManager.Interfaces;
using TaskManager.Models;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.Services
{
    public class TaskTypeServices : ITaskTypeServices
    {
        private readonly TaskManagerDbContext _context;
        private readonly ILogger<TaskServices> _logger;
        private readonly IMapper _mapper;

        public TaskTypeServices(TaskManagerDbContext context, ILogger<TaskServices> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task CreateTaskTypeAsync(CreateTaskTypeRequest request)
        {
            try
            {
                var value = _mapper.Map<TaskType>(request);
                _context.TaskTypes.Add(value);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при создании типа задачи.");
                throw new Exception("Ошибка при создании типа задачи.");
            }
        }

        public async Task DeleteTaskTypeAsync(int id)
        {
            var value = await _context.TaskTypes.FindAsync(id);
            if (value != null)
            {
                _context.TaskTypes.Remove(value);
                await _context.SaveChangesAsync();

            }
            else
            {
                throw new Exception($"Тип задачи с идентификатором {id} не найдена и не может быть удален.");
            }
        }

        public async Task<IEnumerable<TaskType>> GetAllAsync()
        {
            var value = await _context.TaskTypes.ToListAsync();
            if (value == null)
                throw new Exception("Типы задач не найдены");
            
            return value;
        }

        public async Task<TaskType> GetByIdAsync(int id)
        {
            var value = await _context.TaskTypes.FindAsync(id);
            if (value == null)
                throw new KeyNotFoundException($"Тип Задачи с идентификатором {id} не найден.");

            return value;
        }

        public async Task UpdateTaskTypeAsync(int id, UpdateTaskTypeRequest request)
        {
            try
            {
                var value = await _context.TaskTypes.FindAsync(id);
                if (value == null)
                    throw new Exception($"Задача с идентификатором {id} не найдена.");

                if (request.Name != null)
                    value.Name = request.Name.ToUpper();

                if (request.Description != null)
                    value.Description = request.Description;

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Тип Задачи с идентификатором {id} не может быть обновлен.");
                throw;
            }
        }
    }
}
