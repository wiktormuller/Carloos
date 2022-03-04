using System.Threading.Tasks;

namespace JobJetRestApi.Application.Ports
{
    public interface ICacheService
    {
        void Add<TItem>(TItem item, string cacheKey);
        TItem Get<TItem>(string cacheKey) where TItem : class;
    }
}