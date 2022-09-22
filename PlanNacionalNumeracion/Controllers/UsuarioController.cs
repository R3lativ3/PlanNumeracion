using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using PlanNacionalNumeracion.Models.Usuario;
using PlanNacionalNumeracion.Models;

namespace PlanNacionalNumeracion.Controllers
{
    [Route("api/usuarios")]
    [ApiController]

    public class UsuarioController : Controller
    {
        public UsuarioController(){
        }

        [HttpGet()]
        public ActionResult<List<Usuario>> ObtenerTodosUsuario()
        {
            try
            {
                UsuarioService usuarioService = new UsuarioService();
                List<Usuario> response = usuarioService.ObtenerTodos();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost()]
        public ActionResult<Response> PostPrueba(UsuarioPost usuarioPost)
        {
            try
            {
                UsuarioService usuarioService = new UsuarioService();
                var respuesta = usuarioService.AgregarUsuario(usuarioPost);
                return respuesta;
            }
            catch (Exception ex)
            {
                return new Response() { Status = 1, Message = ex.Message };
            }
        }
    }
}
    

    

