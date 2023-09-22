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
        public static List<string> usedNames = new List<string>();
        public static string currency = "SEK";
        string[] usernames = File.ReadAllLines(@"C:\Users\marcu\source\repos\Lab2-master\Lab2\Usernames.txt");
        string[] passwords = File.ReadAllLines(@"C:\Users\marcu\source\repos\Lab2-master\Lab2\Passwords.txt");
        string[] memberships = File.ReadAllLines(@"C:\Users\marcu\source\repos\Lab2-master\Lab2\Memberships.txt");
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
            for (int i = 0; i < usernames.Length; i++)
            {
                if (memberships[i] == "Normal")
                {
                    Customer user = new Customer(usernames[i], passwords[i], memberships[i]);
                    customers.Add(user);
                } else if (memberships[i] == "Bronze")
                {
                    BronzeCustomer user = new BronzeCustomer(usernames[i], passwords[i], memberships[i]);
                    customers.Add(user);
                } else if (memberships[i] == "Silver")
                {
                    SilverCustomer user = new SilverCustomer(usernames[i], passwords[i], memberships[i]);
                    customers.Add(user);
                } else
                {
                    GoldCustomer user = new GoldCustomer(usernames[i], passwords[i], memberships[i]);
                    customers.Add(user);
                }
            }
            Customer knatte = new("Knatte", "123", "Normal");
            Customer fnatte = new("Fnatte", "321", "Normal");
            Customer tjatte = new("Tjatte", "213", "Normal");
        }

        public void Login()
        {
            Console.Clear();
            Console.WriteLine("Login:\nEnter username:");
            string username = Console.ReadLine();
            Console.WriteLine("Enter Password");
            string password = Console.ReadLine();
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
            loggedInCustomer[0].ClearCart();
            loggedInCustomer.Clear();
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
                    string password = Console.ReadLine();
                    Console.WriteLine("Enter what membership you want, Normal, Bronze, Silver or Gold");
                    string membership = Console.ReadLine();
                    if (membership == "Normal" || membership == "Bronze" || membership == "Silver" || membership == "Gold" )
                    {
                        Customer customer = new Customer(userName, password, membership);
                        customers.Add(customer);
                        Console.Clear();
                        Console.WriteLine("Thanks for registering your account " + userName);
                        Console.WriteLine("Please restart application to activate your membership discount");
                        Console.ReadKey();
                        Menu();
                    } else
                    {
                        Console.WriteLine("Invalid membership input");
                    }
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

        public void TopMenuInfo()
        {
            Console.Clear();
            Console.WriteLine("User: " + loggedInCustomer[0].Name);
            Console.WriteLine($"Membership: {loggedInCustomer[0].Membership}: {loggedInCustomer[0].Discount()}");
            Console.WriteLine("Currency: " + currency);
            Console.WriteLine();
        }

        public void ItemMenu()
        {
            TopMenuInfo();
            loggedInCustomer[0].GetCartItems();
            Console.WriteLine("Enter the items corresponding number to add the item to you shoppingcart\n");
            Console.WriteLine($"1: Sausage. {products[0].GetPrice(loggedInCustomer[0].Membership)} {currency}");
            Console.WriteLine($"2: Red bull. {products[1].GetPrice(loggedInCustomer[0].Membership)} {currency}");
            Console.WriteLine($"3: Apple. {products[2].GetPrice(loggedInCustomer[0].Membership)} {currency}");
            Console.WriteLine("4: to go back to checkout menu");
            try
            {
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
            } catch
            {
                Console.Clear();
                ItemMenu();
            }
        }

        public void CheckoutMenu()
        {
            Console.WriteLine($"You went to the cashier and paid: {loggedInCustomer[0].PriceOfItems()} {currency}");
            loggedInCustomer[0].ClearCart();
            Console.ReadKey();
        }

        public void CartMenu()
        {
            if (loggedInCustomer[0].Cart.Count > 0)
            {
                TopMenuInfo();
                Console.WriteLine($"Remove items by entering corresponding number to the item: ");
                Console.WriteLine($"1: Remove 1 Sausage: you have {loggedInCustomer[0].GetSausage()} in cart");
                Console.WriteLine($"2: Remove 1 Red Bull: you have {loggedInCustomer[0].GetRedbull()} in cart");
                Console.WriteLine($"3: Remove 1 Apple: you have {loggedInCustomer[0].GetApple()} in cart");
                Console.WriteLine($"4: back to Shopping menu");
                Console.WriteLine();
                Console.WriteLine($"6: Clear shopping cart from all items");
                Console.WriteLine();
                Console.WriteLine($"Total price for all items: {loggedInCustomer[0].PriceOfItems()} {currency}");
                try
                {
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
                } catch
                {
                    Console.Clear();
                    CartMenu();
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
            TopMenuInfo();
            Console.WriteLine("1: See items");
            Console.WriteLine("2: Shopping cart");
            Console.WriteLine("3: Checkout");
            Console.WriteLine("4: Main menu");
            try
            {
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
            } catch
            {
                Console.Clear();
                ShoppingMenu();
            }
        }

        public void Menu()
        {
            if (loggedInCustomer.Count < 1)
            {
                Console.WriteLine("1: Log in");
                Console.WriteLine("2: Register");
                Console.WriteLine("3: Exit the store");
                try
                {
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
                            break;
                    }
                }
                catch
                {
                    Console.Clear();
                    Menu();
                }
            }
            else
            {
                TopMenuInfo();
                Console.WriteLine("1: Enter store");
                Console.WriteLine("2: Change user");
                Console.WriteLine("3: Change currency");
                Console.WriteLine("4: Logout");
                Console.WriteLine("6: See all account information");
                try 
                {
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
                            Menu();
                            break;
                        case 6:
                            Console.Clear();
                            Console.WriteLine(loggedInCustomer[0].ToString());
                            Console.WriteLine("Press any key to return to the menu");
                            Console.ReadKey();
                            Console.Clear();
                            Menu();
                            break;
                    }
                }
                catch
                {
                    Console.Clear();
                    Menu();
                }
            }
        }

        public void ChangeCurrency()
        {
            Console.WriteLine("Enter the corresponding number to the currency you wish to use!");
            Console.WriteLine("1: SEK");
            Console.WriteLine("2: EURO");
            Console.WriteLine("3: USD");
            try
            {
                int userInput = Convert.ToInt32(Console.ReadLine());
                switch (userInput)
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
            } catch
            {
                Console.Clear() ;
                ChangeCurrency();
            }
            Console.Clear();
            Menu();
        }

        public List<Customer> GetCustomers()
        {
            return customers;
        }
    }
}
