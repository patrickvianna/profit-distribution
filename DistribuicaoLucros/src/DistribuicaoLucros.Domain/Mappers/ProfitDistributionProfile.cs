using AutoMapper;
using DistribuicaoLucros.Domain.DTO;
using DistribuicaoLucros.Domain.Entities;
using DistribuicaoLucros.Domain.Tools;

namespace DistribuicaoLucros.Domain.Mappers
{
    public class ProfitDistributionProfile : Profile
    {
        public ProfitDistributionProfile()
        {
            CreateMap<ProfitDistribution, ProfitDistributionDto>()
                .ForMember(dest => dest.Participacoes, opt => opt.MapFrom(src => src.Participations))
                .ForMember(dest => dest.Total_De_Funcionarios, opt => opt.MapFrom(src => src.TotalEmployees.ToString()))
                .ForMember(dest => dest.Total_Distribuido, opt => opt.MapFrom(src => src.TotalDistributed.ToCurrency()))
                .ForMember(dest => dest.Total_Disponibilizado, opt => opt.MapFrom(src => src.TotalAvailable.ToCurrency()))
                .ForMember(dest => dest.Saldo_Total_Disponibilizado, opt => opt.MapFrom(src => src.TotalBalanceAvailable.ToCurrency()));
        }
    }
}
