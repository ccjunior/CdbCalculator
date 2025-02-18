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
            var resultado = _cdbService.InvestCalculate(request);
            
            return Ok(resultado);
        }
    }
}
