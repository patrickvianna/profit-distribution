using System;

namespace DistribuicaoLucros.Domain.Entities
{
    public class Employee
    {
        public int Registration { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        public double GrossSalary { get; set; }
        public DateTime AdmissionDate { get; set; }
    }
}
