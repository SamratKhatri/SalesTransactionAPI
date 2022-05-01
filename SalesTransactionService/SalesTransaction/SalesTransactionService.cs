using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using SalesTransactionService.Models;

namespace SalesTransactionService.SalesTransaction
{
    public class SalesTransactionService : ISalesTransactionService
    {
        private IDatabaseService _databaseService;

        public SalesTransactionService(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task AddSalesTransaction(SalesTransactionModel salesTransaction)
        {
            DynamicParameters parameters = GetParameters(salesTransaction);
            using (var conn = await _databaseService.GetConnection())
            {
                 conn.Query<SalesTransactionModel>("SP_InsertSalesTransaction", parameters,null,true,null,CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<SalesTransactionModel>> GetSalesTransaction()
        {
            using (var conn = await _databaseService.GetConnection())
            {
                return conn.Query<SalesTransactionModel>("SP_GetSalesTransaction", null, null, true, null,
                    CommandType.StoredProcedure);
            }
        }

        public async Task UpdateSalesTransaction(string id, SalesTransactionModel salesTransaction)
        {
            DynamicParameters parameters = GetParameters(salesTransaction);
            parameters.Add("@Id", id);
            using (var conn = await _databaseService.GetConnection())
            {
                conn.Query<SalesTransactionModel>("SP_UpdateSalesTransaction", parameters,null,true,null,CommandType.StoredProcedure);
            }
        }

        public async Task<SalesTransactionModel> GetSalesTransactionById(string id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            using (var conn = await _databaseService.GetConnection())
            {
                return conn.Query<SalesTransactionModel>("SP_GetSalesTransactionById", parameters, null, true, null,
                    CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        private DynamicParameters GetParameters(SalesTransactionModel salesTransaction)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@CustomerId", salesTransaction.CustomerIdFK);
            parameters.Add("@ProductId", salesTransaction.ProductIdFK);
            parameters.Add("@Rate", salesTransaction.Rate);
            parameters.Add("@Quantity", salesTransaction.Quantity);
            parameters.Add("@Total", salesTransaction.Rate * salesTransaction.Quantity);
            return parameters;
        }
    }
}