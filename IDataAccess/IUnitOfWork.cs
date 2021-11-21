
using System.Threading.Tasks;

namespace IDataAccess
{
    public interface IUnitOfWork
    {
        IEstimateDataRepository EstimateDataRepository();
        IActualDataRepository ActualDataRepository();
        Task SaveAsync();
        ValueTask DisposeAsync();
    }
}
