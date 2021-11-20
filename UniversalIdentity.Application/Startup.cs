using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using UniversalIdentity.Domain.Entities;
using UniversalIdentity.Domain.Interfaces;
using UniversalIdentity.Domain.Settings;
using UniversalIdentity.Infra.Data.Context;
using UniversalIdentity.Infra.Data.Mapping;
using UniversalIdentity.Infra.Data.Repository;
using UniversalIdentity.Service.Services;

namespace UniversalIdentity.Application
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            //Desativar a validação automática do ModelStates
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services
                .AddControllers()
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services
                .AddDbContext<UniversalIdentityContext>(options => options.UseSqlServer(Configuration.GetConnectionString("UniversalIdentityContext")));

            #region health checks
            #endregion health checks

            #region Settings

            services.Configure<JwtSettings>(Configuration.GetSection(JwtSettings.Jwt));

            #endregion Settings

            #region repository

            services.AddScoped<IBaseRepository<Pessoa>, BaseRepository<Pessoa>>();
            services.AddScoped<IBaseRepository<Login>, BaseRepository<Login>>();
            services.AddScoped<IBaseRepository<Atividade>, BaseRepository<Atividade>>();
            services.AddScoped<ILoginRepository, LoginRepository>();
            services.AddScoped<IAtividadeRepository, AtividadeRepository>();
            services.AddScoped<IPessoaRepository, PessoaRepository>();

            #endregion repository

            #region service

            services.AddScoped<IBaseService<Pessoa>, BaseService<Pessoa>>();
            services.AddScoped<IBaseService<Login>, BaseService<Login>>();
            services.AddScoped<IBaseService<Atividade>, BaseService<Atividade>>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IAtividadeService, AtividadeService>();
            services.AddScoped<IUniversalIdentityService, UniversalIdentityService>();
            services.AddScoped<IQRCodeService, QRCodeService>();

            #endregion service

            #region autoMapper
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            var mapper = configuration.CreateMapper();
            services.AddSingleton(mapper);
            #endregion autoMapper

            #region Autenticação

            services.AddAuthentication
                (JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                });

            #endregion Autenticação

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
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
}
