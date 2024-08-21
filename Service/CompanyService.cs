using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service
{
    internal sealed class CompanyService : ICompanyService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public CompanyService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<CompanyDto> CreateCompany(CompanyForCreationDto company)
        {
            var companyEntity = _mapper.Map<Company>(company);
            _repository.Company.CreateCompany(companyEntity);
            await _repository.Save();

            var CompanyToReturn = _mapper.Map<CompanyDto>(companyEntity);

            return CompanyToReturn;
        }

        public async Task<IEnumerable<CompanyDto>> GetAllCompanies(bool trackChanges)
        { 
            var companies = await _repository.Company.GetAllCompanies(trackChanges);
            var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);
            return companiesDto;
                
        }

        public async Task<CompanyDto> GetCompany(Guid companyId, bool trackChanges)
        {
           var company = await _repository.Company.GetCompany(companyId, trackChanges);
            if(company is null)
            {
                throw new CompanyNotFoundException(companyId);
            }

            var companyDto = _mapper.Map<CompanyDto>(company);

            return companyDto;
        }

        public async Task<IEnumerable<CompanyDto>> GetByIds(IEnumerable<Guid> ids, bool trackChanges)
        {
            if(ids is null)
            {
                throw new IdParametersBadRequestException();
            }
            var companyEntities = await _repository.Company.GetByIds(ids, trackChanges);
            if (ids.Count() != companyEntities.Count())
                throw new CollectionByIdsBadRequestException();
            var companiesToReturn = _mapper.Map<IEnumerable<CompanyDto>>(companyEntities);
            return companiesToReturn;

        }

        public async Task<(IEnumerable<CompanyDto> companies, string ids)> CreateCompanyCollection(IEnumerable<CompanyForCreationDto> companyCollection)
        {
            if(companyCollection is null)
            {
                throw new CompanyCollectionBadRequest();
            }
            var companyEntities = _mapper.Map<IEnumerable<Company>>(companyCollection);
            foreach(var company in companyEntities)
            {
                _repository.Company.CreateCompany(company);
            }

            await _repository.Save();
            var companyCollectionToReturn = _mapper.Map<IEnumerable<CompanyDto>>(companyEntities);
            var ids = string.Join(",", companyCollectionToReturn.Select(c =>c.id));

            return (companies: companyCollectionToReturn, ids: ids);

        }

        public async Task DeleteCompany(Guid companyId, bool trackChanges)
        {
            var company = await _repository.Company.GetCompany(companyId, trackChanges);
            if(company is null)
                throw new CompanyNotFoundException(companyId);
            try
            {
                _repository.Company.DeleteCompany(company);
                await _repository.Save();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting company: {ex.Message}");
                throw; // Re-throw or handle accordingly
            }
        }

        public async Task UpdateCompany(Guid companyId, CompanyForUpdateDto companyForUpdateDto, bool trackChanges)
        {
            var companyEntity = await _repository.Company.GetCompany(companyId, trackChanges);
            if(companyEntity is null)
                throw new CompanyNotFoundException(companyId);
            _mapper.Map(companyForUpdateDto, companyEntity);
            await _repository.Save();
        }
    }
}
