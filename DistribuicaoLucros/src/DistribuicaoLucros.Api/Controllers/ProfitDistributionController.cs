using System.Threading.Tasks;
using DistribuicaoLucros.Domain.DTO;
using DistribuicaoLucros.Domain.Interfaces.Service;
using DistribuicaoLucros.Domain.Notification;
using Microsoft.AspNetCore.Mvc;

namespace DistribuicaoLucros.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfitDistributionController : ControllerBase
    {
        private IProfitDistributionService _profitDistributionService;
        private NotificationContext _notificationContext;


        public ProfitDistributionController(IProfitDistributionService profitDistributionService,
                                            NotificationContext notificationContext)
        {
            _profitDistributionService = profitDistributionService;
            _notificationContext = notificationContext;
        }

        /// <summary>
        /// Calcula qual o lucro que será distribuído para todos os funcionários cadastrados
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET
        ///     valueToDistribute = 000.00
        ///
        /// </remarks>
        /// <param name="valueToDistribute"></param>
        /// <returns>Objeto com o cálculo do lucro que será distribuído e os funcionários participantes desse cálculo</returns>
        /// <response code="200">OK</response>
        /// <response code="400">Regra de negócio.</response>
        /// <response code="500">Erro de sistema.</response>
        [HttpGet("calculeProfitDistribution/{valueToDistribute}")]
        public async Task<ProfitDistributionDto> CalculeProfitDistributionAsync(double valueToDistribute)
        {
            return await _profitDistributionService.GetProfitDistributionAsync(valueToDistribute);
        }
    }
}
