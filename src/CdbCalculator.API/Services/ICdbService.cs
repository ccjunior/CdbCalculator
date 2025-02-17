using CdbCalculator.API.Dtos;

namespace CdbCalculator.API.Services
{
    public interface ICdbService
    {
        CdbResponseDto InvestCalculate(CdbRequestDto request);
    }
}
