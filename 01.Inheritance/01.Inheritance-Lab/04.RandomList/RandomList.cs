using System;
using System.Collections.Generic;
using System.Text;

namespace CustomRandomList
{
    public class RandomList : List<string>
    {
        private Random rnd;
        public RandomList(Random rnd)
        {
            this.rnd = rnd;
        }

        public string RandomString()
        {
            int index = rnd.Next(0, this.Count) ;
            string str = this[index];
            this.RemoveAt(index);

            return str;
        }
    }
}
