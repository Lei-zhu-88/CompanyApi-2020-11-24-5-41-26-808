using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyApi
{
    public class Company
    {
        public Company()
        {
        }

        public Company(string id, string name)
        {
            this.Id = id;

            this.Name = name;
        }

        public string Id { get; set; }
        public string Name { get; set; }

        public override bool Equals(object other)
        {
            var otherCompany = (Company)other;
            return Name == otherCompany.Name && Id == otherCompany.Id;
        }
    }
}
