using Microsoft.AspNetCore.Mvc;
using TaskManager.Contracts;
using TaskManager.Interfaces;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskTypeController : ControllerBase
    {
        private readonly ITaskTypeServices _taskTypeServices;

        public TaskTypeController(ITaskTypeServices taskTypeServices)
        {
            _taskTypeServices = taskTypeServices;
        }

        #region Methods

        /// <summary>
        /// Получение списка типов задач
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var tasks = await _taskTypeServices.GetAllAsync();
                if (tasks == null || !tasks.Any())
                    return Ok(new { message = "Типы  задач не найдены" });

                return Ok(new { message = "Типы задач получены", data = tasks });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ошибка при получении списка типов задач", error = ex.Message });
            }
        }

        /// <summary>
        /// Получение типа задачи по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор в формате Int</param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var task = await _taskTypeServices.GetByIdAsync(id);
                if (task == null)
                    return NotFound(new { message = $"Тип задачи с идентификатором {id} не найден." });

                return Ok(new { message = $"Тип задачи с идентификатором получен {id}.", data = task });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Ошибка получения задачи с идентификатором {id}.", error = ex.Message });
            }
        }

        /// <summary>
        /// Создание типа задачи
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateTaskTypeAsync(CreateTaskTypeRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {

                await _taskTypeServices.CreateTaskTypeAsync(request);
                return Ok(new { message = "Тип задачи создан успешно" });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ошибка при создании типа задачи", error = ex.Message });

            }
        }

        /// <summary>
        /// Обновление типа задачи
        /// </summary>
        /// <param name="id">Идентификатор задачи</param>
        /// <param name="request">Параметры</param>
        /// <returns></returns>
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateTaskTypeAsync(int id, UpdateTaskTypeRequest request)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _taskTypeServices.UpdateTaskTypeAsync(id, request);
                return Ok(new { message = $"Тип задачи {id} - успешно обновлен" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Ошибка обновления типа задачи с идентификатором {id}", error = ex.Message });
            }
        }

        /// <summary>
        /// Удаление типа задачи
        /// </summary>
        /// <param name="id">Идентификатор типа задачи</param>
        /// <returns></returns>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteTaskTypeAsync(int id)
        {
            try
            {
                await _taskTypeServices.DeleteTaskTypeAsync(id);
                return Ok(new { message = $"Тип задачи {id} - успешно удален" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Ошибка удаления типа задачи с идентификаторм {id}", error = ex.Message });
            }
        }

        #endregion 
    }
}
