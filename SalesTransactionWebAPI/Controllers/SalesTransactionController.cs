using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesTransactionService.Customer;
using SalesTransactionService.Models;
using System.Threading.Tasks;
using SalesTransactionService.SalesTransaction;

namespace SalesTransactionWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesTransactionController : ControllerBase
    {
        ISalesTransactionService salesTransactionService;
        public SalesTransactionController(ISalesTransactionService salesTransactionService)
        {
            this.salesTransactionService = salesTransactionService;
        }
        
        [HttpPost(nameof(AddSalesTransaction))]
        public async Task<IActionResult> AddSalesTransaction([FromBody] SalesTransactionModel transaction)
        {
            await salesTransactionService.AddSalesTransaction(transaction);
            return Ok();
        }

        [HttpGet(nameof(GetSalesTransaction))]
        public async Task<IActionResult> GetSalesTransaction()
        {
            return Ok(await salesTransactionService.GetSalesTransaction());
        }
        
        [HttpGet("getsalestransactionbyid/{id}")]
        public async Task<IActionResult> GetSalesTransactionById(string id)
        {
            return Ok(await salesTransactionService.GetSalesTransactionById(id));
        }
        
        [HttpPut("updatesalestransaction/{id}")]
        public async Task<IActionResult> UpdateSalesTransaction(string id, [FromBody] SalesTransactionModel transaction)
        {
            await salesTransactionService.UpdateSalesTransaction(id,transaction);
            return Ok();
        }
        
    }
}
