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
                Console.WriteLine("[Id: ]" + Id);
                Console.WriteLine("Name: " + Name);
                Console.WriteLine("Price: " + Price);
                Console.WriteLine("RemainingStock: " + RemainingStock);
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
    static void Main(string[] args)
    {
        Product[] menu = {
            new Product(1, "Apple",   25.00,  50),
            new Product(2, "Bread",   45.00,  30),
            new Product(3, "Milk",    80.00,  20),
            new Product(4, "Eggs",   120.00,  15),
            new Product(5, "Butter",  95.00,  10)
        };


