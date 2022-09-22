using Microsoft.AspNetCore.Mvc;
using PlanNacionalNumeracion.Models;
using PlanNacionalNumeracion.Models.Destino;
using PlanNacionalNumeracion.Models.ModelsYat;
using PlanNacionalNumeracion.Services;
using System;
using System.Collections.Generic;

namespace PlanNacionalNumeracion.Controllers
{
    [ApiController]
    [Route("api/cargaDestino")]
    public class CargaDestinoController: Controller
    {
        [HttpGet]
        public ActionResult<List<CargaDestino>> ObtenerTodosCargaDestino()
        {
            try
            {
                CargaDestinoService cargaDestinoService = new CargaDestinoService();
                List<CargaDestino> response = cargaDestinoService.ObtenerTodosCargaDestino();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /*
        public ActionResult<List<CargaDestino>> Get()
        {
            return new List<CargaDestino>() {

                new CargaDestino() { id=1, fechaCarga = DateTime.Now, nombreArchivo="archivo1", idPnnDestino=1,idPnnUsuario=1 },
                new CargaDestino() { id=2, fechaCarga = DateTime.Now, nombreArchivo="archivo2", idPnnDestino=2,idPnnUsuario=2 },

            };

        }*/
        [HttpPost]
        public ActionResult<Response> AddFileToTranscription(CargaDestinoPost cargaDestinoPost)
        {
            try
            {
                CargaDestinoService cargaDestinoService = new CargaDestinoService();
                Response response = cargaDestinoService.AgregarCargaDestino(cargaDestinoPost);
                if (response.Status == 0)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /*
        [HttpPost]
        public ActionResult<string> PostCargaDestino(string nombre)
        {
            return "prueba " + nombre;
        }*/

        [HttpPut]
        public ActionResult<string> PutCargaDestino(CargaDestinoPost nombre)
        {
            return "prueba " + nombre;
        }

        [HttpDelete]
        public ActionResult<string> DeleteCargaDestino(string nombre)
        {
            return "prueba " + nombre;
        }


    }
}
