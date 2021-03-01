using DistribuicaoLucros.Domain.Entities;
using DistribuicaoLucros.Domain.Interfaces.Tools;
using DistribuicaoLucros.Domain.Messages;
using DistribuicaoLucros.Domain.Tools;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace DistribuicaoLucro.Tests.Domain.ProftiDistribution
{
    public class ProfitDistributionCtorTest
    {
        private List<Employee> _employees;
        private int _totalAvailable;
        private Mock<IDateTimeTools> _dateTimeToolsMock;

        public ProfitDistributionCtorTest()
        {
            var employee1 = new Employee
            {
                Registration = "0006877",
                Name = "Cross Perkins",
                Department = "Relacionamento com o Cliente",
                Position = "Auxiliar de Ouvidoria",
                GrossSalary = 1800.16,
                AdmissionDate = new DateTime(2017, 03, 31)
            };
            var employee2 = new Employee
            {
                Registration = "0008601",
                Name = "Taylor Mccarthy",
                Department = "Relacionamento com o Cliente",
                Position = "Líder de Ouvidoria",
                GrossSalary = 3371.47,
                AdmissionDate = new DateTime(2016, 12, 06)
            };

            _employees = new List<Employee> { employee1, employee2 };
            _totalAvailable = 1000000;

            _dateTimeToolsMock = new Mock<IDateTimeTools>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-2)]
        public void ShouldBeThrowArgumentioException_WhenTotalDistributed_IsNotGreaterThenZero(double totalAvailable)
        {
            //Arrange
            var employees = _employees;

            //Act
            var profit = new ProfitDistribution(employees, totalAvailable, _dateTimeToolsMock.Object);

            //Assert
            var expectedMessage = Message.ProfitDistributionMessage.TotalAvailableShouldBeGreaterThanZero;
            var hasExpectedMessage = profit.Notifications.Any(x => x.Message.Equals(expectedMessage));

            Assert.Single(profit.Notifications);
            Assert.True(hasExpectedMessage);
        }

        [Fact]
        public void ShouldBeThrowArgumentioException_WhenEmployees_CountIsEqualZero()
        {
            //Arrange
            var employees = new List<Employee>();
            var totalAvailable = _totalAvailable;

            //Act
            var profit = new ProfitDistribution(employees, totalAvailable, _dateTimeToolsMock.Object);

            //Assert
            var expectedMessage = Message.ProfitDistributionMessage.MustHaveLeastOneEmployee;
            var hasExpectedMessage = profit.Notifications.Any(x => x.Message.Equals(expectedMessage));

            Assert.Single(profit.Notifications);
            Assert.True(hasExpectedMessage);
        }

        [Fact]
        public void ShouldBeThrowArgumentioException_WhenEmployees_CountIsNull()
        {
            // Arrange
            List<Employee> employees = null;
            var totalAvailable = _totalAvailable;

            //Act
            var profit = new ProfitDistribution(employees, totalAvailable, _dateTimeToolsMock.Object);

            //Assert
            var expectedMessage = Message.ProfitDistributionMessage.MustHaveLeastOneEmployee;
            var hasExpectedMessage = profit.Notifications.Any(x => x.Message.Equals(expectedMessage));

            Assert.Single(profit.Notifications);
            Assert.True(hasExpectedMessage);

        }
    }
}
