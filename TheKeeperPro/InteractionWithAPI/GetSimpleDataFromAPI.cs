using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TheKeeperPro.Class;
using TheKeeperPro.Forms.Windows;
using System.Text.Json;

namespace TheKeeperPro.InteractionWithAPI
{
    internal static class GetSimpleDataFromAPI
    {
        static HttpClient httpClient = new HttpClient();
        static PassingEmployee passingEmployee = MainWindow.PassingEmployee;

        public async static Task<IEnumerable<RequestToView>> GetAllRequestFromAPI()
        {
            return await APIRequests.POSTRequestToGetData<IEnumerable<RequestToView>, PassingEmployee>(passingEmployee, "Request/GetAllRequests");
        }

        public static List<string> GetAllType()
        {
            List<string> type = new List<string>()
            {
                "Одиночная",
                "Групповая"
            };
            return type;
        }

        public static async Task<IEnumerable<Division>> GetAllDivisions()
        {
            return await APIRequests.POSTRequestToGetData<IEnumerable<Division>, PassingEmployee>(passingEmployee, "Division/GetAllDivisions");
        }

        public static async Task<IEnumerable<RequestStatus>> GetAllStatuses()
        {
            return await APIRequests.POSTRequestToGetData<IEnumerable<RequestStatus>, PassingEmployee>(passingEmployee, "RequestStatus/GetAllRequestStatuses");
        }

        public static async Task<IEnumerable<VisitPurpose>> GetAllVisitPurpose()
        {
            return await APIRequests.POSTRequestToGetData<IEnumerable<VisitPurpose>, PassingEmployee>(passingEmployee, "VisitPurpose/GetAllVisitPurposes");
        }

        public static async Task<IEnumerable<Employer>> GetAllEmployers()
        {
            return await APIRequests.POSTRequestToGetData<IEnumerable<Employer>, PassingEmployee>(passingEmployee, "Employer/GetAllEmployers");
        }
    }
}
