using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheKeeperProWebAPI.Models;
using TheKeeperProWebAPI.HelperClasses;
using Microsoft.AspNetCore.Authorization;

namespace TheKeeperProWebAPI.Controllers
{
    public class DivisionController : Controller
    {
        private TheKeeperProPracticeContext db;
        [HttpPost]
        public async Task<JsonResult> GetAllDivisions()
        {
            db = new TheKeeperProPracticeContext();
            if (!await PassEmployees.Pass(db, Request.Body)) return (JsonResult)Results.BadRequest();

            JsonResult result = Json(await db.Divisions.ToListAsync());
            return result;
        }
        [Authorize]
        [HttpGet]
        public async Task<JsonResult> GetEmployeersDivisionAutorisate(string div)
        {
            db = new TheKeeperProPracticeContext();

            int id = (await db.Divisions.FirstOrDefaultAsync(e => e.Name == div)).DivisionId;
            var result = (await db.Employers.ToListAsync()).Where(e => e.DivisionId == id).ToList();
            List<string> list = new List<string>();
            foreach (var item in result)
            {
                list.Add(item.Surname + " " + item.Name);
            }
            return Json(list);
        }
    }
}
