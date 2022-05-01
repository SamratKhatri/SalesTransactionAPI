using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using SalesTransactionService.Models;

namespace SalesTransactionService.Invoice
{
    public class InvoiceService : IInvoiceService
    {
        private IDatabaseService _databaseService;

        public InvoiceService(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }
        public async Task AddInvoice(InvoiceModel invoice)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@InvoiceDate", DateTime.UtcNow);
            parameters.Add("@CustomerId", invoice.CustomerIdFK);
            parameters.Add("@SalesTransactionIds", string.Join(",", invoice.SalesTransactionIds));

            using (var conn = await _databaseService.GetConnection())
            {
                conn.Query<InvoiceModel>("SP_InsertInvoice", parameters,null,true,null,CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<InvoiceModel>> GetInvoice()
        {
            using (var conn = await _databaseService.GetConnection())
            {
                return conn.Query<InvoiceModel>("SP_GetInvoice", null, null, true, null,
                    CommandType.StoredProcedure);
            }
        }

        public async Task<InvoiceWithSalesTransactionModel> GetInvoiceById(string id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            using (var conn = await _databaseService.GetConnection())
            {
                var data =  conn.Query<InvoiceDbModel>("SP_GetInvoiceById", parameters, null, true, null,
                    CommandType.StoredProcedure);

                var salesTransaction = data?.Select(x => new SalesTransactionDataModel
                {
                    Quantity = x.Quantity,
                    Rate = x.Rate,
                    Total = x.IndividualTotal,
                    ProductName = x.ProductName
                });
                var commonData = data?.FirstOrDefault();
                return new InvoiceWithSalesTransactionModel
                {
                    CustomerName = commonData?.CustomerName, InvoiceDate = commonData?.InvoiceDate,
                    Total = commonData?.Total, SalesTransactionDetail = salesTransaction?.ToList(),
                    Id= commonData?.Id
                };
            }
        }
    }
}