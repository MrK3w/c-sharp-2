using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Models
{
    class IgnoreAttribute : Attribute
    {

    }
    public class Customer
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public int Age { get; set; }

        [Ignore]
        public bool IsActive { get; set; }

        public Customer()
        {
        }
    }
}
