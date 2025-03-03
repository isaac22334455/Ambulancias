using Microsoft.AspNetCore.Mvc;
using SUMAAmbulanciasAPI.Areas.Empleados.Models;
using SUMAAmbulanciasAPI.Areas.Empleados.Services;
using SUMAAmbulanciasAPI.Areas.GeneralModels;
using SUMAAmbulanciasAPI.Areas.Usuarios.Models;
using SUMAAmbulanciasAPI.Areas.Usuarios.Services;

namespace SUMAAmbulanciasAPI.Areas.Empleados.Controllers
{

    [ApiController]
    [Route("[controller]")] 
    public class EmpleadosController: Controller{

        private readonly EmpleadosService _empleadoService;

        // Constructor donde inyectamos el servicio UsuarioService
        public EmpleadosController(EmpleadosService empleadoService)
        {
            _empleadoService = empleadoService;
        }

        [HttpGet]
        [Route("GetEmpleados")]
        public ActionResult<List<EmpleadosModel>> GetEmpleados()
        {
            List<EmpleadosModel> empleados = _empleadoService.GetEmpleados();

            return Ok(empleados);
        }

        [HttpGet]
        [Route("GetEmpleadoById/{id}")]
        public ActionResult<List<EmpleadosModel>> GetEmpleadoById(int id)
        {
            List<EmpleadosModel> empleados = _empleadoService.GetEmpleadoById(id);

            return Ok(empleados);
        }

        [HttpGet]
        [Route("GetHorasByIdEmpleado/{id}")]
        public ActionResult<List<EmpleadoHorasModel>> GetHorasByIdEmpleado(int id)
        {
            List<EmpleadoHorasModel> horas = _empleadoService.GetHorasByIdEmpleado(id);

            return Ok(horas);
        }

        [HttpGet]
        [Route("GetHorasPagadasByIdEmpleado/{id}")]
        public ActionResult<List<EmpleadoHorasPagadasModel>> GetHorasPagadasByIdEmpleado(int id)
        {
            List<EmpleadoHorasPagadasModel> horas = _empleadoService.GetHorasPagadasByIdEmpleado(id);

            return Ok(horas);
        }

        [HttpPost]
        [Route("PagarHoras")]
        public IActionResult PagarHoras([FromBody] EmpleadoPagarModel pagar)
        {
            if (pagar == null)
            {
                return BadRequest(new { message = "Error." });
            }

            ResponseModel result = _empleadoService.PagarHoras(pagar);

            return result.IsSuccess ? Ok(new { message = result.Message }) : BadRequest(new { message = result.Message });
        }

        [HttpPost]
        [Route("AsignarHoras")]
        public IActionResult AsignarHoras([FromBody] EmpleadoHorasModel horas)
        {
            if (horas == null)
            {
                return BadRequest(new { message = "Error." });
            }

            ResponseModel result = _empleadoService.AsignarHoras(horas);

            return result.IsSuccess ? Ok(new { message = result.Message }) : BadRequest(new { message = result.Message });
        }

        [HttpPut]
        [Route("ActualizarAsignacionHoras")]
        public IActionResult ActualizarAsignacionHoras([FromBody] EmpleadoHorasModel horas)
        {
            if (horas == null)
            {
                return BadRequest(new { message = "Error." });
            }

            ResponseModel result = _empleadoService.ActualizarAsignacionHoras(horas);

            return result.IsSuccess ? Ok(new { message = result.Message }) : BadRequest(new { message = result.Message });
        }

        [HttpDelete]
        [Route("EliminarAsignacionHorasByIdHoras/{id}")]
        public IActionResult EliminarAsignacionHorasByIdHoras(int id)
        {

            ResponseModel result = _empleadoService.EliminarAsignacionHorasByIdHoras(id);

            return result.IsSuccess ? Ok(new { message = result.Message }) : BadRequest(new { message = result.Message });
        }

        [HttpDelete]
        [Route("EliminarHorasPagadasByIdHorasP/{id}")]
        public IActionResult EliminarHorasPagadasByIdHorasP(int id)
        {

            ResponseModel result = _empleadoService.EliminarHorasPagadasByIdHorasP(id);

            return result.IsSuccess ? Ok(new { message = result.Message }) : BadRequest(new { message = result.Message });
        }

        [HttpPost]
        [Route("InsertarEmpleado")]
        public IActionResult InsertarEmpleado([FromBody] EmpleadosModel nuevoEmpleado)
        {
            if (nuevoEmpleado == null)
            {
                return BadRequest(new { message = "Error." });
            }

            ResponseModel result =_empleadoService.InsertarEmpleado(nuevoEmpleado);

            return result.IsSuccess ? Ok(new { message = result.Message }) : BadRequest(new { message = result.Message });
        }

        [HttpPut]
        [Route("ActualizarEmpleado")]
        public IActionResult ActualizarEmpleado([FromBody] EmpleadosModel ActualizarEmpleado)
        {
            if (ActualizarEmpleado == null)
            {
                return BadRequest(new { message = "Error." });
            }

            ResponseModel result = _empleadoService.ActualizarEmpleado(ActualizarEmpleado);

            return result.IsSuccess ? Ok(new { message = result.Message }) : BadRequest(new { message = result.Message });
        }

        [HttpDelete]
        [Route("EliminarEmpleado/{id}")]
        public IActionResult EliminarEmpleado(int id)
        {
          
            ResponseModel result = _empleadoService.EliminarEmpleado(id);

            return result.IsSuccess ? Ok(new { message = result.Message }) : BadRequest(new { message = result.Message });
        }

    }
}
