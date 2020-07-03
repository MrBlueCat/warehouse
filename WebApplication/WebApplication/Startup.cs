using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Warehouse;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddCors();

            InitRepositories(services);
            InitServices(services);
            InitValidators(services);
            InitAuthentication(services);
            InitSwagger(services);

            services.AddAutoMapper((conf) =>
            {
                conf.AddProfile(new ItemProfile());
                conf.AddProfile(new ManufacturerProfile());
                conf.AddProfile(new CustomerProfile());
            }, typeof(Startup));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(options =>
            {
                options.AllowAnyOrigin();
                options.AllowAnyHeader();
                options.AllowAnyMethod();
            });
            app.UseAuthentication();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Warehouse v1");
                c.RoutePrefix = string.Empty;
            });
        }

        private void InitRepositories(IServiceCollection services)
        {
            services.AddTransient<MongoDbContext, MongoDbContext>((md) =>
            {
                return new MongoDbContext(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddTransient<IRepository<Item>, MongoRepository<Item>>();
            services.AddTransient<IRepository<Customer>, MongoRepository<Customer>>();
            services.AddTransient<IRepository<Manufacturer>, MongoRepository<Manufacturer>>();
            services.AddTransient<IRepository<User>, MongoRepository<User>>();
        }

        private void InitServices(IServiceCollection services)
        {
            services.AddTransient<IService<Customer>, DataBaseService<Customer>>();
            services.AddTransient<IService<Manufacturer>, DataBaseService<Manufacturer>>();

            services.AddTransient<ItemService, ItemService>();
        }

        private void InitValidators(IServiceCollection services)
        {
            services.AddTransient<IValidator<RequestItemModel>, ItemRequestValidator>();
            services.AddTransient<IValidator<RequestManufacturerModel>, ManufacturerRequestValidator>();
            services.AddTransient<IValidator<RequestCustomerModel>, CustomerRequestValidator>();
        }

        private void InitAuthentication(IServiceCollection services)
        {
            services.AddTransient<IJwtGenerator, JwtGenerator>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidIssuer = AuthOptions.ISSUER,

                            ValidateAudience = true,
                            ValidAudience = AuthOptions.AUDIENCE,

                            ValidateLifetime = true,

                            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                            ValidateIssuerSigningKey = true,
                        };
                    });
        }

        private void InitSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Warehouse", Version = "v1" });
                c.AddSecurityDefinition("Bearer",
                    new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.",
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey
                    });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            },
                            Scheme="oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });
        }
    }
}
