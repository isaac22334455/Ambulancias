using K4os.Compression.LZ4.Internal;
using Microsoft.AspNetCore.Mvc;
using SUMAAmbulanciasAPI.Areas.Empleados.Models;
using SUMAAmbulanciasAPI.Areas.Empleados.Services;
using SUMAAmbulanciasAPI.Areas.GeneralModels;
using SUMAAmbulanciasAPI.Areas.Membrecias.Models;
using SUMAAmbulanciasAPI.Areas.Membrecias.Services;

namespace SUMAAmbulanciasAPI.Areas.Membrecias.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MembreciaController:Controller
    {
        private readonly MembreciaService _membreciaService;

        // Constructor donde inyectamos el servicio UsuarioService
        public MembreciaController(MembreciaService membreciaService)
        {
            _membreciaService = membreciaService;
        }

        [HttpGet]
        [Route("GetMembrecias")]
        public ActionResult<List<MembreciaModel>> GetMembrecias()
        {
            List<MembreciaModel> membrecias = _membreciaService.GetMembrecias();

            return Ok(membrecias);
        }

        [HttpGet]
        [Route("GetcboMembrecias")]
        public ActionResult<List<MembreciaModel>> GetcboMembrecias()
        {
            List<MembreciaModel> membrecias = _membreciaService.GetcboMembrecias();

            return Ok(membrecias);
        }

        [HttpGet]
        [Route("GetMembreciasById/{id}")]
        public ActionResult<List<MembreciaModel>> GetMembreciasById(int id)
        {
            List<MembreciaModel> membrecias = _membreciaService.GetMembreciasById(id);

            return Ok(membrecias);
        }

        [HttpPost]
        [Route("InsertarMembrecia")]
        public IActionResult InsertarMembrecia([FromBody] MembreciaModel mem)
        {
            if (mem == null)
            {
                return BadRequest(new { message = "Error." });
            }

            ResponseModel result = _membreciaService.InsertarMembrecia(mem);

            return result.IsSuccess ? Ok(new { message = result.Message }) : BadRequest(new { message = result.Message });
        }

        [HttpPut]
        [Route("ActualizarMembrecia")]
        public IActionResult ActualizarMembrecia([FromBody] MembreciaModel mem)
        {
            if (mem == null)
            {
                return BadRequest(new { message = "Error." });
            }

            ResponseModel result = _membreciaService.ActualizarMembrecia(mem);

            return result.IsSuccess ? Ok(new { message = result.Message }) : BadRequest(new { message = result.Message });
        }

        [HttpDelete]
        [Route("EliminarMembrecia/{id}")]
        public IActionResult EliminarMembrecia(int id)
        {
            ResponseModel result = _membreciaService.EliminarMembrecia(id);
            return result.IsSuccess ? Ok(new { message = result.Message }) : BadRequest(new { message = result.Message });
        }
    }
}
