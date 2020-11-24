using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CompanyApi.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class CompaniesController : ControllerBase
    {
        private static List<Company> companies = new List<Company>();

        [HttpPost]
        public Company Post(Company addcompany)

        {
            foreach (var company in companies)
            {
                if (addcompany.Name == company.Name)
                {
                    BadRequest($"Exsiting company name {company.Name}");
                    return addcompany;
                }
            }

            companies.Add(addcompany);
            return addcompany;
        }

        [HttpPost("addNewCompaniesForTest")]
        public List<Company> AddCompany(List<Company> newCompanies)
        {
            foreach (var newCompany in newCompanies)
            {
                companies.Add(newCompany);
            }

            return companies;
        }

        [HttpGet]
        public List<Company> GetAll()
        {
            return companies;
        }

        [HttpGet("{name}")]
        public Company GetByName(string name)
        {
            return companies.FirstOrDefault(company => company.Name == name);
        }

        //[HttpPost]
        //public async Task<ActionResult<Company>> PostAsync(Company addcompany)
        //{
        //    foreach (var company in companies)
        //    {
        //        if (addcompany.Name == company.Name)
        //        {
        //            return BadRequest(new Dictionary<string, string>() { { "message", $"{addcompany.Name} is already Existed" } });
        //        }
        //    }

        //    companies.Add(addcompany);
        //    return Ok(addcompany);
        //}
    }
}
