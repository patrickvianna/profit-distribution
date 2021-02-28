using AutoMapper;
using DistribuicaoLucros.Domain.DTO;
using DistribuicaoLucros.Domain.Entities;
using DistribuicaoLucros.Domain.Interfaces.Service;
using DistribuicaoLucros.Domain.Interfaces.Tools;
using System;
using System.Threading.Tasks;

namespace DistribuicaoLucros.Service.Implementation
{
    public class ProfitDistributionService : IProfitDistributionService
    {
        private IEmployeeService _employeeService;
        private IDateTimeTools _dateTimeTools;
        private IMapper _mapper;

        public ProfitDistributionService(IEmployeeService employeeService, IDateTimeTools dateTimeTools, IMapper mapper)
        {
            _employeeService = employeeService;
            _dateTimeTools = dateTimeTools;
            _mapper = mapper;
        }

        public async Task<ProfitDistributionDto> GetProfitDistributionAsync(double valueToDistribute)
        {
            var employees = await _employeeService.GetAllCollectionAsync();

            var profit = new ProfitDistribution(employees, valueToDistribute, _dateTimeTools);
            profit.Calculate();

            return _mapper.Map<ProfitDistributionDto>(profit);
        }
    }
}
