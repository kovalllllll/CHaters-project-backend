using API.Mappers.Users;
using API.Mappers.Characteristics; // Додати ваш профіль для характеристик
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
        
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
        });
        
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // AutoMapper
        builder.Services.AddAutoMapper(typeof(UserMappingProfile), typeof(CharacteristicMappingProfile));
        
        // Dependency Injection
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IPasswordEncoder, PasswordEncoder>();
        builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
        builder.Services.AddScoped<ICharacteristicRepository, CharacteristicRepository>();
        builder.Services.AddScoped<ICharacteristicService, CharacteristicService>();
        builder.Services.AddScoped<CharacteristicService>(); // Для безпосереднього використання класу

        builder.Services.AddControllers();
        
        var app = builder.Build();
        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.MapControllers();
        app.Run();
    }
}