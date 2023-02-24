using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MimunYashirInfrastructure.Configuration
{
    public class ConfigurationService
    {
        private readonly IConfiguration _config;
        public ConfigurationService(IConfiguration config)
        {
            _config = config;
        }
        public string GetConnectionString()
        {
            return _config.GetConnectionString("MainConnectionString")!;
        }
    }
}
