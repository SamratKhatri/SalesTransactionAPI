using Dapper;
using SalesTransactionService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTransactionService.Customer
{
    public class CustomerService:ICustomerService
    {
        private IDatabaseService _dataService;

        public CustomerService(IDatabaseService dataService)
        {
            _dataService= dataService;
        }

        public async Task AddCustomer(CustomerModel customer)
        {
            using (var conn = await _dataService.GetConnection())
            {
                await conn.ExecuteAsync("SP_InsertProduct", new { customer.CustomerName, customer.CustomerAddress, customer.ContactNo });
            }
        }



        public async Task<IEnumerable<CustomerModel>> GetCustomer()
        {
            using (var conn = await _dataService.GetConnection())
            {
                return await conn.QueryAsync<CustomerModel>("SP_GetAllCustomers");
            }
        }
    }
}
