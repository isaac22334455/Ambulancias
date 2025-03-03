using Microsoft.AspNetCore.Mvc;
using SUMAAmbulanciasAPI.Areas.Clientes.Models;
using SUMAAmbulanciasAPI.Areas.Clientes.Services;
using SUMAAmbulanciasAPI.Areas.Empleados.Models;
using SUMAAmbulanciasAPI.Areas.Empleados.Services;
using SUMAAmbulanciasAPI.Areas.GeneralModels;

namespace SUMAAmbulanciasAPI.Areas.Clientes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientesController : Controller
    {
        private readonly ClienteService _clienteService;

        // Constructor donde inyectamos el servicio UsuarioService
        public ClientesController(ClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        [Route("GetClientes")]
        public ActionResult<List<ClientesModel>> GetClientes()
        {
            List<ClientesModel> Clientes = _clienteService.GetClientes();

            return Ok(Clientes);
        }

        [HttpGet]
        [Route("GetClienteById/{id}")]
        public ActionResult<List<ClientesModel>> GetClienteById(int id)
        {
            List<ClientesModel> Clientes = _clienteService.GetClienteById(id);

            return Ok(Clientes);
        }

        [HttpPost]
        [Route("InsertarCliente")]
        public IActionResult InsertarCliente([FromBody] ClientesModel cliente)
        {
            if (cliente == null)
            {
                return BadRequest(new { message = "Error." });
            }

            ResponseModel result = _clienteService.InsertarCliente(cliente);

            return result.IsSuccess ? Ok(new { message = result.Message }) : BadRequest(new { message = result.Message });
        }

        [HttpPost]
        [Route("RenovarMembrecia")]
        public IActionResult RenovarMembrecia([FromBody] RenovarMembreciaModel cliente)
        {
            if (cliente == null)
            {
                return BadRequest(new { message = "Error." });
            }

            ResponseModel result = _clienteService.RenovarMembrecia(cliente);

            return result.IsSuccess ? Ok(new { message = result.Message }) : BadRequest(new { message = result.Message });
        }

        [HttpPut]
        [Route("ActualizarCliente")]
        public IActionResult ActualizarCliente([FromBody] ClientesModel cliente)
        {
            if (cliente == null)
            {
                return BadRequest(new { message = "Error." });
            }

            ResponseModel result = _clienteService.ActualizarCliente(cliente);

            return result.IsSuccess ? Ok(new { message = result.Message }) : BadRequest(new { message = result.Message });
        }

        [HttpDelete]
        [Route("EliminarClienteById/{id}")]
        public IActionResult EliminarClienteById(int id)
        {
            ResponseModel result = _clienteService.EliminarClienteById(id);

            return result.IsSuccess ? Ok(new { message = result.Message }) : BadRequest(new { message = result.Message });
        }

        [HttpGet]
        [Route("GetTiposClientes")]
        public ActionResult<List<TipoClientesModel>> GetTiposClientes()
        {
            List<TipoClientesModel> tiposClientes = _clienteService.GetTiposClientes();

            return Ok(tiposClientes);
        }

        [HttpPost]
        [Route("InsertarTipoCliente")]
        public IActionResult InsertarTipoCliente([FromBody] TipoClientesModel tipo)
        {
            if (tipo == null)
            {
                return BadRequest(new { message = "Error." });
            }

            ResponseModel result = _clienteService.InsertarTipoCliente(tipo);

            return result.IsSuccess ? Ok(new { message = result.Message }) : BadRequest(new { message = result.Message });
        }

        [HttpDelete]
        [Route("EliminarTipoClienteById/{id}")]
        public IActionResult EliminarTipoClienteById(int id)
        {

            ResponseModel result = _clienteService.EliminarTipoClienteById(id);

            return result.IsSuccess ? Ok(new { message = result.Message }) : BadRequest(new { message = result.Message });
        }

    }
}
