using DistribuicaoLucros.Domain.Entities;
using DistribuicaoLucros.Domain.Interfaces.Tools;
using DistribuicaoLucros.Domain.Messages;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DistribuicaoLucro.Tests.Domain.ProftiDistribution
{
    public class ProfitDistributionCalculateTest
    {
        private List<Employee> _employees;
        private int _totalAvailable;
        private Mock<IDateTimeTools> _dateTimeToolsMock;

        public ProfitDistributionCalculateTest()
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

        [Fact]
        public void ShouldBe_EmployeesCount_EqualsPartipationsCount()
        {
            //Arrange
            var employees = _employees;
            var totalAvailable = _totalAvailable;

            var profitDistribution = new ProfitDistribution(employees, totalAvailable, _dateTimeToolsMock.Object);

            //Act
            profitDistribution.Calculate();

            //Assert
            Assert.Equal(employees.Count(), profitDistribution.Participations.Count());
        }

        [Fact]
        public void ShouldBeNotification_WhenTotalAvailable_LessThanTotalDistributed()
        {
            //Arrange
            var nowFake = new DateTime(2021, 02, 28);
            _dateTimeToolsMock.Setup(x => x.GetDateTimeNow()).Returns(nowFake);

            var totalAvailable = 70000;

            var profitDistribution = new ProfitDistribution(_employees, totalAvailable, _dateTimeToolsMock.Object);

            //Act
            profitDistribution.Calculate();

            //Assert
            var expectedMessage = Message.ProfitDistributionMessage.TotalAvailableIsNotEnough(profitDistribution.TotalDistributed);
            var hasExpectedMessage = profitDistribution.Notifications.Any(x => x.Message.Equals(expectedMessage));

            Assert.Single(profitDistribution.Notifications);
            Assert.True(hasExpectedMessage);
        }

        [Fact]
        public void ShouldBe_TotalDistributed_WhenAdimissionDateLessThan_OneYear()
        {
            //Arrange
            var nowFake = new DateTime(2021, 02, 28);
            var yearOfAdmission = 2020;
            _dateTimeToolsMock.Setup(x => x.GetDateTimeNow()).Returns(nowFake);

            var employee = new Employee
            {
                Registration = "0006877",
                Name = "Cross Perkins",
                Department = "Relacionamento com o Cliente",
                Position = "Auxiliar de Ouvidoria",
                GrossSalary = 1800.16,
                AdmissionDate = new DateTime(yearOfAdmission, 03, 31)
            };

            var employees = new List<Employee> { employee };


            var totalAvailable = _totalAvailable;

            var profitDistribution = new ProfitDistribution(employees, totalAvailable, _dateTimeToolsMock.Object);

            //Act
            profitDistribution.Calculate();

            //Assert
            var expectedDistributed = 129611.52;
            Assert.Equal(profitDistribution.TotalDistributed, expectedDistributed);
        }
        
        [Theory]
        [InlineData(2018)]
        [InlineData(2019)]
        public void ShouldBe_TotalDistributed_WhenAdimissionDateBetween_OneAndThreeYears(int yearOfAdmission)
        {
            //Arrange
            var nowFake = new DateTime(2021, 02, 28);
            _dateTimeToolsMock.Setup(x => x.GetDateTimeNow()).Returns(nowFake);

            var employee = new Employee
            {
                Registration = "0006877",
                Name = "Cross Perkins",
                Department = "Relacionamento com o Cliente",
                Position = "Auxiliar de Ouvidoria",
                GrossSalary = 1800.16,
                AdmissionDate = new DateTime(yearOfAdmission, 03, 31)
            };

            var employees = new List<Employee> { employee };


            var totalAvailable = _totalAvailable;

            var profitDistribution = new ProfitDistribution(employees, totalAvailable, _dateTimeToolsMock.Object);

            //Act
            profitDistribution.Calculate();

            //Assert
            var expectedDistributed = 151213.44;
            Assert.Equal(profitDistribution.TotalDistributed, expectedDistributed);
        } 
        
        [Theory]
        [InlineData(2014)]
        [InlineData(2017)]
        public void ShouldBe_TotalDistributed_WhenAdimissionDateBetween_ThreeAndEightYears(int yearOfAdmission)
        {
            //Arrange
            var nowFake = new DateTime(2021, 02, 28);
            _dateTimeToolsMock.Setup(x => x.GetDateTimeNow()).Returns(nowFake);

            var employee = new Employee
            {
                Registration = "0006877",
                Name = "Cross Perkins",
                Department = "Relacionamento com o Cliente",
                Position = "Auxiliar de Ouvidoria",
                GrossSalary = 1800.16,
                AdmissionDate = new DateTime(yearOfAdmission, 03, 31)
            };

            var employees = new List<Employee> { employee };


            var totalAvailable = _totalAvailable;

            var profitDistribution = new ProfitDistribution(employees, totalAvailable, _dateTimeToolsMock.Object);

            //Act
            profitDistribution.Calculate();

            //Assert
            var expectedDistributed = 172815.36;
            Assert.Equal(profitDistribution.TotalDistributed, expectedDistributed);
        }

        [Fact]
        public void ShouldBe_TotalDistributed_WhenAdimissionDateGreaterThan_EightYears()
        {
            //Arrange
            var nowFake = new DateTime(2021, 02, 28);
            var yearOfAdmission = 2012;
            _dateTimeToolsMock.Setup(x => x.GetDateTimeNow()).Returns(nowFake);

            var employee = new Employee
            {
                Registration = "0006877",
                Name = "Cross Perkins",
                Department = "Relacionamento com o Cliente",
                Position = "Auxiliar de Ouvidoria",
                GrossSalary = 1800.16,
                AdmissionDate = new DateTime(yearOfAdmission, 03, 31)
            };

            var employees = new List<Employee> { employee };


            var totalAvailable = _totalAvailable;

            var profitDistribution = new ProfitDistribution(employees, totalAvailable, _dateTimeToolsMock.Object);

            //Act
            profitDistribution.Calculate();

            //Assert
            var expectedDistributed = 216019.2;
            Assert.Equal(profitDistribution.TotalDistributed, expectedDistributed);
        }
              
           
        [Theory]
        [InlineData(3300, 316800)]
        [InlineData(1100, 105600)]
        [InlineData(990, 95040)]
        public void ShouldBe_TotalDistributed_WhenMultipleGrossBySlarayBasicSalaryLessThan_Three(double grossSalary, double expectedDistributed)
        {
            //Arrange
            var nowFake = new DateTime(2021, 02, 28);
            _dateTimeToolsMock.Setup(x => x.GetDateTimeNow()).Returns(nowFake);

            var employee = new Employee
            {
                Registration = "0006877",
                Name = "Cross Perkins",
                Department = "Relacionamento com o Cliente",
                Position = "Auxiliar de Ouvidoria",
                GrossSalary = grossSalary,
                AdmissionDate = new DateTime(2017, 03, 31)
            };

            var employees = new List<Employee> { employee };


            var totalAvailable = _totalAvailable;

            var profitDistribution = new ProfitDistribution(employees, totalAvailable, _dateTimeToolsMock.Object);

            //Act
            profitDistribution.Calculate();

            //Assert
            Assert.Equal(profitDistribution.TotalDistributed, expectedDistributed);
        } 
        
        [Theory]
        [InlineData(5500, 264000)]
        [InlineData(3301, 158448)]
        public void ShouldBe_TotalDistributed_WhenMultipleGrossBySlarayBasicSalaryBetween_ThreeAndFive(double grossSalary, double expectedDistributed)
        {
            //Arrange
            var nowFake = new DateTime(2021, 02, 28);
            _dateTimeToolsMock.Setup(x => x.GetDateTimeNow()).Returns(nowFake);

            var employee = new Employee
            {
                Registration = "0006877",
                Name = "Cross Perkins",
                Department = "Relacionamento com o Cliente",
                Position = "Auxiliar de Ouvidoria",
                GrossSalary = grossSalary,
                AdmissionDate = new DateTime(2017, 03, 31)
            };

            var employees = new List<Employee> { employee };


            var totalAvailable = _totalAvailable;

            var profitDistribution = new ProfitDistribution(employees, totalAvailable, _dateTimeToolsMock.Object);

            //Act
            profitDistribution.Calculate();

            //Assert
            Assert.Equal(profitDistribution.TotalDistributed, expectedDistributed);
        } 

        [Theory]
        [InlineData(8800, 281600)]
        [InlineData(5501, 176032)]
        public void ShouldBe_TotalDistributed_WhenMultipleGrossBySlarayBasicSalaryBetween_FiveAndEight(double grossSalary, double expectedDistributed)
        {
            //Arrange
            var nowFake = new DateTime(2021, 02, 28);
            _dateTimeToolsMock.Setup(x => x.GetDateTimeNow()).Returns(nowFake);

            var employee = new Employee
            {
                Registration = "0006877",
                Name = "Cross Perkins",
                Department = "Relacionamento com o Cliente",
                Position = "Auxiliar de Ouvidoria",
                GrossSalary = grossSalary,
                AdmissionDate = new DateTime(2017, 03, 31)
            };

            var employees = new List<Employee> { employee };


            var totalAvailable = _totalAvailable;

            var profitDistribution = new ProfitDistribution(employees, totalAvailable, _dateTimeToolsMock.Object);

            //Act
            profitDistribution.Calculate();

            //Assert
            Assert.Equal(profitDistribution.TotalDistributed, expectedDistributed);
        } 

        [Theory]
        [InlineData(9000, 172800)]
        [InlineData(8801, 168979.2)]
        public void ShouldBe_TotalDistributed_WhenMultipleGrossBySlarayBasicSalaryGreaterThen_Eight(double grossSalary, double expectedDistributed)
        {
            //Arrange
            var nowFake = new DateTime(2021, 02, 28);
            _dateTimeToolsMock.Setup(x => x.GetDateTimeNow()).Returns(nowFake);

            var employee = new Employee
            {
                Registration = "0006877",
                Name = "Cross Perkins",
                Department = "Relacionamento com o Cliente",
                Position = "Auxiliar de Ouvidoria",
                GrossSalary = grossSalary,
                AdmissionDate = new DateTime(2017, 03, 31)
            };

            var employees = new List<Employee> { employee };


            var totalAvailable = _totalAvailable;

            var profitDistribution = new ProfitDistribution(employees, totalAvailable, _dateTimeToolsMock.Object);

            //Act
            profitDistribution.Calculate();

            //Assert
            Assert.Equal(profitDistribution.TotalDistributed, expectedDistributed);
        }


        [Fact]
        public void ShouldBe_TotalDistributed_WhenDepartmentEqual_Diretoria()
        {
            //Arrange
            var nowFake = new DateTime(2021, 02, 28);
            _dateTimeToolsMock.Setup(x => x.GetDateTimeNow()).Returns(nowFake);

            var department = "Diretoria";
            var employee = new Employee
            {
                Registration = "0006877",
                Name = "Cross Perkins",
                Department = department,
                Position = "Auxiliar de Ouvidoria",
                GrossSalary = 1800.16,
                AdmissionDate = new DateTime(2017, 03, 31)
            };

            var employees = new List<Employee> { employee };


            var totalAvailable = _totalAvailable;

            var profitDistribution = new ProfitDistribution(employees, totalAvailable, _dateTimeToolsMock.Object);

            //Act
            profitDistribution.Calculate();

            //Assert
            var expectedDistributed = 86407.68;
            Assert.Equal(profitDistribution.TotalDistributed, expectedDistributed);
        }
        
        [Theory]
        [InlineData("Contabilidade", 108009.6)]
        [InlineData("Financeiro", 108009.6)]
        [InlineData("Tecnologia", 108009.6)]
        public void ShouldBe_TotalDistributed_WhenDepartmentEqual_ContabilidadeFinanceiroTecnologia(string department, double expectedDistributed)
        {
            //Arrange
            var nowFake = new DateTime(2021, 02, 28);
            _dateTimeToolsMock.Setup(x => x.GetDateTimeNow()).Returns(nowFake);

            var employee = new Employee
            {
                Registration = "0006877",
                Name = "Cross Perkins",
                Department = department,
                Position = "Auxiliar de Ouvidoria",
                GrossSalary = 1800.16,
                AdmissionDate = new DateTime(2017, 03, 31)
            };

            var employees = new List<Employee> { employee };


            var totalAvailable = _totalAvailable;

            var profitDistribution = new ProfitDistribution(employees, totalAvailable, _dateTimeToolsMock.Object);

            //Act
            profitDistribution.Calculate();

            //Assert
            Assert.Equal(profitDistribution.TotalDistributed, expectedDistributed);
        }
        
        [Fact]
        public void ShouldBe_TotalDistributed_WhenDepartmentEqual_ServicosGerais()
        {
            //Arrange
            var nowFake = new DateTime(2021, 02, 28);
            _dateTimeToolsMock.Setup(x => x.GetDateTimeNow()).Returns(nowFake);

            var department = "Serviços Gerais";
            var employee = new Employee
            {
                Registration = "0006877",
                Name = "Cross Perkins",
                Department = department,
                Position = "Auxiliar de Ouvidoria",
                GrossSalary = 1800.16,
                AdmissionDate = new DateTime(2017, 03, 31)
            };

            var employees = new List<Employee> { employee };


            var totalAvailable = _totalAvailable;

            var profitDistribution = new ProfitDistribution(employees, totalAvailable, _dateTimeToolsMock.Object);

            //Act
            profitDistribution.Calculate();

            //Assert
            var expectedDistributed = 129611.52;
            Assert.Equal(profitDistribution.TotalDistributed, expectedDistributed);
        }

        [Fact]
        public void ShouldBe_TotalDistributed_WhenDepartmentEqual_RelacionamentoCliente()
        {
            //Arrange
            var nowFake = new DateTime(2021, 02, 28);
            _dateTimeToolsMock.Setup(x => x.GetDateTimeNow()).Returns(nowFake);

            var department = "Relacionamento com o Cliente";
            var employee = new Employee
            {
                Registration = "0006877",
                Name = "Cross Perkins",
                Department = department,
                Position = "Auxiliar de Ouvidoria",
                GrossSalary = 1800.16,
                AdmissionDate = new DateTime(2017, 03, 31)
            };

            var employees = new List<Employee> { employee };


            var totalAvailable = _totalAvailable;

            var profitDistribution = new ProfitDistribution(employees, totalAvailable, _dateTimeToolsMock.Object);

            //Act
            profitDistribution.Calculate();

            //Assert
            var expectedDistributed = 172815.36;
            Assert.Equal(profitDistribution.TotalDistributed, expectedDistributed);
        }

        [Fact]
        public void ShouldBeThrowArgumentException_Department_WhenDoesNotExist()
        {
            //Arrange
            var nowFake = new DateTime(2021, 02, 28);
            _dateTimeToolsMock.Setup(x => x.GetDateTimeNow()).Returns(nowFake);

            var department = "Relacionamento com o Cliente";
            var employee = new Employee
            {
                Registration = "0006877",
                Name = "Cross Perkins",
                Department = department,
                Position = "Auxiliar de Ouvidoria",
                GrossSalary = 1800.16,
                AdmissionDate = new DateTime(2017, 03, 31)
            };

            var employees = new List<Employee> { employee };


            var totalAvailable = _totalAvailable;

            var profitDistribution = new ProfitDistribution(employees, totalAvailable, _dateTimeToolsMock.Object);

            //Act
            profitDistribution.Calculate();

            //Assert
            var expectedDistributed = 172815.36;
            Assert.Equal(profitDistribution.TotalDistributed, expectedDistributed);
        }
    }
}
