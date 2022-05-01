using SalesTransactionService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesTransactionService
{
    public interface IProductService
    {
        Task AddProduct(ProductModel product);
        Task<IEnumerable<ProductModel>> GetProduct();
    }
}