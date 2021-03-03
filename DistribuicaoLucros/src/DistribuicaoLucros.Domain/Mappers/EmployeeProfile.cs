using AutoMapper;
using DistribuicaoLucros.Domain.DTO;
using DistribuicaoLucros.Domain.Entities;
using DistribuicaoLucros.Domain.Tools;
using System;
using System.Globalization;

namespace DistribuicaoLucros.Domain.Mappers
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeDto, Employee>()
                .ForMember(dest => dest.Registration, opt => opt.MapFrom(src => src.Matricula))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Nome))
                .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Area))
                .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Cargo))
                .ForMember(dest => dest.GrossSalary, opt => opt.MapFrom(src => double.Parse(src.Salario_Bruto, NumberStyles.Currency)))
                .ForMember(dest => dest.AdmissionDate, opt => opt.MapFrom(src => Convert.ToDateTime(src.Data_De_Admissao)));
            
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.Matricula, opt => opt.MapFrom(src => src.Registration))
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Area, opt => opt.MapFrom(src => src.Department))
                .ForMember(dest => dest.Cargo, opt => opt.MapFrom(src => src.Position))
                .ForMember(dest => dest.Salario_Bruto, opt => opt.MapFrom(src => src.GrossSalary.ToCurrency()))
                .ForMember(dest => dest.Data_De_Admissao, opt => opt.MapFrom(src => src.AdmissionDate.ToString("yyyy-MM-dd")));
        }
    }
}
