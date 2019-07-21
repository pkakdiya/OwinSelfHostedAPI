using Owin.SelfHosted.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Owin.SelfHosted.API.Controller
{
    [Authorize]
    public class CompaniesController : ApiController
    {
        private static List<Company> companies = new List<Company>()
        {
            new Company() { Name= "Microsoft", Id =1 },
            new Company() { Name= "Google", Id =2 },
            new Company() { Name= "BMC", Id =3 }
        };

        public IEnumerable<Company> Get()
        {
            return companies;
        }

        public Company GetCompany(int Id)
        {
            var company = companies.Where(s => s.Id == Id).FirstOrDefault();

            if (company == null)
            {
                throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);
            }

            return company;
        }

        public IHttpActionResult Post(Company company)
        {
            if (company == null)
            {
                return BadRequest("Argument Null");
            }

            var companyExist = companies.Any(s => s.Id == company.Id);

            if (companyExist)
            {
                return BadRequest("Record Already Exist");
            }

            companies.Add(company);
            return Ok();
        }

        public IHttpActionResult Put(Company company)
        {
            if (company == null)
            {
                return BadRequest("Argument Null");
            }

            var companyExist = companies.FirstOrDefault(s => s.Id == company.Id);

            if (companyExist == null)
            {
                return NotFound();
            }

            companyExist.Name = company.Name;
            return Ok();
        }


        public IHttpActionResult Put(int id)
        {
            var companyExist = companies.FirstOrDefault(s => s.Id == id);

            if (companyExist == null)
            {
                return NotFound();
            }

            companies.Remove(companyExist);
            return Ok();
        }

    }
}
