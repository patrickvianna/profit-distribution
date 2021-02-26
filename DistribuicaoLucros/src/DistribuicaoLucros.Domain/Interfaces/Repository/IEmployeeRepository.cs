using DistribuicaoLucros.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DistribuicaoLucros.Domain.Interfaces.Repository
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAllCollectionAsync();
        Task InsertCollectionAsync(List<Employee> employess);
    }
}
