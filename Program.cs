# product class


using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp5
{
    public class Product

    {
            public int Id;
            public string Name;
            public double Price;
            public int RemainingStock;

                   public Product(int id, string name, double price, int stock)
        {
            Id = id;
            Name = name;
            Price = price;
            RemainingStock = stock;
        }

        public void DisplayProduct()
        {
            Console.WriteLine("[Id: " + Id + "]");
            Console.WriteLine("[Name: " + Name + "]");
            Console.WriteLine("[Price: " + Price + "]");
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
    }
}

# MAIN

using ConsoleApp5;
using Microsoft.VisualBasic;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;

internal class Program
{
    private static object addMore;
    
    static void Main(string[] args)
    {
        Product[] menu = {
           new Product(1,"Infinix",4500.00,50),
           new Product(2,"Oppo",5500.00,30),
           new Product(3,"Vivo",6000.00,20),
           new Product(4,"Samsung",10000.00,15),
           new Product(5,"Apple",20000.00,10)
        };

        Product[] cartProducts = new Product[10];
        int[] cartQuantities = new int[10];
        double[] cartSubtotals = new double[10];
        int cartCount = 0;
        addMore = "Y";

         while (addMore.ToUpper() == "Y")
         {
            Console.WriteLine("===== STORE MENU =====");
            foreach (Product p in menu)
         {
             if (p.RemainingStock == 0)
                 Console.WriteLine("[{p.Id}] {p.Name} - OUT OF STOCK");
             else
                 p.DisplayProduct();
         }

         Console.Write("\nEnter product number: ");
         string productInput = Console.ReadLine();
         int productId;

        if (!int.TryParse(productInput, out productId))
        {
            Console.WriteLine("Invalid input! Please enter a number.");
            continue;
        }

        Product chosen = null;
        foreach (Product p in menu)
        {
            if (p.Id == productId)
            {
                chosen = p;
                break;
            }
        }
        if (chosen == null)
        {
            Console.WriteLine("Product not found!");
            continue;
        }

        if (chosen.RemainingStock == 0)
        {
            Console.WriteLine("Sorry, this product is out of stock!");
            continue;
        }

        Console.Write("Enter quantity: ");
        string quantityInput = Console.ReadLine();
        int quantity;

        if (!int.TryParse(quantityInput, out quantity) || quantity <= 0)
        {
            Console.WriteLine("Invalid quantity!");
            continue;
        }

        if (!chosen.HasEnoughStock(quantity))
        {
            Console.WriteLine($"Not enough stock! Only {chosen.RemainingStock} left.");
            continue;
        }

        if (cartCount == 10)
        {
            Console.WriteLine("Cart is full!");
            continue;
        }

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

        if (!found)
        {
            cartProducts[cartCount] = chosen;
            cartQuantities[cartCount] = quantity;
            cartSubtotals[cartCount] = chosen.GetItemTotal(quantity);
            cartCount++;
            Console.WriteLine($"{chosen.Name} added to cart!");
        }

        chosen.DeductStock(quantity);

        Console.Write("\nAdd another item? (Y/N): ");
        addMore = Console.ReadLine();

        
        


        

        


