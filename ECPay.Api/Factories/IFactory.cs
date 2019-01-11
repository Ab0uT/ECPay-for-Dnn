using ECPay.Api.Models;
using System.Threading.Tasks;

namespace ECPay.Api.Factories
{
    public interface IFactory<TEntity> where TEntity : class
    {
        string PrintForECPay(string url, TEntity instance);

        Task<APIResult> PostAsyncForECPay(string url, TEntity entity);

        bool EnableSecurityProtocol { set; }
    }
}
