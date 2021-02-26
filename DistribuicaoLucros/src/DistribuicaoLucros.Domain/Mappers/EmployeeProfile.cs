using AutoMapper;
using DistribuicaoLucros.Domain.DTO;
using DistribuicaoLucros.Domain.Entities;
using System;
using System.Globalization;

namespace DistribuicaoLucros.Domain.Mappers
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeDto, Employee>()
                .ForMember(dest => dest.Registration, opt => opt.MapFrom(src => Convert.ToInt32(src.Matricula)))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Nome))
                .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Area))
                .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Cargo))
                .ForMember(dest => dest.GrossSalary, opt => opt.MapFrom(src => double.Parse(src.Salario_Bruto, NumberStyles.Currency)))
                .ForMember(dest => dest.AdmissionDate, opt => opt.MapFrom(src => Convert.ToDateTime(src.Data_De_Admissao)));
        }
    }
}
