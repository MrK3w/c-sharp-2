using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Library.Models;

namespace Library.Controllers
{
    class CustomerController
    {
        private static List<Customer> _customers = new List<Customer>
        {
            new Customer()
            {
                Id = 1,
                Name = "Jakub",
                Age = 15,
                IsActive = false
            },
            new Customer()
            {
                Id = 2,
                Name = "david",
                Age = 22,
                IsActive = true
            },
            new Customer()
            {
                Id = 3,
                Name = "robert",
                Age = 33,
                IsActive = true
            }
        };

        public string List(int limit)
        {
            StringBuilder allCustomers = new StringBuilder();
            int i = 1;
            foreach (var customer in _customers)
            {
                allCustomers.Append(
                    $"Customer id:{customer.Id} name:{customer.Name} age:{customer.Age} state:{customer.IsActive}\n");
                if(i == limit) break;
                i++;
            }

            return allCustomers.ToString();
        }

        public string Add(Customer customer)
        {
            _customers.Add(customer);
            return customer.Id.ToString();
        }
    }
}
