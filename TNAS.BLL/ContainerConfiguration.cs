using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.BLL.Interfaces;
using TMAS.BLL.Services;
using TMAS.BLL.Validator;
using TMAS.DAL.DTO.Created;
using TMAS.DAL.DTO.View;
using TMAS.DB.Models;

namespace TMAS.BLL
{
    public static class ContainerConfiguration
    {
        public static IServiceCollection Configure(this IServiceCollection services)
        {
            DAL.ContainerConfiguration.Configure(services);
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBoardService, BoardService>();
            services.AddScoped<ICardService, CardService>();
            services.AddScoped<IColumnService, ColumnService>();
            services.AddScoped<IColumnsSortService, ColumnsSortService>();
            services.AddScoped<IHistoryService, HistoryService>();
            services.AddScoped<ICardsSortService, CardsSortService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IBoardAccessService, BoardsAccessService>();
            services.AddScoped<ITokenService,TokenService>();
            services.AddSingleton<AbstractValidator<UserCreatedDto>, UserValidator>();
            services.AddSingleton<AbstractValidator<CardCreatedDTO>, CardsValidator>();
            services.AddSingleton<AbstractValidator<CardContentDTO>, CardsContentValidator>();
            services.AddSingleton<AbstractValidator<AccessCreatedDTO>, AccessValidator>();
            services.AddSingleton<AbstractValidator<CardViewDTO>, CardsViewValidator>();
            services.AddSingleton<AbstractValidator<ColumnViewDTO>, ColumnViewValidator>();
            services.AddSingleton<AbstractValidator<History>, HistoryValidator>();
            services.AddSingleton<AbstractValidator<File>, FileValidator>();
            return services;
        }
    }
}