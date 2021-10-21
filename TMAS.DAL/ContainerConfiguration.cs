using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DAL.Interfaces;
using TMAS.DAL.Repositories;

namespace TMAS.DAL
{
    public static class ContainerConfiguration
    {
        public static IServiceCollection Configure(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBoardRepository, BoardRepository>();
            services.AddScoped<ICardRepository, CardRepository>();
            services.AddScoped<IColumnRepository, ColumnRepository>();
            services.AddScoped<IHistoryRepository, HistoryRepository>();
            services.AddScoped<IFileRepository, FileRepository>();
            services.AddScoped<IBoardAccessRepository, BoardAccessRepository>();
            return services;
        }
    }
}