using CompanyApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xunit;

namespace CompanyApiTest
{
    public class CompanyAPITest1
    {
        [Fact]
        public async void Should_create_company_when_name_not_exist()
        {
            //given
            TestServer server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            HttpClient client = server.CreateClient();
            Company company = new Company("1", "Shell");
            string request = JsonConvert.SerializeObject(company);
            StringContent requestBody = new StringContent(request, Encoding.UTF8, "application/json");
            //when
            var response = await client.PostAsync("Companies", requestBody);
            //then
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Company actualCompany = JsonConvert.DeserializeObject<Company>(responseString);
            Assert.Equal(company, actualCompany);
        }

        [Fact]
        public async void Should_fail_add_when_name_already_exist()
        {
            //given
            TestServer server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            HttpClient client = server.CreateClient();
            Company company1 = new Company("1", "Shell");
            Company company2 = new Company("2", "HuaWei");
            Company company3 = new Company("3", "XiaoMi");
            List<Company> companyList = new List<Company>() { company1, company2, company3 };
            string request = JsonConvert.SerializeObject(companyList);
            StringContent requestBody = new StringContent(request, Encoding.UTF8, "application/json");
            //when
            await client.PostAsync("Companies/addNewCompaniesForTest", requestBody); //companyList加入几个初始company

            Company company4 = new Company("4", "Shell");
            string request2 = JsonConvert.SerializeObject(company4);
            StringContent requestBody2 = new StringContent(request2, Encoding.UTF8, "application/json");
            await client.PostAsync("Companies", requestBody2);
            //then
            var response = await client.GetAsync("Companies");
            //then
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            List<Company> actualCompanies = JsonConvert.DeserializeObject<List<Company>>(responseString);
            Assert.Equal(3, actualCompanies.Count);
        }
    }
}
