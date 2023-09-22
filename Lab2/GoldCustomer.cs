using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    class GoldCustomer : Customer
    {
        public GoldCustomer(string name, string password, string membership) : base(name, password, membership)
        {
            this.Membership = membership;
            this.Name = name;
            this.Password = password;
            this.Cart = new List<Product>();
            Store.usedNames.Add(name);
        }

        public override string Discount()
        {
            return "Discount: 15%";
        }
    }
}
