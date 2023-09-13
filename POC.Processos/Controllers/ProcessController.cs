using Microsoft.AspNetCore.Mvc;
using POC.Processos.Repository;

namespace POC.Processos.Controllers;

[ApiController]
[Route("[controller]")]
public class ProcessController : ControllerBase
{
    private readonly IProcessRepository _repository;

    public ProcessController(IProcessRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> Get(string number)
    {
        return Ok(await _repository.GetBy(number));
    }
}