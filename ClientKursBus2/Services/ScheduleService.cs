using ClientKursBus2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace ClientKursBus2.Services
{
    public class ScheduleService : BaseService<Schedule>
    {
        private HttpClient httpClient;
        public ScheduleService()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization",
               "Bearer " + RegisterUser.access_token);
        }
        public override async Task Add(Schedule obj)
        {
            try
            {
                JsonContent content = JsonContent.Create(obj);
                using var response = await httpClient.PostAsync("https://localhost:7114/api/Schedule", content);
                string responseText = await response.Content.ReadAsStringAsync();
                if (responseText != null)
                {
                    Schedule resp = JsonSerializer.Deserialize<Schedule>(responseText!)!;
                    if (resp == null) MessageBox.Show(responseText);
                }
            }
            catch { }
        }

        public override async Task Delete(Schedule obj)
        {
            using var response = await httpClient.DeleteAsync($"https://localhost:7114/api/Schedule/{obj.TripId}");

        }

        public override async Task<List<Schedule>> GetAll()
        {
            return (await httpClient.GetFromJsonAsync<List<Schedule>>("https://localhost:7114/api/Schedule/"))!;
        }


        public override Task<List<Schedule>> Search(string str)
        {
            throw new NotImplementedException();
        }

        public override async Task Update(Schedule obj)
        {
            try
            {
                JsonContent content = JsonContent.Create(obj);
                using var response = await httpClient.PutAsync($"https://localhost:7114/api/Schedule/{obj.TripId}", content);
                string responseText = await response.Content.ReadAsStringAsync();
                if (responseText != null)
                {
                    Schedule resp = JsonSerializer.Deserialize<Schedule>(responseText!)!;
                    if (resp == null) MessageBox.Show(responseText);
                }

            }
            catch { }
        }
    }
}
