using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MimunYashirInfrastructure.Extensions
{
    public static class ConfigurationExtensions
    {
        public static string GetMainConnectionString(this IConfiguration Configuration)
        {
            return Configuration.GetConnectionString("MainConnectionString")!;
        }
    }
}
