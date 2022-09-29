using System;
using Dapper;
using PlanNacionalNumeracion.Common;
using PlanNacionalNumeracion.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using PlanNacionalNumeracion.Models.Destino;

namespace PlanNacionalNumeracion.Services
{
	public class DestinosService
	{
		public DestinosService()
		{
		}

        public List<Destino> ObtenerTodosDestinos()
        {
            try
            {
                string query = @"
                    SELECT id, nombre, ruta, ip, puerto
                    FROM PNN_destino WITH(NOLOCK)
                ";
                using (IDbConnection conn = new SqlConnection(Global.ConnectionString))
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    var respuesta = conn.Query<Destino>(query);
                    return respuesta.AsList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Destino ObtenerDestino(int id)
        {
            try
            {
                string query = @"
                    SELECT id, nombre, ruta, ip, puerto
                    FROM PNN_destino WITH(NOLOCK)
                    WHERE id = @id
                ";
                using (IDbConnection conn = new SqlConnection(Global.ConnectionString))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    var respuesta = conn.QueryFirstOrDefault<Destino>(query, new { id });
                    return respuesta;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Response AgregarDestino(DestinoPost destinoPost)
        {
            try
            {
                string insert = @"
                    INSERT INTO PNN_destino (nombre, ruta, ip, puerto)  OUTPUT INSERTED.*
                    VALUES (@nombre, @ruta, @ip, @puerto);
                ";
                using (IDbConnection conn = new SqlConnection(Global.ConnectionString)) // utilizar la cadena de conexion que establecimos en el archivo Startup.cs
                {
                    if (conn.State == ConnectionState.Closed)                           // validar si la conexion esta cerrada, si lo esta, abrir la conexion
                    {
                        conn.Open();
                    }

                    /*
                    DynamicParameters parameters = new DynamicParameters();             // instanciar objeto que vamos a insertar
                    parameters.Add("@nombre", destinoPost.Nombre);
                    parameters.Add("@ruta", destinoPost.Ruta);
                    parameters.Add("@ip", destinoPost.Ip);
                    parameters.Add("@puerto", destinoPost.Puerto);
                    */
                    //int id = conn.ExecuteScalar<int>(insert, parameters);                    // ejecutarlo
                    
                    int id = conn.ExecuteScalar<int>(insert, new {
                        nombre = destinoPost.Nombre,
                        ruta = destinoPost.Ruta,
                        ip = destinoPost.Ip,
                        puerto = destinoPost.Puerto
                    });
                    Response resp = new Response();
                    resp.Status = 0;
                    resp.Message = $"Creado satisfactoriamente ID: {id}";
                    return resp;
                }
            }
            catch (Exception ex)
            {
                return new Response { Status = 1, Message = ex.Message };
            }
        }

        /*
        public Ambiente GetAmbiente(int id)
        {
            try
            {
                string query = @"
                    SELECT id, tipo_ambiente as tipoAmbiente, status 
                    FROM ambiente_servidor WITH(NOLOCK)
                    WHERE id = @id
                ";
                using (IDbConnection conn = new SqlConnection(Global.ConnectionString))
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    var ambiente = conn.QueryFirstOrDefault<Ambiente>(query, new { id = id });
                    return ambiente;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Response UpdateAmbiente(int id, Ambiente ambiente)
        {
            string update = @"
                UPDATE ambiente_servidor  
                SET tipo_ambiente = @tipoAmbiente, status = @status 
                WHERE id = @id
            ";
            try
            {
                using (IDbConnection conn = new SqlConnection(Global.ConnectionString))
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    var updated = conn.Execute(update, new { tipoAmbiente = ambiente.tipoAmbiente, status = ambiente.status, id });
                    return new Response { Status = 0, Message = "Actualizado correctamente" };
                }
            }
            catch (Exception ex)
            {
                return new Response { Status = 1, Message = ex.Message };
            }
        }

        public Response DeleteAmbiente(int id)
        {
            string update = @"UPDATE ambiente_servidor SET STATUS = 0 WHERE id = @id";
            try
            {
                using (IDbConnection conn = new SqlConnection(Global.ConnectionString))
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();
                    var updated = conn.Execute(update, new { id });
                    return new Response { Status = 0, Message = Util.Util.Eliminado("AmbienteServidor", id) };
                }
            }
            catch (Exception ex)
            {
                return new Response { Status = 1, Message = ex.Message };
            }
        }
        */
    }
}

