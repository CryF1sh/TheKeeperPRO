using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TheKeeperPro.Class;
using TheKeeperPro.Forms.Windows;

namespace TheKeeperPro.InteractionWithAPI
{
    internal static class APIRequests
    {
        static HttpClient httpClient = new HttpClient();
        public static async Task<Output> POSTRequestToGetData<Output, Input>(Input input, string dopUrl)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(input);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var mainUrl = Properties.Resources.ConnectionString.ToString() + dopUrl;

            var respounse = await httpClient.PostAsync(mainUrl, data);
            var stream = await respounse.Content.ReadAsStreamAsync();
            string result = new StreamReader(stream, Encoding.UTF8).ReadToEnd();
            return JsonSerializer.Deserialize<Output>(result);
        }
    }
}
