using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Models.Products.Components
{
    public class RandomAccessMemory : Component
    {
        public RandomAccessMemory(int id, string manufacturer, string model, decimal price, double overallPerformance) : base(id, manufacturer, model, price, overallPerformance)
        {
            this.OverallPerformance *= 1.20;

        }
    }
}
