using Domain.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Aggregates.CatalogAggregate
{

    //TODO: Implement discount for a period of time
    public class Product
    {
        public Product(){}

        public Product(string name, decimal price, int quantity, Category category
            , int calories, string description) 
            => (Name, Price, Category, Calories, Description) 
            = (name, price, category, calories, description);

        public Product(int id, string name, decimal price, int quantity, Category category
            , int calories, string description)
            : this(name, price, quantity, category, calories, description)
            => (this.Id) = (id);

        public int Id { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public int StockQuantity { get; private set; }
        public Category Category { get; private set; }
        public int Calories { get; private set; }
        public string Description { get; private set; }
        public bool OutOfStock { get; private set; } = false;
        public List<Image> Images { get; private set; }

        public void ChangePrice(decimal newPrice)
            => this.Price = newPrice;
    }
}
