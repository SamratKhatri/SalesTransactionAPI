using System.Collections.Generic;
using System.Threading.Tasks;
using SalesTransactionService.Models;

namespace SalesTransactionService.Invoice
{
    public interface IInvoiceService
    {
        Task AddInvoice(InvoiceModel invoice);
        Task<IEnumerable<InvoiceModel>> GetInvoice();
        Task<InvoiceWithSalesTransactionModel> GetInvoiceById(string id);
    }
}