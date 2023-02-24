using MimunYashirPersistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MimunYashirCore.Models
{
    public class ContractModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ContractType Type { get; set; }
        public IEnumerable<PackageModel> Packages { get; set; }
    }
}
