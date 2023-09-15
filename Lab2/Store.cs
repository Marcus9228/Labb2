using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace Lab2
{
    internal class Store
    {
        public List<Product> products;
        private List<Customer> customers;
        private List<Customer> loggedInCustomer;
        public static List<string> usedNames = new List<string>(); // static so name gets added when initializing customers.
        public static string currency = "SEK";
        public Store()
        {
            SetUpProducts();
            SetUpCustomers();
            Menu();
        }
        public void SetUpProducts() 
        {
            products = new List<Product>();
            Product sausage = new("Sausage", 20);
            Product redBull = new("Red Bull", 10);
            Product apple = new("Apple", 7);
            products.Add(sausage);
            products.Add(redBull);
            products.Add(apple);
        }
        public void SetUpCustomers()
        {
            loggedInCustomer = new List<Customer>();
            customers = new List<Customer>();
            Customer knatte = new("Knatte", "123");
            Customer fnatte = new("Fnatte", "321");
            Customer tjatte = new("Tjatte", "213");
            GoldCustomer MrGold = new("MrGold", "123");
            SilverCustomer MrSilver = new("MrSilver", "123");
            BronzeCustomer MrBronze = new("MrBronze", "123");
            customers.Add(knatte);
            customers.Add(fnatte);
            customers.Add(tjatte);
            customers.Add(MrGold);
            customers.Add(MrSilver);
            customers.Add(MrBronze);
        }
        public void Login()
        {
            Console.Clear();
            Console.WriteLine("Login:\nEnter username:");
            string username = Console.ReadLine();
            Console.WriteLine("Enter Password");
            Console.ForegroundColor = ConsoleColor.Black;
            string password = Console.ReadLine();
            Console.ForegroundColor= ConsoleColor.White;
            foreach (Customer customer in customers)
            {
                if (customer.ConfirmLogin(username, password))
                {
                    loggedInCustomer.Add(customer);
                }
            }
            if (loggedInCustomer.Count > 0)
            {
                Console.Clear();
                Console.WriteLine("You are now logged in as: " + username);
            }
            else
            {
                Console.Clear();
                if (usedNames.Contains(username))
                {
                    Console.WriteLine("Wrong password, please try again");
                } else
                {
                    Console.WriteLine("Username does not exist, please register or try again");
                }
            }
        }
        public void Logout()
        {
            Console.Clear();
            loggedInCustomer.Clear();
            Console.WriteLine("You have logged out");
        }
        public void Register()
        {
            Console.Clear();
            Console.WriteLine("Register: \nEnter the username you want: (minumum 3 letters) ");
            string userName = Console.ReadLine();
            if (!usedNames.Contains(userName))
            {
                if (userName.Length > 2)
                {
                    Console.WriteLine("Enter password");
                    Console.ForegroundColor = ConsoleColor.Black;
                    string password = Console.ReadLine();
                    Console.ForegroundColor = ConsoleColor.White;
                    Customer customer = new Customer(userName, password);
                    customers.Add(customer);
                    Console.Clear();
                    Console.WriteLine("Thanks for registering your account " + userName);
                    Menu();
                } else 
                {
                    Console.Clear();
                    Console.WriteLine("Username has to be 3 letters or more!");
                }
            } else
            {
                Console.Clear();
                Console.WriteLine("Username already in use please try again.");
            }
        }
        public void ItemMenu()
        {
            Console.Clear();
            loggedInCustomer[0].GetCartItems();
            Console.WriteLine("User: " + loggedInCustomer[0].GetName());
            Console.WriteLine("Currency: " + currency);
            Console.WriteLine("Enter the items corresponding number to add the item to you shoppingcart\n");
            Console.WriteLine($"1: Sausage. {products[0].GetPrice()} {currency}");
            Console.WriteLine($"2: Red bull. {products[1].GetPrice()} {currency}");
            Console.WriteLine($"3: Apple. {products[2].GetPrice()} {currency}");
            Console.WriteLine("4: to go back to checkout menu");
            int userInput = Convert.ToInt32(Console.ReadLine());
            switch (userInput)
            {
                case 1:
                    loggedInCustomer[0].AddItem(products[0]);
                    ItemMenu();
                    break;
                case 2:
                    loggedInCustomer[0].AddItem(products[1]);
                    ItemMenu();
                    break;
                case 3:
                    loggedInCustomer[0].AddItem(products[2]);
                    ItemMenu();
                    break;
                case 4:
                    Console.Clear();
                    ShoppingMenu();
                    break;
                default:
                    ItemMenu();
                    break;
            }
        }
        public void CheckoutMenu()
        {

        }
        public void CartMenu()
        {
            if (loggedInCustomer[0].GetCart().Count > 0)
            {
                Console.Clear();
                //loggedInCustomer[0].GetCartItems();
                Console.WriteLine($"User: {loggedInCustomer[0].GetName()}");
                Console.WriteLine($"Currency: {currency}");
                Console.WriteLine($"Remove items by entering corresponding number to the item: ");
                Console.WriteLine($"1: Remove 1 Sausage: you have {loggedInCustomer[0].GetKorv()} in cart");
                Console.WriteLine($"2: Remove 1 Red Bull: you have {loggedInCustomer[0].GetRedbull()} in cart");
                Console.WriteLine($"3: Remove 1 Apple: you have {loggedInCustomer[0].GetApple()} in cart");
                Console.WriteLine($"4: back to Shopping menu");
                Console.WriteLine();
                Console.WriteLine($"6: Clear shopping cart from all items");
                Console.WriteLine();
                Console.WriteLine($"Total price for all items: {loggedInCustomer[0].PriceOfItems()} {currency}");
                int userInput = Convert.ToInt32(Console.ReadLine());
                switch (userInput)
                {
                    case 1:
                        loggedInCustomer[0].RemoveItem(products[0]);
                        CartMenu();
                        break;
                    case 2:
                        loggedInCustomer[0].RemoveItem(products[1]);
                        CartMenu();
                        break;
                    case 3:
                        loggedInCustomer[0].RemoveItem(products[2]);
                        CartMenu();
                        break;
                    case 4:
                        Console.Clear();
                        ShoppingMenu();
                        break;
                    case 6:
                        Console.Clear();
                        loggedInCustomer[0].ClearCart();
                        CartMenu();
                        break;
                    default:
                        CartMenu();
                        break;
                }
            } else
            {
                Console.Clear();
                Console.WriteLine("Your shopping cart is empty!\n\nPress any key to return to store");
                Console.ReadKey();

                ShoppingMenu();
            }
        }
        public void ShoppingMenu()
        {
            Console.Clear();
            Console.WriteLine("User: " + loggedInCustomer[0].GetName());
            Console.WriteLine("Currency: " + currency);
            Console.WriteLine("Welcome inside the store!");
            Console.WriteLine("1: See items");
            Console.WriteLine("2: Shopping cart");
            Console.WriteLine("3: Checkout");
            Console.WriteLine("4: Main menu");
            int userInput = Convert.ToInt32(Console.ReadLine());
            switch (userInput)
            {
                case 1:
                    Console.Clear();
                    ItemMenu();
                    ShoppingMenu();
                    break;
                case 2:
                    Console.Clear();
                    CartMenu();
                    break;
                case 3:
                    Console.Clear();
                    CheckoutMenu();
                    break;
                case 4:
                    Console.Clear();
                    Menu();
                    break;
            }
        }
        public void Menu()
        {
            try
            {
                if (loggedInCustomer.Count < 1)
                {
                    //Console.WriteLine("Welcome our store!! To proceed please log in!");
                    //Console.WriteLine("No account? enter 2 to register!");
                    Console.WriteLine("1: Log in");
                    Console.WriteLine("2: Register");
                    Console.WriteLine("3: Exit the store");
                    int userInput = Convert.ToInt32(Console.ReadLine());
                    switch (userInput)
                    {
                        case 1:
                            Console.Clear();
                            Login();
                            Menu();
                            break;
                        case 2:
                            Console.Clear();
                            Register();
                            Menu();
                            break;
                        case 3:
                            Console.WriteLine("Thanks for using our store, Welcome back!");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("User: " + loggedInCustomer[0].GetName());
                    Console.WriteLine("Membership: " + loggedInCustomer[0].GetMembership());
                    Console.WriteLine("Currency: " + currency);
                    Console.WriteLine("1: Enter store");
                    Console.WriteLine("2: Change user");
                    Console.WriteLine("3: Change currency");
                    Console.WriteLine("4: Back to login menu");
                    int userInput = Convert.ToInt32(Console.ReadLine());
                    switch (userInput)
                    {
                        case 1:
                            Console.Clear();
                            ShoppingMenu();
                            Menu();
                            break;
                        case 2:
                            Console.Clear();
                            Logout();
                            Login();
                            Menu();
                            break;
                        case 3:
                            Console.Clear();
                            ChangeCurrency();
                            break;
                        case 4:
                            Logout();
                            Console.WriteLine("Thanks for using our store, Welcome back!");
                            break;
                    }
                }
            }
            catch
            {
                Console.Clear();
                Console.WriteLine("Invalid input, please enter the corresponding number");
                Menu();
            }

        }
        public void Settings()
        {

        }
        public void ChangeCurrency()
        {
            Console.WriteLine("Enter the corresponding number to the currency you wish to use!");
            Console.WriteLine("1: SEK");
            Console.WriteLine("2: EURO");
            Console.WriteLine("3: USD");
            int userInput = Convert.ToInt32(Console.ReadLine());
            switch(userInput)
            {
                case 1:
                    Console.WriteLine("All prices are now in SEK");
                    currency = "SEK";
                    break;
                case 2:
                    Console.WriteLine("All prices are now in EURO");
                    currency = "EURO";
                    break;
                case 3:
                    Console.WriteLine("All prices are now in USD");
                    currency = "USD";
                    break;
            }
            Console.WriteLine("Press any key to return to store menu");
            Console.ReadKey();
            Console.Clear();
            Menu();

        }
        public List<Customer> GetCustomers()
        {
            return customers;
        }
        public void LoggedInCustomer()
        {
            foreach (Customer user in loggedInCustomer)
            {
                Console.WriteLine(user.GetName());
            }
        }
    }
}
