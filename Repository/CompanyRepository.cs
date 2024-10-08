﻿using Contracts;
using Entities.Models;
using Repository.Configuration;

namespace Repository
{
    internal sealed class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }
        public IEnumerable<Company> GetAllCompanies(bool trackChanges) => FindAll(trackChanges).OrderBy(c => c.Name).ToList();

        public Company GetCompany(Guid companyId, bool trackChanges)
        {
            var company = FindByCondition(c => c.Id.Equals(companyId), trackChanges).SingleOrDefault();

            return company;
            
        }
    }
}
