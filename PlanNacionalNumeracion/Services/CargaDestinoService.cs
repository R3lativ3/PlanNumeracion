using Dapper;
using System.Data.SqlClient;
using PlanNacionalNumeracion.Common;
using PlanNacionalNumeracion.Models;
using PlanNacionalNumeracion.Models.Destino;
using PlanNacionalNumeracion.Models.ModelsYat;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Http;
using Renci.SshNet;
using System.IO;

namespace PlanNacionalNumeracion.Services
{
    public class CargaDestinoService
    {
        public List<CargaDestino> ObtenerTodosCargaDestino() 
        {
            try
            {
                string consulta = @"SELECT id,fecha_carga fechaCarga,nombre_archivo nombreArchivo,id_PNN_destino  idPnnDestino, id_PNN_usuario  idPnnUsuario
                                 FROM	PNN_carga_destino WITH(NOLOCK)";
                using (IDbConnection conn = new SqlConnection(Global.ConnectionString))
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    var list = conn.Query<CargaDestino>(consulta);
                    return list.AsList();
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public Response AgregarCargaDestino(CargaDestinoPost cargaDestinoPost)
        {
            try
            {
                string insert = @"
                    INSERT INTO PNN_carga_destino (fecha_carga, nombre_archivo, id_PNN_destino, id_PNN_usuario)  
                    VALUES (@fecha_carga, @nombre_archivo, @id_PNN_destino, @id_PNN_usuario);

                ";
                using (IDbConnection conn = new SqlConnection(Global.ConnectionString)) // utilizar la cadena de conexion que establecimos en el archivo Startup.cs
                {
                    if (conn.State == ConnectionState.Closed)                           // validar si la conexion esta cerrada, si lo esta, abrir la conexion
                    {
                        conn.Open();
                    }

                    int id = conn.ExecuteScalar<int>(insert, new
                    {
                        fecha_carga = cargaDestinoPost.FechaCarga,
                        nombre_archivo = cargaDestinoPost.NombreArchivo,
                        id_PNN_destino = cargaDestinoPost.IdPnnDestino,
                        id_PNN_usuario = cargaDestinoPost.IdPnnUsuario

                    });
                    Response resp = new Response();
                    resp.Status = 0;
                    resp.Message = $"se a creado el registro exitosamente";
                    return resp;
                }
            }
            catch (Exception ex)
            {
                return new Response { Status = 1, Message = ex.Message };
            }
        }

        public Response CargarArchivoServidor(string ip, string path, string usuarioServidor, string pswUsuario, IFormFile archivo)
        {
            try
            {
                using (ScpClient client = new ScpClient(ip, usuarioServidor, pswUsuario))
                {
                    client.Connect();
                    client.Upload(archivo.OpenReadStream(), path + archivo.FileName);
                    return new Response(){
                        Status = 0,
                        Message = $"Archivo: {archivo.FileName}, cargado exitosamente en servidor: {ip}, ruta: {path}"
                    };
                }
            }
            catch(Exception ex)
            {
                return new Response()
                {
                    Status = 1,
                    Message = $"Error cargando archivo: {archivo.FileName}, en servidor: {ip}, path: {path}, error: {ex.Message}"
                };
            }
        }

        public List<Response> CargarArchivoDestino(int[] idDestino, IFormFile archivo)
        {
            var response = new List<Response>();
            var usuarioDestinoService = new UsuarioDestinoService();
            var destinoService = new DestinosService();
            foreach (var id in idDestino)
            {
                try
                {
                    var credenciales = usuarioDestinoService.ObtenerUsuarioDestinoPorIdDestino(id);
                    var destino = destinoService.ObtenerDestino(id);
                    if (credenciales is not null && destino is not null)
                    {

                        var uploaded = CargarArchivoServidor(destino.Ip, destino.Puerto, credenciales.Usuario, credenciales.Psw, archivo);
                        if (uploaded.Status == 1)
                        {
                            response.Add(uploaded);
                            break;
                        }
                        var guardadoEnBd = AgregarCargaDestino(new CargaDestinoPost() { FechaCarga = DateTime.Now, IdPnnDestino = id, IdPnnUsuario = 1, NombreArchivo = archivo.FileName });
                        response.Add(new Response()
                        {
                            Status = 0,
                            Message = $"Carga del archivo en servidor: {uploaded.Message}, Constancia en BD: {guardadoEnBd.Message}"
                        });
                    }
                    else
                    {
                        response.Add(new Response()
                        {
                            Status = 1,
                            Message = $"Archivo: {archivo.FileName}, no pudo ser cargado en destino: {id} porque no existe un destino o credenciales " +
                            $"asociadas con la llave, destino: {destino.Ip}, credeciales asociadas: {credenciales}"
                        });
                    }
                }
                catch (Exception ex)
                {
                    response.Add(new Response()
                    {
                        Status = 1,
                        Message = $"Archivo: {archivo.FileName}, no pudo ser cargado en servidor, error: {ex.Message}"
                    }); ;
                }
            }

            return response;
        }
    }
}
