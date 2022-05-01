using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTransactionService.Models
{
    public class CustomerModel
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string ContactNo { get; set; }
    }
}
