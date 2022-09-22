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


    }
}
