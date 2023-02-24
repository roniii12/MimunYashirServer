using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MimunYashirPersistence
{
    [Table("Contract")]
    public class Contract
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public string Name { get; set; }
        public ContractType Type { get; set; }
        public Customer Customer { get; set; }
        public IEnumerable<Package> Packages { get; set; }
    }


    public enum ContractType
    {
        Basic,
        Premium
    }
}
