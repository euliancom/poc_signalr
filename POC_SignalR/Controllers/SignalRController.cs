using Microsoft.AspNetCore.Mvc;
using POC_SignalR.Handlers;
using System.ComponentModel.DataAnnotations;

namespace POC_SignalR.Controllers;

[ApiController]
[Route("/")]
public class SignalRController : ControllerBase
{
    private readonly ILogger<SignalRController> _logger;
    private readonly SignalRHandler _signalRHandler;

    public SignalRController(ILogger<SignalRController> logger, SignalRHandler signalRHandler)
    {
        _logger = logger;
        _signalRHandler = signalRHandler ?? throw new ArgumentNullException(nameof(signalRHandler));
    }

    [HttpPost("message")]
    public async Task<ActionResult> Post([Required] string message)
    {
        bool result = await _signalRHandler.Handle(message);
        _logger.LogInformation(message);
        return (result) ? Ok() : NotFound();
    }
}
