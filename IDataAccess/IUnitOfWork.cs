
using System.Threading.Tasks;

namespace IDataAccess
{
    public interface IUnitOfWork
    {
        Task SaveAsync();
        ValueTask DisposeAsync();
    }
}
