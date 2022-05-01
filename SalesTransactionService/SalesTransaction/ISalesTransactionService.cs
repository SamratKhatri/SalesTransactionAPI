using System.Collections.Generic;
using System.Threading.Tasks;
using SalesTransactionService.Models;

namespace SalesTransactionService.SalesTransaction
{
    public interface ISalesTransactionService
    {
        Task AddSalesTransaction(SalesTransactionModel salesTransaction);
        Task<IEnumerable<SalesTransactionModel>> GetSalesTransaction();
        Task UpdateSalesTransaction(string id, SalesTransactionModel salesTransaction);
        Task<SalesTransactionModel> GetSalesTransactionById(string id);
    }
}