using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using TheKeeperProWebAPI.Models;
using TheKeeperProWebAPI.ResponesEntities;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using TheKeeperProWebAPI.HelperClasses;

namespace TheKeeperProWebAPI.Controllers
{
    /// <summary>
    /// API
    /// Контроллер для таблицы заявок
    /// </summary>
    public class RequestController : Controller
    {
        private TheKeeperProPracticeContext db;
        /// <summary>
        /// API
        /// Получение списка всех заявок
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> GetAllRequests()
        {
            db = new TheKeeperProPracticeContext();
            if (!await PassEmployees.Pass(db, Request.Body)) return (JsonResult)Results.BadRequest();
            JsonResult result = Json(await RequestManager.GetAllRequestToView());
            return result;
        }
    }
}