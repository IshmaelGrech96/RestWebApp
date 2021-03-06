﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShoppingCart.Application.AutoMapper;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.Services;
using ShoppingCart.Data.Context;
using ShoppingCart.Data.Repositories;
using ShoppingCart.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.IOC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ShoppingCartDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<IProductsRepository, ProductsRepository>();
            services.AddScoped<IProductsService, ProductsService>();

            services.AddScoped<ICategoriesRepository, CategoriesRepository>();
            services.AddScoped<ICategoryService, CategoriesService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<ICartProductRepository, CartProductRepository>();
            services.AddScoped<ICartProductService, CartProductService>();

            services.AddScoped<ICartRepository, CartRepository>();

            services.AddScoped<IOrderDetailsRepository, OrderDetailsRepository>();

            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddAutoMapper(typeof(AutoMapperConfiguration));


        }

    }
}
