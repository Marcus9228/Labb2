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
        public string membership = "Gold";

        public GoldCustomer(string name, string password) : base(name, password)
        {
            this.name = name;
            this.password = password;
            cart = new List<Product>();
            Store.usedNames.Add(name);
        }

        public override double PriceOfItems()
        {
            Console.WriteLine("You are a gold member so your price is reduced by 15%");
            return Math.Round((base.PriceOfItems() * 0.85), 1);
        }
        public override string GetMembership()
        {
            return membership;
        }
    }
}
