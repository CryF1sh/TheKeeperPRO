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
    /// Контроллер для таблицы цели посещения
    /// </summary>
    public class VisitPurposeController : Controller
    {
        private TheKeeperProPracticeContext db;
        /// <summary>
        /// API
        /// Получение списка всех целей
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> GetAllVisitPurposes()
        {
            db = new TheKeeperProPracticeContext();
            if (!await PassEmployees.Pass(db, Request.Body)) return (JsonResult)Results.BadRequest();

            var visitPurpose = from vp in db.VisitPurposes
                                select new VisitPurpose
                                {
                                    VisitPurposeId = vp.VisitPurposeId,
                                    Name = vp.Name,
                                };
            JsonResult result = Json(await visitPurpose.ToListAsync());
            return result;
        }
    }
}
