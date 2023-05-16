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
    /// контроллер для таблицы списка посетителей
    /// </summary>
    public class ListOfVisitersController : Controller
    {
        private TheKeeperProPracticeContext db;
        /// <summary>
        /// API
        /// Получение списка всех посетителей в заявках
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> GetAllListOfVisiters()
        {
            db = new TheKeeperProPracticeContext();
            if (!await PassEmployees.Pass(db, Request.Body)) return (JsonResult)Results.BadRequest();

            var listOfVisiters = from lov in db.ListOfVisiters
                                join user in db.Users
                                    on lov.UserId equals user.UserId
                                select new ListOfVisitersResponse
                                {
                                    listOfVisitersId = lov.ListOfVisitersId,
                                    requestId = lov.RequestId,
                                    userId = lov.UserId,
                                    userName = user.Name,
                                    userSurname = user.Surname,
                                    userMail = user.Mail,
                                    userTelephone = user.Telephone,
                                    usersPhoto = user.VisitersPhote,
                                    creater = lov.Creater,
                                };
            JsonResult result = Json(await listOfVisiters.ToListAsync());
            return result;
        }
    }
}
