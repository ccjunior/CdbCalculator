using CdbCalculator.API.Dtos;

namespace CdbCalculator.API.Services.Impl
{
    public class CdbService : ICdbService
    {
        private const decimal CDI = 0.009m;  
        private const decimal TB = 1.08m;    

        public CdbResponseDto InvestCalculate(CdbRequestDto request)
        {
            var endValue = request.InitialValue;

            for (int i = 0; i < request.DeadlineMonths; i++)
            {
                endValue *= (1 + (CDI * TB));
            }

            decimal tax = TaxCalculate(request.DeadlineMonths, endValue - request.InitialValue);
            decimal netValue = endValue - tax;

            return new CdbResponseDto
            {
                GrossValue = Math.Round(endValue, 2),
                NetValue = Math.Round(netValue, 2)
            };
        }

        private decimal TaxCalculate(int deadline, decimal yield)
        {
            decimal aliquot = deadline switch
            {
                <= 6 => 0.225m,   
                <= 12 => 0.20m,   
                <= 24 => 0.175m,  
                _ => 0.15m        
            };

            return yield * aliquot;
        }
    }
}
