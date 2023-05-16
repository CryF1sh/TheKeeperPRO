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
    /// контроллер для таблицы списка сообщений
    /// </summary>
    public class MessageController : Controller
    {
        private TheKeeperProPracticeContext db;
        /// <summary>
        /// API
        /// Получение списка всех сообщений
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> GetAllMessages()
        {
            db = new TheKeeperProPracticeContext();
            if (!await PassEmployees.Pass(db, Request.Body)) return (JsonResult)Results.BadRequest();

            var message = from mess in db.Messages
                            select new Message
                            {
                                MessageId = mess.MessageId,
                                UserId = mess.UserId,
                                Message1 = mess.Message1, //крч из-за того что таблица названа мессаге то и поле назвалось мессаге1 
                            };
            JsonResult result = Json(await message.ToListAsync());
            return result;
        }
    }
}
