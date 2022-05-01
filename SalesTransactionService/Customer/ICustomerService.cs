using SalesTransactionService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTransactionService.Customer
{
    public  interface ICustomerService
    {
        Task AddCustomer(CustomerModel customer);
        Task<IEnumerable<CustomerModel>> GetCustomer();
    }
}
