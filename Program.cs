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


        public Product(int id, string name, double price, int remainingStock)

        {
            this.Id = id;
            this.Name = name;
            this.Price = price;
            this.RemainingStock = remainingStock;
        }
