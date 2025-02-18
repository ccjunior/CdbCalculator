using CdbCalculator.API.Dtos;

namespace CdbCalculator.API.Services.Impl
{
    public class CdbService : ICdbService
    {
        private const decimal CDI = 0.009m;
        private const decimal TB = 1.08m;
        private static readonly IReadOnlyDictionary<Range, decimal> TaxRanges = new Dictionary<Range, decimal>
        {
            { new Range(0, 6), 0.225m },
            { new Range(7, 12), 0.20m },
            { new Range(13, 24), 0.175m }
        };

        private const decimal DEFAULT_TAX_RATE = 0.15m;


        public CdbResponseDto InvestCalculate(CdbRequestDto request)
        {
            ValidateRequest(request);

            var yieldRate = 1 + (CDI * TB);
            var endValue = CalculateEndValue(request.InitialValue, request.DeadlineMonths, yieldRate);
            var yield = endValue - request.InitialValue;
            var tax = CalculateTax(request.DeadlineMonths, yield);
            var netValue = endValue - tax;

            return new CdbResponseDto
            {
                GrossValue = Math.Round(endValue, 2),
                NetValue = Math.Round(netValue, 2)
            };
        }

        private static void ValidateRequest(CdbRequestDto request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (request.InitialValue <= 0)
                throw new ArgumentException("Valor inicial deve ser maior que zero.", nameof(request.InitialValue));

            if (request.DeadlineMonths <= 0)
                throw new ArgumentException("Prazo deve ser maior que zero.", nameof(request.DeadlineMonths));
        }

        private static decimal CalculateEndValue(decimal initialValue, int months, decimal yieldRate)
        {
            return initialValue * (decimal)Math.Pow((double)yieldRate, months);
        }

        private static decimal CalculateTax(int months, decimal yield)
        {
            var taxRate = TaxRanges.FirstOrDefault(x => months <= x.Key.End).Value;
            return yield * (taxRate == 0 ? DEFAULT_TAX_RATE : taxRate);
        }
    }
    public readonly struct Range
    {
        public Range(int start, int end)
        {
            Start = start;
            End = end;
        }

        public int Start { get; }
        public int End { get; }
    }
}
