using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheKeeperProWebAPI.Models;
using TheKeeperProWebAPI.HelperClasses;

namespace TheKeeperProWebAPI.Controllers
{
    /// <summary>
    /// API
    /// контроллер для таблицы Департаментов(аля роли)
    /// </summary>
    public class DepartmentController : Controller
    {
        private TheKeeperProPracticeContext db;

        /// <summary>
        /// API
        /// получение списка всех департаментов
        /// </summary>
        /// <returns>Json список всех департаментов</returns>
        [HttpPost]
        public async Task<JsonResult> GetAllDepartments()
        {
            db = new TheKeeperProPracticeContext();
            if (!await PassEmployees.Pass(db, Request.Body)) return (JsonResult)Results.BadRequest();

            JsonResult result = Json(await db.Departments.ToListAsync());
            return result;
        }
    }
}
