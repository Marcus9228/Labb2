using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    class GoldCustomer : Customer
    {
        private string name;
        private string password;
        private List<Product> cart;
        private string membership;

        public GoldCustomer(string name, string password, string membership) : base(name, password, membership)
        {
            this.membership = membership;
            this.name = name;
            this.password = password;
            cart = new List<Product>();
            Store.usedNames.Add(name);
        }
        public override string Discount()
        {
            return "Discount: 15%";
        }
    }
}
