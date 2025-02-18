using CdbCalculator.API.Dtos;
using CdbCalculator.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CdbCalculator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CdbController : ControllerBase
    {
        [HttpPost("calculate")]
        public IActionResult Calcular([FromServices] ICdbService _cdbService,  [FromBody] CdbRequestDto request)
        {
            if (request.InitialValue <= 0 || request.DeadlineMonths < 2)
            {
                return BadRequest("O valor inicial deve ser positivo e o prazo deve ser maior que 1 mês.");
            }

            var resultado = _cdbService.InvestCalculate(request);
            
            return Ok(resultado);
        }
    }
}
