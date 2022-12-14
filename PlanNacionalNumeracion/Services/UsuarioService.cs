using System.Collections.Generic;
using PlanNacionalNumeracion.Common;
using System.Data;
using PlanNacionalNumeracion.Models.Usuario;
using System.Data.SqlClient;
using Dapper;
using System;
using PlanNacionalNumeracion.Models;
using EncriptadorMA;
using Microsoft.VisualBasic;

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
                VALUES (@nombres, @apellido_materno, @apellido_paterno, @attuid, @psw)
            ";

            using (IDbConnection conexion = new SqlConnection(Global.ConnectionString))
            {
                if (conexion.State == ConnectionState.Closed)
                {
                    conexion.Open();
                }
                Encrypt encriptador = new Encrypt();
                string psw = encriptador.Encriptar(usuarioPost.Psw);

                DynamicParameters paramateres = new DynamicParameters();
                paramateres.Add("@nombres", usuarioPost.Nombres);
                paramateres.Add("@apellido_materno", usuarioPost.ApellidoMaterno);
                paramateres.Add("@apellido_paterno", usuarioPost.ApellidoPaterno);
                paramateres.Add("@attuid", usuarioPost.Attuid);
                paramateres.Add("@psw", encriptador.Encriptar(usuarioPost.Psw));

                conexion.Execute(query, paramateres);

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
                UPDATE PNN_usuario 
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

                var enc = new Encrypt();
                var updated = conn.Execute(update, new {
                    nombres= usuarioPost.Nombres,
                    apellido_paterno = usuarioPost.ApellidoPaterno, 
                    apellido_materno = usuarioPost.ApellidoMaterno,
                    attuid = usuarioPost.Attuid,
                    psw = enc.Encriptar(usuarioPost.Psw),
                    id
                });
                return new Response { Status = 0, Message = "Actualizado correctamente" };
            }
        }
        catch (Exception ex)
        {
            return new Response { Status = 1, Message = ex.Message };
        }
    }

    public Response DeleteUsuario(int id)
    {
        try
        {
            string query = @"DELETE FROM PNN_usuario WHERE id = @id";
            using (IDbConnection conn = new SqlConnection(Global.ConnectionString))
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                conn.Execute(query, new { id });
                return new Response { Status = 0, Message = "Usuario Eliminado Correctamente"};
            }
        }
        catch (Exception ex)
        {
            return new Response { Status = 1, Message = ex.Message };
        }
    }
}