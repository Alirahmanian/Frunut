using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrunutStock.Data.Infrastructure;
using FrunutStock.Model.Models;

namespace FrunutStock.Data.Repositories
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(IDbFactory dbFactory) : base(dbFactory) { }
        public Company GetCompanyById(Int64 companyId)
        {
            var company = this.DbContext.Companies.Where(c => c.ID == companyId).FirstOrDefault();

            return company;
        }

        public Company GetCompanyByName(string companyName)
        {
            var company = this.DbContext.Companies.Where(c => c.Name == companyName).FirstOrDefault();

            return company;
        }
                
    }

    public interface ICompanyRepository : IRepository<Company>
    {
        Company GetCompanyById(Int64 companyId);
        Company GetCompanyByName(string companyName);
    }
}
