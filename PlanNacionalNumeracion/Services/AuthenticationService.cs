using System;
using PlanNacionalNumeracion.Common;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Principal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using PlanNacionalNumeracion.Models.Authentication;
using System.Data.SqlClient;
using EncriptadorMA;
using PlanNacionalNumeracion.Interfaces;

namespace PlanNacionalNumeracion.Services
{
	public class AuthenticationService : IAuthentication
	{
        private readonly AppSettings _appSettings;
        public IConfiguration Configuration { get; }

        public AuthenticationService(IOptions<AppSettings> appSettings, IConfiguration configuration)
		{
            _appSettings = appSettings.Value;
            Configuration = configuration;
        }

        public UserResponse Auth(AuthRequest authRequest)
        {
            try
            {
                UserResponse userResponse = new UserResponse();
                using IDbConnection conn = new SqlConnection(Global.ConnectionString);
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                var enc = new Encrypt();
                string password = enc.Encriptar(authRequest.Password);  /* Encrypt.GetSHA256(authRequest.Password);*/
                string query = @"SELECT id, nombres, apellido_paterno apellidoPaterno, apellido_materno apellidoMaterno, attuid
                                FROM PNN_usuario WITH(NOLOCK)
                                WHERE attuid = @attuid and psw = @pass";

                var user = conn.QueryFirstOrDefault<UserLogin>(query, new { attuid = authRequest.UserName, pass = password });

                if (user == null)
                    return new UserResponse { Ok = false, Message = "Error en inicio de sesion", User = null };

                userResponse = new UserResponse
                {
                    Ok = true,
                    Message = "Login correcto",
                    User = new TokenUsername
                    {
                        UserName = user.Attuid,
                        Name = user.Nombres + " " + user.ApellidoPaterno + " " + user.ApellidoMaterno,
                        Token = GetToken(user)
                    }
                };

                return userResponse;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        private string GetToken(UserLogin authRequest)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                        new Claim[]
                        {
                            new Claim("ID", authRequest.Id.ToString()),
                            new Claim("ATTUID", authRequest.Attuid),
                            new Claim("NOMBRE", authRequest.Nombres+" "+authRequest.ApellidoPaterno+" "+authRequest.ApellidoMaterno)
                        }
                    ),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public bool ValidateToken(string token)
        {
            if (string.IsNullOrEmpty(token))
                return false;
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = GetValidationParameters();

                SecurityToken validatedToken;
                IPrincipal principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }


        private TokenValidationParameters GetValidationParameters()
        {
            var appSettingsSection = Configuration.GetSection("AppSettings");
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            return new TokenValidationParameters()
            {
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                ValidateAudience = false,
                ValidateIssuer = false,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };
        }

        public UserResponse Validate(string token)
        {
            if (string.IsNullOrEmpty(token))
                return new UserResponse { Ok = false, Message = "Error en validacion", User = null };
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = GetValidationParameters();

                SecurityToken validatedToken;
                IPrincipal principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
                var userResponse = new UserResponse
                {
                    Ok = true,
                    Message = "Usuario validado",
                    User = new TokenUsername
                    {
                        UserName = principal.Identity.Name,
                        Name = principal.Identity.Name,
                        Token = token
                    }
                };

                return userResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new UserResponse { Ok = false, Message = "Error en validacion", User = null };
            }
        }


    }
}

