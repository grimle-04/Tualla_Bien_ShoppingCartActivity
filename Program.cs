using System;
namespace HelloWorld;

class Product
{
    public int Id;
    public string Name;
    public string Category;
    public double Price;
    public int RemainingStock;

    public Product(int id, string name, string category, double price, int stock)
    {
        Id = id;
        Name = name;
        Category = category;
        Price = price;
        RemainingStock = stock;
    }

    public void DisplayProduct()
    {
        Console.WriteLine("[Id: " + Id + "]");
        Console.WriteLine("[Name: " + Name + "]");
        Console.WriteLine("[Price: ₱" + Price + "]");
        Console.WriteLine("[RemainingStock: " + RemainingStock + "]");
        Console.WriteLine();
    }

    public double GetItemTotal(int quantity)
    {
        return Price * quantity;
    }

    public bool HasEnoughStock(int quantity)
    {
        return RemainingStock >= quantity;
    }

    public void DeductStock(int quantity)
    {
        RemainingStock -= quantity;
    }

    public void RestoreStock(int quantity)
    {
        RemainingStock += quantity;
    }
}

class CartItem
{
    public Product Product;
    public int Quantity;
    public double Subtotal;

    public CartItem(Product product, int quantity)
    {
        Product = product;
        Quantity = quantity;
        Subtotal = product.GetItemTotal(quantity);
    }
}

class Order
{
    public int ReceiptNumber;
    public DateTime DateTime;
    public CartItem[] Items;
    public int ItemCount;
    public double GrandTotal;
    public double Discount;
    public double FinalTotal;
    public double Payment;
    public double Change;
}

class Program
{
    static int receiptCounter = 1;
    static Order[] orderHistory = new Order[100];
    static int orderCount = 0;

    static void Main(string[] args)
    {
        Product[] menu = {
            new Product(1, "Infinix",  "Electronics", 4500.00,  50),
            new Product(2, "Oppo",     "Electronics", 5500.00,  30),
            new Product(3, "Banana",   "Food",        25.00,    75),
            new Product(4, "Apple",    "Food",        30.00,    85),
            new Product(5, "T-Shirt",  "Clothing",    500.00,   67),
            new Product(6, "Pants",    "Clothing",    750.00,   76),
        };

        CartItem[] cart = new CartItem[10];
        int cartCount = 0;

        bool running = true;

        while (running)
        {
            Console.WriteLine("\n===== MAIN MENU ======");
            Console.WriteLine();
            Console.WriteLine("[1] Browse All Products");
            Console.WriteLine("[2] Search Product by Name");
            Console.WriteLine("[3] Filter by Category");
            Console.WriteLine("[4] View / Manage Cart");
            Console.WriteLine("[5] View Order History");
            Console.WriteLine("[6] Exit");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    ShowAllProducts(menu);
                    AddToCart(menu, cart, ref cartCount);
                    break;
                case "2":
                    SearchProduct(menu);
                    AddToCart(menu, cart, ref cartCount);
                    break;
                case "3":
                    FilterByCategory(menu);
                    AddToCart(menu, cart, ref cartCount);
                    break;
                case "4":
                    CartMenu(cart, ref cartCount, menu);
                    break;
                case "5":
                    ShowOrderHistory();
                    break;
                case "6":
                    running = false;
                    Console.WriteLine("Thank you for visiting! Goodbye!");
                    break;
                default:
                    Console.WriteLine("Invalid option. Please enter 1 to 6 only.");
                    break;
            }
        }
    }

        static void ShowAllProducts(Product[] menu)
    {
        Console.WriteLine("===== STORE MENU =====");
        Console.WriteLine();
        foreach (Product p in menu)
        {
            if (p.RemainingStock == 0)
                Console.WriteLine($"[{p.Id}] {p.Name} - OUT OF STOCK");
            else
                p.DisplayProduct();
        }
    }

    static void SearchProduct(Product[] menu)
    {
        Console.Write("Enter product name to search: ");
        string keyword = Console.ReadLine().ToLower();
        bool found = false;

        Console.WriteLine("\n----- Search Results -----");
        Console.WriteLine();
        foreach (Product p in menu)
        {
            if (p.Name.ToLower().Contains(keyword))
            {
                p.DisplayProduct();
                found = true;
            }
        }

        if (!found)
            Console.WriteLine("No products found.");
    }
    
    static void FilterByCategory(Product[] menu)
    {
        Console.WriteLine("--- Select Category ---");
        Console.WriteLine("[1] Food");
        Console.WriteLine("[2] Electronics");
        Console.WriteLine("[3] Clothing");
        Console.WriteLine();
        Console.Write("Choose category: ");

        string input = Console.ReadLine();
        string category = "";

        if (input == "1") category = "Food";
        else if (input == "2") category = "Electronics";
        else if (input == "3") category = "Clothing";
        else { Console.WriteLine("Invalid category."); return; }

        Console.WriteLine($"\n--- {category} Products ---");
        bool found = false;

        foreach (Product p in menu)
        {
            if (p.Category == category)
            {
                p.DisplayProduct();
                found = true;
            }
        }

        if (!found)
            Console.WriteLine();
        Console.WriteLine("No products in this category.");
    }

    static void AddToCart(Product[] menu, CartItem[] cart, ref int cartCount)
    {
        Console.Write("\nEnter product number to add to cart (0 to go back): ");
        string input = Console.ReadLine();
        int productId;

        if (!int.TryParse(input, out productId) || productId == 0)
            return;

        Product chosen = null;
        foreach (Product p in menu)
        {
            if (p.Id == productId) { chosen = p; break; }
        }

        if (chosen == null) { Console.WriteLine("Product not found!"); return; }
        if (chosen.RemainingStock == 0) { Console.WriteLine("Sorry, this product is out of stock!"); return; }

        Console.Write("Enter quantity: ");
        string qInput = Console.ReadLine();
        Console.WriteLine();
        int quantity;

        if (!int.TryParse(qInput, out quantity) || quantity <= 0)
        {
            Console.WriteLine("Invalid quantity!");
            return;
        }

        if (!chosen.HasEnoughStock(quantity))
        {
            Console.WriteLine();
            Console.WriteLine($"Not enough stock! Only {chosen.RemainingStock} left.");
            return;
        }

        if (cartCount == 10) { Console.WriteLine("Cart is full!"); return; }
        

            bool found = false;
            for (int i = 0; i < cartCount; i++)
            {
                if (cartProducts[i].Id == chosen.Id)
                {
                    cartQuantities[i] += quantity;
                    cartSubtotals[i] += chosen.GetItemTotal(quantity);
                    found = true;
                    Console.WriteLine($"{chosen.Name} updated in cart!");
                    break;
                }
            }

            chosen.DeductStock(quantity);

            Console.Write("\nAdd another item? (Y/N): ");
            addMore = Console.ReadLine().ToUpper();

            while (addMore != "Y" && addMore != "N")
            {
                Console.WriteLine("Invalid input! Please enter Y or N only.");
                Console.Write("Add another item? (Y/N): ");
                addMore = Console.ReadLine().ToUpper();
            }
        }

        Console.WriteLine("\n===== RECEIPT =====");
        Console.WriteLine("\nThanks for buying!");
        Console.WriteLine();
        double grandTotal = 0;

        for (int i = 0; i < cartCount; i++)
        {
            Console.WriteLine($"{cartProducts[i].Name} x{cartQuantities[i]} = ₱{cartSubtotals[i]:F2}");
            grandTotal += cartSubtotals[i];
        }

        Console.WriteLine($"\nGrand Total: ₱{grandTotal:F2}");

        double finalTotal = grandTotal;

        if (grandTotal >= 5000)
        {
            double discount = grandTotal * 0.10;
            finalTotal = grandTotal - discount;
            Console.WriteLine();
            Console.WriteLine("You have 10% discount on your overall purchase!");
            Console.WriteLine();
            Console.WriteLine($"Discount (10%): -₱{discount:F2}");
        }

        Console.WriteLine();
        Console.WriteLine($"Final Total: ₱{finalTotal:F2}");

        Console.WriteLine("\n===== UPDATED STOCK =====");
        foreach (Product p in menu)
        {
            p.DisplayProduct();
        }

    }
}

