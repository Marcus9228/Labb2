﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    internal class Customer
    {
        private string name;
        private string password;
        private List<Product> cart;
        private string membership = "Normal";

        public Customer(string name, string password)
        {
            this.name = name;
            this.password = password;
            cart = new List<Product>();
            Store.usedNames.Add(name);
        }
        public bool ConfirmLogin(string userName, string password)
        {
            if ((password == this.password) && (this.name == userName))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void AddItem(Product item)
        {
            cart.Add(item);
            Console.WriteLine(item.GetName() + " was added to cart");
        }
        public void RemoveItem(Product item)
        {
            try
            {
                cart.Remove(item);
                Console.WriteLine(item.GetName() + " was removed from cart");
            }
            catch
            {
                Console.WriteLine("None of that items are in your cart");
            }
        }
        public void ClearCart()
        {
            cart.Clear();
        }
        public virtual double PriceOfItems()
        {
            double sum = 0;
            foreach (Product item in cart)
            {
                sum += item.GetPrice();
            }
            return Math.Round(sum, 1);
        }
        public string GetName()
        {
            return this.name;
        }
        public List<Product> GetCart()
        {
            return cart;
        }
        public void GetCartItems()
        {
            int sausages = 0;
            int redbulls = 0;
            int apples = 0;
            Console.WriteLine("Items in cart:");
            foreach (Product item in cart)
            {
                if (item.GetName() == "Sausage")
                {
                    sausages += 1;
                }
                else if (item.GetName() == "Red Bull")
                {
                    redbulls += 1;
                }
                else
                {
                    apples += 1;
                }
            }
            Console.WriteLine(sausages + " Sausages.");
            Console.WriteLine(redbulls + " Red Bull.");
            Console.WriteLine(apples + " Apples.");
            Console.WriteLine($"Current total price: {PriceOfItems()} {Store.currency}");
            Console.WriteLine();
        }
        public int GetKorv()
        {
            int sausages = 0;
            foreach (Product item in cart)
            {
                if (item.GetName() == "Sausage")
                {
                    sausages += 1;
                }
            }
            return sausages;
        }
        public int GetRedbull()
        {
            int redbull = 0;
            foreach (Product item in cart)
            {
                if (item.GetName() == "Red Bull")
                {
                    redbull += 1;
                }
            }
            return redbull;
        }
        public int GetApple()
        {
            int apple = 0;
            foreach (Product item in cart)
            {
                if (item.GetName() == "Apple")
                {
                    apple += 1;
                }
            }
            return apple;
        }
        public virtual string GetMembership()
        {
            return membership;
        }
    }
}
