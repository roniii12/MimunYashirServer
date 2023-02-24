using Microsoft.Extensions.DependencyInjection;
using MimunYashirCore.Interfaces;
using MimunYashirCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MimunYashirCore
{
    public class ServicesConfiguration
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddAutoMapper(typeof(DomainAutoMapperProfile).Assembly);
        }
    }
}
