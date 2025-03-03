using Microsoft.AspNetCore.Mvc;
using SUMAAmbulanciasAPI.Areas.Empleados.Models;
using SUMAAmbulanciasAPI.Areas.Empleados.Services;
using SUMAAmbulanciasAPI.Areas.Ganancias.Models;
using SUMAAmbulanciasAPI.Areas.Ganancias.Services;
using SUMAAmbulanciasAPI.Areas.GeneralModels;

namespace SUMAAmbulanciasAPI.Areas.Ganancias.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class gananciasController : Controller
    {
        private readonly gananciasService _gananciasService;

        // Constructor donde inyectamos el servicio UsuarioService
        public gananciasController(gananciasService GananciasService)
        {
            _gananciasService =  GananciasService;
        }

        [HttpGet]
        [Route("GetGanancias")]
        public ActionResult<List<gananciasModel>> GetGanancias()
        {
            List<gananciasModel> ganancias = _gananciasService.GetGanancias();

            return Ok(ganancias);
        }

        [HttpGet]
        [Route("GetGastos")]
        public ActionResult<List<gastosModel>> GetGastos()
        {
            List<gastosModel> gastos = _gananciasService.GetGastos();

            return Ok(gastos);
        }

        [HttpDelete]
        [Route("EliminarSalida/{id}")]
        public IActionResult EliminarSalida(int id)
        {

            ResponseModel result = _gananciasService.EliminarSalida(id);

            return result.IsSuccess ? Ok(new { message = result.Message }) : BadRequest(new { message = result.Message });
        }

        [HttpDelete]
        [Route("EliminarGanancia/{id}")]
        public IActionResult EliminarGanancia(int id)
        {

            ResponseModel result = _gananciasService.EliminarGanancia(id);

            return result.IsSuccess ? Ok(new { message = result.Message }) : BadRequest(new { message = result.Message });
        }
    }
}
