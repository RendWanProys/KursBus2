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
    public class RaceService : BaseService<Race>
    {
        private HttpClient httpClient;
        public RaceService()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization",
               "Bearer " + RegisterUser.access_token);
        }
        public override async Task Add(Race obj)
        {
            try
            {
                JsonContent content = JsonContent.Create(obj);
                using var response = await httpClient.PostAsync("https://localhost:7229/api/Chitateli", content);
                string responseText = await response.Content.ReadAsStringAsync();
                if (responseText != null)
                {
                    Race resp = JsonSerializer.Deserialize<Race>(responseText!)!;
                    if (resp == null) MessageBox.Show(responseText);
                }
            }
            catch { }
        }

        public override async Task Delete(Race obj)
        {
            using var response = await httpClient.DeleteAsync($"https://localhost:7229/api/Chitateli/{obj.RaceId}");

        }

        public override async Task<List<Race>> GetAll()
        {
            return (await httpClient.GetFromJsonAsync<List<Race>>("https://localhost:7229/api/Chitateli"))!;
        }


        public override Task<List<Race>> Search(string str)
        {
            throw new NotImplementedException();
        }

        public override async Task Update(Race obj)
        {
            try
            {
                JsonContent content = JsonContent.Create(obj);
                using var response = await httpClient.PutAsync($"https://localhost:7229/api/Chitateli/{obj.RaceId}", content);
                string responseText = await response.Content.ReadAsStringAsync();
                if (responseText != null)
                {
                    Race resp = JsonSerializer.Deserialize<Race>(responseText!)!;
                    if (resp == null) MessageBox.Show(responseText);
                }

            }
            catch { }
        }
    }
}
