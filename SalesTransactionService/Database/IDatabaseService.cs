using System.Data;
using System.Threading.Tasks;

namespace SalesTransactionService
{
    public interface IDatabaseService
    {
        Task<IDbConnection> GetConnection();
    }
}