using AutoMapper;
using DistribuicaoLucros.Domain.DTO;
using DistribuicaoLucros.Domain.Entities;
using DistribuicaoLucros.Domain.Interfaces.Repository;
using DistribuicaoLucros.Domain.Interfaces.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DistribuicaoLucros.Service.Implementation
{
    public class EmployeeService : IEmployeeService
    {
        private IEmployeeRepository _repository;
        private IMapper _mapper;

        public EmployeeService(IEmployeeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<Employee>> GetAllCollectionAsync()
        {
            return  await _repository.GetAllCollectionAsync();
        }

        public async Task<List<EmployeeDto>> GetAllCollectionDtoAsync()
        {
            var employees = await GetAllCollectionAsync();
            return _mapper.Map<List<EmployeeDto>>(employees);
        }

        public async Task InsertCollectionAsync(List<EmployeeDto> employessDto)
        {
            var employess = _mapper.Map<List<Employee>>(employessDto);
            await _repository.InsertCollectionAsync(employess);
        }

        public async Task DeleteCollection()
        {
            await _repository.DeleteCollection();
        }        
    }
}
