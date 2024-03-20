//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;

//namespace TuProyecto.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class AlertasController : ControllerBase
//    {
//        private readonly ILogger<AlertasController> _logger;

//        public AlertasController(ILogger<AlertasController> logger)
//        {
//            _logger = logger;
//        }

//        [HttpPost]
//        public IActionResult EnviarAlerta(string mensaje)
//        {
//            _logger.LogInformation("La alerta de eliminación de mascota se envió correctamente.");
//            return Ok($"<div style='color: green;'>{mensaje}</div>");
//        }
//    }
//}
