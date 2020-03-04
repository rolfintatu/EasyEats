using Domain.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{

    //TODO: Implement discount for a period of time
    public class Product : AuditableEntity
    {
        public Product()
        {

        }

        public Product(string name
            ,decimal price
            ,int quantity
            ,Category category
            ,int calories
            ,string description)
        {
            this.Name = name;
            this.Price = price;
            this.Category = category;
            this.Calories = calories;
            this.Description = description;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }
        public Category Category { get; private set; }
        public int Calories { get; private set; }
        public string Description { get; private set; }
        public bool OutOfStock { get; private set; } = false;

        public List<OrderItems> OrderProducts { get; set; }

        public  void IncreaseQuantity(int quantity)
        {
            this.Quantity += quantity;

            if (OutOfStock is true)
            {
                this.OutOfStock = false;
            }
        }

        public void DecreaseQuantity(int quantity)
        {
            if (quantity > this.Quantity)
            {
                throw new Exception($"Cant buy more then {this.Quantity} products.");
            }

            this.Quantity -= quantity;

            if (this.Quantity is 0)
            {
                this.OutOfStock = true;
            }
        }

        public void ChangePrice(decimal newPrice)
        {
            this.Price = newPrice;
        }
    }
}
