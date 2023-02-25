using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MimunYashirCore.Models
{
    public class UpdateAddressModel
    {
        public string City { get; set; }
        public string Street { get; set; }
        public string Apartment { get; set; }
        public string Zip { get; set; }
    }
}
