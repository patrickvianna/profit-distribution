using DistribuicaoLucros.Domain.Entities;
using DistribuicaoLucros.Domain.Interfaces.Repository;
using Firebase.Database;
using Firebase.Database.Query;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DistribuicaoLucros.Infraestructure.Data.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private FirebaseClient _firebaseClient;
        private string employeeCollectionKey = "employee";

        public EmployeeRepository(FirebaseClient firebaseClient)
        {
            _firebaseClient = firebaseClient;
        }
        public async Task<List<Employee>> GetAllCollectionAsync()
        {
            var firebaseEmployees = await _firebaseClient.Child(employeeCollectionKey).OnceAsync<Employee>();

            return firebaseEmployees.Select(x => x.Object).ToList();
        }

        public async Task InsertCollectionAsync(List<Employee> employess)
        {
            foreach (var employee in employess)
            {
                await _firebaseClient.Child(employeeCollectionKey).PostAsync(employee);
            }
        }
    }
}
