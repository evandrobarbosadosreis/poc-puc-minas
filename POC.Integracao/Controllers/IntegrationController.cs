using Microsoft.AspNetCore.Mvc;
using POC.Integracao.Infrastructure;
using POC.Integracao.Services;

namespace POC.Integracao.Controllers;

[Route("api/integration")]
[ApiController]
public class IntegrationController : ControllerBase
{
    private readonly IIntegrationRepository _repository;

    public IntegrationController(IIntegrationRepository repository)
    {
        _repository = repository;
    }
    
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] IntegrationRequest request, [FromServices] ISQSService amazonSQS)
    {
        if (string.IsNullOrWhiteSpace(request.Number))
        {
            return BadRequest(ModelState);
        }

        await _repository.Create(request.Number);

        await amazonSQS.RequestNewIntegration(request.Number);
        
        return Ok("Solicitação de importação recebida com sucesso");
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] string number = "")
    {
        return Ok(await _repository.GetBy(number));
    }
}