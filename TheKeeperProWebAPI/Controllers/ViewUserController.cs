using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TheKeeperProWebAPI.Models;

namespace TheKeeperProWebAPI.Controllers
{
    public class ViewUserController : Controller
    {
        string? userlogin;
        TheKeeperProPracticeContext? db;
        [HttpGet]
        public IActionResult Login(int? a)
        {
            string message = string.Empty;
            if (a is not null || a == 0) { message = "Пароль или логин не совпадают"; }
            return View("Login", message);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> PrivateOffice() 
        {
            userlogin = User.Claims.First().Value;
            db = new TheKeeperProPracticeContext();
            Models.User? user = await db.Users.FirstOrDefaultAsync(e => e.Login == userlogin);
            return View(user);
        }

        [HttpPost]
        [Authorize]
        public async Task<IResult> PrivateOffice(User newUser)
        {
            userlogin = User.Claims.First().Value;
            db = new TheKeeperProPracticeContext();
            Models.User oldUser = await db.Users.FirstOrDefaultAsync(e => e.Login == userlogin) ?? new Models.User();
            ChangeDateUser(ref oldUser, newUser);
            db.Users.Update(oldUser);
            await db.SaveChangesAsync();
            return Results.Redirect("/ViewRequest/ListRequest");
        }

        private void ChangeDateUser(ref User oldUser, User newUser)
        {
            oldUser.Surname = newUser.Surname;
            oldUser.Name = newUser.Name;
            oldUser.Patronymic = newUser.Patronymic;
            oldUser.Telephone = newUser.Telephone;
            oldUser.Mail = newUser.Mail;
            oldUser.DateOfBorn = newUser.DateOfBorn;
            oldUser.PasportSeries = newUser.PasportSeries;
            oldUser.PasportNumber = newUser.PasportNumber;
        }
    }
}