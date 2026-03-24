using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDoListApi.Domain.Interfaces;
using ToDoListApi.Infrastructure.Data;
using ToDoListApi.Infrastructure.Repositories;

namespace ToDoListApi.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IToDoItemRepository, ToDoItemRepository>();

        return services;
    }
}
