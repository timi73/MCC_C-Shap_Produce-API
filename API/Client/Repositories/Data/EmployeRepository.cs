using API.Models;
using API.ViewModel;
using Client.Base;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client.Repositories.Data
{
    public class EmployeRepository : GeneralRepository<Employee, string>
    {
        private readonly Address address;
        private readonly string request;
        private readonly HttpClient httpClient;
        public EmployeRepository(Address address, string request = "Employees/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };
        }

        public async Task<List<GetRegisterVM>> RegisterGet()
        {
            List<GetRegisterVM> entities = new List<GetRegisterVM>();

            using (var response = await httpClient.GetAsync(request + "Register/Get/"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<GetRegisterVM>>(apiResponse);
            }
            return entities;
        }

        public async Task<GetRegisterVM> RegisterDetail(String NIK)
        {
            GetRegisterVM entity = null;

            using (var response = await httpClient.GetAsync(request + "Register/Get/" + NIK))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entity = JsonConvert.DeserializeObject<GetRegisterVM>(apiResponse);
            }
            return entity;
        }

        public Object AddRegister(RegisterVM registerVM)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(registerVM), Encoding.UTF8, "application/json");
            Object entities = new Object();
            using (var result = httpClient.PostAsync(address.link + request + "Register/", content).Result)
            {
                string apiResponse = result.Content.ReadAsStringAsync().Result;
                entities = JsonConvert.DeserializeObject<Object>(apiResponse);
            }
            
            return entities;
        }

        public Object UpdateRegisterData(RegisterVM registerVM)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(registerVM), Encoding.UTF8, "application/json");
            Object entities = new Object();
            using (var result = httpClient.PutAsync(request + "Register/Update/", content).Result) {
                string apiResponse = result.Content.ReadAsStringAsync().Result;
                entities = JsonConvert.DeserializeObject<Object>(apiResponse);
            }
                return entities;
        }
    }
}
