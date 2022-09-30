using System.Collections.Generic;
using PlanNacionalNumeracion.Common;
using System.Data;
using PlanNacionalNumeracion.Models.Usuario;
using System.Data.SqlClient;
using Dapper;
using System;
using PlanNacionalNumeracion.Models;
using EncriptadorMA;

public class UsuarioService
{
    public List<Usuario> ObtenerTodos()
    {
        string consulta = @"
            Select id, nombres, apellido_paterno ApellidoPaterno, apellido_materno ApellidoMaterno, attuid, psw
            From PNN_usuario";
        using (IDbConnection conn = new SqlConnection(Global.ConnectionString))
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            List<Usuario> list = new List<Usuario>(); //opcion #1
            //var list = new List<Usuario>();             // opcion #2
            list = conn.Query<Usuario>(consulta).AsList();
            return list;
        }
    }

    public Response AgregarUsuario(UsuarioPost usuarioPost)
    {
        try
        {
            string query = @"
                            INSERT INTO PNN_usuario(nombres, apellido_materno, apellido_paterno, attuid, psw)
                    VALUES (@nombres, @apellido_materno, @apellido_paterno, @attuid, @psw)";

                using (IDbConnection conexion = new SqlConnection(Global.ConnectionString))
                {
                    if (conexion.State == ConnectionState.Closed)
                    {
                        conexion.Open();
                    }
                Encrypt encriptador = new Encrypt();
                string psw = encriptador.Encriptar(usuarioPost.Psw);
                    // opcion 1
                    DynamicParameters paramateres = new DynamicParameters();
                    paramateres.Add("@nombres", usuarioPost.Nombres);
                    paramateres.Add("@apellido_materno", usuarioPost.ApellidoMaterno);
                    paramateres.Add("@apellido_paterno", usuarioPost.ApellidoPaterno);
                    paramateres.Add("@attuid", usuarioPost.Attuid);
                    paramateres.Add("@psw", encriptador.Encriptar(usuarioPost.Psw));
                    conexion.Execute(query, paramateres);
                    
                    /*
                    // opcion 2
                    
                    var respuesta = conexion.ExecuteScalar(query, new
                    {
                        nombres = usuarioPost.nombres,
                        apellido_materno = usuarioPost.apellidoMaterno,
                        apellido_paternox = usuarioPost.apellidoPaterno,
                        attuid = usuarioPost.attuid,
                        psw = usuarioPost.psw
                    });*/
                    return new Response() { Status = 0, Message = "Se ha creado el registro exitosamente"};
                }
        }
        catch(Exception ex)
        {
            return new Response() { Status = 1, Message = ex.Message };
        }
    }

    public Usuario GetUsuario(int id)
    {
        try
        {
            string query = @"
                    SELECT id, nombres, apellido_paterno as ApellidoPaterno, apellido_materno as ApellidoMaterno, attuid, psw 
                    FROM PNN_usuario WITH(NOLOCK)
                    WHERE id = @id
                ";
            using (IDbConnection conn = new SqlConnection(Global.ConnectionString))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                var usuario = conn.QueryFirstOrDefault<Usuario>(query, new { id = id });
                return usuario;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public Response UpdateUsuario(int id, UsuarioPost usuarioPost)
    {
        string update = @"
                UPDATE nombres, apellido_paterno as ApellidoPaterno, apellido_materno as ApellidoMaterno, attuid, psw 
                SET nombres = @nombres, apellido_paterno = @apellido_paterno, apellido_materno = @apellido_materno, attuid = @attuid, psw = @psw
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
                var updated = conn.Execute(update, new {  nombres= usuarioPost.Nombres, apellido_paterno = usuarioPost.ApellidoMaterno, 
                                                          apellido_materno = usuarioPost.ApellidoMaterno, attuid = usuarioPost.Attuid, psw = usuarioPost.Psw, id });
                return new Response { Status = 0, Message = "Actualizado correctamente" };
            }
        }
        catch (Exception ex)
        {
            return new Response { Status = 1, Message = ex.Message };
        }
    }
}