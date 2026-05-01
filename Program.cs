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

        Console.WriteLine($"\n----- {category} Products -----");
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
            if (p.Id == productId) { chosen = p; 
            break; }
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

        if (cartCount == 10) 
        { Console.WriteLine("Cart is full!"); 
         return; 
        }
        
        bool alreadyInCart = false;
        for (int i = 0; i < cartCount; i++)
        {
            if (cart[i].Product.Id == chosen.Id)
            {
                cart[i].Quantity += quantity;
                cart[i].Subtotal += chosen.GetItemTotal(quantity);
                alreadyInCart = true;
                Console.WriteLine($"{chosen.Name} quantity updated in cart!");
                break;
            }
        }

        if (!alreadyInCart)
        {
            cart[cartCount] = new CartItem(chosen, quantity);
            cartCount++;
            Console.WriteLine($"{chosen.Name} added to cart!");
        }

        chosen.DeductStock(quantity);
    }

    static void CartMenu(CartItem[] cart, ref int cartCount, Product[] menu)
    {
        bool inCartMenu = true;

        while (inCartMenu)
        {
            Console.WriteLine("\n===== CART MENU =====");
            Console.WriteLine();
            Console.WriteLine("[1] View Cart");
            Console.WriteLine("[2] Remove an Item");
            Console.WriteLine("[3] Update Item Quantity");
            Console.WriteLine("[4] Clear Cart");
            Console.WriteLine("[5] Checkout");
            Console.WriteLine("[6] Back to Main Menu");
            Console.WriteLine();
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    ViewCart(cart, cartCount);
                    break;
                case "2":
                    RemoveFromCart(cart, ref cartCount);
                    break;
                case "3":
                    UpdateQuantity(cart, cartCount);
                    break;
                case "4":
                    ClearCart(cart, ref cartCount);
                    break;
                case "5":
                    bool checkoutDone = Checkout(cart, ref cartCount, menu);
                    if (checkoutDone) inCartMenu = false;
                    break;
                case "6":
                    inCartMenu = false;
                    break;
                default:
                    Console.WriteLine("Invalid option. Please enter 1 to 6 only.");
                    break;
            }
        }
    }

    static void ViewCart(CartItem[] cart, int cartCount)
    {
        if (cartCount == 0)
        {
            Console.WriteLine("Your cart is empty.");
            return;
        }

        Console.WriteLine("===== YOUR CART =====");
        Console.WriteLine();
        double total = 0;

        for (int i = 0; i < cartCount; i++)
        {
            Console.WriteLine($"[{i + 1}] {cart[i].Product.Name} x{cart[i].Quantity} = ₱{cart[i].Subtotal:F2}");
            total += cart[i].Subtotal;
        }

        Console.WriteLine($"\nGrand Total: ₱{total:F2}");
    }

    static void RemoveFromCart(CartItem[] cart, ref int cartCount)
    {
        if (cartCount == 0) { Console.WriteLine("Cart is empty."); return; }

        ViewCart(cart, cartCount);
        Console.Write("\nEnter item number to remove: ");
        string input = Console.ReadLine();
        int index;

        if (!int.TryParse(input, out index) || index < 1 || index > cartCount)
        {
            Console.WriteLine("Invalid item number.");
            return;
        }

        index--;

        cart[index].Product.RestoreStock(cart[index].Quantity);
        Console.WriteLine($"{cart[index].Product.Name} removed from cart.");

        for (int i = index; i < cartCount - 1; i++)
            cart[i] = cart[i + 1];

        cart[cartCount - 1] = null;
        cartCount--;
    }

    static void UpdateQuantity(CartItem[] cart, int cartCount)
    {
        if (cartCount == 0) { Console.WriteLine("Cart is empty."); return; }

        ViewCart(cart, cartCount);
        Console.Write("\nEnter item number to update: ");
        string input = Console.ReadLine();
        int index;

        if (!int.TryParse(input, out index) || index < 1 || index > cartCount)
        {
            Console.WriteLine("Invalid item number.");
            return;
        }

        index--;

        Console.Write($"Enter new quantity (current: {cart[index].Quantity}): ");
        string qInput = Console.ReadLine();
        int newQty;

        if (!int.TryParse(qInput, out newQty) || newQty <= 0)
        {
            Console.WriteLine("Invalid quantity.");
            return;
        }

        int oldQty = cart[index].Quantity;

        cart[index].Product.RestoreStock(oldQty);

        if (!cart[index].Product.HasEnoughStock(newQty))
        {
            Console.WriteLine($"Not enough stock! Only {cart[index].Product.RemainingStock} available.");
            cart[index].Product.DeductStock(oldQty);
            return;
        }

        cart[index].Product.DeductStock(newQty);
        cart[index].Quantity = newQty;
        cart[index].Subtotal = cart[index].Product.GetItemTotal(newQty);
        Console.WriteLine("Quantity updated successfully!");
       }

        static void ClearCart(CartItem[] cart, ref int cartCount)
        {
        if (cartCount == 0) { Console.WriteLine("Cart is already empty."); return; }

        for (int i = 0; i < cartCount; i++)
            cart[i].Product.RestoreStock(cart[i].Quantity);

        for (int i = 0; i < cartCount; i++)
            cart[i] = null;

        cartCount = 0;
        Console.WriteLine("Cart has been cleared.");
        }

        static bool Checkout(CartItem[] cart, ref int cartCount, Product[] menu)
    {
        if (cartCount == 0)
        {
            Console.WriteLine("Your cart is empty! Add items before checking out.");
            return false;
        }

        double grandTotal = 0;
        for (int i = 0; i < cartCount; i++)
            grandTotal += cart[i].Subtotal;

        double discount = 0;
        if (grandTotal >= 5000)
            discount = grandTotal * 0.10;

        double finalTotal = grandTotal - discount;

        Console.WriteLine($"Final Total: ₱{finalTotal:F2}");

        double payment = 0;
        while (true)
        {
            Console.Write("Enter payment: ₱");
            string pInput = Console.ReadLine();

            if (!double.TryParse(pInput, out payment))
            {
                Console.WriteLine("Invalid input. Payment must be a number.");
                continue;
            }

            if (payment < finalTotal)
            {
                Console.WriteLine("Insufficient payment. Please try again.");
                continue;
            }

            break;
        }

        double change = payment - finalTotal;
        string receiptNo = receiptCounter.ToString("D4");
        DateTime now = DateTime.Now;

        Console.WriteLine("\n ========================================");
        Console.WriteLine("|               RECEIPT                  |");
        Console.WriteLine(" ========================================");
        Console.WriteLine();
        Console.WriteLine($" Receipt No : {receiptNo}");
        Console.WriteLine($" Date       : {now:MMMM dd, yyyy hh:mm tt}");
        Console.WriteLine(" ----------------------------------------");

        for (int i = 0; i < cartCount; i++)
            Console.WriteLine($" {cart[i].Product.Name,-15} x{cart[i].Quantity,-5} ₱{cart[i].Subtotal:F2}");

        Console.WriteLine(" ----------------------------------------");
        Console.WriteLine($" Grand Total  : ₱{grandTotal:F2}");

        if (discount > 0)
            Console.WriteLine($" Discount 10% : -₱{discount:F2}");

        Console.WriteLine($" Final Total  : ₱{finalTotal:F2}");
        Console.WriteLine($" Payment      : ₱{payment:F2}");
        Console.WriteLine($" Change       : ₱{change:F2}");
        Console.WriteLine(" ========================================");

        Order order = new Order();
        order.ReceiptNumber = receiptCounter;
        order.DateTime = now;
        order.Items = new CartItem[cartCount];
        order.ItemCount = cartCount;

        for (int i = 0; i < cartCount; i++)
            order.Items[i] = cart[i];

        order.GrandTotal = grandTotal;
        order.Discount = discount;
        order.FinalTotal = finalTotal;
        order.Payment = payment;
        order.Change = change;

        orderHistory[orderCount] = order;
        orderCount++;
        receiptCounter++;

        ShowLowStockAlert(menu);

        for (int i = 0; i < cartCount; i++)
            cart[i] = null;
        cartCount = 0;

        return true;
    }

    static void ShowLowStockAlert(Product[] menu)
    {
        bool hasAlert = false;

        Console.WriteLine("\n===== LOW STOCK ALERT =====");
        Cosnole.WriteLine();

        foreach (Product p in menu)
        {
            if (p.RemainingStock == 0)
            {
                Console.WriteLine($"[OUT OF STOCK] {p.Name}");
                hasAlert = true;
            }
            else if (p.RemainingStock <= 5)
            {
                Console.WriteLine($"[LOW STOCK] {p.Name} has only {p.RemainingStock} stock(s) left!");
                hasAlert = true;
            }
        }

        if (!hasAlert)
            Console.WriteLine("All products have sufficient stock.");
    } 

        Console.WriteLine("\n===== UPDATED STOCK =====");
        foreach (Product p in menu)
        {
            p.DisplayProduct();
        }

    }
}

