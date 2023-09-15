using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    internal class Product
    {
        private string name;
        private double price;

        public Product(string name, int price)
        {
            this.name = name;
            this.price = price;
        }
        public string GetName()
        {
            return name;
        }
        private void SetName(string newName)
        {
            this.name = newName;
        }
        public double GetPrice()
        {
            double priceWithCurrency = price;
            if (Store.currency == "SEK")
            {
                priceWithCurrency = price;
            } else if (Store.currency == "EURO")
            {
                priceWithCurrency = (double)(price / 12);
            } else
            {
                priceWithCurrency = (double)(price / 10);
            }
            return Math.Round(priceWithCurrency, 1);
        }
    }
}
