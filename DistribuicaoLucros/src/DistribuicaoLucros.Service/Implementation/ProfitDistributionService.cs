using AutoMapper;
using DistribuicaoLucros.Domain.DTO;
using DistribuicaoLucros.Domain.Entities;
using DistribuicaoLucros.Domain.Interfaces.Service;
using DistribuicaoLucros.Domain.Interfaces.Tools;
using DistribuicaoLucros.Domain.Notification;
using System.Threading.Tasks;

namespace DistribuicaoLucros.Service.Implementation
{
    public class ProfitDistributionService : IProfitDistributionService
    {
        private IEmployeeService _employeeService;
        private IDateTimeTools _dateTimeTools;
        private IMapper _mapper;
        private NotificationContext _notificationContext;

        public ProfitDistributionService(IEmployeeService employeeService,
                                         IDateTimeTools dateTimeTools,
                                         IMapper mapper,
                                         NotificationContext notificationContext)
        {
            _employeeService = employeeService;
            _dateTimeTools = dateTimeTools;
            _mapper = mapper;
            _notificationContext = notificationContext;
        }

        public async Task<ProfitDistributionDto> GetProfitDistributionAsync(double valueToDistribute)
        {
            var employees = await _employeeService.GetAllCollectionAsync();

            var profit = new ProfitDistribution(employees, valueToDistribute, _dateTimeTools);
            if(profit.IsValid)
                profit.Calculate();

            if (profit.IsInvalid)
            {
                _notificationContext.AddNotifications(profit.Notifications);
                return null;
            }

            return _mapper.Map<ProfitDistributionDto>(profit);
        }
    }
}
