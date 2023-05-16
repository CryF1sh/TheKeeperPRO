using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheKeeperProWebAPI.HelperClasses;
using TheKeeperProWebAPI.Models;
using TheKeeperProWebAPI.ResponesEntities;

namespace TheKeeperProWebAPI.Controllers
{
    /// <summary>
    /// API
    /// контроллер для таблицы сотрудников
    /// </summary>
    public class EmployerController : Controller
    {
        private TheKeeperProPracticeContext db;
        /// <summary>
        /// получение списка всех сотрудников
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> GetAllEmployers()
        {
            db = new TheKeeperProPracticeContext();
            if (!await PassEmployees.Pass(db, Request.Body)) return (JsonResult)Results.BadRequest();

            var employers = from emp in db.Employers
                            join department in db.Departments
                            on emp.DepartmentId equals department.DepartmentId
                            join division in db.Divisions
                            on emp.DivisionId equals division.DivisionId
                            select new EmployersResponse
                            {
                                employeeId = emp.EmployeeId,
                                name = emp.Name,
                                surname = emp.Surname,
                                departmentId = emp.DepartmentId,
                                departmentName = department.Name,
                                divisionId = emp.DivisionId,
                                divisionName = division.Name,
                            };

            JsonResult result = Json(await employers.ToListAsync());

            return result;
        }
    }
}
