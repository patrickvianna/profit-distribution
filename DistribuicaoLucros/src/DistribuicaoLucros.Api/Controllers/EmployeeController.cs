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
        /// <remarks>
        /// <param name="codigoAgenda"></param>
        /// <returns>Agenda atualizada.</returns>
        /// <response code="200">Confirmação realizado com sucesso.</response>
        /// <response code="500">Erro de sistema.</response>
        [HttpGet]
        public async Task<List<Employee>> GetCollection()
        {
            return await _employeeService.GetAllCollectionAsync();
        }

        /// <summary>
        /// Persiste os funcionários enviados
        /// </summary>
        /// <param name="employees"></param>
        /// <returns>Agenda atualizada.</returns>
        /// <response code="200">Persistência realizada com sucesso.</response>
        /// <response code="500">Erro de sistema.</response>
        [HttpPost]
        public async Task PersistCollectionAsync([FromBody] List<EmployeeDto> employees)
        {
            await _employeeService.InsertCollectionAsync(employees);
        }
    }
}
