using DistribuicaoLucros.Domain.Tools;

namespace DistribuicaoLucros.Domain.Messages
{
    public partial class Message
    {
        public class ProfitDistributionMessage
        {
            public static string TotalAvailableShouldBeGreaterThanZero => "O valor a ser distribuído deve ser maior que zero";
            public static string MustHaveLeastOneEmployee => "Deve ter pelo menos um funcionário para calcular a distribuição dos lucros";
            public static string TotalAvailableIsNotEnough(double totalDistributed) => $"O saldo disponibilizado não é suficiente para a operação. É necessário {totalDistributed.ToCurrency()} para distribuir para todos funcionários.";
        }
    }
}
