using System;
using System.Collections.Generic;
using System.Text;

namespace WildFarm.Exeptions
{
    public class FoodNotPreferredException : Exception
    {
        public FoodNotPreferredException(string message)
            : base(message)
        {
        }
    }
}
