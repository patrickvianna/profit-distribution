namespace DistribuicaoLucros.Domain.Tools
{
    public static class CurrencyTools
    {
        public static string ToCurrency(this double value)
        {
            return value.ToString("C");
        }
    }
}
