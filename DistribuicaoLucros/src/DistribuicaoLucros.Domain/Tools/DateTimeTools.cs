using DistribuicaoLucros.Domain.Interfaces.Tools;
using System;

namespace DistribuicaoLucros.Domain.Tools
{
    public class DateTimeTools : IDateTimeTools
    {
        public DateTime GetDateTimeNow()
        {
            return DateTime.Now;
        }
    }
}
