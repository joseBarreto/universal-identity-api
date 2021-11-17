using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UniversalIdentity.Domain.Entities;
using UniversalIdentity.Domain.Interfaces;
using UniversalIdentity.Infra.Data.Context;
using UniversalIdentity.Infra.Data.Repository;
using UniversalIdentity.Service.Services;

namespace UniversalIdentity.Application
{
#pragma warning disable CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services
                .AddControllers()
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services
                .AddDbContext<UniversalIdentityContext>(options => options.UseSqlServer(Configuration.GetConnectionString("UniversalIdentityContext")));

            #region health checks
            #endregion health checks

            #region repository

            services.AddScoped<IBaseRepository<Pessoa>, BaseRepository<Pessoa>>();
            services.AddScoped<IBaseRepository<Login>, BaseRepository<Login>>();

            #endregion repository

            #region service

            services.AddScoped<IBaseService<Pessoa>, BaseService<Pessoa>>();
            services.AddScoped<IBaseService<Login>, BaseService<Login>>();

            #endregion service

            #region autoMapper
            #endregion autoMapper

            #region autenticação
            #endregion autenticação

            #region swagger
            _ = services.AddSwaggerGen(c =>
              {
                  c.SwaggerDoc("v1",
                      new OpenApiInfo
                      {
                          Version = "v1",
                          Title = "UniversalIdentity Service",
                          Description = "API do Universal Identity"
                      });

                  c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                  {
                      Description = $@"Cabeçalho de autorização JWT usando o esquema Bearer.
                                    {Environment.NewLine}Digite 'Bearer' [espaço] e, em seguida, seu token.
                                    {Environment.NewLine}Exemplo: 'Bearer 12345abcdef'",
                      Name = "Authorization",
                      In = ParameterLocation.Header,
                      Type = SecuritySchemeType.ApiKey,
                      Scheme = "Bearer"
                  });

                  c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                  {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                  });

                  var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                  var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                  c.IncludeXmlComments(xmlPath);
              });
            #endregion swagger
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Universal Identity v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
#pragma warning restore CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
}
