using TheKeeperProWebAPI.ResponesEntities;
using TheKeeperProWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace TheKeeperProWebAPI.HelperClasses
{
    public static class RequestManager
    {
        static TheKeeperProPracticeContext? db;
        public static async Task<List<RequestResponse>> GetListOfRequestResponse(string userLogin)
        {
            db = new TheKeeperProPracticeContext();

            int userId = (await db.Users.FirstOrDefaultAsync(e => e.Login == userLogin)).UserId;

            List<RequestResponse> requestResponse = await (from request in db.Requests
                                                           join status in db.RequestStatuses
                                                               on request.RequestStatusId equals status.RequestStatusId
                                                           join visitPurpose in db.VisitPurposes
                                                               on request.VisitPurpouseId equals visitPurpose.VisitPurposeId
                                                           join employer in db.Employers
                                                               on request.EmployeeId equals employer.EmployeeId
                                                           join division in db.Divisions
                                                               on employer.DivisionId equals division.DivisionId
                                                           join listOfVisiters in db.ListOfVisiters
                                                               on request.RequestId equals listOfVisiters.RequestId
                                                           where listOfVisiters.UserId == userId
                                                           select new RequestResponse
                                                           {
                                                               requestId = request.RequestId,
                                                               groupRequest = request.GroupRequest,
                                                               desiredEndDate = request.DesiredEndDate,
                                                               desiredStartDate = request.DesiredStartDate,
                                                               visitPurpouseId = request.VisitPurpouseId,
                                                               visitPurpouseName = visitPurpose.Name,
                                                               employeeId = request.EmployeeId,
                                                               employeeName = employer.Name,
                                                               employeeSurname = employer.Surname,
                                                               employeeDivisionId = employer.DivisionId,
                                                               employeeDivisionName = division.Name,
                                                               organisation = request.Organisation,
                                                               note = request.Note,
                                                               requestStatusId = request.RequestStatusId,
                                                               requestStatusName = status.Name,
                                                               startDate = request.StartDate,
                                                               endDate = request.EndDate,
                                                               trueStartDate = request.TrueStartDate,
                                                               trueEndDate = request.TrueEndDate,
                                                               qrCode = request.QrCode,
                                                               trueStartDateInDivision = request.TrueStartDateInDivision,
                                                               trueEndDateInDivision = request.TrueEndDateInDivision,
                                                           }).ToListAsync();
            return requestResponse;
        }
        public static async Task<RequestResponse?> GetRequestResponse(Func<Request, bool> predicate)
        {
            db = new TheKeeperProPracticeContext();
            var requestResponse = await (from request in db.Requests
                                        join status in db.RequestStatuses
                                            on request.RequestStatusId equals status.RequestStatusId
                                        join visitPurpose in db.VisitPurposes
                                            on request.VisitPurpouseId equals visitPurpose.VisitPurposeId
                                        join employer in db.Employers
                                            on request.EmployeeId equals employer.EmployeeId
                                        join division in db.Divisions
                                            on employer.DivisionId equals division.DivisionId
                                        where predicate(request)
                                        select new RequestResponse
                                        {
                                            requestId = request.RequestId,
                                            groupRequest = request.GroupRequest,
                                            desiredEndDate = request.DesiredEndDate,
                                            desiredStartDate = request.DesiredStartDate,
                                            visitPurpouseId = request.VisitPurpouseId,
                                            visitPurpouseName = visitPurpose.Name,
                                            employeeId = request.EmployeeId,
                                            employeeName = employer.Name,
                                            employeeSurname = employer.Surname,
                                            employeeDivisionId = employer.DivisionId,
                                            employeeDivisionName = division.Name,
                                            organisation = request.Organisation,
                                            note = request.Note,
                                            requestStatusId = request.RequestStatusId,
                                            requestStatusName = status.Name,
                                            startDate = request.StartDate,
                                            endDate = request.EndDate,
                                            trueStartDate = request.TrueStartDate,
                                            trueEndDate = request.TrueEndDate,
                                            qrCode = request.QrCode,
                                            trueStartDateInDivision = request.TrueStartDateInDivision,
                                            trueEndDateInDivision = request.TrueEndDateInDivision,
                                        }).ToListAsync();
            return requestResponse.FirstOrDefault();
        }
        public static async Task<RequestToView?> GetRequestToView(Func<RequestToView, bool> predicate)
        {
            db = new TheKeeperProPracticeContext();

            var requestToView = await (from request in db.Requests
                                 join status in db.RequestStatuses
                                     on request.RequestStatusId equals status.RequestStatusId
                                 join visitPurpose in db.VisitPurposes
                                     on request.VisitPurpouseId equals visitPurpose.VisitPurposeId
                                 join employer in db.Employers
                                     on request.EmployeeId equals employer.EmployeeId
                                 join division in db.Divisions
                                     on employer.DivisionId equals division.DivisionId
                                 join listOfCreater in (from listOfUser in db.ListOfVisiters 
                                                        join users in db.Users
                                                            on listOfUser.UserId equals users.UserId
                                                        where listOfUser.Creater == true
                                                        select new
                                                        {
                                                            RequestId = listOfUser.RequestId,
                                                            UserId = listOfUser.UserId,
                                                            Name = users.Name,
                                                            Surname = users.Surname,
                                                            Patronymic = users.Patronymic,
                                                            Mail = users.Mail,
                                                            Telephone = users.Telephone,
                                                            DateOfBorn = users.DateOfBorn,
                                                            OnBlackList = users.OnBlackList,
                                                            PasportNumber = users.PasportNumber,
                                                            PasportSeries = users.PasportSeries,
                                                        })
                                     on request.RequestId equals listOfCreater.RequestId
                                 select new RequestToView
                                 {
                                     requestId = request.RequestId,
                                     groupRequest = request.GroupRequest,
                                     desiredEndDate = request.DesiredEndDate,
                                     desiredStartDate = request.DesiredStartDate,
                                     visitPurpouseId = request.VisitPurpouseId,
                                     visitPurpouseName = visitPurpose.Name,
                                     employeeId = request.EmployeeId,
                                     employeeName = employer.Name,
                                     employeeSurname = employer.Surname,
                                     employeeDivisionId = employer.DivisionId,
                                     employeeDivisionName = division.Name,
                                     organisation = request.Organisation,
                                     note = request.Note,
                                     requestStatusId = request.RequestStatusId,
                                     requestStatusName = status.Name,
                                     startDate = request.StartDate,
                                     endDate = request.EndDate,
                                     trueStartDate = request.TrueStartDate,
                                     trueEndDate = request.TrueEndDate,
                                     qrCode = request.QrCode,
                                     trueStartDateInDivision = request.TrueStartDateInDivision,
                                     trueEndDateInDivision = request.TrueEndDateInDivision,
                                     requestCreaterId = listOfCreater.UserId,
                                     requestCreaterName = listOfCreater.Name,
                                     requestCreaterSurname = listOfCreater.Surname,
                                     requestCreaterPatronymic = listOfCreater.Patronymic,
                                     requestCreaterMail = listOfCreater.Mail,
                                     requestCreaterTelephone = listOfCreater.Telephone,
                                     requestCreaterDateOfBorn = listOfCreater.DateOfBorn,
                                     requestCreaterOnBlackList = listOfCreater.OnBlackList,
                                     requestCreaterPasportNumber = listOfCreater.PasportNumber,
                                     requestCreaterPasportSeries = listOfCreater.PasportSeries,
                                 }).ToListAsync();

            return requestToView.FirstOrDefault(predicate);
        }
        public static async Task<List<RequestToView>?> GetAllRequestToView()
        {
            db = new TheKeeperProPracticeContext();

            var requestToView = await (from request in db.Requests
                                       join status in db.RequestStatuses
                                           on request.RequestStatusId equals status.RequestStatusId
                                       join visitPurpose in db.VisitPurposes
                                           on request.VisitPurpouseId equals visitPurpose.VisitPurposeId
                                       join employer in db.Employers
                                           on request.EmployeeId equals employer.EmployeeId
                                       join division in db.Divisions
                                           on employer.DivisionId equals division.DivisionId
                                       join listOfCreater in (from listOfUser in db.ListOfVisiters
                                                              join users in db.Users
                                                                  on listOfUser.UserId equals users.UserId
                                                              where listOfUser.Creater == true
                                                              select new
                                                              {
                                                                  RequestId = listOfUser.RequestId,
                                                                  UserId = listOfUser.UserId,
                                                                  Name = users.Name,
                                                                  Surname = users.Surname,
                                                                  Patronymic = users.Patronymic,
                                                                  Mail = users.Mail,
                                                                  Telephone = users.Telephone,
                                                                  DateOfBorn = users.DateOfBorn,
                                                                  OnBlackList = users.OnBlackList,
                                                                  PasportNumber = users.PasportNumber,
                                                                  PasportSeries = users.PasportSeries,
                                                              })
                                           on request.RequestId equals listOfCreater.RequestId
                                       select new RequestToView
                                       {
                                           requestId = request.RequestId,
                                           groupRequest = request.GroupRequest,
                                           desiredEndDate = request.DesiredEndDate,
                                           desiredStartDate = request.DesiredStartDate,
                                           visitPurpouseId = request.VisitPurpouseId,
                                           visitPurpouseName = visitPurpose.Name,
                                           employeeId = request.EmployeeId,
                                           employeeName = employer.Name,
                                           employeeSurname = employer.Surname,
                                           employeeDivisionId = employer.DivisionId,
                                           employeeDivisionName = division.Name,
                                           organisation = request.Organisation,
                                           note = request.Note,
                                           requestStatusId = request.RequestStatusId,
                                           requestStatusName = status.Name,
                                           startDate = request.StartDate,
                                           endDate = request.EndDate,
                                           trueStartDate = request.TrueStartDate,
                                           trueEndDate = request.TrueEndDate,
                                           qrCode = request.QrCode,
                                           trueStartDateInDivision = request.TrueStartDateInDivision,
                                           trueEndDateInDivision = request.TrueEndDateInDivision,
                                           requestCreaterId = listOfCreater.UserId,
                                           requestCreaterName = listOfCreater.Name,
                                           requestCreaterSurname = listOfCreater.Surname,
                                           requestCreaterPatronymic = listOfCreater.Patronymic,
                                           requestCreaterMail = listOfCreater.Mail,
                                           requestCreaterTelephone = listOfCreater.Telephone,
                                           requestCreaterDateOfBorn = listOfCreater.DateOfBorn,
                                           requestCreaterOnBlackList = listOfCreater.OnBlackList,
                                           requestCreaterPasportNumber = listOfCreater.PasportNumber,
                                           requestCreaterPasportSeries = listOfCreater.PasportSeries,
                                       }).ToListAsync();

            return requestToView;
        }
    }
}
