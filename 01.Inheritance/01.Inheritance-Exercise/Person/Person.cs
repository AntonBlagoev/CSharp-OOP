using System;
using System.Collections.Generic;
using System.Text;

namespace Person
{
    public class Person
    {
        //private string name;
        private int age;

        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public virtual string Name { get; set; }
        public virtual int Age
        {
            get { return age; }
            set 
            { 
                if (value > 0) age = value;
            }
        }


        public override string ToString()
        {
            //StringBuilder sb = new StringBuilder();
            //sb.Append($"Name: {Name}, Age: {Age} ");
            //sb.Append(String.Format("Name: {0}, Age: {1}", this.Name, this.Age));
            //return sb.ToString().TrimEnd();
            return $"Name: {Name}, Age: {Age}";
        }

    }
}
