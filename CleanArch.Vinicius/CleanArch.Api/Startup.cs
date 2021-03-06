using AutoMapper;
using CleanArch.Api.Models;
using CleanArch.Domain.DTOs;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Interface;
using CleanArch.Domain.Validation;
using CleanArch.Infra.Data.Banco;
using CleanArch.Infra.Data.Repositories;
using CleanArch.Service.services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace CleanArch.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers()
                .AddFluentValidation(x =>
                {
                    x.RegisterValidatorsFromAssemblyContaining<CategoryValidator>();
                    x.RegisterValidatorsFromAssemblyContaining<ProductValidator>();
                });

            services.AddDbContext<Context>(options =>
            {
                options.UseNpgsql("server=localhost;Port=5432;Database=CleanArcVncdb;UserId=postgres;Password=adm");
            });

            services.AddSingleton(new MapperConfiguration(config =>
            {
                config.CreateMap<Category, CategoryModel>();
                config.CreateMap<CategoryModel, Category>();
                config.CreateMap<CategoryDto, Category>();
                config.CreateMap<Product, ProductDto>().ReverseMap();
                config.CreateMap<Product, ProductModel>();
                config.CreateMap<ProductModel, Product>();

            }).CreateMapper());

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CleanArch.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => {
                 c.SwaggerEndpoint("/swagger/v1/swagger.json", "CleanArch.Api v1");
                 c.RoutePrefix = string.Empty;
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
