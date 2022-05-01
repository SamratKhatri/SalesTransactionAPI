using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesTransactionService.Customer;
using SalesTransactionService.Models;
using System.Threading.Tasks;

namespace SalesTransactionWebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        ICustomerService cs;
        public CustomerController(ICustomerService cs)
        {
            this.cs = cs;
        }

        public async Task<IActionResult> AddCustomer([FromBody] CustomerModel customer)
        {
            await cs.AddCustomer(customer);
            return Ok();
        }

        public async Task<IActionResult> GetCustomer()
        {
            return Ok(await cs.GetCustomer());
        }
    }
}
