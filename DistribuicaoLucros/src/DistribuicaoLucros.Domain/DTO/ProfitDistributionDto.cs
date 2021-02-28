using System.Collections.Generic;

namespace DistribuicaoLucros.Domain.DTO
{
    public class ProfitDistributionDto
    {
        public List<EmployeeProfitSharingDto> Participacoes { get; set; }
        public string Total_De_Funcionarios { get; set; }
        public string Total_Distribuido { get; set; }
        public string Total_Disponibilizado { get; set; }
        public string Saldo_Total_Disponibilizado { get; set; }
    }
}
