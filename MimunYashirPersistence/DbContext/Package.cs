using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MimunYashirPersistence
{
    [Table("Package")]
    public class Package
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ContractId { get; set; }
        public PackageType PackageType { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int Usage { get; set; }
        public Contract Contract { get; set; }
    }

    public enum PackageType
    {
        Complete,
        Basic
    }
}
