using System.Collections.Generic;
using CompanyApp.Interface;
using CompanyApp.Model;
using CompanyApp.Repository;

namespace CompanyApp.Controller
{
    public class LocationController
    {
        readonly IBaseInterface<Company> _companyRepository;

        public LocationController(string DbConStr)
        {
            _companyRepository = new CompanyRepository(DbConStr);
        }

        public Company Create(Company company)
        {
            return _companyRepository.Create(company);
        }

        public List<Company> Read()
        {
            return _companyRepository.Read();
        }

        public Company Read(int id)
        {
            return _companyRepository.Read(id);
        } 

        public Company Update(Company company)
        {
            return _companyRepository.Update(company);
        }

        public bool Delete(int id)
        {
            return _companyRepository.Delete(id);
        }

    }
}
