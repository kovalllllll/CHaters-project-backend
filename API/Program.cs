using API.Mappers.Users;
using API.Mappers.Product;
using API.Security;
using API.Security.Impl;
using BLL.Services;
using BLL.Services.Impl;
using DAL;
using DAL.Repositories;
using DAL.Repositories.Impl;
using Microsoft.EntityFrameworkCore;

namespace API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add logging
        builder.Logging.AddConsole();
        builder.Logging.SetMinimumLevel(LogLevel.Debug);

        // Setting up the database
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
        });

        // Setting up CORS
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowFrontend",
                policy =>
                {
                    policy.WithOrigins("http://localhost:5173") // FrontEnd URL
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials(); // If you need to support cookies or authorization
                });
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddAutoMapper(typeof(UserMappingProfile));
        builder.Services.AddAutoMapper(typeof(UserMappingProfile), typeof(CharacteristicMappingProfile));
        builder.Services.AddAutoMapper(typeof(UserMappingProfile), typeof(ProductMappingProfile));
        builder.Services.AddAutoMapper(typeof(UserMappingProfile), typeof(ImageMappingProfile));
        builder.Services.AddAutoMapper(typeof(UserMappingProfile), typeof(ProductCharacteristicMappingProfile));

        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IPasswordEncoder, PasswordEncoder>();
        builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
        builder.Services.AddScoped<ICharacteristicRepository, CharacteristicRepository>();
        builder.Services.AddScoped<ICharacteristicService, CharacteristicService>();
        builder.Services.AddScoped<CharacteristicService>();
        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddScoped<ProductService>();
        builder.Services.AddScoped<IImageRepository, ImageRepository>();
        builder.Services.AddScoped<IImageService, ImageService>();
        builder.Services.AddScoped<ImageService>();
        builder.Services.AddScoped<IProductCharacteristicRepository, ProductCharacteristicRepository>();
        builder.Services.AddScoped<IProductCharacteristicService, ProductCharacteristicService>();
        builder.Services.AddScoped<ProductCharacteristicService>();

        var bucket = builder.Configuration.GetValue<string>("FileStorage:Bucket");
        builder.Services.AddSingleton(bucket);

        builder.Services.AddControllers();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseRouting();

        // Using CORS should come before app.UseAuthorization()
        app.UseCors("AllowFrontend");

        // app.UseAuthorization();

        app.MapControllers();
        app.Run();
    }
}