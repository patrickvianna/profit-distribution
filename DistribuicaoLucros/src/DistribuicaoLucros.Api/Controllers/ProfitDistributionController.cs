using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DistribuicaoLucros.Domain.DTO;
using DistribuicaoLucros.Domain.Interfaces.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DistribuicaoLucros.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfitDistributionController : ControllerBase
    {
        private IProfitDistributionService _profitDistributionService;

        public ProfitDistributionController(IProfitDistributionService profitDistributionService)
        {
            _profitDistributionService = profitDistributionService;
        }
        /// <summary>
        /// Calcular a distribuição de lucros para todos os funcionários cadastrados
        /// </summary>
        /// <remarks>
        /// <param name="codigoAgenda"></param>
        /// <returns>Agenda atualizada.</returns>
        /// <response code="200">Confirmação realizado com sucesso.</response>
        /// <response code="500">Erro de sistema.</response>
        [HttpGet("calculeProfitDistribution/{valueToDistribute}")]
        public async Task<ProfitDistributionDto> CalculeProfitDistributionAsync(double valueToDistribute)
        {
            return await _profitDistributionService.GetProfitDistributionAsync(valueToDistribute);
        }
    }
}
