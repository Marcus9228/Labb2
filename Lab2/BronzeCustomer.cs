using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    class BronzeCustomer : Customer
    {
        private string name;
        private string password;
        private List<Product> cart;
        public string membership = "Bronze";

        public BronzeCustomer(string name, string password) : base(name, password)
        {
            this.name = name;
            this.password = password;
            cart = new List<Product>();
            Store.usedNames.Add(name);
        }

        public override double PriceOfItems()
        {
            Console.WriteLine("You are a bronze member so your price is reduced by 5%");
            return Math.Round((base.PriceOfItems() * 0.95), 1);
        }
        public override string GetMembership()
        {
            return membership;
        }
    }
}
