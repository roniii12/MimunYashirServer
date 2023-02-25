using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MimunYashirCore.Interfaces
{
    public interface IMemoryCacheService
    {
        public const string CUSTOMER_DETAILS = "CUSTOMER_DETAILS";
        public const string CUSTOMER_ID = "CUSTOMER_ID";
        public object? TryGetObjectFromCache(string key);
        public void UpdateOrCreateCache(string key, object value, int slidingMin = 5, int absoluteMin = 30);
    }
}
