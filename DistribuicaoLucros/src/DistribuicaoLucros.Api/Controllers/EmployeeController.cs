using System.Collections.Generic;
using System.Threading.Tasks;
using DistribuicaoLucros.Domain.DTO;
using DistribuicaoLucros.Domain.Entities;
using DistribuicaoLucros.Domain.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;

namespace DistribuicaoLucros.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        /// <summary>
        /// Retorna todos os funcionários cadastrados
        /// </summary>
        /// <returns>Lista funcionarios.</returns>
        /// <response code="200">OK</response>
        /// <response code="400">Regra de negócio.</response>
        /// <response code="500">Erro de sistema.</response>
        [HttpGet]
        public async Task<List<EmployeeDto>> GetCollection()
        {
            return await _employeeService.GetAllCollectionDtoAsync();
        }

        /// <summary>
        /// Persiste os funcionários enviados
        /// </summary>
        /// <param name="employees"></param>
        /// <returns>Persistência realizada com sucesso</returns>
        /// <response code="200">OK</response>
        /// <response code="400">Regra de negócio.</response>
        /// <response code="500">Erro de sistema.</response>
        [HttpPost]
        public async Task PersistCollectionAsync([FromBody] List<EmployeeDto> employees)
        {
            await _employeeService.InsertCollectionAsync(employees);
        }

        /// <summary>
        /// Deleta todos os funcionários cadastrados
        /// </summary>
        /// <returns>Coleção deletada com sucesso</returns>
        /// <response code="200">OK</response>
        /// <response code="400">Regra de negócio.</response>
        /// <response code="500">Erro de sistema.</response>
        [HttpDelete]
        public async Task DeleteCollection()
        {
            await _employeeService.DeleteCollection();
        }
    }
}
