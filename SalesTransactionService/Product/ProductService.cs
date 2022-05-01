using SalesTransactionService.Models;
using Dapper;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SalesTransactionService
{
    public class ProductService : IProductService
    {
        private IDatabaseService _dataService;

        public ProductService(IDatabaseService dataService)
        {
            _dataService = dataService;
        }

        public async Task AddProduct(ProductModel product)
        {
            using (var conn = await _dataService.GetConnection())
            {
                await conn.ExecuteAsync("SP_InsertProduct", new { ProductName = product.ProductName, ProductRate = product.ProductRate}, commandType: System.Data.CommandType.StoredProcedure);
            }
        }
        
       

        public async Task<IEnumerable<ProductModel>> GetProduct()
        {
            using (var conn = await _dataService.GetConnection())
            {
                return await conn.QueryAsync<ProductModel>("SP_GetAllProducts");
            }
        }

    }
}