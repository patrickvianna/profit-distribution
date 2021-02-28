using DistribuicaoLucros.Domain.DTO;
using System.Threading.Tasks;

namespace DistribuicaoLucros.Domain.Interfaces.Service
{
    public interface IProfitDistributionService
    {
        Task<ProfitDistributionDto> GetProfitDistributionAsync(double valueToDistribute);
    }
}
