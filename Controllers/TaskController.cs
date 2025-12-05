using Microsoft.AspNetCore.Mvc;
using TaskManager.Contracts;
using TaskManager.Interfaces;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskServices _taskServices;

        public TaskController(ITaskServices taskServices)
        {
            _taskServices = taskServices;
        }

        #region Methods

        /// <summary>
        /// Получение списка задач
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var tasks = await _taskServices.GetAllAsync();
                if (tasks == null || !tasks.Any())
                    return Ok(new { message = "Задачи не найдены" });

                return Ok(new { message = "Задачи получены", data = tasks });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ошибка при получении списка задач", error = ex.Message });
            }
        }

        /// <summary>
        /// Получение задачи по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор в формате GUID</param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                var task = await _taskServices.GetByIdAsync(id);
                if (task == null)
                    return NotFound(new { message = $"Задача с идентификатором {id} не найдена." });

                return Ok(new { message = $"Задача с идентификатором получена {id}.", data = task });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Ошибка получения задачи с идентификатором {id}.", error = ex.Message });
            }
        }

        /// <summary>
        /// Создание задачи
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateTaskAsync(CreateTaskRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {

                await _taskServices.CreateTaskAsync(request);
                return Ok(new { message = "Задача создана успешно" });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ошибка при создании задачи", error = ex.Message });

            }
        }

        /// <summary>
        /// Обновление задачи
        /// </summary>
        /// <param name="id">Идентификатор задачи</param>
        /// <param name="request">Параметры</param>
        /// <returns></returns>
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateTaskAsync(Guid id, UpdateTaskRequest request)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                //var task = await _taskServices.GetByIdAsync(id);
                //if (task == null)
                //    return NotFound(new { message = $"Задача с идентификатором {id} не найдена" });

                await _taskServices.UpdateTaskAsync(id, request);
                return Ok(new { message = $"задача {id} - успешно обновлена" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Ошибка обновления задачи с идентификатором {id}", error = ex.Message });
            }
        }

        /// <summary>
        /// Удаление задачи
        /// </summary>
        /// <param name="id">Идентификатор задачи</param>
        /// <returns></returns>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteTaskAsync(Guid id)
        {
            try
            {
                await _taskServices.DeleteTaskAsync(id);
                return Ok(new { message = $"Задача {id} - успешно удалена" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Ошибка удаления задачи с идентификаторм {id}", error = ex.Message });
            }
        }

        #endregion
    }
}
