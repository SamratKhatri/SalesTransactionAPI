using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesTransactionService.Customer;
using SalesTransactionService.Models;
using System.Threading.Tasks;
using SalesTransactionService.Invoice;
using SalesTransactionService.SalesTransaction;

namespace SalesTransactionWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        IInvoiceService invoiceService;
        public InvoiceController(IInvoiceService invoiceService)
        {
            this.invoiceService = invoiceService;
        }
        
        [HttpPost(nameof(AddInvoice))]
        public async Task<IActionResult> AddInvoice([FromBody] InvoiceModel transaction)
        {
            await invoiceService.AddInvoice(transaction);
            return Ok();
        }

        [HttpGet(nameof(GetInvoice))]
        public async Task<IActionResult> GetInvoice()
        {
            return Ok(await invoiceService.GetInvoice());
        }
        
        [HttpGet("GetInvoiceById/{id}")]
        public async Task<IActionResult> GetInvoiceById(string id)
        {
            return Ok(await invoiceService.GetInvoiceById(id));
        }
        
        
    }
}
