using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlanNacionalNumeracion.Common;

namespace PlanNacionalNumeracion
{
    public class Startup
    {
        private readonly string _Cors = "Cors";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            // BASE DE DATOS
            services.AddSingleton<IConfiguration>(Configuration);
<<<<<<< HEAD
            Global.ConnectionString = Configuration.GetConnectionString("localDb");
=======
            Global.ConnectionString = Configuration.GetConnectionString("localdb");
>>>>>>> 447041186293578429c72c82f906c46d5655e81a

            // INYECCION DEPENDENCIAS
            // services.AddScoped<IActividad, ActividadService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PlanNacionalNumeracion", Version = "v1" });

                /*
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
                 */
            });

            services.AddCors(options =>
            {
                options.AddPolicy(name: _Cors, builder =>
                {
                    //builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
                    //.AllowAnyHeader().AllowAnyMethod();
                    builder.WithOrigins("http://localhost:4200")
                    .AllowAnyHeader().AllowAnyMethod();
                });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PlanNacionalNumeracion v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(_Cors);

            //app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
