using DistribuicaoLucros.Domain.DTO;
using DistribuicaoLucros.Domain.Interfaces.Tools;
using DistribuicaoLucros.Domain.Messages;
using DistribuicaoLucros.Domain.Tools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DistribuicaoLucros.Domain.Entities
{
    public class ProfitDistribution : Entity
    {
        public List<EmployeeProfitSharingDto> Participations { get; set; }
        public int TotalEmployees { get; set; }
        public double TotalDistributed { get; set; }
        public double TotalAvailable { get; set; }
        public double TotalBalanceAvailable { get; set; }

        private IDateTimeTools _dateTimeTools;

        private List<Employee> Employees { get; set; }
        private double BasicSalary { get; set; } = 1100;

        public ProfitDistribution(List<Employee> employees, double totalAvailable, IDateTimeTools dateTimeTools)
        {
            if (totalAvailable <= 0)
                AddNotification("total_disponibilizado", Message.ProfitDistributionMessage.TotalAvailableShouldBeGreaterThanZero);

            if (employees == null || !employees.Any())
                AddNotification("quantidade_funcionarios", Message.ProfitDistributionMessage.MustHaveLeastOneEmployee);

            _dateTimeTools = dateTimeTools;
            Employees = employees;
            TotalAvailable = totalAvailable;
            Participations = new List<EmployeeProfitSharingDto>();
        }

        public void Calculate()
        {
            foreach (var employee in Employees)
            {
                var participationValue = GetProfitSharing(employee);
                var employeeParticipation = new EmployeeProfitSharingDto
                {
                    Matricula = employee.Registration.ToString(),
                    Nome = employee.Name,
                    Valor_Da_Participacao = participationValue.ToCurrency()
                };

                Participations.Add(employeeParticipation);

                TotalEmployees++;
                TotalDistributed += participationValue;
            }

            TotalBalanceAvailable = TotalAvailable - TotalDistributed;

            IsTotalAvailableSufficient();
        }

        #region Private
        private double GetProfitSharing(Employee employee)
        {
            var ratingAdmissionTime = GetRatingByAdmissionTime(employee.AdmissionDate);
            var ratingDepartment = GetRatingByDepartment(employee.Department);
            var ratingGrossSalary = GetRatingByGlossSalary(employee.GrossSalary, employee.Position);
            var mouthsOfYear = 12;

            var calc = (((employee.GrossSalary * ratingAdmissionTime) + (employee.GrossSalary * ratingDepartment)) / ratingGrossSalary) * mouthsOfYear;
            return Math.Round(calc, 2);
        }

        private int GetRatingByAdmissionTime(DateTime admissionDate)
        {
            var rangeOfDays = _dateTimeTools.GetDateTimeNow()
                                            .Subtract(admissionDate).Days;
            var years = Convert.ToInt32(rangeOfDays / 365);

            if (years < 1) return 1;
            if (years < 3) return 2;
            if (years < 8) return 3;
            return 5;
        }

        private int GetRatingByDepartment(string department)
        {
            switch (department)
            {
                case "Diretoria":
                    return 1;
                case "Contabilidade":
                case "Financeiro":
                case "Tecnologia":
                    return 2;
                case "Serviços Gerais":
                    return 3;
                case "Relacionamento com o Cliente":
                    return 5;
            }

            throw new ArgumentException("Não existe esse departamento");
        }

        private int GetRatingByGlossSalary(double grossSalary, string position)
        {
            var grossSalaryByBasicSalary = grossSalary / BasicSalary;

            if (position == "Estagiário" || grossSalaryByBasicSalary <= 3) return 1;
            if (grossSalaryByBasicSalary <= 5) return 2;
            if (grossSalaryByBasicSalary <= 8) return 3;
            return 5;
        }

        private void IsTotalAvailableSufficient()
        {
            if (TotalBalanceAvailable < 0)
                AddNotification("total_disponibilizado", Message.ProfitDistributionMessage.TotalAvailableIsNotEnough(TotalDistributed));
        }
        #endregion
    }
}
