using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TheKeeperProWebAPI.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc();
builder.Services.AddControllers();

builder.Services.AddAuthentication("Cookies").AddCookie(
    option => option.LoginPath = "/ViewUser/Login");
builder.Services.AddAuthorization();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(option =>
{
    option.IdleTimeout = TimeSpan.FromMinutes(40);
    option.Cookie.IsEssential = true;
});

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapPost("/ViewUser/Login", async (string? returnUrl, HttpContext context) =>
{
    var form = context.Request.Form;
    if (!form.ContainsKey("login") || !form.ContainsKey("password"))
        return Results.BadRequest("Не введены данные!");

    string? login = form["login"];
    string? password = form["password"];

    if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
        return Results.BadRequest("Не введены данные!");

    using(TheKeeperProPracticeContext db = new TheKeeperProPracticeContext())
    {
        var request = from users in db.Users
                   where users.Login == login && users.Password == password
                   select new 
                   { 
                       userId = users.UserId,
                   };
        var user = request.ToList();
        if (user.Count == 0) return Results.Redirect("/ViewUser/Login?a=0");
    }

    var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, login) };
    ClaimsIdentity identity = new ClaimsIdentity(claims, "Cookies");
    await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
    return Results.Redirect(returnUrl ?? "/ViewRequest/ListRequest");
});

app.MapGet("/logout", async (HttpContext context) =>
{
    await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    return Results.Redirect("/ViewUser/Login?a=1");
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ViewRequest}/{action=ListRequest}/{id?}");

app.UseStaticFiles();

app.Run();