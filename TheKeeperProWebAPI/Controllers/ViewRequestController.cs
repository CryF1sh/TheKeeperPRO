using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheKeeperProWebAPI.Models;
using TheKeeperProWebAPI.ResponesEntities;
using TheKeeperProWebAPI.HelperClasses;
using Microsoft.Data.SqlClient;

namespace TheKeeperProWebAPI.Controllers
{
    /// <summary>
    /// View
    /// контроллер для отображения заявок
    /// </summary>
    [Authorize]
    public class ViewRequestController : Controller
    {
        TheKeeperProPracticeContext? db;
        /// <summary>
        /// Создание новой заявки
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> NewRequest()
        {
            string userLogin = User.Claims.First().Value;
            db = new TheKeeperProPracticeContext();
            Models.User? user = await db.Users.FirstOrDefaultAsync(x => x.Login == userLogin);
            List<VisitPurpose> purposes = await db.VisitPurposes.ToListAsync();
            List<Employer> employers = (await db.Employers.ToListAsync()).Where(e => e.DivisionId is not null).ToList();
            List<Division> divisions = await db.Divisions.ToListAsync();

            ViewData["purposes"] = purposes;
            ViewData["employers"] = employers;
            ViewData["divisions"] = divisions;
            return View(user);
        }
        /// <summary>
        /// сохранение заявки в бд
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> NewRequest(string userId, DateTime startDate, DateTime endDate) 
        {
            db = new TheKeeperProPracticeContext();
            Models.Request request = new Models.Request();
            request.DesiredStartDate = startDate;
            request.DesiredEndDate = endDate;
            var req = await db.VisitPurposes.FirstOrDefaultAsync(e => e.Name == Request.Form["visitPurposeName"].ToString());
            request.VisitPurpouseId = req == null ? 1 : req.VisitPurposeId;
            var req1 = await db.Employers.FirstOrDefaultAsync(e => e.Name + " " + e.Surname == Request.Form["employeeName"].ToString());
            request.RequestStatusId = 1;
            request.EmployeeId = req1 == null ? 1 : req1.EmployeeId;
            request.GroupRequest = false;
            request.Note = Request.Form["note"].ToString();
            request.Organisation = Request.Form["organization"].ToString();

            List<ListOfVisiter> listOfVisiter = new List<ListOfVisiter>();
            listOfVisiter.Add(new ListOfVisiter
            {
                UserId = int.Parse(userId),
                Creater = true
            });

            request.ListOfVisiters = listOfVisiter;

            db.Requests.Add(request);
            db.SaveChanges();

            string userLogin = User.Claims.First().Value;

            List<RequestResponse> requests = await RequestManager.GetListOfRequestResponse(userLogin);
            return View("ListRequest", requests);
        }
        /// <summary>
        /// Просмотр списка всех заявок
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ListRequest()
        {
            string userLogin = User.Claims.First().Value;

            List<RequestResponse> requests = await RequestManager.GetListOfRequestResponse(userLogin);//GetListOfRequestResponseForList(userLogin); 
            return View(requests);
        }
        /// <summary>
        /// просмотр конкретной заявки
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> SeeRequest_Single(int id)
        {
            db = new TheKeeperProPracticeContext();
            RequestToView requestResponse = await RequestManager.GetRequestToView(e => e.requestId == id) ?? new RequestToView();
            return View(requestResponse);
        }
        [HttpGet]
        public async Task<IActionResult> SeeRequest_Group(int id)
        {
            db = new TheKeeperProPracticeContext();
            RequestToView request = await RequestManager.GetRequestToView(e => e.requestId == id) ?? new RequestToView();
            List<ListOfVisiter> visiters = await db.ListOfVisiters.Where(e => e.RequestId == id).ToListAsync();
            List<User> users = new List<User>(); 
            foreach (ListOfVisiter visiter in visiters)
            {
                users.Add(await db.Users.FirstOrDefaultAsync(e => e.UserId == visiter.UserId) ?? new User());
            }
            ViewData["request"] = request;
            ViewData["visiters"] = visiters;
            ViewData["users"] = users;
            return View();
        }
        /// <summary>
        /// Окно выбора конкретной заявки
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ChoiceType()
        {
            return View();
        }
        /// <summary>
        /// Создание новой групповой заявки
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> NewGroupRequest()
        {
            string userLogin = User.Claims.First().Value;
            db = new TheKeeperProPracticeContext();

            User user = await db.Users.FirstOrDefaultAsync(x => x.Login == userLogin) ?? new User();
            RequestResponse request = new RequestResponse();
            List<User> listOfVisiters = new List<User>();
            List<VisitPurpose> purposes = await db.VisitPurposes.ToListAsync();
            List<Employer> employers = (await db.Employers.ToListAsync()).Where(e => e.DivisionId is not null).ToList();
            List<Division> divisions = await db.Divisions.ToListAsync();

            if (HttpContext.Session.Keys.Contains(userLogin))
            {
                request = HttpContext.Session.Get<RequestResponse>("Request") ?? new RequestResponse();
                listOfVisiters = HttpContext.Session.Get<List<User>>("ListOfVisiters") ?? new List<User>();
            }

            HttpContext.Session.Set<User>(userLogin, user);
            HttpContext.Session.Set<RequestResponse>("Request", request);
            HttpContext.Session.Set<List<User>>("ListOfVisiters", listOfVisiters);

            ViewData["request"] = request;
            ViewData["listOfVisiters"] = listOfVisiters;
            ViewData["purposes"] = purposes;
            ViewData["employers"] = employers;
            ViewData["divisions"] = divisions;
            return View(user);
        }
        [HttpPost]
        public IActionResult AddUserInrequest(RequestResponse request)
        {
            HttpContext.Session.Set<RequestResponse>("Request", request);

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SaveUserInRequest(User newUser)
        {
            string userLogin = User.Claims.First().Value;
            db = new TheKeeperProPracticeContext();

            User user = HttpContext.Session.Get<User>(userLogin) ?? new User();
            RequestResponse request = HttpContext.Session.Get<RequestResponse>("Request") ?? new RequestResponse();
            List<User> listOfVisiters = HttpContext.Session.Get<List<User>>("ListOfVisiters") ?? new List<User>();
            List<VisitPurpose> purposes = await db.VisitPurposes.ToListAsync();
            List<Employer> employers = (await db.Employers.ToListAsync()).Where(e => e.DivisionId is not null).ToList();
            List<Division> divisions = await db.Divisions.ToListAsync();

            listOfVisiters.Add(newUser);

            HttpContext.Session.Set<List<User>>("ListOfVisiters", listOfVisiters);

            ViewData["request"] = request;
            ViewData["listOfVisiters"] = listOfVisiters;
            ViewData["purposes"] = purposes;
            ViewData["employers"] = employers;
            ViewData["divisions"] = divisions;
            return View("NewGroupRequest", user);
        }

        [HttpPost]
        public async Task<IActionResult> NewGroupRequest(RequestResponse requestResponse)
        {
            string userLogin = User.Claims.First().Value;
            db = new TheKeeperProPracticeContext();

            User curentUser = HttpContext.Session.Get<User>(userLogin) ?? new User();
            List<User> users = HttpContext.Session.Get<List<User>>("ListOfVisiters") ?? new List<User>();

            Request request = new Request();
            request.DesiredStartDate = requestResponse.desiredStartDate;
            request.DesiredEndDate = requestResponse.desiredEndDate;
            var req = await db.VisitPurposes.FirstOrDefaultAsync(e => e.Name == Request.Form["visitPurpouseName"].ToString());
            request.VisitPurpouseId = req == null ? 1 : req.VisitPurposeId;
            var req1 = await db.Employers.FirstOrDefaultAsync(e => e.Name + " " + e.Surname == Request.Form["employeeName"].ToString());
            request.RequestStatusId = 1;
            request.EmployeeId = req1 == null ? 1 : req1.EmployeeId;
            request.GroupRequest = true;
            request.Note = Request.Form["note"].ToString();
            request.Organisation = Request.Form["organization"].ToString();

            List<ListOfVisiter> listOfVisiters = new List<ListOfVisiter>()
            {
                new ListOfVisiter()
                {
                    UserId = curentUser.UserId,
                    Creater = true
                }
            };

            foreach (var newuser in users)
            {
                db.Users.Add(newuser);
                db.SaveChanges();
                listOfVisiters.Add(new ListOfVisiter
                {
                    UserId = (await db.Users.FirstOrDefaultAsync(e => e.Login == newuser.Login)).UserId,
                    Creater = false
                }) ;
            }

            request.ListOfVisiters = listOfVisiters;
            db.Requests.Add(request);

            db.SaveChanges();

            HttpContext.Session.Clear();
            List<RequestResponse> requests = await RequestManager.GetListOfRequestResponse(userLogin);
            return View("ListRequest", requests);
        }
    }
}
