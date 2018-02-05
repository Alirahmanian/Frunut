using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrunutStock.Data.Infrastructure;
using FrunutStock.Data.Repositories;
using FrunutStock.Model.Models;

namespace FrunutStock.Service
{
    // operations you want to expose
    public interface ICompanyService
    {
        IEnumerable<Company> GetCompanies(Int64? id);
        Company GetCompany(Int64 id);
        Company GetCompanyByName(string companyName);
        void CreateCompany(Company company);
        void SaveCompany();
    }

    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository companyRepository;
        private readonly IUnitOfWork unitOfWork;

        public CompanyService(ICompanyRepository companyRepository, IUnitOfWork unitOfWork)
        {
            this.companyRepository = companyRepository;
            this.unitOfWork = unitOfWork;
        }

        #region ICompanyService Members

        public IEnumerable<Company> GetCompanies(Int64? id)
        {
            if (id == null || id <= 0)
                return companyRepository.GetAll();
            else
                return companyRepository.GetAll().Where(o => o.ID == id);
        }

        public Company GetCompany(Int64 id)
        {
            var company = companyRepository.GetById(id);
            return company;
        }

        public Company GetCompanyByName(string companyName)
        {
            return companyRepository.GetCompanyByName(companyName);
        }

        public void CreateCompany(Company company)
        {
            companyRepository.Add(company);
        }

        public void SaveCompany()
        {
            unitOfWork.Commit();
        }

        #endregion
    }
}

