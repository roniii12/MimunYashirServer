using AutoMapper;
using MimunYashirCore.Models;
using MimunYashirPersistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MimunYashirCore
{
    public class DomainAutoMapperProfile : Profile
    {
        public DomainAutoMapperProfile()
        {
            CreateMap<Package, PackageModel>();
            CreateMap<Contract, ContractModel>();
            CreateMap<Customer, CustomerModel>().ReverseMap();
        }
    }
}
