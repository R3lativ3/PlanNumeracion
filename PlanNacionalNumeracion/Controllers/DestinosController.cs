﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlanNacionalNumeracion.Models;
using PlanNacionalNumeracion.Models.Destino;
using PlanNacionalNumeracion.Services;

namespace PlanNacionalNumeracion.Controllers
{
    [Route("api/destinos")]
    [ApiController]
    public class DestinosController : Controller
	{
		public DestinosController()
		{
		}

		[HttpGet]
        public ActionResult<List<Destino>> ObtenerTodosDestinos()
        {
            try
            {
                DestinosService destinoService = new DestinosService();
                List<Destino> response = destinoService.ObtenerTodosDestinos();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

