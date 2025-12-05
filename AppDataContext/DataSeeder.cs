using Microsoft.EntityFrameworkCore;

namespace TaskManager.AppDataContext
{
    public class DataSeeder
    {
        public static void SeedTaskTypes(TaskManagerDbContext context)
        {
            if (!context.TaskTypes.Any())
            {
                var tasks = new List<Models.TaskType>
            {
                new Models.TaskType { Name = "CREATE", Description = "Создана" },
                new Models.TaskType { Name = "ASSIGNED" , Description = "Назначена" },
                new Models.TaskType { Name = "IN PROGRESS" , Description = "Выполняется" },
                new Models.TaskType { Name = "COMPLETED" , Description = "Выполнена" },
                new Models.TaskType { Name = "CANCEL" , Description = "Отменена" },
            };
                context.AddRange(tasks);
                context.SaveChanges();
            }
        }
    }
}
