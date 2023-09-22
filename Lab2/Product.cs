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

        public double GetPrice(String membership)
        {
            double defaultPrice = this.price;
            if (membership == "Bronze")
            {
                defaultPrice = (this.price * 0.95);
            } else if (membership == "Silver")
            {
                defaultPrice = (this.price * 0.9);
            } else if (membership == "Gold")
            {
                defaultPrice = (this.price * 0.85);
            } else
            {
                defaultPrice = this.price;
            }
            double priceWithCurrency = defaultPrice;
            if (Store.currency == "SEK")
            {
                priceWithCurrency = defaultPrice;
            } else if (Store.currency == "EURO")
            {
                priceWithCurrency = (double)(defaultPrice / 12);
            } else
            {
                priceWithCurrency = (double)(defaultPrice / 10);
            }
            return Math.Round(priceWithCurrency, 1);
        }
    }
}
