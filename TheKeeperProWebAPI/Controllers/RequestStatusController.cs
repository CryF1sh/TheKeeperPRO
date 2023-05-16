using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheKeeperProWebAPI.HelperClasses;
using TheKeeperProWebAPI.Models;

namespace TheKeeperProWebAPI.Controllers
{
    /// <summary>
    /// API
    /// контроллер для таблицы статусов заявок
    /// </summary>
    public class RequestStatusController : Controller
    {
        private TheKeeperProPracticeContext db;
        /// <summary>
        /// API
        /// получение списка всех статусов заявок
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> GetAllRequestStatuses()
        {
            db = new TheKeeperProPracticeContext();
            if (!await PassEmployees.Pass(db, Request.Body)) return (JsonResult)Results.BadRequest();

            var requestStatus = from rs in db.RequestStatuses
                                select new RequestStatus
                                {
                                    RequestStatusId = rs.RequestStatusId,
                                    Name = rs.Name,
                                };
            JsonResult result = Json(await requestStatus.ToListAsync());
            return result;
        }
    }
}
