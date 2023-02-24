using MimunYashirInfrastructure.Cummon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MimunYashirPersistence.Repositories
{
    public class MainEfRepository<T> : EfRepository<T> where T : class
    {
        public MainEfRepository(MainDbContext dbContext, IAppContext serviceContext) : base(dbContext, serviceContext)
        {

        }
    }

}
