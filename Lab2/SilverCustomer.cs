using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    class SilverCustomer : Customer
    {
        private string name;
        private string password;
        private List<Product> cart;
        public string membership = "Silver";

        public SilverCustomer(string name, string password) : base(name, password)
        {
            this.name = name;
            this.password = password;
            cart = new List<Product>();
            Store.usedNames.Add(name);
        }

        public override double PriceOfItems()
        {
            Console.WriteLine("You are a silver member so your price is reduced by 10%");
            return Math.Round((base.PriceOfItems() * 0.9), 1);
        }
        public override string GetMembership()
        {
            return membership;
        }
    }
}
