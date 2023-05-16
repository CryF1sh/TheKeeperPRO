using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using TheKeeperProWebAPI.HelperClasses;
using TheKeeperProWebAPI.Models;
//using Newtonsoft.Json;
using System.Text;
using System.Text.Json;

namespace TheKeeperProWebAPI.Controllers
{
    /// <summary>
    /// API 
    /// Контроллер для таблицы Пользователи(посетители)
    /// </summary>
    public class UserController : Controller
    {
        private TheKeeperProPracticeContext? db;
        /// <summary>
        /// API
        /// получение списка всех пользователей(посетители)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> GetAllUsers()
        {
            db = new TheKeeperProPracticeContext();
            if (!await PassEmployees.Pass(db, Request.Body)) return (JsonResult)Results.BadRequest();

            var user = from us in db.Users
                        select new User
                        {
                            UserId = us.UserId,
                            Surname = us.Surname,
                            Name = us.Name,
                            Patronymic = us.Patronymic,
                            Mail = us.Mail,
                            Login = us.Login,
                            Password = us.Password,
                            OnBlackList = us.OnBlackList,
                            Telephone = us.Telephone,
                            DateOfBorn = us.DateOfBorn,
                            PasportNumber = us.PasportNumber,
                            PasportSeries = us.PasportSeries,
                            VisitersPhote = us.VisitersPhote,
                            PhotoOfPasport = us.PhotoOfPasport,
                        };
            JsonResult result = Json(await user.ToListAsync());
            return result;
        }
        [HttpPost]
        public async Task<JsonResult> UserVerification()
        {
            db = new TheKeeperProPracticeContext();
            var ladno = Request.Body;
            //if (await PassEmployees.Pass(db, Request.Body))
            {
                PassingEmployee passingEmployee;
                using (var reder = new StreamReader(ladno, Encoding.UTF8))
                {   
                    string bodystring = await reder.ReadToEndAsync();
                    try
                    {
                        passingEmployee = JsonSerializer.Deserialize<PassingEmployee>(bodystring);
                        var empl = (await (from emp in db.Employers
                                   where emp.EmployeeId == passingEmployee.Id
                                   select new Employer
                                   {
                                       EmployeeId = emp.EmployeeId,
                                       Name = emp.Name,
                                       Surname = emp.Surname,
                                       DepartmentId = emp.DepartmentId,
                                       DivisionId = emp.DivisionId,
                                   }).ToListAsync()).FirstOrDefault();
                        return Json(empl);
                    }
                    catch
                    {
                        return Json(new Employer());
                    }
                }
            }
            //return Json(new Employer());
        }
    }
}
