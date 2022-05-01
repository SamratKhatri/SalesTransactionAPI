using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesTransactionService;
using SalesTransactionService.Models;
using System;
using System.Threading.Tasks;

namespace SalesTransactionWebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        IProductService ps;
        public ProductController(IProductService ps)
        {
            this.ps = ps;
        }

        public async Task<IActionResult> AddProduct([FromBody]ProductModel product)
        {
            await ps.AddProduct(product);
            return Ok();
        }

        public async Task<IActionResult> GetProduct()
        {
            return Ok(await ps.GetProduct());
        }
    }
}
