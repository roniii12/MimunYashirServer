using MimunYashirPersistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MimunYashirCore.Models
{
    public class PackageModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public PackageType PackageType { get; set; }
        public int Quantity { get; set; }
        public int Usage { get; set; }
    }
}
