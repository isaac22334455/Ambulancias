using Microsoft.AspNetCore.Mvc;
using SUMAAmbulanciasAPI.Areas.Empleados.Models;
using SUMAAmbulanciasAPI.Areas.Empleados.Services;
using SUMAAmbulanciasAPI.Areas.GeneralModels;
using SUMAAmbulanciasAPI.Areas.Usuarios.Models;
using SUMAAmbulanciasAPI.Areas.Usuarios.Services;

namespace SUMAAmbulanciasAPI.Areas.Usuarios.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuariosController : Controller
    {
        private readonly UsuarioService _usuarioService;

        // Constructor donde inyectamos el servicio UsuarioService
        public UsuariosController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        [Route("GetUsuarios")]
        public ActionResult<List<UsuarioModel>> GetUsuarios()
        {
            List<UsuarioModel> usuarios = _usuarioService.GetUsuarios();
            return Ok(usuarios);

        }

        [HttpGet]
        [Route("GetUsuarioById/{id}")]
        public ActionResult<List<UsuarioModel>> GetUsuarioById(int id)
        {
            List<UsuarioModel> usuarios = _usuarioService.GetUsuarioById(id);
            return Ok(usuarios);

        }

        [HttpGet]
        [Route("GetUsuarioByName/{nombre}")]
        public ActionResult<List<UsuarioModel>> GetUsuarioByName(string nombre)
        {
            List<UsuarioModel> usuarios = _usuarioService.GetUsuarioByName(nombre);
            return Ok(usuarios);

        }

        [HttpPost]
        [Route("InsertUsuario")]
        public IActionResult InsertUsuario([FromBody] UsuarioModel nuevoUsuario)
        {
            if (nuevoUsuario == null)
            {
                return BadRequest(new { message = "Error." });
            }

            _usuarioService.InsertUsuario(nuevoUsuario);
            return Ok(new { message = "Usuario insertado correctamente." });
        }

        [HttpPut]
        [Route("ActualizarUsuario")]
        public IActionResult ActualizarUsuario([FromBody] UsuarioModel usuario)
        {
            if (usuario == null)
            {
                return BadRequest(new { message = "Error." });
            }

            ResponseModel result = _usuarioService.ActualizarUsuario(usuario);

            return result.IsSuccess ? Ok(new { message = result.Message }) : BadRequest(new { message = result.Message });
        }

        [HttpDelete]
        [Route("DeleteUserById/{id}")]
        public IActionResult DeleteUserById(int id)
        {
            _usuarioService.DeleteUserById(id);
            return Ok(new { message = "Usuario Eliminado correctamente." });

        }

    }
}
