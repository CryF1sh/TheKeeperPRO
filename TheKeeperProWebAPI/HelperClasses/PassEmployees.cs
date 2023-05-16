using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.Json;
using TheKeeperProWebAPI.Models;

namespace TheKeeperProWebAPI.HelperClasses
{
    /// <summary>
    /// класс для пропуска сотрудников по коду
    /// </summary>
    public static class PassEmployees
    {
        /// <summary>
        /// собственно сам поиск сотрудника о коду в контексте БД
        /// </summary>
        /// <param name="context">контекст БД</param>
        /// <param name="employeeId">ИД сотрудника</param>
        /// <returns>Истина - если сотрудник есть, Ложь - если нет</returns>
        private static async Task<bool> EmployeeIs(TheKeeperProPracticeContext context, int employeeId)
        {
            var request = await context.Employers.FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
            if (request == null) return false;
            return true;
        }
        /// <summary>
        /// Извлечение из тела запроса структуры PassingEmployee, для дальнейшей верификации сотрудника
        /// </summary>
        /// <param name="context">Контекст базы данных</param>
        /// <param name="body">Тело POST-Запроса</param>
        /// <returns>Истина - Если удалась верификаци, Ложь - если нет</returns>
        public static async Task<bool> Pass(TheKeeperProPracticeContext context, Stream body)
        {
            PassingEmployee employeeId;
            using (var reader = new StreamReader(body, Encoding.UTF8))
            {
                string bodystr = await reader.ReadToEndAsync();
                try
                {
                    employeeId = JsonSerializer.Deserialize<PassingEmployee>(bodystr);
                }
                catch
                {
                    return false;
                }
            }
            return await EmployeeIs(context, employeeId.Id);
        }
    }
}
