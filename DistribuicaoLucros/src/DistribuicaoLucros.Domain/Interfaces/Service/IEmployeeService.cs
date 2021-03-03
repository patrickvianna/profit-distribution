using DistribuicaoLucros.Domain.DTO;
using DistribuicaoLucros.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DistribuicaoLucros.Domain.Interfaces.Service
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetAllCollectionAsync();
        Task<List<EmployeeDto>> GetAllCollectionDtoAsync();
        Task InsertCollectionAsync(List<EmployeeDto> employess);
        Task DeleteCollection();
    }
}
