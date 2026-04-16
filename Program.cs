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


        

        


